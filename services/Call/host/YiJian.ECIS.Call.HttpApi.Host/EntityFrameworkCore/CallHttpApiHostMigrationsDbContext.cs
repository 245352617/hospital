using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.ECIS.Call.EntityFrameworkCore
{
    public class CallHttpApiHostMigrationsDbContext : AbpDbContext<CallHttpApiHostMigrationsDbContext>
    {
        public CallHttpApiHostMigrationsDbContext(DbContextOptions<CallHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureCall();
        }
    }
}
