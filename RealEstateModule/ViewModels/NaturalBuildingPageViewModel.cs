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
    public class NaturalBuildingPageViewModel : TablePage
    {

        #region Properties
        IEventAggregator EA;
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
        /// 房屋用途
        /// </summary>
        private Dictionary<string, string> fwytList;
        public Dictionary<string, string> FWYTList
        {
            get { return fwytList; }
            set { SetProperty(ref fwytList, value); }
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

        #endregion


        NaturalBuildingDal NaturalBuildingDal = new NaturalBuildingDal();
        #region ctor
        public NaturalBuildingPageViewModel(IEventAggregator ea) : base(ea)
        {
            EA = ea;
        }
        #endregion

  
        public override void AddTable()
        {
            NaturalBuilding.ProjectID = Project.ID;
            NaturalBuilding.ID = Guid.NewGuid();
            NaturalBuilding.UpdateTime = DateTime.Now;
            NaturalBuildingDal.Add(NaturalBuilding);

            NaturalBuilding = null;
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.NaturalBuildingPage);
        }



        public override void EditTable()
        {
            NaturalBuilding.UpdateTime = DateTime.Now;
            NaturalBuildingDal.Modify(NaturalBuilding);

            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.NaturalBuildingPage);
        }

        public override void InitialComboBoxList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = DictionaryUtil.GetDictionaryByName("房屋结构");
            FWJGList = dic;

            dic = DictionaryUtil.GetDictionaryByName("房屋用途");
            FWYTList = dic;

            dic = DictionaryUtil.GetDictionaryByName("不动产单元状态");
            ZTList = dic;
        }

        public override void InitialTable()
        {
            NaturalBuilding = new NaturalBuilding();
        }

        public override void SelectBusiness(Business business)
        {
            NaturalBuilding = business?.NaturalBuilding;
        }

        public override bool canExecute()
        {
            if (NaturalBuilding == null) return false;

            bool isValid = true;
            CultureInfo cultureInfo = new CultureInfo("");
            // 非空验证
            NotEmptyValidationRule notEmptyValidationRule = new NotEmptyValidationRule();
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.BSM, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.YSDM, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.ZRZH, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.GHYT, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.FWJG, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(NaturalBuilding.ZT, cultureInfo).IsValid;
            // 数字和非空验证
            NumbericAndNotEmptyValidationRule numbericAndNotEmptyValidationRule = new NumbericAndNotEmptyValidationRule();
            isValid &= numbericAndNotEmptyValidationRule.Validate(NaturalBuilding.ZYDMJ, cultureInfo).IsValid;
            isValid &= numbericAndNotEmptyValidationRule.Validate(NaturalBuilding.ZZDMJ, cultureInfo).IsValid;
            if (MappingType == EMappingType.PredictiveMapping)
                isValid &= numbericAndNotEmptyValidationRule.Validate(NaturalBuilding.YCJZMJ, cultureInfo).IsValid;
            else if (MappingType == EMappingType.SurveyingMapping)
                isValid &= numbericAndNotEmptyValidationRule.Validate(NaturalBuilding.SCJZMJ, cultureInfo).IsValid;
            // 整数和非空验证
            IntegerAndNotEmptyValidationRule integerAndNotEmptyValidationRule = new IntegerAndNotEmptyValidationRule();
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.DSCS, cultureInfo).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.DXCS, cultureInfo).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.ZCS, cultureInfo).IsValid;
            isValid &= integerAndNotEmptyValidationRule.Validate(NaturalBuilding.ZTS, cultureInfo).IsValid;
            // 数字验证
            NumbericValidationRule numbericValidationRule = new NumbericValidationRule();
            isValid &= numbericValidationRule.Validate(NaturalBuilding.JZWGD, cultureInfo).IsValid;
            //isValid &= numbericValidationRule.Validate(NaturalBuilding.SCJZMJ, cultureInfo).IsValid;
            //isValid &= numbericValidationRule.Validate(NaturalBuilding.YCJZMJ, cultureInfo).IsValid;
            // 不动产单元号验证
            BDCDYHValidationRule bDCDYHValidationRule = new BDCDYHValidationRule();
            isValid &= bDCDYHValidationRule.Validate(NaturalBuilding.BDCDYH, cultureInfo).IsValid;
            // 宗地代码验证
            ZDDMValidationRule zDDMValidationRule = new ZDDMValidationRule();
            isValid &= zDDMValidationRule.Validate(NaturalBuilding.ZDDM, cultureInfo).IsValid;

            return isValid;
        }
    }
}
