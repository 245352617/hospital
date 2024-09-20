using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using StackExchange.Redis;
using System;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.RabbitMQ;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using YiJian.ECIS.Core.Extensions;
using YiJian.ECIS.Core.Hosting.Microservices;
using YiJian.ECIS.Core.Options;
using YiJian.ECIS.Core.Redis;
using YiJian.ECIS.ShareModel;

namespace YiJian.ECIS.Core;

/// <summary>
/// 核心基础依赖模块
/// 提供服务发现、事件总线等组建的初始化
/// </summary>
[DependsOn(
    typeof(ShareModelModule),
    typeof(AbpAutofacModule),
    typeof(AbpEventBusModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpBackgroundJobsModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public class ECISCoreModule : AbpModule
{
    //private ConsulOptions _consulOptions;

    /// <summary>
    /// 重写Configuration Services
    /// </summary>
    /// <param name="context"></param>
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var environment = context.Services.GetHostingEnvironment();

        SwaggerConfigurationHelper.Configure(context);

        JwtBearerConfigurationHelper.Configure(context);

        #region Redis
        var services = context.Services;
        var config = context.Services.GetConfiguration();
        ConfigureRedis(services, config);

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = configuration["Redis:KeyPrefix"];
        });

        //if (!environment.IsDevelopment())
        //{
        var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
        context.Services
            .AddDataProtection()
            .PersistKeysToStackExchangeRedis(redis, "Medicine-Protection-Keys");
        //}

        #endregion

        #region Consul

        // 添加健康监测
        //context.Services.AddHealthChecks();

        #endregion

        #region Localization

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ECISCoreModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            //options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            //options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            //options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            //options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            //options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            //options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            //options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            //options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));

            options.Resources
                .Add<ECISResource>("zh-Hans")
                .AddBaseTypes(
                    typeof(AbpValidationResource)
                ).AddVirtualJson("/Localization/ECIS");

            options.DefaultResourceType = typeof(ECISResource);
        });

        #endregion

        #region Cors

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        #endregion

        #region RabbitMQ

        Configure<AbpRabbitMqOptions>(options =>
        {
            options.Connections.Default.UserName = configuration["RabbitMQ:Connections:Default:UserName"];
            options.Connections.Default.Password = configuration["RabbitMQ:Connections:Default:Password"];
            options.Connections.Default.HostName = configuration["RabbitMQ:Connections:Default:HostName"];
            options.Connections.Default.Port = int.Parse(configuration["RabbitMQ:Connections:Default:Port"]);

            Console.WriteLine($"[-] 配置RabbitMQ\n " +
                      $"UserName={options.Connections.Default.UserName}\n " +
                      $"Password={options.Connections.Default.Password}\n " +
                      $"HostName={options.Connections.Default.HostName}\n " +
                      $"Port={options.Connections.Default.Port}");
        });

        Configure<AbpRabbitMqEventBusOptions>(options =>
        {
            options.ClientName = configuration["RabbitMQ:EventBus:ClientName"];
            options.ExchangeName = configuration["RabbitMQ:EventBus:ExchangeName"];

            Console.WriteLine($"[-] 配置RabbitMQ\n " +
                      $"ClientName={options.ClientName}\n " +
                      $"ExchangeName={options.ExchangeName}");
        });

        #endregion


        #region CAP

        #region 移除默认的 CAP 配置 by: ywlin 
        /* -----------------------------------------------------------------------------------------------------------------
        ||    默认的 CAP 配置跟个毒瘤一样，用不上还拿不掉，比小米全家桶还糟糕
        ||    个人感觉这些默认配置使得 YiJian.ECIS.Core 变得一点也不 Core，引入 Core 依赖的项目被迫引入很多不必要的配置及依赖
        ||    所以我将以下代码封装到了扩展方法里，如果有需要使用下面的默认 CAP 配置，请使用:
        ||    context.Services.AddEcisDefaultCap();
        ||    context.AddEcisDefaultCapEventBus();
        ||----by: ywlin----
        --------------------------------------------------------------------------------------------------------------------*/

        //var connectionStringName = configuration["RabbitMQ:EventBus:ConnectionStringName"];

        //context.Services.AddCap(capOptions =>
        //{
        //    capOptions.UseSqlServer(opt =>
        //    {
        //        opt.ConnectionString = configuration[$"ConnectionStrings:{connectionStringName}"];
        //    });
        //});

        //context.AddCapEventBus(capOptions =>
        //{
        //    capOptions.ProducerThreadCount = Environment.ProcessorCount;
        //    capOptions.ConsumerThreadCount = Environment.ProcessorCount;
        //    capOptions.DefaultGroupName = configuration["RabbitMQ:EventBus:DefaultGroupName"];
        //    capOptions.FailedThresholdCallback = (failed) =>
        //    {
        //        var logger = failed.ServiceProvider.GetService<ILogger<ECISCoreModule>>();
        //        logger.LogError($@"消息类型 {failed.MessageType} 失败，重试次数 {capOptions.FailedRetryCount} , 
        //            请手动处理. 消息名: {failed.Message.GetName()}");
        //    };

        //    capOptions.UseRabbitMQ(options =>
        //    {
        //        options.ExchangeName = configuration["RabbitMQ:EventBus:ExchangeName"];
        //        options.UserName = configuration["RabbitMQ:Connections:Default:UserName"];
        //        options.Password = configuration["RabbitMQ:Connections:Default:Password"];
        //        options.HostName = configuration["RabbitMQ:Connections:Default:HostName"];
        //        options.Port = int.Parse(configuration["RabbitMQ:Connections:Default:Port"] ?? "5672");

        //    });
        //    // Configure the host of RabbitMQ
        //    capOptions.UseDashboard();
        //});
        #endregion

        #endregion
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        //var lifetime = context.ServiceProvider.GetService<IHostApplicationLifetime>();
        var configuration = context.GetConfiguration();
        var consulEnabled = Convert.ToBoolean(configuration["App:ConsulEnabled"] ?? "false");
        //if (!consulEnabled) return;

        //是否开启服务发现
        if (consulEnabled)
        {
            try
            {
                Log.Information("启动服务发现注册");

                /* 苏总的consul配置
                
                var consulOptions = configuration.GetSection(ConsulOptions.Name).Get<ConsulOptions>()
                                    ?? throw new ArgumentNullException(nameof(ConsulOptions), "服务发现配置异常");
                app.RegistryConsul(lifetime, consulOptions.ConsulUrl, consulOptions.ServiceUrl,
                    consulOptions.ServiceName);
                */

                // 获取consul的配置信息
                var serviceOptions = app.ApplicationServices.GetRequiredService<IOptions<ConsulServiceOptions>>();
                // 配置健康检测地址，.NET Core 内置的健康检测地址中间件
                app.UseHealthChecks(serviceOptions.Value.HealthCheck);
                app.UseConsul(configuration);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "服务发现注册失败：{Message}", ex.Message);
            }
        }
    }


    /// <summary>
    /// 注入Redis
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    private void ConfigureRedis(IServiceCollection services, IConfiguration config)
    {
        if (string.IsNullOrEmpty(config["Redis:InstanceName"]) || string.IsNullOrEmpty(config["Redis:Configuration"]))
        {
            Log.Error("Redis配置失败,请检查Redis:Configuration,InstanceName配置节点");
        }
        services.AddRedis(option =>
        {
            option.Connection = config["Redis:Configuration"];
            option.InstanceName = config["Redis:InstanceName"];
        });
    }
}