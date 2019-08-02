using LoginModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LoginModule
{
    public class LoginModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Login>();
        }
    }
}