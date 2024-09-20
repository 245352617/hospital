using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Handover.EntityFrameworkCore
{
    public class HandoverHttpApiHostMigrationsDbContext : AbpDbContext<HandoverHttpApiHostMigrationsDbContext>
    {
        public HandoverHttpApiHostMigrationsDbContext(DbContextOptions<HandoverHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureHandover();
        }
    }
}
