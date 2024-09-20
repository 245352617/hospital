using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Szyjian.BaseDomain;
using Szyjian.BaseFreeSql;
using Szyjian.Ecis.Patient.Domain.Common.Extensions;
using Volo.Abp.Modularity;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(SzyjianBaseDomainModule),
        typeof(SzyjianBaseFreeSqlModule))]
    public class SzyjianEcisPatientDomainModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            IConfiguration configuration = context.Services.GetConfiguration();
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            context.Services.AddFreeSql<Def>(configuration.GetConnectionString("Default"), DataType.SqlServer, false);
            if (configuration["HospitalCode"] == "PKU")
            {
                context.Services.AddFreeSql<His_Ids>(configuration.GetConnectionString("His_Ids"));
                context.Services.AddFreeSql<His_View>(configuration.GetConnectionString("His_View"));
            }
            context.Services.AddFreeRepository(filter =>
                filter.Apply<AuditEntity>("SoftDelete", a => a.IsDeleted == false));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Def
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class His_Ids
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class His_View
    {
    }

}