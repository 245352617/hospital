using Volo.Abp.Modularity;

namespace YiJian.Health.Report
{
    [DependsOn(
        typeof(ReportApplicationModule),
        typeof(ReportDomainTestModule)
        )]
    public class ReportApplicationTestModule : AbpModule
    {

    }
}
