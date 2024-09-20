using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using System;
using System.Diagnostics;
using System.Linq;
using Tray;
using WebSocketSharp;
using WebSocketSharp.Server;
using YiJian.CardReader.WebServer;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace YiJian.Tray
{
    /// <summary>
    /// 用于调用本地程序服务
    /// </summary>
    public class InvokeLocalExeBehavior : WebSocketBehavior
    {
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();
        //private static readonly ILogger _log = new NullLogger(new LogFactory());
        private string _iePath = "";

        public string GetSessionInfoStr()
        {
            if (Sessions != null)
                return $"目前操作的ID:{ID}，目前在连接的ID列表:{string.Join('|', Sessions.IDs.ToArray())},InvokeLocalExeBehavior,";
            return "";
        }
        public void SetupConnect(string iePath)
        {
            _iePath = iePath;
            _log.Info(GetSessionInfoStr() + "SetupConnect Methon invoke");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            _log.Info(GetSessionInfoStr() + "OnClose Methon invoke");
            base.OnClose(e);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            _log.Error(GetSessionInfoStr() + "OnError Methon invoke");
            base.OnError(e);
        }

        private void WebSocketSend(string data)
        {
            try
            {
                Send(data);
            }
            catch (InvalidOperationException ex)
            {
                _log.Error($"WebSocket 断开连接： {ex}");
                return;
            }
            catch (Exception ex)
            {
                _log.Error($"WebSocketSend 断开异常： {ex}");
                return;
            }
        }

        /// <summary>
        /// 收到消息时
        /// </summary>
        protected override async void OnMessage(MessageEventArgs e)
        {
            string message = GetSessionInfoStr() + "OnMessage Methon Start";

            _log.Info(message);
            //MessageBox.Show(message);

            message = GetSessionInfoStr() + $"接受到的信息如下 : {e.Data}";
            _log.Info(message);
            //ControlUpdateHelper.TextBlockTextUpdate("StatusBarTxt", message);
            //Send(JsonConvert.SerializeObject(JsonResult.Ok("接受到消息", e.Data)));

            if (string.IsNullOrWhiteSpace(e.Data)) return;

            //客户端定时发送心跳 ping，回应连接 pong
            if (e.Data.Equals("ping"))
            {
                WebSocketSend("pong");
                return;
            }

            try
            {
                var info = JsonConvert.DeserializeObject<InvokeLocalExeProcessInfo>(e.Data);

                switch (info?.ProcessName)
                {
                    case "ie":
                        {
                            try
                            {
                                if (RunProgram(_iePath, info.ProcessArgs))
                                {
                                    WebSocketSend(JsonConvert.SerializeObject(InvokeLocalExeProcessResult.Success(info?.ProcessName), _serializerSettings));
                                }
                                else
                                {
                                    WebSocketSend(JsonConvert.SerializeObject(InvokeLocalExeProcessResult.Error(info?.ProcessName, "打开程序失败"), _serializerSettings));
                                }
                            }
                            catch (Exception ex)
                            {
                                message = GetSessionInfoStr() + $"OnMessage Methon 失败：{ex.Message}";
                                _log.Error(message);

                                WebSocketSend(JsonConvert.SerializeObject(InvokeLocalExeProcessResult.Error(info?.ProcessName, ex.Message), _serializerSettings));
                            }
                        }
                        break;
                }
            }
            catch
            {
                message = GetSessionInfoStr() + $"接受到的信息不符合格式，对消息不做处理！";
                _log.Error(message);
                //ControlUpdateHelper.TextBlockTextUpdate("StatusBarTxt", message);
                return;
            }

            message = GetSessionInfoStr() + "OnMessage Methon End";
            _log.Info(message);
        }

        protected override void OnOpen()
        {
            _log.Info(GetSessionInfoStr() + "OnOpen Methon Start");
            base.OnOpen();

            try
            {
                //ControlUpdateHelper.TextBlockTextUpdate("StatusBarTxt", "客户端通过 WebSocket 连接成功");
                _log.Info(GetSessionInfoStr() + "WPF客户端 WebSocket 连接成功");
                WebSocketSend(JsonConvert.SerializeObject(JsonResult.Ok(GetSessionInfoStr() + "WPF客户端 WebSocket 连接成功")));
            }
            catch (Exception ex)
            {
                WebSocketSend(JsonConvert.SerializeObject(JsonResult.Fail($"WPF客户端 WebSocket 连接失败，失败信息如下：{ex.Message}")));
            }
        }

        /// <summary>
        /// 运行特定程序
        /// 通过批处理文件运行程序，批处理文件在程序根目录下
        /// 使用批处理文件的原因是，直接使用Process.Start()方法运行程序时，会出现程序无法置前问题
        /// </summary>
        /// <param name="programPath">运行程序名或地址</param>
        /// <param name="arguments">参数</param>
        /// <returns></returns>
        private bool RunProgram(string programPath, string arguments)
        {
            string batchFilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}\Launcher.bat";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = batchFilePath;
            startInfo.Arguments = $"\"{programPath}\" \"{arguments}\"" ;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            Process process = new Process() { StartInfo = startInfo };
            bool isProcess = process.Start();

            return isProcess;
        }
    }
}
