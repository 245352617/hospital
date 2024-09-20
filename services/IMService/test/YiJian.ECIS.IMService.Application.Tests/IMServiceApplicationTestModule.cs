using Volo.Abp.Modularity;

namespace YiJian.ECIS.IMService
{
    [DependsOn(
        typeof(IMServiceApplicationModule),
        typeof(IMServiceDomainTestModule)
        )]
    public class IMServiceApplicationTestModule : AbpModule
    {

    }
}
