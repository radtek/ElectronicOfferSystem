using BusinessData;
using BusinessData.Dal;
using Common;
using Common.Models;
using Prism.Commands;
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

        private ObservableCollection<Business<object>> businesses;
        public ObservableCollection<Business<object>> Businesses
        {
            get { return businesses; }
            set { SetProperty(ref businesses, value); }
        }

        private readonly IRegionManager RegionManager;
        public DelegateCommand<string> BusinessNavCommand { get; private set; }
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        public DelegateCommand<object> SelectBusinessCommand { get; set; }

        ProjectDal projectDal = new ProjectDal();

        public RealEstatePageViewModel(IRegionManager regionManager)
        {
            // 显示业务数据列表
            NaturalBuildingDal naturalBuildingDal = new NaturalBuildingDal();
            //Businesses = new ObservableCollection<object>(naturalBuildingDal.GetListBy((n) => n.ID != null));

            // 导航到不同的业务数据页面
            RegionManager = regionManager;
            BusinessNavCommand = new DelegateCommand<string>(Navigate);

            // 在项目列表选择一个项目之后执行
            SelectProjectCommand = new DelegateCommand<object>((obj) => {

                ListView listView = obj as ListView;
                Project = listView.SelectedItem as Project;
                // 加载该项目的数据
                Project = projectDal.InitialRealEstateProject(Project); 
                // 初始进入自然幢页面
                BusinessNavCommand.Execute("NaturalBuildingPage");
            });
            GlobalCommands.SelectProjectCommand.RegisterCommand(SelectProjectCommand);
            
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath == null) return;

            Businesses = new ObservableCollection<Business<object>>();
            switch (navigatePath)
            {
                case "NaturalBuildingPage":
                    // 加载数据列表
                    if (Project != null)
                    {
                        foreach (NaturalBuilding naturalBuilding in Project.NaturalBuildings)
                        {
                            Business<object> business = new Business<object>();
                            business.Name = naturalBuilding.ZRZH;
                            business.BusinessObject = naturalBuilding;
                            Businesses.Add(business);
                        }
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
        }

    }
}
