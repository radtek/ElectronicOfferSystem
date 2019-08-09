using BusinessData;
using BusinessData.Models;
using Common.Enums;
using Common.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Common.Base
{
    public abstract class TablePage : BindableBase, INavigationAware
    {
        #region Properties

        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }

        private EMappingType mappingType;
        /// <summary>
        /// 测绘类型
        /// </summary>
        public EMappingType MappingType
        {
            get { return mappingType; }
            set { SetProperty(ref mappingType, value); }
        }


        private string buttonContent = "确认新增";
        /// <summary>
        /// 新增/修改按钮内容
        /// </summary>
        public string ButtonContent
        {
            get { return buttonContent; }
            set { SetProperty(ref buttonContent, value); }
        }

        #region 命令

        /// <summary>
        /// 新增或修改表格
        /// </summary>
        public DelegateCommand AddOrEditTableCommand { get; set; }
        public DelegateCommand<object> SelectBusinessCommand { get; set; }
        #endregion

        #endregion

        #region ctor

        public TablePage()
        {

            // 初始化下拉框
            InitialComboBoxList();
            // 新增或修改表格信息
            AddOrEditTableCommand = new DelegateCommand(() => {
                switch (ButtonContent)
                {
                    case "确认新增":
                        AddTableAOP();
                        break;
                    case "确认修改":
                        EditTableAOP();
                        break;
                    default:
                        break;
                }
            });

            // 选中业务列表中的一项
            SelectBusinessCommand = new DelegateCommand<object>(SelectBusinessAOP);
            GlobalCommands.SelectBusinessCommand.RegisterCommand(SelectBusinessCommand);
        }

        /// <summary>
        /// 选中业务列表中的一项
        /// </summary>
        /// <param name="obj"></param>
        public void SelectBusinessAOP(object obj)
        {
            try
            {
                // 加载自业务数据
                ListView listView = obj as ListView;
                Business business = new Business();
                business = listView.SelectedItem as Business;

                SelectBusiness(business);
                // 按钮为修改状态
                ButtonContent = "确认修改";
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }
        }


        public void AddTableAOP()
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
                AddTable();
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }
        }


        public void EditTableAOP()
        {
            if (!canExecute())
            {
                MessageBox.Show("验证失败", "提示");
                return;
            }
            try
            {
                EditTable();
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }
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
                MappingType = (EMappingType)int.Parse(Project.MappingType);
            }

            // 初始自然幢数据
            InitialTable();
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
        /// 初始表格
        /// </summary>
        public abstract void InitialTable();
        /// <summary>
        /// 新增表格
        /// </summary>
        public abstract void AddTable();
        /// <summary>
        /// 编辑表格
        /// </summary>
        public abstract void EditTable();

        /// <summary>
        /// 初始下拉框
        /// </summary>
        public abstract void InitialComboBoxList();
        /// <summary>
        /// 选择业务列表中的一项
        /// </summary>
        public abstract void SelectBusiness(Business business);
        /// <summary>
        /// 能否执行新增或修改
        /// </summary>
        /// <returns></returns>
        public abstract bool canExecute();
    }
}
