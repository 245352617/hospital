using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.PrintCatalogs;
using YiJian.Health.Report.Hospitals.Entities;
using YiJian.Health.Report.NursingDocuments;
using YiJian.Health.Report.NursingDocuments.Entities;
using YiJian.Health.Report.NursingSettings.Entities;
using YiJian.Health.Report.PrintSettings;
using YiJian.Health.Report.ReportDatas;
using YiJian.Health.Report.Domain.PhraseCatalogues.Entities;
using System.Linq;
using YiJian.Health.Report.Statisticses.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace YiJian.Health.Report.EntityFrameworkCore
{
    [ConnectionStringName(ReportDbProperties.ConnectionStringName)]
    public class ReportDbContext : AbpDbContext<ReportDbContext>, IReportDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        #region 护理单

        /// <summary>
        /// 护理单
        /// </summary>
        public DbSet<NursingDocument> NursingDocuments { get; set; }

        /// <summary>
        /// 护理记录
        /// </summary>
        public DbSet<NursingRecord> NursingRecords { get; set; }

        /// <summary>
        /// 动态字段名字描述
        /// </summary>
        public DbSet<DynamicField> DynamicFields { get; set; }

        /// <summary>
        /// 动态字段数据
        /// </summary>
        public DbSet<DynamicData> DynamicDatas { get; set; }

        /// <summary>
        /// 危重情况记录
        /// </summary>
        public DbSet<CriticalIllness> CriticalIllnesses { get; set; }

        /// <summary>
        /// 指尖血糖 mmol/L
        /// </summary>
        public DbSet<Mmol> Mmols { get; set; }

        /// <summary>
        /// 瞳孔评估
        /// </summary>
        public DbSet<Pupil> Pupils { get; set; }

        /// <summary>
        /// 入量出量
        /// </summary>
        public DbSet<Intake> Intakes { get; set; }

        /// <summary>
        /// 病人特征记录
        /// </summary>
        public DbSet<Characteristic> Characteristics { get; set; }

        /// <summary>
        /// 查房签名
        /// </summary>
        public DbSet<WardRound> WardRounds { get; set; }

        /// <summary>
        /// 出入量统计
        /// </summary>
        public DbSet<IntakeStatistics> IntakeStatistics { get; set; }


        #endregion

        #region 护理单内容配置

        /// <summary>
        /// 护理单配置
        /// </summary>
        public DbSet<NursingSetting> NursingSettings { get; set; }

        /// <summary>
        /// 护理单表头配置
        /// </summary>
        public DbSet<NursingSettingHeader> NursingSettingHeaders { get; set; }

        /// <summary>
        /// 护理单配置项
        /// </summary>
        public DbSet<NursingSettingItem> NursingSettingItems { get; set; }

        #region 常用语

        public DbSet<PhraseCatalogue> PhraseCatalogues { get; set; }
        public DbSet<Phrase> Phrases { get; set; }

        #endregion

        #region 出入量配置

        /// <summary>
        /// 出入量配置
        /// </summary>
        public DbSet<IntakeSetting> IntakeSettings { get; set; }

        #endregion

        #endregion

        /// <summary>
        /// 医院信息
        /// </summary>
        public DbSet<HospitalInfo> HospitalInfos { get; set; }

        /// <summary>
        /// 纸张大小
        /// </summary>
        public DbSet<PageSize> PageSize { get; set; }

        #region FastReport

        /// <summary>
        /// 打印目录
        /// </summary>
        public DbSet<PrintCatalog> PrintCatalog { get; set; }

        /// <summary>
        /// 打印设置
        /// </summary>
        public DbSet<PrintSetting> PrintSetting { get; set; }

        /// <summary>
        /// 打印数据
        /// </summary>
        public DbSet<ReportData> ReportDatas { get; set; }

        #endregion

        #region 质控和报表统计视图

        //采用每个月度，季度，年度统计的方式统计，减少资源集中调用

        //急诊科医患月度视图
        public DbSet<StatisticsMonthDoctorAndPatient> StatisticsMonthDoctorAndPatients { get; set; }
        //急诊科医患季度视图
        public DbSet<StatisticsQuarterDoctorAndPatient> StatisticsQuarterDoctorAndPatients { get; set; }
        //急诊科医患年度视图
        public DbSet<StatisticsYearDoctorAndPatient> StatisticsYearDoctorAndPatients { get; set; }

        //急诊科护患月度视图 
        public DbSet<StatisticsMonthNurseAndPatient> StatisticsMonthNurseAndPatients { get; set; }
        //急诊科护患季度视图
        public DbSet<StatisticsQuarterNurseAndPatient> StatisticsQuarterNurseAndPatients { get; set; }
        //急诊科护患年度视图
        public DbSet<StatisticsYearNurseAndPatient> StatisticsYearNurseAndPatients { get; set; }

        //急诊科各级患者比例月度视图
        public DbSet<StatisticsMonthLevelAndPatient> StatisticsMonthLevelAndPatients { get; set; }
        //急诊科各级患者比例季度视图
        public DbSet<StatisticsQuarterLevelAndPatient> StatisticsQuarterLevelAndPatients { get; set; }
        //急诊科各级患者比例年度视图
        public DbSet<StatisticsYearLevelAndPatient> StatisticsYearLevelAndPatients { get; set; }

        //抢救室滞留时间中位数月度视图
        public DbSet<StatisticsMonthEmergencyroomAndPatient> StatisticsMonthEmergencyroomAndPatients { get; set; }
        //抢救室滞留时间中位数季度视图
        public DbSet<StatisticsQuarterEmergencyroomAndPatient> StatisticsQuarterEmergencyroomAndPatients { get; set; }
        //抢救室滞留时间中位数年度视图
        public DbSet<StatisticsYearEmergencyroomAndPatient> StatisticsYearEmergencyroomAndPatients { get; set; }

        //急诊抢救室患者死亡率月度视图
        public DbSet<StatisticsMonthEmergencyroomAndDeathPatient> StatisticsMonthEmergencyroomAndDeathPatients { get; set; }
        //急诊抢救室患者死亡率季度视图
        public DbSet<StatisticsQuarterEmergencyroomAndDeathPatient> StatisticsQuarterEmergencyroomAndDeathPatients { get; set; }
        //急诊抢救室患者死亡率年度视图
        public DbSet<StatisticsYearEmergencyroomAndDeathPatient> StatisticsYearEmergencyroomAndDeathPatients { get; set; }
        // 医生就诊汇总信息
        public DbSet<UspDoctorPatientRatio> UspDoctorPatientRatios { get; set; }
        //接诊病人详细视图
        public DbSet<ViewAdmissionRecord> ViewAdmissionRecords { get; set; }
        //入院患者流转记录视图
        public DbSet<ViewAdmissionTransfeRecord> ViewAdmissionTransfeRecords { get; set; }
        // 护士执行汇总信息
        public DbSet<UspNursePatientRatio> UspNursePatientRatios { get; set; }

        #endregion

        /// <summary>
        /// 患者详情
        /// </summary>
        public DbSet<StatisticsPatient> StatisticsPatient { get; set; }

        public DbSet<TotalCount> TotalCount { get; set; }

        public DbSet<AdmissionRecord> AdmissionRecord { get; set; }

        public ReportDbContext(DbContextOptions<ReportDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureReport();
        }
    }
}
