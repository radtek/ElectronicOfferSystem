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
    
    /// <summary>
    /// 附件信息
    /// </summary>
    public partial class FileInfo
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
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 存储路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
