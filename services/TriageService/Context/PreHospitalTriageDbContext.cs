using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace SamJan.MicroService.PreHospital.TriageService
{
    [ConnectionStringName("DefaultConnection")]
    public class PreHospitalTriageDbContext : AbpDbContext<PreHospitalTriageDbContext>
    {
        //自定义EF 输出控制台日志
        private static readonly ILoggerFactory LoggerFactory =
            Microsoft.Extensions.Logging.LoggerFactory.Create(builder => { builder.AddNLog(); });

        private const string DefaultTableScheme = "Triage_";
        private const string DefaultDictScheme = "Dict_";
        private const string DefaultReportScheme = "Rpt_";
        private const string DefaultSmsScheme = "Sms_";

        private readonly IEncryptionProvider _encryption;

        private readonly IConfiguration _configuration;

        #region DbSet

        public DbSet<ConsequenceInfo> ConsequenceInfo { get; set; }

        public DbSet<ScoreInfo> ScoreInfo { get; set; }

        public DbSet<VitalSignInfo> VitalSignInfo { get; set; }

        public DbSet<GroupInjuryInfo> GroupInjuryInfo { get; set; }

        public DbSet<PatientInfo> PatientInfo { get; set; }

        public DbSet<JudgmentItem> JudgmentItem { get; set; }

        public DbSet<JudgmentType> JudgmentType { get; set; }

        public DbSet<JudgmentMaster> JudgmentMaster { get; set; }

        public DbSet<VitalSignExpression> VitalSignScoreExpression { get; set; }

        public DbSet<LevelTriageRelationDirection> LevelTriageRelationDirection { get; set; }

        public DbSet<TriageConfig> TriageConfig { get; set; }

        public DbSet<TriageConfigTypeDescription> TriageConfigTypeDescription { get; set; }


        public DbSet<RegisterInfo> RegisterInfo { get; set; }

        public DbSet<ScoreManage> ScoreManage { get; set; }

        public DbSet<TableSetting> TableSetting { get; set; }

        public DbSet<FastTrackRegisterInfo> FastTrackRegisterInfo { get; set; }

        public DbSet<AdmissionInfo> AdmissionInfo { get; set; }

        public DbSet<ReportSetting> ReportSetting { get; set; }

        public DbSet<ReportPermission> ReportPermission { get; set; }

        public DbSet<ReportSettingQueryOption> ReportSettingQueryOption { get; set; }

        public DbSet<TriageDevice> TriageDevice { get; set; }

        public DbSet<FastTrackSetting> FastTrackSetting { get; set; }

        public DbSet<Covid19Exam> Covid19Exam { get; set; }

        public DbSet<RegisterMode> RegisterMode { get; set; }

        public DbSet<ReportHotMorningAndNight> ReportHotMorningAndNight { get; set; }

        public DbSet<ReportDeathCount> ReportDeathCount { get; set; }

        public DbSet<ReportRescueAndView> ReportRescueAndView { get; set; }
        public DbSet<ReportFeverCount> ReportFeverCount { get; set; }
        public DbSet<ReportTriageCount> ReportTriageCount { get; set; }

        /// <summary>
        /// 病人信息变更记录表
        /// </summary>
        public DbSet<PatientInfoChangeRecord> patientInfoChangeRecords { get; set; }

        /// <summary>
        /// 评分字典
        /// </summary>
        public DbSet<ScoreDict> ScoreDicts { get; set; }
        /// <summary>
        /// 告知单患者
        /// </summary>
        public DbSet<InformPatInfo> InformPatInfo { get; set; }

        #region SMS

        public DbSet<TagSettings> TagSettings { get; set; }

        public DbSet<DutyTelephone> DutyTelephoneSettings { get; set; }

        public DbSet<TextMessageRecord> TextMessageRecords { get; set; }

        public DbSet<TextMessageTemplate> TextMessageTemplates { get; set; }

        #endregion

        #endregion

        public PreHospitalTriageDbContext(DbContextOptions<PreHospitalTriageDbContext> options,
            IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            _encryption = new SM4EncryptionProvider(configuration, LoggerFactory.CreateLogger<SM4EncryptionProvider>());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //添加日志输出
            if (Convert.ToBoolean(_configuration["Settings:IsEnabledEFCoreSqlLog"]))
            {
                optionsBuilder.UseLoggerFactory(LoggerFactory);
            }
            //显示隐私数据
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseEncryption(_encryption);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            #region 自定义表

            modelBuilder.Entity<PatientInfo>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "PatientInfo");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("院前分诊患者信息表");
            });

            modelBuilder.Entity<ConsequenceInfo>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "ConsequenceInfo");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("院前分诊结果表");
            });

            modelBuilder.Entity<ScoreInfo>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "ScoreInfo");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("院前分诊评分表");
            });

            modelBuilder.Entity<VitalSignInfo>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "VitalSignInfo");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("院前分诊生命体征表");
            });

            modelBuilder.Entity<GroupInjuryInfo>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "GroupInjuryInfo");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.Property(p => p.Remark).HasComment("详细描述").HasMaxLength(500);
                builder.HasComment("院前分诊群伤表");
            });


            modelBuilder.Entity<JudgmentType>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "JudgmentType");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("院前分诊判定依据科室分类表");
            });

            modelBuilder.Entity<JudgmentMaster>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "JudgmentMaster");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("院前分诊判定依据主诉分类表");
            });

            modelBuilder.Entity<JudgmentItem>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "JudgmentItem");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("院前分诊判定依据项目表");
                builder.HasQueryFilter(x => !x.IsDeleted);
            });

            modelBuilder.Entity<VitalSignExpression>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "VitalSignExpression");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("院前分诊生命体征评级标准表");
            });

            modelBuilder.Entity<LevelTriageRelationDirection>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "LevelTriageRelationDirection");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("院前分诊级别关联取消字典表");
            });

            modelBuilder.Entity<RegisterInfo>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "RegisterInfo");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.Property(p => p.IsDeleted).HasComment("挂号状态 0：已挂号 1：已退号");
                builder.HasComment("患者挂号信息表");
            });
            modelBuilder.Entity<ScoreManage>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "ScoreManage");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("评分管理表");
            });

            modelBuilder.Entity<TableSetting>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "TableSetting");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.Property(p => p.IsDeleted).HasComment("启用状态 0：已启用 1：未启用");
                builder.HasComment("表格配置表");
            });

            modelBuilder.Entity<FastTrackRegisterInfo>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "FastTrackRegisterInfo");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("快速通道登记信息表");
            });

            modelBuilder.Entity<AdmissionInfo>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "AdmissionInfo");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("入院情况信息表");
            });

            modelBuilder.Entity<ReportSetting>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "ReportSetting");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("分诊报表设置表");
            });

            modelBuilder.Entity<ReportPermission>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "ReportPermission");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("分诊报表权限设置");
            });

            modelBuilder.Entity<ReportSettingQueryOption>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "ReportSettingQueryOption");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("分诊报表查询选项表");
            });

            modelBuilder.Entity<TriageConfig>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "TriageConfig");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("分诊字典");
            });

            modelBuilder.Entity<TriageConfigTypeDescription>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "TriageConfigTypeDescription");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("分诊字典类型");
            });

            modelBuilder.Entity<TriageDevice>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "TriageDevice");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("设备信息");
            });

            modelBuilder.Entity<FastTrackSetting>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "FastTrackSetting");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("快速通道");
            });

            modelBuilder.Entity<Covid19Exam>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "Covid19Exam");
                builder.Property(x => x.BeenAbroadStatus).HasDefaultValue(0);
                if (modelBuilder.IsUsingSqlServer())
                {
                    builder.Property(x => x.Temperature).HasColumnType("decimal(18, 4)");
                }

                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("新冠问卷");
            });

            modelBuilder.Entity<RegisterMode>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "RegisterMode");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("挂号模式");
            });

            modelBuilder.Entity<ReportHotMorningAndNight>(builder =>
            {
                builder.ToTable(DefaultReportScheme + "HotMorningAndNight");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("早八晚八发热统计");
            });

            modelBuilder.Entity<ReportDeathCount>(builder =>
            {
                builder.ToTable(DefaultReportScheme + "DeathCount");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("死亡人数统计");
            });

            modelBuilder.Entity<ReportRescueAndView>(builder =>
            {
                builder.ToTable(DefaultReportScheme + "RescueAndView");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("抢救区、留观区统计");
            });
            modelBuilder.Entity<ReportFeverCount>(builder =>
            {
                builder.ToTable(DefaultReportScheme + "FeverCount");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("发热人数统计");
            });
            modelBuilder.Entity<ReportTriageCount>(builder =>
            {
                builder.ToTable(DefaultReportScheme + "TriageCount");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("分诊人数统计");
            });

            modelBuilder.Entity<ScoreDict>(builder =>
            {
                builder.ToTable(DefaultDictScheme + "ScoreDict");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("评分字典");
            });

            modelBuilder.Entity<InformPatInfo>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "InformPatInfo");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("告知单患者");
            });

            modelBuilder.Entity<PatientInfoChangeRecord>(builder =>
            {
                builder.ToTable(DefaultTableScheme + "PatientInfoChangeRecord");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("病人信息变更记录表");
            });

            #region SMS

            modelBuilder.Entity<TagSettings>(builder =>
            {
                builder.ToTable(DefaultSmsScheme + "TagSettings");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("标签管理");
            });

            modelBuilder.Entity<DutyTelephone>(builder =>
            {
                builder.ToTable(DefaultSmsScheme + "DutyTelephone");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("值班电话");
            });

            modelBuilder.Entity<TextMessageRecord>(builder =>
            {
                builder.ToTable(DefaultSmsScheme + "TextMessageRecord");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("短信消息记录");
            });

            modelBuilder.Entity<TextMessageTemplate>(builder =>
            {
                builder.ToTable(DefaultSmsScheme + "TextMessageTemplate");
                builder.ConfigureByConvention();
                builder.ConfigureColumnComment();
                builder.HasComment("短信模板");
            });

            #endregion

            #endregion
        }
    }
}