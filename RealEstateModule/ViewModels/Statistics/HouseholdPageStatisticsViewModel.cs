using BusinessData;
using Common.Enums;
using Common.Utils;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;

namespace RealEstateModule.ViewModels.Statistics
{
    public class HouseholdPageStatisticsViewModel : BindableBase, INavigationAware
    {
        #region Properties
        private Project project;
        public Project Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }
        private EMappingType mappingType;
        public EMappingType MappingType
        {
            get { return mappingType; }
            set { SetProperty(ref mappingType, value); }
        }

        private int count;
        /// <summary>
        /// 统计个数
        /// </summary>
        public int Count
        {
            get { return count; }
            set { SetProperty(ref count, value); }
        }
  
        private double ycjzmj;
        /// <summary>
        /// 预测建筑面积
        /// </summary>
        public double YCJZMJ
        {
            get { return ycjzmj; }
            set { SetProperty(ref ycjzmj, value); }
        }

        private double yctnjzmj;
        /// <summary>
        /// 预测套内建筑面积
        /// </summary>
        public double YCTNJZMJ
        {
            get { return yctnjzmj; }
            set { SetProperty(ref yctnjzmj, value); }
        }
        private double ycftjzmj;
        /// <summary>
        /// 预测分摊建筑面积
        /// </summary>
        public double YCFTJZMJ
        {
            get { return ycftjzmj; }
            set { SetProperty(ref ycftjzmj, value); }
        }
        private double ycdxbfjzmj;
        /// <summary>
        /// 预测地下部分建筑面积
        /// </summary>
        public double YCDXBFJZMJ
        {
            get { return ycdxbfjzmj; }
            set { SetProperty(ref ycdxbfjzmj, value); }
        }
        private double ycqtjzmj;
        /// <summary>
        /// 预测其它建筑面积
        /// </summary>
        public double YCQTJZMJ
        {
            get { return ycqtjzmj; }
            set { SetProperty(ref ycqtjzmj, value); }
        }

        private double scjzmj;
        /// <summary>
        /// 实测建筑面积
        /// </summary>
        public double SCJZMJ
        {
            get { return scjzmj; }
            set { SetProperty(ref scjzmj, value); }
        }
        private double sctnjzmj;
        /// <summary>
        /// 实测套内建筑面积
        /// </summary>
        public double SCTNJZMJ
        {
            get { return sctnjzmj; }
            set { SetProperty(ref sctnjzmj, value); }
        }
        private double scftjzmj;
        /// <summary>
        /// 实测分摊建筑面积
        /// </summary>
        public double SCFTJZMJ
        {
            get { return scftjzmj; }
            set { SetProperty(ref scftjzmj, value); }
        }
        private double scdxbfjzmj;
        /// <summary>
        /// 实测地下部分建筑面积
        /// </summary>
        public double SCDXBFJZMJ
        {
            get { return scdxbfjzmj; }
            set { SetProperty(ref scdxbfjzmj, value); }
        }
        private double scqtjzmj;
        /// <summary>
        /// 实测其它建筑面积
        /// </summary>
        public double SCQTJZMJ
        {
            get { return scqtjzmj; }
            set { SetProperty(ref scqtjzmj, value); }
        }
        #endregion

        public HouseholdPageStatisticsViewModel()
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 获取选中项目
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                Project = project;
                MappingType = (EMappingType)int.Parse(Project.MappingType);
            }

            // 初始统计数据
            InitialStatistics();
        }

        private void InitialStatistics()
        {
            Count = Project.Households.Count;

            if (MappingType == EMappingType.PredictiveMapping)
            {
                YCJZMJ = Math.Round(Project.Households.Sum(t => ToolArith.StringToDouble(t.YCJZMJ)));
                YCTNJZMJ = Math.Round(Project.Households.Sum(t => ToolArith.StringToDouble(t.YCTNJZMJ)));
                YCFTJZMJ = Math.Round(Project.Households.Sum(t => ToolArith.StringToDouble(t.YCFTJZMJ)));
                YCDXBFJZMJ = Math.Round(Project.Households.Sum(t => ToolArith.StringToDouble(t.YCDXBFJZMJ)));
                YCQTJZMJ = Math.Round(Project.Households.Sum(t => ToolArith.StringToDouble(t.YCQTJZMJ)));
            }
            if (MappingType == EMappingType.SurveyingMapping)
            {
                SCJZMJ = Math.Round(Project.Households.Sum(t => ToolArith.StringToDouble(t.SCJZMJ)));
                SCTNJZMJ = Math.Round(Project.Households.Sum(t => ToolArith.StringToDouble(t.SCTNJZMJ)));
                SCFTJZMJ = Math.Round(Project.Households.Sum(t => ToolArith.StringToDouble(t.SCFTJZMJ)));
                SCDXBFJZMJ = Math.Round(Project.Households.Sum(t => ToolArith.StringToDouble(t.SCDXBFJZMJ)));
                SCQTJZMJ = Math.Round(Project.Households.Sum(t => ToolArith.StringToDouble(t.SCQTJZMJ)));
            }
        }
    }
}
