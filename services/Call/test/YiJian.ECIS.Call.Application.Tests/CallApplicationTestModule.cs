using Volo.Abp.Modularity;

namespace YiJian.ECIS.Call
{
    [DependsOn(
        typeof(CallApplicationModule),
        typeof(CallDomainTestModule)
        )]
    public class CallApplicationTestModule : AbpModule
    {

    }
}
