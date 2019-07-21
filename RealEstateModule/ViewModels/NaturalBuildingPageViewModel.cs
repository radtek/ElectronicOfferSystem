using BusinessData;
using BusinessData.Dal;
using BusinessData.Models;
using Common;
using Common.Utils;
using Common.ValidationRules;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;

namespace RealEstateModule.ViewModels
{
    class NaturalBuildingPageViewModel : BindableBase, INavigationAware
    {
        #region Properties
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
                RaisePropertyChanged(NaturalBuilding?.BSM);
                RaisePropertyChanged(NaturalBuilding?.YSDM);
                RaisePropertyChanged(NaturalBuilding?.BDCDYH);
                RaisePropertyChanged(NaturalBuilding?.ZDDM);
                RaisePropertyChanged(NaturalBuilding?.ZRZH);
                RaisePropertyChanged(NaturalBuilding?.ZYDMJ.ToString());
                RaisePropertyChanged(NaturalBuilding?.ZZDMJ.ToString());
                RaisePropertyChanged(NaturalBuilding?.DSCS);
                RaisePropertyChanged(NaturalBuilding?.ZCS);
                RaisePropertyChanged(NaturalBuilding?.DXCS);
                RaisePropertyChanged(NaturalBuilding?.ZTS);
                RaisePropertyChanged(NaturalBuilding?.JZWGD.ToString());
                RaisePropertyChanged(NaturalBuilding?.SCJZMJ.ToString());
                RaisePropertyChanged(NaturalBuilding?.YCJZMJ.ToString());
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
        public NaturalBuildingPageViewModel()
        {         
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
            // 加载自然幢数据
            ListView listView = obj as ListView;
            Business business = new Business();
            business = listView.SelectedItem as Business;
            NaturalBuilding = business?.NaturalBuilding;

            // 按钮为修改状态
            ButtonContent = "确认修改";
        }

        /// <summary>
        /// 新增自然幢
        /// </summary>
        private void AddNaturalBuilding()
        {
            if (Project == null) return;
            if (NaturalBuilding == null) return;
            NaturalBuilding.ProjectID = Project.ID;
            NaturalBuilding.ID = Guid.NewGuid();
            NaturalBuilding.UpdateTime = DateTime.Now;
            NaturalBuildingDal.Add(NaturalBuilding);

            NaturalBuilding = null;
        }

        /// <summary>
        /// 编辑自然幢
        /// </summary>
        private void EditNaturalBuilding()
        {
            if (NaturalBuilding == null) return;
            NaturalBuilding.UpdateTime = DateTime.Now;
            NaturalBuildingDal.Modify(NaturalBuilding);
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
            // 非空验证
            NotEmptyValidationRule notEmptyValidationRule = new NotEmptyValidationRule();
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.BSM, new CultureInfo("")).IsValid;
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.YSDM, new CultureInfo("")).IsValid;
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.BDCDYH, new CultureInfo("")).IsValid;
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.ZDDM, new CultureInfo("")).IsValid;
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.ZRZH, new CultureInfo("")).IsValid;
            // 数字和非空验证
            NumbericAndNotEmptyValidationRule numbericAndNotEmptyValidationRule = new NumbericAndNotEmptyValidationRule();
            isValid &= numbericAndNotEmptyValidationRule.Validate(NaturalBuilding.ZYDMJ, new CultureInfo("")).IsValid;
            isValid &= numbericAndNotEmptyValidationRule.Validate(NaturalBuilding.ZZDMJ, new CultureInfo("")).IsValid;
            // 整数和非空验证
            IntegerAndNotEmptyValidationRule integerAndNotEmptyValidationRule = new IntegerAndNotEmptyValidationRule();
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.DSCS, new CultureInfo("")).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.DXCS, new CultureInfo("")).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.ZCS, new CultureInfo("")).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.ZTS, new CultureInfo("")).IsValid;
            // 数字验证
            NumbericValidationRule numbericValidationRule = new NumbericValidationRule();
            isValid &= numbericValidationRule.Validate(NaturalBuilding.JZWGD, new CultureInfo("")).IsValid;
            isValid &= numbericValidationRule.Validate(NaturalBuilding.SCJZMJ, new CultureInfo("")).IsValid;
            isValid &= numbericValidationRule.Validate(NaturalBuilding.YCJZMJ, new CultureInfo("")).IsValid;

            return isValid;
        }

        private void RaiseCanExecuteChanged()
        {
            this.AddOrEditNaturalBuildingCommand.RaiseCanExecuteChanged();           
        }

    }
}
