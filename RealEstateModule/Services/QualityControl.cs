using BusinessData;
using Common.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateModule.Services
{
    public class QualityControl
    {
        public Project Project { get; set; }
        public List<string> ErrorMsg { get; set; }

        public QualityControl()
        {
            ErrorMsg = new List<string>();
        }
        /// <summary>
        /// 质检
        /// </summary>
        /// <returns></returns>
        public List<string> Check()
        {
            CheckNaturalBuilding();
            CheckLogicalBuilding();
            CheckFloor();
            CheckHousehold();
            CheckObligee();

            return ErrorMsg;
        }

        /// <summary>
        /// 自然幢质检
        /// </summary>
        public void CheckNaturalBuilding()
        {
            if (Project.NaturalBuildings == null) return;
            String table = "自然幢";
            HashSet<String> ZRZHSet = new HashSet<string>();
            HashSet<String> BSMSet = new HashSet<string>();
            int index = 0;
            foreach (var NaturalBuilding in Project.NaturalBuildings)
            {
                index++;
                // 重复检查
                if (!ZRZHSet.Add(NaturalBuilding.ZRZH))
                {
                    ErrorMsg.Add("自然幢表中，自然幢号【" + NaturalBuilding.ZRZH + "】重复存在");
                }
                if (!BSMSet.Add(NaturalBuilding.BSM))
                {
                    ErrorMsg.Add("自然幢表中，标识码【" + NaturalBuilding.BSM + "】重复存在");
                }
                // 表内检查
                CheckNull(table, index, NaturalBuilding.BSM, "标识码");
                CheckNull(table, index, NaturalBuilding.YSDM, "要素代码");
                CheckBDCDYH(table, index, NaturalBuilding.BDCDYH, "不动产单元号");
                CheckZDDM(table, index, NaturalBuilding.ZDDM, "宗地代码");
                CheckNull(table, index, NaturalBuilding.ZRZH, "自然幢号");
                CheckNumic(table, index, NaturalBuilding.ZYDMJ, "幢用地面积");
                CheckNumic(table, index, NaturalBuilding.ZZDMJ, "幢占地面积");
                CheckNumic(table, index, NaturalBuilding.DSCS, "地上层数");
                CheckNumic(table, index, NaturalBuilding.ZCS, "总层数");
                CheckNumic(table, index, NaturalBuilding.DXCS, "地下层数");
                CheckNull(table, index, NaturalBuilding.FWJG, "房屋结构");
                CheckNumic(table, index, NaturalBuilding.ZTS, "总套数");
                CheckNUllAndNumic(table, index, NaturalBuilding.JZWGD, "建筑物高度");
                CheckNUllAndNumic(table, index, NaturalBuilding.SCJZMJ, "实测建筑面积");
                CheckNUllAndNumic(table, index, NaturalBuilding.YCJZMJ, "预测建筑面积");
                CheckNUllAndNumic(table, index, NaturalBuilding.DXSD, "地下深度");
                // 表间检查
                //if (Project.LogicalBuildings == null || Project.LogicalBuildings.Count == 0)
                //{
                //}
                //else
                //{
                //    int count = Project.LogicalBuildings.Count(l => l.ZRZH == NaturalBuilding.BSM);
                //    if (count < 1)
                //    {
                //        ErrorMsg.Add("自然幢表中，标识码【" + NaturalBuilding.BSM + "】没有对应的逻辑幢");
                //    }
                //}
            }
        }


        /// <summary>
        /// 逻辑幢质检
        /// </summary>
        public void CheckLogicalBuilding()
        {
            if (Project.LogicalBuildings == null || Project.LogicalBuildings.Count == 0) return;
            String table = "逻辑幢";
            HashSet<String> LJZHSet = new HashSet<string>();
            int index = 0;
            foreach (var LogicalBuilding in Project.LogicalBuildings)
            {
                index++;
                // 重复检查
                if (!LJZHSet.Add(LogicalBuilding.LJZH))
                {
                    ErrorMsg.Add("逻辑幢表中，逻辑幢号【" + LogicalBuilding.LJZH + "】重复存在");
                }
                // 表内检查
                CheckNull(table, index, LogicalBuilding.LJZH, "逻辑幢号");
                CheckNull(table, index, LogicalBuilding.YSDM, "要素代码");
                CheckNull(table, index, LogicalBuilding.ZRZH, "自然幢号");
                CheckNUllAndNumic(table, index, LogicalBuilding.YCJZMJ, "预测建筑面积");
                CheckNUllAndNumic(table, index, LogicalBuilding.YCDXMJ, "预测地下面积");
                CheckNUllAndNumic(table, index, LogicalBuilding.YCQTMJ, "预测其它面积");
                CheckNUllAndNumic(table, index, LogicalBuilding.SCJZMJ, "实测建筑面积");
                CheckNUllAndNumic(table, index, LogicalBuilding.SCDXMJ, "实测地下面积");
                CheckNUllAndNumic(table, index, LogicalBuilding.SCQTMJ, "实测其它面积");
                CheckNUllAndInt(table, index, LogicalBuilding.ZCS, "总层数");
                CheckNUllAndInt(table, index, LogicalBuilding.DSCS, "地上层数");
                CheckNUllAndInt(table, index, LogicalBuilding.DXCS, "地下层数");
                // 表间检查
                int count = Project.NaturalBuildings.Count(n=>LogicalBuilding.ZRZH.Equals(n.BSM));
                if (count < 1)
                {
                    ErrorMsg.Add("逻辑幢表中，自然幢号【" + LogicalBuilding.ZRZH + "】没有对应的自然幢");
                }
            }
        }

        /// <summary>
        /// 层质检
        /// </summary>
        public void CheckFloor()
        {
            if (Project.Floors == null || Project.Floors.Count == 0) return;
            String table = "层";
            HashSet<String> CHSet = new HashSet<string>();
            int index = 0;
            foreach (var Floor in Project.Floors)
            {
                index++;
                if (!CHSet.Add(Floor.CH))
                {
                    ErrorMsg.Add("层表中，层号【" + Floor.CH + "】重复存在");
                }
                CheckNull(table, index, Floor.CH, "层号");
                CheckNull(table, index, Floor.YSDM, "要素代码");
                CheckNull(table, index, Floor.ZRZH, "自然幢号");
                CheckNUllAndInt(table, index, Floor.SJC, "实际层");
                CheckNUllAndInt(table, index, Floor.MYC, "名义层");
                CheckNUllAndNumic(table, index, Floor.CJZMJ, "层建筑面积");
                CheckNUllAndNumic(table, index, Floor.CTNJZMJ, "层套内建筑面积");
                CheckNUllAndNumic(table, index, Floor.CYTMJ, "层阳台面积");
                CheckNUllAndNumic(table, index, Floor.CGYJZMJ, "层共有建筑面积");
                CheckNUllAndNumic(table, index, Floor.CFTJZMJ, "层分摊建筑面积");
                CheckNUllAndNumic(table, index, Floor.CBQMJ, "层半墙面积");
                CheckNUllAndNumic(table, index, Floor.CG, "层高");
                CheckNUllAndNumic(table, index, Floor.SPTYMJ, "水平投影面积");
                // 表间检查
                int count = Project.NaturalBuildings.Count(n => Floor.ZRZH.Equals(n.BSM));
                if (count < 1)
                {
                    ErrorMsg.Add("层表中，自然幢号【" + Floor.ZRZH + "】没有对应的自然幢");
                }
            }
        }

        /// <summary>
        /// 户质检
        /// </summary>
        public void CheckHousehold()
        {
            if (Project.Households == null || Project.Households.Count == 0) return;
            String table = "户";
            HashSet<String> HBSMSet = new HashSet<string>();
            int index = 0;
            foreach (var Household in Project.Households)
            {
                index++;
                if (!HBSMSet.Add(Household.HBSM))
                {
                    ErrorMsg.Add("户表中，户标识码【" + Household.HBSM + "】重复存在");
                }
                CheckNull(table, index, Household.HBSM, "户标识码");
                CheckBDCDYH(table, index, Household.BDCDYH, "不动产单元号");
                CheckNull(table, index, Household.FWBM, "房屋编码");
                CheckNull(table, index, Household.YSDM, "要素代码");
                CheckNull(table, index, Household.ZRZH, "自然幢号");
                CheckNull(table, index, Household.ZL, "坐落");
                CheckNumic(table, index, Household.SZC, "所在层");
                CheckNumic(table, index, Household.QSC, "起始层");
                CheckNull(table, index, Household.MJDW, "面积单位");
                CheckNull(table, index, Household.SHBW, "室号部位");
                CheckNumic(table, index, Household.ZZC, "终止层");
                CheckNUllAndInt(table, index, Household.ZCS, "总层数");
                CheckNUllAndNumic(table, index, Household.YCTNJZMJ, "预测套内建筑面积");
                CheckNUllAndNumic(table, index, Household.YCFTJZMJ, "预测分摊建筑面积");
                CheckNUllAndNumic(table, index, Household.YCJZMJ, "预测建筑面积");
                CheckNUllAndNumic(table, index, Household.YCQTJZMJ, "预测其它建筑面积");
                CheckNUllAndNumic(table, index, Household.YCFTXS, "预测分摊系数");
                CheckNUllAndNumic(table, index, Household.YCDXBFJZMJ, "预测地下部分建筑面积");
                CheckNUllAndNumic(table, index, Household.SCTNJZMJ, "实测套内建筑面积");
                CheckNUllAndNumic(table, index, Household.SCFTJZMJ, "实测分摊建筑面积");
                CheckNUllAndNumic(table, index, Household.SCJZMJ, "实测建筑面积");
                CheckNUllAndNumic(table, index, Household.SCQTJZMJ, "实测其它建筑面积");
                CheckNUllAndNumic(table, index, Household.SCFTXS, "实测分摊系数");
                CheckNUllAndNumic(table, index, Household.SCDXBFJZMJ, "实测地下部分建筑面积");
                CheckNUllAndNumic(table, index, Household.FTTDMJ, "分摊土地面积");
                CheckNUllAndNumic(table, index, Household.DYTDMJ, "独用土地面积");
                CheckNUllAndNumic(table, index, Household.GYTDMJ, "共有土地面积");
                CheckNUllAndNumic(table, index, Household.FDCJYJG, "房地产交易价格");
                // 表间检查
                int count = Project.NaturalBuildings.Count(n => Household.ZRZH.Equals(n.BSM));
                if (count < 1)
                {
                    ErrorMsg.Add("户表中，自然幢号【" + Household.ZRZH + "】没有对应的自然幢");
                }
                count = Project.Obligees.Count(o => Household.HBSM.Equals(o.HBSM));
                if (count < 1)
                {
                    ErrorMsg.Add("户表中，户标识码【" + Household.HBSM + "】没有对应的权利人");
                }
            }
        }

        /// <summary>
        /// 权利人质检
        /// </summary>
        public void CheckObligee()
        {
            if (Project.Obligees == null || Project.Obligees.Count == 0) return;
            String table = "户";
            HashSet<String> HBSMSet = new HashSet<string>();
            int index = 0;
            foreach (var Obligee in Project.Obligees)
            {
                index++;
                CheckNull(table, index, Obligee.HBSM, "户标识码");
                CheckNull(table, index, Obligee.QLRMC, "权利人名称");
                CheckNull(table, index, Obligee.ZJZL, "证件种类");
                CheckNull(table, index, Obligee.ZJH, "证件号");
                CheckNull(table, index, Obligee.GJ, "国家地区");
                CheckNull(table, index, Obligee.QLRLX, "权利人类型");
                CheckNull(table, index, Obligee.QLLX, "权利类型");
                CheckNull(table, index, Obligee.GYFS, "共有方式");
                CheckNUllAndInt(table, index, Obligee.DH, "电话");
                CheckNUllAndInt(table, index, Obligee.YB, "邮编");
                CheckNUllAndNumic(table, index, Obligee.QLMJ, "权利面积");
                CheckNUllAndNumic(table, index, Obligee.QLBL, "权利比例");
                CheckNUllAndInt(table, index, Obligee.FRDH, "法人电话");
                CheckNUllAndInt(table, index, Obligee.DLRDH, "代理人电话");
                // 表间检查
                int count = Project.Households.Count(h => Obligee.HBSM.Equals(h.HBSM));
                if (count < 1)
                {
                    ErrorMsg.Add("权利人表中，户标识码【" + Obligee.HBSM + "】没有对应的户");
                }
            }
        }

        /// <summary>
        /// 空值检查
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="rownum">行号</param>
        /// <param name="value">字段值</param>
        /// <param name="name">字段名</param>
        private void CheckNull(String table, int rownum, String value, String name)
        {
            if (!RuleHelper.IsNotEmpty(value))
            {
                ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "为空");
            }
        }
        /// <summary>
        /// 检查是否是数字
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rownum"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        private void CheckNumic(String table, int rownum, String value, String name)
        {
            if (!RuleHelper.IsNotEmpty(value))
            {
                ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "为空");
                return;
            }
            if (!RuleHelper.IsNumberic(value))
            {
                ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，应是数字");
            }
        }
        private void CheckNUllAndNumic(String table, int rownum, String value, String name)
        {
            if (RuleHelper.IsNotEmpty(value))
            {
                if (!RuleHelper.IsNumberic(value))
                {
                    ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，应是数字");
                }
            }

        }

        private void CheckNUllAndInt(String table, int rownum, String value, String name)
        {
            if (RuleHelper.IsNotEmpty(value))
            {
                if (!RuleHelper.IsInteger(value))
                {
                    ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，应是整数");
                }
            }

        }

        /// <summary>
        /// 检查不动产单元号
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rownum"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        private void CheckBDCDYH(String table, int rownum, String value, String name)
        {
            if (!RuleHelper.IsNotEmpty(value))
            {
                ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "为空");
                return;
            }
            if (!RuleHelper.IsNumberAndWord(value))
            {
                ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，应是数字或字母");
                return;
            }
            if (!RuleHelper.IsRequiredLength(value, 28))
            {
                ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，长度应是28位");
                return;
            }
        }
        /// <summary>
        /// 检查宗地代码
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rownum"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        private void CheckZDDM(String table, int rownum, String value, String name)
        {
            if (!RuleHelper.IsNotEmpty(value))
            {
                ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "为空");
                return;
            }
            if (!RuleHelper.IsNumberAndWord(value))
            {
                ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，应是数字或字母");
                return;
            }
            if (!RuleHelper.IsRequiredLength(value, 19))
            {
                ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，长度应是19位");
                return;
            }

        }
    }
}
