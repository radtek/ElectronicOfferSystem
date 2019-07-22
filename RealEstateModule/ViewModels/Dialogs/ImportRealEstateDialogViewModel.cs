using BusinessData;
using BusinessData.Dal;
using Common.Tasks;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateModule.ViewModels.Dialogs
{
    class ImportRealEstateDialogViewModel : BindableBase
    {
        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set { SetProperty(ref filePath, value); }
        }

        public DelegateCommand ChooseFileCommand { get; set; }
        public DelegateCommand ImportRealEstateCommand { get; set; }

        public ImportRealEstateDialogViewModel()
        {
            ChooseFileCommand = new DelegateCommand(()=> {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog()
                {
                    Filter = "Excel Files (*.xls,*.xlsx)|*.xls;*.xlsx"
                };
                var result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    FilePath = openFileDialog.FileName;
                }
            });

            ImportRealEstateCommand = new DelegateCommand(()=> {
                //FinishInteraction?.Invoke();
                if (string.IsNullOrEmpty(FilePath))
                {
                    //DevComponents.DotNetBar.MessageBoxEx.Show(this, "请选择文件", "提示");
                    return;
                }

                // 判断项目名称是否已经存在
                int start = FilePath.LastIndexOf("\\");
                int end = FilePath.LastIndexOf(".");
                String projectName = FilePath.Substring(start + 1, end - start);

                ProjectDal projectDal = new ProjectDal();
                var projectList = projectDal.GetListBy(p => p.ProjectName == projectName && p.Type == "1");

                if (projectList.Count > 0)
                {
                    //DevComponents.DotNetBar.MessageBoxEx.Show("项目名称已存在，请更改excel名称");
                    return;
                }
                ImportRealEstateTask task = new ImportRealEstateTask();
                task.FullPath = FilePath;
                task.Ongo();
                //this.Close();
            });
        }

        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }
    }
}
