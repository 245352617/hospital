using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Nursing.EntityFrameworkCore
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class NursingHttpApiHostMigrationsDbContext : AbpDbContext<NursingHttpApiHostMigrationsDbContext>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="options"></param>
        public NursingHttpApiHostMigrationsDbContext(DbContextOptions<NursingHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureNursing();
        }
    }
}
