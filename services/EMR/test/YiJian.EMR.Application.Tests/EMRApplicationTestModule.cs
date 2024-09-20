using Volo.Abp.Modularity;

namespace YiJian.EMR
{
    [DependsOn(
        typeof(EMRApplicationModule),
        typeof(EMRDomainTestModule)
        )]
    public class EMRApplicationTestModule : AbpModule
    {

    }
}
