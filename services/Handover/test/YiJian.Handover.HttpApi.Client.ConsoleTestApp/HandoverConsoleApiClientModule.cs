using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace YiJian.Handover
{
    [DependsOn(
        typeof(HandoverHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class HandoverConsoleApiClientModule : AbpModule
    {
        
    }
}
