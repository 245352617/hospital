using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.ECIS.Call.EntityFrameworkCore
{
    [ConnectionStringName(CallDbProperties.ConnectionStringName)]
    public interface ICallDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}