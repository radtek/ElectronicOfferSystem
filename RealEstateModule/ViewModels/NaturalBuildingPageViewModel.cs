using BusinessData;
using BusinessData.Dal;
using Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RealEstateModule.ViewModels
{
    class NaturalBuildingPageViewModel : BindableBase, INavigationAware
    {
        public Guid ProjectID { get; set; }


        private NaturalBuilding naturalBuilding;
        public NaturalBuilding NaturalBuilding
        {
            get { return naturalBuilding; }
            set { SetProperty(ref naturalBuilding, value); }
        }

        public DelegateCommand AddOrEditNaturalBuildingCommand { get; set; }
        public DelegateCommand<object> SelectProjectCommand { get; set; }

        NaturalBuildingDal NaturalBuildingDal = new NaturalBuildingDal();

        public NaturalBuildingPageViewModel()
        {
            NaturalBuilding = new NaturalBuilding();

            AddOrEditNaturalBuildingCommand = new DelegateCommand(() => {
                
                NaturalBuilding.ProjectId = ProjectID;
                NaturalBuildingDal.Add(NaturalBuilding);

            });
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Guid? projectID = navigationContext.Parameters["ProjectID"] as Guid?;
            if (projectID != null)
            {
                ProjectID = (Guid)projectID;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            Guid? projectID = navigationContext.Parameters["ProjectID"] as Guid?;
            if (projectID != null)
            {
                return ProjectID != null;
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
