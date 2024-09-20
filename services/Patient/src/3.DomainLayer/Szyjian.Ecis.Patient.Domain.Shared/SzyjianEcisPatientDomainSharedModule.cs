using Volo.Abp;
using Volo.Abp.Modularity;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 领域共享模块配置
    /// </summary>
    [DependsOn(typeof(SzyjianEcisPatientDomainSharedModule))]
    public class SzyjianEcisPatientDomainSharedModule : AbpModule
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
