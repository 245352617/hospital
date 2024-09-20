using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.EntityFrameworkCore;


[ConnectionStringName("HIS")]
public class HISDbContext : AbpDbContext<HISDbContext>,IHISDbContext
{

    //药品表
    public DbSet<HISMedicine> V_Emergency_DrugStockQuery { get; set; }


    public HISDbContext(DbContextOptions<HISDbContext> options)
       : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureHISData();
    }


}
