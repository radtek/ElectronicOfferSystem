using BusinessData;
using BusinessData.Dal;
using Common;
using Common.Configurations;
using Common.Enums;
using Common.Models;
using Common.Utils;
using Common.ViewModels;
using Common.Views;
using ElectronicOfferSystem.ViewModels.Dialogs;
using ElectronicOfferSystem.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicOfferSystem.ViewModels
{
    public class IndexPageViewModel : BindableBase
    {

        public DelegateCommand<EMainPage?> NavigateCommand { get; set; }
        public DelegateCommand ImportDictionaryCommand { get; set; }
        public DelegateCommand OpenProjectPathDialogCommand { get; set; }

        ProjectPathDialogViewModel ProjectPathDialogViewModel;

        public IndexPageViewModel()
        {
            // 页面导航
            NavigateCommand = new DelegateCommand<EMainPage?>(Navigate);
            GlobalCommands.NavigateCommand.RegisterCommand(NavigateCommand);

            OpenProjectPathDialogCommand = new DelegateCommand(ExecuteProjectPathDialog);
            ImportDictionaryCommand = new DelegateCommand(ImportDictionary);
        }


        private void Navigate(EMainPage? obj)
        {
        }


        /// <summary>
        /// 打开导入框
        /// </summary>
        private async void ExecuteProjectPathDialog()
        {
            var view = new ProjectPathDialog();
            ProjectPathDialogViewModel = new ProjectPathDialogViewModel();
            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ConfirSaveProjectPathHandler);

        }
        /// <summary>
        /// 点击按钮，确认/取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ConfirSaveProjectPathHandler(object sender, DialogClosingEventArgs eventArgs)
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

            try
            {
                string cfgINI = AppDomain.CurrentDomain.BaseDirectory + LocalConfiguration.INI_CFG;
                IniFileHelper ini = new IniFileHelper(cfgINI);
                ini.IniWriteValue("Project", "FilePath", FullPath);

                // 显示加载1s
                eventArgs.Session.UpdateContent(new SampleProgressDialog());
                Task.Delay(TimeSpan.FromSeconds(0.3))
                    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                        TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().updateShow(ex, eventArgs.Session);
                return;
            }

        }


        private void ImportDictionary()
        {
            var view = new TaskInfoDialog();
            var result = DialogHost.Show(view, "RootDialog");

            TaskInfoDialogViewModel taskInfoDialog = TaskInfoDialogViewModel.getInstance();
            TaskMessage taskMessage = new TaskMessage();
            taskMessage.Title = "执行导入字典目录任务";
            taskMessage.Progress = 0.0;
            taskInfoDialog.Messages.Add(taskMessage);
            Task task = new Task(() =>
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    taskMessage.DetailMessages.Add("开始导入。。");
                }));
                
                // 导入BDCS_CONSTCLS
                BaseDal<CONSTCLS> baseDal = new BaseDal<CONSTCLS>();
                StreamReader sr = new StreamReader(@"C:\Users\Administrator\Desktop\BDCS_CONSTCLS 1.txt", Encoding.Default);
                String line;
                int index = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = line.Split(',');
                    CONSTCLS c = new CONSTCLS();
                    c.MBBSM = int.Parse(s[0].Trim());
                    c.CONSTSLSID = int.Parse(s[1].Trim());
                    c.CONSTCLSNAME = s[2].Trim();
                    c.CONSTCLSTYPE = s[3].Trim();
                    c.BZ = s[4].Trim();
                    baseDal.Add(c);
                    index++;
                    double per = index / 79.0 * 100;
                    taskMessage.Progress = per;
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        taskMessage.DetailMessages.Add("字典目录表：进度" + per.ToString("#0.#0") + "%");
                    }));
                }

                index = 0;
                taskMessage.Progress = 0.0;
                BaseDal<CONST> baseDal2 = new BaseDal<CONST>();
                StreamReader sr2 = new StreamReader(@"C:\Users\Administrator\Desktop\BDCS_CONST 1.txt", Encoding.Default);
                String line2;
                while ((line2 = sr2.ReadLine()) != null)
                {
                    string[] s = line2.Split(',');
                    CONST c = new CONST();
                    c.MBBSM = int.Parse(s[0].Trim());
                    c.CONSTSLSID = int.Parse(s[1].Trim());
                    c.CONSTVALUE = s[2].Trim();
                    c.CONSTTRANS = s[3].Trim();
                    if (s[4].Trim() != "")
                        c.PARENTNODE = int.Parse(s[4].Trim());
                    else
                        c.PARENTNODE = null;
                    if (s[5].Trim() != "")
                        c.CONSTORDER = int.Parse(s[5].Trim());
                    else
                        c.CONSTORDER = null;
                    c.BZ = s[6].Trim();
                    c.CREATETIME = null;
                    c.MODIFYTIME = null;
                    c.REPORTVALUE = s[9].Trim();
                    c.GJCONSTTRANS = s[10].Trim();
                    c.SFSY = s[11].Trim();
                    c.GJVALUE = s[12].Trim();

                    baseDal2.Add(c);
                    index++;
                    double per = index / 1918.0 * 100;

                    taskMessage.Progress = per;
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        taskMessage.DetailMessages.Add("字典表：进度" + per.ToString("#0.#0") + "%");
                    }));
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
                        taskMessage.DetailMessages.Add("导入完成。");

                    }, null);
                });

            });
        }
    }
}
