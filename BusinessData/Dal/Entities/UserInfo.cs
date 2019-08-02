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
    /// 用户信息
    /// </summary>
    public partial class UserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public System.Guid ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 最近登录时间
        /// </summary>
        public Nullable<System.DateTime> LastLoginTime { get; set; }
        /// <summary>
        /// 最近登出时间
        /// </summary>
        public Nullable<System.DateTime> LastLogoutTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<System.DateTime> CreateTime { get; set; }
        /// <summary>
        /// 是否管理员（0.否 1.是）
        /// </summary>
        public string IsAdmin { get; set; }
    }
}
