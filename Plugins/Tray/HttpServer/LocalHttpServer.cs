using System;
using System.Net;
using System.Threading;

namespace Tray.HttpServer
{
    /// <summary>
    /// HTTP服务端
    /// </summary>
    public class LocalHttpServer : IDisposable
    {
        private readonly HttpListener _httpListener;
        Thread _listenerThread;
        private CancellationTokenSource _cancellationTokenSource;

        public LocalHttpServer()
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add($"http://localhost:{23234}/");
            _httpListener.Start();

            _cancellationTokenSource = new CancellationTokenSource();
            _listenerThread = new Thread(() => ListenForRequests(_cancellationTokenSource.Token));
            _listenerThread.Start();
        }

        private void ListenForRequests(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_httpListener.IsListening)
                {
                    try
                    {
                        var context = _httpListener.GetContext(); // 阻塞等待请求
                        ProcessRequest(context);
                    }
                    catch (Exception ex)
                    {
                        // 处理异常
                    }
                }
            }
        }

        /// <summary>
        /// HTTP请求分发处理
        /// </summary>
        /// <param name="context"></param>
        private void ProcessRequest(HttpListenerContext context)
        {
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.AddHeader("Access-Control-Allow-Methods", "*");
            context.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
            string requestUrl = context.Request.RawUrl;
            requestUrl = requestUrl.Substring(0, 3);
            string responseString;
            switch (requestUrl)
            {
                case "/ip":
                    responseString = GetLocalIPv4Address();
                    break;
                default:
                    responseString = "<HTML><BODY>尚哲医健急诊系统托盘程序</BODY></HTML>";
                    break;
            }

            // 读取请求并发送响应
            var response = context.Response;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        /// <summary>
        /// 获取本机IPv4地址
        /// </summary>
        /// <returns></returns>
        private string GetLocalIPv4Address()
        {
            string hostName = Dns.GetHostName(); // 获取本机的主机名
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

            foreach (IPAddress ip in hostEntry.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    // 这里我们只关心 IPv4 地址
                    return ip.ToString();
                }
            }

            return string.Empty;
        }

        public void Dispose()
        {
            if (_httpListener != null)
            {
                _httpListener.Stop();
                _httpListener.Close();
            }

            _cancellationTokenSource.Cancel();
            _listenerThread.Join(); // 等待线程安全退出
        }
    }
}
