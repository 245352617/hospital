using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace YiJian.ECIS.Call
{
    [DependsOn(
        typeof(CallDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class CallApplicationContractsModule : AbpModule
    {

    }
}
