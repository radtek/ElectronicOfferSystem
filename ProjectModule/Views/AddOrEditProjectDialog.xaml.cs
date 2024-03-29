﻿using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using ProjectModule.ViewModels;
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

namespace ProjectModule.Views
{
    /// <summary>
    /// AddOrEditProjectDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddOrEditProjectDialog : UserControl
    {
        public AddOrEditProjectDialog()
        {
            InitializeComponent();
            DataContext = AddOrEditProjectDialogViewModel.getInstance();
        }
       
    }
}
