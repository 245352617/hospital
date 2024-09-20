using System;
using Volo.Abp;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Szyjian.BaseDomain
{
    /// <summary>
    /// 领域模块配置
    /// </summary>
    [DependsOn(new Type[] { typeof(AbpDddDomainModule) })]
    public class SzyjianBaseDomainModule : AbpModule
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
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