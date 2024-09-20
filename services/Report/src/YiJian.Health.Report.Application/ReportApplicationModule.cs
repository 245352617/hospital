using MasterDataService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RecipeService;
using System;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using YiJian.ECIS.DDP;
using YiJian.ECIS.Grpc;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.Health.Report
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(ReportDomainModule),
        typeof(ReportApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(DdpModule)
        )]
    public class ReportApplicationModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddAutoMapperObjectMapper<ReportApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ReportApplicationModule>(validate: true);
            });

            context.Services.AddGrpcClient<GrpcMasterData.GrpcMasterDataClient>(o =>
            {
                var configuration = context.Services.GetConfiguration();
                o.Address = new Uri(configuration["GrpcMasterData"]);
            });

            context.Services.AddGrpcClient<GrpcRecipe.GrpcRecipeClient>(o =>
            {
                var configuration = context.Services.GetConfiguration();
                o.Address = new Uri(configuration["GrpcRecipe"]);
            });

            //注入httpcontextaccessor
            context.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //注入字典服务
            context.Services.AddHttpClient(BaseAddress.Masterdata, c =>
            {
                c.BaseAddress = new Uri(configuration["RemoteServices:Default:BaseUrl"]);
            });
            //注入预检分诊服务
            context.Services.AddHttpClient(BaseAddress.Triage, c =>
            {
                c.BaseAddress = new Uri(configuration["RemoteServices:Triage:BaseUrl"]);
            });

            //注入患者服务
            context.Services.AddHttpClient(BaseAddress.Patient, c =>
            {
                c.BaseAddress = new Uri(configuration["RemoteServices:Patient:BaseUrl"]);
            });
            //注入医嘱请求
            context.Services.AddHttpClient(BaseAddress.Recipe, c =>
            {
                c.BaseAddress = new Uri(configuration["RemoteServices:Recipe:BaseUrl"]);
            });
            //注入分诊请求
            context.Services.AddHttpClient(BaseAddress.Nursing, c =>
            {
                c.BaseAddress = new Uri(configuration["RemoteServices:Nursing:BaseUrl"]);
            });

            // 添加 Grpc 终结点配置
            context.Services.AddAppServiceGrpc();
        }
    }
}
