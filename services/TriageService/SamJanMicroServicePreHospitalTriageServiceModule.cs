using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using Hangfire;
using Hangfire.Redis;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.PreHospital.Core.Authentication;
using SkyApm.Utilities.DependencyInjection;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;
using Volo.Abp.Users;
using YJHealth.MedicalInsurance;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 院前分诊
    /// </summary>
    [DependsOn(typeof(SamJanMicroServicePreHospitalCoreModule))]
    public class SamJanMicroServicePreHospitalTriageServiceModule : AbpModule
    {
        private const string DefaultSystemName = "PreHospitalTriage";
        private const string DefaultServiceName = "TriageService";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var config = context.Services.GetConfiguration();

            context.Services.AddPollyHttpClient("PreHospitalTriage", config);
            context.Services.AddHttpClient("HisApi")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                });

            context.Services.AddAbpDbContext<PreHospitalTriageDbContext>(options =>
            {
                options.AddDefaultRepositories(true);
            });

            context.Services.Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            //context.Services.AddYiJianAuthentication();
            ConfigurationJwt(context.Services, config);
            // 覆盖 Abp 的当前用户定义，与急危重症一体化平台保持一致，不会因 Abp 版本更新导致问题
            context.Services.AddSingleton<ICurrentUser, EcisCurrentUser>();

            //是否跳过权限检查
            //SkipAuth(context.Services);

            context.Services.AddRedis();

            context.Services.AddConsulRegistry();

            context.Services.AddSwagger(new List<string> { "SamJan.MicroService.PreHospital.TriageService" });

            // 配置自动生成Controller
            context.Services.Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(
                    typeof(SamJanMicroServicePreHospitalTriageServiceModule).Assembly,
                    opt => { opt.RootPath = DefaultServiceName; });
            });

            // SkyWalking APM
            context.Services.AddSkyApmExtensions();

            //// 移除ABP默认拦截器
            //Configure<MvcOptions>(options =>
            //{
            //    var defaultFilter = options.Filters
            //        .FirstOrDefault(x =>
            //            x is ServiceFilterAttribute attribute
            //            && attribute.ServiceType == typeof(AbpExceptionFilter));
            //    options.Filters.Remove(defaultFilter);
            //    //加入拦截器
            //    options.Filters.Add(typeof(MyExceptionFilter));
            //});

            //添加事件总线CAP
            context.Services.AddCapClient<PreHospitalTriageDbContext>();

            //配置AbpVnext事件总线
            context.Services.AddEventBus();

            context.Services.AddSingleton<IDapperRepository, DapperRepository>();

            ConfigureHangfire(context.Services, config);

            // 省医保相关配置
            ConfigureInsuc();
            // 与调用 HIS 相关的依赖注入，可在此区分不同HIS的不同对接方式
            context.Services.ConfigureHisApi(config);

            ConfigureFreeSql(context.Services, config);

            // 注入外部 API
            context.Services.ConfigureExternalApi();
        }

        ///// <summary>
        ///// 通过设置默认策略，跳过所有接口的权限认证要求
        ///// 用人话说就是在appsetting.json文件配置了"IsSkipAuth": true的话，那么就相当于在所有接口上添加 [AllowAnonymous]
        ///// </summary>
        //private static void SkipAuth(IServiceCollection service)
        //{
        //    // 添加授权策略要求
        //    service.AddAuthorization(options =>
        //    {
        //        options.DefaultPolicy = new AuthorizationPolicyBuilder()
        //            //.RequireAuthenticatedUser()
        //            .AddRequirements(new IsSkipAuthRequirement())
        //            .Build();
        //    });
        //    // 添加自定义授权处理程序
        //    service.AddSingleton<IAuthorizationHandler, IsSkipAuthHandler>();
        //}

        /// <summary>
        /// 省医保相关配置
        /// </summary>
        private static void ConfigureInsuc()
        {
            // 配置文件
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.insuplc.json", true); // 医保配置
            var InsuConfiguration = builder.Build();
            MedicalInsuranceProxy.Config(options =>
            {
                options.BaseAddress = InsuConfiguration["gdsyb:url"];
                options.User = InsuConfiguration["gdsyb:user"];
                options.AppKey = InsuConfiguration["gdsyb:key"];
                options.Organization = InsuConfiguration["gdsyb:fixmedins_name"];
                options.OrganizationCode = InsuConfiguration["gdsyb:fixmedins_code"];
                options.AreaCode = InsuConfiguration["gdsyb:mdtrtarea_admvs"];
            });
        }

        /// <summary>
        /// 配置freesql，现在就只是用来做dataseed
        /// </summary>
        /// <param name="service"></param>
        /// <param name="configuration"></param>
        private static void ConfigureFreeSql(IServiceCollection service, IConfiguration configuration)
        {
            var fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, configuration.GetConnectionString("DefaultConnection"))
                .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句
                .UseAutoSyncStructure(false) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                .Build();

            service.AddSingleton(fsql);
        }


        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            var config = context.GetConfiguration();

            app.UseSwagger();

            //虚拟文件
            app.UseVirtualFiles();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCookiePolicy();

            //if (Convert.ToBoolean(config["Settings:IsEnabledAuthentication"]))
            //{
            app.UseAuthentication();
            //}

            app.UseAuthorization();

            app.UseConfiguredEndpoints(endpoint =>
            {
                endpoint.MapDefaultControllerRoute();
            });

            //初始化mapster 
            //(推荐： 将所有的映射规则写一起，当项目执行时一起进行初始化操作 ，之后直接使用，避免每次映射都需要进行初始化)
            MapsterMappingConfig.InitMapster();

            app.UseHangfireServer();
            app.UseHangfireDashboard(pathMatch: $"/api/{DefaultServiceName}/hangfire");
            app.UseGenDailyReportsBackgroundJob(config);

            //数据播种初始化
            SeedData(context);
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

                if (context.GetEnvironment().IsDevelopment())
                {
                    // 自动迁移文件
                    await scope.ServiceProvider.GetRequiredService<PreHospitalTriageDbContext>()
                    .Database
                    .MigrateAsync();
                }

                //启动发送种子数据
                //await scope.ServiceProvider.GetRequiredService<IDataSeeder>().SeedAsync();
            });
        }

        /// <summary>
        /// 配置Hangfire后台作业
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        private void ConfigureHangfire(IServiceCollection services, IConfiguration config)
        {
            var connection = ConnectionMultiplexer.Connect(config["Redis:Default:Connection"]);
            services.AddHangfire(configuration =>
            {
                configuration.UseNLogLogProvider();
                // 半小时自动过期成功的作业
                GlobalStateHandlers.Handlers.Add(new SucceedStateExpireHandler(TimeSpan.FromMinutes(30)));
                configuration.UseRedisStorage(connection, new RedisStorageOptions
                {
                    Db = Convert.ToInt32(config["Redis:Default:DefaultDB"]),
                    UseTransactions = true,
                    Prefix = $"{config["ServiceName"]}:Hangfire:"
                });
            });
        }

        private void ConfigurationJwt(IServiceCollection services, IConfiguration config)
        {
            bool skipValidate = bool.TryParse(config["AuthServer:SkipValidate"], out var result) && result;
            if (skipValidate)
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                services.AddAuthentication(o =>
                {
                    o.DefaultScheme = nameof(EcisApiResponseHandler);
                    o.DefaultChallengeScheme = nameof(EcisApiResponseHandler);
                    o.DefaultForbidScheme = nameof(EcisApiResponseHandler);
                }).AddScheme<AuthenticationSchemeOptions, EcisApiResponseHandler>(nameof(EcisApiResponseHandler), o =>
                {
                });
            }
            else
            {
                services.AddYiJianAuthentication();
            }
        }
    }
}