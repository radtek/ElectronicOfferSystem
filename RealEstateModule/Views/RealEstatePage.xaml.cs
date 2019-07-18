using Prism.Regions;
using RealEstateModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealEstateModule.Views
{
    /// <summary>
    /// RealEstatePage.xaml 的交互逻辑
    /// </summary>
    public partial class RealEstatePage : UserControl
    {
        public RealEstatePage(IRegionManager regionManager)
        {
            InitializeComponent();
            // view discovery
            regionManager.RegisterViewWithRegion("RealEstateToolBarRegion", typeof(RealEstateToolBar));
            regionManager.RegisterViewWithRegion("NaturalBuildingPageRegion", typeof(NaturalBuildingPage));

            DataContext = new RealEstatePageViewModel();
        }
    }
}
