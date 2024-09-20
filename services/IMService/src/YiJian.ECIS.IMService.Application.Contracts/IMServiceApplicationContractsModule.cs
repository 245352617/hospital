using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace YiJian.ECIS.IMService
{
    [DependsOn(
        typeof(IMServiceDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class IMServiceApplicationContractsModule : AbpModule
    {

    }
}
