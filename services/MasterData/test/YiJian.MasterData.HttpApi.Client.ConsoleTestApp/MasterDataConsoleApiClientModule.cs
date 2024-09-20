using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace YiJian.MasterData
{
    [DependsOn(
        typeof(MasterDataHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class MasterDataConsoleApiClientModule : AbpModule
    {
        
    }
}
