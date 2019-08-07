using BusinessData;
using BusinessData.Dal;
using Common.Models;
using Common.Utils;
using Common.ViewModels;
using RealEstateModule.Services.Export;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RealEstateModule.Tasks
{
    public class ExportRealEstateTask
    {
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SaveFileName { get; set; }

        public string TemplateFileName { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }

        public List<string> ErrorMsg { get; set; }

        public TaskInfoDialogViewModel TaskInfoDialog { get; set; }

        public ExportRealEstateBook book { get; set; }

        public ExportRealEstateTask()
        {
            ErrorMsg = new List<string>();
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

                NaturalBuildingDal naturalBuildingDal = new NaturalBuildingDal();
                LogicalBuildingDal logicalBuildingDal = new LogicalBuildingDal();
                FloorDal floorDal = new FloorDal();
                HouseholdDal householdDal = new HouseholdDal();
                ObligeeDal obligeeDal = new ObligeeDal();
                MortgageDal mortgageDal = new MortgageDal();
                SequestrationDal sequestrationDal = new SequestrationDal();

                Task task = new Task(() =>
                {
                    try
                    {
                        book.TaskMessage = taskMessage;
                        book.NaturalBuildings = naturalBuildingDal.GetListBy(t => t.ProjectID == Project.ID);
                        book.LogicalBuildings = logicalBuildingDal.GetListBy(t => t.ProjectID == Project.ID);
                        book.Floors = floorDal.GetListBy(t => t.ProjectID == Project.ID);
                        book.Households = householdDal.GetListBy(t => t.ProjectID == Project.ID);
                        book.Obligees = obligeeDal.GetListBy(t => t.ProjectID == Project.ID);
                        if ("2".Equals(Project.OwnershipType))
                        {
                            book.Mortgages = mortgageDal.GetListBy(t => t.ProjectID == Project.ID);
                            book.Sequestrations = sequestrationDal.GetListBy(t => t.ProjectID == Project.ID);
                        }
                        
                        book.Open(TemplateFileName);
                        book.Write();
                        //book.SaveAsExcel(SaveFileName);
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg.Add(ex.Message);
                    }
                    ErrorMsg.AddRange(book.ErrorMsg);
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
                                string bufferPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Buffer\" + Path.GetFileName(SaveFileName);
                                book.SaveAsExcel(bufferPath);

                                //// 压缩成报盘
                                //ZipHelper zipHelper = new ZipHelper();
                                //zipHelper.ZipFile(SaveFileName.Replace(".bpf", ".xls"), SaveFileName, 5, 500);
                                //// 删除excel
                                //File.Delete(SaveFileName.Replace(".bpf", ".xls"));

                                File.Move(bufferPath.Replace(".bpf", ".xls"), SaveFileName.Replace(".bpf", ".xls"));
                                
                                //taskMessage.Progress = 100.00;


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
