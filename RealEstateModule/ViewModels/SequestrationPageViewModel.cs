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
    public class SequestrationPageViewModel : TablePage
    {

        #region Properties
        IEventAggregator EA;

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

        SequestrationDal SequestrationDal = new SequestrationDal();

        #endregion


        public SequestrationPageViewModel(IEventAggregator ea)
        {
            EA = ea;
        }

        public override bool canExecute()
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

        public override void InitialComboBoxList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = DictionaryUtil.GetDictionaryByName("查封类型");
            CFLXList = dic;
        }

        public override void InitialTable()
        {
            Sequestration = new Sequestration();
        }

        public override void AddTable()
        {
            Sequestration.ProjectID = Project.ID;
            Sequestration.ID = Guid.NewGuid();
            Sequestration.UpdateTime = DateTime.Now;
            SequestrationDal.Add(Sequestration);

            Sequestration = null;
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.SequestrationPage);
        }

        public override void EditTable()
        {
            Sequestration.UpdateTime = DateTime.Now;
            SequestrationDal.Modify(Sequestration);
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.SequestrationPage);
        }

        public override void SelectBusiness(Business business)
        {
            Sequestration = business?.Sequestration;
        }
    }
}
