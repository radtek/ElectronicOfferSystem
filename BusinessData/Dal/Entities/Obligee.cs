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
    /// 权利人
    /// </summary>
    public partial class Obligee
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
        /// 户标识码
        /// </summary>
        public string HBSM { get; set; }
        /// <summary>
        /// 权利人名称
        /// </summary>
        public string QLRMC { get; set; }
        /// <summary>
        /// 不动产权证号
        /// </summary>
        public string BDCQZH { get; set; }
        /// <summary>
        /// 证件种类
        /// </summary>
        public int ZJZL { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string ZJH { get; set; }
        /// <summary>
        /// 国家/地区
        /// </summary>
        public int GJ { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int XB { get; set; }
        /// <summary>
        /// 权利人类型
        /// </summary>
        public string QLRLX { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string DH { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string YB { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string DZ { get; set; }
        /// <summary>
        /// 权利类型
        /// </summary>
        public int QLLX { get; set; }
        /// <summary>
        /// 共有方式
        /// </summary>
        public int GYFS { get; set; }
        /// <summary>
        /// 权利面积
        /// </summary>
        public double QLMJ { get; set; }
        /// <summary>
        /// 权利比例
        /// </summary>
        public string QLBL { get; set; }
        /// <summary>
        /// 法人姓名
        /// </summary>
        public string FRXM { get; set; }
        /// <summary>
        /// 法人证件类型
        /// </summary>
        public int FRZJLX { get; set; }
        /// <summary>
        /// 法人证件号
        /// </summary>
        public string FRZJH { get; set; }
        /// <summary>
        /// 法人电话
        /// </summary>
        public string FRDH { get; set; }
        /// <summary>
        /// 代理人姓名
        /// </summary>
        public string DLRXM { get; set; }
        /// <summary>
        /// 代理人证件类型
        /// </summary>
        public int DLRZJLX { get; set; }
        /// <summary>
        /// 代理人证件号
        /// </summary>
        public string DLRZJH { get; set; }
        /// <summary>
        /// 代理人电话
        /// </summary>
        public string DLRDH { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW { get; set; }
        /// <summary>
        /// 代理机构名称
        /// </summary>
        public string DLJGMC { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string DZYJ { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime UpdateTime { get; set; }
    
        public virtual Project Project { get; set; }
    }
}
