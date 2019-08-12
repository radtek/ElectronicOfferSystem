using BusinessData;
using Common.Configurations;
using Common.Utils;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace ElectronicOfferSystem.ViewModels.Dialogs
{
    public class ServerDialogViewModel : BindableBase
    {
        private string updateIP;

        public string UpdateIP
        {
            get { return updateIP; }
            set { SetProperty(ref updateIP, value); }
        }


        public ServerDialogViewModel()
        {
            ReadConfigInfo();
        }

        /// <summary>
        /// 读取本地配置信息
        /// </summary>
        public void ReadConfigInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + LocalConfiguration.INI_CFG;

            if (System.IO.File.Exists(cfgINI))
            {
                IniFileHelper ini = new IniFileHelper(cfgINI);
                UpdateIP = ini.IniReadValue("OAUS", "UpdateIP");
            }
        }
    }
}
