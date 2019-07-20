using Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicOfferSystem.ViewModels
{
    class ProjectTabViewModel : BindableBase
    {
        private readonly IRegionManager RegionManager;
        public DelegateCommand<string> NavigateCommand { get; set; }


        public ProjectTabViewModel(IRegionManager regionManager)
        {
            RegionManager = regionManager;

            // 页面导航
            NavigateCommand = new DelegateCommand<string>(Navigate);
            GlobalCommands.NavigateCommand.RegisterCommand(NavigateCommand);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                RegionManager.RequestNavigate("ContentRegion", navigatePath, NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            //System.Windows.MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
        }
    }
}
