using Volo.Abp.Modularity;

namespace YiJian.Handover
{
    [DependsOn(
        typeof(HandoverApplicationModule),
        typeof(HandoverDomainTestModule)
        )]
    public class HandoverApplicationTestModule : AbpModule
    {

    }
}
