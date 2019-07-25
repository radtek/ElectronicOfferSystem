using BusinessData;
using BusinessData.Dal;
using Common.Utils;
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

        #region 字典
        /// <summary>
        /// 性别
        /// </summary>
        private Dictionary<string, string> xbList;
        public Dictionary<string, string> XBList
        {
            get { return xbList; }
            set { SetProperty(ref xbList, value); }
        }
        /// <summary>
        /// 证件类型
        /// </summary>
        private Dictionary<string, string> zjlxList;
        public Dictionary<string, string> ZJLXList
        {
            get { return zjlxList; }
            set { SetProperty(ref zjlxList, value); }
        }
        /// <summary>
        /// 国家
        /// </summary>
        private Dictionary<string, string> gjList;
        public Dictionary<string, string> GJList
        {
            get { return gjList; }
            set { SetProperty(ref gjList, value); }
        }
        /// <summary>
        /// 省市
        /// </summary>
        private Dictionary<string, string> ssList;
        public Dictionary<string, string> SSList
        {
            get { return ssList; }
            set { SetProperty(ref ssList, value); }
        }
        /// <summary>
        /// 所属行业
        /// </summary>
        private Dictionary<string, string> sshyList;
        public Dictionary<string, string> SSHYList
        {
            get { return sshyList; }
            set { SetProperty(ref sshyList, value); }
        }
        /// <summary>
        /// 权利人类型
        /// </summary>
        private Dictionary<string, string> qlrlxList;
        public Dictionary<string, string> QLRLXList
        {
            get { return qlrlxList; }
            set { SetProperty(ref qlrlxList, value); }
        }
        /// <summary>
        /// 共有方式
        /// </summary>
        private Dictionary<string, string> gyfsList;
        public Dictionary<string, string> GYFSList
        {
            get { return gyfsList; }
            set { SetProperty(ref gyfsList, value); }
        }
        /// <summary>
        /// 申请人类别
        /// </summary>
        private Dictionary<string, string> sqrlbList;
        public Dictionary<string, string> SQRLBList
        {
            get { return sqrlbList; }
            set { SetProperty(ref sqrlbList, value); }
        }
        /// <summary>
        /// 不动产单元类型
        /// </summary>
        private Dictionary<string, string> bdcdylxList;
        public Dictionary<string, string> BDCDYLXList
        {
            get { return bdcdylxList; }
            set { SetProperty(ref bdcdylxList, value); }
        }
        /// <summary>
        /// 是否
        /// </summary>
        private Dictionary<string, string> sfList;
        public Dictionary<string, string> SFList
        {
            get { return sfList; }
            set { SetProperty(ref sfList, value); }
        }
        #endregion


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

            // 初始化下拉框
            InitialComboBoxList();

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

        private void InitialComboBoxList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = DictionaryUtil.GetDictionaryByName("性别");
            XBList = dic;

            dic = DictionaryUtil.GetDictionaryByName("证件类型");
            ZJLXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("国家和地区");
            GJList = dic;

            dic = DictionaryUtil.GetDictionaryByName("省市");
            SSList = dic;

            dic = DictionaryUtil.GetDictionaryByName("所属行业");
            SSHYList = dic;

            dic = DictionaryUtil.GetDictionaryByName("权利人类型");
            QLRLXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("共有方式");
            GYFSList = dic;

            dic = DictionaryUtil.GetDictionaryByName("申请人类别");
            SQRLBList = dic;

            dic = DictionaryUtil.GetDictionaryByName("不动产单元类型");
            BDCDYLXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("是否字典");
            SFList = dic;

        }
    }
}
