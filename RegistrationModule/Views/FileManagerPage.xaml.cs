using RegistrationModule.ViewModels;
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

namespace RegistrationModule.Views
{
    /// <summary>
    /// FileManagerPage.xaml 的交互逻辑
    /// </summary>
    public partial class FileManagerPage : UserControl
    {
        public FileManagerPage()
        {
            InitializeComponent();
            DataContext = new FileManagerPageViewMode();
        }
        public FileManagerPageViewMode ViewModel => DataContext as FileManagerPageViewMode;
        /// <summary>
        /// TreesView's SelectedItem is read-only. Hence we can't bind it. There is a way to obtain a selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (ViewModel == null) return;

            ViewModel.SelectedTreeNode = e.NewValue;
        }
    }
}
