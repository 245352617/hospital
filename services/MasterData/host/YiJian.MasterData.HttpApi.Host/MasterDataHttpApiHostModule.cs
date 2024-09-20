using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Threading;
using Volo.Abp.VirtualFileSystem;
using YiJian.ECIS.Core;
using YiJian.ECIS.Core.Middlewares;
using YiJian.MasterData.EntityFrameworkCore;
using YiJian.MasterData.MasterData.Users;
using Microsoft.AspNetCore.Http;
using Volo.Abp.BackgroundJobs.Hangfire;
using Hangfire;
using System;
using YiJian.MasterData.External.LongGang.Medicines;
using YiJian.MasterData.External.LongGang;
using SkyApm.Utilities.DependencyInjection;
using Volo.Abp.Users;
using YiJian.ECIS.Authorization;

namespace YiJian.MasterData
{
    [DependsOn(
        typeof(MasterDataApplicationModule),
        typeof(MasterDataEntityFrameworkCoreModule),
        typeof(MasterDataHttpApiModule),
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
        typeof(AbpBackgroundJobsHangfireModule),
        typeof(EcisAuthorizationModule)
    )]
    public class MasterDataHttpApiHostModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var env = context.Services.GetHostingEnvironment();

            base.PreConfigureServices(context);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();
            //注入httpcontextaccessor
            context.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            context.Services.AddMemoryCache();
            context.Services.AddScoped<ISyncHisMedicineAppService, SyncHisMedicineAppService>();
            context.Services.Configure<RemoteServices>(configuration.GetSection("RemoteServices")); //远程服务地址配置 

            Configure<AbpDbContextOptions>(options => { options.UseSqlServer(); });

            //注入HIS上下文对象
            context.Services.AddDbContext<HISDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("HIS"));
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer<HISDbContext>(); //选择使用HIS数据库
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<MasterDataDomainSharedModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.MasterData.Domain.Shared",
                                Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MasterDataDomainModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.MasterData.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MasterDataApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.MasterData.Application.Contracts",
                                Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MasterDataApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.MasterData.Application",
                                Path.DirectorySeparatorChar)));
                });
            }

            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(MasterDataApplicationModule).Assembly,
                    opts => { opts.RootPath = configuration["Swagger:RootPath"]; });
            });
            // CAP 配置
            AddCap(context.Services);
            // 统一返回配置
            context.AddUnifyResult<RESTfulUnifyResultProvider>();
            //任务调度工具HangFire
            ConfigureHangfire(context, configuration);

            // SkyWalking APM
            context.Services.AddSkyApmExtensions();

            // 覆盖 Abp 的当前用户定义，与急危重症一体化平台保持一致，不会因 Abp 版本更新导致问题
            context.Services.AddSingleton<ICurrentUser, EcisCurrentUser>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            var configuration = context.GetConfiguration();
            var name = configuration["Swagger:Name"];
            var apiName = configuration["Swagger:apiName"];
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
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
                options.DocExpansion(DocExpansion.List);
                options.DefaultModelsExpandDepth(0);
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
            // 使用统一返回中间件
            app.UseUnifyResultStatusCodes();
            SeedData(context);

            app.UseHangfireDashboard();
        }

        /// <summary>
        /// 定时任务暂时不用 （明确之后再启用）
        /// </summary>
        /// <param name="context"></param>
        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPostApplicationInitialization(context);

            var configuration = context.GetConfiguration();
            var sw = Boolean.Parse(configuration["HangfireSwitch:ProjectAsync:Switch"]);
            if (sw)
            {
                //医院状态查询触发作业
                //每两分钟触发一次   
                //var cron = "0 */2 * * * ?";
                //var cron = "*/10 * * * * ?"; 
                var cron = configuration["HangfireSwitch:ProjectAsync:Cron"];
                RecurringJob.AddOrUpdate<ISyncHisMedicineAppService>("ExamProjectAsync", x => x.SyncExamProjectAsync(), cron);
            }

        }

        private void AddCap(IServiceCollection service)
        {
            var configuration = service.GetConfiguration();
            service.AddCap(x =>
            {
                x.DefaultGroupName = configuration[$"RabbitMQ:CapBus:DefaultGroupName"];
                x.UseSqlServer(opt => { opt.ConnectionString = configuration[$"ConnectionStrings:Default"]; });
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
        }

        private void ConfigureHangfire(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(configuration.GetConnectionString("Default"));
            });
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
                //     await scope.ServiceProvider.GetRequiredService<MasterDataDbContext>()
                //         .Database
                //         .MigrateAsync();
                // }

                //v2.3.2.0 临时注释
                //启动发送种子数据
                //await scope.ServiceProvider.GetRequiredService<IDataSeeder>()
                //    .SeedAsync();
            });
        }
    }
}