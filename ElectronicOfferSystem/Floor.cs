//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElectronicOfferSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Floor
    {
        public System.Guid ID { get; set; }
        public System.Guid ProjectID { get; set; }
        public System.Guid NaturalBuildingID { get; set; }
        public string CH { get; set; }
        public string ZRZH { get; set; }
        public string YSDM { get; set; }
        public string SJC { get; set; }
        public string MYC { get; set; }
        public double CJZMJ { get; set; }
        public double CTNJZMJ { get; set; }
        public double CYTMJ { get; set; }
        public double CGYJZMJ { get; set; }
        public double CFTJZMJ { get; set; }
        public int CBQMJ { get; set; }
        public double CG { get; set; }
        public double SPTYMJ { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public string CH86 { get; set; }
        public string CH87 { get; set; }
        public string CH88 { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual NaturalBuilding NaturalBuilding { get; set; }
    }
}
