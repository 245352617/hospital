using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tray.Install.Models;
using Tray.Install.Utils;
using Newtonsoft.Json;
using System.IO.Compression;
using System.DirectoryServices.ActiveDirectory;
using HandyControl.Tools.Converter;
using System.Diagnostics;
using System.IO;
using System.Windows.Threading;

namespace Tray.Install
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MinioSetting _minioSetting;
        private readonly string _version;

        public MainWindow()
        {
            InitializeComponent();
            _minioSetting = new MinioSetting {
                 Endpoint = ConfigurationManager.AppSettings["MinioSetting.Endpoint"] ?? "192.168.241.101:9000",
                 AccessKey = ConfigurationManager.AppSettings["MinioSetting.AccessKey"] ?? "admin",
                 SecretKey = ConfigurationManager.AppSettings["MinioSetting.SecretKey"] ?? "admin123",
                 Bucket = ConfigurationManager.AppSettings["MinioSetting.Bucket"] ?? "tray",
            };
            _version = ConfigurationManager.AppSettings["Version"] ?? "v2.1.0.0";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            var ret = Dispatcher.InvokeAsync(new Action(async () =>
            {
                await UpdateAsync();
            }), DispatcherPriority.Background); 
        } 

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        private async Task UpdateAsync()
        {
            try
            {
                // 全部转成同步，方便进度条跟进
                //1. 获取版本文件，对比版本看是否需要下载
                //2. 如果需要下载更新，这下载更新包
                //3. 备份老版本
                //4. 删除解压缩文件里的对应的所有文件（老版本）
                //5. 解压缩新版本
                //6. 启动tray 程序进程
                //7. 更新本地版本信息
                //8. 关闭本进程
                 
                var versionInfo = await GetVersionInfoAsync();
                var zipfile = await DownloadUpdateFileAsync(versionInfo); 
                _ = Backups();
                UnZip(zipfile);
                UpdateVersion(versionInfo);
                StartTray();
                MessageBox.Show("更新完毕", "更新提示", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "更新提示", MessageBoxButton.OK, MessageBoxImage.Information);
                StartTray();
                Close(); 
            }
        }

        /// <summary>
        /// 启动Tray程序
        /// </summary>
        private void StartTray()
        {
            string exe = System.IO.Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "Tray.exe");
            Process p = new Process();
            p.StartInfo.FileName = exe;
            p.Start(); 
        } 

        /// <summary>
        /// 更新本地的版本信息
        /// </summary>
        /// <param name="versionInfo"></param>
        private void UpdateVersion(UpdateVersionInfo versionInfo)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var settings = (string key, string value) =>
            {
                if (!config.AppSettings.Settings.AllKeys.Contains(key)) config.AppSettings.Settings.Add(new KeyValueConfigurationElement(key, value));
                else config.AppSettings.Settings[key].Value = value;
            };

            settings("Version", versionInfo.Version); 
            config.Save();
        }

        /// <summary>
        /// 备份
        /// </summary>
        /// <returns></returns>
        private string Backups()
        { 
            var currentDir = AppDomain.CurrentDomain.BaseDirectory;
            var zipFile = ZipUtils.Zip(currentDir,"traybak.zip");
            return zipFile; 
        }

        /// <summary>
        /// 对比并且解压缩文件
        /// </summary>
        /// <returns></returns>
        private void UnZip(string zipFile)
        {
            var currentDir = AppDomain.CurrentDomain.BaseDirectory;
            ZipUtils.UnZip(zipFile, currentDir);
        }

        /// <summary>
        /// 下载更新文件
        /// </summary>
        /// <returns></returns>
        private async Task<string> DownloadUpdateFileAsync(UpdateVersionInfo versionInfo)
        {  
            var v1 = new Version(_version.Trim('v'));
            var v2 = new Version(versionInfo.Version.Trim('v'));
            if (v1 >= v2) throw new Exception("没有新的更新包");

            string zipFileUrl = await MinioUtils.GetUrlAsync(_minioSetting, versionInfo.UpdateFile);
            var file = await  HttpClientUtils.DownloadAsync(zipFileUrl,versionInfo.UpdateFile);
            return file;
        }

        /// <summary>
        /// 获取更新版本信息
        /// </summary>
        /// <returns></returns>
        private async Task<UpdateVersionInfo> GetVersionInfoAsync()
        {
            string jsonFileUrl = await MinioUtils.GetUrlAsync(_minioSetting, "update.json");
            var txt = await HttpClientUtils.GetAsync(jsonFileUrl);
            if (string.IsNullOrEmpty(txt)) throw new Exception("没有找到更新信息");
            var versionInfo = JsonConvert.DeserializeObject<UpdateVersionInfo>(txt);
            if (versionInfo == null) throw new Exception("反序列化更新配置信息异常"); 
            return versionInfo;
        }

    }
}
