using BusinessData;
using BusinessData.Dal;
using BusinessData.Models;
using Common;
using Common.Enums;
using Common.Events;
using Common.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace RealEstateModule.ViewModels
{
    public class RealEstatePageViewModel : BindableBase
    {
        private Project project;
        public Project Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }

        public ERealEstatePage NavigatePath { get; set; }

        private ObservableCollection<Business> businesses;
        public ObservableCollection<Business> Businesses
        {
            get { return businesses; }
            set { SetProperty(ref businesses, value); }
        }

        public Business Business { get; set; }

        private IRegionManager RegionManager;
        private IEventAggregator EA;
        public DelegateCommand<ERealEstatePage?> BusinessNavCommand { get; private set; }
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        public DelegateCommand<object> SelectBusinessCommand { get; set; }
        public DelegateCommand AddBusinessCommand { get; set; }
        public DelegateCommand DelBusinessCommand { get; set; }

        ProjectDal ProjectDal = new ProjectDal();

        public RealEstatePageViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            EA = ea;
            RegionManager = regionManager;

            // 导航到不同的业务数据页面
            BusinessNavCommand = new DelegateCommand<ERealEstatePage?>(Navigate);
            // 在业务表执行增删改之后
            EA.GetEvent<RefreshBusinessEvent>().Subscribe((navPage)=> {
                BusinessNavCommand.Execute(navPage);
            });

            // 在项目列表选择一个项目之后执行
            SelectProjectCommand = new DelegateCommand<object>((obj) => {

                ListView listView = obj as ListView;
                Project = listView.SelectedItem as Project;
                // 加载该项目的数据
                //Project = ProjectDal.InitialRealEstateProject(Project); 
                // 初始进入自然幢页面
                BusinessNavCommand.Execute(ERealEstatePage.NaturalBuildingPage);
            });
            GlobalCommands.SelectProjectCommand.RegisterCommand(SelectProjectCommand);

            // 选中业务列表中的一项
            SelectBusinessCommand = new DelegateCommand<object>((obj)=> {
                // 加载业务数据
                ListView listView = obj as ListView;
                Business = listView.SelectedItem as Business;
            });
            GlobalCommands.SelectBusinessCommand.RegisterCommand(SelectBusinessCommand);

            // 新增
            AddBusinessCommand = new DelegateCommand(()=> {
                Navigate(NavigatePath);
            });

            DelBusinessCommand = new DelegateCommand(DelBusiness);
        }

        private void Navigate(ERealEstatePage? navigatePath)
        {
            if (Project == null) return;
            // 若不是楼盘项目，返回
            if (!"1".Equals(Project.Type)) return;

            // 点击业务的导航页后发送通知
            NavigatePath = (ERealEstatePage)navigatePath;
            EA.GetEvent<NavBusinessEvent>().Publish(NavigatePath);
            Businesses = new ObservableCollection<Business>();

            try
            {
                // 加载该项目的数据
                Project = ProjectDal.InitialRealEstateProject(Project);

                switch (NavigatePath)
                {
                    case ERealEstatePage.NaturalBuildingPage:
                        // 加载数据列表
                        foreach (NaturalBuilding naturalBuilding in Project.NaturalBuildings)
                        {
                            Business business = new Business();
                            business.Name = naturalBuilding.ZRZH;
                            business.NaturalBuilding = naturalBuilding;
                            Businesses.Add(business);
                        }
                        break;
                    case ERealEstatePage.LogicalBuildingPage:
                        foreach (LogicalBuilding logicalBuilding in Project.LogicalBuildings)
                        {
                            Business business = new Business();
                            business.Name = logicalBuilding.LJZH;
                            business.LogicalBuilding = logicalBuilding;
                            Businesses.Add(business);
                        }
                        break;
                    case ERealEstatePage.FloorPage:
                        foreach (Floor floor in Project.Floors)
                        {
                            Business business = new Business();
                            business.Name = floor.CH;
                            business.Floor = floor;
                            Businesses.Add(business);
                        }
                        break;
                    case ERealEstatePage.HouseholdPage:
                        foreach (Household household in Project.Households)
                        {
                            Business business = new Business();
                            business.Name = household.HBSM;
                            business.Household = household;
                            Businesses.Add(business);
                        }
                        break;
                    case ERealEstatePage.ObligeePage:
                        foreach (Obligee obligee in Project.Obligees)
                        {
                            Business business = new Business();
                            business.Name = obligee.QLRMC;
                            business.Obligee = obligee;
                            Businesses.Add(business);
                        }
                        break;
                    case ERealEstatePage.MortgagePage:
                        foreach (Mortgage mortgage in Project.Mortgages)
                        {
                            Business business = new Business();
                            business.Name = mortgage.QLRMC;
                            business.Mortgage = mortgage;
                            Businesses.Add(business);
                        }
                        break;
                    case ERealEstatePage.SequestrationPage:
                        foreach (Sequestration sequestration in Project.Sequestrations)
                        {
                            Business business = new Business();
                            business.Name = sequestration.DBR;
                            business.Sequestration = sequestration;
                            Businesses.Add(business);
                        }
                        break;
                    default:
                        break;

                }

                // 页面跳转
                var parameters = new NavigationParameters();
                parameters.Add("Project", Project);
                RegionManager.RequestNavigate("BusinessContentRegion", NavigatePath.ToString(), NavigationComplete, parameters);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }



        }

        private void NavigationComplete(NavigationResult result)
        {
            //System.Windows.MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
            //Businesses = null;
        }

        private void DelBusiness()
        {
            try
            {
                switch (NavigatePath)
                {
                    case ERealEstatePage.NaturalBuildingPage:
                        NaturalBuilding naturalBuilding = Business.NaturalBuilding;
                        NaturalBuildingDal naturalBuildingDal = new NaturalBuildingDal();
                        naturalBuildingDal.Del(naturalBuilding);
                        break;
                    case ERealEstatePage.LogicalBuildingPage:
                        LogicalBuilding logicalBuilding = Business.LogicalBuilding;
                        LogicalBuildingDal logicalBuildingDal = new LogicalBuildingDal();
                        logicalBuildingDal.Del(logicalBuilding);
                        break;
                    case ERealEstatePage.FloorPage:
                        Floor floor = Business.Floor;
                        FloorDal floorDal = new FloorDal();
                        floorDal.Del(floor);
                        break;
                    case ERealEstatePage.HouseholdPage:
                        Household household = Business.Household;
                        HouseholdDal householdDal = new HouseholdDal();
                        householdDal.Del(household);
                        break;
                    case ERealEstatePage.ObligeePage:
                        Obligee obligee = Business.Obligee;
                        ObligeeDal obligeeDal = new ObligeeDal();
                        obligeeDal.Del(obligee);
                        break;
                    case ERealEstatePage.MortgagePage:
                        Mortgage mortgage = Business.Mortgage;
                        MortgageDal mortgageDal = new MortgageDal();
                        mortgageDal.Del(mortgage);
                        break;
                    case ERealEstatePage.SequestrationPage:
                        Sequestration sequestration = Business.Sequestration;
                        SequestrationDal sequestrationDal = new SequestrationDal();
                        sequestrationDal.Del(sequestration);
                        break;
                    default:
                        break;
                }
                BusinessNavCommand.Execute(NavigatePath);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

        }

    }
}
