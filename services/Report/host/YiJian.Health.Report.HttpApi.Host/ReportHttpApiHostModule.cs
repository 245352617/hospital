using EasyAbp.Abp.EventBus.Cap;
using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkyApm.Utilities.DependencyInjection;
using System.IO;
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
using Volo.Abp.Users;
using Volo.Abp.VirtualFileSystem;
using YiJian.ECIS.Authorization;
using YiJian.ECIS.Core;
using YiJian.ECIS.Core.Middlewares;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.Hospitals.Dto;

namespace YiJian.Health.Report
{
    [DependsOn(
        typeof(ReportApplicationModule),
        typeof(ReportEntityFrameworkCoreModule),
        typeof(ReportHttpApiModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpEventBusCapModule),
        typeof(ECISCoreModule),
        typeof(EcisAuthorizationModule)
    )]
    public class ReportHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpDbContextOptions>(options => { options.UseSqlServer(); });
            context.Services.Configure<RemoteServices>(configuration.GetSection("RemoteServices")); //远程服务地址配置

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<ReportDomainSharedModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.Health.Report.Domain.Shared",
                                Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<ReportDomainModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.Health.Report.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<ReportApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.Health.Report.Application.Contracts",
                                Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<ReportApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            string.Format("..{0}..{0}src{0}YiJian.Health.Report.Application",
                                Path.DirectorySeparatorChar)));
                });
            }

            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(ReportApplicationModule).Assembly,
                    opts => { opts.RootPath = configuration["Swagger:RootPath"]; });
            });

            //// 默认 CAP 配置
            ////（Core里面的CAP与abp eventbus 使用同一个交换机，然而CAP使用的类型是topic而abp eventbus使用的类型是direct，所以Core里面的配置必然会导致abp eventbus异常）
            //// by: ywlin-20211027
            //context.Services.AddEcisDefaultCap();
            //context.AddEcisDefaultCapEventBus();
            //// CAP 配置
            context.Services.AddCap(x =>
            {
                x.UseSqlServer(opt => { opt.ConnectionString = configuration[$"ConnectionStrings:Report"]; });
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
            //配置Mvc
            context.Services.AddMvc(options => { options.EnableEndpointRouting = false; });
            // 统一返回配置
            context.AddErrorUnifyResult<RESTfulUnifyResultProvider>();

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
            var apiTitle = configuration["Swagger:apiTitle"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();

            app.UseAbpRequestLocalization();
            app.UseAuthorization();
            app.UseSwagger(options => { options.RouteTemplate = "api/{documentName}/swagger.json"; });

            app.UseKnife4UI(c =>
            {
                c.DocumentTitle = "KnifeUI 报表微服务 API";
                c.RoutePrefix = "";
                c.SwaggerEndpoint($"/api/{name}/swagger.json", apiName);
            });

            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/api/{name}/swagger.json", apiName);
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                options.DefaultModelsExpandDepth(0);
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
            // 使用统一返回中间件
            app.UseUnifyResultStatusCodes();
            //FastReport
            //app.UseFastReport();
            app.UseDefaultFiles();
        }
    }
}