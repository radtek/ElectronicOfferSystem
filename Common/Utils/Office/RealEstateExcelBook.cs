using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Office
{
    public class RealEstateExcelBook : ExcelBase
    {
        #region Fields


        #endregion

        #region Propertys
        /// <summary>
        /// 开始行
        /// </summary>
        public int StartRow { get; set; }

        /// <summary>
        /// 结束列
        /// </summary>
        public string EndColumn { get; set; }

        #endregion

        #region Delegate

        public delegate void PostProgressDelegate(int progress, string info = "");
        public event PostProgressDelegate PostProgressEvent;

        public delegate void PostErrorInformationDelegate(string message);
        public event PostErrorInformationDelegate PostErrorInformationEvent;

        public delegate void PostInformaionDelegate(string message);
        public event PostInformaionDelegate PostInformationEvent;

        #endregion

        #region Ctor

        /// <summary>
        /// 构造方法
        /// </summary>
        public RealEstateExcelBook()
        {
        }

        /// <summary>
        /// 进度报告
        /// </summary>
        /// <param name="progress"></param>
        private void toolProgress_OnPostProgress(int progress)
        {
            PostProgress(progress);
        }

        #endregion

        #region Override

        /// <summary>
        /// 写信息
        /// </summary>
        public override void Write()
        {
        }

        /// <summary>
        /// 读信息
        /// </summary>
        public override void Read()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// 获取列值字母
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetColumnLetter(int value)
        {
            return ExcelHelper.GetColumnLetter(value);
        }

        /// <summary>
        /// 获取字母代表列值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetColumnValue(string value)
        {
            return ExcelHelper.GetColumnValue(value);
        }


            /// <summary>
            /// 初始化域信息
            /// </summary>
            /// <param name="rowIndex">行索引</param>
            /// <param name="columnIndex">列索引</param>
            /// <param name="value">值</param>
            public void InitalizeRangeInformation(int rowIndex, int columnIndex, string value, int endRowIndex = 0)
        {
            try
            {
                string range = ExcelHelper.GetColumnLetter(columnIndex) + rowIndex.ToString();
                string endRange = string.Empty;
                if (endRowIndex == 0)
                {
                    endRange = range;
                }
                else
                {
                    endRange = ExcelHelper.GetColumnLetter(columnIndex) + endRowIndex.ToString();
                }
                InitalizeRangeValue(range, endRange, value);
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 初始化域信息
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="columnIndex">列索引</param>
        /// <param name="value">值</param>
        public void InitalizeCellInformation(int rowIndex, int columnIndex, object value, int endRowIndex = 0)
        {
            try
            {
                string range = ExcelHelper.GetColumnLetter(columnIndex) + rowIndex.ToString();
                string endRange = string.Empty;
                if (endRowIndex == 0)
                {
                    endRange = range;
                }
                else
                {
                    endRange = ExcelHelper.GetColumnLetter(columnIndex) + endRowIndex.ToString();
                }
                InitalizeRangeValue(range, endRange, value);
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 初始化域信息
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="columnIndex">列索引</param>
        /// <param name="value">值</param>
        public void InitalizeRangeInformation(int rowIndex, int columnIndex, string value, double height)
        {
            try
            {
                string range = ExcelHelper.GetColumnLetter(columnIndex) + rowIndex.ToString();
                InitalizeRangeValue(range, range, value);
                SetRangeHeight(range, range, height);
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Helper





        #endregion

        #region Events

        /// <summary>
        /// 报告进度
        /// </summary>
        /// <param name="progress"></param>
        protected void PostProgress(int progress)
        {
            if (PostProgressEvent != null)
            {
                PostProgressEvent(progress);
            }
        }

        /// <summary>
        /// 报告错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected bool PostErrorInformation(string message)
        {
            if (PostErrorInformationEvent != null)
            {
                PostErrorInformationEvent(message);
            }
            return false;
        }

        /// <summary>
        /// 报告异常信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected bool PostInformation(string message)
        {
            if (PostInformationEvent != null)
            {
                PostInformationEvent(message);
            }
            return false;
        }


        #endregion
    }
}
