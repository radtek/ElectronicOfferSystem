using BusinessData;
using BusinessData.Dal;
using Common;
using Common.ViewModels;
using Common.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProjectModule.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectModule.ViewModels
{
    public class ProjectPageViewModel : BindableBase
    {
        private Project project;
        public Project Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }

        private string dialogTitle;
        public string DialogTitle
        {
            get { return dialogTitle; }
            set { SetProperty(ref dialogTitle, value); }
        }

        public string NavigatePath { get; set; }

        private bool isBtnEnabled;
        public bool IsBtnEnabled
        {
            get { return isBtnEnabled; }
            set { SetProperty(ref isBtnEnabled, value); }
        }


        private ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects
        {
            get { return projects; }
            set { SetProperty(ref projects, value); }
        }

        private Project selectedProject;
        public Project SelectedProject
        {
            get { return selectedProject; }
            set { SetProperty(ref selectedProject, value); }
        }

        private string searchProjectName;
        public string SearchProjectName
        {
            get { return searchProjectName; }
            set { SetProperty(ref searchProjectName, value); }
        }

        private string projectType;
        public string ProjectType
        {
            get { return projectType; }
            set { SetProperty(ref projectType, value); }
        }


        public AddOrEditProjectDialogViewModel AddOrEditProjectDialog { get; set; }

        /// <summary>
        /// 页面导航
        /// </summary>
        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand<string> OpenAddOrEditProjectDialogCommand { get; private set; }
        public DelegateCommand CancelAddOrEditProjectDialogCommand { get; set; }
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        public DelegateCommand DelProjectCommand { get; set; }
        public DelegateCommand SearchProjectCommand { get; set; }
        ProjectDal projectDal = new ProjectDal();

        public ProjectPageViewModel()
        {
            AddOrEditProjectDialog = AddOrEditProjectDialogViewModel.getInstance();

            // 页面导航
            NavigateCommand = new DelegateCommand<string>(Navigate);
            GlobalCommands.NavigateCommand.RegisterCommand(NavigateCommand);

            // 加载项目列表
            //Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => p.Type == 1));
            projects = new ObservableCollection<Project>();

            // 选中ListView中的一项
            SelectProjectCommand = new DelegateCommand<object>((obj) =>
            {
                ListView listView = obj as ListView;
                Project = listView.SelectedItem as Project;
                //if (Project == null) return;
            });
            GlobalCommands.SelectProjectCommand.RegisterCommand(SelectProjectCommand);

            // 关闭模态框
            CancelAddOrEditProjectDialogCommand = new DelegateCommand(() => {
                Project = null;
            });

            // 打开模态框
            OpenAddOrEditProjectDialogCommand = new DelegateCommand<string>((string dialogTitle) => {
                DialogTitle = dialogTitle;
                if ("新增项目".Equals(DialogTitle))
                {
                    Project = new Project();
                }
                var view = new AddOrEditProjectDialog();
                AddOrEditProjectDialog.DialogTitle = DialogTitle;
                AddOrEditProjectDialog.Project = Project;
                var result = DialogHost.Show(view, "RootDialog", ConfirAddOrEditProjectEventHandler);
                
            });
            // 删除项目
            DelProjectCommand = new DelegateCommand(DelProject);

            SearchProjectCommand = new DelegateCommand(()=> {
                Projects = new ObservableCollection<Project>(projectDal.GetListBy(p=>ProjectType.Equals(p.Type) && p.ProjectName.Contains(SearchProjectName)));
            });

            // 初始化
            Projects = new ObservableCollection<Project>(projectDal.GetListBy(p => !"-1".Equals(p.Type)));
            IsBtnEnabled = false;
        }

        private void ConfirAddOrEditProjectEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            //cancel the close...
            eventArgs.Cancel();

            DialogTitle = AddOrEditProjectDialog.DialogTitle;
            Project = AddOrEditProjectDialog.Project;

            try
            {
                switch (DialogTitle)
                {
                    case "新增项目":
                        AddProject();
                        break;
                    case "编辑项目":
                        UpdProject();
                        break;
                    default:
                        break;
                }
                // 显示加载1s
                eventArgs.Session.UpdateContent(new SampleProgressDialog());
                Task.Delay(TimeSpan.FromSeconds(0.5))
                    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                        TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().updateShow(ex, eventArgs.Session);
                return;
            }

        }

        private void Navigate(string navigatePath)
        {
            NavigatePath = navigatePath;
            IsBtnEnabled = true;
            switch (navigatePath)
            {
                case "IndexPage":
                    ProjectType = "-1";
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy(p=>!ProjectType.Equals(p.Type)));
                    IsBtnEnabled = false;
                    break;
                case "RealEstatePage":
                    ProjectType = "1";
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type) ));

                    //Project = Projects.FirstOrDefault();
                    SelectedProject = Projects.FirstOrDefault();
                    break;
                case "RegistrationPage":
                    ProjectType = "2";
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type) ));
                    //Project = Projects.FirstOrDefault();
                    SelectedProject = Projects.FirstOrDefault();
                    break;
                case "SettingPage":
                    ProjectType = "-1";
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy(p => !ProjectType.Equals(p.Type) ));
                    break;
                default:
                    break;
            }
            
        }

        private void AddProject()
        {
            if (string.IsNullOrWhiteSpace(NavigatePath))
            {
                MessageBox.Show("请选择项目类型", "提示");
                return;
            }
            // 新增项目的初始化
            Project.ID = Guid.NewGuid();
            Project.UptateTime = DateTime.Now;
            
            Project.State = "0";
            switch (NavigatePath)
            {
                case "RealEstatePage":
                    Project.Type = "1";
                    break;
                case "RegistrationPage":
                    Project.Type = "2";
                    // 同时新增转移信息
                    TransferDal transferDal = new TransferDal();
                    Transfer transfer = new Transfer();
                    transfer.ID = Guid.NewGuid();
                    transfer.ProjectID = Project.ID;
                    transfer.UpdateTime = DateTime.Now;
                    transferDal.Add(transfer);
                    break;
                default:
                    Project.Type = "-1";
                    break;
            }
                projectDal.Add(Project);

            RefreshProjectList();
        }
        /// <summary>
        /// 修改项目
        /// </summary>
        private void UpdProject()
        {
            if (Project == null)
            {
                MessageBox.Show("请选择一个项目", "提示");
                return;
            }

            Project.UptateTime = DateTime.Now;
            projectDal.Modify(Project);
            RefreshProjectList();
            
        }
        /// <summary>
        /// 删除项目
        /// </summary>
        private void DelProject()
        {
            if (Project == null)
            {
                MessageBox.Show("请选择一个项目", "提示");
                return;
            }
            try
            {
                projectDal.Del(Project);

            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }
            
            RefreshProjectList();
        }

        private void RefreshProjectList()
        {
            Project = null;
            switch (NavigatePath)
            {
                case "RealEstatePage":
                    ProjectType = "1";
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type)));
                    break;
                case "RegistrationPage":
                    ProjectType = "2";
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type)));
                    break;
                default:
                    ProjectType = "-1";
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type)));
                    break;
            }
           
        }
    }
}
