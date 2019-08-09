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
    public class ObligeePageViewModel : TablePage
    {

        #region Properties
        IEventAggregator EA;

        private Obligee obligee;
        public Obligee Obligee
        {
            get { return obligee; }
            set { SetProperty(ref obligee, value); }
        }

        #region 字典
        /// <summary>
        /// 证件种类
        /// </summary>
        private Dictionary<string, string> zjlxList;
        public Dictionary<string, string> ZJLXList
        {
            get { return zjlxList; }
            set { SetProperty(ref zjlxList, value); }
        }
        /// <summary>
        /// 国家
        /// </summary>
        private Dictionary<string, string> gjList;
        public Dictionary<string, string> GJList
        {
            get { return gjList; }
            set { SetProperty(ref gjList, value); }
        }
        /// <summary>
        /// 性别
        /// </summary>
        private Dictionary<string, string> xbList;
        public Dictionary<string, string> XBList
        {
            get { return xbList; }
            set { SetProperty(ref xbList, value); }
        }
        /// <summary>
        /// 权利人类型
        /// </summary>
        private Dictionary<string, string> qlrlxList;
        public Dictionary<string, string> QLRLXList
        {
            get { return qlrlxList; }
            set { SetProperty(ref qlrlxList, value); }
        }
        /// <summary>
        /// 权利类型
        /// </summary>
        private Dictionary<string, string> qllxList;
        public Dictionary<string, string> QLLXList
        {
            get { return qllxList; }
            set { SetProperty(ref qllxList, value); }
        }
        /// <summary>
        /// 共有方式
        /// </summary>
        private Dictionary<string, string> gyfsList;
        public Dictionary<string, string> GYFSList
        {
            get { return gyfsList; }
            set { SetProperty(ref gyfsList, value); }
        }

        #endregion

        ObligeeDal ObligeeDal = new ObligeeDal();

        #endregion

        #region ctor
        public ObligeePageViewModel(IEventAggregator ea)
        {
            EA = ea;
        }
        #endregion

        private void SetPerson()
        {
            if (string.IsNullOrWhiteSpace(Obligee.FRXM)) // 若法人姓名为空
            {
                Obligee.FRZJLX = null;
                Obligee.FRZJH = null;
                Obligee.FRDH = null;
            }
            if (string.IsNullOrWhiteSpace(Obligee.DLRXM)) // 若代理人姓名为空
            {
                Obligee.DLRZJLX = null;
                Obligee.DLRZJH = null;
                Obligee.DLRDH = null;
            }
        }

        public override void InitialComboBoxList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = DictionaryUtil.GetDictionaryByName("证件类型");
            ZJLXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("国家和地区");
            GJList = dic;

            dic = DictionaryUtil.GetDictionaryByName("性别");
            XBList = dic;

            dic = DictionaryUtil.GetDictionaryByName("权利人类型");
            QLRLXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("权利类型");
            QLLXList = dic;

            dic = DictionaryUtil.GetDictionaryByName("共有方式");
            GYFSList = dic;

        }

        /// <summary>
        /// 能否执行新增或修改操作
        /// </summary>
        /// <returns></returns>
        public override bool canExecute()
        {
            if (Obligee == null) return false;

            bool isValid = true;
            CultureInfo cultureInfo = new CultureInfo("");
            // 非空验证
            NotEmptyValidationRule notEmptyValidationRule = new NotEmptyValidationRule();
            isValid &= notEmptyValidationRule.Validate(Obligee.HBSM, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.QLRMC, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.ZJZL, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.ZJH, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.GJ, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.XB, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.QLRLX, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.QLLX, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Obligee.GYFS, cultureInfo).IsValid;

            if (!string.IsNullOrWhiteSpace(Obligee.FRXM)) // 若法人姓名不为空
            {
                isValid &= notEmptyValidationRule.Validate(Obligee.FRZJLX, cultureInfo).IsValid;
                isValid &= notEmptyValidationRule.Validate(Obligee.FRZJH, cultureInfo).IsValid;
            }
            if (!string.IsNullOrWhiteSpace(Obligee.DLRXM)) // 若代理人姓名不为空
            {
                isValid &= notEmptyValidationRule.Validate(Obligee.DLRZJLX, cultureInfo).IsValid;
                isValid &= notEmptyValidationRule.Validate(Obligee.DLRZJH, cultureInfo).IsValid;
            }

            // 数字验证
            NumbericValidationRule numbericValidationRule = new NumbericValidationRule();
            isValid &= numbericValidationRule.Validate(Obligee.QLMJ, cultureInfo).IsValid;
            
            return isValid;
        }

        public override void InitialTable()
        {
            Obligee = new Obligee();
        }

        public override void AddTable()
        {
            Obligee.ProjectID = Project.ID;
            Obligee.ID = Guid.NewGuid();
            Obligee.UpdateTime = DateTime.Now;

            SetPerson();

            ObligeeDal.Add(Obligee);

            Obligee = null;
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.ObligeePage);
        }

        public override void EditTable()
        {
            Obligee.UpdateTime = DateTime.Now;
            SetPerson();
            ObligeeDal.Modify(Obligee);
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.ObligeePage);
        }

        public override void SelectBusiness(Business business)
        {
            Obligee = business?.Obligee;
        }
    }
}
