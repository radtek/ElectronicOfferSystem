using BusinessData;
using BusinessData.Dal;
using BusinessData.Models;
using Common.Base;
using Common.Enums;
using Common.Events;
using Common.Utils;
using Common.ValidationRules;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace RealEstateModule.ViewModels
{
    class HouseholdPageViewModel : TablePage
    {
        #region Properties
        IEventAggregator EA;
        
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

        HouseholdDal HouseholdDal = new HouseholdDal();
        #endregion

        #region ctor
        public HouseholdPageViewModel(IEventAggregator ea) : base(ea)
        {
            EA = ea;
        }
        #endregion





        /// <summary>
        /// 初始化下拉框
        /// </summary>
        public override void InitialComboBoxList()
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
        public override bool canExecute()
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

        public override void InitialTable()
        {
            Household = new Household();
        }

        public override void AddTable()
        {
            Household.ProjectID = Project.ID;
            Household.ID = Guid.NewGuid();
            Household.UpdateTime = DateTime.Now;
            HouseholdDal.Add(Household);

            Household = null;
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.HouseholdPage);
        }

        public override void EditTable()
        {
            Household.UpdateTime = DateTime.Now;
            HouseholdDal.Modify(Household);

            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.HouseholdPage);
        }

        public override void SelectBusiness(Business business)
        {
            Household = business?.Household;
        }
    }
}
