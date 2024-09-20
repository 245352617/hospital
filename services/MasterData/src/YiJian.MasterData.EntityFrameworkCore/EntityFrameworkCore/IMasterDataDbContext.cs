using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.AllItems;
using YiJian.MasterData.Departments;
using YiJian.MasterData.Domain;
using YiJian.MasterData.Exams;
using YiJian.MasterData.Labs;
using YiJian.MasterData.Labs.Container;
using YiJian.MasterData.Labs.Position;
using YiJian.MasterData.MasterData.Doctors;
using YiJian.MasterData.Medicines;
using YiJian.MasterData.Sequences;
using YiJian.MasterData.Treats;
using YiJian.MasterData.ViewSettings;
using YiJian.MasterData.VitalSign;

namespace YiJian.MasterData.EntityFrameworkCore;

[ConnectionStringName("Default")]
public interface IMasterDataDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    //药品表
    DbSet<Medicine> Medicines { get; }

    //检验目录
    DbSet<LabCatalog> LabCatalogs { get; }

    //检验项目
    DbSet<LabProject> LabProjects { get; }

    //检验明细项
    DbSet<LabTarget> LabTargets { get; }

    //字典类型编码
    DbSet<DictionariesTypes.DictionariesType> DictionariesTypes { get; }

    //检验标本
    DbSet<LabSpecimen> LabSpecimens { get; }

    //检查目录
    DbSet<ExamCatalog> ExamCatalogs { get; }


    //检查部位
    DbSet<ExamPart> ExamParts { get; }

    //检查申请项目
    DbSet<ExamProject> ExamProjects { get; }

    //检查明细项
    DbSet<ExamTarget> ExamTargets { get; }

    //序列
    DbSet<Sequence> Sequences { get; }

    //药品频次字典
    DbSet<MedicineFrequency> MedicineFrequencies { get; }

    //药品用法字典
    DbSet<MedicineUsage> MedicineUsages { get; }

    //容器编码
    DbSet<LabContainer> LabContainers { get; }

    //检验标本采集部位
    DbSet<LabSpecimenPosition> LabSpecimenPositions { get; }

    //诊疗项目字典
    DbSet<Treat> Treats { get; }

    //视图配置
    DbSet<ViewSetting> ViewSettings { get; }
    
    //诊疗检查检验药品项目合集
    DbSet<AllItem> AllItems { get; }
    
    //评分项
    DbSet<VitalSignExpression> VitalSignExpressions { get; }
    
    //检查申请注意事项
    DbSet<ExamNote> ExamNotes { get; }

    /// <summary>
    /// 外部消息接收 日志存储
    /// </summary>
    DbSet<ReceivedLog> ReceivedLogs { get; }

    /// <summary>
    /// 医生
    /// </summary>
    public DbSet<Doctor> Doctors { get; }

    /// <summary>
    /// 执行科室规则字典
    /// </summary>
    public DbSet<ExecuteDepRuleDic> ExecuteDepRuleDics { get; set; }

    //检验单信息
    public DbSet<LabReportInfo> LabReportInfo { get; }

    //检查Tree结构
    public DbSet<ExamTree> ExamTree { get; }

    //检查Tree结构
    public DbSet<LabTree> LabTree { get; }
}
