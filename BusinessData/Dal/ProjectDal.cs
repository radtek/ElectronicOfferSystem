using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessData.Dal
{
    /// <summary>
    /// 项目信息数据访问层
    /// </summary>
    public class ProjectDal : BaseDal<Project>
    {
        /// <summary>
        /// 初始化楼盘表项目
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public Project InitialRealEstateProject(Project project)
        {
            if (project == null)
                return null;
            if (project.Type != 1)
                return project;
            NaturalBuildingDal naturalBuildingDal = new NaturalBuildingDal();
            LogicalBuildingDal logicalBuildingDal = new LogicalBuildingDal();
            FloorDal floorDal = new FloorDal();
            HouseholdDal householdDal = new HouseholdDal();
            ObligeeDal obligeeDal = new ObligeeDal();
            project.NaturalBuildings = naturalBuildingDal.GetListBy((n) => n.ProjectID == project.ID);
            project.LogicalBuildings = logicalBuildingDal.GetListBy((l) => l.ProjectID == project.ID);
            project.Floors = floorDal.GetListBy((f) => f.ProjectID == project.ID);
            project.Households = householdDal.GetListBy((h) => h.ProjectID == project.ID);
            project.Obligees = obligeeDal.GetListBy((o) => o.ProjectID == project.ID);

            return project;
        }
    }
}
