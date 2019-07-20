using BusinessData;
using BusinessData.Dal;
using Common;
using Common.Models;
using Common.Utils;
using Common.ValidationRules;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RealEstateModule.ViewModels
{
    class NaturalBuildingPageViewModel : BindableBase, INavigationAware
    {
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
            }
        }

        #region 需要验证的字段
        /// <summary>
        /// 标识码/唯一码
        /// </summary>
        public string BSM
        {
            get { return NaturalBuilding.BSM; }
            set
            {
                NaturalBuilding.BSM = value;
                RaisePropertyChanged("BSM");
                RaiseCanExecuteChanged();
            }

        }
        /// <summary>
        /// 要素代码
        /// </summary>
        public string YSDM
        {
            get { return NaturalBuilding.YSDM; }
            set
            {
                NaturalBuilding.YSDM = value;
                RaisePropertyChanged("YSDM");
                RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH
        {
            get { return NaturalBuilding.BDCDYH; }
            set
            {
                NaturalBuilding.BDCDYH = value;
                RaisePropertyChanged("BDCDYH");
                RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 宗地代码
        /// </summary>
        public string ZDDM
        {
            get { return NaturalBuilding.ZDDM; }
            set
            {
                NaturalBuilding.ZDDM = value;
                RaisePropertyChanged("ZDDM");
                RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 自然幢号
        /// </summary>
        public string ZRZH
        {
            get { return NaturalBuilding.ZRZH; }
            set
            {
                NaturalBuilding.ZRZH = value;
                RaisePropertyChanged("ZRZH");
                RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 幢用地面积
        /// </summary>
        public double ZYDMJ
        {
            get { return NaturalBuilding.ZYDMJ; }
            set
            {
                NaturalBuilding.ZYDMJ = value;
                RaisePropertyChanged("ZYDMJ");
                RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 幢占地面积
        /// </summary>
        public double ZZDMJ
        {
            get { return NaturalBuilding.ZZDMJ; }
            set
            {
                NaturalBuilding.ZZDMJ = value;
                RaisePropertyChanged("ZZDMJ");
                RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 地上层数
        /// </summary>
        public int DSCS
        {
            get { return NaturalBuilding.DSCS; }
            set
            {
                NaturalBuilding.DSCS = value;
                RaisePropertyChanged("DSCS");
                RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 总层数
        /// </summary>
        public int ZCS
        {
            get { return NaturalBuilding.ZCS; }
            set
            {
                NaturalBuilding.ZCS = value;
                RaisePropertyChanged("ZCS");
                RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 地下层数
        /// </summary>
        public int DXCS
        {
            get { return NaturalBuilding.DXCS; }
            set
            {
                NaturalBuilding.DXCS = value;
                RaisePropertyChanged("DXCS");
                RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 总套数
        /// </summary>
        public int ZTS
        {
            get { return NaturalBuilding.ZTS; }
            set
            {
                NaturalBuilding.ZTS = value;
                RaisePropertyChanged("ZTS");
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// 建筑物高度
        /// </summary>
        public double? JZWGD
        {
            get { return NaturalBuilding.JZWGD; }
            set
            {
                NaturalBuilding.JZWGD = value;
                RaisePropertyChanged("JZWGD");
                RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 实测建筑面积
        /// </summary>
        public double? SCJZMJ
        {
            get { return NaturalBuilding.SCJZMJ; }
            set
            {
                NaturalBuilding.SCJZMJ = value;
                RaisePropertyChanged("SCJZMJ");
                RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 预测建筑面积
        /// </summary>
        public double? YCJZMJ
        {
            get { return NaturalBuilding.YCJZMJ; }
            set
            {
                NaturalBuilding.YCJZMJ = value;
                RaisePropertyChanged("YCJZMJ");
                RaiseCanExecuteChanged();
            }
        }

        #endregion


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
            },canExecute);

            // 初始化自然幢（必须放在新增命令之下）
            NaturalBuilding = new NaturalBuilding();

            // 选中自然幢列表中的一项
            SelectBusinessCommand = new DelegateCommand<object>(SelectBusiness);
            GlobalCommands.SelectBusinessCommand.RegisterCommand(SelectBusinessCommand);
        }



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
            //InitialNaturalBuilding();
            
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
            Business<NaturalBuilding> business = new Business<NaturalBuilding>();
            business = listView.SelectedItem as Business<NaturalBuilding>;
            var item = listView.SelectedItem;
            if (item == null) return;
            Type type = item.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                var name = pi.Name;
                var value = pi.GetValue(item, null);
                var t = value?.GetType() ?? typeof(object);
                if (value.GetType() == typeof(NaturalBuilding))
                {
                    NaturalBuilding = value as NaturalBuilding;
                }
            }

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
            // 默认房屋结构为"钢结构"
            //NaturalBuilding.FWJG = "1";
            // 默认状态为"有效"
            //NaturalBuilding.ZT = "1";
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
            isValid &= notEmptyValidationRule.Validate(BSM, new CultureInfo("")).IsValid;
            isValid &= notEmptyValidationRule.Validate(YSDM, new CultureInfo("")).IsValid;
            isValid &= notEmptyValidationRule.Validate(BDCDYH, new CultureInfo("")).IsValid;
            isValid &= notEmptyValidationRule.Validate(ZDDM, new CultureInfo("")).IsValid;
            isValid &= notEmptyValidationRule.Validate(ZRZH, new CultureInfo("")).IsValid;
            // 数字和非空验证
            NumbericAndNotEmptyValidationRule numbericAndNotEmptyValidationRule = new NumbericAndNotEmptyValidationRule();
            isValid &= numbericAndNotEmptyValidationRule.Validate(ZYDMJ, new CultureInfo("")).IsValid;
            isValid &= numbericAndNotEmptyValidationRule.Validate(ZZDMJ, new CultureInfo("")).IsValid;
            // 整数和非空验证
            IntegerAndNotEmptyValidationRule integerAndNotEmptyValidationRule = new IntegerAndNotEmptyValidationRule();
            isValid &= integerAndNotEmptyValidationRule.Validate(DSCS, new CultureInfo("")).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(DXCS, new CultureInfo("")).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(ZCS, new CultureInfo("")).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(ZTS, new CultureInfo("")).IsValid;
            // 数字验证
            NumbericValidationRule numbericValidationRule = new NumbericValidationRule();
            isValid &= numbericValidationRule.Validate(JZWGD, new CultureInfo("")).IsValid;
            isValid &= numbericValidationRule.Validate(SCJZMJ, new CultureInfo("")).IsValid;
            isValid &= numbericValidationRule.Validate(YCJZMJ, new CultureInfo("")).IsValid;

            return isValid;
        }

        private void RaiseCanExecuteChanged()
        {
            this.AddOrEditNaturalBuildingCommand.RaiseCanExecuteChanged();
           
        }

    }
}
