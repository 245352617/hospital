using NLog;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YiJian.Tray
{
    /// <summary>
    /// 调用其他程序，并在调用完成后，查询His获得报卡填报状态
    /// </summary>
    public class ProcessExe
    {
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();

        private static bool IsOnProcess = false;
        private readonly string? _processPath;
        private string? _processArgs;
        private string? _action;
        private readonly int continueWaitFor = 100;

        public ProcessExe(string? processPath, string? processArgs, string? action, int continueWaitFor = 100)
        {
            this._processPath = processPath;
            this._processArgs = processArgs;
            this._action = action;
            this.continueWaitFor = continueWaitFor;
        }

        /// <summary>
        /// 调用其他程序，并在调用完成后，查询His获得报卡填报状态
        /// </summary>
        public async Task<string> process()
        {
            //ControlUpdateHelper.TextBlockTextUpdate("StatusBarTxt", $"运行程序地址：{_processPath} ，运行参数为：{_processArgs}");
            _log.Info($"即将开始调用其他程序，运行程序地址：{_processPath} ，运行参数为：{_processArgs + GetActionCode("Add")}，Acition：{_action}");

            if (_action?.ToUpper() == "Add".ToUpper())
            {
                // // 开子线程调用其他程序
                // var task = Task.Run(() =>
                // {
                try
                {
                    var retCode = RunProgram(_processPath, _processArgs + GetActionCode("Add"));

                    if (retCode != -1) //TODO：有时间需要再仔细确认不同情况下其他程序的返回值
                    {
                        _log.Info($"顺利执行程序，程序名或程序地址为：{_processPath}，参数为：{_processArgs + GetActionCode("Add")}，Acition：{_action}");
                    }
                    //return retCode;
                    await Task.Delay(continueWaitFor); // 继续前等待一段时间，以便His准备妥当
                    try
                    {
                        //int retC = retCode.Result;
                        if (retCode == -1)
                            return "-1";
                        _log.Info($"即将查询报卡状态，参数为： {_processArgs + GetActionCode("ViewStatus")}");
                        retCode = GetReportCardStatus(_processPath, _processArgs + GetActionCode("ViewStatus"));
                        return retCode.ToString();
                    }
                    catch (Exception ex)
                    {
                        _log.Error($"执行程序出错，程序名或程序地址为：{_processPath} ，参数为： {_processArgs}， exMsg：{ex.Message}");
                        return "-1";
                    }
                }
                catch (Exception ex)
                {
                    _log.Error($"执行程序出错，程序名或程序地址为：{_processPath} ，参数为： {_processArgs + GetActionCode("Add")}，Acition：{_action}， exMsg：{ex.Message}");
                    return "-1";
                }
                // });

                // // 子线程调用其他程序后，假如返回非-1值，则调用程序查询报卡情况
                // var result = await task.ContinueWith(async (retCode) =>
                // {
                //await Task.Delay(continueWaitFor); // 继续前等待一段时间，以便His准备妥当
                //try
                //{
                //    int retC = retCode.Result;
                //    if (retC == -1)
                //        return "-1";
                //    _log.Info($"即将查询报卡状态，参数为： {_processArgs + GetActionCode("ViewStatus")}");
                //    Task<int> task = GetReportCardStatus(_processPath, _processArgs + GetActionCode("ViewStatus"));
                //    return task.Result.ToString();
                //}
                //catch (Exception ex)
                //{
                //    _log.Error($"执行程序出错，程序名或程序地址为：{_processPath} ，参数为： {_processArgs}， exMsg：{ex.Message}");
                //    return "-1";
                //}
                // });

                //return result.Result;
            }
            else if (_action?.ToUpper() == "View".ToUpper())
            {
                string retCode = ViewReportCard(_processPath, _processArgs + GetActionCode("View"));
                return retCode;
            }
            else if (_action?.ToUpper() == "ViewStatus".ToUpper())
            {
                int task = GetReportCardStatus(_processPath, _processArgs + GetActionCode("ViewStatus"));

                return task.ToString();
            }
            else
            {
                return $"未知Action: {_action}，未处理";
            }
        }

        /// <summary>
        /// 查看报卡信息
        /// </summary>
        /// <returns></returns>
        private string ViewReportCard(string? processPath, string? processArgs)
        {
            // var task = Task.Run( () =>
            // {
            try
            {
                var retCode = RunProgram(processPath, processArgs);
                return "查看报卡信息完毕";
            }
            catch (Exception ex)
            {
                _log.Error($"ViewReportCard 执行程序出错，程序名或程序地址为：{processPath} ，参数为： {processArgs}， exMsg：{ex.Message}");
                return "-1";
            }
            // });
            //return task;
        }

        /// <summary>
        /// 查看报卡状态
        /// </summary>
        private int GetReportCardStatus(string? processPath, string? processArgs)
        {
            // // 开子线程调用其他程序
            // var task = Task.Run(() =>
            // {
            try
            {
                int retCode = 0; //由于目前并未提供查询状态接口，因此统一返回0
                                 //var retCode = RunProgram(_processPath, _processArgs);
                                 //int retCode = ((new Random()).Next(10) > 5) ? 1 : 0; //测试使用随机数模拟
                _log.Info($"GetReportCardStatus 顺利执行程序，程序名或程序地址为：{processPath}，参数为：{processArgs}，retCode：{retCode}");
                return retCode;
            }
            catch (Exception ex)
            {
                _log.Error($"GetReportCardStatus 执行程序出错，程序名或程序地址为：{processPath} ，参数为： {processArgs}， exMsg：{ex.Message}");
                return -1;
            }
            // });
            //return task;
        }

        /// <summary>
        /// 根据action获取对接exe文件的调用类型
        /// </summary>
        /// <returns>-1：不匹配任何 1.添加报卡 2.查询报卡 3.查询报卡状态</returns>
        private string GetActionCode(string? action)
        {
            switch (action?.ToUpper())
            {
                case "ADD":
                    return "1|";
                case "VIEW":
                    return "2|";
                case "VIEWSTATUS":
                    return "3|";
                default:
                    return "-1|";
            }
        }

        /// <summary>
        /// 运行特定程序
        /// </summary>
        /// <param name="programPath">运行程序名或地址</param>
        /// <param name="arguments">参数</param>
        /// <returns></returns>
        private int RunProgram(string? programPath, string? arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(programPath);
            startInfo.FileName = programPath;
            startInfo.Arguments = arguments;
            //startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;

            Process process = new Process() { StartInfo = startInfo };
            bool isProcess = process.Start();

            //Task.Run(async () =>
            //{
            //    bool isDoTop = false;
            //    await Task.Delay(4000);
            //    for (int i = 1; i <= 100; i--)
            //    {
            //        if (isProcess)
            //            isDoTop = BringWindowToFront();
            //        if (isDoTop)
            //            return;
            //        await Task.Delay(4000);
            //    }
            //});

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            int exitCode = process.ExitCode;

            return exitCode;
        }

        public static bool BringWindowToFront()
        {
            var searchProcess = Process.GetProcessesByName("reportcard").FirstOrDefault();
            //var searchProcess = Process.GetProcessesByName("ILSpy").FirstOrDefault();
            if (searchProcess == null) return false;

            _log.Info($"BringWindowToFront 顺利找到进程，进程名为：{searchProcess.ProcessName}，进程ID为：{searchProcess.Id}，Title：{searchProcess.MainWindowTitle}");
            if (searchProcess.MainWindowTitle != string.Empty)
            {
                _log.Info($"searchProcess.MainWindowTitle: {searchProcess.MainWindowTitle}");
                SetForegroundWindow(searchProcess.MainWindowHandle);
                //SwitchToThisWindow(searchProcess.MainWindowHandle, true);
                //ShowWindow(searchProcess.MainWindowHandle, 9);
                SetWindowPos(searchProcess.MainWindowHandle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

                if (searchProcess.MainWindowTitle.Contains("选择角色")
                    || searchProcess.MainWindowTitle.Contains("稍等")
                    || searchProcess.MainWindowTitle.Contains("提示信息")
                    || searchProcess.MainWindowTitle.Contains("提示"))
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        //窗体置顶
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        //不调整窗体位置
        private const uint SWP_NOMOVE = 0x0002;

        //不调整窗体大小
        private const uint SWP_NOSIZE = 0x0001;
    }
}
