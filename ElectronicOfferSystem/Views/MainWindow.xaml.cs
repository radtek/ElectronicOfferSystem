using ElectronicOfferSystem.Dal;
using ElectronicOfferSystem.ViewModels;
using Prism.Regions;
using System.Windows;
using System.Windows.Input;


namespace ElectronicOfferSystem.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            // view discovery
            regionManager.RegisterViewWithRegion("WindowTopRegion", typeof(WindowTop));

            DataContext = new MainWindowViewModel();
        }
        private void NavBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
