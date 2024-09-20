using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.AllItems;
using YiJian.MasterData.Departments;
using YiJian.MasterData.DictionariesMultitypes;
using YiJian.MasterData.Domain;
using YiJian.MasterData.Exams;
using YiJian.MasterData.Labs;
using YiJian.MasterData.Labs.Container;
using YiJian.MasterData.Labs.Position;
using YiJian.MasterData.MasterData.Doctors;
using YiJian.MasterData.Medicines;
using YiJian.MasterData.Pharmacies.Entities;
using YiJian.MasterData.Regions;
using YiJian.MasterData.Separations.Entities;
using YiJian.MasterData.Sequences;
using YiJian.MasterData.Treats;
using YiJian.MasterData.ViewSettings;
using YiJian.MasterData.VitalSign;

namespace YiJian.MasterData.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class MasterDataDbContext : AbpDbContext<MasterDataDbContext>, IMasterDataDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    //公共字典
    public DbSet<Dictionaries> Dictionaries { get; set; }

    //手术字典
    public DbSet<Operation> Operation { get; set; }

    //药品表
    public DbSet<Medicine> Medicines { get; set; }


    //检验目录
    public DbSet<LabCatalog> LabCatalogs { get; set; }

    //检验项目
    public DbSet<LabProject> LabProjects { get; set; }

    //检验明细项
    public DbSet<LabTarget> LabTargets { get; set; }

    //字典类型编码
    public DbSet<DictionariesTypes.DictionariesType> DictionariesTypes { get; set; }

    //检验标本
    public DbSet<LabSpecimen> LabSpecimens { get; set; }

    //检查目录
    public DbSet<ExamCatalog> ExamCatalogs { get; set; }

    //检查部位
    public DbSet<ExamPart> ExamParts { get; set; }

    //检查申请项目
    public DbSet<ExamProject> ExamProjects { get; set; }

    /// <summary>
    /// 检查附加项
    /// </summary>
    public DbSet<ExamAttachItem> ExamAttachItems { get; set; }

    //检查明细项
    public DbSet<ExamTarget> ExamTargets { get; set; }

    //序列
    public DbSet<Sequence> Sequences { get; set; }

    //药品频次字典
    public DbSet<MedicineFrequency> MedicineFrequencies { get; set; }

    //药品用法字典
    public DbSet<MedicineUsage> MedicineUsages { get; set; }

    //容器编码
    public DbSet<LabContainer> LabContainers { get; set; }


    //检验标本采集部位
    public DbSet<LabSpecimenPosition> LabSpecimenPositions { get; set; }


    //诊疗项目字典
    public DbSet<Treat> Treats { get; set; }


    //视图配置
    public DbSet<ViewSetting> ViewSettings { get; set; }


    //诊疗检查检验药品项目合集
    public DbSet<AllItem> AllItems { get; set; }


    //评分项
    public DbSet<VitalSignExpression> VitalSignExpressions { get; set; }


    //检查申请注意事项
    public DbSet<ExamNote> ExamNotes { get; set; }

    /// <summary>
    /// 分方配置实体
    /// </summary>
    public DbSet<Separation> Separations { get; set; }

    /// <summary>
    /// 用药途经
    /// </summary>
    public DbSet<Usage> Usages { get; set; }

    /// <summary>
    /// 药房配置
    /// </summary>
    public DbSet<Pharmacy> Pharmacies { get; set; }

    /// <summary>
    /// 嘱托配置
    /// </summary>
    public DbSet<Entrust> Entrusts { get; set; }

    /// <summary>
    /// 诊疗分组
    /// </summary>
    public DbSet<TreatGroup> TreatGroups { get; set; }

    /// <summary>
    /// 外部消息接收 日志存储
    /// </summary>
    public DbSet<ReceivedLog> ReceivedLogs { get; set; }

    /// <summary>
    /// 地区
    /// </summary>
    public DbSet<Region> Regions { get; set; }

    /// <summary>
    /// 地区带全称
    /// </summary>
    public DbSet<Area> Areas { get; set; }

    /// <summary>
    /// 医生
    /// </summary>
    public DbSet<Doctor> Doctors { get; set; }

    /// <summary>
    /// 执行科室规则字典
    /// </summary>
    public DbSet<ExecuteDepRuleDic> ExecuteDepRuleDics { get; set; }

    /// <summary>
    /// 字典多类型
    /// </summary>
    public DbSet<DictionariesMultitype> DictionariesMultitypes { get; set; }

    //检验单信息
    public DbSet<LabReportInfo> LabReportInfo { get; set; }

    //检查Tree结构
    public DbSet<ExamTree> ExamTree { get; }

    //检查Tree结构
    public DbSet<LabTree> LabTree { get; }

    /// <summary>
    /// 护士医嘱类别
    /// </summary>
    public DbSet<NursingRecipeType> NursingRecipeTypes { get; set; }

    public MasterDataDbContext(DbContextOptions<MasterDataDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureMasterData();
    }
}