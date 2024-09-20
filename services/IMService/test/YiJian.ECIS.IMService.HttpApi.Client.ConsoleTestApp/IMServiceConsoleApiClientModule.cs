using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace YiJian.ECIS.IMService
{
    [DependsOn(
        typeof(IMServiceHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class IMServiceConsoleApiClientModule : AbpModule
    {
        
    }
}
