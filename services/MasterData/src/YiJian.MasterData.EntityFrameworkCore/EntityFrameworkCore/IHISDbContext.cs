using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.EntityFrameworkCore;

[ConnectionStringName("HIS")]
public interface IHISDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    //药品表
    DbSet<HISMedicine> V_Emergency_DrugStockQuery { get; }
     
}
