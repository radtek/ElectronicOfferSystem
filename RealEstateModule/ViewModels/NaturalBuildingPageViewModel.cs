using BusinessData;
using BusinessData.Dal;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateModule.ViewModels
{
    class NaturalBuildingPageViewModel : BindableBase
    {
        private NaturalBuilding naturalBuilding;
        public NaturalBuilding NaturalBuilding
        {
            get { return naturalBuilding; }
            set { SetProperty(ref naturalBuilding, value); }
        }

        public DelegateCommand AddOrEditNaturalBuildingCommand { get; set; }

        NaturalBuildingDal NaturalBuildingDal = new NaturalBuildingDal();

        public NaturalBuildingPageViewModel()
        {
            AddOrEditNaturalBuildingCommand = new DelegateCommand(() => {
                NaturalBuildingDal.Add(NaturalBuilding);

            });
        }
    }
}
