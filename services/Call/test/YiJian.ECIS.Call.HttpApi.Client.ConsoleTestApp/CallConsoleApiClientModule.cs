using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace YiJian.ECIS.Call
{
    [DependsOn(
        typeof(CallHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class CallConsoleApiClientModule : AbpModule
    {
        
    }
}
