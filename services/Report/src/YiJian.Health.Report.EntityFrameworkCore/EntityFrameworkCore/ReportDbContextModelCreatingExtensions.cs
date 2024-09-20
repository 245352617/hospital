using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Health.Report.PrintCatalogs;
using YiJian.Health.Report.Hospitals.Entities;
using YiJian.Health.Report.NursingDocuments;
using YiJian.Health.Report.NursingDocuments.Entities;
using YiJian.Health.Report.NursingSettings.Entities;
using YiJian.Health.Report.PrintSettings;
using YiJian.Health.Report.ReportDatas;
using YiJian.Health.Report.Domain.PhraseCatalogues.Entities;
using YiJian.Health.Report.Statisticses.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace YiJian.Health.Report.EntityFrameworkCore
{
    public static class ReportDbContextModelCreatingExtensions
    {
        static string ViewPrefix = "View_";

        public static void ConfigureReport(
            this ModelBuilder builder,
            Action<ReportModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ReportModelBuilderConfigurationOptions(
                ReportDbProperties.DbTablePrefix,
                ReportDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            #region 护理单

            //护理单
            builder.Entity<NursingDocument>(b =>
            {
                b.ToTable(options.TablePrefix + "NursingDocument", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(x => x.PatientId).IsUnique(false);
                b.HasIndex(x => x.AdmissionTime).IsUnique(false);
            });
            //护理记录
            builder.Entity<NursingRecord>(b =>
            {
                b.ToTable(options.TablePrefix + "NursingRecord", options.Schema);
                b.ConfigureByConvention();
                b.HasMany(x => x.Characteristic);
            });
            //动态字段名字描述
            builder.Entity<DynamicField>(b =>
            {
                b.ToTable(options.TablePrefix + "DynamicField", options.Schema);
                b.HasIndex(x => x.NursingDocumentId).IsUnique(false);
                b.ConfigureByConvention();
            });
            //动态字段数据
            builder.Entity<DynamicData>(b =>
            {
                b.ToTable(options.TablePrefix + "DynamicData", options.Schema);
                b.HasIndex(x => x.NursingDocumentId).IsUnique(false);
                b.HasIndex(x => x.Header).IsUnique(false);
                b.HasIndex(x => x.SheetIndex).IsUnique(false);
                b.ConfigureByConvention();
            });
            //危重情况记录
            builder.Entity<CriticalIllness>(b =>
            {
                b.ToTable(options.TablePrefix + "CriticalIllness", options.Schema);
                b.ConfigureByConvention();
            });
            //指尖血糖 mmol/L
            builder.Entity<Mmol>(b =>
            {
                b.ToTable(options.TablePrefix + "Mmol", options.Schema);
                b.HasIndex(x => x.NursingRecordId).IsUnique(false);
                b.ConfigureByConvention();
            });
            //瞳孔评估
            builder.Entity<Pupil>(b =>
            {
                b.ToTable(options.TablePrefix + "Pupil", options.Schema);
                b.ConfigureByConvention();
            });
            //入量出量
            builder.Entity<Intake>(b =>
            {
                b.ToTable(options.TablePrefix + "Intake", options.Schema);
                b.ConfigureByConvention();
            });
            //病人特征记录
            builder.Entity<Characteristic>(b =>
            {
                b.ToTable(options.TablePrefix + "Characteristic", options.Schema);
                //b.Property(x=>x.JsonData).HasColumnType("text");
                b.ConfigureByConvention();
            });
            //查房签名
            builder.Entity<WardRound>(b =>
            {
                b.ToTable(options.TablePrefix + "WardRound", options.Schema);
                //b.Property(x=>x.JsonData).HasColumnType("text");
                b.ConfigureByConvention();
            });

            //出入量统计
            builder.Entity<IntakeStatistics>(b =>
            {
                b.ToTable(options.TablePrefix + "IntakeStatistics", options.Schema);
                //b.Property(x=>x.JsonData).HasColumnType("text");
                b.ConfigureByConvention();
            });

            //特殊护理记录
            builder.Entity<SpecialNursingRecord>(b =>
            {
                b.ToTable(options.TablePrefix + "SpecialNursingRecord", options.Schema);
                b.ConfigureByConvention();
            });
            #endregion

            #region 护理单内容配置

            builder.Entity<NursingSetting>(b =>
            {
                b.ToTable(options.TablePrefix + "NursingSetting", options.Schema);
                b.HasMany(x => x.Headers);
                b.ConfigureByConvention();
            });
            builder.Entity<NursingSettingHeader>(b =>
            {
                b.ToTable(options.TablePrefix + "NursingSettingHeader", options.Schema);
                b.HasMany(x => x.Items);
                b.ConfigureByConvention();
            });
            builder.Entity<NursingSettingItem>(b =>
            {
                b.ToTable(options.TablePrefix + "NursingSettingItem", options.Schema);
                b.HasMany(x => x.Items);
                b.Property(x => x.Lv).HasDefaultValue(0);
                b.Property(x => x.IsCarryInputBox).HasDefaultValue(false);
                b.Property(x => x.HasNext).HasDefaultValue(false);
                b.ConfigureByConvention();
            });

            builder.Entity<PhraseCatalogue>(b =>
            {
                b.ToTable(options.TablePrefix + "PhraseCatalogue", options.Schema);
                b.ConfigureByConvention();
            });

            builder.Entity<Phrase>(b =>
            {
                b.ToTable(options.TablePrefix + "Phrase", options.Schema);
                b.HasIndex(x => x.CatalogueId).IsUnique(false);
                b.ConfigureByConvention();
            });

            builder.Entity<IntakeSetting>(b =>
            {
                b.ToTable(options.TablePrefix + "IntakeSetting", options.Schema);
                b.ConfigureByConvention();
            });
            #endregion



            #region 医院信息

            builder.Entity<HospitalInfo>(b =>
            {
                b.ToTable(options.TablePrefix + "HospitalInfo", options.Schema);
                b.ConfigureByConvention();
            });

            #endregion


            #region 打印

            //打印目录
            builder.Entity<PrintCatalog>(b =>
            {
                b.ToTable(options.TablePrefix + "PrintCatalog", options.Schema);
                b.ConfigureByConvention();
            });
            //打印设置
            builder.Entity<PrintSetting>(b =>
            {
                b.ToTable(options.TablePrefix + "PrintSetting", options.Schema);
                b.ConfigureByConvention();
            });
            //打印设置
            builder.Entity<PageSize>(b =>
            {
                b.ToTable(options.TablePrefix + "PageSize", options.Schema);
                b.ConfigureByConvention();
            });
            //打印数据
            builder.Entity<ReportData>(b =>
            {
                b.ToTable(options.TablePrefix + "ReportData", options.Schema);
                b.ConfigureByConvention();
            });
            #endregion

            #region 质控和统计分析

            builder.Entity<StatisticsMonthDoctorAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsMonthDoctorAndPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });
            builder.Entity<StatisticsQuarterDoctorAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsQuarterDoctorAndPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });
            builder.Entity<StatisticsYearDoctorAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsYearDoctorAndPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });

            builder.Entity<StatisticsMonthNurseAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsMonthNurseAndPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });
            builder.Entity<StatisticsQuarterNurseAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsQuarterNurseAndPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });
            builder.Entity<StatisticsYearNurseAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsYearNurseAndPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });

            builder.Entity<StatisticsMonthLevelAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsMonthLevelAndPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });
            builder.Entity<StatisticsQuarterLevelAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsQuarterLevelAndPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });
            builder.Entity<StatisticsYearLevelAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsYearLevelAndPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });

            builder.Entity<StatisticsMonthEmergencyroomAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsMonthEmergencyroomAndPatient", options.Schema);
                b.ConfigureByConvention();
            });
            builder.Entity<StatisticsQuarterEmergencyroomAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsQuarterEmergencyroomAndPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });
            builder.Entity<StatisticsYearEmergencyroomAndPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsYearEmergencyroomAndPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });

            builder.Entity<StatisticsMonthEmergencyroomAndDeathPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsMonthEmergencyroomAndDeathPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });
            builder.Entity<StatisticsQuarterEmergencyroomAndDeathPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsQuarterEmergencyroomAndDeathPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });
            builder.Entity<StatisticsYearEmergencyroomAndDeathPatient>(b =>
            {
                b.ToTable(options.TablePrefix + "StatisticsYearEmergencyroomAndDeathPatient", options.Schema);
                b.HasKey(x => x.Id);
                b.ConfigureByConvention();
            });
            //接诊病人详细视图
            builder.Entity<ViewAdmissionRecord>().ToView("v_AdmissionRecord").HasNoKey();
            //入院患者流转记录视图
            builder.Entity<ViewAdmissionTransfeRecord>().ToView("v_AdmissionTransfeRecord").HasNoKey();

            #endregion

        }
    }
}