using BusinessData;
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
        public List<string> ErrorMsg { get; set; }
        public List<NaturalBuilding> NaturalBuildings { get; set; }
        public List<Household> Households { get; set; }
        public List<Floor> Floors { get; set; }
        public List<LogicalBuilding> LogicalBuildings { get; set; }
        public List<Obligee> Obligees { get; set; }

        public ExportRealEstateBook()
        {
            ErrorMsg = new List<string>();
        }

        public override void Read()
        {

        }

        public override void Write()
        {
            try
            {
                WriteNaturalBuilding();
                WriteLogicalBuilding();
                WriteFloor();
                WriteHouse();
                WriteObligee();
            }
            catch (Exception ex)
            {
                ErrorMsg.Add(ex.Message);
            }
        }

        #region 自然幢
        private void WriteNaturalBuilding()
        {
            if (NaturalBuildings == null || NaturalBuildings.Count == 0)
                return;
            SetSheetIndex(0);
            int rowIndex = 2;
            foreach (var NaturalBuilding in NaturalBuildings)
            {
                InitalizeNaturalBuildingValue(NaturalBuilding, rowIndex);
                rowIndex++;
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
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.JGRQ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.JZWGD.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.ZZDMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.ZYDMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.YCJZMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.SCJZMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.ZCS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.DSCS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.DXCS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.DXSD.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.GHYT);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.FWJG);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.ZTS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.JZWJBYT);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.BZ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, NaturalBuilding.ZT);
        }
        #endregion

        #region 逻辑幢
        private void WriteLogicalBuilding()
        {
            if (LogicalBuildings == null || LogicalBuildings.Count == 0)
                return;
            SetSheetIndex(1);
            int rowIndex = 2;
            foreach (var LogicalBuilding in LogicalBuildings)
            {
                InitalizeNaturalValue(LogicalBuilding, rowIndex);
                rowIndex++;
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
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.YCJZMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.YCDXMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.YCQTMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.SCJZMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.SCDXMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, LogicalBuilding.SCQTMJ.ToString());
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
            foreach (var Floor in Floors)
            {
                InitalizeWriteFloorValue(Floor, rowIndex);
                rowIndex++;
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
            foreach (var Household in Households)
            {
                InitalizeHouselValue(Household, rowIndex);
                rowIndex++;
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
            InitalizeRangeInformation(rowIndex, columnIndex, Household.MJDW);
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
            InitalizeRangeInformation(rowIndex, columnIndex, Household.HX);
            columnIndex++;                                      
            InitalizeRangeInformation(rowIndex, columnIndex, Household.HXJG);
            columnIndex++;                                      
            InitalizeRangeInformation(rowIndex, columnIndex, Household.GHYT);
            columnIndex++;                                     
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FWYT1);
            columnIndex++;                                     
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FWYT2);
            columnIndex++;                                  
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FWYT3);
            columnIndex++;                                
            InitalizeRangeInformation(rowIndex, columnIndex, Household.YCJZMJ.ToString());
            columnIndex++;                                    
            InitalizeRangeInformation(rowIndex, columnIndex, Household.YCTNJZMJ.ToString());
            columnIndex++;                                
            InitalizeRangeInformation(rowIndex, columnIndex, Household.YCFTJZMJ.ToString());
            columnIndex++;                                  
            InitalizeRangeInformation(rowIndex, columnIndex, Household.YCDXBFJZMJ.ToString());
            columnIndex++;                                
            InitalizeRangeInformation(rowIndex, columnIndex, Household.YCQTJZMJ.ToString());
            columnIndex++;                                 
            InitalizeRangeInformation(rowIndex, columnIndex, Household.YCFTXS);
            columnIndex++;                                   
            InitalizeRangeInformation(rowIndex, columnIndex, Household.SCJZMJ.ToString());
            columnIndex++;                                    
            InitalizeRangeInformation(rowIndex, columnIndex, Household.SCTNJZMJ.ToString());
            columnIndex++;                                    
            InitalizeRangeInformation(rowIndex, columnIndex, Household.SCFTJZMJ.ToString());
            columnIndex++;                                
            InitalizeRangeInformation(rowIndex, columnIndex, Household.SCDXBFJZMJ.ToString());
            columnIndex++;                                   
            InitalizeRangeInformation(rowIndex, columnIndex, Household.SCQTJZMJ.ToString());
            columnIndex++;                               
            InitalizeRangeInformation(rowIndex, columnIndex, Household.SCFTXS.ToString());
            columnIndex++;                                
            InitalizeRangeInformation(rowIndex, columnIndex, Household.GYTDMJ.ToString());
            columnIndex++;                               
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FTTDMJ.ToString());
            columnIndex++;                                  
            InitalizeRangeInformation(rowIndex, columnIndex, Household.DYTDMJ.ToString());
            columnIndex++;                                 
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FWLX);
            columnIndex++;                                     
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FWJG);
            columnIndex++;                                  
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FWXZ);
            columnIndex++;                                  
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FDCJYJG);
            columnIndex++;                                
            InitalizeRangeInformation(rowIndex, columnIndex, Household.JGSJ.ToString());
            columnIndex++;                           
            InitalizeRangeInformation(rowIndex, columnIndex, Household.FWCB);
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
            InitalizeRangeInformation(rowIndex, columnIndex, Household.ZT);

        }
        #endregion

        #region 权利人
        private void WriteObligee()
        {
            if (Obligees == null || Obligees.Count == 0)
                return;
            SetSheetIndex(4);
            int rowIndex = 2;
            foreach (var Obligee in Obligees)
            {
                InitalizeObligeeValue(Obligee, rowIndex);
                rowIndex++;
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
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.ZJZL);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.ZJH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.GJ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.XB);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.QLRLX);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.DH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.YB);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.DZ);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.QLLX);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.GYFS);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.QLMJ.ToString());
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.QLBL);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.FRXM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.FRZJLX);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.FRZJH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.FRDH);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.DLRXM);
            columnIndex++;
            InitalizeRangeInformation(rowIndex, columnIndex, Obligee.DLRZJLX);
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
    }
}
