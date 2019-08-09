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
    public class MortgagePageViewModel : TablePage
    {
        #region Properties
        IEventAggregator EA;

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

        MortgageDal MortgageDal = new MortgageDal();

        #endregion

        public MortgagePageViewModel(IEventAggregator ea)
        {
            EA = ea;
            // 初始化下拉框
            InitialComboBoxList();
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

        public override bool canExecute()
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

        public override void InitialComboBoxList()
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

        public override void InitialTable()
        {
            Mortgage = new Mortgage();
        }

        public override void AddTable()
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

        public override void EditTable()
        {
            Mortgage.UpdateTime = DateTime.Now;
            SetPerson();
            MortgageDal.Modify(Mortgage);
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.MortgagePage);
        }

        public override void SelectBusiness(Business business)
        {
            Mortgage = business?.Mortgage;
        }
    }
}
