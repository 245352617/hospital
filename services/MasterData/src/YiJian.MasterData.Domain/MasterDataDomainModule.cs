using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.EventBus.RabbitMq;

namespace YiJian.MasterData;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(MasterDataDomainSharedModule),
    typeof(AbpEventBusRabbitMqModule))]
public class MasterDataDomainModule : AbpModule
{

}
