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
            if ("1".Equals(project.Type))
            {
                project.NaturalBuildings = InitialNaturalBuildings(project);
                project.LogicalBuildings = InitialLogicalBuildings(project);
                project.Floors = InitialFloors(project);
                project.Households = InitialHouseholds(project);
                project.Obligees = InitialObligees(project);
                project.Mortgages = InitialMortgages(project);
                project.Sequestrations = InitialSequestrations(project);
            }
            return project;
        }

        public ICollection<NaturalBuilding> InitialNaturalBuildings(Project project)
        {
            NaturalBuildingDal naturalBuildingDal = new NaturalBuildingDal();
            return naturalBuildingDal.GetListBy((t) => t.ProjectID == project.ID);
        }

        public ICollection<LogicalBuilding> InitialLogicalBuildings(Project project)
        {
            LogicalBuildingDal logicalBuildingDal = new LogicalBuildingDal();
            return logicalBuildingDal.GetListBy((t) => t.ProjectID == project.ID);
        }

        public ICollection<Floor> InitialFloors(Project project)
        {
            FloorDal floorDal = new FloorDal();
            return floorDal.GetListBy((t) => t.ProjectID == project.ID);
        }

        public ICollection<Household> InitialHouseholds(Project project)
        {
            HouseholdDal householdDal = new HouseholdDal();
            return householdDal.GetListBy((t) => t.ProjectID == project.ID);
        }

        public ICollection<Obligee> InitialObligees(Project project)
        {
            ObligeeDal obligeeDal = new ObligeeDal();
            return obligeeDal.GetListBy((t) => t.ProjectID == project.ID);
        }

        public ICollection<Mortgage> InitialMortgages(Project project)
        {
            MortgageDal mortgageDal = new MortgageDal();
            return mortgageDal.GetListBy((t) => t.ProjectID == project.ID);
        }

        public ICollection<Sequestration> InitialSequestrations(Project project)
        {
            SequestrationDal sequestrationDal = new SequestrationDal();
            return sequestrationDal.GetListBy((t) => t.ProjectID == project.ID);
        }


        /// <summary>
        /// 初始化登记业务项目
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public Project InitialRegistrationProject(Project project)
        {
            if (project == null)
            {
                return null;
            }

            if ("2".Equals(project.Type))
            {
                ApplicantDal applicantDal = new ApplicantDal();
                TransferDal transferDal = new TransferDal();
                FileInfoDal fileInfoDal = new FileInfoDal();
                project.Applicants = applicantDal.GetListBy(a => a.ProjectID == project.ID);
                project.Transfer = transferDal.GetModel(t => t.ProjectID == project.ID);
                project.FileInfos = fileInfoDal.GetListBy(f => f.ProjectID == project.ID);
            }
            return project;
        }
    }
}
