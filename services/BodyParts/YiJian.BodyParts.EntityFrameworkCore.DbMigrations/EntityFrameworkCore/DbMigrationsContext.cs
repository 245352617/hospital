using YiJian.BodyParts.Model;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.BodyParts.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See DbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     */
    [ConnectionStringName("BodyParts")]
    public class DbMigrationsContext : AbpDbContext<DbMigrationsContext>,IEfCoreDbContext
    {
        public DbMigrationsContext(DbContextOptions<DbMigrationsContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            /* Configure your own tables/entities inside the Configure method */

            builder.Configure();

            builder.ConfigDatabaseDescription();
        }
    }
}