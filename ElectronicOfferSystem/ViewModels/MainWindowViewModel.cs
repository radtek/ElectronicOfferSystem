using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicOfferSystem.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public DelegateCommand MinCommand { get; set; }
        public DelegateCommand MaxCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        

        public MainWindowViewModel()
        {
            CloseCommand = new DelegateCommand(() => {
                Application.Current.Shutdown();
            });
            MinCommand = new DelegateCommand(() => {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });
            MaxCommand = new DelegateCommand(() => {
                if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                {
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                }
                else
                {
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                }
            });

        }
    }
}
