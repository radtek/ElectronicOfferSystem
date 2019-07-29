using BusinessData;
using BusinessData.Dal;
using Common;
using Common.Events;
using Common.ViewModels;
using Common.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using RealEstateModule.Tasks;
using RealEstateModule.ViewModels.Dialogs;
using RealEstateModule.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RealEstateModule.ViewModels
{
    public class RealEstateToolBarViewModel : BindableBase
    {
        public Project Project { get; set; }
        public string NavigatePath { get; set; }

        public RealEstatePageViewModel RealEstatePageViewModel { get; set; }
        public ImportRealEstateDialogViewModel ImportRealEstateViewModel { get; set; }
        public ExportRealEstateDialogViewModel ExportRealEstateViewModel { get; set; }


        public DelegateCommand AddBusinessCommand { get; set; }
        public DelegateCommand DelBusinessCommand { get; set; }
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        public DelegateCommand OpenImportRealEstateDialogCommand { get; set; }
        public DelegateCommand OpenExportRealEstateDialogCommand { get; set; }
        public DelegateCommand QualityControlCommand { get; set; }

        public RealEstateToolBarViewModel(IRegionManager regionManager, IEventAggregator ea)
        {

            RealEstatePageViewModel = new RealEstatePageViewModel(regionManager, ea);

            // 点击业务的导航页后
            ea.GetEvent<NavBusinessEvent>().Subscribe((navPage) => {
                NavigatePath = navPage;
            });

            AddBusinessCommand = new DelegateCommand(() => {
                RealEstatePageViewModel.NavigatePath = NavigatePath;
                RealEstatePageViewModel.AddBusinessCommand.Execute();
            });
            DelBusinessCommand = new DelegateCommand(() => {
                RealEstatePageViewModel.NavigatePath = NavigatePath;
                RealEstatePageViewModel.DelBusinessCommand.Execute();
            });
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
        private  void QualityControl()
        {
            if (Project == null || !"1".Equals(Project.Type))
            {
                MessageBox.Show("请选择楼盘项目", "提示");
                return;
            }
            var view = new TaskInfoDialog();
            TaskInfoDialogViewModel TaskInfoDialog = TaskInfoDialogViewModel.getInstance();
            //TaskInfoDialog.Messages.Add("开始质检项目：" + Project.ProjectName);
            var result = DialogHost.Show(view, "RootDialog");
            
            QualityControlTask task = new QualityControlTask();
            try
            {
                task.Project = Project;
                task.Ongo();
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }

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
                MessageBox.Show("请选择文件", "提示");
                return;
            }
            // 判断项目名称是否已经存在
            String projectName = Path.GetFileNameWithoutExtension(FullPath);
            ProjectDal projectDal = new ProjectDal();
            var projectList = projectDal.GetListBy(p => p.ProjectName == projectName && p.Type == "1");
            if (projectList.Count > 0)
            {
                MessageBox.Show("项目名称已存在，请更改excel名称", "提示");
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
                MessageBox.Show("请选择导出项目", "提示");
                return;
            }
            if ("0".Equals(Project.State))
            {
                // 请确认此项目质检合格
                MessageBox.Show("请确认此项目质检合格", "提示");
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
