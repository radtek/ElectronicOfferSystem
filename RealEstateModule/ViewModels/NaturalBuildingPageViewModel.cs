using BusinessData;
using BusinessData.Dal;
using BusinessData.Models;
using Common;
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
    class NaturalBuildingPageViewModel : BindableBase, INavigationAware
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
        /// 自然幢信息
        /// </summary>
        private NaturalBuilding naturalBuilding;
        public NaturalBuilding NaturalBuilding
        {
            get { return naturalBuilding; }
            set
            {
                SetProperty(ref naturalBuilding, value);
                //RaisePropertyChanged(NaturalBuilding?.BSM);
                //RaisePropertyChanged(NaturalBuilding?.YSDM);
                //RaisePropertyChanged(NaturalBuilding?.BDCDYH);
                //RaisePropertyChanged(NaturalBuilding?.ZDDM);
                //RaisePropertyChanged(NaturalBuilding?.ZRZH);
                //RaisePropertyChanged(NaturalBuilding?.ZYDMJ.ToString());
                //RaisePropertyChanged(NaturalBuilding?.ZZDMJ.ToString());
                //RaisePropertyChanged(NaturalBuilding?.DSCS);
                //RaisePropertyChanged(NaturalBuilding?.ZCS);
                //RaisePropertyChanged(NaturalBuilding?.DXCS);
                //RaisePropertyChanged(NaturalBuilding?.ZTS);
                //RaisePropertyChanged(NaturalBuilding?.JZWGD.ToString());
                //RaisePropertyChanged(NaturalBuilding?.SCJZMJ.ToString());
                //RaisePropertyChanged(NaturalBuilding?.YCJZMJ.ToString());
                //RaiseCanExecuteChanged();
            }
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
        /// 新增或修改自然幢
        /// </summary>
        public DelegateCommand AddOrEditNaturalBuildingCommand { get; set; }
        /// <summary>
        /// 在项目列表选择一个项目
        /// </summary>
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        /// <summary>
        /// 在自然幢列表选择一个自然幢
        /// </summary>
        public DelegateCommand<object> SelectBusinessCommand { get; set; }
        #endregion 

        NaturalBuildingDal NaturalBuildingDal = new NaturalBuildingDal();
        #endregion

        #region ctor
        public NaturalBuildingPageViewModel(IEventAggregator ea)
        {
            EA = ea;
            // 初始化下拉框
            InitialComboBoxList();
            // 新增或修改自然幢信息
            AddOrEditNaturalBuildingCommand = new DelegateCommand(() => {
                switch (ButtonContent)
                {
                    case "确认新增":
                        AddNaturalBuilding();
                        break;
                    case "确认修改":
                        EditNaturalBuilding();
                        break;
                    default:
                        break;
                }
            });

            // 选中自然幢列表中的一项
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

            // 初始自然幢数据
            InitialNaturalBuilding();          
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
        /// 选择自然幢列表的一项
        /// </summary>
        /// <param name="obj"></param>
        private void SelectBusiness(object obj)
        {
            try
            {
                // 加载自然幢数据
                ListView listView = obj as ListView;
                Business business = new Business();
                business = listView.SelectedItem as Business;
                NaturalBuilding = business?.NaturalBuilding;

                // 按钮为修改状态
                ButtonContent = "确认修改";
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

        }

        /// <summary>
        /// 新增自然幢
        /// </summary>
        private void AddNaturalBuilding()
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
                NaturalBuilding.ProjectID = Project.ID;
                NaturalBuilding.ID = Guid.NewGuid();
                NaturalBuilding.UpdateTime = DateTime.Now;
                NaturalBuildingDal.Add(NaturalBuilding);

                NaturalBuilding = null;
                // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
                EA.GetEvent<RefreshBusinessEvent>().Publish("NaturalBuildingPage");
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

        }

        /// <summary>
        /// 编辑自然幢
        /// </summary>
        private void EditNaturalBuilding()
        {
            if (NaturalBuilding == null)
            {
                MessageBox.Show("请选择自然幢", "提示");
                return;
            }
            if (!canExecute())
            {
                MessageBox.Show("验证失败", "提示");
                return;
            }
            try
            {
                NaturalBuilding.UpdateTime = DateTime.Now;
                NaturalBuildingDal.Modify(NaturalBuilding);

                // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
                EA.GetEvent<RefreshBusinessEvent>().Publish("NaturalBuildingPage");
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

        }

        /// <summary>
        /// 初始化自然幢
        /// </summary>
        private void InitialNaturalBuilding()
        {
            NaturalBuilding = new NaturalBuilding();
            
        }

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitialComboBoxList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = DictionaryUtil.GetDictionaryByName("房屋结构");
            FWJGList = dic;

            dic = DictionaryUtil.GetDictionaryByName("不动产单元状态");
            ZTList = dic;
            
        }

        /// <summary>
        /// 能否执行新增或修改操作
        /// </summary>
        /// <returns></returns>
        private bool canExecute()
        {
            if (NaturalBuilding == null) return false;

            bool isValid = true;
            CultureInfo cultureInfo = new CultureInfo("");
            // 非空验证
            NotEmptyValidationRule notEmptyValidationRule = new NotEmptyValidationRule();
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.BSM, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.YSDM, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.ZRZH, cultureInfo).IsValid;
            // 数字和非空验证
            NumbericAndNotEmptyValidationRule numbericAndNotEmptyValidationRule = new NumbericAndNotEmptyValidationRule();
            isValid &= numbericAndNotEmptyValidationRule.Validate(NaturalBuilding.ZYDMJ, cultureInfo).IsValid;
            isValid &= numbericAndNotEmptyValidationRule.Validate(NaturalBuilding.ZZDMJ, cultureInfo).IsValid;
            // 整数和非空验证
            IntegerAndNotEmptyValidationRule integerAndNotEmptyValidationRule = new IntegerAndNotEmptyValidationRule();
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.DSCS, cultureInfo).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.DXCS, cultureInfo).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.ZCS, cultureInfo).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.ZTS, cultureInfo).IsValid;
            // 数字验证
            NumbericValidationRule numbericValidationRule = new NumbericValidationRule();
            isValid &= numbericValidationRule.Validate(NaturalBuilding.JZWGD, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(NaturalBuilding.SCJZMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(NaturalBuilding.YCJZMJ, cultureInfo).IsValid;
            // 不动产单元号验证
            BDCDYHValidationRule bDCDYHValidationRule = new BDCDYHValidationRule();
            isValid &= bDCDYHValidationRule.Validate(NaturalBuilding.BDCDYH, cultureInfo).IsValid;
            // 宗地代码验证
            ZDDMValidationRule zDDMValidationRule = new ZDDMValidationRule();
            isValid &= zDDMValidationRule.Validate(NaturalBuilding.ZDDM, cultureInfo).IsValid;

            return isValid;
        }

        private void RaiseCanExecuteChanged()
        {
            this.AddOrEditNaturalBuildingCommand.RaiseCanExecuteChanged();           
        }

    }
}
