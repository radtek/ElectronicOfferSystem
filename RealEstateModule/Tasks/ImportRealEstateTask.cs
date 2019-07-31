using BusinessData;
using BusinessData.Dal;
using Common.Models;
using Common.ViewModels;
using Common.Views;
using MaterialDesignThemes.Wpf;
using RealEstateModule.Services.Import;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateModule.Tasks
{
    public class ImportRealEstateTask
    {
        public string FullPath { get; set; }
        public List<string> ErrorMsg { get; set; }
        public TaskInfoDialogViewModel TaskInfoDialog { get; set; }

        public ImportRealEstateTask()
        {
            ErrorMsg = new List<string>();
        }

        public void Ongo()
        {
            try
            {
                TaskInfoDialog = TaskInfoDialogViewModel.getInstance();
                String FileName = Path.GetFileName(FullPath);
                //TaskInfoDialog.Messages.Add("开始导入：" + FileName);
                TaskMessage taskMessage = new TaskMessage();
                taskMessage.Title = "导入项目：" + FileName;
                taskMessage.Progress = 0.0;
                TaskInfoDialog.Messages.Insert(0, taskMessage);
                Task task = new Task(() =>
                {
                    ImportRealEstateBook import = new ImportRealEstateBook();
                    import.FileName = FullPath;
                    import.Read();

                    bool canContinue = import.ReadInformation();

                    if (canContinue)
                    {
                        taskMessage.Progress = 50.00;
                        //var naturalEffective = NaturalEffective(import.ZRZList);
                        var isNaturalBuildingUnique = IsNaturalBuildingUnique(import.NaturalBuildings);
                        var isHouseholdUnique = IsHouseholdUnique(import.Households);
                        if (isNaturalBuildingUnique && isHouseholdUnique)
                        {

                            Project project = InitialProject();
                            ProjectDal projectDal = new ProjectDal();
                            NaturalBuildingDal naturalBuildingDal = new NaturalBuildingDal();
                            LogicalBuildingDal logicalBuildingDal = new LogicalBuildingDal();
                            FloorDal floorDal = new FloorDal();
                            HouseholdDal householdDal = new HouseholdDal();
                            ObligeeDal obligeeDal = new ObligeeDal();
                            try
                            {
                                foreach (var naturalBuilding in import.NaturalBuildings)
                                {
                                    naturalBuilding.ID = Guid.NewGuid();
                                    naturalBuilding.ProjectID = project.ID;
                                    naturalBuilding.UpdateTime = DateTime.Now;
                                    naturalBuildingDal.Add(naturalBuilding);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMsg.Add("自然幢数据异常：" + ex.Message);
                            }
                            try
                            { 
                                foreach (var logicalBuilding in import.LogicalBuildings)
                                {
                                    logicalBuilding.ID = Guid.NewGuid();
                                    logicalBuilding.ProjectID = project.ID;
                                    logicalBuilding.UpdateTime = DateTime.Now;
                                    logicalBuildingDal.Add(logicalBuilding);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMsg.Add("逻辑幢数据异常：" + ex.Message);
                            }
                            try
                            { 
                                foreach (var floor in import.Floors)
                                {
                                    floor.ID = Guid.NewGuid();
                                    floor.ProjectID = project.ID;
                                    floor.UpdateTime = DateTime.Now;
                                    floorDal.Add(floor);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMsg.Add("层数据异常：" + ex.Message);
                            }
                            try
                            {
                                foreach (var household in import.Households)
                                {
                                    household.ID = Guid.NewGuid();
                                    household.ProjectID = project.ID;
                                    household.UpdateTime = DateTime.Now;
                                    householdDal.Add(household);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMsg.Add("户数据异常：" + ex.Message);
                            }
                            try
                            { 
                                foreach (var obligee in import.Obligees)
                                {
                                    obligee.ID = Guid.NewGuid();
                                    obligee.ProjectID = project.ID;
                                    obligee.UpdateTime = DateTime.Now;
                                    obligeeDal.Add(obligee);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMsg.Add("权利人数据异常：" + ex.Message);
                            }
                            try
                            { 
                                projectDal.Add(project);
                            }
                            catch (Exception ex)
                            {
                                ErrorMsg.Add("项目数据异常：" + ex.Message);
                            }

                        }

                    }
                    ErrorMsg.AddRange(import.ErrorMsg);

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
                             taskMessage.DetailMessages.Add("导入失败");
                         }
                         else
                         {
                             taskMessage.Progress = 1.00;
                             taskMessage.DetailMessages.Add("导入成功");
                             // 刷新项目列表

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

        /// <summary>
        /// 判断自然幢号是否唯一
        /// </summary>
        /// <param name="naturalBuildings"></param>
        /// <returns></returns>
        private bool IsNaturalBuildingUnique(List<NaturalBuilding> naturalBuildings)
        {
            bool isUnique = true;
            if (naturalBuildings == null || naturalBuildings.Count == 0)
            {
                //ErrorList.Add("自然幢为空");
                return true;
            }
            var zrzh = naturalBuildings.GroupBy(x => x.ZRZH).Where(x => x.Count() > 1).ToList();
            if (zrzh != null && zrzh.Count > 0)
            {
                foreach (var item in zrzh)
                {
                    ErrorMsg.Add(string.Format("自然幢号为：{0}的数据重复", item));
                }
                isUnique = false;
            }
            return isUnique;
        }

        /// <summary>
        /// 判断户标识嘛是否唯一
        /// </summary>
        /// <param name="households"></param>
        /// <returns></returns>
        private bool IsHouseholdUnique(List<Household> households)
        {
            bool isUnique = true;
            if (households == null || households.Count == 0)
            {
                //ErrorList.Add("户数据为空");
                return true;
            }
            var hbsmList = households.GroupBy(x => x.HBSM).Where(x => x.Count() > 1).ToList();
            if (hbsmList != null && hbsmList.Count > 0)
            {
                foreach (var hbsm in hbsmList)
                {
                    ErrorMsg.Add(string.Format("户标识码为：{0}的数据重复", hbsm));
                }
                isUnique = false;
            }
            return isUnique;
        }

        private Project InitialProject()
        {
            //int start = FullPath.LastIndexOf("\\");
            //int end = FullPath.LastIndexOf(".");
            //String projectName = FullPath.Substring(start + 1, end - start - 1);
            String projectName = Path.GetFileNameWithoutExtension(FullPath);
            Project project = new Project();
            project.ID = Guid.NewGuid();
            project.ProjectName = projectName;
            project.UptateTime = DateTime.Now;
            project.Type = "1";
            project.State = "0";

            return project;
        }
    }
}
