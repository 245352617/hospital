using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace YiJian.Nursing
{
    [DependsOn(
        typeof(NursingHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class NursingConsoleApiClientModule : AbpModule
    {
        
    }
}
