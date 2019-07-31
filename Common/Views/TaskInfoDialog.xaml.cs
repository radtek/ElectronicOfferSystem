using Common.ViewModels;
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

namespace Common.Views
{
    /// <summary>
    /// TaskInfoDialog.xaml 的交互逻辑
    /// </summary>
    public partial class TaskInfoDialog : UserControl
    {
        public TaskInfoDialog()
        {
            InitializeComponent();
            DataContext = TaskInfoDialogViewModel.getInstance();

            // 解决鼠标中轴滚动问题
            UseTheScrollViewerScrolling(Messages);
        }

        public void UseTheScrollViewerScrolling(FrameworkElement fElement)
         {
             fElement.PreviewMouseWheel += (sender, e) =>
             {
                 var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                 eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                 eventArg.Source = sender;
                 fElement.RaiseEvent(eventArg);
             };
         }
    }
}
