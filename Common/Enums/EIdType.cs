using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    /// <summary>
    /// 证件类型
    /// </summary>
    public enum EIdType
    {
        /// <summary>
        /// 身份证
        /// </summary>
        SFZ = 1,
        /// <summary>
        /// 港澳台身份证
        /// </summary>
        GATSFZ = 2,
        /// <summary>
        /// 护照
        /// </summary>
        HZ = 3,
        /// <summary>
        /// 户口簿
        /// </summary>
        HKB = 4,
        /// <summary>
        /// 军官证（士兵证）
        /// </summary>
        JGZ = 5,
        /// <summary>
        /// 组织机构代码
        /// </summary>
        ZZJGDM = 6,
        /// <summary>
        /// 营业执照
        /// </summary>
        YYZZ = 7,
        /// <summary>
        /// 其它
        /// </summary>
        QT = 99
    }
}
