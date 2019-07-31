using BusinessData;
using BusinessData.Dal;
using Common.ViewModels;
using Common.Views;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicOfferSystem.ViewModels
{
    class WindowTopViewModel : BindableBase
    {
        public DelegateCommand MinCommand { get; set; }
        public DelegateCommand MaxCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public InteractionRequest<IConfirmation> ConfirmCloseRequest { get; set; }
        public DelegateCommand ConfirmCloseCommand { get; set; }

        public WindowTopViewModel()
        {
            CloseCommand = new DelegateCommand(() =>
            {
                Application.Current.Shutdown();
            });
            MinCommand = new DelegateCommand(() =>
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });
            MaxCommand = new DelegateCommand(() =>
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                {
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                }
                else
                {
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                }
            });
            ConfirmCloseRequest = new InteractionRequest<IConfirmation>();
            ConfirmCloseCommand = new DelegateCommand(RaiseConfirmClose);
        }

        private void RaiseConfirmClose()
        {
            ConfirmCloseRequest.Raise(new Confirmation { Title = "确认消息", Content = "请确认退出系统？" }, r =>
            {
                if (r.Confirmed) CloseCommand.Execute();
                else return;

            });
        }

    }
}
