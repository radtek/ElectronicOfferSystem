using Common;
using Common.Enums;
using Common.Views;
using ElectronicOfferSystem.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ProjectModule.Views;
using RealEstateModule.Views;
using RegistrationModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ElectronicOfferSystem.ViewModels
{
    class ProjectTabViewModel : BindableBase
    {
        private IRegionManager RegionManager;
        //private IUnityContainer Container;

        public IndexPage IndexPage { get; set; }
        public RealEstatePage RealEstatePage { get; set; }
        public RegistrationPage RegistrationPage { get; set; }

        private EMainPage mainPage = EMainPage.IndexPage;
        public EMainPage MainPage
        {
            get { return mainPage; }
            set { SetProperty(ref mainPage, value); }
        }


        public DelegateCommand<EMainPage?> NavigateCommand { get; set; }
        public DelegateCommand OpenTaskInfoDialogCommand { get; set; }

        public ProjectTabViewModel(IRegionManager regionManager)
        {
            RegionManager = regionManager;
            
            // 页面导航
            NavigateCommand = new DelegateCommand<EMainPage?>(Navigate);
            GlobalCommands.NavigateCommand.RegisterCommand(NavigateCommand);

            OpenTaskInfoDialogCommand = new DelegateCommand(() =>{
                var view = new TaskInfoDialog();
                //ImportRealEstateViewModel = new TaskInfoDialogViewModel();
                //show the dialog
                var result = DialogHost.Show(view, "RootDialog");
            });
        }

        public void Navigate(EMainPage? navigatePath)
        {
            if (navigatePath != null)
            {
                MainPage = (EMainPage)navigatePath;
               RegionManager.RequestNavigate("ContentRegion", navigatePath.ToString(), NavigationComplete);        
            }
        }

        private void NavigationComplete(NavigationResult result)
        {
            //var view = Container.Resolve<ProjectPage>();
            
            //System.Windows.MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
        }
    }
}
