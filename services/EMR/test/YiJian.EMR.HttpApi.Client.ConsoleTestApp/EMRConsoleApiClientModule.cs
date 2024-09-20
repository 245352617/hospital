using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace YiJian.EMR
{
    [DependsOn(
        typeof(EMRHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EMRConsoleApiClientModule : AbpModule
    {
        
    }
}
