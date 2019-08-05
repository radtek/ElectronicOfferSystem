using BusinessData;
using BusinessData.Dal;
using Common;
using Common.Enums;
using Common.Utils;
using Common.ValidationRules;
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
using System.Globalization;
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

        public EMainPage NavigatePath { get; set; }

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
        public DelegateCommand<EMainPage?> NavigateCommand { get; set; }
        public DelegateCommand<string> OpenAddOrEditProjectDialogCommand { get; private set; }
        public DelegateCommand CancelAddOrEditProjectDialogCommand { get; set; }
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        public DelegateCommand DelProjectCommand { get; set; }
        public DelegateCommand SearchProjectCommand { get; set; }
        ProjectDal projectDal = new ProjectDal();

        public ProjectPageViewModel()
        {
            // 初始化
            Projects = new ObservableCollection<Project>(projectDal.GetListBy(p => !"-1".Equals(p.Type)));
            ProjectType = ((int)EProjectType.Default).ToString();
            IsBtnEnabled = false;

            AddOrEditProjectDialog = AddOrEditProjectDialogViewModel.getInstance();

            // 页面导航
            NavigateCommand = new DelegateCommand<EMainPage?>(Navigate);
            GlobalCommands.NavigateCommand.RegisterCommand(NavigateCommand);

            // 选中ListView中的一项
            SelectProjectCommand = new DelegateCommand<object>((obj) =>
            {
                ListView listView = obj as ListView;
                Project = listView.SelectedItem as Project;
                //if (Project == null) return;
            });
            GlobalCommands.SelectProjectCommand.RegisterCommand(SelectProjectCommand);

            // 关闭模态框
            CancelAddOrEditProjectDialogCommand = new DelegateCommand(() =>
            {
                Project = null;
            });

            // 打开模态框
            OpenAddOrEditProjectDialogCommand = new DelegateCommand<string>((string dialogTitle) =>
            {
                DialogTitle = dialogTitle;
                if ("新增项目".Equals(DialogTitle))
                {
                    Project = new Project();
                    Project.Type = ProjectType; // 设置项目类型
                }
                AddOrEditProjectDialog.DialogTitle = DialogTitle;
                AddOrEditProjectDialog.Project = Project;
                DialogHost.Show(new AddOrEditProjectDialog(), "RootDialog", ConfirAddOrEditProjectEventHandler);

            });
            // 删除项目
            DelProjectCommand = new DelegateCommand(DelProject);

            SearchProjectCommand = new DelegateCommand(() =>
            {
                if (int.Parse(ProjectType) == (int)EProjectType.Default)
                {
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy(p => p.ProjectName.Contains(SearchProjectName)));
                }
                else
                {
                Projects = new ObservableCollection<Project>(projectDal.GetListBy(p => ProjectType.Equals(p.Type) && p.ProjectName.Contains(SearchProjectName)));
                }
            });


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
                bool IsSuccess = false;
                switch (DialogTitle)
                {
                    case "新增项目":
                        IsSuccess = AddProject();
                        break;
                    case "编辑项目":
                        IsSuccess = UpdProject();
                        break;
                    default:
                        break;
                }
                if(IsSuccess)
                {
                    // 显示加载1s
                    eventArgs.Session.UpdateContent(new SampleProgressDialog());
                    Task.Delay(TimeSpan.FromSeconds(0.3))
                        .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                            TaskScheduler.FromCurrentSynchronizationContext());
                }

            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().updateShow(ex, eventArgs.Session);
                return;
            }

        }

        private void Navigate(EMainPage? navigatePath)
        {
            NavigatePath = (EMainPage)navigatePath;
            IsBtnEnabled = true;
            switch (NavigatePath)
            {
                case EMainPage.IndexPage:
                    ProjectType = ((int)EProjectType.Default).ToString();
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy(p => !ProjectType.Equals(p.Type)));
                    IsBtnEnabled = false;
                    break;
                case EMainPage.RealEstatePage:
                    ProjectType = ((int)EProjectType.RealEstate).ToString();
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type)));
                    break;
                case EMainPage.RegistrationPage:
                    ProjectType = ((int)EProjectType.Registration).ToString();
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type)));
                    break;
                default:
                    break;
            }

            SelectedProject = Projects.FirstOrDefault();

        }

        private bool AddProject()
        {
            if (string.IsNullOrWhiteSpace(ProjectType))
            {
                MessageBox.Show("请选择项目类型", "提示");
                return false;
            }
            if (!canExecute())
            {
                MessageBox.Show("验证失败", "提示");
                return false;
            }
            // 新增项目的初始化
            Project.ID = Guid.NewGuid();
            Project.UptateTime = DateTime.Now;
            Project.State = "0";
            switch (int.Parse(ProjectType))
            {
                case (int)EProjectType.RealEstate:
                    //Project.Type = EProjectType.RealEstate.ToString();
                    break;
                case (int)EProjectType.Registration:
                   // Project.Type = EProjectType.Registration.ToString();
                    // 同时新增转移信息
                    TransferDal transferDal = new TransferDal();
                    Transfer transfer = new Transfer();
                    transfer.ID = Guid.NewGuid();
                    transfer.ProjectID = Project.ID;
                    transfer.UpdateTime = DateTime.Now;
                    transferDal.Add(transfer);
                    break;
                default:
                    //Project.Type = EProjectType.Default.ToString();
                    break;
            }

            projectDal.Add(Project);

            RefreshProjectList();

            return true;
        }
        /// <summary>
        /// 修改项目
        /// </summary>
        private bool UpdProject()
        {
            if (Project == null)
            {
                MessageBox.Show("请选择一个项目", "提示");
                return false;
            }
            if (!canExecute())
            {
                MessageBox.Show("验证失败", "提示");
                return false;
            }
            Project.UptateTime = DateTime.Now;
            projectDal.Modify(Project);
            //RefreshProjectList();
            return true;
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
                if ("1".Equals(Project.Type)) // 楼盘表项目
                {
                    NaturalBuildingDal naturalBuildingDal = new NaturalBuildingDal();
                    LogicalBuildingDal logicalBuildingDal = new LogicalBuildingDal();
                    FloorDal floorDal = new FloorDal();
                    HouseholdDal householdDal = new HouseholdDal();
                    ObligeeDal obligeeDal = new ObligeeDal();
                    MortgageDal mortgageDal = new MortgageDal();
                    SequestrationDal sequestrationDal = new SequestrationDal();

                    naturalBuildingDal.DelBy(t => t.ProjectID == Project.ID);
                    logicalBuildingDal.DelBy(t => t.ProjectID == Project.ID);
                    floorDal.DelBy(t => t.ProjectID == Project.ID);
                    householdDal.DelBy(t => t.ProjectID == Project.ID);
                    obligeeDal.DelBy(t => t.ProjectID == Project.ID);
                    mortgageDal.DelBy(t => t.ProjectID == Project.ID);
                    sequestrationDal.DelBy(t => t.ProjectID == Project.ID);
                }
                else if ("2".Equals(Project.Type)) // 登记业务项目
                {
                    TransferDal transferDal = new TransferDal();
                    ApplicantDal applicantDal = new ApplicantDal();

                    transferDal.DelBy(t => t.ProjectID == Project.ID);
                    applicantDal.DelBy(t => t.ProjectID == Project.ID);
                    FileHelper.DelDir(Project);
                }
                projectDal.Del(Project);

            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

            RefreshProjectList();
        }

        /// <summary>
        /// 能否执行新增或修改操作
        /// </summary>
        /// <returns></returns>
        private bool canExecute()
        {
            if (Project == null) return false;

            bool isValid = true;
            CultureInfo cultureInfo = new CultureInfo("");
            // 非空验证
            NotEmptyValidationRule notEmptyValidationRule = new NotEmptyValidationRule();
            isValid &= notEmptyValidationRule.Validate(Project.DeveloperName, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Project.ProjectName, cultureInfo).IsValid;

            return isValid;
        }

        /// <summary>
        /// 刷新项目列表
        /// </summary>
        private void RefreshProjectList()
        {
            Project = null;
            //switch (NavigatePath)
            //{
            //    case "RealEstatePage":
            //        ProjectType = EProjectType.RealEstate.ToString();
            //        Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type)));
            //        break;
            //    case "RegistrationPage":
            //        ProjectType = EProjectType.Registration.ToString();
            //        Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type)));
            //        break;
            //    default:
            //        ProjectType = EProjectType.Default.ToString();
            //        Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type)));
            //        break;
            //}
            switch (int.Parse(ProjectType))
            {
                case (int)EProjectType.RealEstate:
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type)));
                    break;
                case (int)EProjectType.Registration:
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type)));
                    break;
                case (int)EProjectType.Default:
                    Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => ProjectType.Equals(p.Type)));
                    break;
                default:
                    break;
            }

        }
    }
}
