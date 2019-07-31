using Common.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public  class TaskInfoDialogViewModel : BindableBase
    {
        //private ObservableCollection<string> messages;
        //public ObservableCollection<string> Messages
        //{
        //    get { return messages; }
        //    set { SetProperty(ref messages, value); }
        //}
        private ObservableCollection<TaskMessage> messages;
        public ObservableCollection<TaskMessage> Messages
        {
            get { return messages; }
            set { SetProperty(ref messages, value); }
        }



        private static TaskInfoDialogViewModel taskInfoDialogViewModel = new TaskInfoDialogViewModel();

        public TaskInfoDialogViewModel()
        {
            //Messages = new ObservableCollection<string>();
            Messages = new ObservableCollection<TaskMessage>();
        }

        public static TaskInfoDialogViewModel getInstance()
        {
            return taskInfoDialogViewModel;
        }

    }
}
