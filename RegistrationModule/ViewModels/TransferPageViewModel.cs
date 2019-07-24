using BusinessData;
using BusinessData.Dal;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;

namespace RegistrationModule.ViewModels
{
    class TransferPageViewModel : BindableBase, INavigationAware
    {
        IEventAggregator EA;

        /// <summary>
        /// 新增/修改按钮内容
        /// </summary>
        private string applicantButtonContent = "确认新增";
        public string ApplicantButtonContent
        {
            get { return applicantButtonContent; }
            set { SetProperty(ref applicantButtonContent, value); }
        }

        /// <summary>
        /// 新增/修改按钮内容
        /// </summary>
        private string transferButtonContent = "确认新增";
        public string TransferButtonContent
        {
            get { return transferButtonContent; }
            set { SetProperty(ref transferButtonContent, value); }
        }

        private ObservableCollection<Applicant> applicants;
        public ObservableCollection<Applicant> Applicants
        {
            get { return applicants; }
            set { SetProperty(ref applicants, value); }
        }

        private Applicant applicant;
        public Applicant Applicant
        {
            get { return applicant; }
            set { SetProperty(ref applicant, value); }
        }

        private Transfer transfer;
        public Transfer Transfer
        {
            get { return transfer; }
            set { SetProperty(ref transfer, value); }
        }


        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }
        public ProjectDal ProjectDal { get; set; }
        public ApplicantDal ApplicantDal { get; set; }
        public TransferDal TransferDal { get; set; }
        public NavigationContext NavigationContext { get; set; }

        public DelegateCommand AddOrEditApplicantCommand { get; set; }
        public DelegateCommand EditTransferCommand { get; set; }
        public DelegateCommand<object> SelectApplicantCommand { get; set; }
        public DelegateCommand AddApplicantCommand { get; set; }
        public DelegateCommand DelApplicantCommand { get; set; }

        public TransferPageViewModel(IEventAggregator ea)
        {
            EA = ea;
            ProjectDal = new ProjectDal();
            ApplicantDal = new ApplicantDal();
            TransferDal = new TransferDal();


            AddOrEditApplicantCommand = new DelegateCommand(()=> {
                switch (ApplicantButtonContent)
                {
                    case "确认新增":
                        AddApplicant();
                        break;
                    case "确认修改":
                        EditApplicant();
                        break;
                    default:
                        break;
                }
            });
            EditTransferCommand = new DelegateCommand(EditTransfer);

            SelectApplicantCommand = new DelegateCommand<object>(SelectApplicant);

            AddApplicantCommand = new DelegateCommand(() => {
                // 重新加载页面
                OnNavigatedTo(NavigationContext);
            });

            DelApplicantCommand = new DelegateCommand(DelApplicant);
        }



        /// <summary>
        /// 选择申请人列表中的一项
        /// </summary>
        /// <param name="obj"></param>
        private void SelectApplicant(object obj)
        {
            // 加载申请人数据
            ListView listView = obj as ListView;
            Applicant = listView.SelectedItem as Applicant;

            // 按钮为修改状态
            ApplicantButtonContent = "确认修改";
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigationContext = navigationContext;
            // 获取选中项目
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                Project = project;
            }
            // 初始化登记项目
            Project = ProjectDal.InitialRegistrationProject(Project);
            // 获取申请人集合
            Applicants = new ObservableCollection<Applicant>(Project.Applicants);

            // 初始申请人与转移信息
            Applicant = new Applicant();
            Transfer = Project.Transfer;
            // 按钮为新增状态
            ApplicantButtonContent = "确认新增";
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                return project != null;
            }
            else
            {
                return true;
            }
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        /// <summary>
        /// 修改转移信息
        /// </summary>
        private void EditTransfer()
        {
            if (Project == null) return;
            if (Transfer == null) return;
            Transfer.UpdateTime = DateTime.Now;
            ProjectDal.Modify(Project);
            TransferDal.Modify(Transfer);
        }


        /// <summary>
        /// 修改申请人
        /// </summary>
        private void EditApplicant()
        {
            if (Applicant == null) return;
            Applicant.UpdateTime = DateTime.Now;
            ApplicantDal.Modify(Applicant);

            // 重新加载页面
            OnNavigatedTo(NavigationContext);
        }

        /// <summary>
        /// 新增申请人
        /// </summary>
        private void AddApplicant()
        {
            if (Project == null) return;
            if (Applicant == null) return;
            Applicant.ProjectID = Project.ID;
            Applicant.ID = Guid.NewGuid();
            Applicant.UpdateTime = DateTime.Now;
            ApplicantDal.Add(Applicant);

            Applicant = null;
            // 重新加载页面
            OnNavigatedTo(NavigationContext);
        }

        private void DelApplicant()
        {
            if (Applicant == null) return;
            ApplicantDal.Del(Applicant);
            // 重新加载页面
            OnNavigatedTo(NavigationContext);
        }
    }
}
