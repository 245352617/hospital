using MasterDataService;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;
using YiJian.Basic;
using YiJian.ECIS.Authorization;
using YiJian.ECIS.Core.Redis;
using YiJian.ECIS.DDP;
using YiJian.ECIS.Grpc;
using YiJian.ECIS.ShareModel.Models;
using YiJian.Hospitals;

namespace YiJian.Recipe
{
    /// <summary>
    /// RecipeApplicationModule
    /// </summary>
    [DependsOn(
        typeof(RecipeDomainModule),
        typeof(RecipeApplicationContractsModule),
        typeof(DdpModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpBackgroundJobsHangfireModule),
        typeof(EcisAuthorizationModule)
    )]
    public class RecipeApplicationModule : AbpModule
    {
        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var services = context.Services;
            ConfigureRedis(services, configuration);

            context.Services.AddAutoMapperObjectMapper<RecipeApplicationModule>();
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<RecipeApplicationModule>(validate: true); });

            context.Services.AddGrpcClient<GrpcMasterData.GrpcMasterDataClient>(o =>
            {
                var configuration = context.Services.GetConfiguration();
                o.Address = new Uri(configuration["GrpcMasterData"]);
            });

            //注入httpcontextaccessor
            context.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //注入患者请求参数
            context.Services.AddHttpClient(BaseAddress.Patient, c =>
            {
                c.BaseAddress = new Uri(configuration["RemoteServices:Patient:BaseUrl"]);
            });

            //龙岗中心医院HIS系统 
            context.Services.AddHttpClient(BaseAddress.Hospital, c =>
            {
                c.BaseAddress = new Uri(configuration["RemoteServices:Hospital:BaseUrl"]);
            });

            context.Services.AddAppServiceGrpc();

            // 根据医院不同HIS注入不同的API，以适配多个医院
            context.Services.ConfigureHospitalApi(configuration);
        }

        /// <summary>
        /// 注入Redis
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        private void ConfigureRedis(IServiceCollection services, IConfiguration config)
        {
            services.AddRedis(option =>
            {
                option.Connection = config["Redis:Configuration"];
                option.InstanceName = config["Redis:InstanceName"];
                option.DefaultDb = int.TryParse(config["Redis:DefaultDb"], out int ret) ? ret : 0;
            });
        }

        /// <summary>
        /// 同步医嘱项目
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task SyncMedicineAsync(ApplicationInitializationContext context)
        {
            using var scope = context.ServiceProvider.CreateScope();
            var logger = context.ServiceProvider.GetService<ILogger>();
            try
            {
                var recipeProjectGrpc = scope.ServiceProvider.GetRequiredService<RecipeProjectGrpcAppService>();
                await recipeProjectGrpc.SyncRecipeProjectAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Grpc 同步医嘱项目失败: {ex.Message}");
            }
        }

    }
}