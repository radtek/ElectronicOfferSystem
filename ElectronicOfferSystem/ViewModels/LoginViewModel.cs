using BusinessData;
using BusinessData.Dal;
using ElectronicOfferSystem.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicOfferSystem.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private IRegionManager RegionManager;

        /// <summary>
        /// 进度报告
        /// </summary>
        private string report;
        public string Report
        {
            get { return report; }
            set { SetProperty(ref report, value); }
        }
        /// <summary>
        /// 皮肤样式
        /// </summary>
        public string SkinName { get; set; }

        /// <summary>
        /// 禁用按钮
        /// </summary>
        private bool isCancel;
        public bool IsCancel
        {
            get { return isCancel; }
            set { SetProperty(ref isCancel, value); }
        }
        /// <summary>
        /// 记住我
        /// </summary>
        private bool userChecked;
        public bool UserChecked
        {
            get { return userChecked; }
            set { SetProperty(ref userChecked, value); }
        }
        /// <summary>
        /// 账号
        /// </summary>
        private string account;
        public string Account
        {
            get { return account; }
            set { SetProperty(ref account, value); }
        }
        /// <summary>
        /// 密码
        /// </summary>
        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }


        #region 命令
        public DelegateCommand SignCommand { get; set; }
        public DelegateCommand ExitCommand { get; set; }
        #endregion

        UserInfoDal UserInfoDal = new UserInfoDal();

        public LoginViewModel(IRegionManager regionManager)
        {
            RegionManager = regionManager;

            IsCancel = true;

            SignCommand = new DelegateCommand(Login);

            ExitCommand = new DelegateCommand(() =>
            {
                Application.Current.Shutdown();
            });
        }

        /// <summary>
        /// 登录系统
        /// </summary>
        private void Login()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Account) && !string.IsNullOrWhiteSpace(Password))
                {

                    //IsCancel = false;
                    Report = "正在验证登录 . . .";
                    UserInfo user = UserInfoDal.Login(Account, Password);
                    if (user == null)
                    {
                        Report = "账号或密码错误，请确认。";
                    }
                    else
                    {
                        //if (UserChecked) SaveLoginInfo();

                        this.Report = "初始化首页 . . .";
                        MainWindow mainWindow = new MainWindow(RegionManager);
                        //Application.Current.MainWindow = mainWindow;
                        //mainWindow.Activate();
                        mainWindow.Show();
                        //App app = new App();
                        //Application.Current.Shutdown();
                        

    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 读取本地配置信息
        /// </summary>
        //public void ReadConfigInfo()
        //{
        //    string cfgINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.INI_CFG;
        //    if (File.Exists(cfgINI))
        //    {
        //        IniFile ini = new IniFile(cfgINI);
        //        UserName = ini.IniReadValue("Login", "User");
        //        Password = CEncoder.Decode(ini.IniReadValue("Login", "Password"));
        //        UserChecked = ini.IniReadValue("Login", "SaveInfo") == "Y";
        //        _SkinName = ini.IniReadValue("Skin", "Skin");
        //    }
        //}

        /// <summary>
        /// 保存登录信息
        /// </summary>
        //private void SaveLoginInfo()
        //{
        //    string cfgINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.INI_CFG;
        //    IniFile ini = new IniFile(cfgINI);
        //    ini.IniWriteValue("Login", "User", UserName);
        //    ini.IniWriteValue("Login", "Password", CEncoder.Encode(Password));
        //    ini.IniWriteValue("Login", "SaveInfo", UserChecked ? "Y" : "N");
        //}
    }

}
