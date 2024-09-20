using Volo.Abp.Modularity;

namespace YiJian.Nursing
{
    [DependsOn(
        typeof(NursingApplicationModule),
        typeof(NursingDomainTestModule)
        )]
    public class NursingApplicationTestModule : AbpModule
    {

    }
}
