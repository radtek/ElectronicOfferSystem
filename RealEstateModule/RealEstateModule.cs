using RealEstateModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using RealEstateModule.Views.Statistics;

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
            containerRegistry.RegisterForNavigation<NaturalBuildingPageStatistics>();
            containerRegistry.RegisterForNavigation<LogicalBuildingPageStatistics>();
            containerRegistry.RegisterForNavigation<FloorPageStatistics>();
            containerRegistry.RegisterForNavigation<HouseholdPageStatistics>();
            containerRegistry.RegisterForNavigation<ObligeePageStatistics>();
            containerRegistry.RegisterForNavigation<MortgagePageStatistics>();
            containerRegistry.RegisterForNavigation<SequestrationPageStatistics>();
        }
    }
}