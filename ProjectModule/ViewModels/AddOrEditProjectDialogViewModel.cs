using BusinessData;
using BusinessData.Dal;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModule.ViewModels
{
    public class AddOrEditProjectDialogViewModel : BindableBase
    {

        private Project project;
        public Project Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }

        private string dialogTitle;
        public string DialogTitle
        {
            get { return dialogTitle; }
            set
            {
                SetProperty(ref dialogTitle, value);
            }
        }

        private static AddOrEditProjectDialogViewModel addOrEditProjectDialogViewModel = new AddOrEditProjectDialogViewModel();


        public AddOrEditProjectDialogViewModel()
        {
           // Project = new Project();
        }

        public static AddOrEditProjectDialogViewModel getInstance()
        {
            return addOrEditProjectDialogViewModel;
        }

    }
}
