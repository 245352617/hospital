
using DevExpress.XtraReports.Expressions.Native;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace YiJian.CardReader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        System.Threading.Mutex mutex;
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;
            _log.Error("程序发生了未处理的异常：" + exception.Message);
        }


        void App_Startup(object sender, StartupEventArgs e)
        {
            bool ret;
            mutex = new System.Threading.Mutex(true, "WpfMuerterrrterterttex", out ret);
            if (!ret)
            {
                MessageBox.Show("急诊托盘程序已经启动，不用再次启动", "急诊托盘程序", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                Environment.Exit(0);
            }
        }
    }
}