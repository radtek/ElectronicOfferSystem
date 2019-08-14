using BusinessData;
using Common.Models;
using Common.Utils;
using Common.Utils.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateModule.Services.Export
{
    public class ExportRealEstateBook : RealEstateExcelBook
    {
        public Project Project { get; set; }
        public List<string> ErrorMsg { get; set; }
        public List<NaturalBuilding> NaturalBuildings { get; set; }
        public List<Household> Households { get; set; }
        public List<Floor> Floors { get; set; }
        public List<LogicalBuilding> LogicalBuildings { get; set; }
        public List<Obligee> Obligees { get; set; }
        public List<Mortgage> Mortgages { get; set; }
        public List<Sequestration> Sequestrations { get; set; }

        public TaskMessage TaskMessage { get; set; }
        public int TotalCount { get; set; }
        public double index { get; set; }

        public ExportRealEstateBook()
        {
            ErrorMsg = new List<string>();
        }

        public override void Read()
        {

        }

        public override void Write()
        {
            TotalCount = 0;
            index = 0.0;

            // 获取总共的条数
            if (NaturalBuildings != null)
                TotalCount += NaturalBuildings.Count;
            if (LogicalBuildings != null)
                TotalCount += LogicalBuildings.Count;
            if (Floors != null)
                TotalCount += Floors.Count;
            if (Households != null)
                TotalCount += Households.Count;
            if (Obligees != null)
                TotalCount += Obligees.Count;
            if ("2".Equals(Project.OwnershipType))
            {
                if (Mortgages != null)
                    TotalCount += Mortgages.Count;
                if (Sequestrations != null)
                    TotalCount += Sequestrations.Count;
            }


            if (TotalCount == 0)
            {
                ErrorMsg.Add("该项目无数据");
                return;
            }
            

            try
            {
                WriteNaturalBuilding();
                WriteLogicalBuilding();
                WriteFloor();
                WriteHouse();
                WriteObligee();
                if ("2".Equals(Project.OwnershipType))
                {
                    WriteMortgage();
                    WriteSequestration();
                }
                   
            }
            catch (Exception ex)
            {
                ErrorMsg.Add(ex.Message+ex.StackTrace);
            }
        }

        #region 自然幢
        private void WriteNaturalBuilding()
        {
            if (NaturalBuildings == null || NaturalBuildings.Count == 0)
                return;

            SetSheetIndex(0);

            int rowIndex = 2;

            InsertRowsCell(rowIndex, NaturalBuildings.Count-1);

            foreach (var NaturalBuilding in NaturalBuildings)
            {
                InitalizeNaturalBuildingValue(NaturalBuilding, rowIndex);
                rowIndex++;

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
            }
        }
        private void InitalizeNaturalBuildingValue(NaturalBuilding NaturalBuilding, int rowIndex)
        {
            int columnIndex = 1;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.BSM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.YSDM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.BDCDYH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.ZDDM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.ZRZH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.XMMC);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.JZWMC);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolDate.GetSlashDate(NaturalBuilding.JGRQ)); 
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.JZWGD);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(NaturalBuilding.ZZDMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(NaturalBuilding.ZYDMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(NaturalBuilding.YCJZMJ, 2)); 
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(NaturalBuilding.SCJZMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.ZCS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.DSCS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.DXCS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.DXSD);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(NaturalBuilding.GHYT, "房屋用途"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(NaturalBuilding.FWJG, "房屋结构"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.ZTS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.JZWJBYT);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.BZ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(NaturalBuilding.ZT, "不动产单元状态"));
        }
        #endregion

        #region 逻辑幢
        private void WriteLogicalBuilding()
        {
            if (LogicalBuildings == null || LogicalBuildings.Count == 0)
                return;
            SetSheetIndex(1);
            int rowIndex = 2;
            InsertRowsCell(rowIndex, LogicalBuildings.Count-1);
            foreach (var LogicalBuilding in LogicalBuildings)
            {
                InitalizeNaturalValue(LogicalBuilding, rowIndex);
                rowIndex++;

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
            }
        }
        private void InitalizeNaturalValue(LogicalBuilding LogicalBuilding, int rowIndex)
        {
            int columnIndex = 1;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.LJZH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.ZRZH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.YSDM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.MPH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.YCJZMJ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.YCDXMJ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.YCQTMJ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.SCJZMJ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.SCDXMJ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.SCQTMJ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.JGRQ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.FWJG1);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.FWJG2);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.FWJG3);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.JZWZT);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.FWYT1);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.FWYT2);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.FWYT3);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.ZCS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.DSCS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.DXCS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.BZ);
        }
        #endregion

        #region 层
        private void WriteFloor()
        {
            if (Floors == null || Floors.Count == 0)
                return;
            SetSheetIndex(2);
            int rowIndex = 2;
            InsertRowsCell(rowIndex, Floors.Count-1);
            foreach (var Floor in Floors)
            {
                InitalizeWriteFloorValue(Floor, rowIndex);
                rowIndex++;

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
            }
        }
        private void InitalizeWriteFloorValue(Floor Floor, int rowIndex)
        {
            int columnIndex = 1;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.CH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.ZRZH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.YSDM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.SJC);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.MYC);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.CJZMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.CTNJZMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.CYTMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.CGYJZMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.CFTJZMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.CBQMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.CG.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Floor.SPTYMJ.ToString());
        }
        #endregion

        #region 户
        private void WriteHouse()
        {
            if (Households == null || Households.Count == 0)
                return;
            SetSheetIndex(3);
            int rowIndex = 2;
            InsertRowsCell(rowIndex, Households.Count-1);
            foreach (var Household in Households)
            {
                InitalizeHouselValue(Household, rowIndex);
                rowIndex++;

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
            }
        }
        private void InitalizeHouselValue(Household Household, int rowIndex)
        {
            int columnIndex = 1;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.HBSM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.YXTBS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.BDCDYH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FWBM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.YSDM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.ZRZH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.LJZH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.DYH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.ZCS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.CH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.ZL);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.MJDW, "面积单位"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.SZC);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.QSC);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.ZZC);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.HH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.SHBW);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.HX, "户型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.HXJG, "户型结构"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.GHYT, "房屋用途"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.FWYT1, "房屋用途"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.FWYT2, "房屋用途"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.FWYT3, "房屋用途"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.YCJZMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.YCTNJZMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.YCFTJZMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.YCDXBFJZMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.YCQTJZMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.YCFTXS, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.SCJZMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.SCTNJZMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.SCFTJZMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.SCDXBFJZMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.SCQTJZMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.SCFTXS, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.GYTDMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.FTTDMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolArith.SetNumbericFormat(Household.DYTDMJ, 2));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.FWLX, "房屋类型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.FWJG, "房屋结构"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.FWXZ, "房屋性质"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FDCJYJG);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolDate.GetSlashDate(Household.JGSJ));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.FWCB, "房屋产别"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.CQLY);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.QTGSD);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.QTGSN);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.QTGSX);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.QTGSB);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.TDSYQR);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FCFHT);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Household.ZT, "不动产单元状态"));

        }
        #endregion

        #region 权利人
        private void WriteObligee()
        {
            if (Obligees == null || Obligees.Count == 0)
                return;
            SetSheetIndex(4);
            int rowIndex = 2;
            InsertRowsCell(rowIndex, Obligees.Count-1);
            foreach (var Obligee in Obligees)
            {
                InitalizeObligeeValue(Obligee, rowIndex);
                rowIndex++;

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
            }
        }
        private void InitalizeObligeeValue(Obligee Obligee, int rowIndex)
        {
            int columnIndex = 1;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.HBSM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.QLRMC);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.BDCQZH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Obligee.ZJZL, "证件类型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.ZJH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Obligee.GJ, "国家和地区"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Obligee.XB, "性别"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Obligee.QLRLX, "权利人类型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.DH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.YB);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.DZ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Obligee.QLLX, "权利类型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Obligee.GYFS, "共有方式"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.QLMJ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.QLBL);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.FRXM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Obligee.FRZJLX, "证件类型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.FRZJH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.FRDH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.DLRXM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Obligee.DLRZJLX, "证件类型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.DLRZJH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.DLRDH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.GZDW);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.DLJGMC);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.DZYJ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.BZ);
        }
        #endregion

        #region 抵押信息
        private void WriteMortgage()
        {
            if (Mortgages == null || Mortgages.Count == 0)
                return;
            SetSheetIndex(5);
            int rowIndex = 2;
            InsertRowsCell(rowIndex, Mortgages.Count-1);
            foreach (var Mortgage in Mortgages)
            {
                InitalizeMortgageValue(Mortgage, rowIndex);
                rowIndex++;

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
            }
        }
        private void InitalizeMortgageValue(Mortgage Mortgage, int rowIndex)
        {
            int columnIndex = 1;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.HBSM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.QLRMC);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.BDCQZH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Mortgage.ZJLX, "证件类型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.ZJH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.DH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.YB);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.DZ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.FRXM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Mortgage.FRZJLX, "证件类型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.FRZJH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.FRDH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.BZ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Mortgage.DYFS, "抵押方式"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Mortgage.ZQDW, "金额单位"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.DYR);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Mortgage.DYRZJLX, "证件类型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.DYRZJH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Mortgage.DYBDCLX, "抵押不动产类型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Mortgage.CZFS, "持证方式"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.DYPGJZ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.BDBZZQSE);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.ZGZQSE);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.ZWR);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.ZGZQQDSS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.ZJJZWZL);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.ZJJZWDYFW);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolDate.GetSlashDate(Mortgage.ZWLXQSSJ));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolDate.GetSlashDate(Mortgage.ZWLXJSSJ));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolDate.GetSlashDate(Mortgage.DJSJ));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.DBR);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Mortgage.FJ);

        }
        #endregion

        #region 查封信息
        private void WriteSequestration()
        {
            if (Sequestrations == null || Sequestrations.Count == 0)
                return;
            SetSheetIndex(6);
            int rowIndex = 2;
            InsertRowsCell(rowIndex, Sequestrations.Count-1);
            foreach (var Sequestration in Sequestrations)
            {
                InitalizeSequestrationValue(Sequestration, rowIndex);
                rowIndex++;

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
            }
        }
        private void InitalizeSequestrationValue(Sequestration Sequestration, int rowIndex)
        {
            int columnIndex = 1;
            InitalizeRangeInformation(rowIndex, columnIndex, Sequestration.HBSM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Sequestration.CFJG);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, DictionaryUtil.GetStringByKeyAndDic(Sequestration.CFLX, "查封类型类型"));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Sequestration.CFWJ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Sequestration.CFWH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Sequestration.JFWJ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Sequestration.JFWH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolDate.GetSlashDate(Sequestration.CFQSSJ));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolDate.GetSlashDate(Sequestration.CFJSSJ));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolDate.GetSlashDate(Sequestration.DJSJ));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Sequestration.DBR);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Sequestration.LHCX);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, ToolDate.GetSlashDate(Sequestration.CFSJ));
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Sequestration.FJ);

        }
        #endregion

        private void InsertRowsCell(int startRowIndex, int totalRow)
        {
            if (workSheet == null)
            {
                return;
            }
            try
            {
                workSheet.Cells.InsertRows(Math.Abs(startRowIndex), Math.Abs(totalRow));
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}
