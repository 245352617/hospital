using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.EventBus.RabbitMq;

namespace YiJian.ECIS.IMService
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(IMServiceDomainSharedModule),
		typeof(AbpEventBusRabbitMqModule))]
    public class IMServiceDomainModule : AbpModule
    {

    }
}
