using Newtonsoft.Json;
using NLog;
using System;
using System.Configuration;
using System.Linq;
using Tray.ReportCard;
using WebSocketSharp;
using WebSocketSharp.Server;
using YiJian.CardReader;
using YiJian.CardReader.WebServer;
using YiJian.Tray.WebServer;

namespace YiJian.Tray
{
    /// <summary>
    /// 用于报卡服务
    /// </summary>
    public class ReportCardBehavior : WebSocketBehavior
    {
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();
        //private static readonly ILogger _log = new NullLogger(new LogFactory());

        public string GetSessionInfoStr()
        {
            if (Sessions != null)
                return $"目前操作的ID:{ID}，目前在连接的ID列表:{string.Join('|', Sessions.IDs.ToArray())},ReportCardBehavior,";
            return "";
        }
        public void SetupConnect()
        {
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
                var info = JsonConvert.DeserializeObject<ReportCardProcessInfo>(e.Data);
                try
                {
                    // Todo: 对info做检查，不符合的就驳回请求，需要需求明确
                    //checkedJson
                    string retStr = string.Empty;
                    switch (info?.UseType)
                    {
                        case 1:
                            string processArgs = $"{info.UserName}|{info.DeptCode}|{info.VisitNo}|{info.ReportCardCode}|";
                            //if (info?.Action?.ToUpper() != "VIEWSTATUS")
                            //{
                            //    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                            //    {
                            //        MainWindow.NotificationPopup?.SetMsg($"调用报卡桌面程序中");
                            //        MainWindow.NotificationPopup?.ShowNotification();
                            //    });
                            //}
                            retStr = await (new ProcessExe(info?.ProcessPath, processArgs, info?.Action)).process();
                            break;
                        default:
                            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            string configProcessPath = config.AppSettings.Settings["ProcessPath"].Value;
                            string configProcessArgs = $"{info?.UserName}|{info?.DeptCode}|{info?.VisitNo}|{info?.ReportCardCode}|"; // 用传入的参数覆盖配置文件中的参数
                            //if (info?.Action?.ToUpper() != "VIEWSTATUS")
                            //{
                            //    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                            //    {
                            //        MainWindow.NotificationPopup?.SetMsg($"调用报卡桌面程序中");
                            //        MainWindow.NotificationPopup?.ShowNotification();
                            //    });
                            //}
                            retStr = await (new ProcessExe(configProcessPath, configProcessArgs, info?.Action)).process();
                            break;
                    }
                    if (info?.Action?.ToUpper() == "Add".ToUpper() || info?.Action?.ToUpper() == "ViewStatus".ToUpper())
                    {
                        var obj = new { reportCardCode = info.ReportCardCode, action = info.Action, isEscalated = (retStr == "1" ? true : false) };
                        WebSocketSend(JsonConvert.SerializeObject(JsonResult.Ok($"调用报卡桌面程序成功，动作为：{info.Action}", obj)));
                    }
                    else
                    {
                        var obj = new { reportCardCode = info?.ReportCardCode, action = info?.Action };
                        WebSocketSend(JsonConvert.SerializeObject(JsonResult.Ok($"调用报卡桌面程序成功，动作为：{info?.Action}", obj)));
                    }
                }
                catch (Exception ex)
                {
                    message = GetSessionInfoStr() + $"OnMessage Methon 失败：{ex.Message}";
                    _log.Error(message);
                    //ControlUpdateHelper.TextBlockTextUpdate("StatusBarTxt", message);

                    WebSocketSend(JsonConvert.SerializeObject(JsonResult.Fail($"失败：{ex.Message}")));
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
    }
}
