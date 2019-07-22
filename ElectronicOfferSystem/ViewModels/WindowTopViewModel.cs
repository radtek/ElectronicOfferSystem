using BusinessData;
using BusinessData.Dal;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicOfferSystem.ViewModels
{
    class WindowTopViewModel : BindableBase
    {
        public DelegateCommand MinCommand { get; set; }
        public DelegateCommand MaxCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand ImportCommand { get; set; }
        public InteractionRequest<IConfirmation> ConfirmCloseRequest { get; set; }
        public DelegateCommand ConfirmCloseCommand { get; set; }

        public WindowTopViewModel()
        {
            ImportCommand = new DelegateCommand(ImportData);
            CloseCommand = new DelegateCommand(() => {
                Application.Current.Shutdown();
            });
            MinCommand = new DelegateCommand(() => {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;  
            });
            MaxCommand = new DelegateCommand(() => {
                if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                {
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                }
                else
                {
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                }
            });
            ConfirmCloseRequest = new InteractionRequest<IConfirmation>();
            ConfirmCloseCommand = new DelegateCommand(RaiseConfirmClose);
        }

        private void RaiseConfirmClose()
        {
            ConfirmCloseRequest.Raise(new Confirmation { Title = "确认消息", Content = "请确认退出系统？" }, r => {
                if (r.Confirmed) CloseCommand.Execute();
                else return;

            });
        }

        private void ImportData()
        {
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
                Console.WriteLine("字典目录表：进度"+per.ToString("#0.#0") + "%");
            }

            index = 0;
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
                Console.WriteLine("字典表：进度" + per.ToString("#0.#0") + "%");
            }
        }
    }
}
