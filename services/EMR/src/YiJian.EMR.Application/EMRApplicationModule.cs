using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using YiJian.ECIS.Grpc;
using System;
using YiJian.ECIS.ShareModel.Models;
using Autofac.Core;
using YiJian.EMR.HttpClients;
using YiJian.EMR.HospitalClients;
using YiJian.EMR.HttpClients.Dto;

namespace YiJian.EMR
{
    [DependsOn(
        typeof(EMRDomainModule),
        typeof(EMRApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class EMRApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            context.Services.AddAutoMapperObjectMapper<EMRApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EMRApplicationModule>(validate: true);
            });

            //注入患者请求参数
            context.Services.AddHttpClient(ECIS.ShareModel.Models.BaseAddress.CloudSign, c =>
            {
                c.BaseAddress = new Uri(configuration["RemoteServices:CloudSign:BaseUrl"]);
            });
            context.Services.Configure<RemoteServices>(configuration.GetSection("RemoteServices")); //远程服务地址配置
            context.Services.AddSingleton<IPatientAppService, PatientAppService>();  

            context.Services.AddAppServiceGrpc();

        }
    }
}
