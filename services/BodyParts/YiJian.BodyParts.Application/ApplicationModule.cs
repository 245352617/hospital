using Abp.Quartz;
using YiJian.BodyParts.Domain;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.BackgroundWorkers.Quartz;
using Microsoft.Extensions.Configuration;
using System;

namespace YiJian.BodyParts
{
    /// <summary>
    /// 应用层模块
    /// </summary>
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class ApplicationModule : AbpModule
    {
        /// <summary>
        /// 配置应用服务
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ApplicationModule>(validate: true);

            });
            var services = context.Services;
            var config = context.Services.GetConfiguration();
            ConfigureCapInEventBus(services, config);

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
                options.UseSqlServer(config["ConnectionStrings:BodyParts"]);
                options.UseRabbitMQ(x =>
                {
                    x.HostName = config["RabbitMq:Connection:HostName"];
                    x.UserName = config["RabbitMq:Connection:UserName"];
                    x.Password = config["RabbitMq:Connection:Password"];
                    x.Port = Convert.ToInt32(config["RabbitMq:Connection:Port"]);
                    x.ExchangeName = config["RabbitMq:CAP:ExchangeName"];
                });

                options.UseDashboard();
            });
        }
    }


}
