using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Health.Report.EntityFrameworkCore
{
    public class ReportHttpApiHostMigrationsDbContext : AbpDbContext<ReportHttpApiHostMigrationsDbContext>
    {
        public ReportHttpApiHostMigrationsDbContext(DbContextOptions<ReportHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureReport();
        }
    }
}
