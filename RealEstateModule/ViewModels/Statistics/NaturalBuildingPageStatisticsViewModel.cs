using BusinessData;
using Common.Enums;
using Common.Utils;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateModule.ViewModels.Statistics
{
    public class NaturalBuildingPageStatisticsViewModel : BindableBase, INavigationAware
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

        private double jzwgd;
        /// <summary>
        /// 建筑物高度
        /// </summary>
        public double JZWGD
        {
            get { return jzwgd; }
            set { SetProperty(ref jzwgd, value); }
        }

        private double zdmj;
        /// <summary>
        /// 占地面积
        /// </summary>
        public double ZDMJ
        {
            get { return zdmj; }
            set { SetProperty(ref zdmj, value); }
        }

        private double ydmj;
        /// <summary>
        /// 用地面积
        /// </summary>
        public double YDMJ
        {
            get { return ydmj; }
            set { SetProperty(ref ydmj, value); }
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

        private double scjzmj;
        /// <summary>
        /// 实测建筑面积
        /// </summary>
        public double SCJZMJ
        {
            get { return scjzmj; }
            set { SetProperty(ref scjzmj, value); }
        }

        #endregion

        public NaturalBuildingPageStatisticsViewModel()
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
            Count = Project.NaturalBuildings.Count;
            JZWGD = Math.Round(Project.NaturalBuildings.Sum(t => ToolArith.StringToDouble(t.JZWGD)));
            ZDMJ = Math.Round(Project.NaturalBuildings.Sum(t => ToolArith.StringToDouble(t.ZZDMJ)));
            YDMJ = Math.Round(Project.NaturalBuildings.Sum(t => ToolArith.StringToDouble(t.ZYDMJ)));
            YCJZMJ = Math.Round(Project.NaturalBuildings.Sum(t => ToolArith.StringToDouble(t.YCJZMJ)));
            SCJZMJ = Math.Round(Project.NaturalBuildings.Sum(t => ToolArith.StringToDouble(t.SCJZMJ)));
        }
    }
}
