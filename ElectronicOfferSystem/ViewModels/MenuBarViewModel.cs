using Common.Configurations;
using Common.Enums;
using Common.Events;
using Common.Utils;
using ElectronicOfferSystem.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using OAUS.Core;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicOfferSystem.ViewModels
{
    public class MenuBarViewModel : BindableBase
    {
        //private IRegionManager RegionManager;
        //private IEventAggregator EA;
        public string UpdateIP { get; set; }
        public string UpdatePort { get; set; }

        public DelegateCommand<string> AddRealEstateProjectCommand { get; set; }
        public DelegateCommand<string> AddRegistrationProjectCommand { get; set; }
        public DelegateCommand ImportRealEstateProjectCommand { get; set; }
        public DelegateCommand ExportRealEstateProjectCommand { get; set; }
        public DelegateCommand ExportRegistrationProjectCommand { get; set; }
        public DelegateCommand QualityControlCommand { get; set; }
        public DelegateCommand SetProjectPathCommand { get; set; }
        public DelegateCommand SetServerIPCommand { get; set; }
        public DelegateCommand ShowHelpCommand { get; set; }
        public DelegateCommand CheckUpdateCommand { get; set; }
        public DelegateCommand AboutCommand { get; set; }


        public MenuBarViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            ProjectPageViewModel projectPageViewModel = new ProjectPageViewModel(regionManager, ea);
            RealEstateToolBarViewModel realEstateToolBarViewModel = new RealEstateToolBarViewModel(regionManager, ea);
            RegistrationToolBarViewModel registrationToolBarViewModel = new RegistrationToolBarViewModel(regionManager, ea);
            IndexPageViewModel indexPageViewModel = new IndexPageViewModel();

            AddRealEstateProjectCommand = new DelegateCommand<string>((str) =>
            {
                projectPageViewModel.ProjectType = "1";
                projectPageViewModel.NavigatePath = EMainPage.RealEstatePage;
                projectPageViewModel.OpenAddOrEditProjectDialogCommand.Execute(str);
            });
            AddRegistrationProjectCommand = new DelegateCommand<string>((str) =>
            {
                projectPageViewModel.ProjectType = "2";
                projectPageViewModel.NavigatePath = EMainPage.RegistrationPage;
                projectPageViewModel.OpenAddOrEditProjectDialogCommand.Execute(str);
            });

            ImportRealEstateProjectCommand = new DelegateCommand(() =>
            {
                realEstateToolBarViewModel.OpenImportRealEstateDialogCommand.Execute();
            });
            ExportRealEstateProjectCommand = new DelegateCommand(() =>
            {
                realEstateToolBarViewModel.OpenExportRealEstateDialogCommand.Execute();
            });
            ExportRegistrationProjectCommand = new DelegateCommand(() =>
            {
                registrationToolBarViewModel.OpenExportRegistrationDialogCommand.Execute();
            });

            QualityControlCommand = new DelegateCommand(() =>
            {
                realEstateToolBarViewModel.QualityControlCommand.Execute();
            });

            SetProjectPathCommand = new DelegateCommand(() =>
            {
                indexPageViewModel.OpenProjectPathDialogCommand.Execute();
            });

            SetServerIPCommand = new DelegateCommand(() =>
            {
                indexPageViewModel.OpenServerDialogCommand.Execute();
            });

            ShowHelpCommand = new DelegateCommand(() =>
            {
                string helpfile = AppDomain.CurrentDomain.BaseDirectory + @"Help\报盘系统帮助文档.chm";
                Process.Start(helpfile);
            });

            CheckUpdateCommand = new DelegateCommand(CheckUpdate);

            AboutCommand = new DelegateCommand(About);
        }


        private void CheckUpdate()
        {
            ReadConfigInfo();
            Task.Factory.StartNew((_this) =>
            {
                try
                {
                    if (VersionHelper.HasNewVersion(UpdateIP, Int32.Parse(UpdatePort)))
                    {
                        MessageBoxResult result = MessageBox.Show("发现新版本，是否更新？", "提示信息", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            App.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                App.Current.Shutdown();
                            }));
                            string updateExePath = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdater\\AutoUpdater.exe";
                            System.Diagnostics.Process myProcess = System.Diagnostics.Process.Start(updateExePath);
                           
                        }
                    }
                    else
                    {
                        MessageBox.Show("无最新版本");
                    }
                }
                catch
                {
                    MessageBox.Show("由于网络原因无法检查版本更新");
                }
            }, this);
        }

        private async void About()
        {
            await DialogHost.Show(new AboutDialog(), "RootDialog");
        }

        /// <summary>
        /// 读取本地配置信息
        /// </summary>
        public void ReadConfigInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + LocalConfiguration.INI_CFG;
            if (File.Exists(cfgINI))
            {
                IniFileHelper ini = new IniFileHelper(cfgINI);
                UpdateIP = ini.IniReadValue("OAUS", "UpdateIP");
                UpdatePort = ini.IniReadValue("OAUS", "UpdatePort");
            }
        }
    }
}
