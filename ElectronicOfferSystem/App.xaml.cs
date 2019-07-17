using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Prism.Modularity;
using ElectronicOfferSystem.Views;

namespace ElectronicOfferSystem
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<RealEstateModule.RealEstateModule>();
            moduleCatalog.AddModule<RegistrationModule.RegistrationModule>();
        }
    }
}
