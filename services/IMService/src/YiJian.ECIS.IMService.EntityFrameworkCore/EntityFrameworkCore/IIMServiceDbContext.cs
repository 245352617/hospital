using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.ECIS.IMService.EntityFrameworkCore
{
    [ConnectionStringName(IMServiceDbProperties.ConnectionStringName)]
    public interface IIMServiceDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}