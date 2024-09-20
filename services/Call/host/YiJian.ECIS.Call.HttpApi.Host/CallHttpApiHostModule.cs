using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Threading;
using Volo.Abp.Users;
using Volo.Abp.VirtualFileSystem;
using YiJian.ECIS.Authorization;
using YiJian.ECIS.Call.EntityFrameworkCore;
using YiJian.ECIS.Core;
using YiJian.ECIS.Core.Middlewares;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.ECIS.Call
{
    [DependsOn( typeof(CallApplicationModule),
        typeof(CallEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreSignalRModule),
        typeof(CallHttpApiModule),
        typeof(ECISCoreModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(EcisAuthorizationModule))]
    public class CallHttpApiHostModule : AbpModule
    {
        public IConfiguration Configuration { get; private set; }

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var env = context.Services.GetHostingEnvironment();
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("appsettings.secrets.json");
            Configuration = builder.Build();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });
            AddVirtualFileSystem(context.Services);

            ConfigureMvcExtension();

            //注入患者请求参数
            context.Services.AddHttpClient(BaseAddress.Patient, c =>
            {
                c.BaseAddress = new Uri(Configuration["RemoteServices:Patient:BaseUrl"]);
            });
            // CAP 配置
            AddCap(context.Services);

            // 统一返回配置
            context.AddUnifyResult<RESTfulUnifyResultProvider>();

            // 覆盖 Abp 的当前用户定义，与急危重症一体化平台保持一致，不会因 Abp 版本更新导致问题
            context.Services.AddSingleton<ICurrentUser, EcisCurrentUser>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            var name = Configuration["Swagger:Name"];
            var apiName = Configuration["Swagger:apiName"];
            var rootPath = Configuration["Swagger:RootPath"];
            var apiTitle = Configuration["Swagger:apiTitle"];

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
            app.UseAbpSerilogEnrichers();
            //app.UseUnitOfWork();
            app.UseConfiguredEndpoints();

            // 使用统一返回中间件
            app.UseUnifyResultStatusCodes();

            //数据播种初始化
            //SeedData(context);
        }

        private void ConfigureMvcExtension()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(CallApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = Configuration["Swagger:RootPath"];
                });
            });
        }

        private void AddVirtualFileSystem(IServiceCollection service)
        {
            var env = service.GetHostingEnvironment();
            if (env.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<CallDomainSharedModule>(Path.Combine(env.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.ECIS.Call.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<CallDomainModule>(Path.Combine(env.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.ECIS.Call.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<CallApplicationContractsModule>(Path.Combine(env.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.ECIS.Call.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<CallApplicationModule>(Path.Combine(env.ContentRootPath, string.Format("..{0}..{0}src{0}YiJian.ECIS.Call.Application", Path.DirectorySeparatorChar)));
                });
            }
        }

        private void AddCap(IServiceCollection service)
        {
            //// 默认 CAP 配置
            ////（Core里面的CAP与abp eventbus 使用同一个交换机，然而CAP使用的类型是topic而abp eventbus使用的类型是direct，所以Core里面的配置必然会导致abp eventbus异常）
            //// by: ywlin-20211027
            //context.Services.AddEcisDefaultCap();
            //context.AddEcisDefaultCapEventBus();
            service.AddCap(x =>
            {
                //x.DefaultGroupName = Configuration[$"RabbitMQ:CapBus:DefaultGroupName"];
                x.UseSqlServer(opt =>
                {
                    opt.ConnectionString = Configuration[$"ConnectionStrings:Call"];
                });
                x.UseRabbitMQ(options =>
                {
                    options.ExchangeName = Configuration["RabbitMQ:CapBus:ExchangeName"];
                    options.UserName = Configuration["RabbitMQ:Connections:Default:UserName"];
                    options.Password = Configuration["RabbitMQ:Connections:Default:Password"];
                    options.HostName = Configuration["RabbitMQ:Connections:Default:HostName"];
                    options.Port = int.Parse(Configuration["RabbitMQ:Connections:Default:Port"] ?? "5672");
                });
                x.UseDashboard();  // CAP provides dashboard pages after the version 2.x
            });
        }

        /// <summary>
        /// 自动迁移、生成种子数据
        /// </summary>
        /// <param name="context"></param>
        private void SeedData(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                using var scope = context.ServiceProvider.CreateScope();

                //启动生成数据库
                /*await scope.ServiceProvider.GetRequiredService<CallDbContext>()
                    .Database.EnsureCreatedAsync();*/
                //if (context.GetEnvironment().IsDevelopment())
                //    // 自动迁移文件
                //    await scope.ServiceProvider.GetRequiredService<CallDbContext>()
                //        .Database
                //        .MigrateAsync();

                //启动发送种子数据
                await scope.ServiceProvider.GetRequiredService<IDataSeeder>()
                    .SeedAsync();
            });
        }
    }
}
