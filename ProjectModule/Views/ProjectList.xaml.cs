using Prism.Regions;
using ProjectModule.ViewModels;
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

namespace ProjectModule.Views
{
    /// <summary>
    /// ProjectList.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectList : UserControl
    {
        public ProjectList(IRegionManager regionManager)
        {
            InitializeComponent();
            // view discovery
            regionManager.RegisterViewWithRegion("AddOrEditProjectDialogRegion", typeof(AddOrEditProjectDialog));
            DataContext = new ProjectListViewModel();
        }
    }
}
