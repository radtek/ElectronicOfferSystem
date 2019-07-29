using BusinessData;
using Common.ViewModels;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using RealEstateModule.Services.Export;
using RealEstateModule.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RealEstateModule.ViewModels.Dialogs
{
    public class ExportRealEstateDialogViewModel : BindableBase
    {
        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set { SetProperty(ref filePath, value); }
        }

        public Project Project { get; set; }

        public DelegateCommand ChooseFileCommand { get; set; }
        public DelegateCommand ExportRealEstateCommand { get; set; }

        public ExportRealEstateDialogViewModel()
        {
            ChooseFileCommand = new DelegateCommand(() => {

                //创建一个保存文件式的对话框
                SaveFileDialog sfd = new SaveFileDialog();
                //设置这个对话框的起始保存路径
                //sfd.InitialDirectory = @"D:\";
                // 默认文件名
                //sfd.FileName = Project.ProjectName;
                //设置保存的文件的类型，注意过滤器的语法
                sfd.Filter = "BPF文件|*.bpf";
                
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

            ExportRealEstateCommand = new DelegateCommand(()=> {
                ExportRealEstateTask task = new ExportRealEstateTask();
                try
                {
                    task.SaveFileName = FilePath;
                    task.book = new ExportRealEstateBook();
                    task.TemplateFileName = System.AppDomain.CurrentDomain.BaseDirectory + @"Templates\补录-批量导入户数据模板.xlt";
                    task.Project = Project;
                    task.Ongo();
                }
                catch (Exception ex)
                {
                    ErrorDialogViewModel.getInstance().show(ex);
                    return;
                }

            });
        }
    }
}
