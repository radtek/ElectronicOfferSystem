using Common.Views;
using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class MessageDialogViewModel : BindableBase
    {
        private string message;

        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        private MessageDialog view;
        private static MessageDialogViewModel messageDialogViewModel = new MessageDialogViewModel();

        public MessageDialogViewModel()
        {
            Message = string.Empty;
        }

        public static MessageDialogViewModel getInstance()
        {
            return messageDialogViewModel;
        }

        public void show()
        {
            view = new MessageDialog();
            DialogHost.Show(view, "RootDialog");
        }

        private static void ConfirEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
