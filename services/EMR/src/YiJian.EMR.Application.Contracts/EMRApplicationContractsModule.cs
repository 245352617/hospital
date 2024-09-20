using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace YiJian.EMR
{
    [DependsOn(
        typeof(EMRDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class EMRApplicationContractsModule : AbpModule
    {

    }
}
