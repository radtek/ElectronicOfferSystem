using BusinessData;
using Common.Configurations;
using Common.Utils;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ReadConfigInfo();

            ChooseFileCommand = new DelegateCommand(() => {

                //创建一个保存文件式的对话框
                SaveFileDialog sfd = new SaveFileDialog();
                //设置这个对话框的起始保存路径
                //sfd.InitialDirectory = @"D:\";
                // 默认文件名
                //sfd.FileName = Project.ProjectName;
                //设置保存的文件的类型，注意过滤器的语法

                //调用ShowDialog()方法显示该对话框，该方法的返回值代表用户是否点击了确定按钮
                if (sfd.ShowDialog() == true)
                {
                    if (sfd.CheckPathExists)
                    {
                        FilePath = sfd.FileName;
                    }
                    else
                    {
                        MessageBox.Show("文件夹路径不能为空", "提示");
                        return;
                    }
                }
            });

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
                FilePath = ini.IniReadValue("Project", "FilePath");
            }
        }
    }
}
