using Prism.Commands;
using Prism.Mvvm;
using ProjectModule.ViewModels;
using RealEstateModule.ViewModels;
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
        public DelegateCommand ImportRealEstateProjectCommand { get; set; }

        public MenuBarViewModel()
        {
            ProjectPageViewModel projectPageViewModel = new ProjectPageViewModel();
            AddRealEstateProjectCommand = new DelegateCommand<string>((str) =>{
                projectPageViewModel.OpenAddOrEditProjectDialogCommand.Execute(str);
            });

            ImportRealEstateProjectCommand = new DelegateCommand(() => {
                RealEstateToolBarViewModel realEstateToolBarViewModel = new RealEstateToolBarViewModel();
                realEstateToolBarViewModel.OpenImportRealEstateDialogCommand.Execute();
            });
        }
    }
}
