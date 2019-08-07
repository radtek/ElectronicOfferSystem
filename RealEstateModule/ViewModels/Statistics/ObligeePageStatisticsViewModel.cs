using BusinessData;
using Common.Enums;
using Common.Utils;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;

namespace RealEstateModule.ViewModels.Statistics
{
    public class ObligeePageStatisticsViewModel : BindableBase, INavigationAware
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
        private int male;
        /// <summary>
        /// 男性人数
        /// </summary>
        public int Male
        {
            get { return male; }
            set { SetProperty(ref male, value); }
        }
        private int female;
        /// <summary>
        /// 女性人数
        /// </summary>
        public int Female
        {
            get { return female; }
            set { SetProperty(ref female, value); }
        }

        #endregion

        public ObligeePageStatisticsViewModel()
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
            Count = Project.Obligees.Count;
            Male = Project.Obligees.Count(t=>t.XB == "1");
            Female = Project.Obligees.Count(t=>t.XB == "2");

        }
    }
}
