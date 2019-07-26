using BusinessData;
using Common.ViewModels;
using RegistrationModule.Services.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RegistrationModule.Tasks
{
    public class ExportRegistrationTask
    {
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SaveFileName { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }

        public List<string> ErrorMsg { get; set; }

        public TaskInfoDialogViewModel TaskInfoDialog { get; set; }
        public ExportRegistration exportRegistration { get; set; }

        public ExportRegistrationTask()
        {
            ErrorMsg = new List<string>();
            exportRegistration = new ExportRegistration();
        }

        public void Ongo()
        {
            try
            {
                TaskInfoDialog = TaskInfoDialogViewModel.getInstance();
                TaskInfoDialog.Messages.Add("开始导出项目：" + Project.ProjectName);


                Task task = new Task(() =>
                {
                    try
                    {
                        exportRegistration.SaveFileName = SaveFileName;
                        exportRegistration.Project = Project;
                        exportRegistration.write();
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg.Add(ex.Message);
                    }
                    ErrorMsg.AddRange(exportRegistration.ErrorMsg);
                });
                task.Start();
                task.ContinueWith(t =>
                {

                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        SynchronizationContext.SetSynchronizationContext(new
                        System.Windows.Threading.DispatcherSynchronizationContext(System.Windows.Application.Current.Dispatcher));
                        SynchronizationContext.Current.Post(pl =>
                        {

                            foreach (var error in ErrorMsg)
                            {
                                TaskInfoDialog.Messages.Add(error);
                            }
                            if (ErrorMsg != null && ErrorMsg.Count > 0)
                                TaskInfoDialog.Messages.Add("导出失败");
                            else
                            {
                                TaskInfoDialog.Messages.Add("导出成功");
                            }
                        }, null);
                    });

                });

            }
            catch (Exception ex)
            {
                //DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            }
        }
    }
}
