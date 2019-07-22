using BusinessData;
using BusinessData.Dal;
using BusinessData.Models;
using Common;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RealEstateModule.ViewModels
{
    class RealEstatePageViewModel : BindableBase
    {
        private Project project;
        public Project Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }

        public string NavigatePath { get; set; }

        private ObservableCollection<Business> businesses;
        public ObservableCollection<Business> Businesses
        {
            get { return businesses; }
            set { SetProperty(ref businesses, value); }
        }

        public Business Business { get; set; }

        private readonly IRegionManager RegionManager;
        IEventAggregator EA;
        public DelegateCommand<string> BusinessNavCommand { get; private set; }
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
            BusinessNavCommand = new DelegateCommand<string>(Navigate);
            // 在业务表执行增删改之后
            EA.GetEvent<PubSubEvent<string>>().Subscribe((navPage)=> {
                BusinessNavCommand.Execute(navPage);
            });

            // 在项目列表选择一个项目之后执行
            SelectProjectCommand = new DelegateCommand<object>((obj) => {

                ListView listView = obj as ListView;
                Project = listView.SelectedItem as Project;
                // 加载该项目的数据
                //Project = ProjectDal.InitialRealEstateProject(Project); 
                // 初始进入自然幢页面
                BusinessNavCommand.Execute("NaturalBuildingPage");
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

            DelBusinessCommand = new DelegateCommand(() => {
                switch (NavigatePath)
                {
                    case "NaturalBuildingPage":
                        NaturalBuilding naturalBuilding = Business.NaturalBuilding;
                        NaturalBuildingDal naturalBuildingDal = new NaturalBuildingDal();
                        naturalBuildingDal.Del(naturalBuilding);
                        break;
                    case "LogicalBuildingPage":
                        LogicalBuilding logicalBuilding = Business.LogicalBuilding;
                        LogicalBuildingDal logicalBuildingDal = new LogicalBuildingDal();
                        logicalBuildingDal.Del(logicalBuilding);
                        break;
                    case "FloorPage":
                        Floor floor = Business.Floor;
                        FloorDal floorDal = new FloorDal();
                        floorDal.Del(floor);
                        break;
                    case "HouseholdPage":
                        Household household = Business.Household;
                        HouseholdDal householdDal = new HouseholdDal();
                        householdDal.Del(household);
                        break;
                    case "ObligeePage":
                        Obligee obligee = Business.Obligee;
                        ObligeeDal obligeeDal = new ObligeeDal();
                        obligeeDal.Del(obligee);
                        break;
                    default:
                        break;
                }
                BusinessNavCommand.Execute(NavigatePath);
            });
        }

        private void Navigate(string navigatePath)
        {
            // 加载该项目的数据
            Project = ProjectDal.InitialRealEstateProject(Project);

            if (navigatePath == null) return;
            if (Project == null) return;
            NavigatePath = navigatePath;
            Businesses = new ObservableCollection<Business>();
            switch (navigatePath)
            {
                case "NaturalBuildingPage":
                    // 加载数据列表
                    foreach (NaturalBuilding naturalBuilding in Project.NaturalBuildings)
                    {
                        Business business = new Business();
                        business.Name = naturalBuilding.ZRZH;
                        business.NaturalBuilding = naturalBuilding;
                        Businesses.Add(business);                      
                    }
                    break;
                case "LogicalBuildingPage":
                    foreach (LogicalBuilding logicalBuilding in Project.LogicalBuildings)
                    {
                        Business business = new Business();
                        business.Name = logicalBuilding.LJZH;
                        business.LogicalBuilding = logicalBuilding;
                        Businesses.Add(business);
                    }
                    break;
                case "FloorPage":
                    foreach (Floor floor in Project.Floors)
                    {
                        Business business = new Business();
                        business.Name = floor.CH;
                        business.Floor = floor;
                        Businesses.Add(business);
                    }
                    break;
                case "HouseholdPage":
                    foreach (Household household in Project.Households)
                    {
                        Business business = new Business();
                        business.Name = household.HBSM;
                        business.Household = household;
                        Businesses.Add(business);
                    }
                    break;
                case "ObligeePage":
                    foreach (Obligee obligee in Project.Obligees)
                    {
                        Business business = new Business();
                        business.Name = obligee.QLRMC;
                        business.Obligee = obligee;
                        Businesses.Add(business);
                    }
                    break;
                default:
                    break;

            }

           

            // 页面跳转
            var parameters = new NavigationParameters();
            parameters.Add("Project", Project);
            RegionManager.RequestNavigate("BusinessContentRegion", navigatePath, NavigationComplete, parameters);
        }
        private void NavigationComplete(NavigationResult result)
        {
            //System.Windows.MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
            //Businesses = null;
        }

    }
}
