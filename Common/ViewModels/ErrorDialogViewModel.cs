using Common.Models;
using Common.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Common.ViewModels
{
    public class ErrorDialogViewModel : BindableBase
    {
        private ErrorMessage errorMessage;
        public ErrorMessage ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        private ErrorDialog view;
        private static ErrorDialogViewModel errorDialogViewModel = new ErrorDialogViewModel();

        public DelegateCommand CopyCommand { get; set; }

        public ErrorDialogViewModel()
        {
            ErrorMessage = new ErrorMessage();
            CopyCommand = new DelegateCommand(() =>{
                Clipboard.SetText(ErrorMessage.Message + ErrorMessage.StackTrace);
            });
        }

        public static ErrorDialogViewModel getInstance()
        {
            return errorDialogViewModel;
        }

        public void show(Exception ex)
        {
            InitalMessage(ex);
            view = new ErrorDialog();
            DialogHost.Show(view, "RootDialog");
        }

        public void show(string message, string stackTrace)
        {
            ErrorMessage.Message = message;
            ErrorMessage.StackTrace = stackTrace;
            view = new ErrorDialog();
            DialogHost.Show(view, "RootDialog");
        }

        public void updateShow(Exception ex, DialogSession session)
        {
            InitalMessage(ex);
            view = new ErrorDialog();
            session.UpdateContent(view);
        }

        private void InitalMessage(Exception ex)
        {
            ErrorMessage.Message = ex.Message;
            ErrorMessage.StackTrace = ex.StackTrace;
        }
    }
}
