using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.ECIS.IMService.EntityFrameworkCore
{
    public class IMServiceHttpApiHostMigrationsDbContext : AbpDbContext<IMServiceHttpApiHostMigrationsDbContext>
    {
        public IMServiceHttpApiHostMigrationsDbContext(DbContextOptions<IMServiceHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureIMService();
        }
    }
}
