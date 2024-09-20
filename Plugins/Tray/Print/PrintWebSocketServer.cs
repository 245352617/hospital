//using DevExpress.XtraReports.UI;
//using System;
//using System.Net;
//using WebSocketSharp.Server;

//namespace YiJian.CardReader.CardReaders
//{
//    /// <summary>
//    /// 打印服务
//    /// </summary>
//    public class PrintWebSocketServer : IDisposable
//    {
//        private readonly WebSocketServer _server;

//        public PrintWebSocketServer(string ip, string port)
//        {
//            _server = new WebSocketServer(IPAddress.Parse(ip), int.Parse(port));
//            _server.AddWebSocketService<PrintBehavior>("/", behavior =>
//            {
//                behavior.SetupPrint();
//            });
//            _server.Start();
//        }

//        public void Dispose()
//        {
//            _server.Stop();
//            GC.SuppressFinalize(this);
//        }
//    }
//}
