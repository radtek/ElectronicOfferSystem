using BusinessData;
using BusinessData.Dal;
using Common.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using RealEstateModule.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateModule.ViewModels.Dialogs
{
    public class ImportRealEstateDialogViewModel : BindableBase
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
                
                ImportRealEstateTask task = new ImportRealEstateTask();
                try
                {
                    task.FullPath = FilePath;
                    task.Ongo();
                }
                catch (Exception ex)
                {
                    ErrorDialogViewModel.getInstance().show(ex);
                    return;
                }
            });
        }

        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }
    }
}
