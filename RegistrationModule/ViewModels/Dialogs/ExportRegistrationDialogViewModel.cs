using BusinessData;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using RegistrationModule.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationModule.ViewModels.Dialogs
{
    public class ExportRegistrationDialogViewModel : BindableBase
    {
        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set { SetProperty(ref filePath, value); }
        }

        public Project Project { get; set; }

        public DelegateCommand ChooseFileCommand { get; set; }
        public DelegateCommand ExportRegistrationCommand { get; set; }

        public ExportRegistrationDialogViewModel()
        {
            ChooseFileCommand = new DelegateCommand(() => {

                //创建一个保存文件式的对话框
                SaveFileDialog sfd = new SaveFileDialog();
                // 默认文件名
                sfd.FileName = "登记项目";
                //保存对话框是否记忆上次打开的目录 
                sfd.RestoreDirectory = true;
                //调用ShowDialog()方法显示该对话框，该方法的返回值代表用户是否点击了确定按钮
                if (sfd.ShowDialog() == true)
                {
                    if (sfd.CheckPathExists)
                    {
                        FilePath = sfd.FileName;
                    }
                    else
                    {
                        //DevComponents.DotNetBar.MessageBoxEx.Show(this, "文件夹路径不能为空", "提示");
                        return;
                    }
                }
            });

            ExportRegistrationCommand = new DelegateCommand(() => {
                ExportRegistrationTask task = new ExportRegistrationTask();
                task.SaveFileName = FilePath;
                task.Project = Project;
                task.Ongo();
            });
        }
    }
}
