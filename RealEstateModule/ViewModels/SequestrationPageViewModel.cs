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
    public class SequestrationPageViewModel : BindableBase, INavigationAware
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

        private Sequestration sequestration;
        public Sequestration Sequestration
        {
            get { return sequestration; }
            set { SetProperty(ref sequestration, value); }
        }

        #region 字典
        /// <summary>
        /// 查封类型
        /// </summary>
        private Dictionary<string, string> cflxList;
        public Dictionary<string, string> CFLXList
        {
            get { return cflxList; }
            set { SetProperty(ref cflxList, value); }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 新增或修改查封信息
        /// </summary>
        public DelegateCommand AddOrEditSequestrationCommand { get; set; }
        /// <summary>
        /// 在项目列表选择一个项目
        /// </summary>
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        /// <summary>
        /// 在查封信息列表选择一个查封信息
        /// </summary>
        public DelegateCommand<object> SelectBusinessCommand { get; set; }
        #endregion 

        SequestrationDal SequestrationDal = new SequestrationDal();

        #endregion


        public SequestrationPageViewModel(IEventAggregator ea)
        {
            EA = ea;
            // 初始化下拉框
            InitialComboBoxList();
            // 新增或修改查封信息
            AddOrEditSequestrationCommand = new DelegateCommand(() => {
                switch (ButtonContent)
                {
                    case "确认新增":
                        AddSequestration();
                        break;
                    case "确认修改":
                        EditSequestration();
                        break;
                    default:
                        break;
                }
            });

            // 选中查封信息列表中的一项
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

            // 初始查封信息数据
            InitialSequestration();
            // 按钮为新增状态
            ButtonContent = "确认新增";
        }

        private void InitialSequestration()
        {
            Sequestration = new Sequestration();
        }

        private void SelectBusiness(object obj)
        {
            try
            {
                // 加载查封数据
                ListView listView = obj as ListView;
                Business business = new Business();
                business = listView.SelectedItem as Business;
                Sequestration = business?.Sequestration;

                // 按钮为修改状态
                ButtonContent = "确认修改";
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }
        }

        private void AddSequestration()
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
                Sequestration.ProjectID = Project.ID;
                Sequestration.ID = Guid.NewGuid();
                Sequestration.UpdateTime = DateTime.Now;
                SequestrationDal.Add(Sequestration);

                Sequestration = null;
                // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
                EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.SequestrationPage);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }
        }

        private void EditSequestration()
        {
            if (Sequestration == null)
            {
                MessageBox.Show("请选择查封信息", "提示");
                return;
            }
            if (!canExecute())
            {
                MessageBox.Show("验证失败", "提示");
                return;
            }
            try
            {
                Sequestration.UpdateTime = DateTime.Now;
                SequestrationDal.Modify(Sequestration);
                // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
                EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.SequestrationPage);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }
        }


        private bool canExecute()
        {
            if (Sequestration == null) return false;

            bool isValid = true;
            CultureInfo cultureInfo = new CultureInfo("");
            // 非空验证
            NotEmptyValidationRule notEmptyValidationRule = new NotEmptyValidationRule();
            isValid &= notEmptyValidationRule.Validate(Sequestration.HBSM, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Sequestration.CFLX, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Sequestration.DBR, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Sequestration.CFSJ, cultureInfo).IsValid;

            return isValid;
        }

        private void InitialComboBoxList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = DictionaryUtil.GetDictionaryByName("查封类型");
            CFLXList = dic;
        }


    }
}
