using Prism.Commands;
using Prism.Mvvm;
using ProjectModule.ViewModels;
using RealEstateModule.ViewModels;
using RegistrationModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicOfferSystem.ViewModels
{
    public class MenuBarViewModel : BindableBase
    {
        public DelegateCommand<string> AddRealEstateProjectCommand { get; set; }
        public DelegateCommand<string> AddRegistrationProjectCommand { get; set; }
        public DelegateCommand ImportRealEstateProjectCommand { get; set; }
        public DelegateCommand ExportRealEstateProjectCommand { get; set; }
        public DelegateCommand ExportRegistrationProjectCommand { get; set; }
        public DelegateCommand QualityControlCommand { get; set; }

        public MenuBarViewModel()
        {
            ProjectPageViewModel projectPageViewModel = new ProjectPageViewModel();
            RealEstateToolBarViewModel realEstateToolBarViewModel = new RealEstateToolBarViewModel();
            RegistrationToolBarViewModel registrationToolBarViewModel = new RegistrationToolBarViewModel();


            AddRealEstateProjectCommand = new DelegateCommand<string>((str) =>
            {
                projectPageViewModel.NavigatePath = "RealEstatePage";
                projectPageViewModel.OpenAddOrEditProjectDialogCommand.Execute(str);
            });
            AddRegistrationProjectCommand = new DelegateCommand<string>((str) =>
            {
                projectPageViewModel.NavigatePath = "RegistrationPage";
                projectPageViewModel.OpenAddOrEditProjectDialogCommand.Execute(str);
            });

            ImportRealEstateProjectCommand = new DelegateCommand(() =>
            {
                realEstateToolBarViewModel.OpenImportRealEstateDialogCommand.Execute();
            });
            ExportRealEstateProjectCommand = new DelegateCommand(() => {
                realEstateToolBarViewModel.OpenExportRealEstateDialogCommand.Execute();
            });
            ExportRegistrationProjectCommand = new DelegateCommand(() => {
                registrationToolBarViewModel.OpenExportRegistrationDialogCommand.Execute();
            });

            QualityControlCommand = new DelegateCommand(() => {
                realEstateToolBarViewModel.QualityControlCommand.Execute();
            });

        }
    }
}
