﻿using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistrationModule.Views
{
    /// <summary>
    /// RegistrationPage.xaml 的交互逻辑
    /// </summary>
    public partial class RegistrationPage : UserControl
    {
        public RegistrationPage(IRegionManager regionManager)
        {
            InitializeComponent();

            // view discovery
            regionManager.RegisterViewWithRegion("RegistrationToolBarRegion", typeof(RegistrationToolBar));
            //regionManager.RegisterViewWithRegion("TransferPageRegion", typeof(TransferPage));
            //regionManager.RegisterViewWithRegion("FileManagerPageRegion", typeof(FileManagerPage));
        }
    }
}
