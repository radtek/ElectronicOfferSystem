using BusinessData;
using Common.Models;
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
                TaskMessage taskMessage = new TaskMessage();
                taskMessage.Title = "导出项目：" + Project.ProjectName;
                taskMessage.Progress = 0.0;
                TaskInfoDialog.Messages.Insert(0, taskMessage);


                Task task = new Task(() =>
                {
                    try
                    {
                        exportRegistration.TaskMessage = taskMessage;
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
                                taskMessage.DetailMessages.Add(error);
                            }
                            if (ErrorMsg != null && ErrorMsg.Count > 0)
                            {
                                taskMessage.DetailMessages.Add("导出失败");
                            }
                            else
                            {
                                taskMessage.Progress = 100.00;
                                taskMessage.DetailMessages.Add("导出成功");
                            }
                        }, null);
                    });

                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
