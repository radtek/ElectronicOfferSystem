using BusinessData;
using BusinessData.Dal;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateModule.ViewModels
{
    class RealEstatePageViewModel : BindableBase
    {
        private ObservableCollection<object> businesses;
        public ObservableCollection<object> Businesses
        {
            get { return businesses; }
            set { SetProperty(ref businesses, value); }
        }

        private readonly IRegionManager RegionManager;
        public DelegateCommand<string> BusinessNavCommand { get; private set; }

        public RealEstatePageViewModel(IRegionManager regionManager)
        {
            NaturalBuildingDal naturalBuildingDal = new NaturalBuildingDal();
            Businesses = new ObservableCollection<object>(naturalBuildingDal.GetListBy((n) => n.ID != null));

            RegionManager = regionManager;
            BusinessNavCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                RegionManager.RequestNavigate("BusinessContentRegion", navigatePath, NavigationComplete);
        }
        private void NavigationComplete(NavigationResult result)
        {
            //System.Windows.MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
        }
    }
}
