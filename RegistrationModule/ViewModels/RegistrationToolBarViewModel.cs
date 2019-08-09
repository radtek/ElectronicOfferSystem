using BusinessData;
using Common;
using Common.Enums;
using Common.Events;
using Common.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using RegistrationModule.ViewModels.Dialogs;
using RegistrationModule.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RegistrationModule.ViewModels
{
    public class RegistrationToolBarViewModel : BindableBase
    {
        IEventAggregator EA;
        public Project Project { get; set; }
        public ExportRegistrationDialogViewModel ExportRegistrationViewModel { get; set; }

        public RegistrationPageViewModel RegistrationPageViewModel { get; set; }

        public DelegateCommand AddApplicantCommand { get; set; }
        public DelegateCommand DelApplicantCommand { get; set; }
        public DelegateCommand OpenExportRegistrationDialogCommand { get; set; }

        public RegistrationToolBarViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            EA = ea;
            OpenExportRegistrationDialogCommand = new DelegateCommand(ExecuteExportRegistrationDialog);
            RegistrationPageViewModel = new RegistrationPageViewModel(regionManager, ea);

            // 在项目列表选择一个项目之后执行
            EA.GetEvent<SelectProjectEvent>().Subscribe(selectProject => {
                Project = selectProject;
            });

            AddApplicantCommand = new DelegateCommand(() => {
                RegistrationPageViewModel.RegistrationNavCommand.Execute(ERegistrationPage.TransferPage);
            });

            DelApplicantCommand = new DelegateCommand(() => {
            });
        }

        /// <summary>
        /// 打开导出框
        /// </summary>
        private async void ExecuteExportRegistrationDialog()
        {
            if (Project == null || !"2".Equals(Project.Type))
            {
                // 请选择导出项目
                MessageBox.Show("请选择登记业务项目", "提示");
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
