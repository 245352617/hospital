using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.ECIS.IMService.EntityFrameworkCore
{
    [ConnectionStringName(IMServiceDbProperties.ConnectionStringName)]
    public class IMServiceDbContext : AbpDbContext<IMServiceDbContext>, IIMServiceDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public IMServiceDbContext(DbContextOptions<IMServiceDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureIMService();
        }
    }
}