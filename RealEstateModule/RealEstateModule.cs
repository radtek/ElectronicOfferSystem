using RealEstateModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace RealEstateModule
{
    public class RealEstateModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RealEstateToolBar>();
            containerRegistry.RegisterForNavigation<RealEstatePage>();
            containerRegistry.RegisterForNavigation<NaturalBuildingPage>();
            containerRegistry.RegisterForNavigation<LogicalBuildingPage>();
            containerRegistry.RegisterForNavigation<FloorPage>();
            containerRegistry.RegisterForNavigation<HouseholdPage>();
            containerRegistry.RegisterForNavigation<ObligeePage>();
            containerRegistry.RegisterForNavigation<MortgagePage>();
            containerRegistry.RegisterForNavigation<SequestrationPage>();
        }
    }
}