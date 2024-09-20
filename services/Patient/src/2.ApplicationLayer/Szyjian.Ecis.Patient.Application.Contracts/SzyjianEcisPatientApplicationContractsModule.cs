using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    [DependsOn(typeof(SzyjianEcisPatientDomainSharedModule))]
    public class SzyjianEcisPatientApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {

        }
    }
}
