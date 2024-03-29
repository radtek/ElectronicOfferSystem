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
    /// 抵押信息
    /// </summary>
    public partial class Mortgage
    {
        /// <summary>
        /// 
        /// </summary>
        public System.Guid ID { get; set; }
        /// <summary>
        /// 项目编号
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
        /// 证件类型
        /// </summary>
        public string ZJLX { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string ZJH { get; set; }
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
        /// 法人姓名
        /// </summary>
        public string FRXM { get; set; }
        /// <summary>
        /// 法人证件类型
        /// </summary>
        public string FRZJLX { get; set; }
        /// <summary>
        /// 法人证件号
        /// </summary>
        public string FRZJH { get; set; }
        /// <summary>
        /// 法人电话
        /// </summary>
        public string FRDH { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
        /// <summary>
        /// 抵押方式
        /// </summary>
        public string DYFS { get; set; }
        /// <summary>
        /// 债权单位
        /// </summary>
        public string ZQDW { get; set; }
        /// <summary>
        /// 抵押人
        /// </summary>
        public string DYR { get; set; }
        /// <summary>
        /// 抵押人证件类型
        /// </summary>
        public string DYRZJLX { get; set; }
        /// <summary>
        /// 抵押人证件号
        /// </summary>
        public string DYRZJH { get; set; }
        /// <summary>
        /// 抵押不动产类型
        /// </summary>
        public string DYBDCLX { get; set; }
        /// <summary>
        /// 持证方式
        /// </summary>
        public string CZFS { get; set; }
        /// <summary>
        /// 抵押评估价值
        /// </summary>
        public string DYPGJZ { get; set; }
        /// <summary>
        /// 被担保主债权数额
        /// </summary>
        public string BDBZZQSE { get; set; }
        /// <summary>
        /// 最高债券数额
        /// </summary>
        public string ZGZQSE { get; set; }
        /// <summary>
        /// 债务人
        /// </summary>
        public string ZWR { get; set; }
        /// <summary>
        /// 最高债权确定事实
        /// </summary>
        public string ZGZQQDSS { get; set; }
        /// <summary>
        /// 在建建筑物坐落
        /// </summary>
        public string ZJJZWZL { get; set; }
        /// <summary>
        /// 在建建筑物抵押范围
        /// </summary>
        public string ZJJZWDYFW { get; set; }
        /// <summary>
        /// 债务履行起始时间
        /// </summary>
        public Nullable<System.DateTime> ZWLXQSSJ { get; set; }
        /// <summary>
        /// 债务履行结束时间
        /// </summary>
        public Nullable<System.DateTime> ZWLXJSSJ { get; set; }
        /// <summary>
        /// 登记时间
        /// </summary>
        public Nullable<System.DateTime> DJSJ { get; set; }
        /// <summary>
        /// 登簿人
        /// </summary>
        public string DBR { get; set; }
        /// <summary>
        /// 附记
        /// </summary>
        public string FJ { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime UpdateTime { get; set; }
    }
}
