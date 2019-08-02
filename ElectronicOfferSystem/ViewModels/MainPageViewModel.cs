using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ElectronicOfferSystem.ViewModels
{
    public class MainPageViewModel
    {

        public MainPageViewModel()
        {
            InitalWindowStyle();
        }


        private void InitalWindowStyle()
        {
            Window mainWindow = Application.Current.Windows[0];
            //mainWindow.AllowsTransparency = false;
            mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            mainWindow.Width = 1100;
            mainWindow.Height = 800;
            mainWindow.ResizeMode = ResizeMode.CanResize;
            mainWindow.WindowState = WindowState.Maximized;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //BrushConverter brushConverter = new BrushConverter();
            //Brush brush = (Brush)brushConverter.ConvertFromString("White");
            //mainWindow.Background = brush;
        }
    }
}
