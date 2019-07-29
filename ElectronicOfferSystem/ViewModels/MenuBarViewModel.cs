﻿using Common.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using ProjectModule.ViewModels;
using RealEstateModule.ViewModels;
using RegistrationModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicOfferSystem.ViewModels
{
    public class MenuBarViewModel : BindableBase
    {
        private IRegionManager RegionManager;
        private IEventAggregator EA;

        public DelegateCommand<string> AddRealEstateProjectCommand { get; set; }
        public DelegateCommand<string> AddRegistrationProjectCommand { get; set; }
        public DelegateCommand ImportRealEstateProjectCommand { get; set; }
        public DelegateCommand ExportRealEstateProjectCommand { get; set; }
        public DelegateCommand ExportRegistrationProjectCommand { get; set; }
        public DelegateCommand QualityControlCommand { get; set; }
        public DelegateCommand ShowHelpCommand { get; set; }

        public MenuBarViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            ProjectPageViewModel projectPageViewModel = new ProjectPageViewModel();
            RealEstateToolBarViewModel realEstateToolBarViewModel = new RealEstateToolBarViewModel(regionManager, ea);
            RegistrationToolBarViewModel registrationToolBarViewModel = new RegistrationToolBarViewModel(regionManager, ea);

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

            ShowHelpCommand = new DelegateCommand(() => {
                string helpfile = AppDomain.CurrentDomain.BaseDirectory + @"Help\报盘系统帮助文档.chm";
                Process.Start(helpfile);
            });
        }
    }
}
