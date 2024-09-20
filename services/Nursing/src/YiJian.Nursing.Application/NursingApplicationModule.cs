using MasterDataService;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using YiJian.ECIS.DDP;
using YiJian.ECIS.Grpc;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.Nursing
{
    /// <summary>
    /// 模块配置
    /// </summary>
    [DependsOn(
        typeof(NursingDomainModule),
        typeof(NursingApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpCachingModule),
        typeof(DdpModule),
        typeof(AbpEventBusRabbitMqModule),
        typeof(AbpCachingModule)
        )]
    public class NursingApplicationModule : AbpModule
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            context.Services.AddAutoMapperObjectMapper<NursingApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<NursingApplicationModule>(validate: true);
            });

            //注入患者请求参数
            context.Services.AddHttpClient(BaseAddress.Patient, c =>
            {
                c.BaseAddress = new Uri(configuration["RemoteServices:Patient:BaseUrl"]);
            });

            context.Services.AddGrpcClient<GrpcMasterData.GrpcMasterDataClient>(o =>
            {
                var configuration = context.Services.GetConfiguration();
                o.Address = new Uri(configuration["GrpcMasterData"]);
            });

            context.Services.AddAppServiceGrpc();
        }
    }
}
