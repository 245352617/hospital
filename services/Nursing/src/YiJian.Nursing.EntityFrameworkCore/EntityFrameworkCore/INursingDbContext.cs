using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Nursing.EntityFrameworkCore
{
    [ConnectionStringName(NursingDbProperties.ConnectionStringName)]
    public interface INursingDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */


    }
}
