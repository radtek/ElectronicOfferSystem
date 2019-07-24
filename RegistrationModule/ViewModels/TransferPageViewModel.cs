using BusinessData;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;

namespace RegistrationModule.ViewModels
{
    class TransferPageViewModel : BindableBase, INavigationAware
    {
        IEventAggregator EA;

        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }

        public TransferPageViewModel(IEventAggregator ea)
        {
            EA = ea;
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 获取选中项目
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                Project = project;
            }

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                return project != null;
            }
            else
            {
                return true;
            }
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
