using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore; 

namespace YiJian.EMR.EntityFrameworkCore
{
    public class EMRHttpApiHostMigrationsDbContext : AbpDbContext<EMRHttpApiHostMigrationsDbContext>
    { 
        public EMRHttpApiHostMigrationsDbContext(DbContextOptions<EMRHttpApiHostMigrationsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.ConfigureEMR();
        }
    }
}
