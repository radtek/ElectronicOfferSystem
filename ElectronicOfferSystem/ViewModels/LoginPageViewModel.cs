using BusinessData;
using BusinessData.Dal;
using Common.Configurations;
using Common.Utils;
using ElectronicOfferSystem.Views;
using OAUS.Core;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ElectronicOfferSystem.ViewModels
{
    public class LoginPageViewModel : BindableBase
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
            set { SetProperty(ref account, value); IsCancel = true; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); IsCancel = true; }
        }

        public string UpdateIP { get; set; }
        public string UpdatePort { get; set; }

        #region 命令
        public DelegateCommand SignCommand { get; set; }
        public DelegateCommand ExitCommand { get; set; }
        #endregion

        UserInfoDal UserInfoDal = new UserInfoDal();

        public LoginPageViewModel(IRegionManager regionManager)
        {
            // 读取配置参数
            try
            {
                ReadConfigInfo();
            }
            catch (Exception ex)
            {
                Report = ex.Message;
            }


            // 初始窗体样式
            InitalWindowStyle();

            // 默认选中记住我
            //UserChecked = true;

            IsCancel = true;

            RegionManager = regionManager;


            SignCommand = new DelegateCommand(Login);

            ExitCommand = new DelegateCommand(() =>
            {
                App.Current.Shutdown();
            });

            Task.Factory.StartNew((_this) =>
            {
                try
                {
                    if (VersionHelper.HasNewVersion(UpdateIP, Int32.Parse(UpdatePort)))
                    {
                        MessageBoxResult result = MessageBox.Show("发现新版本，是否更新？", "提示信息", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            App.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                App.Current.Shutdown();
                            }));
                            
                            string updateExePath = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdater\\AutoUpdater.exe";
                            System.Diagnostics.Process myProcess = System.Diagnostics.Process.Start(updateExePath);
                        }
                    }
                }
                catch
                {
                    //MessageBox.Show("由于网络原因无法检查版本更新");
                }

            }, this);
        }

        public void InitalWindowStyle()
        {
            Window mainWindow = Application.Current.Windows[0];
            mainWindow.WindowStyle = WindowStyle.None;
            mainWindow.ResizeMode = ResizeMode.NoResize;
            mainWindow.WindowState = WindowState.Normal;
            mainWindow.Width = 400;
            mainWindow.Height = 300;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //mainWindow.AllowsTransparency = true;
            //BrushConverter brushConverter = new BrushConverter();
            //Brush brush = (Brush)brushConverter.ConvertFromString("Transparent");
            //mainWindow.Background = brush;
        }

        /// <summary>
        /// 登录系统
        /// </summary>
        private void Login()
        {
            IsCancel = false;
            if (string.IsNullOrWhiteSpace(Account))
            {
                Report = "请输入账号";
                return;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                Report = "请输入密码";
                return;
            }
            try
            {
                bool canLogin = false;
                Task task = new Task(() =>
                    {
                        //UserInfo u = new UserInfo();
                        //u.ID = Guid.NewGuid();
                        //u.Account = "admin";
                        //u.UserName = "管理员";
                        //u.Password = CEncoder.Encode("123456");
                        //u.CreateTime = DateTime.Now;
                        //u.IsAdmin = "1";
                        //UserInfoDal.Add(u);

                        App.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            Report = "正在验证登录 . . .";
                        }));
                        UserInfo user = new UserInfo();
                        user = UserInfoDal.GetModel(u => u.Account.Equals(Account));
                        if (user == null)
                        {
                            App.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                Report = "账号不存在，请确认";
                            }));
                            return;
                        }

                        user = UserInfoDal.Login(Account, CEncoder.Encode(Password));
                        if (user == null)
                        {
                            App.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                Report = "账号或密码错误，请确认";
                            }));
                        }
                        else
                        {
                            // 保存登录信息
                            try
                            {
                                SaveLoginInfo();
                            }
                            catch (Exception ex)
                            {
                                App.Current.Dispatcher.Invoke((Action)(() =>
                                {
                                    Report = ex.Message;
                                }));
                                return;
                            }

                            App.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                Report = "初始化首页 . . .";
                            }));

                            App.Current.Properties["User"] = user;
                            canLogin = true;
                            App.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                Application.Current.Windows[0].Visibility = Visibility.Hidden;
                            }));
                        }
                    });
                task.Start();
                task.ContinueWith(t =>
                {
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        if (canLogin)
                        {
                            RegionManager.RequestNavigate("MainRegion", "MainPage", NavigationComplete);
                        }
                    }));


                });

            }
            catch (Exception ex)
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    Report = ex.Message;
                }));

            }
            finally
            {

            }
        }

        private void NavigationComplete(NavigationResult obj)
        {
        }

        /// <summary>
        /// 读取本地配置信息
        /// </summary>
        public void ReadConfigInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + LocalConfiguration.INI_CFG;
            if (File.Exists(cfgINI))
            {
                IniFileHelper ini = new IniFileHelper(cfgINI);
                Account = ini.IniReadValue("Login", "Account");
                Password = CEncoder.Decode(ini.IniReadValue("Login", "Password"));
                UserChecked = ini.IniReadValue("Login", "SaveInfo") == "Y";
                SkinName = ini.IniReadValue("Skin", "Skin");
                UpdateIP = ini.IniReadValue("OAUS", "UpdateIP");
                UpdatePort = ini.IniReadValue("OAUS", "UpdatePort");
            }
        }

        /// <summary>
        /// 保存登录信息
        /// </summary>
        private void SaveLoginInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + LocalConfiguration.INI_CFG;
            IniFileHelper ini = new IniFileHelper(cfgINI);
            ini.IniWriteValue("Login", "Account", Account);
            if (UserChecked)
            {
                ini.IniWriteValue("Login", "Password", CEncoder.Encode(Password));
                ini.IniWriteValue("Login", "SaveInfo", "Y");
            }
            else
            {
                ini.IniWriteValue("Login", "Password", string.Empty);
                ini.IniWriteValue("Login", "SaveInfo", "N");
            }

        }
    }
}
