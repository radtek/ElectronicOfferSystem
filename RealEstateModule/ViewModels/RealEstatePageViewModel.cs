using BusinessData;
using BusinessData.Dal;
using Common;
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

        private ObservableCollection<Business> businesses;
        public ObservableCollection<Business> Businesses
        {
            get { return businesses; }
            set { SetProperty(ref businesses, value); }
        }

        private readonly IRegionManager RegionManager;
        public DelegateCommand<string> BusinessNavCommand { get; private set; }
        public DelegateCommand<object> SelectProjectCommand { get; set; }
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
                Project = projectDal.InitialRealEstateProject(Project); // 加载该项目的数据
            });
            GlobalCommands.SelectProjectCommand.RegisterCommand(SelectProjectCommand);

            // 加载数据
            if (Project != null)
            {
                foreach (NaturalBuilding n in Project.NaturalBuildings)
                {
                    Business business = new Business();
                    business.Name = n.ZRZH;
                    business.BusinessObject = n;
                    Businesses.Add(business);
                }
            }
        }

        private void Navigate(string navigatePath)
        {
            var parameters = new NavigationParameters();
            parameters.Add("ProjectID", Project.ID);

            if (navigatePath != null)
                RegionManager.RequestNavigate("BusinessContentRegion", navigatePath, NavigationComplete, parameters);
        }
        private void NavigationComplete(NavigationResult result)
        {
            //System.Windows.MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
        }

        public class Business
        {
            public string Name { get; set; }
            public object BusinessObject { get; set; }
        }
    }
}
