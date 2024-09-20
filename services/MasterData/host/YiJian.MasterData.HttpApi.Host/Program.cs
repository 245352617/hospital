using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace YiJian.MasterData
{
    /// <summary>
    /// 程序入口
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 启动方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> Main(string[] args)
        {
            var applicationName = typeof(Program).Assembly.GetName().Name;

            var logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
            logger.Information($"Starting {applicationName}.");

            try
            {
                await CreateHostBuilder(args).Build().RunAsync();
                logger.Information("Stopped cleanly");

                return 0;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, $"{applicationName} terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args)
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    builder.AddJsonFile("appsettings.secrets.json", optional: true);
                    // 通过环境变量或配置文件配置 Apollo配置中心。Apollo配置中心为可选组件，不配置则不添加
                    var apolloConfig = builder.Build().GetSection("apollo");
                    if (apolloConfig.Exists())
                    {
                        builder.AddApollo(apolloConfig);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseAutofac()
                .UseSerilog((context, services, configuration) =>
                {// Serilog 配置，从配置文件读取日志配置
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext();
                });

            return isWindows ? builder.UseWindowsService() : builder;
        }
    }
}
