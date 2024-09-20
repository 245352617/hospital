using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace YiJian.Handover
{
    [DependsOn(
        typeof(HandoverDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class HandoverApplicationContractsModule : AbpModule
    {

    }
}
