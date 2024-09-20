using System;
using YiJian.BodyParts.EntityFrameworkCore.Migrations;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace YiJian.BodyParts.DbMigrator
{
    [DependsOn(typeof(EntityFrameworkCoreDbMigrationsModule))]
    public class DbMigrationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });
        }
    }
}
