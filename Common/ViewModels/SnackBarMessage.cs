using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class SnackBarMessage : BindableBase
    {

        public SnackbarMessageQueue MessageQueue { get; set; }


        private static SnackBarMessage snackBarMessage = new SnackBarMessage();

        private SnackBarMessage()
        {

        }

        public SnackBarMessage(SnackbarMessageQueue messageQueue)
        {
            MessageQueue = messageQueue;
        }

        public static SnackBarMessage getInstance()
        {
            return snackBarMessage;
        }

        //public static void show(string message)
        //{
        //    Task.Factory.StartNew(() => MessageQueue.Enqueue(message));
        //}
    }
}
