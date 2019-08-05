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
    public class MortgagePageViewModel : BindableBase, INavigationAware
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

        private Mortgage mortgage;
        public Mortgage Mortgage
        {
            get { return mortgage; }
            set { SetProperty(ref mortgage, value); }
        }

        #region 字典
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
        /// 抵押方式
        /// </summary>
        private Dictionary<string, string> dyfsList;
        public Dictionary<string, string> DYFSList
        {
            get { return dyfsList; }
            set { SetProperty(ref dyfsList, value); }
        }
        /// <summary>
        /// 抵押不动产类型
        /// </summary>
        private Dictionary<string, string> dybdclxList;
        public Dictionary<string, string> DYBDCLXList
        {
            get { return dybdclxList; }
            set { SetProperty(ref dybdclxList, value); }
        }
        /// <summary>
        /// 持证方式
        /// </summary>
        private Dictionary<string, string> czfsList;
        public Dictionary<string, string> CZFSList
        {
            get { return czfsList; }
            set { SetProperty(ref czfsList, value); }
        }
        /// <summary>
        /// 金额单位
        /// </summary>
        private Dictionary<string, string>jedwList;
        public Dictionary<string, string> JEDWList
        {
            get { return jedwList; }
            set { SetProperty(ref jedwList, value); }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 新增或修改抵押信息
        /// </summary>
        public DelegateCommand AddOrEditMortgageCommand { get; set; }
        /// <summary>
        /// 在项目列表选择一个项目
        /// </summary>
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        /// <summary>
        /// 在抵押信息列表选择一个抵押信息
        /// </summary>
        public DelegateCommand<object> SelectBusinessCommand { get; set; }
        #endregion 

        MortgageDal MortgageDal = new MortgageDal();

        #endregion

        public MortgagePageViewModel(IEventAggregator ea)
        {
            EA = ea;
            // 初始化下拉框
            InitialComboBoxList();
            // 新增或修改抵押信息
            AddOrEditMortgageCommand = new DelegateCommand(() => {
                switch (ButtonContent)
                {
                    case "确认新增":
                        AddMortgage();
                        break;
                    case "确认修改":
                        EditMortgage();
                        break;
                    default:
                        break;
                }
            });

            // 选中抵押信息列表中的一项
            SelectBusinessCommand = new DelegateCommand<object>(SelectBusiness);
            GlobalCommands.SelectBusinessCommand.RegisterCommand(SelectBusinessCommand);
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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 获取选中项目
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                Project = project;
            }

            // 初始抵押信息数据
            InitialMortgage();
            // 按钮为新增状态
            ButtonContent = "确认新增";
        }

        private void InitialMortgage()
        {
            Mortgage = new Mortgage();
        }

        private void SelectBusiness(object obj)
        {
            try
            {
                // 加载抵押数据
                ListView listView = obj as ListView;
                Business business = new Business();
                business = listView.SelectedItem as Business;
                Mortgage = business?.Mortgage;

                // 按钮为修改状态
                ButtonContent = "确认修改";
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }
        }

        private void AddMortgage()
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
                Mortgage.ProjectID = Project.ID;
                Mortgage.ID = Guid.NewGuid();
                Mortgage.UpdateTime = DateTime.Now;
                SetPerson();

                MortgageDal.Add(Mortgage);

                Mortgage = null;
                // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
                EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.MortgagePage);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }
        }


        private void EditMortgage()
        {
            if (Mortgage == null)
            {
                MessageBox.Show("请选择抵押信息", "提示");
                return;
            }
            if (!canExecute())
            {
                MessageBox.Show("验证失败", "提示");
                return;
            }
            try
            {
                Mortgage.UpdateTime = DateTime.Now;
                SetPerson();
                MortgageDal.Modify(Mortgage);
                // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
                EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.MortgagePage);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }
        }

        private void SetPerson()
        {
            if (string.IsNullOrWhiteSpace(Mortgage.FRXM)) // 若法人姓名为空
            {
                Mortgage.FRZJLX = null;
                Mortgage.FRZJH = null;
                Mortgage.FRDH = null;
            }
        }

        private bool canExecute()
        {
            if (Mortgage == null) return false;

            bool isValid = true;
            CultureInfo cultureInfo = new CultureInfo("");
            // 非空验证
            NotEmptyValidationRule notEmptyValidationRule = new NotEmptyValidationRule();
            isValid &= notEmptyValidationRule.Validate(Mortgage.HBSM, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Mortgage.QLRMC, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Mortgage.ZJLX, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Mortgage.ZJH, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Mortgage.DYFS, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Mortgage.DYR, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Mortgage.DYRZJLX, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Mortgage.DYRZJH, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Mortgage.DYBDCLX, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Mortgage.CZFS, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Mortgage.ZWR, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Mortgage.DBR, cultureInfo).IsValid;
            if (!string.IsNullOrWhiteSpace(Mortgage.FRXM)) // 若法人姓名不为空
            {
                isValid &= notEmptyValidationRule.Validate(Mortgage.FRZJLX, cultureInfo).IsValid;
                isValid &= notEmptyValidationRule.Validate(Mortgage.FRZJH, cultureInfo).IsValid;
            }
            return isValid;
        }

        private void InitialComboBoxList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = DictionaryUtil.GetDictionaryByName("证件类型");
            ZJLXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("抵押方式");
            DYFSList = dic;

            dic = DictionaryUtil.GetDictionaryByName("抵押不动产类型");
            DYBDCLXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("持证方式");
            CZFSList = dic;

            dic = DictionaryUtil.GetDictionaryByName("金额单位");
            JEDWList = dic;
        }

    }
}
