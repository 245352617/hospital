using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using YiJian.AuditLogs.Entities;
using YiJian.Cases.Entities;
using YiJian.DoctorsAdvices.Entities;
using YiJian.Emrs.Entities;
using YiJian.OwnMedicines.Entities;
using YiJian.Recipe.Packages;
using YiJian.Recipes.DoctorsAdvices.Entities;
using YiJian.Recipes.GroupConsultation;
using YiJian.Recipes.InviteDoctor;
using YiJian.Recipes.Preferences.Entities;
using YiJian.Sequences.Entities;

namespace YiJian.Recipe.EntityFrameworkCore
{
    /// <summary>
    /// 数据上下文
    /// </summary>
    [ConnectionStringName(RecipeDbProperties.ConnectionStringName)]
    public class RecipeDbContext : AbpDbContext<RecipeDbContext>, IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        //public DbSet<OperationApply> OperationApply { get; set; }

        /// <summary>
        /// 会诊邀请医生
        /// </summary>
        DbSet<InviteDoctor> InviteDoctors { get; set; }

        /// <summary>
        /// 会诊管理
        /// </summary>
        DbSet<GroupConsultation> GroupConsultations { get; set; }

        /// <summary>
        /// 会诊配置
        /// </summary>
        DbSet<SettingPara> SettingPara { get; set; }

        /// <summary>
        /// 会诊纪要医生
        /// </summary>
        DbSet<DoctorSummary> DoctorSummary { get; set; }

        /// <summary>
        /// 手术申请
        /// </summary>
        public DbSet<OperationApply> OperationApplies { get; set; }

        /// <summary>
        /// 医嘱套餐
        /// </summary>
        public DbSet<Package> Packages { get; set; }

        /// <summary>
        /// 医嘱套餐分组
        /// </summary>
        public DbSet<PackageGroup> PackageGroups { get; set; }

        // 非聚合根实体，不开放DbSet
        ///// <summary>
        ///// 医嘱套餐-医嘱信息
        ///// </summary>
        //public DbSet<PackageRecipe> PackageRecipes { get; set; }

        ///// <summary>
        ///// 医嘱套餐-检验项目
        ///// </summary>
        //public DbSet<PackageLabProject> PackageLabProjects { get; set; }

        /// <summary>
        /// 医嘱
        /// </summary>
        public DbSet<DoctorsAdvice> DoctorsAdvices { get; set; }

        /// <summary>
        /// 药方项
        /// </summary>
        public DbSet<Prescribe> Prescribes { get; set; }

        /// <summary>
        /// 自定义规则药品一次剂量名单 (自己维护)
        /// </summary>
        public DbSet<PrescribeCustomRule> PrescribeCustomRules { get; set; }

        /// <summary>
        /// 药理等级
        /// </summary>
        public DbSet<Toxic> Toxics { get; set; }

        /// <summary>
        /// 检验项
        /// </summary>
        public DbSet<Lis> Lises { get; set; }

        /// <summary>
        /// 检验项列表
        /// </summary>
        public DbSet<LisItem> LisItems { get; set; }

        /// <summary>
        /// 检查项
        /// </summary>
        public DbSet<Pacs> Pacses { get; set; }

        /// <summary>
        /// 检查项列表
        /// </summary>
        public DbSet<PacsItem> PacsItems { get; set; }

        /// <summary>
        /// 检查病理小项
        /// </summary>
        public DbSet<PacsPathologyItem> PacsPathologyItems { get; set; }

        /// <summary>
        /// 检查病理小项序号
        /// </summary>
        public DbSet<PacsPathologyItemNo> PacsPathologyItemNos { get; set; }

        /// <summary>
        /// 打印信息
        /// </summary>
        public DbSet<PrintInfo> PrintInfos { get; set; }

        /// <summary>
        /// 诊疗项
        /// </summary>
        public DbSet<Treat> Treats { get; set; }

        /// <summary>
        /// 医嘱审计
        /// </summary>
        public DbSet<DoctorsAdviceAudit> DoctorsAdviceAudits { get; set; }

        /// <summary>
        /// 快速开嘱的目录
        /// </summary>
        public DbSet<QuickStartCatalogue> QuickStartCatalogues { get; set; }

        /// <summary>
        /// 快速开嘱医嘱信息
        /// </summary>
        public DbSet<QuickStartAdvice> QuickStartAdvicess { get; set; }

        /// <summary>
        /// 快速开嘱药品信息
        /// </summary>
        public DbSet<QuickStartMedicine> QuickStartMedicines { get; set; }

        /// <summary>
        /// 分方管理类
        /// </summary>
        public DbSet<Prescription> Prescriptions { get; set; }

        /// <summary>
        /// 系列号管理器
        /// </summary>
        public DbSet<MySequence> MySequences { get; set; }

        /// <summary>
        /// 新冠rna检测申请
        /// </summary>
        public DbSet<NovelCoronavirusRna> NovelCoronavirusRnas { get; set; }

        /// <summary>
        /// 存储的库存信息
        /// </summary>
        public DbSet<DrugStockQuery> DrugStockQuerys { get; set; }

        /// <summary>
        /// 检查检验诊疗管理的主键类，为了医院自增Id添加的
        /// </summary>
        public DbSet<MedicalTechnologyMap> MedicalTechnologyMaps { get; set; }

        /// <summary>
        /// 回调HIS返回的数据
        /// </summary>
        public DbSet<MedDetailResult> MedDetailResults { get; set; }

        /// <summary>
        /// 电子病历过来的病历信息
        /// </summary>
        public DbSet<PatientCase> PatientCases { get; set; }

        /// <summary>
        /// 医嘱映射表，解决不同医院主键问题
        /// </summary>
        public DbSet<DoctorsAdviceMap> DoctorsAdviceMaps { get; set; }
        /// <summary>
        /// 用户配置表
        /// </summary>
        public DbSet<UserSetting> UserSettings { get; set; }
        /// <summary>
        /// 自备药
        /// </summary>
        public DbSet<OwnMedicine> OwnMedicines { get; set; }

        /// <summary>
        /// HIS对接接口审计日志
        /// </summary>
        public DbSet<AuditLog> AuditLogs { get; set; }

        /// <summary>
        /// 使用过的医嘱记录信息
        /// </summary>
        public DbSet<EmrUsedAdviceRecord> EmrUsedAdviceRecords { get; set; }

        /// <summary>
        /// 疫苗接种记录
        /// </summary>
        public DbSet<ImmunizationRecord> ImmunizationRecords { get; set; }

        public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureRecipe();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}