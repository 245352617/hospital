using EasyAbp.Abp.EventBus.Cap;
using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkyApm.Utilities.DependencyInjection;
using System.IO;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Users;
using Volo.Abp.VirtualFileSystem;
using YiJian.ECIS.Authorization;
using YiJian.ECIS.Core;
using YiJian.ECIS.Core.Middlewares;
using YiJian.Nursing.EntityFrameworkCore;
using DocExpansion = Swashbuckle.AspNetCore.SwaggerUI.DocExpansion;

namespace YiJian.Nursing
{
    /// <summary>
    /// 项目模块配置
    /// </summary>
    [DependsOn(
        typeof(NursingApplicationModule),
        typeof(NursingEntityFrameworkCoreModule),
        typeof(NursingHttpApiModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEventBusCapModule),
        typeof(ECISCoreModule),
        typeof(EcisAuthorizationModule)
        )]
    public class NursingHttpApiHostModule : AbpModule
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            IWebHostEnvironment hostingEnvironment = context.Services.GetHostingEnvironment();
            IConfiguration configuration = context.Services.GetConfiguration();

            context.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<NursingDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.Nursing.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<NursingDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.Nursing.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<NursingApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.Nursing.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<NursingApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.Nursing.Application", Path.DirectorySeparatorChar)));
                });
            }

            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(NursingApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = configuration["Swagger:RootPath"];
                });
            });

            // CAP 配置
            context.Services.AddCap(x =>
            {
                x.UseSqlServer(opt =>
                {
                    opt.ConnectionString = configuration[$"ConnectionStrings:Nursing"];
                });
                x.FailedRetryCount = 3;
                x.UseRabbitMQ(options =>
                {
                    options.ExchangeName = configuration["RabbitMQ:CapBus:ExchangeName"];
                    options.UserName = configuration["RabbitMQ:Connections:Default:UserName"];
                    options.Password = configuration["RabbitMQ:Connections:Default:Password"];
                    options.HostName = configuration["RabbitMQ:Connections:Default:HostName"];
                    options.Port = int.Parse(configuration["RabbitMQ:Connections:Default:Port"] ?? "5672");
                });
                x.UseDashboard();  // CAP provides dashboard pages after the version 2.x
            });
            // 统一返回配置
            context.AddUnifyResult<RESTfulUnifyResultProvider>();

            // SkyWalking APM
            context.Services.AddSkyApmExtensions();

            // 覆盖 Abp 的当前用户定义，与急危重症一体化平台保持一致，不会因 Abp 版本更新导致问题
            context.Services.AddSingleton<ICurrentUser, EcisCurrentUser>();
        }

        /// <summary>
        /// asp配置
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            IApplicationBuilder app = context.GetApplicationBuilder();
            IWebHostEnvironment env = context.GetEnvironment();
            IConfiguration configuration = context.GetConfiguration();

            var name = configuration["Swagger:Name"];
            var apiName = configuration["Swagger:apiName"];
            var rootPath = configuration["Swagger:RootPath"];
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

            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();

            app.UseAbpRequestLocalization();
            app.UseAuthorization();
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api/{documentName}/swagger.json";
            });

            app.UseKnife4UI(c =>
            {
                c.DocumentTitle = "KnifeUI 电子病例微服务 API";
                c.RoutePrefix = "";
                c.SwaggerEndpoint($"/api/{name}/swagger.json", apiName);
            });

            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/api/{name}/swagger.json", apiName);
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelsExpandDepth(0);

            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();

            // 使用统一返回中间件
            app.UseUnifyResultStatusCodes();
        }

        /// <summary>
        /// 初始化完成
        /// </summary>
        /// <param name="context"></param>
        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPostApplicationInitialization(context);
        }
    }
}
