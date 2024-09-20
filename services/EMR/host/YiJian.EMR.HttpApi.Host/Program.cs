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

namespace YiJian.EMR
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
                .ConfigureAppConfiguration(build => { build.AddJsonFile("appsettings.secrets.json", optional: true); })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseAutofac()
                .UseSerilog((context, services, configuration) =>
                {
                    // Serilog 配置，从配置文件读取日志配置
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext();
                });
            
            return isWindows ? builder.UseWindowsService() : builder;
        }
    }
}
