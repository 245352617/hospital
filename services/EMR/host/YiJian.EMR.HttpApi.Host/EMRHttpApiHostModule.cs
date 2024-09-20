using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
using YiJian.ECIS.ShareModel.Models;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Templates.Dto;
using YiJian.EMR.Writes.Dto;

namespace YiJian.EMR
{
    [DependsOn(
        typeof(EMRApplicationModule),
        typeof(EMREntityFrameworkCoreModule),
        typeof(EMRHttpApiModule),
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
        typeof(EcisAuthorizationModule)
        )]
    public class EMRHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();
            context.Services.Configure<PushEmrDataSetting>(configuration.GetSection("PushEmrData")); //需要推送给医嘱的配置
            context.Services.Configure<EmrWatermarkModel>(configuration.GetSection("EmrWatermark"));//电子病历水印配置

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<EMRDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.EMR.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<EMRDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.EMR.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<EMRApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.EMR.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<EMRApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.EMR.Application", Path.DirectorySeparatorChar)));
                });
            }

            var minioSetting = configuration.GetSection("MinioSetting");
            context.Services.Configure<MinioSetting>(minioSetting);
            context.Services.Configure<CloudSign.CloudSign>(configuration.GetSection("CloudSign"));


            //// 默认 CAP 配置
            ////（Core里面的CAP与abp eventbus 使用同一个交换机，然而CAP使用的类型是topic而abp eventbus使用的类型是direct，所以Core里面的配置必然会导致abp eventbus异常）
            //// by: ywlin-20211027
            //context.Services.AddEcisDefaultCap();
            //context.AddEcisDefaultCapEventBus();
            //// CAP 配置
            context.Services.AddCap(x =>
            {
                x.DefaultGroupName = configuration[$"RabbitMQ:CapBus:DefaultGroupName"];
                x.UseSqlServer(opt => { opt.ConnectionString = configuration[$"ConnectionStrings:EMR"]; });
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
                options.ConventionalControllers.Create(typeof(EMRApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = configuration["Swagger:RootPath"].ToLower();
                });
            });

            context.Services.AddMvc().AddSessionStateTempDataProvider();
            context.Services.AddSession();
            context.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            context.Services.Configure<FormOptions>(options => { options.ValueCountLimit = 5000; options.ValueLengthLimit = 2097152000; });
            context.Services.AddControllersWithViews();

            //Configure<JsonOptions>(options => {
            //    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            //});
            // 统一返回配置
            context.AddErrorUnifyResult<RESTfulUnifyResultProvider>();

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
            var rootPath = configuration["Swagger:RootPath"];

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


            //app.UseCookiePolicy();
            //app.UseSession();

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
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                options.DefaultModelsExpandDepth(0);
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
