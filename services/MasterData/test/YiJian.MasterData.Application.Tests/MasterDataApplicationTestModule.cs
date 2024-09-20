using Volo.Abp.Modularity;

namespace YiJian.MasterData
{
    [DependsOn(
        typeof(MasterDataApplicationModule),
        typeof(MasterDataDomainTestModule)
        )]
    public class MasterDataApplicationTestModule : AbpModule
    {

    }
}
