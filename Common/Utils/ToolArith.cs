using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Utils
{
    public class ToolArith
    {
        public static string GetPercentString(int percent)
        {
            return string.Format("{0}%", percent);
        }

        public static string GetPercentString(int n1, int n2)
        {
            return GetPercentString((int)GetPercent(n1, n2));
        }

        public static double GetPercent(int n1, int n2)
        {
            return (double)n1 / n2 * 100;
        }

        /// <summary>
        /// 匹配整数数字字符串
        /// </summary>
        /// <param name="number">字符串</param>
        /// <returns></returns>
        public static bool MatchEntiretyNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            string pattern = @"^[0-9]*$"; //所有整数
            if (Regex.IsMatch(number, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 匹配所有小数字符串
        /// </summary>
        /// <param name="number">字符串</param>
        /// <returns></returns>
        public static bool MatchNovelNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            string pattern = @"^[0-9]*\.[0-9]*$"; //所有的小数
            if (Regex.IsMatch(number, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            pattern = @"^[0-9]*\。[0-9]*$"; //所有的小数
            if (Regex.IsMatch(number, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 匹配所有数字字符串
        /// </summary>
        /// <param name="number">字符串</param>
        /// <returns></returns>
        public static bool MatchAllNumber(string number)
        {
            bool isRight = false;
            if (MatchEntiretyNumber(number))//匹配整数
            {
                isRight = true;
            }
            if (MatchNovelNumber(number))//匹配小数
            {
                isRight = true;
            }
            return isRight;
        }

        /// <summary>
        /// 只支持24位的整数转换小写中文
        /// 有些特殊情况不支持
        /// </summary>
        /// <param name="number">英文数字字符串</param>
        /// <returns></returns>
        public static string GetChineseLowNumber(string number)
        {
            int length = number.Length;
            if (length > 24)
            {
                return number;
            }
            string result = "";
            string[] numeric = new string[length];
            int flag = DecideNumeric(numeric, number);//判断数字
            for (int i = 0; i < length; i++)
            {
                int num = length - i;
                if (numeric[i] != "0")
                {
                    result += ChangeChineseLowNumeric(numeric[i], num);//转换数字
                }
                else
                {
                    if (i > flag)
                    {
                        result += AddChineseLowNumericUnit(i, length, numeric, num);//添加数字单位
                    }
                }
            }
            if (length == 2 && result.Length >= 2 && result.Substring(0, 1) == "一")
            {
                result = result.Substring(1);
            }
            return result;
        }

        /// <summary>
        /// 只支持24位的整数转换大写中文
        /// 有些特殊情况不支持
        /// </summary>
        /// <param name="number">英文数字字符串</param>
        /// <returns></returns>
        public static string GetChineseUpperNumber(string number)
        {
            int length = number.Length;
            if (length > 24)
            {
                return number;
            }
            string result = "";
            string[] numeric = new string[length];
            int flag = DecideNumeric(numeric, number);//判断数字
            for (int i = 0; i < length; i++)
            {
                int num = length - i;
                if (numeric[i] != "0")
                {
                    result += ChangeChineseUpperNumeric(numeric[i], num);//转换数字
                }
                else
                {
                    if (i > flag)
                    {
                        result += AddChineseUpperNumericUnit(i, length, numeric, num);//添加数字单位
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 判断数字
        /// </summary>
        /// <param name="numeric">数字数组</param>
        /// <param name="number">数字字符串</param>
        /// <returns></returns>
        private static int DecideNumeric(string[] numeric, string number)
        {
            int flag = -1;//标识
            bool canContinue = true;//是否继续
            int length = number.Length;
            for (int i = 0; i < length; i++)
            {
                numeric[i] = number.Substring(i, 1);
                if (!MatchEntiretyNumber(numeric[i]))//匹配是否是整数
                {
                    return flag;
                }
                if (numeric[0] == "0" && canContinue)//检验首位出现零的情况
                {
                    if (i != length - 1 && numeric[i] == "0" && numeric[i + 1] != "0")
                        flag = i;
                    else
                        canContinue = false;
                }
            }
            return flag;
        }

        /// <summary>
        /// 转换中文小写数字
        /// </summary>
        /// <param name="numeric">数字</param>
        /// <param name="currentNumeric">当前数字</param>
        public static string ChangeChineseLowNumeric(string numeric, int currentNumeric)
        {
            string result = string.Empty;//结果字符串
            string[] numberArray = { "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            result += numberArray[Convert.ToInt32(numeric) - 1];//将阿拉伯数字转换成中文大写数字
            //加上单位
            if (currentNumeric % 4 == 2)
                result += "十";
            if (currentNumeric % 4 == 3)
                result += "百";
            if (currentNumeric % 4 == 0)
                result += "千";
            if (currentNumeric % 4 == 1)
            {
                if (currentNumeric / 4 == 1)
                    result += "万";
                if (currentNumeric / 4 == 2)
                    result += "亿";
                if (currentNumeric / 4 == 3)
                    result += "万";
                if (currentNumeric / 4 == 4)
                    result += "亿";
                if (currentNumeric / 4 == 5)
                    result += "万";
            }
            return result;
        }

        /// <summary>
        /// 添加小写数字单位
        /// </summary>
        /// <param name="index">索引值</param>
        /// <param name="length">数字长度</param>
        /// <param name="numeric">数字数组</param>
        /// <param name="currentIndex">当前索引值</param>
        /// <returns></returns>
        private static string AddChineseLowNumericUnit(int index, int length, string[] numeric, int currentIndex)
        {
            string result = string.Empty;//结果字符串
            if ((index != length - 1 && numeric[index + 1] != "0" && (currentIndex - 1) % 4 != 0))
            {
                //此处判断“0”不是出现在末尾，且下一位也不是“0”；
                //如 10012332 在此处读法应该为壹仟零壹萬贰仟叁佰叁拾贰,两个零只要读一个零
                result += "零";
            }
            if (index != length - 1 && numeric[index + 1] != "0")
            {
                switch (currentIndex)
                {
                    //此处出现的情况是如 10002332，“0”出现在万位上就应该加上一个“萬”读成壹仟萬零贰仟叁佰叁拾贰
                    case 5: result += "万";
                        break;
                    case 9: result += "亿";
                        break;
                    case 13: result += "万";
                        break;
                }
            }
            if (index != length - 1 && numeric[index + 1] != "0" && (currentIndex - 1) % 4 == 0)
            {
                //此处出现的情况是如 10002332，“0”出现在万位上就应该加上一个“零”读成壹仟萬零贰仟叁佰叁拾贰
                result += "零";
            }
            return result;
        }

        /// <summary>
        /// 转换中文大写数字
        /// </summary>
        /// <param name="numeric">数字</param>
        /// <param name="currentNumeric">当前数字</param>
        public static string ChangeChineseUpperNumeric(string numeric, int currentNumeric)
        {
            string result = string.Empty;//结果字符串
            string[] numberArray = { "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            result += numberArray[Convert.ToInt32(numeric) - 1];//将阿拉伯数字转换成中文大写数字
            //加上单位
            if (currentNumeric % 4 == 2)
                result += "拾";
            if (currentNumeric % 4 == 3)
                result += "百";
            if (currentNumeric % 4 == 0)
                result += "仟";
            if (currentNumeric % 4 == 1)
            {
                if (currentNumeric / 4 == 1)
                    result += "萬";
                if (currentNumeric / 4 == 2)
                    result += "億";
                if (currentNumeric / 4 == 3)
                    result += "萬";
                if (currentNumeric / 4 == 4)
                    result += "億";
                if (currentNumeric / 4 == 5)
                    result += "萬";
            }
            return result;
        }

        /// <summary>
        /// 添加大写数字单位
        /// </summary>
        /// <param name="index">索引值</param>
        /// <param name="length">数字长度</param>
        /// <param name="numeric">数字数组</param>
        /// <param name="currentIndex">当前索引值</param>
        /// <returns></returns>
        private static string AddChineseUpperNumericUnit(int index, int length, string[] numeric, int currentIndex)
        {
            string result = string.Empty;//结果字符串
            if ((index != length - 1 && numeric[index + 1] != "0" && (currentIndex - 1) % 4 != 0))
            {
                //此处判断“0”不是出现在末尾，且下一位也不是“0”；
                //如 10012332 在此处读法应该为壹仟零壹萬贰仟叁佰叁拾贰,两个零只要读一个零
                result += "零";
            }
            if (index != length - 1 && numeric[index + 1] != "0")
            {
                switch (currentIndex)
                {
                    //此处出现的情况是如 10002332，“0”出现在万位上就应该加上一个“萬”读成壹仟萬零贰仟叁佰叁拾贰
                    case 5: result += "萬";
                        break;
                    case 9: result += "億";
                        break;
                    case 13: result += "萬";
                        break;
                }
            }
            if (index != length - 1 && numeric[index + 1] != "0" && (currentIndex - 1) % 4 == 0)
            {
                //此处出现的情况是如 10002332，“0”出现在万位上就应该加上一个“零”读成壹仟萬零贰仟叁佰叁拾贰
                result += "零";
            }
            return result;
        }

        /// <summary>
        /// 年份转换
        /// </summary>
        /// <param name="year">要求为4位年</param>
        /// <returns>中文小写年份</returns>
        public static string GetChineseLowYear(string year)
        {
            string reslut = string.Empty;
            if (year.Length != 4)//不是四位表示的年
            {
                return "";
            }
            if (!Regex.IsMatch(year, "^[0-9]*$"))
            {
                return "";
            }
            char[] chars = year.ToCharArray(0, year.Length);
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '0')
                {
                    reslut += "○";
                    continue;
                }
                reslut += ChangeChineseLowNumeric(chars[i].ToString(), -1);//数字转换
            }
            return reslut;
        }

        /// <summary>
        /// 年份转换
        /// </summary>
        /// <param name="year">要求为4位年</param>
        /// <returns>中文小写年份</returns>
        public static string GetChineseLowNimeric(string numeric)
        {
            string reslut = string.Empty;
            if (!Regex.IsMatch(numeric, "^[0-9]*$"))
            {
                return "";
            }
            char[] chars = numeric.ToCharArray(0, numeric.Length);
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '0')
                {
                    reslut += "○";
                    continue;
                }
                reslut += ChangeChineseLowNumeric(chars[i].ToString(), -1);//数字转换
            }
            return reslut;
        }

        /// <summary>
        /// 年份转换
        /// </summary>
        /// <param name="year">要求为4位年</param>
        /// <returns>中文小写年份</returns>
        public static string GetChineseUpperYear(string year)
        {
            string reslut = string.Empty;
            if (year.Length != 4)//不是四位表示的年
            {
                return "";
            }
            if (!Regex.IsMatch(year, "^[0-9]*$"))
            {
                return "";
            }
            char[] chars = year.ToCharArray(0, year.Length);
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '0')
                {
                    reslut += "零";
                    continue;
                }
                reslut += ChangeChineseUpperNumeric(chars[i].ToString(), -1);//数字转换
            }
            return reslut;
        }

        /// <summary>
        /// 截取小数位数
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="digits">位数</param>
        /// <returns></returns>
        public static double CutNumericFormat(double value, int digits)
        {
            double number = value + 0.00000001;
            double numeric = (int)(number * 100) / 100.0;
            switch (digits)
            {
                case 2:
                    numeric = (int)(number * 100) / 100.0;
                    break;
                case 3:
                    numeric = (int)(number * 1000) / 1000.0;
                    break;
                case 4:
                    numeric = (int)(number * 10000) / 10000.0;
                    break;
                case 5:
                    numeric = (int)(number * 100000) / 100000.0;
                    break;
                case 6:
                    numeric = (int)(number * 1000000) / 1000000.0;
                    break;
                case 7:
                    numeric = (int)(number * 10000000) / 10000000.0;
                    break;
                case 8:
                    numeric = (int)(number * 100000000) / 100000000.0;
                    break;
            }
            return numeric;
        }

        /// <summary>
        /// 截取小数位数
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="digits">位数</param>
        /// <returns></returns>
        public static double RoundNumericFormat(double value, int digits)
        {
            double numeric = Math.Round(value, digits, MidpointRounding.AwayFromZero);
            //double number = value + 0.00000001;
            //double numeric = Convert.ToUInt32(number * 100) / 100.0;
            //switch (digits)
            //{
            //    case 2:
            //        numeric = Convert.ToUInt32(number * 100) / 100.0;
            //        break;
            //    case 3:
            //        numeric = Convert.ToUInt32(number * 1000) / 1000.0;
            //        break;
            //    case 4:
            //        numeric = Convert.ToUInt32(number * 10000) / 10000.0;
            //        break;
            //    case 5:
            //        numeric = Convert.ToUInt32(number * 100000) / 100000.0;
            //        break;
            //    case 6:
            //        numeric = Convert.ToUInt32(number * 1000000) / 1000000.0;
            //        break;
            //    case 7:
            //        numeric = Convert.ToUInt32(number * 10000000) / 10000000.0;
            //        break;
            //    case 8:
            //        numeric = Convert.ToUInt32(number * 100000000) / 100000000.0;
            //        break;
            //}
            return numeric;
        }

        /// <summary>
        /// 设置小数格式
        /// </summary>
        /// <param name="value"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static double SetNumericFormat(double value, int digits, int mode)
        {
            double numeric = value;
            if (mode == 0)
            {
                numeric = CutNumericFormat(value, digits);
            }
            else
            {
                numeric = RoundNumericFormat(value, digits);
            }
            return numeric;
        }

        public static int GetScopeNumber(int num, int min, int max)
        {
            if (num < min)
                num = min;
            if (num > max)
                num = max;
            return num;
        }

        /// <summary>
        /// 设置数字字符串小数点位数
        /// </summary>
        /// <param name="numStr">字符串</param>
        /// <param name="digits">位数</param>
        /// <returns></returns>
        public static string SetNumbericFormat(string numStr, int digits)
        {
            if (!MatchAllNumber(numStr))
            {
                return numStr;
            }
            if (MatchEntiretyNumber(numStr))
            {
                numStr += ".";
                for (int i = 0; i < digits; i++)
                {
                    numStr += "0";
                }
                return numStr;
            }
            int index = numStr.IndexOf(".");
            if (index < 0)
            {
                return numStr;
            }
            string perFix = numStr.Substring(0, index + 1);
            string sufFix = numStr.Substring(index + 1);
            while (sufFix.Length < digits)
            {
                sufFix += "0";
            }
            return perFix + sufFix;
        }
    }
}
