using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Szyjian.BaseFreeSql
{
    /// <summary>
    /// freesql模块
    /// </summary>
    [DependsOn(new Type[] { typeof(AbpDddDomainModule) })]
    public class SzyjianBaseFreeSqlModule : AbpModule
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            IConfiguration configuration = context.Services.GetConfiguration();
            context.Services.AddFreeSqlForSqlServer(configuration.GetConnectionString("Default"));
        }

        /// <summary>
        /// 应用配置
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
        }
    }
}
