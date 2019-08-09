using BusinessData;
using BusinessData.Dal;
using BusinessData.Models;
using Common;
using Common.Base;
using Common.Enums;
using Common.Events;
using Common.ValidationRules;
using Common.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RealEstateModule.ViewModels
{
    public class FloorPageViewModel : TablePage
    {
        #region Properties
        IEventAggregator EA;


        private Floor floor;
        public Floor Floor
        {
            get { return floor; }
            set { SetProperty(ref floor, value); }
        }


        FloorDal FloorDal = new FloorDal();
        #endregion

        #region ctor
        public FloorPageViewModel(IEventAggregator ea) : base(ea)
        {
            EA = ea;
        }
        #endregion

        public override void AddTable()
        {
            Floor.ProjectID = Project.ID;
            Floor.ID = Guid.NewGuid();
            Floor.UpdateTime = DateTime.Now;
            FloorDal.Add(Floor);

            Floor = null;
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.FloorPage);
        }

        public override bool canExecute()
        {
            if (Floor == null) return false;

            bool isValid = true;
            CultureInfo cultureInfo = new CultureInfo("");
            // 非空验证
            NotEmptyValidationRule notEmptyValidationRule = new NotEmptyValidationRule();
            isValid &= notEmptyValidationRule.Validate(Floor.CH, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Floor.ZRZH, cultureInfo).IsValid;
            isValid &= notEmptyValidationRule.Validate(Floor.YSDM, cultureInfo).IsValid;
            // 数字验证
            NumbericValidationRule numbericValidationRule = new NumbericValidationRule();
            isValid &= numbericValidationRule.Validate(Floor.CJZMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(Floor.CTNJZMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(Floor.CYTMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(Floor.CGYJZMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(Floor.CFTJZMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(Floor.CBQMJ, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(Floor.CG, cultureInfo).IsValid;
            isValid &= numbericValidationRule.Validate(Floor.SPTYMJ, cultureInfo).IsValid;

            return isValid;
        }

        public override void EditTable()
        {
            Floor.UpdateTime = DateTime.Now;
            FloorDal.Modify(Floor);

            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<RefreshBusinessEvent>().Publish(ERealEstatePage.FloorPage);
        }

        public override void InitialComboBoxList()
        {
        }

        public override void InitialTable()
        {
            Floor = new Floor();
        }

        public override void SelectBusiness(Business business)
        {
            Floor = business?.Floor;
        }
    }
}
