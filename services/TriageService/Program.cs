using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using NLog.Web;
using Winton.Extensions.Configuration.Consul;
using System.Runtime.InteropServices;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //清除提供程序生成器
                    //logging.ClearProviders();
                    //从appsettings.json中获取Logging的配置
                    //logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    //去除Apb框架日志
                    //logging.AddConsole();
                    logging.ClearProviders();
                    //添加调试输出
                    //logging.AddDebug();
                    //日志EventSource 记录提供程序
                    //logging.AddEventSourceLogger();
                    //logging.SetMinimumLevel(LogLevel.Trace);
                }).UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        // 使用环境变量的配置，(替换json文件里面的配置)
                        config.AddEnvironmentVariables();
                        // 1、加载环境变量配置
                        IWebHostEnvironment webHostEnvironment = context.HostingEnvironment;
                        string EnvironmentName = webHostEnvironment.EnvironmentName; // 这个是环境名
                        string ApplicationName = webHostEnvironment.ApplicationName; // 应用名称
                        // 2、根据环境变量加载配置文件
                        IConfiguration configuration = config
                            .AddJsonFile($"appsettings.{EnvironmentName}.json", false, true)
                            .Build(); // 动态监听配置文件改变

                        #region 3、从consul配置中心加载配置文件
                        //string CongfigCenter = configuration["Consul_Url"];
                        //config.AddConsul($"{ApplicationName}/appsettings.{EnvironmentName}.json", options =>
                        //{
                        //    options.ConsulConfigurationOptions = cco => { cco.Address = new Uri(CongfigCenter); };

                        //    // 3.1、热加载配置文件
                        //    options.ReloadOnChange = true;
                        //    // 3.2、忽略加载异常(没有配置文件的时候会出现异常)
                        //    options.OnLoadException = context => context.Ignore = true;
                        //});
                        #endregion

                        #region 一个开关，是否启用授权权限

                        PermissionDefinition.IsEnabledPermission = Convert.ToBoolean(configuration["Settings:IsEnabledPermission"]);

                        #endregion

                        PermissionDefinition.AppName = configuration["ApplicationName"];
                        PermissionDefinition.ServiceName = configuration["ServiceName"];

                        // 4、根据变量加载NLog配置文件
                        NLogBuilder.ConfigureNLog($"NLog.{EnvironmentName.Split('.')[0]}.config");
                    });
                })
                .UseAutofac();
            return isWindows ? builder.UseWindowsService() : builder;
        }

    }
}