﻿using BusinessData;
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
    class HouseholdPageViewModel : BindableBase, INavigationAware
    {
        #region Properties
        IEventAggregator EA;
        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }

        private EMappingType mappingType;
        public EMappingType MappingType
        {
            get { return mappingType; }
            set { SetProperty(ref mappingType, value); }
        }

        /// <summary>
        /// 新增/修改按钮内容
        /// </summary>
        private string buttonContent = "确认新增";
        public string ButtonContent
        {
            get { return buttonContent; }
            set { SetProperty(ref buttonContent, value); }
        }

        private Household household;
        public Household Household
        {
            get { return household; }
            set { SetProperty(ref household, value); }
        }

        #region 字典
        /// <summary>
        /// 房屋结构
        /// </summary>
        private Dictionary<string, string> fwjglList;
        public Dictionary<string, string> FWJGList
        {
            get { return fwjglList; }
            set { SetProperty(ref fwjglList, value); }
        }
        /// <summary>
        /// 面积单位
        /// </summary>
        private Dictionary<string, string> mjdwList;
        public Dictionary<string, string> MJDWList
        {
            get { return mjdwList; }
            set { SetProperty(ref mjdwList, value); }
        }
        /// <summary>
        /// 户型结构
        /// </summary>
        private Dictionary<string, string> hxjgList;
        public Dictionary<string, string> HXJGList
        {
            get { return hxjgList; }
            set { SetProperty(ref hxjgList, value); }
        }
        /// <summary>
        /// 户型
        /// </summary>
        private Dictionary<string, string> hxList;
        public Dictionary<string, string> HXList
        {
            get { return hxList; }
            set { SetProperty(ref hxList, value); }
        }
        /// <summary>
        /// 房屋用途
        /// </summary>
        private Dictionary<string, string> fwytList;
        public  Dictionary<string, string> FWYTList
        {
            get { return fwytList; }
            set { SetProperty(ref fwytList, value); }
        }
        /// <summary>
        /// 房屋性质
        /// </summary>
        private Dictionary<string, string> fwxzList;
        public Dictionary<string, string> FWXZList
        {
            get { return fwxzList; }
            set { SetProperty(ref fwxzList, value); }
        }
        /// <summary>
        /// 房屋类型
        /// </summary>
        private Dictionary<string, string> fwlxList;
        public  Dictionary<string, string> FWLXList
        {
            get { return fwlxList; }
            set { SetProperty(ref fwlxList, value); }
        }
        /// <summary>
        /// 房屋产别
        /// </summary>
        private Dictionary<string, string> fwcbList;
        public Dictionary<string, string> FWCBList
        {
            get { return fwcbList; }
            set { SetProperty(ref fwcbList, value); }
        }
        /// <summary>
        /// 状态
        /// </summary>
        private Dictionary<string, string> ztList;
        public Dictionary<string, string> ZTList
        {
            get { return ztList; }
            set { SetProperty(ref ztList, value); }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 新增或修改户
        /// </summary>
        public DelegateCommand AddOrEditHouseholdCommand { get; set; }
        /// <summary>
        /// 在项目列表选择一个项目
        /// </summary>
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        /// <summary>
        /// 在户列表选择一个户
        /// </summary>
        public DelegateCommand<object> SelectBusinessCommand { get; set; }
        #endregion

        HouseholdDal HouseholdDal = new HouseholdDal();
        #endregion

        #region ctor
        public HouseholdPageViewModel(IEventAggregator ea)
        {
            EA = ea;
            // 初始化下拉框
            InitialComboBoxList();
            // 新增或修改户信息
            AddOrEditHouseholdCommand = new DelegateCommand(() => {
                switch (ButtonContent)
                {
                    case "确认新增":
                        AddHousehold();
                        break;
                    case "确认修改":
                        EditHousehold();
                        break;
                    default:
                        break;
                }
            });

            // 选中户列表中的一项
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
                MappingType = (EMappingType)int.Parse(Project.MappingType);
            }

            // 初始户数据
            InitialHousehold();
            // 按钮为新增状态
            ButtonContent = "确认新增";
        }

        private void InitialHousehold()
        {
            Household = new Household();
        }
        /// <summary>
        /// 选择户列表中的一项
        /// </summary>
        /// <param name="obj"></param>
        private void SelectBusiness(object obj)
        {
            try
            {
                // 加载户数据
                ListView listView = obj as ListView;
                Business business = new Business();
                business = listView.SelectedItem as Business;
                Household = business?.Household;

                // 按钮为修改状态
                ButtonContent = "确认修改";
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

        }

        private void EditHousehold()
        {
            if (Household == null)
            {
                MessageBox.Show("请选择户", "提示");
                return;
            }
            if (!canExecute())
            {
                MessageBox.Show("验证失败", "提示");
                return;
            }
            try
            {
                Household.UpdateTime = DateTime.Now;
                HouseholdDal.Modify(Household);

                // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
                EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.HouseholdPage);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

        }

        private void AddHousehold()
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
                Household.ProjectID = Project.ID;
                Household.ID = Guid.NewGuid();
                Household.UpdateTime = DateTime.Now;
                HouseholdDal.Add(Household);

                Household = null;
                // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
                EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.HouseholdPage);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

        }

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitialComboBoxList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = DictionaryUtil.GetDictionaryByName("面积单位");
            MJDWList = dic;

            dic = DictionaryUtil.GetDictionaryByName("户型结构");
            HXJGList = dic;

            dic = DictionaryUtil.GetDictionaryByName("户型");
            HXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("房屋用途");
            FWYTList = dic;

            dic = DictionaryUtil.GetDictionaryByName("房屋结构");
            FWJGList = dic;

            dic = DictionaryUtil.GetDictionaryByName("房屋性质");
            FWXZList = dic;

            dic = DictionaryUtil.GetDictionaryByName("房屋类型");
            FWLXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("房屋产别");
            FWCBList = dic;

            dic = DictionaryUtil.GetDictionaryByName("不动产单元状态");
            ZTList = dic;

        }

        /// <summary>
        /// 能否执行新增或修改操作
        /// </summary>
        /// <returns></returns>
        private bool canExecute()
        {
            if (Household == null) return false;

            bool isValid = true;
            CultureInfo cultureInfo = new CultureInfo("");
            // 非空验证
            NotEmptyValidationRule notEmptyValidationRule = new NotEmptyValidationRule();
            isValid &= notEmptyValidationRule.Validate(Household.HBSM, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Household.FWBM, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Household.YSDM, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Household.ZL, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Household.MJDW, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Household.SZC, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Household.QSC, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Household.ZZC, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Household.SHBW, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Household.ZT, cultureInfo).IsValid;
            // 数字和非空验证
            NumbericAndNotEmptyValidationRule numbericAndNotEmptyValidationRule = new NumbericAndNotEmptyValidationRule();
            NumbericValidationRule numbericValidationRule = new NumbericValidationRule();
            if (MappingType == EMappingType.PredictiveMapping)
            {
                isValid &= numbericAndNotEmptyValidationRule.Validate(Household.YCTNJZMJ, cultureInfo).IsValid;
                isValid &= numbericAndNotEmptyValidationRule.Validate(Household.YCFTJZMJ, cultureInfo).IsValid;
                isValid &= numbericAndNotEmptyValidationRule.Validate(Household.YCJZMJ, cultureInfo).IsValid;
                isValid &= numbericAndNotEmptyValidationRule.Validate(Household.YCQTJZMJ, cultureInfo).IsValid;
                isValid &= numbericAndNotEmptyValidationRule.Validate(Household.YCDXBFJZMJ, cultureInfo).IsValid;

                isValid &= numbericValidationRule.Validate(Household.YCFTXS, cultureInfo).IsValid;
            }
            else if (MappingType == EMappingType.SurveyingMapping)
            {
                isValid &= numbericAndNotEmptyValidationRule.Validate(Household.SCTNJZMJ, cultureInfo).IsValid;
                isValid &= numbericAndNotEmptyValidationRule.Validate(Household.SCFTJZMJ, cultureInfo).IsValid;
                isValid &= numbericAndNotEmptyValidationRule.Validate(Household.SCJZMJ, cultureInfo).IsValid;
                isValid &= numbericAndNotEmptyValidationRule.Validate(Household.SCQTJZMJ, cultureInfo).IsValid;
                isValid &= numbericAndNotEmptyValidationRule.Validate(Household.SCDXBFJZMJ, cultureInfo).IsValid;

                isValid &= numbericValidationRule.Validate(Household.SCFTXS, cultureInfo).IsValid;
            }
            // 数字验证
            isValid &= numbericValidationRule.Validate(Household.FTTDMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(Household.DYTDMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(Household.GYTDMJ, cultureInfo).IsValid;
            // 不动产单元号验证
            BDCDYHValidationRule bDCDYHValidationRule = new BDCDYHValidationRule();
            isValid &= bDCDYHValidationRule.Validate(Household.BDCDYH, cultureInfo).IsValid;

            return isValid;
        }
    }
}
