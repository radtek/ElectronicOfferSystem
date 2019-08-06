﻿using BusinessData;
using Common.Configurations;
using Common.Utils;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ElectronicOfferSystem.ViewModels.Dialogs
{
    public class ProjectPathDialogViewModel : BindableBase
    {
        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set { SetProperty(ref filePath, value); }
        }

        public Project Project { get; set; }

        public DelegateCommand ChooseFileCommand { get; set; }

        public ProjectPathDialogViewModel()
        {
            FilePath = FileHelper.ReadConfigInfo();

            ChooseFileCommand = new DelegateCommand(() =>
            {

                //创建一个保存文件式的对话框

                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                fbd.Description = "请选择一个目录";
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FilePath = fbd.SelectedPath;
                }

            });

        }
    }
}
