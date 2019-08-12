using BusinessData;
using BusinessData.Dal;
using Common.Enums;
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
        public static bool IsID(EIdType type, object value)
        {
            if (value == null) return false;

            string text = value.ToString().Trim();
            bool res = false;
            switch (type)
            {
                case EIdType.SFZ:
                    // 身份证
                    string sfz = @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}(\d|x|X)$";
                    res = Regex.IsMatch(text, sfz);
                    break;
                case EIdType.GATSFZ:
                    // 港澳通行证
                    string gatxz = @"^[a-zA-Z0-9]{6,10}$";
                    // 台胞证
                    string tbz = @"^([0-9]{8}|[0-9]{10})$";
                    res = Regex.IsMatch(text, gatxz) || Regex.IsMatch(text, tbz);
                    break;
                case EIdType.HZ:
                    // 护照
                    string hz = @"^[a-zA-Z0-9]{5,17}$";
                    res = Regex.IsMatch(text, hz);
                    break;
                case EIdType.HKB:

                    res = true;
                    break;
                case EIdType.JGZ:
                    // 军官证
                    string jgz = @"^[0-9]{8}$";
                    res = Regex.IsMatch(text, jgz);
                    break;
                case EIdType.ZZJGDM:
                    // 组织机构代码
                    string zzjgdm = @"^[a-zA-Z0-9]{10,20}$";
                    res = Regex.IsMatch(text, zzjgdm);
                    break;
                case EIdType.YYZZ:
                    // 营业执照
                    string yyzz = @"^[a-zA-Z0-9]{10,20}$";
                    res = Regex.IsMatch(text, yyzz);
                    break;
                case EIdType.QT:

                    res = true;
                    break;
                default:
                    break;
            }
            return res;
            
            
        }

        /// <summary>
        /// AB之和是否为C
        /// </summary>
        /// <param name="valueA"></param>
        /// <param name="valueB"></param>
        /// <param name="valueC"></param>
        /// <returns></returns>
        public static bool IsAnd(object valueA, object valueB, object valueC)
        {
            if (valueA == null || valueB == null || valueC == null) return false;
            Decimal decimalA = Convert.ToDecimal(valueA);
            Decimal decimalB = Convert.ToDecimal(valueB);
            Decimal decimalC = Convert.ToDecimal(valueC);

            return (decimalA + decimalB == decimalC);
        }
    }
}
