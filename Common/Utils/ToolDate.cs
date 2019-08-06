using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utils
{
    public class ToolDate
    {

        /// <summary>
        /// 获取斜杠年月日 yyyy/MM/dd 
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static string GetSlashDate(DateTime? date)
        {
            if (date == null) return string.Empty;
            return string.Format("{0:yyyy/MM/dd}", date);
        }

        /// <summary>
        /// 获取短横线年月日 yyyy-MM-dd 
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static string GetLineDate(DateTime date)
        {
            return string.Format("{0:yyyy-MM-dd}", date);
        }

        /// <summary>
        /// 获取中文日期 yyyy年MM月dd日 
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static string GetCNDate(DateTime date)
        {
            return string.Format("{0:yyyy年MM月dd日}", date);
        }


        /// <summary>
        /// 获取年
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static string GetYear(DateTime date)
        {
            return date.Year.ToString() + "年";
        }

        /// <summary>
        /// 获取月
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static string GetMonth(DateTime date)
        {
            return date.Day.ToString() + "月";
        }

        /// <summary>
        /// 获取日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static string GetDay(DateTime date)
        {
            return date.Day.ToString() + "日";
        }


        /// <summary>
        /// 计算起始到结束见期限
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static string CalcateTerm(DateTime startTime, DateTime endTime)
        {
            int year = endTime.Year - startTime.Year;//年
            int month = endTime.Month - startTime.Month;//月
            int day = endTime.Day - startTime.Day;//日
            if (day == 30 && month == 11)
            {
                year += 1;
            }
            return year.ToString();
        }
    }
}
