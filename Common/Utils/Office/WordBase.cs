using System;
using System.IO;
using Aspose.Words;
using Aspose.Words.Tables;
using Aspose.Words.Drawing;
using Aspose.Words.Fields;
using Aspose.Words.Saving;

namespace Common.Utils.Office
{
    /// <summary>
    /// Word对象基类
    /// </summary>
    [Serializable]
    public abstract class WordBase
    {
        #region Fields

        private string fileName;//文件名称

        #endregion

        #region ConstName

        public const string TEMPLATEFILE_NOT_OPEN = "未初始化模板";

        #endregion

        #region Fields

        protected Document doc;//文档
        protected DocumentBuilder builder;//构建器

        #endregion

        #region Ctor

        public WordBase()
        {
        }

        ~WordBase()
        {
        }

        #endregion

        #region Methods

        #region Methods - Public

        /// <summary>
        /// 注销
        /// </summary>
        public static void Dispose()
        {

        }

        /// <summary>
        /// 打开模版文件
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <returns></returns>
        public bool OpenTemplate(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }
            this.fileName = fileName;
            doc = new Document(fileName);
            if (doc != null)
            {
                builder = new DocumentBuilder(doc);
            }
            return doc != null;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (doc == null)
            {
                return;
            }
            doc = null;
            builder = null;
            GC.Collect();
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="data"></param>
        public void Print(object data)
        {
            if (doc == null)
            {
                throw new Exception(TEMPLATEFILE_NOT_OPEN);
            }
            bool success = OnSetParamValue(data);
            if (!success)
            {
                return;
            }
            doc.Print();
            Close();
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="close"></param>
        public void Print(object data, bool close)
        {
            if (doc == null)
            {
                throw new Exception(TEMPLATEFILE_NOT_OPEN);
            }
            bool success = OnSetParamValue(data);
            if (!success)
            {
                return;
            }
            doc.Print();
            if (close)
            {
                Close();
            }
        }


        /// <summary>
        /// 保存文件
        /// </summary>
        public void Save()
        {
            if (doc == null)
            {
                throw new Exception(TEMPLATEFILE_NOT_OPEN);
            }
            fileName = WordOperator.InitalizeValideFileName(fileName);
            string extension = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
            {
                fileName += ".doc";
            }
            else
            {
                fileName = extension == ".doc" ? fileName : fileName.Replace(extension, ".doc");
            }
            WordOperator.InitalzieDirectory(fileName);
            doc.Save(fileName, SaveFormat.Doc);
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="fileName">文件名称</param>
        public void SaveAs(object data, string fileName)
        {
            if (doc == null)
            {
                throw new Exception(TEMPLATEFILE_NOT_OPEN);
            }
            fileName = WordOperator.InitalizeValideFileName(fileName);
            string extension = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
            {
                fileName += ".doc";
            }
            else
            {
                fileName = extension == ".doc" ? fileName : fileName.Replace(extension, ".doc");
            }
            bool success = OnSetParamValue(data);
            if (success)
            {
                WordOperator.InitalzieDirectory(fileName);
                doc.Save(fileName, SaveFormat.Doc);
                Close();
            }
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="close">是否关闭</param>
        public void SaveAs(object data, string fileName, bool close)
        {
            if (doc == null)
            {
                throw new Exception(TEMPLATEFILE_NOT_OPEN);
            }
            fileName = WordOperator.InitalizeValideFileName(fileName);
            string extension = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
            {
                fileName += ".doc";
            }
            else
            {
                fileName = extension == ".doc" ? fileName : fileName.Replace(extension, ".doc");
            }
            bool success = OnSetParamValue(data);
            if (success)
            {
                WordOperator.InitalzieDirectory(fileName);
                doc.Save(fileName, SaveFormat.Doc);
                if (close)
                {
                    Close();
                }
            }
        }

        /// <summary>
        /// 另存为Xps文档
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="fileName">文件名称</param>
        public void SaveAsXps(object data, string fileName)
        {
            if (doc == null)
            {
                throw new Exception(TEMPLATEFILE_NOT_OPEN);
            }
            fileName = WordOperator.InitalizeValideFileName(fileName);
            string extension = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
            {
                fileName += ".xps";
            }
            else
            {
                fileName = extension == ".xps" ? fileName : fileName.Replace(extension, ".xps");
            }
            bool success = OnSetParamValue(data);
            if (success)
            {
                WordOperator.InitalzieDirectory(fileName);
                doc.Save(fileName);
                Close();
            }
        }

        /// <summary>
        /// 另存为Xps文档
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="fileName">文件名称</param>
        public void SaveAsJpeg(object data, string fileName)
        {
            if (doc == null)
            {
                throw new Exception(TEMPLATEFILE_NOT_OPEN);
            }
            fileName = WordOperator.InitalizeValideFileName(fileName);
            string extension = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
            {
                fileName += ".jpg";
            }
            else
            {
                fileName = extension == ".jpg" ? fileName : fileName.Replace(extension, ".jpg");
            }
            bool success = OnSetParamValue(data);
            if (success)
            {
                WordOperator.InitalzieDirectory(fileName);
                ImageSaveOptions options = new ImageSaveOptions(SaveFormat.Jpeg);
                options.Resolution = 300;
                doc.Save(fileName, options);
                Close();
            }
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="close">是否关闭</param>
        public void SaveAsMultiFile(object data, string fileName)
        {
            if (doc == null)
            {
                throw new Exception(TEMPLATEFILE_NOT_OPEN);
            }
            fileName = WordOperator.InitalizeValideFileName(fileName);
            string extension = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
            {
                fileName += ".doc";
            }
            else
            {
                fileName = extension == ".doc" ? fileName : fileName.Replace(extension, ".doc");
            }
            bool success = OnSetParamValue(data);
            if (success)
            {
                WordOperator.InitalzieDirectory(fileName);
                doc.Save(fileName, SaveFormat.Doc);
                ImageSaveOptions options = new ImageSaveOptions(SaveFormat.Jpeg);
                options.Resolution = 300;
                fileName = fileName.Replace(".doc", "");
                string number = fileName.Substring(fileName.Length - 1);
                int order = 1;
                Int32.TryParse(number, out order);
                fileName = fileName.Substring(0, fileName.Length - 1);
                string nameFile = fileName + (order == 0 ? number : order.ToString()) + ".jpg";
                for (int i = 0; i < doc.PageCount; i++)
                {
                    options.PageIndex = i;
                    doc.Save(nameFile, options);
                    order++;
                    nameFile = fileName + order.ToString() + ".jpg";
                }
                Close();
            }
        }

        /// <summary>
        /// 清空书签值
        /// </summary>
        public void ClearBookmarkValue()
        {
            if (doc == null)
            {
                return;
            }
            for (int index = 1; index < doc.Range.Bookmarks.Count; index++)
            {
                doc.Range.Bookmarks[index].Text = "";
            }
        }

        #endregion

        #region Methods - Protected

        /// <summary>
        /// 设置书签值
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        protected void SetBookmarkValue(object paramName, string paramValue)
        {
            if (doc == null || paramName == null)
            {
                return;
            }
            try
            {
                Bookmark bookmark = doc.Range.Bookmarks[paramName.ToString()];
                if (bookmark == null)
                {
                    return;
                }
                bookmark.Text = string.IsNullOrEmpty(paramValue) ? "" : paramValue;
                bookmark = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 设置控件值
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        protected void SetControlValue(object paramName, object paramValue, int controlType = 71)
        {
            if (doc == null || paramName == null || paramValue == null)
            {
                return;
            }
            try
            {
                FieldType fieldType = (FieldType)Enum.Parse(typeof(FieldType), controlType.ToString());
                foreach (FormField field in doc.Range.FormFields)
                {
                    if (field != null && field.Type == fieldType
                        && field.Name == paramName.ToString())
                    {
                        field.Checked = paramValue != null;
                        break;
                    }
                }
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 设置表格单元值
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        protected string GetTableCellValue(int tableIndex, int rowIndex, int columnIndex)
        {
            if (doc == null)
            {
                return "";
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return "";
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return "";
            }
            try
            {
                if (rowIndex >= table.Rows.Count)
                {
                    return "";
                }
                Row row = table.Rows[rowIndex];
                if (columnIndex >= row.Cells.Count)
                {
                    return "";
                }
                Cell cell = row.Cells[columnIndex];
                row = null;
                table = null;
                tables = null;
                return cell.GetText();
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return "";
        }

        /// <summary>
        /// 设置表格单元值
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        protected void SetTableRowHeight(int tableIndex, int rowIndex, double height)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                if (rowIndex >= table.Rows.Count)
                {
                    return;
                }
                Row row = table.Rows[rowIndex];
                row.RowFormat.Height = height;
                row = null;
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 设置表格单元值
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        protected void SetTableCellValue(int tableIndex, int rowIndex, int colIndex, string value)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                if (rowIndex >= table.Rows.Count)
                {
                    return;
                }
                Row row = table.Rows[rowIndex];
                Cell cell = row.Cells[colIndex];
                if (cell != null)
                {
                    builder.MoveToCell(tableIndex, rowIndex, colIndex, 0);
                    builder.Write(value);
                }
                cell = null;
                row = null;
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 设置表格单元值
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        protected void SetAbjointTableCellValue(int tableIndex, int rowIndex, int colIndex, string value)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                if (rowIndex >= table.Rows.Count)
                {
                    return;
                }
                Row row = table.Rows[rowIndex];
                Cell cell = row.Cells[colIndex];
                if (cell != null)
                {
                    builder.MoveToSection(1);
                    builder.MoveToCell(tableIndex - 1, rowIndex, colIndex, 0);
                    builder.Write(value);
                }
                cell = null;
                row = null;
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 设置表格单元值
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        protected void SetTableCellValue(int tableIndex, int rowIndex, int colIndex, string value, int textOrientation, int alignment)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                if (rowIndex >= table.Rows.Count)
                {
                    return;
                }
                Row row = table.Rows[rowIndex];
                Cell cell = row.Cells[colIndex];
                if (cell != null)
                {
                    builder.MoveToCell(tableIndex, rowIndex, colIndex, 0);
                    if (textOrientation > 0)
                    {
                        cell.CellFormat.Orientation = InitalizeTextOrientation(textOrientation);
                    }
                    if (alignment > 0)
                    {
                        cell.CellFormat.VerticalAlignment = InitalizeCellAlignment(alignment);
                    }
                    builder.Write(value);
                }
                cell = null;
                row = null;
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 设置表格单元值
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        protected void SetAbjointTableCellValue(int tableIndex, int rowIndex, int colIndex, string value, int textOrientation, int alignment)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                if (rowIndex >= table.Rows.Count)
                {
                    return;
                }
                Row row = table.Rows[rowIndex];
                Cell cell = row.Cells[colIndex];
                if (cell != null)
                {
                    builder.MoveToSection(1);
                    builder.MoveToCell(tableIndex - 1, rowIndex, colIndex, 0);
                    if (textOrientation > 0)
                    {
                        cell.CellFormat.Orientation = InitalizeTextOrientation(textOrientation);
                    }
                    if (alignment > 0)
                    {
                        cell.CellFormat.VerticalAlignment = InitalizeCellAlignment(alignment);
                    }
                    builder.Write(value);
                }
                cell = null;
                row = null;
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 设置文字方向
        /// </summary>
        /// <param name="orientation"></param>
        private TextOrientation InitalizeTextOrientation(int orientation)
        {
            TextOrientation textOrientation = TextOrientation.Horizontal;
            switch (orientation)
            {
                case 1:
                    textOrientation = TextOrientation.Downward;
                    break;
                case 2:
                    textOrientation = TextOrientation.Horizontal;
                    break;
                case 3:
                    textOrientation = TextOrientation.HorizontalRotatedFarEast;
                    break;
                case 4:
                    textOrientation = TextOrientation.Upward;
                    break;
                case 5:
                    textOrientation = TextOrientation.VerticalFarEast;
                    break;
                default:
                    break;
            }
            return textOrientation;
        }

        /// <summary>
        /// 初始化单元格对齐方式
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        private CellVerticalAlignment InitalizeCellAlignment(int alignment)
        {
            CellVerticalAlignment cellAlignment = CellVerticalAlignment.Center;
            switch (alignment)
            {
                case 1:
                    cellAlignment = CellVerticalAlignment.Top;
                    break;
                case 2:
                    cellAlignment = CellVerticalAlignment.Center;
                    break;
                case 3:
                    cellAlignment = CellVerticalAlignment.Bottom;
                    break;
                default:
                    break;
            }
            return cellAlignment;
        }

        /// <summary>
        /// 设置表格单元值
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        protected void SetTableCellValue(int tableIndex, int rowIndex, int colIndex, string imagePath, double width, double height, bool setHeight = true)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                if (rowIndex >= table.Rows.Count)
                {
                    return;
                }
                Row row = table.Rows[rowIndex];
                Cell cell = row.Cells[colIndex];
                if (cell == null)
                {
                    return;
                }
                DocumentBuilder shapeBuilder = new DocumentBuilder(doc);
                shapeBuilder.MoveToCell(tableIndex, rowIndex, colIndex, 0);
                Shape shape = new Shape(doc, ShapeType.Image);
                shape.ImageData.SetImage(imagePath);
                shape.Width = width;
                shape.Height = height;
                shape.HorizontalAlignment = HorizontalAlignment.Center;//水平对齐
                shape.VerticalAlignment = VerticalAlignment.Center;//垂直对齐
                shape.WrapSide = WrapSide.Both;
                shapeBuilder.CellFormat.TopPadding = 2;
                shapeBuilder.CellFormat.LeftPadding = 2;
                shapeBuilder.CellFormat.RightPadding = 2;
                shapeBuilder.CellFormat.BottomPadding = 2;
                shapeBuilder.InsertNode(shape);
                if (setHeight)
                {
                    shapeBuilder.RowFormat.Height = height + 2;
                }
                cell = null;
                row = null;
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 设置表格单元值
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        protected void SetAbjointTableCellValue(int tableIndex, int rowIndex, int colIndex, string imagePath, double width, double height, bool setHeight = true)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                if (rowIndex >= table.Rows.Count)
                {
                    return;
                }
                Row row = table.Rows[rowIndex];
                Cell cell = row.Cells[colIndex];
                if (cell == null)
                {
                    return;
                }
                DocumentBuilder shapeBuilder = new DocumentBuilder(doc);
                shapeBuilder.MoveToSection(1);
                shapeBuilder.MoveToCell(tableIndex - 1, rowIndex, colIndex, 0);
                Shape shape = new Shape(doc, ShapeType.Image);
                shape.ImageData.SetImage(imagePath);
                shape.Width = width;
                shape.Height = height;
                shape.HorizontalAlignment = HorizontalAlignment.Center;//水平对齐
                shape.VerticalAlignment = VerticalAlignment.Center;//垂直对齐
                shape.WrapSide = WrapSide.Both;
                shapeBuilder.CellFormat.TopPadding = 2;
                shapeBuilder.CellFormat.LeftPadding = 2;
                shapeBuilder.CellFormat.RightPadding = 2;
                shapeBuilder.CellFormat.BottomPadding = 2;
                shapeBuilder.InsertNode(shape);
                if (setHeight)
                {
                    shapeBuilder.RowFormat.Height = height + 2;
                }
                cell = null;
                row = null;
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 设置表格单元值
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        protected void SetTableCellBordor(int tableIndex, int rowIndex, int columnIndex, int border)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                if (rowIndex >= table.Rows.Count)
                {
                    return;
                }
                Row row = table.Rows[rowIndex];
                if (columnIndex >= row.Cells.Count)
                {
                    return;
                }
                Cell cell = row.Cells[columnIndex];
                switch (border)
                {
                    case 1:
                        cell.CellFormat.Borders.Top.Shadow = true;
                        break;
                    case 2:
                        cell.CellFormat.Borders.Right.Shadow = true;
                        break;
                    case 3:
                        cell.CellFormat.Borders.Bottom.Shadow = true;
                        break;
                    case 4:
                        cell.CellFormat.Borders.Left.Shadow = true;
                        break;
                }
                row = null;
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 删除表格
        /// </summary>
        protected void DeleteTable(int tableIndex)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                doc.ChildNodes.Remove(table);
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 添加克隆表格
        /// </summary>
        protected void InsertCloneTable(int tableIndex)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                Table tableClone = (Table)table.Clone(true);
                table.ParentNode.InsertAfter(tableClone, table);
                table.ParentNode.InsertAfter(new Paragraph(doc), table);
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 获取表格
        /// </summary>
        protected Table GetTable(int tableIndex)
        {
            if (doc == null)
            {
                return null;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return null;
            }
            Table table = tables[tableIndex] as Table;
            return table;
        }

        /// <summary>
        /// 向指定的表中插入单元格
        /// </summary>
        /// <param name="tableIndex"></param>
        protected void InsertTableCell(int tableIndex)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                table.Rows.Add(table.LastRow.Clone(true));
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 向指定的表中插入单元格
        /// </summary>
        /// <param name="tableIndex">表号</param>
        /// <param name="rowCount">插入几行数据</param>
        protected void InsertTableCell(int tableIndex, int rowCount)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                for (int index = 0; index < rowCount; index++)
                {
                    Node node = table.LastRow.Clone(true);
                    table.Rows.Add(node);
                    node = null;
                }
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 向指定的表中插入单元格
        /// </summary>
        /// <param name="tableIndex">表号</param>
        /// <param name="tableIndex">开始插入行</param>
        /// <param name="rowCount">插入几行数据</param>
        protected void InsertTableRow(int tableIndex, int startRow, int rowCount)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            if (startRow >= table.Rows.Count)
            {
                return;
            }
            try
            {
                for (int index = 0; index < rowCount; index++)
                {
                    Node node = table.Rows[startRow].Clone(true);
                    table.Rows.Insert(startRow, node);
                    node = null;
                }
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 向指定的表中插入单元格
        /// </summary>
        /// <param name="tableIndex">表号</param>
        /// <param name="tableIndex">开始插入行</param>
        /// <param name="rowCount">插入几行数据</param>
        protected void InsertTableRowClone(int tableIndex, int startRow, int rowCount = 0)
        {
            if (doc == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            if (startRow >= table.Rows.Count)
            {
                return;
            }
            try
            {
                Node node = null;
                if (rowCount == 0)
                {
                    node = table.Rows[startRow].Clone(true);
                    table.Rows.Add(node);
                }
                else
                {
                    for (int index = 0; index < rowCount; index++)
                    {
                        node = table.Rows[startRow].Clone(true);
                        table.Rows.Insert(table.Rows.Count, node);
                    }
                }
                node = null;
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="imagePath"></param>
        protected void InsertImageCell(object paramName, string imagePath, double width, double height, bool setHeight = true)
        {
            if (paramName == null || string.IsNullOrEmpty(imagePath))
            {
                return;
            }
            Bookmark bookmark = doc.Range.Bookmarks[paramName.ToString()];
            if (bookmark == null)
            {
                return;
            }
            if (doc == null || builder == null)
            {
                return;
            }
            Shape shape = new Shape(doc, ShapeType.Image);
            shape.ImageData.SetImage(imagePath);
            shape.Width = width;
            shape.Height = height;
            shape.HorizontalAlignment = HorizontalAlignment.Center;//水平对齐
            shape.VerticalAlignment = VerticalAlignment.Center;//垂直对齐
            builder.MoveToBookmark(paramName.ToString());
            builder.CellFormat.TopPadding = 2;
            builder.CellFormat.LeftPadding = 2;
            builder.CellFormat.RightPadding = 2;
            builder.CellFormat.BottomPadding = 2;
            if (setHeight)
            {
                builder.RowFormat.Height = height;
            }
            builder.InsertNode(shape);
            shape = null;
            bookmark = null;
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="imagePath"></param>
        protected void InsertImageCellWithoutPading(object paramName, string imagePath, double width, double height)
        {
            if (paramName == null || string.IsNullOrEmpty(imagePath))
            {
                return;
            }
            Bookmark bookmark = doc.Range.Bookmarks[paramName.ToString()];
            if (bookmark == null)
            {
                return;
            }
            if (doc == null || builder == null)
            {
                return;
            }
            Shape shape = new Shape(doc, ShapeType.Image);
            shape.ImageData.SetImage(imagePath);
            shape.Width = width;
            shape.Height = height;
            shape.HorizontalAlignment = HorizontalAlignment.Center;//水平对齐
            shape.VerticalAlignment = VerticalAlignment.Center;//垂直对齐
            builder.MoveToBookmark(paramName.ToString());
            builder.RowFormat.Height = height;
            builder.InsertNode(shape);
            shape = null;
            bookmark = null;
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="imagePath"></param>
        protected void InsertImageCell(object paramName, string imagePath, double width, double height, int hAlignment, int vAlignment)
        {
            if (paramName == null || string.IsNullOrEmpty(imagePath))
            {
                return;
            }
            Bookmark bookmark = doc.Range.Bookmarks[paramName.ToString()];
            if (bookmark == null)
            {
                return;
            }
            if (doc == null || builder == null)
            {
                return;
            }
            Shape shape = new Shape(doc, ShapeType.Image);
            shape.ImageData.SetImage(imagePath);
            shape.Width = width;
            shape.Height = height;
            shape.HorizontalAlignment = (HorizontalAlignment)hAlignment;//水平对齐
            shape.VerticalAlignment = (VerticalAlignment)vAlignment;//垂直对齐
            builder.MoveToBookmark(paramName.ToString());
            builder.CellFormat.TopPadding = 2;
            builder.CellFormat.LeftPadding = 2;
            builder.CellFormat.RightPadding = 2;
            builder.CellFormat.BottomPadding = 2;
            builder.RowFormat.Height = height + 2;
            builder.InsertNode(shape);
            shape = null;
            bookmark = null;
        }

        /// <summary>
        /// 水平合并表中单元格
        /// </summary>
        protected void HorizontalMergeTable(int tableIndex, int rowIndex, int startColumnIndex, int endColumnIndex)
        {
            if (doc == null || builder == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                if (rowIndex >= table.Rows.Count)
                {
                    return;
                }
                if (endColumnIndex <= startColumnIndex)
                {
                    return;
                }
                Row row = table.Rows[rowIndex];
                if (endColumnIndex > row.Cells.Count)
                {
                    return;
                }
                Cell cell = row.Cells[startColumnIndex];
                if (cell == null)
                {
                    return;
                }
                for (int index = startColumnIndex; index < endColumnIndex; index++)
                {
                    cell = row.Cells[index];
                    cell.CellFormat.HorizontalMerge = CellMerge.Previous;
                }
                cell = null;
                row = null;
                table = null;
                tables = null;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 垂直合并表中单元格
        /// </summary>
        protected void VerticalMergeTable(int tableIndex, int startRowIndex, int endRowIndex, int columnIndex)
        {
            if (doc == null || builder == null)
            {
                return;
            }
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            if (tables == null || tables.Count == 0 || tableIndex >= tables.Count)
            {
                return;
            }
            Table table = tables[tableIndex] as Table;
            if (table == null)
            {
                return;
            }
            try
            {
                if (startRowIndex >= table.Rows.Count || endRowIndex >= table.Rows.Count)
                {
                    return;
                }
                if (endRowIndex <= startRowIndex)
                {
                    return;
                }
                for (int index = startRowIndex; index <= endRowIndex; index++)
                {
                    Row row = table.Rows[index];
                    if (columnIndex > row.Cells.Count)
                    {
                        continue;
                    }
                    Cell cell = row.Cells[columnIndex];
                    if (cell == null)
                    {
                        continue;
                    }
                    cell.CellFormat.VerticalMerge = CellMerge.Previous;
                    cell = null;
                    row = null;
                }
                table = null;
                tables = null;
                GC.Collect();
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        #endregion

        #region Methods - Override

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual bool OnSetParamValue(object data)
        {
            return true;
        }

        #endregion

        #endregion
    }
}
