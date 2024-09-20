using Microsoft.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;
using YiJian.ECIS.Core;
using YiJian.ECIS.DDP;
using YiJian.ECIS.Grpc;
using YiJian.MasterData.Domain;
using YiJian.MasterData.MasterData.HospitalInfo;
using System.Collections.Generic;
using YiJian.MasterData.Exams;

namespace YiJian.MasterData;

[DependsOn(
    typeof(MasterDataDomainModule),
    typeof(MasterDataApplicationContractsModule),
    typeof(DdpModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpBackgroundJobsHangfireModule))]
public class MasterDataApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<MasterDataApplicationModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MasterDataApplicationModule>(validate: false);
        });


        var configuration = context.Services.GetConfiguration();
        Configure<HospitalInfoConfigDto>(configuration.GetSection("HospitalInfoConfig"));
        Configure<List<ExamExecDeptConfig>>(configuration.GetSection("ExamExecDeptConfigs"));

        // 添加 Grpc 终结点配置
        context.Services.AddAppServiceGrpc();
    }
}
