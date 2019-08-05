using BusinessData;
using BusinessData.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Rules
{
    public static class RuleHelper
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(object value)
        {
            return !string.IsNullOrWhiteSpace((value ?? "").ToString());
        }
        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumberic(object value)
        {
            if (value == null) return false;
            string text = value.ToString().Trim();
            // 数字验证
            string pattern = @"^(-?\d+)(\.\d+)?$";
            return Regex.IsMatch(text, pattern);
        }
        /// <summary>
        /// 是否整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInteger(object value)
        {
            if (value == null) return false;
            string text = value.ToString().Trim();
            // 整数验证
            string pattern = @"^\d+$";
            return Regex.IsMatch(text, pattern);
        }
        /// <summary>
        /// 数字和字母验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumberAndWord(object value)
        {
            if (value == null) return false;
            string text = value.ToString().Trim();
            string pattern = @"^[a-zA-Z0-9]+$";
            return Regex.IsMatch(text, pattern);
        }

        /// <summary>
        /// 是否要求长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool IsRequiredLength(object value, int length)
        {
            if (value == null) return false;
            return value.ToString().Length == length;
        }

        /// <summary>
        /// 证件号验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsID(object value)
        {
            if (value == null) return false;
            string text = value.ToString().Trim();
            // 身份证
            string sfz = @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}(\d|x|X)$";
            // 港澳通行证
            string gatxz = @"^[a-zA-Z0-9]{6,10}$";
            // 台胞证
            string tbz = @"^([0-9]{8}|[0-9]{10})$";
            // 护照
            string hz = @"^[a-zA-Z0-9]{5,17}$";
            // 户口簿

            // 军官证
            string jgz = @"^[0-9]{8}$";
            // 组织机构代码
            string zzjgdm = @"^[a-zA-Z0-9]{10,20}$";
            // 营业执照
            string yyzz = @"^[a-zA-Z0-9]{10,20}$";
            // 其他

            return Regex.IsMatch(text, sfz);
        }
    }
}
