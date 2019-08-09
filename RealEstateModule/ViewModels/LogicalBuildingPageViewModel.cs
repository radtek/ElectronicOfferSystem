using BusinessData;
using BusinessData.Dal;
using BusinessData.Models;
using Common.Base;
using Common.Enums;
using Common.Events;
using Common.ValidationRules;
using Prism.Events;
using System;
using System.Globalization;

namespace RealEstateModule.ViewModels
{
    class LogicalBuildingPageViewModel : TablePage
    {
        #region Properties
        IEventAggregator EA;

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

        LogicalBuildingDal LogicalBuildingDal = new LogicalBuildingDal();

        #endregion

        #region ctor
        public LogicalBuildingPageViewModel(IEventAggregator ea)
        {
            EA = ea;
        }
        #endregion

        /// <summary>
        /// 能否执行新增或修改操作
        /// </summary>
        /// <returns></returns>
        public override bool canExecute()
        {
            if (LogicalBuilding == null) return false;

            bool isValid = true;
            CultureInfo cultureInfo = new CultureInfo("");
            // 非空验证
            NotEmptyValidationRule notEmptyValidationRule = new NotEmptyValidationRule();
            isValid &= notEmptyValidationRule.Validate(LogicalBuilding.LJZH, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(LogicalBuilding.ZRZH, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(LogicalBuilding.YSDM, cultureInfo).IsValid;
            // 数字验证
            NumbericValidationRule numbericValidationRule = new NumbericValidationRule();
            isValid &= numbericValidationRule.Validate(LogicalBuilding.YCJZMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(LogicalBuilding.YCDXMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(LogicalBuilding.YCQTMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(LogicalBuilding.SCJZMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(LogicalBuilding.SCDXMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(LogicalBuilding.SCQTMJ, cultureInfo).IsValid;

            return isValid;
        }

        public override void InitialTable()
        {
            LogicalBuilding = new LogicalBuilding();
        }

        public override void AddTable()
        {
            LogicalBuilding.ProjectID = Project.ID;
            LogicalBuilding.ID = Guid.NewGuid();
            LogicalBuilding.UpdateTime = DateTime.Now;
            LogicalBuildingDal.Add(LogicalBuilding);

            LogicalBuilding = null;
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.LogicalBuildingPage);
        }

        public override void EditTable()
        {
            LogicalBuilding.UpdateTime = DateTime.Now;
            LogicalBuildingDal.Modify(LogicalBuilding);
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.LogicalBuildingPage);
        }

        public override void InitialComboBoxList()
        {
        }

        public override void SelectBusiness(Business business)
        {
            LogicalBuilding = business?.LogicalBuilding;
        }
    }
}
