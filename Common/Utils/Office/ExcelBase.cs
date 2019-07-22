using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Cells;
using System.Drawing;

namespace Common.Utils.Office
{
    /// <summary>
    /// Excel数据基类
    /// </summary>
    [Serializable]
    public abstract class ExcelBase : IDisposable
    {
        #region Fields

        private object missing = System.Reflection.Missing.Value;//类型
        protected Workbook workbook;//工作薄
        protected Worksheet workSheet = null;//工作表
        private string fileName;//当前文件
        private Dictionary<string, int> columnIndex;//列索引值
        private string extionString = ".xls";//后缀名称
        private SaveFormat format = SaveFormat.Excel97To2003;

        #endregion

        #region Propertys

        /// <summary>
        /// 使用范围
        /// </summary>
        public Range UsedRange
        {
            get
            {
                return null;
            }
        }

        #endregion

        #region ConstName

        public const string TEMPLATEFILE_NOT_OPEN = "未初始化模板";

        #endregion

        #region Abstract

        /// <summary>
        /// 写数据
        /// </summary>
        public abstract void Write();

        /// <summary>
        /// 读数据
        /// </summary>
        public abstract void Read();

        #endregion

        #region Methods-BasicOperation

        /// <summary>
        /// 打印预览
        /// </summary>
        public void PrintView()
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            if (workSheet != null && WordOperator.SheetColumnAutoFit)
            {
                workSheet.AutoFitRow(4, workSheet.Cells.MaxRow, 0, workSheet.Cells.MaxColumn);
            }
            fileName = WordOperator.InitalizeValideFileName(fileName);
            string filePath = WordOperator.InitalzieDefatultDirectory();
            filePath += System.IO.Path.GetFileName(fileName) + extionString;
            WordOperator.InitalzieDirectory(filePath);
            workbook.Save(filePath, format);
            System.Diagnostics.Process.Start(fileName);
        }

        /// <summary>
        /// 打印
        /// <param name="isPreview">是否预览打印</param>
        /// <param name="sheet">当前需要打印的表</param>
        /// </summary>
        public void Print(bool isPreview, Worksheet sheet)
        {
            if (sheet != null && WordOperator.SheetColumnAutoFit)
            {
                sheet.AutoFitRow(4, workSheet.Cells.MaxRow, 0, workSheet.Cells.MaxColumn);
            }
            if (isPreview)
            {
                Show();//打印预览
            }
            else
            {
                Print();
            }
        }

        /// <summary>
        /// 打印
        /// <param name="isPreview">是否预览打印</param>
        /// <param name="sheet">当前需要打印的表</param>
        /// </summary>
        public void Print()
        {
            if (workSheet == null)
            {
                return;
            }
            if (WordOperator.SheetColumnAutoFit)
            {
                workSheet.AutoFitRow(4, workSheet.Cells.MaxRow, 0, workSheet.Cells.MaxColumn);
            }
            Aspose.Cells.Rendering.ImageOrPrintOptions options = new Aspose.Cells.Rendering.ImageOrPrintOptions();
            options.PrintingPage = PrintingPageType.Default;
            Aspose.Cells.Rendering.SheetRender sr = new Aspose.Cells.Rendering.SheetRender(workSheet, options);
            if (sr == null)
            {
                return;
            }
            List<string> printers = PrinterOperator.GetLocalPrinters();
            if (printers == null || printers.Count == 0)
            {
                return;
            }
            sr.ToPrinter(printers[0]);
        }

        /// <summary>
        /// 打印
        /// <param name="isPreview">是否预览打印</param>
        /// <param name="sheet">当前需要打印的表</param>
        /// </summary>
        public void Print(string printerName)
        {
            if (workSheet == null)
            {
                return;
            }
            if (WordOperator.SheetColumnAutoFit)
            {
                workSheet.AutoFitRow(4, workSheet.Cells.MaxRow, 0, workSheet.Cells.MaxColumn);
            }
            Aspose.Cells.Rendering.ImageOrPrintOptions options = new Aspose.Cells.Rendering.ImageOrPrintOptions();
            options.PrintingPage = PrintingPageType.Default;
            Aspose.Cells.Rendering.SheetRender sr = new Aspose.Cells.Rendering.SheetRender(workSheet, options);
            if (sr == null)
            {
                return;
            }
            sr.ToPrinter(printerName);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public bool Open(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && !File.Exists(fileName))
            {
                workbook = new Workbook();
            }
            try
            {
                InitalizeColumn();
                this.fileName = fileName;
                if (workbook == null)
                {
                    workbook = string.IsNullOrEmpty(fileName) ? new Workbook() : new Workbook(fileName);
                }
                if (workbook != null)
                {
                    if (workbook.Worksheets.Count == 1 && string.IsNullOrEmpty(fileName))
                    {
                        workbook.Worksheets[0].Name = "鱼鳞图";
                    }
                    workSheet = workbook.Worksheets[0];
                }
                return true;
            }
            catch
            {
                DisposeExcel(workbook);
            }
            return false;
        }

        /// <summary>
        /// 显示工作簿
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ShowWorkSheet(int index)
        {
            if (workbook == null)
            {
                return false;
            }
            if (workbook.Worksheets == null || workbook.Worksheets.Count <= index)
            {
                return false;
            }
            workSheet = workbook.Worksheets[index];
            return true;
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void Show()
        {
            if (string.IsNullOrEmpty(fileName) || workbook == null)
            {
                return;
            }
            if (workSheet != null && WordOperator.SheetColumnAutoFit)
            {
                workSheet.AutoFitRow(4, workSheet.Cells.MaxRow, 0, workSheet.Cells.MaxColumn);
            }
            fileName = WordOperator.InitalizeValideFileName(fileName);
            string filePath = WordOperator.InitalzieDefatultDirectory();
            filePath += System.IO.Path.GetFileNameWithoutExtension(fileName) + extionString;
            int i = 1;
            bool newfile = false;
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                    newfile = true;
                }
            }
            if (newfile)
            {
                while (File.Exists(filePath))
                {
                    filePath += System.IO.Path.GetFileNameWithoutExtension(fileName) + "(" + i + ")" + extionString;
                    i++;
                }
            }
            WordOperator.InitalzieDirectory(filePath);
            workbook.Save(filePath, format);
            System.Diagnostics.Process.Start(filePath);
            System.Threading.Thread.Sleep(500);
            this.Dispose();
        }

        /// <summary>
        /// 字符串
        /// </summary>
        public string GetString(object obj)
        {
            return obj == null ? string.Empty : obj.ToString().Trim().Replace(" ", "");
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DateTime? GetDateTime(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            DateTime date = DateTime.Now;
            DateTime.TryParse(obj.ToString(), out date);
            if (date.Year == 1)
            {
                return null;
            }
            return date;
        }

        /// <summary>
        /// int32
        /// </summary>
        public int GetInt32(object obj)
        {
            int result = 0;
            if (obj != null)
            {
                string str = obj.ToString().Trim();
                Int32.TryParse(str, out result);
            }
            return result;
        }

        /// <summary>
        /// double
        /// </summary>
        public double GetDouble(object obj)
        {
            double result = 0.0;
            if (obj != null)
            {
                double.TryParse(obj.ToString().Trim(), out result);
            }
            return result;
        }

        /// <summary>
        /// 获取单元格区域
        /// </summary>
        /// <param name="start">起始单元格</param>
        /// <param name="end">结束单元格</param>
        /// <returns></returns>
        public Range GetExcelRange(string start, string end)
        {
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeLetter(start);
            Cell cell = workSheet.Cells[startIndex, endIndex];
            return cell.GetMergedRange();
        }

        /// <summary>
        /// 获取单元格区域的值
        /// </summary>
        /// <param name="start">起始单元格</param>
        /// <param name="end">结束单元格</param>
        /// <returns></returns>
        public object GetExcelRangeValue(string start, string end)
        {
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeLetter(start);
            Cell cell = workSheet.Cells[startIndex, endIndex];
            return cell.Value;
        }

        /// <summary>
        /// 获取所有使用域值
        /// </summary>
        /// <returns></returns>
        public object[,] GetAllRangeValue()
        {
            if (workSheet == null)
            {
                return null;
            }
            int rowCount = workSheet.Cells.MaxRow + 1;
            int clumnCount = workSheet.Cells.MaxColumn + 1;
            object[,] allItem = workSheet.Cells.ExportArray(0, 0, rowCount, clumnCount);
            return allItem;
        }

        /// <summary>
        /// 获取使用域行数
        /// </summary>
        /// <returns></returns>
        public int GetRangeRowCount()
        {
            if (workSheet == null)
            {
                return 0;
            }
            return workSheet.Cells.MaxRow + 1;
        }

        /// <summary>
        /// 获取使用域列数
        /// </summary>
        /// <returns></returns>
        public int GetRangeColumnCount()
        {
            if (workSheet == null)
            {
                return 0;
            }
            return workSheet.Cells.MaxColumn + 1;
        }

        /// <summary>
        /// 保存所有更改。
        /// </summary>
        /// <history>
        ///     2011年1月23日 1:40:40   Roc 创建
        /// </history>
        public void Save()
        {
            if (string.IsNullOrEmpty(fileName) || workbook == null)
            {
                return;
            }
            if (workSheet != null && WordOperator.SheetColumnAutoFit)
            {
                workSheet.AutoFitRow(4, workSheet.Cells.MaxRow, 0, workSheet.Cells.MaxColumn);
            }
            fileName = WordOperator.InitalizeValideFileName(fileName);
            string extension = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
            {
                fileName += extionString;
            }
            else
            {
                fileName = extension == extionString ? fileName : fileName.Replace(extension, extionString);
            }
            WordOperator.InitalzieDirectory(fileName);
            workbook.Save(fileName, format);
        }

        /// <summary>
        /// 保存所有更改。
        /// </summary>
        /// <history>
        /// 2011年1月23日 1:40:40   Roc 创建
        /// </history>
        public void SaveData()
        {
            if (string.IsNullOrEmpty(fileName) || workbook == null)
            {
                return;
            }
            SaveFormat saveFormat = SaveFormat.Excel97To2003;
            string extension = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension) || extension.ToLower() != ".xls")
            {
                saveFormat = SaveFormat.Xlsx;
            }
            workbook.Save(fileName, saveFormat);
        }

        /// <summary>
        /// 将当前 Excel 文档另存为 <paramref name="fileName"/> 指定的文件。
        /// </summary>
        /// <param name="fileName">指定的文件名。</param>
        /// <history>
        ///     2011年1月23日 1:42:25   Roc 创建
        /// </history>
        public void SaveAs(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || workbook == null)
            {
                return;
            }
            if (workSheet != null && WordOperator.SheetColumnAutoFit)
            {
                workSheet.AutoFitRow(4, workSheet.Cells.MaxRow, 0, workSheet.Cells.MaxColumn);
            }
            fileName = WordOperator.InitalizeValideFileName(fileName);
            string extension = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
            {
                fileName += extionString;
            }
            else
            {
                fileName = extension == extionString ? fileName : fileName.Replace(extension, extionString);
            }
            WordOperator.InitalzieDirectory(fileName);
            workbook.Save(fileName, format);
        }

        #endregion

        #region Methods-SetValue

        /// <summary>
        /// 初始化范围值
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        public void InitalizeCellValue(int start, int end, object value)
        {
            if (workbook == null || workSheet == null)
            {
                return;
            }
            Cell cell = workSheet.Cells[start, end];
            cell = cell == null ? InitalizeSheetCell(start, end) : cell;
            if (cell == null)
            {
                return;
            }
            cell.PutValue(value);
        }

        /// <summary>
        /// 初始化范围值
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        public void InitalizeRangeValue(string start, string end, object value)
        {
            int startRow = InitalizeIndex(start);
            int startColumn = InitalizeLetter(start);
            int endRow = InitalizeIndex(end);
            int endColumn = InitalizeLetter(end);
            if (startRow == endRow && startColumn == endColumn)
            {
                InitalizeCellValue(startRow, startColumn, value);
                return;
            }
            int rowCount = Math.Abs(endRow - startRow);
            int columnCount = Math.Abs(endColumn - startColumn);
            Range range = SetRangeMerge(start, end, true);
            if (range == null)
            {
                return;
            }
            range.Merge();
            Cell cell = range[0, 0];
            if (cell == null)
            {
                return;
            }
            cell.Value = value;
        }

        /// <summary>
        /// 设置区域值
        /// </summary>
        /// <param name="range">区域</param>
        /// <param name="value">值</param>
        public void SetRange(int start, int end, object value)
        {
            InitalizeCellValue(start, end, value);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        public void SetRange(string start, string end, string value)
        {
            InitalizeRangeValue(start, end, value);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        public void SetRange(string start, string end, string value, double width)
        {
            InitalizeRangeValue(start, end, value);
            int startIndex = InitalizeLetter(start);
            int endIndex = InitalizeLetter(end);
            SetRangeWidth(startIndex, endIndex, width);
        }

        /// <summary>
        /// 供导出集体建设用地使用权确权颁证公示表Excel使用
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        public void SetRangAndFont(string start, string end, double rowHeight, double size, bool bold, string value)
        {
            InitalizeRangeValue(start, end, value);
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeIndex(end);
            SetRangeHeight(startIndex, endIndex, rowHeight);
            SetRangeFont(start, end, (int)size, 266, bold, false);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        public void SetRangeWidthAndHeight(string start, string end, string value, double width, double height)
        {
            SetRange(start, end, value);
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeIndex(end);
            SetRangeHeight(startIndex, endIndex, height);
            int rowStartIndex = InitalizeLetter(start);
            int rowEndIndex = InitalizeLetter(end);
            SetRangeWidth(rowStartIndex, rowEndIndex, width);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        public void SetRangeWidthAndHeight(string start, string end, string value, double width, double height, bool isMerge)
        {
            SetRange(start, end, value);
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeIndex(end);
            SetRangeHeight(startIndex, endIndex, height);
            int rowStartIndex = InitalizeLetter(start);
            int rowEndIndex = InitalizeLetter(end);
            SetRangeWidth(rowStartIndex, rowEndIndex, width);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        public void SetRangeWidth(string start, string end, string value, double width, bool isMerge)
        {
            SetRange(start, end, value);
            int rowStartIndex = InitalizeLetter(start);
            int rowEndIndex = InitalizeLetter(end);
            SetRangeWidth(rowStartIndex, rowEndIndex, width);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        public void SetRangeAndHeight(string start, string end, string value, double height)
        {
            SetRange(start, end, value);
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeIndex(end);
            SetRangeHeight(startIndex, endIndex, height);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        public void SetRangeHeight(string start, string end, double height)
        {
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeIndex(end);
            SetRangeHeight(startIndex, endIndex, height);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        public void SetRangeWidth(string start, string end, double width)
        {
            int rowStartIndex = InitalizeLetter(start);
            int rowEndIndex = InitalizeLetter(end);
            SetRangeWidth(rowStartIndex, rowEndIndex, width);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        public void SetRangeAndHeight(string start, string end, string value, double height, bool isMerge)
        {
            SetRange(start, end, value);
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeIndex(end);
            SetRangeHeight(startIndex, endIndex, height);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        /// <param name="isMerge">是否合并</param>
        public void SetRange(string start, string end, string value, bool isMerge)
        {
            SetRange(start, end, value);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        /// <param name="isMerge">是否合并</param>
        public void SetRange(string start, string end, string value, bool isMerge, bool bold)
        {
            SetRange(start, end, value);
            SetRangeFont(start, end, 0, 266, bold, false);
        }

        /// <summary>
        /// 设置Rang值
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="value">值</param>
        /// <param name="isMerge">是否合并</param>
        public void SetRange(string start, string end, string value, double height, bool isMerge)
        {
            SetRangeMerge(start, end, isMerge);
            SetRange(start, end, value);
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeIndex(end);
            SetRangeHeight(startIndex, endIndex, height);
        }

        /// <summary>
        /// 设置面域
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="rowHeight"></param>
        /// <param name="size"></param>
        /// <param name="bold"></param>
        /// <param name="value"></param>
        public void SetRange(string start, string end, double rowHeight, double size, bool bold, string value)
        {
            InitalizeRangeValue(start, end, value);
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeIndex(end);
            SetRangeHeight(startIndex, endIndex, rowHeight);
            SetRangeFont(start, end, (int)size, 266, bold, false);
        }

        #endregion

        #region Methods-Style

        /// <summary>
        /// 设置字体
        /// </summary>
        /// <param name="range">区域</param>
        /// <param name="size">字体大小</param>
        /// <param name="colorIndex">颜色值</param>
        /// <param name="isBold">是否是粗体</param>
        public void SetRangeFont(string start, string end, double size, int colorIndex, bool isBold, bool isUnderLine)
        {
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeLetter(start);
            Cell cell = workSheet.Cells[startIndex, endIndex];
            if (cell == null)
            {
                return;
            }
            Style cellStyle = cell.GetStyle();
            cellStyle.Font.Name = "宋体";
            if (size != 0)
            {
                cellStyle.Font.Size = (int)size;//字体大小
            }
            if (Math.Abs(colorIndex) < 256)
            {
                cellStyle.Font.Color = System.Drawing.ColorTranslator.FromOle(colorIndex);
            }
            cellStyle.Font.IsBold = isBold;//是否粗体
            if (isUnderLine)
            {
                cellStyle.Font.Underline = FontUnderlineType.Single;//下划线
            }
            Range range = cell.GetMergedRange();
            if (range != null)
            {
                StyleFlag flag = new StyleFlag();
                flag.Font = true;
                flag.FontBold = true;
                flag.FontColor = true;
                flag.FontSize = true;
                flag.FontUnderline = true;
                range.ApplyStyle(cellStyle, flag);
            }
            else
            {
                cell.SetStyle(cellStyle);
            }
        }

        /// <summary>
        /// 设置格式
        /// </summary>
        /// <param name="range">区域</param>
        /// <param name="format">格式</param>
        public void SetRangeNumberFormat(string start, string end, string format)
        {
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeLetter(start);
            Cell cell = workSheet.Cells[start];
            if (cell == null)
            {
                return;
            }
            cell.Formula = format;
        }

        /// <summary>
        /// 设置对齐方式
        /// </summary>
        /// <param name="range">区域</param>
        /// <param name="hAlign">水平</param>
        /// <param name="vAlign">垂直</param>
        public void SetRangeAlignment(string start, string end, int hAlign, int vAlign)
        {
            int startColumnIndex = InitalizeLetter(start);
            int startRowIndex = InitalizeIndex(start);
            int endColumnIndex = InitalizeLetter(end);
            int endRowIndex = InitalizeIndex(end);
            if (startColumnIndex == endColumnIndex && startRowIndex == endRowIndex)
            {
                SetCellAlignment(startRowIndex, startColumnIndex, hAlign, vAlign);
                return;
            }
            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                for (int j = startColumnIndex; j <= endColumnIndex; j++)
                {
                    SetCellAlignment(i, j, hAlign, vAlign);
                }
            }
        }

        /// <summary>
        /// 设置单元格对齐方式
        /// </summary>
        public void SetCellAlignment(int rowIndex, int columnIndex, int hAlign, int vAlign)
        {
            Cell cell = workSheet.Cells[rowIndex, columnIndex];
            if (cell == null)
            {
                return;
            }
            Style style = cell.GetStyle();
            SetTableAlignStyle(style, hAlign, vAlign);
            cell.SetStyle(style);
        }

        /// <summary>
        /// 设置表格对齐方式
        /// </summary>
        /// <param name="hAlign"></param>
        /// <param name="vAlign"></param>
        public void SetTableAlignStyle(Style cellStyle, int hAlign, int vAlign)
        {
            cellStyle.IsTextWrapped = true;
            switch (hAlign)
            {
                case 1:
                    cellStyle.HorizontalAlignment = TextAlignmentType.Left;
                    break;
                case 2:
                    cellStyle.HorizontalAlignment = TextAlignmentType.Right;
                    break;
                case 3:
                    cellStyle.HorizontalAlignment = TextAlignmentType.Center;
                    break;
                case 4:
                    cellStyle.HorizontalAlignment = TextAlignmentType.Fill;
                    break;
                case 5:
                    cellStyle.HorizontalAlignment = TextAlignmentType.General;
                    break;
                case 6:
                    cellStyle.HorizontalAlignment = TextAlignmentType.Justify;
                    break;
                case 7:
                    cellStyle.HorizontalAlignment = TextAlignmentType.Distributed;
                    break;
                case 8:
                    cellStyle.HorizontalAlignment = TextAlignmentType.CenterAcross;
                    break;
                default:
                    break;
            }
            switch (vAlign)
            {
                case 1:
                    cellStyle.VerticalAlignment = TextAlignmentType.Bottom;
                    break;
                case 2:
                    cellStyle.VerticalAlignment = TextAlignmentType.Center;
                    break;
                case 3:
                    cellStyle.VerticalAlignment = TextAlignmentType.Distributed;
                    break;
                case 4:
                    cellStyle.VerticalAlignment = TextAlignmentType.Justify;
                    break;
                case 5:
                    cellStyle.VerticalAlignment = TextAlignmentType.Top;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 设置是否合并
        /// </summary>
        /// <param name="range">区域</param>
        /// <param name="isMerge">是否合并</param>
        public Range SetRangeMerge(string start, string end, bool isMerge)
        {
            int startRow = InitalizeIndex(start);
            int startColumn = InitalizeLetter(start);
            int endRow = InitalizeIndex(end);
            int endColumn = InitalizeLetter(end);
            if (startRow == endRow && startColumn == endColumn)
            {
                return null;
            }
            int rowCount = Math.Abs(endRow - startRow);
            int columnCount = Math.Abs(endColumn - startColumn);
            rowCount += 1;
            columnCount += 1;
            Range range = workSheet.Cells.CreateRange(startRow, startColumn, rowCount, columnCount);
            if (range != null)
            {
                range.UnMerge();
                range.Merge();
            }
            return range;
        }

        /// <summary>
        /// 设置是否合并
        /// </summary>
        /// <param name="range">区域</param>
        /// <param name="isMerge">是否合并</param>
        public Range SetRangeMerge(int start, int end, int rowCount = 1, int columnCount = 1)
        {
            Range range = workSheet.Cells.CreateRange(start, end, rowCount == 0 ? 1 : rowCount, columnCount == 0 ? 1 : columnCount);
            if (range != null)
            {
                range.UnMerge();
                range.Merge();
            }
            return range;
        }

        /// <summary>
        /// 初始化单元格值
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        public Cell InitalizeSheetCell(int start, int end, int rowCount = 1, int columnCount = 1)
        {
            Range range = SetRangeMerge(start, end, rowCount, columnCount);
            if (range == null)
            {
                return workSheet.Cells[start, end];
            }
            Cell cell = range[0, 0];
            return cell;
        }

        /// <summary>
        /// 设置单元格线条样式
        /// </summary>
        /// <param name="range">区域</param>
        /// <param name="isMerge">是否合并</param>
        public void SetRangeLineStyle(string start, string end, int value, bool alignment = true)
        {
            int startColumnIndex = InitalizeLetter(start);
            int startRowIndex = InitalizeIndex(start);
            int endColumnIndex = InitalizeLetter(end);
            int endRowIndex = InitalizeIndex(end);
            if (startColumnIndex == endColumnIndex && startRowIndex == endRowIndex)
            {
                SetCellLineStyle(startRowIndex, startColumnIndex, value, alignment);
                return;
            }
            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                for (int j = startColumnIndex; j <= endColumnIndex; j++)
                {
                    SetCellLineStyle(i, j, value, alignment);
                }
            }
        }

        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        public void SetCellLineStyle(int rowIndex, int columnIndex, int value, bool alignment = true)
        {
            Cell cell = workSheet.Cells[rowIndex, columnIndex];
            if (cell == null)
            {
                return;
            }
            Style style = cell.GetStyle();
            SetTableLineStyle(style, value, alignment);
            cell.SetStyle(style, true);
        }

        /// <summary>
        /// 设置表格格式
        /// </summary>
        /// <returns></returns>
        private void SetTableLineStyle(Style cellStyle, int value, bool alignment = true)
        {
            if (workbook == null)
            {
                return;
            }
            cellStyle.IsTextWrapped = true;
            CellBorderType borderType = CellBorderType.Thin;
            switch (value)
            {
                case 1:
                    borderType = CellBorderType.Thick;
                    break;
                case 2:
                    borderType = CellBorderType.DashDot;
                    break;
                case 3:
                    borderType = CellBorderType.DashDotDot;
                    break;
                case 4:
                    borderType = CellBorderType.Dashed;
                    break;
                case 5:
                    borderType = CellBorderType.Dotted;
                    break;
                case 6:
                    borderType = CellBorderType.Double;
                    break;
                case 7:
                    borderType = CellBorderType.Hair;
                    break;
                case 8:
                    borderType = CellBorderType.Medium;
                    break;
                case 9:
                    borderType = CellBorderType.MediumDashDot;
                    break;
                case 10:
                    borderType = CellBorderType.MediumDashDotDot;
                    break;
                case 11:
                    borderType = CellBorderType.MediumDashed;
                    break;
                case 12:
                    borderType = CellBorderType.None;
                    break;
                case 13:
                    borderType = CellBorderType.SlantedDashDot;
                    break;
                default:
                    break;
            }
            cellStyle.Borders[BorderType.LeftBorder].LineStyle = borderType;
            cellStyle.Borders[BorderType.RightBorder].LineStyle = borderType;
            cellStyle.Borders[BorderType.TopBorder].LineStyle = borderType;
            cellStyle.Borders[BorderType.BottomBorder].LineStyle = borderType;
            if (alignment)
            {
                cellStyle.VerticalAlignment = TextAlignmentType.Center;
                cellStyle.HorizontalAlignment = TextAlignmentType.Center;
            }
        }

        /// <summary>
        /// 设置Sheet且改变使用域
        /// </summary>
        /// <param name="index"></param>
        public void SetSheetIndex(int index)
        {
            workSheet = workbook.Worksheets[index];
        }

        /// <summary>
        /// 设置Sheet且改变使用域
        /// </summary>
        /// <param name="index"></param>
        public void SetSheetName(string sheetName)
        {
            workSheet = workbook.Worksheets[sheetName];
        }

        /// <summary>
        /// 获取工作表总数
        /// </summary>
        /// <returns></returns>
        public int GetWorkSheetCount()
        {
            if (workbook == null)
            {
                return 0;
            }
            return workbook.Worksheets.Count;
        }

        /// <summary>
        /// 获取工作表总数
        /// </summary>
        /// <returns></returns>
        public string GetWorkSheetName(int index)
        {
            if (workbook == null)
            {
                return "";
            }
            if (index >= workbook.Worksheets.Count)
            {
                return "";
            }
            return workbook.Worksheets[index].Name;
        }

        /// <summary>
        /// 设置工作表中行颜色
        /// </summary>
        /// <returns></returns>
        public void SetWorkSheetRowColor(string start, string end, Color color)
        {
            int startRow = InitalizeIndex(start);
            int startColumn = InitalizeLetter(start);
            int endRow = InitalizeIndex(end);
            int endColumn = InitalizeLetter(end);
            Cell cell = workSheet.Cells[startRow, startColumn];
            Style style = cell.GetStyle();
            style.BackgroundColor = color;
            style.ForegroundColor = color;
            style.Pattern = BackgroundType.Solid;
            if (startRow == endRow && startColumn == endColumn)
            {
                cell.SetStyle(style);
                return;
            }
            int rowCount = Math.Abs(endRow - startRow);
            int columnCount = Math.Abs(endColumn - startColumn);
            rowCount += 1;
            columnCount += 1;
            Range range = workSheet.Cells.CreateRange(startRow, startColumn, rowCount, columnCount);
            if (range == null)
            {
                return;
            }
            StyleFlag flag = new StyleFlag();
            flag.All = true;
            range.ApplyStyle(style, flag);
        }

        /// <summary>
        /// 隐藏列
        /// </summary>
        /// <param name="index"></param>
        public void SetColumnVisible(int index, bool visible = true)
        {
            if (workSheet == null)
            {
                return;
            }
            if (workSheet.Cells.Columns.Count <= index)
            {
                return;
            }
            Column column = workSheet.Cells.Columns[index];
            column.IsHidden = visible;
        }

        /// <summary>
        /// 隐藏行
        /// </summary>
        /// <param name="index"></param>
        public void SetRowVisible(int index, bool visible = true)
        {
            if (workSheet == null)
            {
                return;
            }
            if (workSheet.Cells.Rows.Count <= index)
            {
                return;
            }
            Row row = workSheet.Cells.Rows[index];
            row.IsHidden = visible;
        }

        /// <summary>
        /// 获取单元格区域的值
        /// </summary>
        /// <param name="start">起始单元格</param>
        /// <param name="end">结束单元格</param>
        /// <returns></returns>
        public object GetRangeToValue(string start, string end)
        {
            int startIndex = InitalizeIndex(start);
            int endIndex = InitalizeLetter(start);
            Cell cell = workSheet.Cells[startIndex, endIndex];
            if (cell == null)
            {
                return null;
            }
            return cell.Value;
        }

        /// <summary>
        /// 添加单元格
        /// </summary>
        /// <param name="row"></param>
        /// <param name="cloumn"></param>
        public void InsertRowCell(int row)
        {
            if (workSheet == null)
            {
                return;
            }
            try
            {
                workSheet.Cells.InsertRow(Math.Abs(row));
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 添加单元格
        /// </summary>
        /// <param name="row"></param>
        /// <param name="cloumn"></param>
        public void InsertColumnCell(int column)
        {
            if (workSheet == null)
            {
                return;
            }
            try
            {
                workSheet.Cells.InsertColumn(Math.Abs(column));
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 设置行高
        /// </summary>
        /// <param name="rowIndex"></param>
        public void SetRowHeight(int rowIndex, double height)
        {
            if (workSheet == null)
            {
                return;
            }
            Row row = workSheet.Cells.Rows[rowIndex];
            if (row == null)
            {
                return;
            }
            row.Height = Math.Abs(height);
        }

        /// <summary>
        /// 设置范围行高
        /// </summary>
        /// <param name="rowIndex"></param>
        public void SetRangeHeight(int start, int end, double height)
        {
            if (workSheet == null)
            {
                return;
            }
            if (start == end)
            {
                SetRowHeight(start, height);
                return;
            }
            int count = Math.Abs(end - start) + 1;
            for (int index = 0; index <= end; index++)
            {
                SetRowHeight(start + index, height);
            }
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="rowIndex"></param>
        public void SetColumnWidth(int colIndex, double width)
        {
            if (workSheet == null)
            {
                return;
            }
            Column column = workSheet.Cells.Columns[colIndex];
            if (column != null)
            {
                column.Width = Math.Abs(width);
            }
        }

        /// <summary>
        /// 设置范围行高
        /// </summary>
        /// <param name="rowIndex"></param>
        public void SetRangeWidth(int start, int end, double width)
        {
            if (workSheet == null)
            {
                return;
            }
            if (start == end)
            {
                SetColumnWidth(start, width);
                return;
            }
            int count = Math.Abs(end - start) + 1;
            for (int index = 0; index <= end; index++)
            {
                SetColumnWidth(start + index, width);
            }
        }

        /// <summary>
        /// 初始化字母所代表的索引值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int InitalizeLetter(string value)
        {
            string number = string.Empty;
            foreach (char item in value)
            {
                if (item >= 48 && item <= 58)
                {
                    number += item;
                }
            }
            if (string.IsNullOrEmpty(number))
            {
                return 0;
            }
            int index = -1;
            string letter = value.Replace(number, "");
            if (columnIndex.ContainsKey(letter))
            {
                columnIndex.TryGetValue(letter, out index);
            }
            return index;
        }

        /// <summary>
        /// 获取字符串中所有数字字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public int InitalizeIndex(string value)
        {
            string number = string.Empty;
            foreach (char item in value)
            {
                if (item >= 48 && item <= 58)
                {
                    number += item;
                }
            }
            int index = -1;
            int.TryParse(number, out index);
            return index - 1;
        }

        /// <summary>
        /// 清空所有数据
        /// </summary>
        public void CleasAll()
        {
            if (workSheet != null)
            {
                workSheet.Cells.Clear();
            }
        }

        #endregion

        #region Methods-Reader

        /// <summary>
        /// 设置面域样式
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        public void SetRangeStyle(string start, string end, double size, int color)
        {
            SetRangeFont(start, end, size, color, false, false);
        }

        /// <summary>
        /// 设置对齐方式
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void SetAlignment(string start, string end)
        {
            SetRangeAlignment(start, end, 3, 2);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void SaveExcel()
        {
            Save();
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="path"></param>
        public void SaveAsExcel(string path)
        {
            SaveAs(path);
        }

        /// <summary>
        /// 创建Excel
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CreateExcel(string path)
        {
            return Open(path);
        }

        /// <summary>
        /// 新建WoekSheet
        /// </summary>
        public void NewWorkSheet()
        {
            if (workbook == null)
            {
                return;
            }
            workbook.Worksheets.Add();
        }

        /// <summary>
        /// 获取面域单元行数
        /// </summary>
        /// <returns></returns>
        public int GetRangeCellsRows()
        {
            return GetRangeRowCount();
        }

        /// <summary>
        /// 获取当前使用范围
        /// </summary>
        /// <returns></returns>
        public object GetUsedRange()
        {
            if (workSheet == null)
            {
                return null;
            }
            return null;
        }

        #endregion

        #region Methods-Helper

        /// <summary>
        /// 初始化列数
        /// </summary>
        private void InitalizeColumn()
        {
            columnIndex = new Dictionary<string, int>();
            columnIndex.Add("A", 0);
            columnIndex.Add("B", 1);
            columnIndex.Add("C", 2);
            columnIndex.Add("D", 3);
            columnIndex.Add("E", 4);
            columnIndex.Add("F", 5);
            columnIndex.Add("G", 6);
            columnIndex.Add("H", 7);
            columnIndex.Add("I", 8);
            columnIndex.Add("J", 9);
            columnIndex.Add("K", 10);
            columnIndex.Add("L", 11);
            columnIndex.Add("M", 12);
            columnIndex.Add("N", 13);
            columnIndex.Add("O", 14);
            columnIndex.Add("P", 15);
            columnIndex.Add("Q", 16);
            columnIndex.Add("R", 17);
            columnIndex.Add("S", 18);
            columnIndex.Add("T", 19);
            columnIndex.Add("U", 20);
            columnIndex.Add("V", 21);
            columnIndex.Add("W", 22);
            columnIndex.Add("X", 23);
            columnIndex.Add("Y", 24);
            columnIndex.Add("Z", 25);
            columnIndex.Add("AA", 26);
            columnIndex.Add("AB", 27);
            columnIndex.Add("AC", 28);
            columnIndex.Add("AD", 29);
            columnIndex.Add("AE", 30);
            columnIndex.Add("AF", 31);
            columnIndex.Add("AG", 32);
            columnIndex.Add("AH", 33);
            columnIndex.Add("AI", 34);
            columnIndex.Add("AJ", 35);
            columnIndex.Add("AK", 36);
            columnIndex.Add("AL", 37);
            columnIndex.Add("AM", 38);
            columnIndex.Add("AN", 39);
            columnIndex.Add("AO", 40);
            columnIndex.Add("AP", 41);
            columnIndex.Add("AQ", 42);
            columnIndex.Add("AR", 43);
            columnIndex.Add("AS", 44);
            columnIndex.Add("AT", 45);
            columnIndex.Add("AU", 46);
            columnIndex.Add("AV", 47);
            columnIndex.Add("AW", 48);
            columnIndex.Add("AX", 49);
            columnIndex.Add("AY", 50);
            columnIndex.Add("AZ", 51);
            columnIndex.Add("BA", 52);
            columnIndex.Add("BB", 53);
            columnIndex.Add("BC", 54);
            columnIndex.Add("BD", 55);
            columnIndex.Add("BE", 56);
            columnIndex.Add("BF", 57);
            columnIndex.Add("BG", 58);
            columnIndex.Add("BH", 59);
            columnIndex.Add("BI", 60);
            columnIndex.Add("BJ", 61);
            columnIndex.Add("BK", 62);
            columnIndex.Add("BL", 63);
            columnIndex.Add("BM", 64);
            columnIndex.Add("BN", 65);
            columnIndex.Add("BO", 66);
            columnIndex.Add("BP", 67);
            columnIndex.Add("BQ", 68);
            columnIndex.Add("BR", 69);
            columnIndex.Add("BS", 70);
            columnIndex.Add("BT", 71);
            columnIndex.Add("BU", 72);
            columnIndex.Add("BV", 73);
            columnIndex.Add("BW", 74);
            columnIndex.Add("BX", 75);
            columnIndex.Add("BY", 76);
            columnIndex.Add("BZ", 77);
            columnIndex.Add("CA", 78);
            columnIndex.Add("CB", 79);
            columnIndex.Add("CC", 80);
            columnIndex.Add("CD", 81);
            columnIndex.Add("CE", 82);
            columnIndex.Add("CF", 83);
            columnIndex.Add("CG", 84);
            columnIndex.Add("CH", 85);
            columnIndex.Add("CI", 86);
            columnIndex.Add("CJ", 87);
            columnIndex.Add("CK", 88);
            columnIndex.Add("CL", 89);
            columnIndex.Add("CM", 90);
            columnIndex.Add("CN", 91);
            columnIndex.Add("CO", 92);
            columnIndex.Add("CP", 93);
            columnIndex.Add("CQ", 94);
            columnIndex.Add("CR", 95);
            columnIndex.Add("CS", 96);
            columnIndex.Add("CT", 97);
            columnIndex.Add("CU", 98);
            columnIndex.Add("CV", 99);
            columnIndex.Add("CW", 100);
            columnIndex.Add("CX", 101);
            columnIndex.Add("CY", 102);
            columnIndex.Add("CZ", 103);
            columnIndex.Add("DA", 104);
            columnIndex.Add("DB", 105);
            columnIndex.Add("DC", 106);
            columnIndex.Add("DD", 107);
            columnIndex.Add("DE", 108);
            columnIndex.Add("DF", 109);
            columnIndex.Add("DG", 110);
            columnIndex.Add("DH", 111);
            columnIndex.Add("DI", 112);
            columnIndex.Add("DJ", 113);
            columnIndex.Add("DK", 114);
            columnIndex.Add("DL", 115);
            columnIndex.Add("DM", 116);
            columnIndex.Add("DN", 117);
            columnIndex.Add("DO", 118);
            columnIndex.Add("DP", 119);
            columnIndex.Add("DQ", 120);
            columnIndex.Add("DR", 121);
            columnIndex.Add("DS", 122);
            columnIndex.Add("DT", 123);
            columnIndex.Add("DU", 124);
            columnIndex.Add("DV", 125);
            columnIndex.Add("DW", 126);
            columnIndex.Add("DX", 127);
            columnIndex.Add("DY", 128);
            columnIndex.Add("DZ", 129);
        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            columnIndex = null;
            workSheet = null;
            workbook = null;
            GC.Collect();
        }

        /// <summary>
        /// 释放excel进程
        /// </summary>
        private void DisposeExcel(Workbook workBook)
        {
            if (workBook == null)
                return;
            try
            {
                if (workSheet != null)
                {
                    workSheet = null;
                }

                if (workBook != null)
                {
                    workBook = null;
                }
                columnIndex = null;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        #endregion
    }
}
