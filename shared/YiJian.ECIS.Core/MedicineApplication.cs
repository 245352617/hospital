using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.Core.Middlewares;

namespace YiJian.ECIS.Core;

/// <summary>
///     应用入库框架
/// </summary>
public class MedicineApplication
{
    private static IConfiguration BuildConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                true)
            .AddEnvironmentVariables()
            .Build();
        return configuration;
    }

    private static IHostBuilder CreateWebHostBuilder(string[] args, Type startup, IConfiguration configuration)
    {
        var skyWalkingEnabled = Convert.ToBoolean(configuration["App:SkyWalkingEnabled"] ?? "false");
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                if (skyWalkingEnabled)
                {
                    webBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "SkyAPM.Agent.AspNetCore");
                }
                webBuilder.UseStartup(startup);
            })
            .UseAutofac()
            .UseSerilog()
            // .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
            .UseAsDaemon();
    }

    /// <summary>
    /// 启动服务
    /// </summary>
    /// <param name="args"></param>
    /// <param name="starup"></param>
    /// <returns></returns>
    public static async Task<int> StartAsync(string[] args, Type starup)
    {
        return await StartAsync(args, starup, null);
    }

    /// <summary>
    /// 启动服务
    /// </summary>
    /// <param name="args"></param>
    /// <param name="startup"></param>
    /// <param name="seedAction"></param>
    /// <returns></returns>
    public static async Task<int> StartAsync(string[] args, Type startup, Action<IServiceProvider> seedAction)
    {
        var configuration = BuildConfiguration();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Code))
            .CreateLogger();

        try
        {
            var install = args.Contains("/install");
            if (install) args = args.Except(new[] { "/install" }).ToArray();

            var host = CreateWebHostBuilder(args, startup, configuration).Build();
            if (install && host != null && seedAction != null)
            {
                Log.Information("安装数据库");
                seedAction(host.Services);
                Log.Information("数据库安装完毕，请重启服务");
                return 0;
            }

            Log.Information("服务启动中...");
            await host.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "服务启动失败:{Message}", ex.Message);
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}