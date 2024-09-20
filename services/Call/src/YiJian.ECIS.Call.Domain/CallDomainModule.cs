using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.EventBus.RabbitMq;

namespace YiJian.ECIS.Call
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(CallDomainSharedModule),
        typeof(AbpEventBusRabbitMqModule))]
    public class CallDomainModule : AbpModule
    {
       
    }
}
