using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace YiJian.Nursing
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(NursingDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class NursingApplicationContractsModule : AbpModule
    {

    }
}
