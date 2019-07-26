using BusinessData;
using Common;
using Common.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using RegistrationModule.ViewModels.Dialogs;
using RegistrationModule.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RegistrationModule.ViewModels
{
    class RegistrationToolBarViewModel : BindableBase
    {
        public Project Project { get; set; }
        public ExportRegistrationDialogViewModel ExportRegistrationViewModel { get; set; }

        public DelegateCommand<object> SelectProjectCommand { get; set; }
        public DelegateCommand OpenExportRegistrationDialogCommand { get; set; }

        public RegistrationToolBarViewModel()
        {
            OpenExportRegistrationDialogCommand = new DelegateCommand(ExecuteExportRegistrationDialog);

            // 在项目列表选择一个项目之后执行
            SelectProjectCommand = new DelegateCommand<object>((obj) =>
            {

                ListView listView = obj as ListView;
                Project = listView.SelectedItem as Project;
            });
            GlobalCommands.SelectProjectCommand.RegisterCommand(SelectProjectCommand);
        }

        /// <summary>
        /// 打开导出框
        /// </summary>
        private async void ExecuteExportRegistrationDialog()
        {
            if (Project == null)
            {
                // 请选择导出项目
                return;
            }
            var view = new ExportRegistrationDialog();
            ExportRegistrationViewModel = new ExportRegistrationDialogViewModel();
            ExportRegistrationViewModel.Project = Project;
            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ConfirExportRealEstateEventHandler);
        }
        /// <summary>
        /// 点击确认/取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ConfirExportRealEstateEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ("False".Equals(eventArgs.Parameter.ToString())) return;
            // cancel the close
            eventArgs.Cancel();

            var FileTextBox = eventArgs.Parameter as TextBox;
            String FullPath = FileTextBox.Text;

            // 开始导出
            ExportRegistrationViewModel.FilePath = FullPath;
            ExportRegistrationViewModel.Project = Project;
            ExportRegistrationViewModel.ExportRegistrationCommand.Execute();

            // 显示任务信息模态框
            eventArgs.Session.UpdateContent(new TaskInfoDialog());
        }
    }
}
