using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Config;
using YiJian.Nursing.RecipeExecutes.Entities;
using YiJian.Nursing.Recipes.Entities;
using YiJian.Nursing.Settings;
using YiJian.Nursing.Temperatures;

namespace YiJian.Nursing.EntityFrameworkCore
{
    [ConnectionStringName(NursingDbProperties.ConnectionStringName)]
    public class NursingDbContext : AbpDbContext<NursingDbContext>, INursingDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        /// <summary>
        /// 医嘱表
        /// </summary>
        public DbSet<Recipe> Recipes { get; set; }

        /// <summary>
        /// 医嘱操作历史表
        /// </summary>
        public DbSet<RecipeHistory> RecipeHistories { get; set; }

        /// <summary>
        /// 药物处方表
        /// </summary>
        public DbSet<Prescribe> Prescribes { get; set; }

        /// <summary>
        /// 检查表
        /// </summary>
        public DbSet<Pacs> Pacss { get; set; }

        /// <summary>
        /// 检验表
        /// </summary>
        public DbSet<Lis> Liss { get; set; }

        /// <summary>
        /// 诊疗表
        /// </summary>
        public DbSet<Treat> Treats { get; set; }

        /// <summary>
        /// 执行记录表
        /// </summary>
        public DbSet<RecipeExecRecord> RecipeExecRecords { get; set; }

        /// <summary>
        /// 医嘱执行表
        /// </summary>
        public DbSet<RecipeExec> RecipeExecs { get; set; }

        /// <summary>
        /// 医嘱执行历史记录
        /// </summary>
        public DbSet<RecipeExecHistory> RecipeExecHistories { get; set; }

        /// <summary>
        /// 自备药表
        /// </summary>
        public DbSet<OwnMedicine> OwnMedicines { get; set; }

        /// <summary>
        /// 模块参数
        /// </summary>
        public DbSet<ParaModule> ParaModules { get; set; }

        /// <summary>
        /// 护理项目表
        /// </summary>
        public DbSet<ParaItem> ParaItems { get; set; }

        /// <summary>
        /// 导管字典-通用业务
        /// </summary>
        public DbSet<Dict> Dicts { get; set; }

        /// <summary>
        /// 人体图-编号字典
        /// </summary>
        public DbSet<CanulaPart> CanulaParts { get; set; }

        /// <summary>
        /// 导管护理信息
        /// </summary>
        public DbSet<NursingCanula> NursingCanula { get; set; }

        /// <summary>
        /// 导管护理记录信息
        /// </summary>
        public DbSet<Canula> Canula { get; set; }

        /// <summary>
        /// 导管参数动态表
        /// </summary>
        public DbSet<CanulaDynamic> CanulaDynamic { get; set; }

        /// <summary>
        /// 临床事件表
        /// </summary>
        public DbSet<ClinicalEvent> ClinicalEvents { get; set; }

        /// <summary>
        /// 体温表动态属性
        /// </summary>
        public DbSet<TemperatureDynamic> TemperatureDynamics { get; set; }

        /// <summary>
        /// 体温表记录
        /// </summary>
        public DbSet<TemperatureRecord> TemperatureRecords { get; set; }

        /// <summary>
        /// 体温单
        /// </summary>
        public DbSet<Temperature> Temperatures { get; set; }

        /// <summary>
        /// 护士站通用配置表
        /// </summary>
        public DbSet<NursingConfig> NursingConfigs { get; set; }

        public NursingDbContext(DbContextOptions<NursingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureNursing();
        }
    }
}
