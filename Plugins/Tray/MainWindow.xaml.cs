using DevExpress.DataProcessing;
using DevExpress.Utils.Extensions;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tray.HttpServer;
using Tray.ReportCard;
using YiJian.CardReader.CardReaders;
using YiJian.CardReader.Domain;
using YiJian.CardReader.ViewModels;
using YiJian.CardReader.WebServer;
using YiJian.Tray.Utils;
using static System.Drawing.Printing.PrinterSettings;

namespace YiJian.CardReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DeviceWebSocketServer? _deviceWebSocketServer;
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();
        public static NotificationPopup? NotificationPopup { get; set; }

        private static bool IsFirstRunWebSocket = true;
        private const string DefaultWebSocketIp = "127.0.0.1";
        private const string DefaultWebSocketPort = "23233";
        private const string DefaultStatusBarText = "欢迎使用尚哲托盘程序";
        private readonly LocalHttpServer _localHttpServer = new LocalHttpServer();

        public MainWindow()
        {
            InitializeComponent();
            NotificationPopup = new NotificationPopup("调用程序启动中..", 10);

            // 1. 读取配置项目
            var vendor = ConfigurationManager.AppSettings["DeviceVendor"] ?? string.Empty;
            var bindIp = ConfigurationManager.AppSettings["WebSocketIp"] ?? DefaultWebSocketIp;
            var bindPort = ConfigurationManager.AppSettings["WebSocketPort"] ?? DefaultWebSocketPort;
            var printCenterUrl = ConfigurationManager.AppSettings["PrintCenterUrl"] ?? string.Empty;
            var defaultPrinter = ConfigurationManager.AppSettings["DefaultPrinter"] ?? string.Empty;
            var defaultPeperName = ConfigurationManager.AppSettings["DefaultPaperName"] ?? string.Empty;

            //var paperNames = ConfigurationManager.AppSettings["PaperNames"] ?? "A5,A4";
            var defaultDoctorsAdvicePrinter = ConfigurationManager.AppSettings["DefaultDoctorsAdvicePrinter"] ?? string.Empty;
            var defaultDoctorsAdvicePaperName = ConfigurationManager.AppSettings["DefaultDoctorsAdvicePaperName"] ?? string.Empty;
            var defaultEmrPrinter = ConfigurationManager.AppSettings["DefaultEmrPrinter"] ?? string.Empty;
            var defaultEmrPaperName = ConfigurationManager.AppSettings["DefaultEmrPaperName"] ?? string.Empty;

            var pastingBottlesPrinter = ConfigurationManager.AppSettings["PastingBottlesPrinter"] ?? string.Empty;
            var pastingBottlesPaperName = ConfigurationManager.AppSettings["PastingBottlesPaperName"] ?? string.Empty;

            var wristStrapPrinter = ConfigurationManager.AppSettings["WristStrapPrinter"] ?? string.Empty;
            var wristStrapPaperName = ConfigurationManager.AppSettings["WristStrapPaperName"] ?? string.Empty;

            // 默认要调用的程序地址和参数
            var processPath = ConfigurationManager.AppSettings["ProcessPath"] ?? string.Empty;
            var processArgs = ConfigurationManager.AppSettings["ProcessArgs"] ?? string.Empty;
            var iePath = ConfigurationManager.AppSettings["IePath"] ?? string.Empty;

            var printers = new ObservableCollection<string>();
            // 获取本机打印机列表
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }

            //获取纸张大小规格
            var paperNameList = GetPageSizes(defaultPrinter);
            var doctorsAdvicePaperList = GetPageSizes(defaultDoctorsAdvicePrinter);
            var emrPaperList = GetPageSizes(defaultEmrPrinter);
            var pastingBottlesPaperList = GetPageSizes(pastingBottlesPrinter);
            var wristStrapPaperList = GetPageSizes(wristStrapPrinter);


            // 4.设置数据绑定
            DataContext = new SettingViewModel()
            {
                Vendor = vendor,
                WebSocketIp = bindIp,
                WebSocketPort = bindPort,
                PrintCenterUrl = printCenterUrl,

                //默认打印
                DefaultPrinter = defaultPrinter,
                DefaultPaperName = defaultPeperName,
                PeperNames = paperNameList,
                Printers = printers,

                //医嘱打印
                DefaultDoctorsAdvicePrinter = defaultDoctorsAdvicePrinter,
                DoctorsAdvicePrinters = printers,
                DoctorsAdvicePaperNames = doctorsAdvicePaperList,
                DefaultDoctorsAdvicePaperName = defaultDoctorsAdvicePaperName,

                //病历打印
                DefaultEmrPrinter = defaultEmrPrinter,
                EmrPrinters = printers,
                EmrPaperNames = emrPaperList,
                DefaultEmrPaperName = defaultEmrPaperName,

                //贴瓶打印
                PastingBottlesPrinter = pastingBottlesPrinter,
                PastingBottlesPrinters = printers,
                PastingBottlesPaperNames = pastingBottlesPaperList,
                PastingBottlesPaperName = pastingBottlesPaperName,

                //腕带打印
                WristStrapPrinter = wristStrapPrinter,
                WristStrapPrinters = printers,
                WristStrapPaperNames = wristStrapPaperList,
                WristStrapPaperName = wristStrapPaperName,

                //调用其他程序
                ProcessPath = processPath,
                ProcessArgs = processArgs,
                IePath = iePath,
            };

            StartWebsocket();
            IsFirstRunWebSocket = false;

            //更新StatusBar信息为默认信息
            ControlUpdateHelper.TextBlockTextUpdate("StatusBarTxt", DefaultStatusBarText);
        }

        #region websocket
        private void StartWebsocket()
        {
            var model = (SettingViewModel)DataContext;
            // 1. 读取配置项目
            var vendor = model.Vendor ?? string.Empty;
            var bindIp = model.WebSocketIp ?? "127.0.0.1";
            var bindPort = model.WebSocketPort ?? "23233";
            var defaultPrinter = model.DefaultPrinter ?? string.Empty;
            var defaultPaperName = model.DefaultPaperName ?? string.Empty;
            var iePath = model.IePath ?? "C:\\Program Files (x86)\\Internet Explorer\\iexplore.exe";

            // 2. 初始化读卡器
            var reader = CardReaderFactory.Create(vendor);
            if (reader != null)
            {
                // 3. 初始化 WebSocket 服务
                try
                {
                    _deviceWebSocketServer = new DeviceWebSocketServer(bindIp, bindPort, defaultPrinter, reader, iePath);
                    _log.Info("WebSocket初始化完成，监听端口：{Port}", bindPort);
                }
                catch (Exception ex)
                {
                    _log.Error("初始化WebSocket服务失败：{Error}", ex.Message);
                }
            }
            else
            {
                MessageBox.Show("未配置读卡器厂家，请先配置读卡器厂家，然后重启本程序", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // 3. 如前面初始化失败，则单独初始化报卡 WebSocket 服务
            if (_deviceWebSocketServer == null)
            {
                try
                {
                    _deviceWebSocketServer = new DeviceWebSocketServer(bindIp, bindPort);
                    string msg = string.Empty;
                    if (IsFirstRunWebSocket)
                        msg = $"WebSocket初始化完成，监听端口：{bindPort}";
                    else
                        msg = $"WebSocket重新绑定完成，监听端口：{bindPort}";
                    _log.Info(msg);

                    //this.StatusBarTxt.Text = msg + "\r\n";
                    BtnDoListen.Content = "监听中......点击按钮重新监听";
                }
                catch (Exception ex)
                {
                    var msg = $"初始化WebSocket服务失败：{ex.Message}";
                    _log.Error(msg);
                    MessageBox.Show(msg);
                }
            }
        }

        private void BtnDoListen_Click(object sender, RoutedEventArgs e)
        {
            RestartWebsocket();
            IsFirstRunWebSocket = false;
            BtnDoListen.Content = "监听中......点击按钮重新监听";
        }

        private void RestartWebsocket()
        {
            if (_deviceWebSocketServer != null) _deviceWebSocketServer.Dispose();

            // 刷新AppSettings配置节
            ConfigurationManager.RefreshSection("appSettings");

            StartWebsocket();
        }


        /// <summary>
        /// 检验字符串是否 IP 地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsValidIpAddress(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            string[] parts = input.Split('.');
            if (parts.Length != 4)
                return false;

            foreach (string part in parts)
            {
                if (!int.TryParse(part, out int num) || num < 0 || num > 255)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 限制只能输入 0-65535 之间的数字
        /// </summary>
        private void TxtWebSocketPort_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // 正则表达式：只能输入数字，且只能在0~65535之间
            Regex regex = new Regex("[^0-9](2,5)");
            bool isValidInput = !regex.IsMatch(e.Text) && int.TryParse((sender as TextBox)?.Text + e.Text, out int port) && port >= 0 && port <= 65535;
            e.Handled = !isValidInput;
        }
        private void TxtWebSocketPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(((TextBox)sender).Text, out int value))
            {
                if (value < 0 || value > 65535)
                {
                    BtnDoListen.IsEnabled = false;
                }
                else
                {
                    BtnDoListen.IsEnabled = true;
                }
            }
            else
            {
                BtnDoListen.IsEnabled = false;
            }
        }
        #endregion

        #region 窗口操作
        private void MainWnd_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void MainWnd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void MainWnd_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        #endregion

        #region 托盘功能
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var model = (SettingViewModel)DataContext;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DeviceVendor"].Value = model.Vendor;
            config.AppSettings.Settings["WebSocketIp"].Value = model.WebSocketIp;
            config.AppSettings.Settings["WebSocketPort"].Value = model.WebSocketPort;
            config.Save();
            // 重启 WebSocket 服务
            RestartWebsocket();
            MessageBox.Show("配置保存成功，服务已重启", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 重启服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buttong_RestartService(object sender, RoutedEventArgs e)
        {
            this.RestartWebsocket();
            MessageBox.Show(this, "服务已重启", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 更新版本
        /// 需要Tary.Install程序配合，注意Tray.Install程序配置的Minio地址，以及在Minio服务中需要有对应的Bucket及更新文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buttong_Upgrade(object sender, RoutedEventArgs e)
        {
            //this.RestartWebsocket();
            string exe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tray.Install.exe");
            if (!File.Exists(exe))
            {
                MessageBox.Show(this, "还未安装更新程序", "更新提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Process p = new Process();
            p.StartInfo.FileName = exe;
            var started = p.Start();
            this.Close();
        }

        /// <summary>
        /// 打开程序所在目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CurrentDir(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            startInfo.FileName = "explorer.exe";
            startInfo.Arguments = currentDir;
            startInfo.UseShellExecute = false;

            Process process = new Process() { StartInfo = startInfo };
            process.Start();
        }

        /// <summary>
        /// 打开最新Log文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_LastestLog(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EcisTray.log");
            startInfo.FileName = "notepad.exe";
            startInfo.Arguments = logDir;
            startInfo.UseShellExecute = false;

            Process process = new Process() { StartInfo = startInfo };
            process.Start();
        }
        #endregion

        #region 打印相关
        /// <summary>
        /// 保存打印配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSavePrintConfig_Click(object sender, RoutedEventArgs e)
        {
            var model = (SettingViewModel)DataContext;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var SetConfig = (string key, string value) =>
            {
                if (!config.AppSettings.Settings.AllKeys.Contains(key)) config.AppSettings.Settings.Add(new KeyValueConfigurationElement(key, value));
                else config.AppSettings.Settings[key].Value = value;
            };

            SetConfig("PrintCenterUrl", model.PrintCenterUrl.ToString());
            //config.AppSettings.Settings["PrintIp"].Value = model.PrintIp;
            //config.AppSettings.Settings["PrintPort"].Value = model.PrintPort.ToString();
            SetConfig("DefaultPrinter", model.DefaultPrinter.ToString());
            SetConfig("DefaultPaperName", model.DefaultPaperName.ToString());
            config.Save();
            // 重启 WebSocket 服务
            RestartWebsocket();
            MessageBox.Show("配置保存成功，服务已重启", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// 下载打印模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownloadTemplates_Click(object sender, RoutedEventArgs e)
        {
            var model = (SettingViewModel)DataContext;
            var printTemplateList = this.GetPrintTemplateList(model.PrintCenterUrl);
            foreach (var printTemplate in printTemplateList)
            {
                if (printTemplate.TempType == "DevExpress")
                {
                    string downloadUrl = model.PrintCenterUrl + $"/reports/template/{printTemplate.Id}.repx";
                    if (!string.IsNullOrEmpty(printTemplate.Code))
                    {
                        DownloadPrintTemplate(downloadUrl, printTemplate.Code, true);
                    }
                    DownloadPrintTemplate(downloadUrl, printTemplate.TempType + "-" + printTemplate.Id, true);
                }
                if (printTemplate.TempType == "FastReport")
                {
                    string downloadUrl = model.PrintCenterUrl + $"/reports/template/{printTemplate.Id}.frx";
                    DownloadPrintTemplate(downloadUrl, printTemplate.TempType + "-" + printTemplate.Id, false);
                }
            }

            MessageBox.Show("下载成功", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 获取打印模板
        /// </summary>
        /// <param name="printCenterUrl"></param>
        /// <returns></returns>
        private List<PrintTemplate> GetPrintTemplateList(string printCenterUrl)
        {
            string getListUrl = printCenterUrl + "/print-setting/print-setting-list";
            var req = (HttpWebRequest)WebRequest.Create(getListUrl);//请求网络资源
            req.UserAgent = "Mozilla/5.0 (Linux; Android 5.0; SM-G900P Build/LRX21T) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.103 Mobile Safari/537.36";
            var response = (HttpWebResponse)req.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("下载失败，请检查打印中心地址", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Information);
                return new List<PrintTemplate>();
            }
            using var stream = response.GetResponseStream();
            using var streamReader = new StreamReader(stream, Encoding.UTF8);
            StringBuilder sb = new StringBuilder();
            while (streamReader.Peek() >= 0)
            {
                sb.Append(streamReader.ReadLine());
            }
            string responseResult = sb.ToString();
            var jResultPrintTemplates = JsonConvert.DeserializeObject<JsonResult<List<PrintTemplate>>>(responseResult, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            if (jResultPrintTemplates?.Code != 200 || jResultPrintTemplates?.Data == null) return new List<PrintTemplate>();

            return jResultPrintTemplates.Data;
        }

        /// <summary>
        /// 下载打印模板
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        private void DownloadPrintTemplate(string url, string fileName, bool isDevExpress = true)
        {
            HttpWebRequest req = null;
            HttpWebResponse rep = null;
            Stream st = null;
            Stream so = null;
            try
            {
                string reportSaveFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Report");
                if (!Directory.Exists(reportSaveFilePath))
                {
                    Directory.CreateDirectory(reportSaveFilePath);
                }
                string fullPath = Path.Combine(reportSaveFilePath, fileName + ".repx");
                if (!isDevExpress)
                    fullPath = Path.Combine(reportSaveFilePath, fileName + ".frx");

                req = (HttpWebRequest)WebRequest.Create(url);
                //请求网络资源
                req.UserAgent = "Mozilla/5.0 (Linux; Android 5.0; SM-G900P Build/LRX21T) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.103 Mobile Safari/537.36";
                rep = (HttpWebResponse)req.GetResponse();
                //返回Internet资源的响应
                long totalBytes = rep.ContentLength;
                //获取请求返回内容的长度
                st = rep.GetResponseStream();
                //读取服务器的响应资源，以IO流的形式进行读写
                so = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);
                long totalDownloadedbyte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedbyte = osize + totalDownloadedbyte;
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                    //读取当前字节流的总长度
                }
                so.Flush();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (so != null)
                {
                    so.Close();
                    so.Dispose();
                }
                if (st != null)
                {
                    st.Close();
                    st.Dispose();
                }
                if (rep != null)
                {
                    rep.Close();

                }
                if (req != null)
                {
                    req.Abort();
                }

            }
        }

        private void AutoStart_Click(object sender, RoutedEventArgs e)
        {
            //获取要自动运行的应用程序名
            string strName = AppDomain.CurrentDomain.BaseDirectory + "Tray.exe";
            //判断要自动运行的应用程序文件是否存在
            if (!File.Exists(strName))
                return;

            //设置开机自启动
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
            if (registry != null)
            {
                //设置该子项的新的“键值对”
                registry.SetValue("Tray", strName);
                MessageBox.Show(this, "设置成功", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// 打印医嘱打印配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveDoctorsAdviceSetting_Click(object sender, RoutedEventArgs e)
        {
            var model = (SettingViewModel)DataContext;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var SetConfig = (string key, string value) =>
            {
                if (!config.AppSettings.Settings.AllKeys.Contains(key)) config.AppSettings.Settings.Add(new KeyValueConfigurationElement(key, value));
                else config.AppSettings.Settings[key].Value = value;
            };

            SetConfig("DefaultDoctorsAdvicePrinter", model.DefaultDoctorsAdvicePrinter.ToString());
            SetConfig("DefaultDoctorsAdvicePaperName", model.DefaultDoctorsAdvicePaperName.ToString());
            config.Save();
            // 重启 WebSocket 服务
            RestartWebsocket();
            MessageBox.Show("配置保存成功，服务已重启", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        /// <summary>
        /// 保存病历打印配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveEmrSetting_Click(object sender, RoutedEventArgs e)
        {
            var model = (SettingViewModel)DataContext;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var SetConfig = (string key, string value) =>
            {
                if (!config.AppSettings.Settings.AllKeys.Contains(key)) config.AppSettings.Settings.Add(new KeyValueConfigurationElement(key, value));
                else config.AppSettings.Settings[key].Value = value;
            };

            SetConfig("DefaultEmrPrinter", model.DefaultEmrPrinter.ToString());
            SetConfig("DefaultEmrPaperName", model.DefaultEmrPaperName.ToString());
            config.Save();
            // 重启 WebSocket 服务
            RestartWebsocket();
            MessageBox.Show("配置保存成功，服务已重启", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnFileSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "exe files (*.exe)|*.exe|bat files (*.bat)|*.bat|All files (*.*)|*.*";
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string[] fileNames = openFileDialog.FileNames;
                var model = (SettingViewModel)DataContext;
                if (fileNames.Length > 0)
                {
                    model.ProcessPath = fileNames[0];
                }
            }
        }

        /// <summary>
        /// 保存贴瓶配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSavePastingBottlesSetting_Click(object sender, RoutedEventArgs e)
        {
            var model = (SettingViewModel)DataContext;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var SetConfig = (string key, string value) =>
            {
                if (!config.AppSettings.Settings.AllKeys.Contains(key)) config.AppSettings.Settings.Add(new KeyValueConfigurationElement(key, value));
                else config.AppSettings.Settings[key].Value = value;
            };

            SetConfig("PastingBottlesPrinter", model.PastingBottlesPrinter.ToString());
            SetConfig("PastingBottlesPaperName", model.PastingBottlesPaperName.ToString());
            config.Save();
            // 重启 WebSocket 服务
            RestartWebsocket();
            MessageBox.Show("配置保存成功，服务已重启", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 保存腕带配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveWristStrapSetting_Click(object sender, RoutedEventArgs e)
        {
            var model = (SettingViewModel)DataContext;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var SetConfig = (string key, string value) =>
            {
                if (!config.AppSettings.Settings.AllKeys.Contains(key)) config.AppSettings.Settings.Add(new KeyValueConfigurationElement(key, value));
                else config.AppSettings.Settings[key].Value = value;
            };

            SetConfig("WristStrapPrinter", model.WristStrapPrinter.ToString());
            SetConfig("WristStrapPaperName", model.WristStrapPaperName.ToString());
            config.Save();
            // 重启 WebSocket 服务
            RestartWebsocket();
            MessageBox.Show("配置保存成功，服务已重启", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region 调用其他程序
        private void BtnProcessRun_Click(object sender, RoutedEventArgs e)
        {
            NotificationPopup?.ShowNotification();

            var model = (SettingViewModel)DataContext;
            //this.StatusBarTxt.Text = $"运行程序地址：{model.ProcessPath} ，运行参数为：{model.ProcessArgs}";

            Task.Run(() =>
            {
                try
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.BtnProcessRun.IsEnabled = false;
                    });
                    //await Task.Delay(5000); //模拟打开程序占用时间
                    if (RunProgram(model.ProcessPath, model.ProcessArgs) == 0)
                        MessageBox.Show($"顺利执行程序，程序名或程序地址为：{model.ProcessPath}，参数为：{model.ProcessArgs}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"执行程序出错，程序名或程序地址为：{model.ProcessPath}，参数为：{model.ProcessArgs}，错误信息为：{ex.Message}");
                }
                finally
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.BtnProcessRun.IsEnabled = true;
                    });
                }
            });
        }

        /// <summary>
        /// 运行特定程序
        /// </summary>
        /// <param name="programPath">运行程序名或地址</param>
        /// <param name="arguments">参数</param>
        /// <returns></returns>
        private int RunProgram(string programPath, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(programPath);
            startInfo.FileName = programPath;
            startInfo.Arguments = arguments;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            Process process = new Process() { StartInfo = startInfo };
            bool isProcess = process.Start();


            Task.Run(async () =>
            {
                bool isDoTop = false;
                await Task.Delay(4000);
                for (int i = 1; i <= 10; i++)
                {
                    if (isProcess)
                        isDoTop = Tray.ProcessExe.BringWindowToFront();
                    if (isDoTop)
                        return;
                    await Task.Delay(4000);
                }
            });

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                NotificationPopup?.CloseNotification();
            });

            //string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            int exitCode = process.ExitCode;

            return exitCode;
        }

        /// <summary>
        /// 保存程序运行配置
        /// </summary>
        private void BtnSaveProcessSetting_Click(object sender, RoutedEventArgs e)
        {
            var model = (SettingViewModel)DataContext;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var SetConfig = (string key, string value) =>
            {
                if (!config.AppSettings.Settings.AllKeys.Contains(key))
                    config.AppSettings.Settings.Add(new KeyValueConfigurationElement(key, value));
                else
                    config.AppSettings.Settings[key].Value = value;
            };

            SetConfig("ProcessPath", model.ProcessPath.ToString());
            SetConfig("ProcessArgs", model.ProcessArgs.ToString());
            SetConfig("IePath", model.IePath.ToString());
            config.Save();
            // 重启 WebSocket 服务
            RestartWebsocket();
            MessageBox.Show("配置保存成功，服务已重启", "尚哲托盘程序", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion



        /// <summary>
        /// 获取对应打印机对应pageSize
        /// </summary>
        /// <param name="printName"></param>
        /// <returns></returns>
        public ObservableCollection<string> GetPageSizes(string printName)
        {
            var paperNameList = new ObservableCollection<string>();
            if (string.IsNullOrEmpty(printName))
            {
                PrintDocument printDoc = new PrintDocument();
                foreach (PaperSize ps in printDoc.PrinterSettings.PaperSizes)
                {
                    paperNameList.Add(ps.PaperName);
                }
            }
            else
            {
                // 使用PrinterSettings获取打印机设置
                PrinterSettings printerSettings = new PrinterSettings();
                printerSettings.PrinterName = printName;

                // 获取指定打印机的PaperSize集合
                PaperSizeCollection paperSizes = printerSettings.PaperSizes;
                foreach (PaperSize paperSize in paperSizes)
                {
                    paperNameList.Add(paperSize.PaperName);
                }
            }
            return paperNameList;
        }

        private void BtnIeSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "exe files (*.exe)|*.exe|bat files (*.bat)|*.bat|All files (*.*)|*.*";
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string[] fileNames = openFileDialog.FileNames;
                var model = (SettingViewModel)DataContext;
                if (fileNames.Length > 0)
                {
                    model.IePath = fileNames[0];
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _localHttpServer.Dispose();
        }
    }
}