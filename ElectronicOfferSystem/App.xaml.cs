using Prism.Ioc;
using Prism.Unity;
using ElectronicOfferSystem.Views;
using System.Windows;

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

    }
}
