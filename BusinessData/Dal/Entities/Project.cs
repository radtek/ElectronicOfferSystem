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
    /// 项目
    /// </summary>
    public partial class Project
    {
        /// <summary>
        /// ID
        /// </summary>
        public System.Guid ID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 开发商名称
        /// </summary>
        public string DeveloperName { get; set; }
        /// <summary>
        /// 项目类型（1.楼盘项目 2.登记业务项目）
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 权籍类型（1.权籍调查 2.权籍补录）
        /// </summary>
        public string OwnershipType { get; set; }
        /// <summary>
        /// 测绘类型（1.预测绘 2.实测绘）
        /// </summary>
        public string MappingType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime UptateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}
