using BusinessData;
using BusinessData.Dal;
using BusinessData.Models;
using Common;
using Common.Enums;
using Common.Events;
using Common.Utils;
using Common.ValidationRules;
using Common.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace RealEstateModule.ViewModels
{
    public class ObligeePageViewModel : BindableBase, INavigationAware
    {

        #region Properties
        IEventAggregator EA;
        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// 新增/修改按钮内容
        /// </summary>
        private string buttonContent = "确认新增";
        public string ButtonContent
        {
            get { return buttonContent; }
            set { SetProperty(ref buttonContent, value); }
        }

        private Obligee obligee;
        public Obligee Obligee
        {
            get { return obligee; }
            set { SetProperty(ref obligee, value); }
        }

        #region 字典
        /// <summary>
        /// 证件种类
        /// </summary>
        private Dictionary<string, string> zjzlList;
        public Dictionary<string, string> ZJZLList
        {
            get { return zjzlList; }
            set { SetProperty(ref zjzlList, value); }
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
        /// 性别
        /// </summary>
        private Dictionary<string, string> xbList;
        public Dictionary<string, string> XBList
        {
            get { return xbList; }
            set { SetProperty(ref xbList, value); }
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
        /// 权利类型
        /// </summary>
        private Dictionary<string, string> qllxList;
        public Dictionary<string, string> QLLXList
        {
            get { return qllxList; }
            set { SetProperty(ref qllxList, value); }
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

        #endregion

        #region 命令
        /// <summary>
        /// 新增或修改权利人
        /// </summary>
        public DelegateCommand AddOrEditObligeeCommand { get; set; }
        /// <summary>
        /// 在项目列表选择一个项目
        /// </summary>
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        /// <summary>
        /// 在权利人列表选择一个权利人
        /// </summary>
        public DelegateCommand<object> SelectBusinessCommand { get; set; }
        #endregion 

        ObligeeDal ObligeeDal = new ObligeeDal();

        #endregion

        #region ctor
        public ObligeePageViewModel(IEventAggregator ea)
        {
            EA = ea;
            // 初始化下拉框
            InitialComboBoxList();
            // 新增或修改权利人信息
            AddOrEditObligeeCommand = new DelegateCommand(() => {
                switch (ButtonContent)
                {
                    case "确认新增":
                        AddObligee();
                        break;
                    case "确认修改":
                        EditObligee();
                        break;
                    default:
                        break;
                }
            });

            // 选中权利人列表中的一项
            SelectBusinessCommand = new DelegateCommand<object>(SelectBusiness);
            GlobalCommands.SelectBusinessCommand.RegisterCommand(SelectBusinessCommand);
        }
        #endregion

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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 获取选中项目
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                Project = project;
            }

            // 初始权利人数据
            InitialObligee();
            // 按钮为新增状态
            ButtonContent = "确认新增";
        }

        private void InitialObligee()
        {
            Obligee = new Obligee();
        }

        private void SelectBusiness(object obj)
        {
            try
            {
                // 加载权利人数据
                ListView listView = obj as ListView;
                Business business = new Business();
                business = listView.SelectedItem as Business;
                Obligee = business?.Obligee;

                // 按钮为修改状态
                ButtonContent = "确认修改";
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

        }

        private void EditObligee()
        {
            if (Obligee == null)
            {
                MessageBox.Show("请选择权利人", "提示");
                return;
            }
            if (!canExecute())
            {
                MessageBox.Show("验证失败", "提示");
                return;
            }
            try
            {
                Obligee.UpdateTime = DateTime.Now;
                ObligeeDal.Modify(Obligee);
                // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
                EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.ObligeePage);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

        }

        private void AddObligee()
        {
            if (Project == null)
            {
                MessageBox.Show("请选择项目", "提示");
                return;
            }
            if (!canExecute())
            {
                MessageBox.Show("验证失败", "提示");
                return;
            }
            try
            {
                Obligee.ProjectID = Project.ID;
                Obligee.ID = Guid.NewGuid();
                Obligee.UpdateTime = DateTime.Now;
                ObligeeDal.Add(Obligee);

                Obligee = null;
                // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
                EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.ObligeePage);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

        }

        private void InitialComboBoxList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = DictionaryUtil.GetDictionaryByName("证件类型");
            ZJZLList = dic;

            dic = DictionaryUtil.GetDictionaryByName("国家和地区");
            GJList = dic;

            dic = DictionaryUtil.GetDictionaryByName("性别");
            XBList = dic;

            dic = DictionaryUtil.GetDictionaryByName("权利人类型");
            QLRLXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("权利类型");
            QLLXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("共有方式");
            GYFSList = dic;

        }

        /// <summary>
        /// 能否执行新增或修改操作
        /// </summary>
        /// <returns></returns>
        private bool canExecute()
        {
            if (Obligee == null) return false;

            bool isValid = true;
            CultureInfo cultureInfo = new CultureInfo("");
            // 非空验证
            NotEmptyValidationRule notEmptyValidationRule = new NotEmptyValidationRule();
            isValid &= notEmptyValidationRule.Validate(Obligee.HBSM, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.QLRMC, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.ZJZL, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.ZJH, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.GJ, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.QLRLX, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.QLLX, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.GYFS, cultureInfo).IsValid;
            // 数字验证
            NumbericValidationRule numbericValidationRule = new NumbericValidationRule();
            isValid &= numbericValidationRule.Validate(Obligee.QLMJ, cultureInfo).IsValid;
            
            return isValid;
        }
    }
}
