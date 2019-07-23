using BusinessData.Dal;
using Common;
using Common.ViewModels;
using Common.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using RealEstateModule.Tasks;
using RealEstateModule.ViewModels.Dialogs;
using RealEstateModule.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealEstateModule.ViewModels
{
    class RealEstateToolBarViewModel : BindableBase
    {

        public ImportRealEstateDialogViewModel ImportRealEstateViewModel { get; set; }

        public DelegateCommand OpenImportRealEstateDialogCommand { get; set; }

        public RealEstateToolBarViewModel()
        {      
            OpenImportRealEstateDialogCommand = new DelegateCommand(ExecuteImportRealEstateDialog);
        }

        private async void ExecuteImportRealEstateDialog()
        {
            var view = new ImportRealEstateDialog();
            ImportRealEstateViewModel = new ImportRealEstateDialogViewModel();
            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ConfirImportRealEstateEventHandler);

        }



        private void ConfirImportRealEstateEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ("False".Equals(eventArgs.Parameter.ToString())) return;
            // cancel the close
            eventArgs.Cancel();

            var FileTextBox = eventArgs.Parameter as System.Windows.Controls.TextBox;
            String FullPath = FileTextBox.Text;
            if (string.IsNullOrEmpty(FullPath))
            {
                //DevComponents.DotNetBar.MessageBoxEx.Show(this, "请选择文件", "提示");
                return;
            }
            // 判断项目名称是否已经存在
            String projectName = Path.GetFileNameWithoutExtension(FullPath);
            ProjectDal projectDal = new ProjectDal();
            var projectList = projectDal.GetListBy(p => p.ProjectName == projectName && p.Type == "1");
            if (projectList.Count > 0)
            {
                //DevComponents.DotNetBar.MessageBoxEx.Show("项目名称已存在，请更改excel名称");
                return;
            }
            // 开始导入
            ImportRealEstateViewModel.FilePath = FullPath;
            ImportRealEstateViewModel.ImportRealEstateCommand.Execute();

            // 显示任务信息模态框
            eventArgs.Session.UpdateContent(new TaskInfoDialog());

            // run a fake operation for 3 seconds then close this baby.
            //Task.Delay(TimeSpan.FromSeconds(1))
            //    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
            //        TaskScheduler.FromCurrentSynchronizationContext());
        }

        
    }
}
