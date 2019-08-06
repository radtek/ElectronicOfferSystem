using BusinessData;
using Common;
using Common.Enums;
using Common.Models;
using Common.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateModule.Services
{
    public class QualityControl
    {
        public Project Project { get; set; }
        public ObservableCollection<string> ErrorMsg { get; set; }
        public TaskMessage TaskMessage { get; set; }
        public int TotalCount { get; set; }
        public double index { get; set; }
        public EMappingType ProjectMappingType { get; set; }
        public EOwnershipType ProjectOwnershipType { get; set; }

        public QualityControl()
        {
            ErrorMsg = new ObservableCollection<string>();
        }
        /// <summary>
        /// 质检
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> Check()
        {
            TotalCount = 0;
            index = 0.0;

            // 获取总共的条数
            if (Project.NaturalBuildings != null)
                TotalCount += Project.NaturalBuildings.Count;
            if (Project.LogicalBuildings != null)
                TotalCount += Project.LogicalBuildings.Count;
            if (Project.Floors != null)
                TotalCount += Project.Floors.Count;
            if (Project.Households != null)
                TotalCount += Project.Households.Count;
            if (Project.Obligees != null)
                TotalCount += Project.Obligees.Count;
            if (Project.Mortgages != null)
                TotalCount += Project.Mortgages.Count;
            if (Project.Sequestrations != null)
                TotalCount += Project.Sequestrations.Count;
            if (TotalCount == 0)
            {
                ErrorMsg.Add("该项目无数据");
                return ErrorMsg;
            }

            ProjectMappingType = (EMappingType)int.Parse(Project.MappingType);
            ProjectOwnershipType = (EOwnershipType)int.Parse(Project.OwnershipType);

            CheckNaturalBuilding();
            CheckLogicalBuilding();
            CheckFloor();
            CheckHousehold();
            CheckObligee();

            if (ProjectOwnershipType == EOwnershipType.OwnershipMakeup)
            {
                CheckMortgage();
                CheckSequestration();
            }

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
            foreach (var NaturalBuilding in Project.NaturalBuildings)
            {
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
                string id = "自然幢号为【" + NaturalBuilding.ZRZH +"】";
                // 特殊字段检查
                CheckBDCDYH(table, id, NaturalBuilding.BDCDYH, "不动产单元号");
                CheckZDDM(table, id, NaturalBuilding.ZDDM, "宗地代码");
                // 非空检查
                CheckNull(table, id, NaturalBuilding.BSM, "标识码");
                CheckNull(table, id, NaturalBuilding.YSDM, "要素代码");
                CheckNull(table, id, NaturalBuilding.ZRZH, "自然幢号");
                CheckNull(table, id, NaturalBuilding.GHYT, "规划用途");
                CheckNull(table, id, NaturalBuilding.FWJG, "房屋结构");
                CheckNull(table, id, NaturalBuilding.ZT, "状态");
                // 非空和数字检查
                CheckNUllAndNumic(table, id, NaturalBuilding.ZYDMJ, "幢用地面积");
                CheckNUllAndNumic(table, id, NaturalBuilding.ZZDMJ, "幢占地面积");
                // 非空和整数检查
                CheckNUllAndInt(table, id, NaturalBuilding.DSCS, "地上层数");
                CheckNUllAndInt(table, id, NaturalBuilding.ZCS, "总层数");
                CheckNUllAndInt(table, id, NaturalBuilding.DXCS, "地下层数");
                CheckNUllAndInt(table, id, NaturalBuilding.ZTS, "总套数");
                // 数字检查
                CheckNumic(table, id, NaturalBuilding.JZWGD, "建筑物高度");

                if (ProjectMappingType == EMappingType.PredictiveMapping) // 若是预测绘项目
                {
                    CheckNUllAndNumic(table, id, NaturalBuilding.YCJZMJ, "预测建筑面积");
                }
                else if (ProjectMappingType == EMappingType.SurveyingMapping) // 若是实测绘项目
                {
                    CheckNUllAndNumic(table, id, NaturalBuilding.SCJZMJ, "实测建筑面积");
                }

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
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
            foreach (var LogicalBuilding in Project.LogicalBuildings)
            {
                // 重复检查
                //if (!LJZHSet.Add(LogicalBuilding.LJZH))
                //{
                //    ErrorMsg.Add("逻辑幢表中，逻辑幢号【" + LogicalBuilding.LJZH + "】重复存在");
                //}
                // 表内检查 
                string id = "逻辑幢号为【" + LogicalBuilding.LJZH + "】";
                CheckNull(table, id, LogicalBuilding.LJZH, "逻辑幢号");
                CheckNull(table, id, LogicalBuilding.YSDM, "要素代码");
                CheckNull(table, id, LogicalBuilding.ZRZH, "自然幢号");
                CheckNumic(table, id, LogicalBuilding.YCJZMJ, "预测建筑面积");
                CheckNumic(table, id, LogicalBuilding.YCDXMJ, "预测地下面积");
                CheckNumic(table, id, LogicalBuilding.YCQTMJ, "预测其它面积");
                CheckNumic(table, id, LogicalBuilding.SCJZMJ, "实测建筑面积");
                CheckNumic(table, id, LogicalBuilding.SCDXMJ, "实测地下面积");
                CheckNumic(table, id, LogicalBuilding.SCQTMJ, "实测其它面积");
                CheckInt(table, id, LogicalBuilding.ZCS, "总层数");
                CheckInt(table, id, LogicalBuilding.DSCS, "地上层数");
                CheckInt(table, id, LogicalBuilding.DXCS, "地下层数");
                // 表间检查
                int count = Project.NaturalBuildings.Count(n=>LogicalBuilding.ZRZH.Equals(n.ZRZH));
                if (count < 1)
                {
                    ErrorMsg.Add("逻辑幢表中，自然幢号【" + LogicalBuilding.ZRZH + "】没有对应的自然幢");
                }

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
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
            foreach (var Floor in Project.Floors)
            {
                //if (!CHSet.Add(Floor.CH))
                //{
                //    ErrorMsg.Add("层表中，层号【" + Floor.CH + "】重复存在");
                //}
                string id = "层号为【" + Floor.CH + "】";
                CheckNull(table, id, Floor.CH, "层号");
                CheckNull(table, id, Floor.YSDM, "要素代码");
                CheckNull(table, id, Floor.ZRZH, "自然幢号");
                CheckNumic(table, id, Floor.CJZMJ, "层建筑面积");
                CheckNumic(table, id, Floor.CTNJZMJ, "层套内建筑面积");
                CheckNumic(table, id, Floor.CYTMJ, "层阳台面积");
                CheckNumic(table, id, Floor.CGYJZMJ, "层共有建筑面积");
                CheckNumic(table, id, Floor.CFTJZMJ, "层分摊建筑面积");
                CheckNumic(table, id, Floor.CBQMJ, "层半墙面积");
                CheckNumic(table, id, Floor.CG, "层高");
                CheckNumic(table, id, Floor.SPTYMJ, "水平投影面积");
                // 表间检查
                int count = Project.NaturalBuildings.Count(n => Floor.ZRZH.Equals(n.ZRZH));
                if (count < 1)
                {
                    ErrorMsg.Add("层表中，自然幢号【" + Floor.ZRZH + "】没有对应的自然幢");
                }

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
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
            HashSet<String> FHSet = new HashSet<string>();
            foreach (var Household in Project.Households)
            {
                if (!HBSMSet.Add(Household.HBSM))
                {
                    ErrorMsg.Add("户表中，户标识码【" + Household.HBSM + "】重复存在");
                }
                if (!FHSet.Add(Household.FH))
                {
                    ErrorMsg.Add("户表中，房号【" + Household.FH + "】重复存在");
                }
                string id = "户标识码为【" + Household.HBSM + "】";
                // 特殊检查
                CheckBDCDYH(table, id, Household.BDCDYH, "不动产单元号");
                // 非空检查
                CheckNull(table, id, Household.HBSM, "户标识码");
                CheckNull(table, id, Household.FWBM, "房屋编码");
                CheckNull(table, id, Household.YSDM, "要素代码");
                CheckNull(table, id, Household.ZL, "坐落");
                CheckNull(table, id, Household.MJDW, "面积单位");
                CheckNull(table, id, Household.SHBW, "室号部位");
                CheckNull(table, id, Household.SZC, "所在层");
                CheckNull(table, id, Household.QSC, "起始层");
                CheckNull(table, id, Household.ZZC, "终止层");
                CheckNull(table, id, Household.ZT, "状态");

                // 数字检查
                CheckNumic(table, id, Household.FTTDMJ, "分摊土地面积");
                CheckNumic(table, id, Household.DYTDMJ, "独用土地面积");
                CheckNumic(table, id, Household.GYTDMJ, "共有土地面积");

                if (ProjectMappingType == EMappingType.PredictiveMapping) // 若是预测绘项目
                {
                    CheckNUllAndNumic(table, id, Household.YCTNJZMJ, "预测套内建筑面积");
                    CheckNUllAndNumic(table, id, Household.YCFTJZMJ, "预测分摊建筑面积");
                    CheckNUllAndNumic(table, id, Household.YCJZMJ, "预测建筑面积");
                    CheckNUllAndNumic(table, id, Household.YCQTJZMJ, "预测其它建筑面积");
                    CheckNUllAndNumic(table, id, Household.YCDXBFJZMJ, "预测地下部分建筑面积");

                    CheckNumic(table, id, Household.YCFTXS, "预测分摊系数");
                }
                else if (ProjectMappingType == EMappingType.SurveyingMapping) // 若是实测绘项目
                {
                    CheckNUllAndNumic(table, id, Household.SCTNJZMJ, "实测套内建筑面积");
                    CheckNUllAndNumic(table, id, Household.SCFTJZMJ, "实测分摊建筑面积");
                    CheckNUllAndNumic(table, id, Household.SCJZMJ, "实测建筑面积");
                    CheckNUllAndNumic(table, id, Household.SCQTJZMJ, "实测其它建筑面积");
                    CheckNUllAndNumic(table, id, Household.SCDXBFJZMJ, "实测地下部分建筑面积");

                    CheckNumic(table, id, Household.SCFTXS, "实测分摊系数");
                }

                // 表间检查
                int count = 0;
                if (Household.ZRZH != null)
                {
                    count = Project.NaturalBuildings.Count(n => Household.ZRZH.Equals(n.ZRZH));
                    if (count < 1)
                    {
                        ErrorMsg.Add("户表中，自然幢号【" + Household.ZRZH + "】没有对应的自然幢");
                    }
                }

                if (ProjectOwnershipType == EOwnershipType.OwnershipSurvey) // 若是权籍调查
                {

                }
                else if (ProjectOwnershipType == EOwnershipType.OwnershipMakeup) // 若是权籍补录
                {
                    count = Project.Obligees.Count(o => Household.HBSM.Equals(o.HBSM));
                    if (count < 1)
                    {
                        ErrorMsg.Add("户表中，户标识码【" + Household.HBSM + "】没有对应的权利人");
                    }
                }

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
            }
        }

        /// <summary>
        /// 权利人质检
        /// </summary>
        public void CheckObligee()
        {
            if (Project.Obligees == null || Project.Obligees.Count == 0) return;
            String table = "权利人";
            HashSet<String> ZJHSet = new HashSet<string>();
            foreach (var Obligee in Project.Obligees)
            {
                if (!ZJHSet.Add(Obligee.ZJH))
                {
                    ErrorMsg.Add("权利人表中，证件号【" + Obligee.ZJH + "】重复存在");
                }
                string id = "证件号为【" + Obligee.ZJH + "】";
                CheckNull(table, id, Obligee.HBSM, "户标识码");
                CheckNull(table, id, Obligee.QLRMC, "权利人名称");
                CheckNull(table, id, Obligee.ZJZL, "证件种类");
                CheckNull(table, id, Obligee.ZJH, "证件号");
                CheckNull(table, id, Obligee.GJ, "国家地区");
                CheckNull(table, id, Obligee.XB, "性别");
                CheckNull(table, id, Obligee.QLRLX, "权利人类型");
                CheckNull(table, id, Obligee.QLLX, "权利类型");
                CheckNull(table, id, Obligee.GYFS, "共有方式");

                CheckNumic(table, id, Obligee.QLMJ, "权利面积");

                if (!string.IsNullOrWhiteSpace(Obligee.FRXM)) // 若法人姓名不为空
                {
                    CheckNull(table, id, Obligee.FRZJLX, "法人证件类型");
                    CheckNull(table, id, Obligee.FRZJH, "法人证件号");
                }
                if (!string.IsNullOrWhiteSpace(Obligee.DLRXM)) // 若代理人姓名不为空
                {
                    CheckNull(table, id, Obligee.DLRZJLX, "代理人证件类型");
                    CheckNull(table, id, Obligee.DLRZJH, "代理人证件号");
                }

                // 表间检查
                int count = Project.Households.Count(h => Obligee.HBSM.Equals(h.HBSM));
                if (count < 1)
                {
                    ErrorMsg.Add("权利人表中，户标识码【" + Obligee.HBSM + "】没有对应的户");
                }

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
            }
        }
        /// <summary>
        /// 抵押信息质检
        /// </summary>
        public void CheckMortgage()
        {
            if (Project.Mortgages == null || Project.Mortgages.Count == 0) return;
            String table = "抵押信息";
            foreach (var Mortgage in Project.Mortgages)
            {
                string id = "证件号为【" + Mortgage.ZJH + "】";
                CheckNull(table, id, Mortgage.HBSM, "户标识码");
                CheckNull(table, id, Mortgage.QLRMC, "权利人名称");
                CheckNull(table, id, Mortgage.ZJLX, "证件类型");
                CheckNull(table, id, Mortgage.ZJH, "证件号");
                CheckNull(table, id, Mortgage.DYFS, "抵押方式");
                CheckNull(table, id, Mortgage.DYR, "抵押人");
                CheckNull(table, id, Mortgage.DYRZJLX, "抵押人证件类型");
                CheckNull(table, id, Mortgage.DYRZJH, "抵押人证件号");
                CheckNull(table, id, Mortgage.DYBDCLX, "抵押不动产类型");
                CheckNull(table, id, Mortgage.CZFS, "持证方式");
                CheckNull(table, id, Mortgage.ZWR, "债务人");
                CheckNull(table, id, Mortgage.DBR, "登簿人");

                if (!string.IsNullOrWhiteSpace(Mortgage.FRXM)) // 若法人姓名不为空
                {
                    CheckNull(table, id, Mortgage.FRZJLX, "法人证件类型");
                    CheckNull(table, id, Mortgage.FRZJH, "法人证件号");
                }

                // 表间检查
                int count = Project.Mortgages.Count(m => Mortgage.HBSM.Equals(m.HBSM));
                if (count < 1)
                {
                    ErrorMsg.Add("抵押信息表中，户标识码【" + Mortgage.HBSM + "】没有对应的户");
                }

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
            }
        }
        /// <summary>
        /// 查封信息质检
        /// </summary>
        public void CheckSequestration()
        {
            if (Project.Sequestrations == null || Project.Sequestrations.Count == 0) return;
            String table = "查封信息";
            foreach (var Sequestration in Project.Sequestrations)
            {
                string id = "户标识码为【" + Sequestration.HBSM + "】";
                CheckNull(table, id, Sequestration.HBSM, "户标识码");
                CheckNull(table, id, Sequestration.CFLX, "查封类型");
                CheckNull(table, id, Sequestration.DBR, "登簿人");
                CheckNull(table, id, Sequestration.CFSJ.ToString(), "查封时间");

                // 表间检查
                int count = Project.Sequestrations.Count(s => Sequestration.HBSM.Equals(s.HBSM));
                if (count < 1)
                {
                    ErrorMsg.Add("查封信息表中，户标识码【" + Sequestration.HBSM + "】没有对应的户");
                }

                // 报告进度
                index++;
                TaskMessage.Progress = index / TotalCount * 100;
            }
        }

        /// <summary>
        /// 空值检查
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="id">主键</param>
        /// <param name="value">字段值</param>
        /// <param name="name">字段名</param>
        private void CheckNull(String table, String id, String value, String name)
        {
            if (!RuleHelper.IsNotEmpty(value))
            {
                //ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "为空");
                ErrorMsg.Add(table + "表中，"+ id +"的" + name + "为空");
            }
        }
        /// <summary>
        /// 检查是否是数字
        /// </summary>
        /// <param name="table"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        private void CheckNumic(String table, String id, String value, String name)
        {
            if (RuleHelper.IsNotEmpty(value))
            {
                if (!RuleHelper.IsNumberic(value))
                {
                    ErrorMsg.Add(table + "表中，" + id + "的" + name + "格式错误，应是数字");
                }
            }
        }
        private void CheckNUllAndNumic(String table, String id, String value, String name)
        {
            if (!RuleHelper.IsNotEmpty(value))
            {
                //ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "为空");
                ErrorMsg.Add(table + "表中，" + id + "的" + name + "为空");
                return;
            }
            if (!RuleHelper.IsNumberic(value))
            {
                ErrorMsg.Add(table + "表中，" + id + "的" + name + "格式错误，应是数字");
                //ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，应是数字");
            }
        }

        private void CheckInt(String table, String id, String value, String name)
        {
            if (RuleHelper.IsNotEmpty(value))
            {
                if (!RuleHelper.IsInteger(value))
                {
                    ErrorMsg.Add(table + "表中，" + id + "的" + name + "格式错误，应是整数");
                }
            }
        }

        private void CheckNUllAndInt(String table, String id, String value, String name)
        {
            if (!RuleHelper.IsNotEmpty(value))
            {
                ErrorMsg.Add(table + "表中，" + id + "的" + name + "为空");
                return;
            }
            if (!RuleHelper.IsInteger(value))
            {
                ErrorMsg.Add(table + "表中，" + id + "的" + name + "格式错误，应是整数");
            }
        }

        /// <summary>
        /// 检查不动产单元号
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rownum"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        private void CheckBDCDYH(String table, String id, String value, String name)
        {
            if (RuleHelper.IsNotEmpty(value)) // 可以为空
            {
                if (!RuleHelper.IsNumberAndWord(value))
                {
                    ErrorMsg.Add(table + "表中，" + id + "的" + name + "格式错误，应是数字或字母");
                    //ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，应是数字或字母");
                    return;
                }
                if (!RuleHelper.IsRequiredLength(value, 28))
                {
                    ErrorMsg.Add(table + "表中，" + id + "的" + name + "格式错误，长度应是28位");
                    //ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，长度应是28位");
                    return;
                }
            }
        }
        /// <summary>
        /// 检查宗地代码
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rownum"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        private void CheckZDDM(String table, String id, String value, String name)
        {
            if (!RuleHelper.IsNotEmpty(value))
            {
                ErrorMsg.Add(table + "表中，" + id + "的" + name + "为空");
                //ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "为空");
                return;
            }
            if (!RuleHelper.IsNumberAndWord(value))
            {
                ErrorMsg.Add(table + "表中，" + id + "的" + name + "格式错误，应是数字或字母");
                //ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，应是数字或字母");
                return;
            }
            if (!RuleHelper.IsRequiredLength(value, 19))
            {
                ErrorMsg.Add(table + "表中，" + id + "的" + name + "格式错误，长度应是19位");
                //ErrorMsg.Add(table + "表中，第" + rownum + "行的" + name + "格式错误，长度应是19位");
                return;
            }

        }
    }
}
