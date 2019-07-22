using Common;
using Common.ViewModels;
using Common.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealEstateModule.ViewModels
{
    class RealEstateToolBarViewModel : BindableBase
    {
        public InteractionRequest<INotification> ImportRealEstateDialogRequest { get; set; }
        public DelegateCommand OpenImportRealEstateDialogCommand { get; set; }

        public RealEstateToolBarViewModel()
        {
            ImportRealEstateDialogRequest = new InteractionRequest<INotification>();
            OpenImportRealEstateDialogCommand = new DelegateCommand(RaiseImportRealEstateDialog);
        }

        private void RaiseImportRealEstateDialog()
        {
            ImportRealEstateDialogRequest.Raise(new Notification { Title = "Custom Popup", Content = "Custom Popup Message " }, r => { });
        }
    }
}
