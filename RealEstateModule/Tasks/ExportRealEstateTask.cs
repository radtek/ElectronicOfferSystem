using BusinessData;
using BusinessData.Dal;
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
                //TaskInfoDialog.Messages.Add("开始导出项目：" + Project.ProjectName);

                NaturalBuildingDal naturalBuildingDal = new NaturalBuildingDal();
                LogicalBuildingDal logicalBuildingDal = new LogicalBuildingDal();
                FloorDal floorDal = new FloorDal();
                HouseholdDal householdDal = new HouseholdDal();
                ObligeeDal obligeeDal = new ObligeeDal();

                Task task = new Task(() =>
                {
                    try
                    {
                        book.NaturalBuildings = naturalBuildingDal.GetListBy(n => n.ProjectID == Project.ID);
                        book.LogicalBuildings = logicalBuildingDal.GetListBy(l => l.ProjectID == Project.ID);
                        book.Floors = floorDal.GetListBy(f => f.ProjectID == Project.ID);
                        book.Households = householdDal.GetListBy(h => h.ProjectID == Project.ID);
                        book.Obligees = obligeeDal.GetListBy(o => o.ProjectID == Project.ID);

                        book.Open(TemplateFileName);
                        book.Write();
                        book.SaveAsExcel(SaveFileName);
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
                                //TaskInfoDialog.Messages.Add(error);
                            }
                            if (ErrorMsg != null && ErrorMsg.Count > 0)
                            {
                                //TaskInfoDialog.Messages.Add("导出失败");
                            }
                            else
                            {
                                // 压缩成报盘
                                ZipHelper zipHelper = new ZipHelper();
                                //zipClass.ZipFile(SaveFileName, filename + ".bpf", 5, 500);
                                zipHelper.ZipFile(SaveFileName.Replace(".bpf", ".xls"), SaveFileName, 5, 500);
                                // 删除excel
                                File.Delete(SaveFileName.Replace(".bpf", ".xls"));
                                //TaskInfoDialog.Messages.Add("导出成功");
                                //errordoalog.CloseDialog();
                            }
                        }, null);
                    });
                   
                });

            }
            catch (Exception ex)
            {
                //DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
                throw ex;
            }
        }
    }
}
