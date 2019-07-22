﻿using BusinessData;
using BusinessData.Dal;
using BusinessData.Models;
using Common;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows.Controls;

namespace RealEstateModule.ViewModels
{
    class LogicalBuildingPageViewModel : BindableBase, INavigationAware
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

        /// <summary>
        /// 逻辑幢信息
        /// </summary>
        private LogicalBuilding logicalBuilding;
        public LogicalBuilding LogicalBuilding
        {
            get { return logicalBuilding; }
            set
            {
                SetProperty(ref logicalBuilding, value);
            }
        }

        #region 命令
        /// <summary>
        /// 新增或修改逻辑幢
        /// </summary>
        public DelegateCommand AddOrEditLogicalBuildingCommand { get; set; }
        /// <summary>
        /// 在项目列表选择一个项目
        /// </summary>
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        /// <summary>
        /// 在逻辑幢列表选择一个逻辑幢
        /// </summary>
        public DelegateCommand<object> SelectBusinessCommand { get; set; }
        #endregion 

        LogicalBuildingDal LogicalBuildingDal = new LogicalBuildingDal();

        #endregion

        #region ctor
        public LogicalBuildingPageViewModel(IEventAggregator ea)
        {
            EA = ea;
            // 新增或修改逻辑幢信息
            AddOrEditLogicalBuildingCommand = new DelegateCommand(() => {
                switch (ButtonContent)
                {
                    case "确认新增":
                        AddLogicalBuilding();
                        break;
                    case "确认修改":
                        EditLogicalBuilding();
                        break;
                    default:
                        break;
                }
            });

            // 选中逻辑幢列表中的一项
            SelectBusinessCommand = new DelegateCommand<object>(SelectBusiness);
            GlobalCommands.SelectBusinessCommand.RegisterCommand(SelectBusinessCommand);
        }
        #endregion

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 获取选中项目
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                Project = project;
            }

            // 初始逻辑幢数据
            InitialLogicalBuilding();
            // 按钮为新增状态
            ButtonContent = "确认新增";
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
        /// 选择逻辑幢列表中的一项
        /// </summary>
        /// <param name="obj"></param>
        private void SelectBusiness(object obj)
        {
            // 加载自然幢数据
            ListView listView = obj as ListView;
            Business business = new Business();
            business = listView.SelectedItem as Business;
            LogicalBuilding = business?.LogicalBuilding;

            // 按钮为修改状态
            ButtonContent = "确认修改";
        }
        /// <summary>
        /// 新增逻辑幢
        /// </summary>
        private void AddLogicalBuilding()
        {
            if (Project == null) return;
            if (LogicalBuilding == null) return;
            LogicalBuilding.ProjectID = Project.ID;
            LogicalBuilding.ID = Guid.NewGuid();
            LogicalBuilding.UpdateTime = DateTime.Now;
            LogicalBuildingDal.Add(LogicalBuilding);

            LogicalBuilding = null;
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<PubSubEvent<string>>().Publish("LogicalBuildingPage");
        }
        /// <summary>
        /// 修改逻辑幢
        /// </summary>
        private void EditLogicalBuilding()
        {
            if (LogicalBuilding == null) return;
            LogicalBuilding.UpdateTime = DateTime.Now;
            LogicalBuildingDal.Modify(LogicalBuilding);
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<PubSubEvent<string>>().Publish("LogicalBuildingPage");
        }


        private void InitialLogicalBuilding()
        {
            LogicalBuilding = new LogicalBuilding();
        }
    }
}