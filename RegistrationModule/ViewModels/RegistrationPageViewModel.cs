using BusinessData;
using BusinessData.Dal;
using BusinessData.Models;
using Common;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace RegistrationModule.ViewModels
{
    public class RegistrationPageViewModel : BindableBase
    {
        private readonly IRegionManager RegionManager;
        IEventAggregator EA;

        private Project project;
        public Project Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }

        public string NavigatePath { get; set; }

        public DelegateCommand<string> RegistrationNavCommand { get; private set; }
        public DelegateCommand<object> SelectProjectCommand { get; set; }

        ProjectDal ProjectDal = new ProjectDal();

        public RegistrationPageViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            EA = ea;
            RegionManager = regionManager;

            // 导航到不同的业务数据页面
            RegistrationNavCommand = new DelegateCommand<string>(Navigate);

            // 在项目列表选择一个项目之后执行
            SelectProjectCommand = new DelegateCommand<object>((obj) =>
            {

                ListView listView = obj as ListView;
                Project = listView.SelectedItem as Project;
                if (Project != null && "2".Equals(Project.Type))
                {
                    // 项目信息初始化
                    //Project = ProjectDal.InitialRegistrationProject(Project);
                    // 初始进入转移信息页面
                    RegistrationNavCommand.Execute("TransferPage");

                }

            });
            GlobalCommands.SelectProjectCommand.RegisterCommand(SelectProjectCommand);

        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath == null) return;
            if (Project == null) return;
            // 若不是登记项目，返回
            if (!"2".Equals(Project.Type)) return;
            // 加载该项目的数据
            Project = ProjectDal.InitialRegistrationProject(Project);


            NavigatePath = navigatePath;
            switch (navigatePath)
            {
                case "TransferPage":

                    break;
                case "FileManagerPage":

                    break;
                default:
                    break;

            }
            // 页面跳转
            var parameters = new NavigationParameters();
            parameters.Add("Project", Project);
            RegionManager.RequestNavigate("RegistrationContentRegion", navigatePath, NavigationComplete, parameters);
        }
        private void NavigationComplete(NavigationResult result)
        {
            //System.Windows.MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
            //Businesses = null;
        }
    }
}
