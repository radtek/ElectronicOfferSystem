//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessData
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// 逻辑幢
    /// </summary>
    public partial class LogicalBuilding
    {
        /// <summary>
        /// ID
        /// </summary>
        public System.Guid ID { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public System.Guid ProjectID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ZRZH { get; set; }
        /// <summary>
        /// 逻辑幢号
        /// </summary>
        public string LJZH { get; set; }
        /// <summary>
        /// 要素代码
        /// </summary>
        public string YSDM { get; set; }
        /// <summary>
        /// 门牌号
        /// </summary>
        public string MPH { get; set; }
        /// <summary>
        /// 预测建筑面积
        /// </summary>
        public Nullable<double> YCJZMJ { get; set; }
        /// <summary>
        /// 预测地下面积
        /// </summary>
        public Nullable<double> YCDXMJ { get; set; }
        /// <summary>
        /// 预测其他面积
        /// </summary>
        public Nullable<double> YCQTMJ { get; set; }
        /// <summary>
        /// 实测建筑面积
        /// </summary>
        public Nullable<double> SCJZMJ { get; set; }
        /// <summary>
        /// 实测地下面积
        /// </summary>
        public Nullable<double> SCDXMJ { get; set; }
        /// <summary>
        /// 实测其他面积
        /// </summary>
        public Nullable<double> SCQTMJ { get; set; }
        /// <summary>
        /// 竣工日期
        /// </summary>
        public Nullable<System.DateTime> JGRQ { get; set; }
        /// <summary>
        /// 房屋结构1
        /// </summary>
        public string FWJG1 { get; set; }
        /// <summary>
        /// 房屋结构2
        /// </summary>
        public string FWJG2 { get; set; }
        /// <summary>
        /// 房屋结构3
        /// </summary>
        public string FWJG3 { get; set; }
        /// <summary>
        /// 建筑物状态
        /// </summary>
        public string JZWZT { get; set; }
        /// <summary>
        /// 总层数
        /// </summary>
        public string ZCS { get; set; }
        /// <summary>
        /// 地上层数
        /// </summary>
        public string DSCS { get; set; }
        /// <summary>
        /// 地下层数
        /// </summary>
        public string DXCS { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime UpdateTime { get; set; }
    }
}
