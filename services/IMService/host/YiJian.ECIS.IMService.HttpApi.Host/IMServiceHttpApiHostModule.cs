using EasyAbp.Abp.EventBus.Cap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YiJian.ECIS.IMService.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
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
using Volo.Abp.VirtualFileSystem;
using YiJian.ECIS.Core;
using YiJian.ECIS.Core.Middlewares;
using YiJian.ECIS.IMService.Hubs;
using Volo.Abp.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace YiJian.ECIS.IMService
{
    [DependsOn(
        typeof(IMServiceApplicationModule),
        typeof(IMServiceEntityFrameworkCoreModule),
        typeof(IMServiceHttpApiModule),
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
        typeof(AbpAspNetCoreSignalRModule)
        )]
    public class IMServiceHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<IMServiceDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.ECIS.IMService.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<IMServiceDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.ECIS.IMService.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<IMServiceApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.ECIS.IMService.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<IMServiceApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.ECIS.IMService.Application", Path.DirectorySeparatorChar)));
                });
            }

            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(IMServiceApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = configuration["Swagger:RootPath"];
                });
            });

            #region 权限处理
            //context.Services.AddAuthentication(options =>
            //{
            //}).AddJwtBearer("JwtBearer", options =>
            //{
            //    options.Events = new JwtBearerEvents
            //    {
            //        OnMessageReceived = (context) =>
            //        {
            //            System.Console.WriteLine(context.Token);
            //            return Task.CompletedTask;
            //        }
            //    };
            //});
            #endregion

            // 添加默认的CAP配置
            context.Services.AddEcisDefaultCap();
            context.AddEcisDefaultCapEventBus();

            // CAP 配置
            context.Services.AddCap(x =>
            {
                x.DefaultGroupName = configuration[$"RabbitMQ:CapBus:DefaultGroupName"];
                x.UseSqlServer(opt =>
                {
                    opt.ConnectionString = configuration[$"ConnectionStrings:IMService"];
                });
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
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api/{documentName}/swagger.json";
            });

            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/api/{name}/swagger.json", apiName);
                options.DocExpansion(DocExpansion.List);
                options.DefaultModelsExpandDepth(0);
            });
            app.UseAuditing();
            app.UseConfiguredEndpoints();
            app.UseAbpSerilogEnrichers();

            // 使用统一返回中间件
            app.UseUnifyResultStatusCodes();
        }
    }
}
