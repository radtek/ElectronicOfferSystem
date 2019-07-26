﻿using BusinessData;
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
using System.Windows.Controls;
using System.Windows.Input;

namespace RealEstateModule.ViewModels
{
    public class RealEstateToolBarViewModel : BindableBase
    {
        public Project Project { get; set; }

        public ImportRealEstateDialogViewModel ImportRealEstateViewModel { get; set; }
        public ExportRealEstateDialogViewModel ExportRealEstateViewModel { get; set; }

        public DelegateCommand<object> SelectProjectCommand { get; set; }
        public DelegateCommand OpenImportRealEstateDialogCommand { get; set; }
        public DelegateCommand OpenExportRealEstateDialogCommand { get; set; }
        public DelegateCommand QualityControlCommand { get; set; }

        public RealEstateToolBarViewModel()
        {
            QualityControlCommand = new DelegateCommand(QualityControl);
            OpenImportRealEstateDialogCommand = new DelegateCommand(ExecuteImportRealEstateDialog);
            OpenExportRealEstateDialogCommand = new DelegateCommand(ExecuteExportRealEstateDialog);

            // 在项目列表选择一个项目之后执行
            SelectProjectCommand = new DelegateCommand<object>((obj) =>
            {

                ListView listView = obj as ListView;
                Project = listView.SelectedItem as Project;
            });
            GlobalCommands.SelectProjectCommand.RegisterCommand(SelectProjectCommand);
        }

        #region 质检
        private async void QualityControl()
        {
            if (Project == null)
            {
                return;
            }
            var view = new TaskInfoDialog();
            var result = await DialogHost.Show(view, "RootDialog");
            QualityControlTask task = new QualityControlTask();
            task.Project = Project;
            task.Ongo();
        }
        #endregion

        #region 导入楼盘表
        /// <summary>
        /// 打开导入框
        /// </summary>
        private async void ExecuteImportRealEstateDialog()
        {
            var view = new ImportRealEstateDialog();
            ImportRealEstateViewModel = new ImportRealEstateDialogViewModel();
            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ConfirImportRealEstateEventHandler);

        }
        /// <summary>
        /// 点击按钮，确认/取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
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
        #endregion

        #region 导出楼盘表
        /// <summary>
        /// 打开导出框
        /// </summary>
        private async void ExecuteExportRealEstateDialog()
        {
            if (Project == null)
            {
                // 请选择导出项目
                return;
            }
            if ("0".Equals(Project.State))
            {
                // 请确认此项目质检合格
                return;
            }
            var view = new ExportRealEstateDialog();
            ExportRealEstateViewModel = new ExportRealEstateDialogViewModel();
            ExportRealEstateViewModel.Project = Project;
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
            ExportRealEstateViewModel.FilePath = FullPath;
            ExportRealEstateViewModel.Project = Project;
            ExportRealEstateViewModel.ExportRealEstateCommand.Execute();

            // 显示任务信息模态框
            eventArgs.Session.UpdateContent(new TaskInfoDialog());

            // run a fake operation for 3 seconds then close this baby.
            //Task.Delay(TimeSpan.FromSeconds(1))
            //    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
            //        TaskScheduler.FromCurrentSynchronizationContext());
        }
        #endregion
    }
}
