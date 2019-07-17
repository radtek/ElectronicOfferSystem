using RegistrationModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace RegistrationModule
{
    public class RegistrationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RegistrationPage>();
        }
    }
}