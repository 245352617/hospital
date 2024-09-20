using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.EventBus.RabbitMq;

namespace YiJian.Health.Report
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(ReportDomainSharedModule),
		typeof(AbpEventBusRabbitMqModule))]
    public class ReportDomainModule : AbpModule
    {

    }
}
