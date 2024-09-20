using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace YiJian.BodyParts.EntityFrameworkCore.Migrations
{
    [DependsOn(typeof(EntityFrameworkCoreModule))]
    public class EntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<DbMigrationsContext>();
        }
    }
}
