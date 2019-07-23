using BusinessData;
using Common.Utils.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateModule.Services.Import
{
    class ImportRealEstateBook : RealEstateExcelBook
    {
        private int rowCount;//行数
        private int colCount;//列数
        private string tableName;//表名称

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        public List<string> ErrorMsg { get; set; }

        public List<NaturalBuilding> NaturalBuildings { get; set; }
        public List<Household> Households { get; set; }
        public List<Floor> Floors { get; set; }
        public List<LogicalBuilding> LogicalBuildings { get; set; }
        public List<Obligee> Obligees { get; set; }

        public ImportRealEstateBook()
        {
            ErrorMsg = new List<string>();
            NaturalBuildings = new List<NaturalBuilding>();
            Households = new List<Household>();
            Floors = new List<Floor>();
            LogicalBuildings = new List<LogicalBuilding>();
            Obligees = new List<Obligee>();
        }
        /// <summary>
        /// 读方法
        /// </summary>
        public override void Read()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                return;
            }
            if (!System.IO.File.Exists(FileName))
            {
                return;
            }
            try
            {
                Open(FileName);//打开文件
                tableName = System.IO.Path.GetFileNameWithoutExtension(FileName);
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 写方法
        /// </summary>
        public override void Write()
        {
        }

        public bool ReadInformation()
        {
            try
            {
                var NaturalBuildingbool = ReadNaturalBuildingInformation();
                var LogicalBuildingbool = ReadLogicalBuildingInformation();
                var cbool = ReadFloorInformation();
                var hbool = ReadHouseholdInformation();
                var Obligeebool = ReadObligeeInformation();
                return NaturalBuildingbool && LogicalBuildingbool && cbool && hbool && Obligeebool;

            }
            catch (Exception ex)
            {
                ErrorMsg.Add(ex.Message);
                return false;
            }
        }

        #region 自然幢
        /// <summary>
        /// 读取数据
        /// </summary>
        private bool ReadNaturalBuildingInformation()
        {
            SetSheetIndex(0);
            string information = "";
            object[,] allItem = GetAllRangeValue();//获取所有使用域值
            if (allItem == null)
            {
                information = tableName + "表中无数据!";
                ErrorMsg.Add(information);
                return false;
            }
            colCount = GetRangeColumnCount();
            int columnCount = GetColumnValue("W");
            bool canContinue = colCount >= columnCount;
            if (!canContinue)
            {
                allItem = null;
                GC.Collect();
                information = tableName + "中" + GetWorkSheetName(0) + "工作表格式不正确!";
                ErrorMsg.Add(information);
                return false;
            }
            rowCount = GetRangeRowCount();
            int calIndex = 1;
            for (int index = calIndex; index < rowCount; index++)
            {
                string rowValue = GetString(allItem[index, 0]);//编号栏数据
                if (string.IsNullOrWhiteSpace(rowValue) || rowValue.Trim() == "合计" || rowValue.Trim() == "总计" || rowValue.Trim() == "共计")
                {
                    break;
                }
                var NaturalBuilding = InitalizeNaturalBuildingValue(index, allItem);
                NaturalBuildings.Add(NaturalBuilding);
            }
            allItem = null;
            GC.Collect();
            return true;
        }


        /// <summary>
        /// 读取自然僮值
        /// </summary>
        /// <param name="allItem"></param>
        /// <returns></returns>
        private NaturalBuilding InitalizeNaturalBuildingValue(int rowIndex, object[,] allItem)
        {
            //string information = "";
            int columnIndex = 0;
            NaturalBuilding NaturalBuilding = new NaturalBuilding();
            NaturalBuilding.BSM = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.YSDM = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.BDCDYH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.ZDDM = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.ZRZH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.XMMC = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.JZWMC = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            //NaturalBuilding.JGRQ  = columnIndex < colCount ? DateTime.Parse(allItem[rowIndex, columnIndex].ToString()) : new DateTime();//存在问题的 这样写
            NaturalBuilding.JGRQ = columnIndex < colCount ? GetDateTime(allItem[rowIndex, columnIndex]) : new DateTime();
            columnIndex++;
            NaturalBuilding.JZWGD = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            NaturalBuilding.ZZDMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            NaturalBuilding.ZYDMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            NaturalBuilding.YCJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            NaturalBuilding.SCJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            NaturalBuilding.ZCS = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.DSCS = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.DXCS = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.DXSD = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            NaturalBuilding.GHYT = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.FWJG = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.ZTS = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.JZWJBYT = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.BZ = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            NaturalBuilding.ZT = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            return NaturalBuilding;
        }
        #endregion

        #region 逻辑幢
        /// <summary>
        /// 读取逻辑幢数据
        /// </summary>
        private bool ReadLogicalBuildingInformation()
        {
            SetSheetIndex(1);
            string information = "";
            object[,] allItem = GetAllRangeValue();//获取所有使用域值
            if (allItem == null)
            {
                information = tableName + "表中无数据!";
                ErrorMsg.Add(information);
                return false;
            }
            colCount = GetRangeColumnCount();
            int columnCount = GetColumnValue("V");
            bool canContinue = colCount >= columnCount;
            if (!canContinue)
            {
                allItem = null;
                GC.Collect();
                information = tableName + "中" + GetWorkSheetName(1) + "工作表格式不正确!";
                ErrorMsg.Add(information);
                return false;
            }
            rowCount = GetRangeRowCount();
            int calIndex = 1;
            for (int index = calIndex; index < rowCount; index++)
            {
                string rowValue = GetString(allItem[index, 0]);//编号栏数据
                if (string.IsNullOrWhiteSpace(rowValue) || rowValue.Trim() == "合计" || rowValue.Trim() == "总计" || rowValue.Trim() == "共计")
                {
                    break;
                }
                var LogicalBuilding = InitalizeLogicalBuildingValue(index, allItem);
                LogicalBuildings.Add(LogicalBuilding);
            }
            allItem = null;
            GC.Collect();
            return true;
        }


        /// <summary>
        /// 读取逻辑幢值
        /// </summary>
        /// <param name="allItem"></param>
        /// <returns></returns>
        private LogicalBuilding InitalizeLogicalBuildingValue(int rowIndex, object[,] allItem)
        {
            //string information = "";
            int columnIndex = 0;
            LogicalBuilding LogicalBuilding = new LogicalBuilding();
            LogicalBuilding.LJZH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.ZRZH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.YSDM = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.MPH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.YCJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            LogicalBuilding.YCDXMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            LogicalBuilding.YCQTMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            LogicalBuilding.SCJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            LogicalBuilding.SCDXMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            LogicalBuilding.SCQTMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            LogicalBuilding.JGRQ = columnIndex < colCount ? GetDateTime(allItem[rowIndex, columnIndex]) : new DateTime();
            columnIndex++;
            LogicalBuilding.FWJG1 = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.FWJG2 = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.FWJG3 = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.JZWZT = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.FWYT1 = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.FWYT2 = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.FWYT3 = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.ZCS = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.DSCS = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.DXCS = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            LogicalBuilding.BZ = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            return LogicalBuilding;
        }
        #endregion

        #region 层
        /// <summary>
        /// 读取层数据
        /// </summary>
        private bool ReadFloorInformation()
        {
            SetSheetIndex(2);
            string information = "";
            object[,] allItem = GetAllRangeValue();//获取所有使用域值
            if (allItem == null)
            {
                information = tableName + "表中无数据!";
                ErrorMsg.Add(information);
                return false;
            }
            colCount = GetRangeColumnCount();
            int columnCount = GetColumnValue("M");
            bool canContinue = colCount >= columnCount;
            if (!canContinue)
            {
                allItem = null;
                GC.Collect();
                information = tableName + "中" + GetWorkSheetName(2) + "工作表格式不正确!";
                ErrorMsg.Add(information);
                return false;
            }
            rowCount = GetRangeRowCount();
            int calIndex = 1;
            for (int index = calIndex; index < rowCount; index++)
            {
                string rowValue = GetString(allItem[index, 0]);//编号栏数据
                if (string.IsNullOrWhiteSpace(rowValue) || rowValue.Trim() == "合计" || rowValue.Trim() == "总计" || rowValue.Trim() == "共计")
                {
                    break;
                }
                var floor = InitalizeFloorValue(index, allItem);
                Floors.Add(floor);
            }
            allItem = null;
            GC.Collect();
            return true;
        }


        /// <summary>
        /// 读取层值
        /// </summary>
        /// <param name="allItem"></param>
        /// <returns></returns>
        private Floor InitalizeFloorValue(int rowIndex, object[,] allItem)
        {
            //string information = "";
            int columnIndex = 0;
            Floor Floor = new Floor();
            Floor.CH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Floor.ZRZH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Floor.YSDM = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Floor.SJC = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Floor.MYC = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Floor.CJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Floor.CTNJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Floor.CYTMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Floor.CGYJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Floor.CFTJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Floor.CBQMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Floor.CG = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Floor.SPTYMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            return Floor;

        }
        #endregion

        #region 户
        /// <summary>
        /// 读取户数据
        /// </summary>
        private bool ReadHouseholdInformation()
        {
            SetSheetIndex(3);
            string information = "";
            object[,] allItem = GetAllRangeValue();//获取所有使用域值
            if (allItem == null)
            {
                information = tableName + "表中无数据!";
                ErrorMsg.Add(information);
                return false;
            }
            colCount = GetRangeColumnCount();
            int columnCount = GetColumnValue("BA");
            bool canContinue = colCount >= columnCount;
            if (!canContinue)
            {
                allItem = null;
                GC.Collect();
                information = tableName + "中" + GetWorkSheetName(3) + "工作表格式不正确!";
                ErrorMsg.Add(information);
                return false;
            }
            rowCount = GetRangeRowCount();
            int calIndex = 1;
            for (int index = calIndex; index < rowCount; index++)
            {
                string rowValue = GetString(allItem[index, 0]);//编号栏数据
                if (string.IsNullOrWhiteSpace(rowValue) || rowValue.Trim() == "合计" || rowValue.Trim() == "总计" || rowValue.Trim() == "共计")
                {
                    break;
                }
                var h = InitalizeHouseholdValue(index, allItem);
                Households.Add(h);
            }
            allItem = null;
            GC.Collect();
            return true;
        }


        /// <summary>
        /// 读取户值
        /// </summary>
        /// <param name="allItem"></param>
        /// <returns></returns>
        private Household InitalizeHouseholdValue(int rowIndex, object[,] allItem)
        {
            //string information = "";
            int columnIndex = 0;
            Household Household = new Household();
            Household.HBSM = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.YXTBS = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.BDCDYH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.FWBM = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.YSDM = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.ZRZH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.LJZH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.DYH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.ZCS = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.CH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.FH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.ZL = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.MJDW = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.SZC = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.QSC = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.ZZC = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.HH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.SHBW = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.HX = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.HXJG = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.GHYT = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.FWYT1 = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.FWYT2 = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.FWYT3 = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.YCJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.YCTNJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.YCFTJZMJ= columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.YCDXBFJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.YCQTJZMJ= columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.YCFTXS = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.SCJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.SCTNJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.SCFTJZMJ= columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.SCDXBFJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.SCQTJZMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.SCFTXS= columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.GYTDMJ= columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.FTTDMJ= columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.DYTDMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Household.FWLX  = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.FWJG= columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.FWXZ = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.FDCJYJG= columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.JGSJ = columnIndex < colCount ? GetDateTime(allItem[rowIndex, columnIndex]) : new DateTime();
            columnIndex++;
            Household.FWCB = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.CQLY = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.QTGSD = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.QTGSN = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.QTGSX= columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.QTGSB = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.TDSYQR = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.FCFHT = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Household.ZT = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";

            return Household;
        }
        #endregion

        #region 权利人
        /// <summary>
        /// 读取权利人数据
        /// </summary>
        private bool ReadObligeeInformation()
        {
            SetSheetIndex(4);
            string information = "";
            object[,] allItem = GetAllRangeValue();//获取所有使用域值
            if (allItem == null)
            {
                information = tableName + "表中无数据!";
                ErrorMsg.Add(information);
                return false;
            }
            colCount = GetRangeColumnCount();
            int columnCount = GetColumnValue("AA");
            bool canContinue = colCount >= columnCount;
            if (!canContinue)
            {
                allItem = null;
                GC.Collect();
                information = tableName + "中" + GetWorkSheetName(4) + "工作表格式不正确!";
                ErrorMsg.Add(information);
                return false;
            }
            rowCount = GetRangeRowCount();
            int calIndex = 1;
            for (int index = calIndex; index < rowCount; index++)
            {
                string rowValue = GetString(allItem[index, 0]);//编号栏数据
                if (string.IsNullOrWhiteSpace(rowValue) || rowValue.Trim() == "合计" || rowValue.Trim() == "总计" || rowValue.Trim() == "共计")
                {
                    break;
                }
                var Obligee = InitalizebligeeBValue(index, allItem);
                Obligees.Add(Obligee);
            }
            allItem = null;
            GC.Collect();
            return true;
        }


        /// <summary>
        /// 读取权利人值
        /// </summary>
        /// <param name="allItem"></param>
        /// <returns></returns>
        private Obligee InitalizebligeeBValue(int rowIndex, object[,] allItem)
        {
            //string information = "";
            int columnIndex = 0;
            Obligee Obligee = new Obligee();
            Obligee.HBSM = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.QLRMC = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.BDCQZH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.ZJZL = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.ZJH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.GJ = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.XB = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.QLRLX = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.DH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.YB = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.DZ = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.QLLX = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.GYFS = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.QLMJ = columnIndex < colCount ? GetDouble(GetString(allItem[rowIndex, columnIndex])) : 0.0;
            columnIndex++;
            Obligee.QLBL = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.FRXM = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.FRZJLX = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.FRZJH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.FRDH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.DLRXM = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.DLRZJLX = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.DLRZJH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.DLRDH = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.GZDW = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.DLJGMC = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.DZYJ = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";
            columnIndex++;
            Obligee.BZ = columnIndex < colCount ? GetString(allItem[rowIndex, columnIndex]) : "";

            return Obligee;
        }
        #endregion
    }
}
