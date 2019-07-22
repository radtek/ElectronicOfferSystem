using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateModule.Tasks
{
    class ImportRealEstateTask
    {
        public string FullPath { get; set; }
        public List<string> ErrorMsg { get; set; }

        public ImportRealEstateTask()
        {
            ErrorMsg = new List<string>();
        }

        public void Ongo()
        {
            try
            {
                //ErrorInfomationDialog errordoalog = new ErrorInfomationDialog();
                //errordoalog.Show();
                //errordoalog.SetRrrorText("开始导入\n");
                Task task = new Task(() =>
                {
                    ImportHosueTable import = new ImportHosueTable();
                    import.FileName = FileName;
                    import.Read();
                    var canContinue = import.ReadInformation();
                    if (canContinue)
                    {
                        //var naturalEffective = NaturalEffective(import.ZRZList);
                        var naturalNumberBool = NaturalNumberBool(import.ZRZList);
                        var houseLogoBool = HouseLogoBool(import.HList);
                        if (naturalNumberBool && houseLogoBool)
                        {
                            Xmxx xmxx = CreatXmxx(import.ZRZList);
                            CService.AddRangC(import.CList, xmxx.Xmbh);
                            HService.AddRangH(import.HList, xmxx.Xmbh);
                            ZrzService.AddRangZRZ(import.ZRZList, xmxx.Xmbh);
                            LjzService.AddRangLjz(import.LJZList, xmxx.Xmbh);
                            QlrService.AddRangQLR(import.QLRList, xmxx.Xmbh);
                            XmxxService.InsertXmxx(xmxx);
                        }
                    }
                    //ErrorList = import.ErrorList;
                    ErrorList.AddRange(import.ErrorList);

                });
                task.Start();
                task.ContinueWith(t =>
                {
                    foreach (var error in ErrorList)
                    {
                        errordoalog.SetRrrorText(error + "\n");
                    }
                    if (ErrorList != null && ErrorList.Count > 0)
                        errordoalog.SetRrrorText("导入失败");
                    else
                    {
                        errordoalog.SetRrrorText("导入成功");
                        // 刷新项目列表
                        Node node = MainForm.mainForm.node1;
                        Xmxx xmxx1 = new Xmxx
                        {
                            Xmzl = "1"
                        };
                        List<Xmxx> xmxxList = XmxxService.SelectXmxx(xmxx1);
                        node.Nodes.Clear();
                        foreach (Xmxx xm in xmxxList)
                        {
                            node.Nodes.Add(new Node(xm.Xmmc));
                        }

                    }
                });

            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            }
        }
    }
}
