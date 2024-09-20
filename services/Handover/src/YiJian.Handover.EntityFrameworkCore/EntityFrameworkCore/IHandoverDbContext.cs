using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Handover.EntityFrameworkCore
{
    using YiJian.Handover;
    [ConnectionStringName(HandoverDbProperties.ConnectionStringName)]
    public interface IHandoverDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

        
        //护士交班
        DbSet<NurseHandover> NurseHandovers { get; }
    }
}
