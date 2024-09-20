using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using YiJian.ECIS.Core.Hosting.AspNetCore;

namespace YiJian.Handover
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var applicationName = typeof(Program).Assembly.GetName().Name;

            var logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger(); 
            try
            {
                logger.Information($"Starting {applicationName}.");
                await CreateHostBuilder(args).Build().RunAsync();
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
