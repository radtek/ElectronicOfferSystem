using BusinessData;
using BusinessData.Dal;
using Common;
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

        private bool isAddOrEditProjectDialogOpen;
        public bool IsAddOrEditProjectDialogOpen
        {
            get { return isAddOrEditProjectDialogOpen; }
            set { SetProperty(ref isAddOrEditProjectDialogOpen, value); }
        }

        private string dialogTitle;
        public string DialogTitle
        {
            get { return dialogTitle; }
            set { SetProperty(ref dialogTitle, value); }
        }

        public string NavigatePath { get; set; }

        private ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects
        {
            get { return projects; }
            set { SetProperty(ref projects, value); }
        }

        /// <summary>
        /// 页面导航
        /// </summary>
        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand<string> OpenAddOrEditProjectDialogCommand { get; private set; }
        public DelegateCommand AcceptCommand { get; set; }
        public DelegateCommand CancelAddOrEditProjectDialogCommand { get; set; }
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        public DelegateCommand DelProjectCommand { get; set; }
        ProjectDal projectDal = new ProjectDal();

        public ProjectPageViewModel()
        {
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
            });
            GlobalCommands.SelectProjectCommand.RegisterCommand(SelectProjectCommand);

            // 关闭模态框
            CancelAddOrEditProjectDialogCommand = new DelegateCommand(() => {
                IsAddOrEditProjectDialogOpen = false;
                Project = null;
            });

            // 模态框的确认按钮
            AcceptCommand = new DelegateCommand(()=> {
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
            });

            // 打开模态框
            OpenAddOrEditProjectDialogCommand = new DelegateCommand<string>((string dialogTitle) => {
                DialogTitle = dialogTitle;
                if ("新增项目".Equals(DialogTitle))
                {
                    Project = new Project();
                }
                IsAddOrEditProjectDialogOpen = true;
            });
            // 删除项目
            DelProjectCommand = new DelegateCommand(()=> {
                if (Project == null) return;
                projectDal.Del(Project);
                RefreshProjectList();
            });


        }

        private void Navigate(string navigatePath)
        {
            NavigatePath = navigatePath;

            switch (navigatePath)
            {
                case "IndexPage":
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy(p=>!"-1".Equals(p.Type)));
                    break;
                case "RealEstatePage":
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => "1".Equals(p.Type) ));
                    break;
                case "RegistrationPage":
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => "2".Equals(p.Type) ));
                    break;
                case "SettingPage":
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy(p => !"-1".Equals(p.Type) ));
                    break;
                default:
                    break;
            }

        }

        private void AddProject()
        {
            if (string.IsNullOrWhiteSpace(NavigatePath))
                return;
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
            CloseDialogAndRefreshProjectList();
        }

        private void UpdProject()
        {  
            if (Project != null)
            {
                Project.UptateTime = DateTime.Now;
                projectDal.Modify(Project);
                CloseDialogAndRefreshProjectList();
            }
        }

        private void CloseDialogAndRefreshProjectList()
        {
            IsAddOrEditProjectDialogOpen = false;
            RefreshProjectList();
        }

        private void RefreshProjectList()
        {
            Project = null;
            switch (NavigatePath)
            {
                case "RealEstatePage":
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => "1".Equals(p.Type)));
                    break;
                case "RegistrationPage":
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => "2".Equals(p.Type)));
                    break;
                default:
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => "-1".Equals(p.Type)));
                    break;
            }
           
        }
    }
}
