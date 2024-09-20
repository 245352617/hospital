using System;
using System.Net;
using WebSocketSharp.Server;
using YiJian.Tray;
using YiJian.Tray.WebServer;

namespace YiJian.CardReader.CardReaders
{
    /// <summary>
    /// 设备服务
    /// </summary>
    public class DeviceWebSocketServer : IDisposable
    {
        private readonly WebSocketServer _server;
        private readonly ICardReader _reader;

        public DeviceWebSocketServer(string ip, string port, string defaultPrinter, ICardReader reader, string iePath)
        {
            _server = new WebSocketServer(IPAddress.Parse(ip), int.Parse(port));
            _server.AddWebSocketService<ReadCardBehavior>("/", behavior =>
            {
                behavior.SetupCardReader(reader);
            });
            _server.AddWebSocketService<PrintBehavior>("/print", behavior =>
            {
                behavior.SetupPrint(defaultPrinter);
            });

            //报卡websocket
            _server.AddWebSocketService<ReportCardBehavior>("/reportCard", behavior =>
            {
                behavior.SetupConnect();
            });

            // 调用IE浏览器websocket
            _server.AddWebSocketService<InvokeLocalExeBehavior>("/invokeLocalExe", behavior =>
            {
                behavior.SetupConnect(iePath);
            });

            _server.Start();

            _reader = reader;
        }

        /// <summary>
        /// 单独开报卡websocket
        /// </summary>
        public DeviceWebSocketServer(string ip, string port)
        {
            _server = new WebSocketServer(IPAddress.Parse(ip), int.Parse(port));
            _server.AddWebSocketService<ReportCardBehavior>("/reportCard", behavior =>
            {
                behavior.SetupConnect();
            });
            _server.Start();
        }

        public void Dispose()
        {
            _server.Stop();
            GC.SuppressFinalize(this);
        }
    }
}
