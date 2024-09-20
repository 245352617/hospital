using Hangfire;
using Hangfire.Redis;
using MasterDataService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using Szyjian.Ecis.Patient.BackgroundJob.Hangfire;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;
using Szyjian.RabbitMq;
using Szyjian.Redis;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace Szyjian.Ecis.Patient.Application
{
    [DependsOn(typeof(SzyjianEcisPatientDomainModule),
        typeof(AbpEventBusModule),
        typeof(AbpEventBusRabbitMqModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpBackgroundJobsHangfireModule))]
    public class SzyjianEcisPatientApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            var config = context.Services.GetConfiguration();

            services.AddHttpClient();
            ConfigureRedis(services, config);
            ConfigureCapInEventBus(services, config);
            ConfigureRabbitMq(services, config);
            ConfigureHangfire(services, config);

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            context.Services.AddGrpcClient<GrpcMasterData.GrpcMasterDataClient>(o =>
            {
                var configuration = context.Services.GetConfiguration();
                o.Address = new Uri(configuration["GrpcMasterData"]);
            });

            //注入字典服务
            context.Services.AddHttpClient(BaseAddress.MASTERDATA, c =>
            {
                c.BaseAddress = new Uri(config["RemoteServices:Default:BaseUrl"]);
            });
            //注入预检分诊服务
            context.Services.AddHttpClient(BaseAddress.TRIAGE, c =>
            {
                c.BaseAddress = new Uri(config["RemoteServices:Triage:BaseUrl"]);
            });

            //注入患者服务
            context.Services.AddHttpClient(BaseAddress.PATIENT, c =>
            {
                c.BaseAddress = new Uri(config["RemoteServices:Patient:BaseUrl"]);
            });
            //注入医嘱请求
            context.Services.AddHttpClient(BaseAddress.RECIPE, c =>
            {
                c.BaseAddress = new Uri(config["RemoteServices:Recipe:BaseUrl"]);
            });
            //注入Emr请求
            context.Services.AddHttpClient(BaseAddress.EMR, c =>
            {
                c.BaseAddress = new Uri(config["RemoteServices:Emr:BaseUrl"]);
            });
            //注入Nursing请求
            context.Services.AddHttpClient(BaseAddress.NURSING, c =>
            {
                c.BaseAddress = new Uri(config["RemoteServices:Nursing:BaseUrl"]);
            });
            //注入报表请求
            context.Services.AddHttpClient(BaseAddress.REPORT, c =>
            {
                c.BaseAddress = new Uri(config["RemoteServices:Report:BaseUrl"]);
            });
            //注入HIS请求
            context.Services.AddHttpClient(BaseAddress.HIS, c =>
            {
                c.BaseAddress = new Uri(config["RemoteServices:HIS:BaseUrl"]);
            });
            // 调用医院相关的依赖注入，可在此区分不同医院的不同对接方式
            context.Services.ConfigureHisApi(config);
        }

        /// <summary>
        /// 配置Redis依赖
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        private void ConfigureRedis(IServiceCollection services, IConfiguration config)
        {
            services.AddRedis(option =>
            {
                option.Connection = config["Redis:Connection"];
                option.InstanceName = config["Redis:InstanceName"];
                option.DefaultDb =
                    Convert.ToInt32(config["Redis:DefaultDb"]) > 15 || Convert.ToInt32(config["Redis:DefaultDb"]) < 0
                        ? 0
                        : Convert.ToInt32(config["Redis:DefaultDb"]);
            });
        }

        /// <summary>
        /// 配置Rabbitmq 依赖
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        private void ConfigureRabbitMq(IServiceCollection services, IConfiguration config)
        {
            services.AddRabbitMq(options =>
            {
                options.Connections.Default.UserName = config["RabbitMq:Connection:UserName"];
                options.Connections.Default.Password = config["RabbitMq:Connection:Password"];
                options.Connections.Default.HostName = config["RabbitMq:Connection:HostName"];
                options.Connections.Default.Port = Convert.ToInt32(config["RabbitMq:Connection:Port"]);
            });
            //EventBus
            services.AddRabbitMqEventBus(options =>
            {
                options.ClientName = config["RabbitMq:EventBus:ClientName"];
                options.ExchangeName = config["RabbitMq:EventBus:ExchangeName"];
            });
        }

        /// <summary>
        /// 使用Cap配置EventBus依赖
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        private void ConfigureCapInEventBus(IServiceCollection services, IConfiguration config)
        {

            services.AddCap(options =>
            {
                options.UseSqlServer(config["ConnectionStrings:Default"]);
                options.DefaultGroup = config["RabbitMq:CAP:QueueName"];
                options.FailedRetryCount = 3;
                options.UseRabbitMQ(x =>
                {
                    x.HostName = config["RabbitMq:Connection:HostName"];
                    x.UserName = config["RabbitMq:Connection:UserName"];
                    x.Password = config["RabbitMq:Connection:Password"];
                    x.Port = Convert.ToInt32(config["RabbitMq:Connection:Port"]);
                    x.ExchangeName = config["RabbitMq:CAP:ExchangeName"];
                    x.VirtualHost = config["RabbitMq:CAP:VirtualHost"];
                });

                options.UseDashboard(dashboardOptions =>
                {
                    dashboardOptions.PathMatch = config["RabbitMq:CAP:PathMatch"];
                });
            });
        }

        /// <summary>
        /// 配置Hangfire后台作业
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private void ConfigureHangfire(IServiceCollection services, IConfiguration configuration)
        {
            var connection = ConnectionMultiplexer.Connect(configuration["Redis:Connection"]);
            services.AddHangfire(config =>
            {
                config.UseNLogLogProvider();
                config.UseSimpleAssemblyNameTypeSerializer();
                config.UseRecommendedSerializerSettings();
                // 半小时自动过期成功的作业
                GlobalStateHandlers.Handlers.Add(new SucceedStateExpireHandler(TimeSpan.FromHours(5)));
                config.UseRedisStorage(connection, new RedisStorageOptions
                {
                    Db = Convert.ToInt32(configuration["Redis:DefaultDB"]),
                    UseTransactions = true,
                    Prefix = $"{configuration["ApplicationName"]}:{configuration["ServiceName"]}:Hangfire:"
                });
            });
        }
    }
}