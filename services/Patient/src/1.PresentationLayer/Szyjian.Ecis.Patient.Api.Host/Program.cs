using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System.Runtime.InteropServices;

namespace Szyjian.Ecis.Patient
{
    /// <summary>
    /// 程序入口类
    /// </summary>
    public class Program
    {
        /// <summary>
        /// main入口方法
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            IHostBuilder builder = Host.CreateDefaultBuilder(args)
                    .ConfigureLogging(builder => { builder.ClearProviders(); })
                    .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                    .UseNLog()
                    .UseAutofac();
            return isWindows ? builder.UseWindowsService() : builder;
        }
    }
}
