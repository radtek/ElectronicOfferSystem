using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Prism.Modularity;
using ElectronicOfferSystem.Views;
using System;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Text;
using Common.ViewModels;
using ElectronicOfferSystem.ViewModels;

namespace ElectronicOfferSystem
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            // 首先注册开始和退出事件
            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_Exit);
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
        }

        /// <summary>
        /// 配置模块
        /// </summary>
        /// <param name="moduleCatalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<RealEstateModule.RealEstateModule>();
            moduleCatalog.AddModule<RegistrationModule.RegistrationModule>();
            moduleCatalog.AddModule<Common.CommonModule>();
            moduleCatalog.AddModule<ProjectModule.ProjectModule>();
        }

        /// <summary>
        /// 启动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_Startup(object sender, StartupEventArgs e)
        {

            //UI线程未捕获异常处理事件
            this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }
        /// <summary>
        /// UI线程未捕获异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true; //把 Handled 属性设为true，表示此异常已处理，程序可以继续运行，不会强制退出      
                //MessageBox.Show("捕获未处理异常:" + e.Exception.Message);
                ErrorDialogViewModel.getInstance().show("捕获未处理异常："+e.Exception.Message , e.Exception.StackTrace);
            }
            catch (Exception)
            {
                //此时程序出现严重异常，将强制结束退出
                MessageBox.Show("程序发生致命错误，将终止，请联系管理员！");
            }
        }
        /// <summary>
        /// 非UI线程未捕获异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder sbEx = new StringBuilder();
            if (e.IsTerminating)
            {
                sbEx.Append("程序发生致命错误，将终止，请联系管理员！\n");
            }
            sbEx.Append("捕获未处理异常：");
            if (e.ExceptionObject is Exception)
            {
                sbEx.Append(((Exception)e.ExceptionObject).Message);
            }
            else
            {
                sbEx.Append(e.ExceptionObject);
            }
            MessageBox.Show(sbEx.ToString());
        }
        /// <summary>
        /// Task线程内未捕获异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //task线程内未处理捕获
            //MessageBox.Show("捕获线程内未处理异常：" + e.Exception.Message);
            ErrorDialogViewModel.getInstance().show("捕获线程内未处理异常：" + e.Exception.Message, e.Exception.StackTrace);
            e.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
        }



        /// <summary>
        /// 退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_Exit(object sender, ExitEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
