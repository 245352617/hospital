using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace YiJian.BodyParts.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var logPath = AppContext.BaseDirectory;
                string logformat = "[{ThreadId}|{Timestamp:yyyy-MM-dd HH:mm:ss}|{Level:u5}|{SourceContext}]|{Message:lj}{NewLine}{Exception}";
                Log.Logger = new LoggerConfiguration()
#if DEBUG
                //最小的日志输出级别是debug
                .MinimumLevel.Debug()
#else
                //其他环境 最小输出级别information
                .MinimumLevel.Debug()
#endif
                //如果类别是Microsoft开头的话，最小输出级别information
                .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                .MinimumLevel.Override("Quartz.Core", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.With<ThreadldEnricher>()
                .WriteTo.Console(outputTemplate: logformat,
                    theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)//输出到控制台
                .WriteTo.Async(c => c.File($"{logPath}/Logs/log.txt", 
                    outputTemplate: logformat, 
                    fileSizeLimitBytes:1024*1024*1024,// 单个文件为200M
                    retainedFileCountLimit:20, // 最大保存文件数
                    rollOnFileSizeLimit: true, // 限制单个文件的最大长度
                    encoding: Encoding.UTF8,   // 文件字符编码
                    rollingInterval:RollingInterval.Day,
                    restrictedToMinimumLevel:LogEventLevel.Debug)) //输出到文件；第一参数输出文件的路径.
                .CreateLogger();//创建文件：
                
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                Console.ReadKey();
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); }).UseAutofac()
                .UseSerilog();
            
            return isWindows ? builder.UseWindowsService() : builder;
        }
    }
}