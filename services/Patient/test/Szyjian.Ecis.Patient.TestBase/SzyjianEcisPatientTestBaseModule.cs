using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Security;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;
using Volo.Abp.Threading;

namespace Szyjian.Ecis.Patient
{

    [DependsOn(
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule),
        typeof(SzyjianEcisPatientDomainModule)
        )]
    public class SzyjianEcisPatientTestBaseModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {

        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //Configure<AbpBackgroundJobOptions>(options =>
            //{
            //    options.IsJobExecutionEnabled = false;
            //});

            context.Services.AddSingleton<ILogger, NullLogger>();
            context.Services.AddSingleton<ICurrentPrincipalAccessor, FakeCurrentPrincipalAccessor>();

            //Func<IServiceProvider, IFreeSql> fsql = r =>
            //{
            //    IFreeSql fsql = new FreeSql.FreeSqlBuilder()
            //        .UseConnectionString(FreeSql.DataType.Sqlite, @"Server=192.168.1.162;Database=Ecis_Patient;uid=sa;pwd=DBA@samjan.com")
            //        .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句
            //        .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
            //        .Build();
            //    return fsql;
            //};
            //context.Services.AddSingleton<IFreeSql>(fsql);

            context.Services.AddAlwaysAllowAuthorization();            
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<ICurrentPrincipalAccessor, FakeCurrentPrincipalAccessor>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            //SeedTestData(context);
        }

        private static void SeedTestData(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>()
                        .SeedAsync();
                }
            });
        }
    }
}
