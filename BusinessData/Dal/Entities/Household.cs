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
    /// 户
    /// </summary>
    public partial class Household
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
        /// 自然幢ID
        /// </summary>
        public System.Guid NaturalBuildingID { get; set; }
        /// <summary>
        /// 户标识码
        /// </summary>
        public string HBSM { get; set; }
        /// <summary>
        /// 原系统标识
        /// </summary>
        public string YXTBS { get; set; }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }
        /// <summary>
        /// 房屋编码
        /// </summary>
        public string FWBM { get; set; }
        /// <summary>
        /// 要素代码
        /// </summary>
        public string YSDM { get; set; }
        /// <summary>
        /// 自然幢号
        /// </summary>
        public string ZRZH { get; set; }
        /// <summary>
        /// 逻辑幢号
        /// </summary>
        public string LJZH { get; set; }
        /// <summary>
        /// 单元号
        /// </summary>
        public string DYH { get; set; }
        /// <summary>
        /// 总层数
        /// </summary>
        public int ZCS { get; set; }
        /// <summary>
        /// 层号
        /// </summary>
        public string CH { get; set; }
        /// <summary>
        /// 房号
        /// </summary>
        public string FH { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }
        /// <summary>
        /// 面积单位
        /// </summary>
        public int MJDW { get; set; }
        /// <summary>
        /// 所在层
        /// </summary>
        public string SZC { get; set; }
        /// <summary>
        /// 起始层
        /// </summary>
        public string QSC { get; set; }
        /// <summary>
        /// 终止层
        /// </summary>
        public string ZZC { get; set; }
        /// <summary>
        /// 户号
        /// </summary>
        public string HH { get; set; }
        /// <summary>
        /// 室号部位
        /// </summary>
        public string SHBW { get; set; }
        /// <summary>
        /// 户型
        /// </summary>
        public int HX { get; set; }
        /// <summary>
        /// 户型结构
        /// </summary>
        public int HXJG { get; set; }
        /// <summary>
        /// 规划用途
        /// </summary>
        public string GHYT { get; set; }
        /// <summary>
        /// 房屋用途1
        /// </summary>
        public int FWYT1 { get; set; }
        /// <summary>
        /// 房屋用途2
        /// </summary>
        public int FWYT2 { get; set; }
        /// <summary>
        /// 房屋用途3
        /// </summary>
        public int FWYT3 { get; set; }
        /// <summary>
        /// 预测建筑面积
        /// </summary>
        public double YCJZMJ { get; set; }
        /// <summary>
        /// 预测套内建筑面积
        /// </summary>
        public double YCTNJZMJ { get; set; }
        /// <summary>
        /// 预测分摊建筑面积
        /// </summary>
        public double YCFTJZMJ { get; set; }
        /// <summary>
        /// 预测地下部分建筑面积
        /// </summary>
        public double YCDXBFJZMJ { get; set; }
        /// <summary>
        /// 预测其它建筑面积
        /// </summary>
        public double YCQTJZMJ { get; set; }
        /// <summary>
        /// 预测分摊系数
        /// </summary>
        public string YCFTXS { get; set; }
        /// <summary>
        /// 实测建筑面积
        /// </summary>
        public double SCJZMJ { get; set; }
        /// <summary>
        /// 实测套内建筑面积
        /// </summary>
        public double SCTNJZMJ { get; set; }
        /// <summary>
        /// 实测分摊建筑面积
        /// </summary>
        public double SCFTJZMJ { get; set; }
        /// <summary>
        /// 实测地下部分建筑面积
        /// </summary>
        public double SCDXBFJZMJ { get; set; }
        /// <summary>
        /// 实测其它建筑面积
        /// </summary>
        public double SCQTJZMJ { get; set; }
        /// <summary>
        /// 实测分摊系数
        /// </summary>
        public string SCFTXS { get; set; }
        /// <summary>
        /// 共有土地面积
        /// </summary>
        public double GYTDMJ { get; set; }
        /// <summary>
        /// 分摊土地面积
        /// </summary>
        public double FTTDMJ { get; set; }
        /// <summary>
        /// 独用土地面积
        /// </summary>
        public double DYTDMJ { get; set; }
        /// <summary>
        /// 房屋类型
        /// </summary>
        public int FWLX { get; set; }
        /// <summary>
        /// 房屋结构
        /// </summary>
        public int FWJG { get; set; }
        /// <summary>
        /// 房屋性质
        /// </summary>
        public int FWXZ { get; set; }
        /// <summary>
        /// 房屋产别
        /// </summary>
        public int FWCB { get; set; }
        /// <summary>
        /// 房地产交易价格
        /// </summary>
        public double FDCJYJG { get; set; }
        /// <summary>
        /// 竣工时间
        /// </summary>
        public System.DateTime JGSJ { get; set; }
        /// <summary>
        /// 产权来源
        /// </summary>
        public string CQLY { get; set; }
        /// <summary>
        /// 墙体归属东
        /// </summary>
        public string QTGSD { get; set; }
        /// <summary>
        /// 墙体归属南
        /// </summary>
        public string QTGSN { get; set; }
        /// <summary>
        /// 墙体归属西
        /// </summary>
        public string QTGSX { get; set; }
        /// <summary>
        /// 墙体归属北
        /// </summary>
        public string QTGSB { get; set; }
        /// <summary>
        /// 土地使用权人
        /// </summary>
        public string TDSYQR { get; set; }
        /// <summary>
        /// 房产分户图
        /// </summary>
        public string FCFHT { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int ZT { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime UpdateTime { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual NaturalBuilding NaturalBuilding { get; set; }
    }
}
