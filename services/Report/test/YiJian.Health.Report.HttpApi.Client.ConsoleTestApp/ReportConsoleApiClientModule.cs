using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace YiJian.Health.Report
{
    [DependsOn(
        typeof(ReportHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class ReportConsoleApiClientModule : AbpModule
    {
        
    }
}
