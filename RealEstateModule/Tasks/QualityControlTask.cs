using BusinessData;
using BusinessData.Dal;
using Common.Models;
using Common.ViewModels;
using RealEstateModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateModule.Tasks
{
    public class QualityControlTask
    {
        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }

        public List<string> ErrorMsg { get; set; }

        public TaskInfoDialogViewModel TaskInfoDialog { get; set; }

        public ProjectDal ProjectDal { get; set; }

        public QualityControlTask()
        {
            ErrorMsg = new List<string>();
            ProjectDal = new ProjectDal();
        }

        public void Ongo()
        {
            try
            {
                TaskInfoDialog = TaskInfoDialogViewModel.getInstance();
                //TaskInfoDialog.Messages.Add("开始质检项目：" + Project.ProjectName);
                TaskMessage taskMessage = new TaskMessage();
                taskMessage.Title = "质检项目：" + Project.ProjectName;
                taskMessage.Progress = 0.0;
                TaskInfoDialog.Messages.Insert(0, taskMessage);
                Task task = new Task(() =>
                {
                    //Thread.Sleep(2000);
                    
                    QualityControl qualityControl = new QualityControl();
                    qualityControl.Project = ProjectDal.InitialRealEstateProject(Project);
                    qualityControl.TaskMessage = taskMessage;
                    try
                    {
                        ErrorMsg.AddRange(qualityControl.Check());
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg.Add(ex.Message);
                    }
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
                                taskMessage.DetailMessages.Add("质检不通过");
                                Project.State = "0";
                                Project.UptateTime = DateTime.Now;
                                ProjectDal.Modify(Project);
                            }
                            else
                            {

                                taskMessage.DetailMessages.Add("质检合格");
                                Project.State = "1";
                                Project.UptateTime = DateTime.Now;
                                ProjectDal.Modify(Project);
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
