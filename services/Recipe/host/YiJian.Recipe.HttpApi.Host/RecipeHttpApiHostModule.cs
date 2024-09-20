using BeetleX.Http.Clients;
using Consul;
using Hangfire;
using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SkyApm.Utilities.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Threading;
using Volo.Abp.Users;
using Volo.Abp.VirtualFileSystem;
using YiJian.ECIS.Authorization;
using YiJian.ECIS.Core;
using YiJian.ECIS.Core.Middlewares;
using YiJian.Hospitals.Dto;
using YiJian.Platform;
using YiJian.Recipe.Application.Backgrounds;
using YiJian.Recipe.Application.Backgrounds.Contracts;
using YiJian.Recipe.EntityFrameworkCore;

namespace YiJian.Recipe
{
    [DependsOn(
        typeof(RecipeApplicationModule),
        typeof(RecipeEntityFrameworkCoreModule),
        typeof(RecipeHttpApiModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule),
        typeof(ECISCoreModule),
        typeof(AbpBackgroundJobsHangfireModule)
    )]
    public class RecipeHttpApiHostModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpJsonOptions>(options =>
            {
                options.DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                // continue to use the Newtonsoft.Json
                options.UseHybridSerializer = false;
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();
            context.Services.Configure<RemoteServices>(configuration.GetSection("RemoteServices")); //远程服务地址配置

            // 注意认证中心Api
            AddIdentityHttpCluster(context);

            Configure<AbpDbContextOptions>(options => { options.UseSqlServer(); });
            context.Services.AddTransient<IHospitalBackground, HospitalBackground>();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<RecipeDomainSharedModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.Recipe.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RecipeDomainModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.Recipe.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RecipeApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.Recipe.Application.Contracts",
                                Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RecipeApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.Recipe.Application", Path.DirectorySeparatorChar)));
                });
            }

            //// 默认 CAP 配置
            ////（Core里面的CAP与abp eventbus 使用同一个交换机，然而CAP使用的类型是topic而abp eventbus使用的类型是direct，所以Core里面的配置必然会导致abp eventbus异常）
            //// by: ywlin-20211027
            //context.Services.AddEcisDefaultCap();
            //context.AddEcisDefaultCapEventBus();
            //// CAP 配置
            context.Services.AddCap(x =>
            {
                x.UseSqlServer(opt => { opt.ConnectionString = configuration[$"ConnectionStrings:Recipe"]; });
                x.UseRabbitMQ(options =>
                {
                    options.ExchangeName = configuration["RabbitMQ:CapBus:ExchangeName"];
                    options.UserName = configuration["RabbitMQ:Connections:Default:UserName"];
                    options.Password = configuration["RabbitMQ:Connections:Default:Password"];
                    options.HostName = configuration["RabbitMQ:Connections:Default:HostName"];
                    options.Port = int.Parse(configuration["RabbitMQ:Connections:Default:Port"] ?? "5672");
                });
                x.UseDashboard(); // CAP provides dashboard pages after the version 2.x
            });
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(RecipeApplicationModule).Assembly, opts =>
                {
                    // 修改成首字母小写
                    var text = configuration["Swagger:RootPath"];
                    var first = text[..1];
                    opts.RootPath = "ecis/" + first.ToLowerInvariant() + text[1..];
                });
            });

            //任务调度工具HangFire
            ConfigureHangfire(context, configuration);

            // 统一返回配置
            context.AddUnifyResult<RESTfulUnifyResultProvider>();

            // SkyWalking APM支持
            context.Services.AddSkyApmExtensions();

            // 覆盖 Abp 的当前用户定义，与急危重症一体化平台保持一致，不会因 Abp 版本更新导致问题
            context.Services.AddSingleton<ICurrentUser, EcisCurrentUser>();
        }

        private static void AddIdentityHttpCluster(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton((provider) =>
            {
                var remoteServices = provider.GetService<IOptionsMonitor<RemoteServices>>().CurrentValue;
                HttpCluster httpCluster = new();
                httpCluster.TimeOut = 5 * 1000;
                foreach (var identity in remoteServices.Identity)
                {
                    httpCluster.DefaultNode.Add(identity.BaseUrl);
                }
                var service = httpCluster.Create<IIdentityProxyService>();
                return service;
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            var configuration = context.GetConfiguration();
            var name = configuration["Swagger:Name"];
            var apiName = configuration["Swagger:apiName"];
            var rootPath = configuration["Swagger:RootPath"];
            var apiTitle = configuration["Swagger:apiTitle"];

            //app = UseConsulRegistry(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAbpRequestLocalization();
            app.UseAuthorization();
            app.UseSwagger(options => { options.RouteTemplate = "api/{documentName}/swagger.json"; });

            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/api/{name}/swagger.json", apiName);
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                options.DefaultModelsExpandDepth(0);
            });

            app.UseKnife4UI(c =>
            {
                c.DocumentTitle = "KnifeUI 电子病例微服务 API";
                c.RoutePrefix = "";
                c.SwaggerEndpoint($"/api/{name}/swagger.json", apiName);
            });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();

            // 使用统一返回中间件
            app.UseUnifyResultStatusCodes();
            //数据播种初始化
            SeedData(context);

            app.UseHangfireDashboard();

        }

        //初始化完成
        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPostApplicationInitialization(context);

            var configuration = context.GetConfiguration();
            var sw = Boolean.Parse(configuration["QueryMedicalInfo:Switch"]);
            if (sw)
            {
                //医院状态查询触发作业
                //每两分钟触发一次   
                //var cron = "0 */2 * * * ?";
                //var cron = "*/10 * * * * ?"; 
                var cron = configuration["QueryMedicalInfo:Cron"];
                RecurringJob.AddOrUpdate<IHospitalBackground>("QueryMedicalInfo", x => x.QueryMedicalInfo(), cron);
            }
            else
            {
                RecurringJob.RemoveIfExists("QueryMedicalInfo");
            }
        }


        public IApplicationBuilder UseConsulRegistry(IApplicationBuilder app)
        {
            var config = app.ApplicationServices.GetRequiredService<IConfiguration>();
            if (Convert.ToBoolean(config["Consul:IsEnabled"]))
            {
                var consulClient = new ConsulClient(x => { x.Address = new Uri(config["Consul:RegistryUrl"]); });

                var address = app.ServerFeatures.Get<IServerAddressesFeature>().Addresses.FirstOrDefault();
                if (address.IsNullOrWhiteSpace())
                {
                    address = config["Urls"].Split(',')[0];
                }

                var uri = new Uri(address);
                var registration = new AgentServiceRegistration
                {
                    ID = config["Consul:ServiceName"] + "-" + Guid.NewGuid(),
                    Name = config["Consul:ServiceName"],
                    Address = uri.Host,
                    Port = uri.Port,
                    Tags = new[] { config["Consul:Tags"] },
                    Check = new AgentServiceCheck
                    {
                        HTTP = uri.Scheme + "://" + uri.Authority + config["Consul:HealthCheckUrl"],
                        Timeout = TimeSpan.FromSeconds(5),
                        Interval = TimeSpan.FromSeconds(10),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(10)
                    }
                };

                //consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                //consulClient.Agent.ServiceRegister(registration).Wait();
                //var lifeTime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
                //lifeTime.ApplicationStopping.Register(() =>
                //{
                //    Log.Logger.Information("开始注销Consul服务");
                //    consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                //    consulClient.Dispose();
                //});
            }

            return app;
        }

        /// <summary>
        ///     自动迁移、生成种子数据
        /// </summary>
        /// <param name="context"></param>
        private void SeedData(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                using var scope = context.ServiceProvider.CreateScope();

                //启动生成数据库 
                // if (context.GetEnvironment().IsDevelopment())
                // {
                //     // 自动迁移文件
                //     await scope.ServiceProvider.GetRequiredService<RecipeDbContext>()
                //         .Database
                //         .MigrateAsync();
                // }

                //启动发送种子数据
                await scope.ServiceProvider.GetRequiredService<IDataSeeder>()
                    .SeedAsync();
            });
        }

        private void ConfigureHangfire(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(configuration.GetConnectionString("Recipe"));
            });
        }

    }
}