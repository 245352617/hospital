using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Szyjian.Ecis.Patient.Application;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Security;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;

namespace Szyjian.Ecis.Patient
{
    [DependsOn(
        typeof(SzyjianEcisPatientApplicationModule),
        typeof(SzyjianEcisPatientDomainModule)
        )]
    public class SzyjianEcisPatientApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<ILogger, NullLogger>();
            context.Services.AddSingleton<ICurrentPrincipalAccessor, FakeCurrentPrincipalAccessor>();
        }
    }

}