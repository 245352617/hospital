using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using YiJian.ECIS.DDP;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// Application Model
    /// </summary>
    [DependsOn(
        typeof(CallDomainModule),
        typeof(CallApplicationContractsModule),
        typeof(DdpModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule))]
    public class CallApplicationModule : AbpModule
    {
        //public IConfiguration Configuration { get; private set; }

        //public override void PreConfigureServices(ServiceConfigurationContext context)
        //{
        //    var env = context.Services.GetHostingEnvironment();
        //    var builder = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json", optional: false)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddJsonFile("appsettings.secrets.json")
        //        .AddJsonFile("triageLevelSettings.json")
        //        .AddJsonFile("triageTargets.json");
        //    Configuration = builder.Build();
        //    base.PreConfigureServices(context);
        //}

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<CallApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<CallApplicationModule>(validate: false);
            });

            //// 分诊登记配置
            //context.Services.Configure<TriageLevelSettingOptions>(Configuration.GetSection("TriageLevels"));
            //// 分诊去向配置
            //context.Services.Configure<TriageTargetSettingOptions>(Configuration.GetSection("TriageTargets"));
        }

        //public override void OnApplicationInitialization(ApplicationInitializationContext context)
        //{
        //    var app = context.GetApplicationBuilder();
        //    var env = context.GetEnvironment();

        //    var configuration = context.GetConfiguration();
        //}
    }
}
