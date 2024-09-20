using System;
using System.ComponentModel;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.Handover;

namespace YiJian.Handover.EntityFrameworkCore
{
    public static class HandoverDbContextModelCreatingExtensions
    {
        public static void ConfigureHandover(
            this ModelBuilder builder,
            Action<HandoverModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new HandoverModelBuilderConfigurationOptions(
                HandoverDbProperties.DbTablePrefix,
                HandoverDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);
            builder.Entity<ShiftHandoverSetting>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "_ShiftHandoverSetting", options.Schema);
                b.ConfigureByConvention();
                b.ConfigureColumnComment();
                b.HasComment("交接班配置表");
            });

            #region DoctorHandover

            // 
            builder.Entity<DoctorHandover>(d =>
            {
                d.ToTable(options.TablePrefix + "_DoctorHandovers", options.Schema);

                d.ConfigureByConvention();

                d.HasComment("医生交接班表");

                d.Property(x => x.HandoverDate).HasComment("交班日期");

                d.Property(x => x.HandoverTime).HasComment("交班时间");

                d.Property(x => x.HandoverDoctorCode).HasComment("交班医生编码")
                    .HasMaxLength(DoctorHandoverConsts.MaxHandoverDoctorCodeLength);

                d.Property(x => x.HandoverDoctorName).HasComment("交班医生名称")
                    .HasMaxLength(DoctorHandoverConsts.MaxHandoverDoctorNameLength);

                d.Property(x => x.ShiftSettingId).HasComment("班次id");

                d.Property(x => x.ShiftSettingName).HasComment("班次名称")
                    .HasMaxLength(DoctorHandoverConsts.MaxShiftSettingNameLength);

                d.Property(x => x.OtherMatters).HasComment("其他事项")
                    .HasMaxLength(DoctorHandoverConsts.MaxOtherMattersLength);
                d.HasIndex(x => x.HandoverDoctorCode);
                d.HasIndex(x => x.HandoverTime);
                d.HasIndex(x => x.HandoverDate);
            });

            #endregion

            #region DoctorPatientStatistic

            // 
            builder.Entity<DoctorPatientStatistic>(d =>
            {
                d.ToTable(options.TablePrefix + "_DoctorPatientStatistics", options.Schema);

                d.ConfigureByConvention();

                d.HasComment("医生患者统计");

                d.Property(x => x.Total).HasComment("接诊总人数");

                d.Property(x => x.ClassI).HasComment("I级  (病危人数)");

                d.Property(x => x.ClassII).HasComment("II级  (病重人数)");

                d.Property(x => x.ClassIII).HasComment("III级");

                d.Property(x => x.ClassIV).HasComment("IV级");

                d.Property(x => x.PreOperation).HasComment("预术人数");

                d.Property(x => x.ExistingDisease).HasComment("现有病人数");

                d.Property(x => x.OutDept).HasComment("出科人数");

                d.Property(x => x.Rescue).HasComment("抢救人数");

                d.Property(x => x.Visit).HasComment("出诊人数");

                d.Property(x => x.Death).HasComment("死亡人数");

                d.Property(x => x.CPR).HasComment("心肺复苏人数");

                d.Property(x => x.Admission).HasComment("收住院人数");

                d.HasIndex(x => x.DoctorHandoverId);
            });

            #endregion

            #region DoctorPatients

            // 
            builder.Entity<DoctorPatients>(d =>
            {
                d.ToTable(options.TablePrefix + "_DoctorPatients", options.Schema);

                d.ConfigureByConvention();

                d.HasComment("医生交班患者");


                d.Property(x => x.DoctorHandoverId).HasComment("医生交班id");

                d.Property(x => x.PI_ID).HasComment("triage分诊患者id");

                d.Property(x => x.PatientId).HasComment("患者id").HasMaxLength(DoctorPatientsConsts.MaxPatientIdLength);

                d.Property(x => x.VisitNo).HasComment("就诊号");

                d.Property(x => x.PatientName).HasComment("患者姓名")
                    .HasMaxLength(DoctorPatientsConsts.MaxPatientNameLength);

                d.Property(x => x.Sex).HasComment("性别").HasMaxLength(DoctorPatientsConsts.MaxSexLength);
                d.Property(x => x.SexName).HasComment("性别名称").HasMaxLength(DoctorPatientsConsts.MaxSexLength);

                d.Property(x => x.Age).HasComment("年龄").HasMaxLength(DoctorPatientsConsts.MaxAgeLength);

                d.Property(x => x.TriageLevelName).HasComment("分诊级别")
                    .HasMaxLength(DoctorPatientsConsts.MaxTriageLevelLength);

                d.Property(x => x.DiagnoseName).HasComment("诊断").HasMaxLength(DoctorPatientsConsts.MaxDiagnoseLength);

                d.Property(x => x.Bed).HasComment("床号").HasMaxLength(DoctorPatientsConsts.MaxBedLength);

                d.Property(x => x.Content).HasComment("交班内容");

                d.Property(x => x.Test).HasComment("检验").HasMaxLength(DoctorPatientsConsts.MaxTestLength);

                d.Property(x => x.Inspect).HasComment("检查").HasMaxLength(DoctorPatientsConsts.MaxInspectLength);

                d.Property(x => x.Emr).HasComment("电子病历").HasMaxLength(DoctorPatientsConsts.MaxEmrLength);

                d.Property(x => x.InOutVolume).HasComment("出入量")
                    .HasMaxLength(DoctorPatientsConsts.MaxInOutVolumeLength);

                d.Property(x => x.VitalSigns).HasComment("生命体征").HasMaxLength(DoctorPatientsConsts.MaxVitalSignsLength);

                d.Property(x => x.Medicine).HasComment("药物").HasMaxLength(DoctorPatientsConsts.MaxMedicineLength);

                d.HasIndex(x => x.DoctorHandoverId);
            });

            #endregion
            #region NurseHandover 护士交班
            // 护士交班
            builder.Entity<NurseHandover>(n =>
            {
                n.ToTable(options.TablePrefix + "_NurseHandovers", options.Schema);

                n.ConfigureByConvention();

                n.HasComment("护士交班");

                #region NurseHandover Properties

                n.Property(x => x.PI_ID).HasComment("triage分诊患者id");

                n.Property(x => x.PatientId).HasComment("患者id").HasMaxLength(NurseHandoverConsts.MaxPatientIdLength);

                n.Property(x => x.VisitNo).HasComment("就诊号");

                n.Property(x => x.PatientName).HasComment("患者姓名").HasMaxLength(NurseHandoverConsts.MaxPatientNameLength);

                n.Property(x => x.Sex).HasComment("性别编码").HasMaxLength(NurseHandoverConsts.MaxSexLength);

                n.Property(x => x.SexName).HasComment("性别名称").HasMaxLength(NurseHandoverConsts.MaxSexNameLength);

                n.Property(x => x.Age).HasComment("年龄").HasMaxLength(NurseHandoverConsts.MaxAgeLength);

                n.Property(x => x.TriageLevel).HasComment("分诊级别").HasMaxLength(NurseHandoverConsts.MaxTriageLevelLength);

                n.Property(x => x.TriageLevelName).HasComment("分诊级别名称").HasMaxLength(NurseHandoverConsts.MaxTriageLevelNameLength);

                n.Property(x => x.AreaCode).HasComment("区域编码").HasMaxLength(NurseHandoverConsts.MaxAreaCodeLength);

                n.Property(x => x.AreaName).HasComment("区域名称").HasMaxLength(NurseHandoverConsts.MaxAreaNameLength);

                n.Property(x => x.InDeptTime).HasComment("入科时间");

                n.Property(x => x.DiagnoseName).HasComment("诊断").HasMaxLength(NurseHandoverConsts.MaxDiagnoseNameLength);

                n.Property(x => x.Bed).HasComment("床号").HasMaxLength(NurseHandoverConsts.MaxBedLength);

                n.Property(x => x.IsNoThree).HasComment("是否三无");

                n.Property(x => x.CriticallyIll).HasComment("是否病危");

                n.Property(x => x.Content).HasComment("交班内容").HasMaxLength(NurseHandoverConsts.MaxContentLength);

                n.Property(x => x.Test).HasComment("检验").HasMaxLength(NurseHandoverConsts.MaxTestLength);

                n.Property(x => x.Inspect).HasComment("检查").HasMaxLength(NurseHandoverConsts.MaxInspectLength);

                n.Property(x => x.Emr).HasComment("电子病历").HasMaxLength(NurseHandoverConsts.MaxEmrLength);

                n.Property(x => x.InOutVolume).HasComment("出入量").HasMaxLength(NurseHandoverConsts.MaxInOutVolumeLength);

                n.Property(x => x.VitalSigns).HasComment("生命体征").HasMaxLength(NurseHandoverConsts.MaxVitalSignsLength);

                n.Property(x => x.Medicine).HasComment("药物").HasMaxLength(NurseHandoverConsts.MaxMedicineLength);

                n.Property(x => x.LatestStatus).HasComment("最新现状").HasMaxLength(NurseHandoverConsts.MaxLatestStatusLength);

                n.Property(x => x.Background).HasComment("背景").HasMaxLength(NurseHandoverConsts.MaxBackgroundLength);

                n.Property(x => x.Assessment).HasComment("评估").HasMaxLength(NurseHandoverConsts.MaxAssessmentLength);

                n.Property(x => x.Proposal).HasComment("建议").HasMaxLength(NurseHandoverConsts.MaxProposalLength);

                n.Property(x => x.Devices).HasComment("设备").HasMaxLength(NurseHandoverConsts.MaxDevicesLength);

                n.Property(x => x.HandoverTime).HasComment("交班时间");

                n.Property(x => x.HandoverNurseCode).HasComment("交班护士编码").HasMaxLength(NurseHandoverConsts.MaxHandoverNurseCodeLength);

                n.Property(x => x.HandoverNurseName).HasComment("交班护士名称").HasMaxLength(NurseHandoverConsts.MaxHandoverNurseNameLength);

                n.Property(x => x.SuccessionNurseCode).HasComment("接班护士编码").HasMaxLength(NurseHandoverConsts.MaxSuccessionNurseCodeLength);

                n.Property(x => x.SuccessionNurseName).HasComment("接班护士名称").HasMaxLength(NurseHandoverConsts.MaxSuccessionNurseNameLength);

                n.Property(x => x.HandoverDate).HasComment("交班日期");

                n.Property(x => x.ShiftSettingId).HasComment("班次id");

                n.Property(x => x.ShiftSettingName).HasComment("班次名称").HasMaxLength(NurseHandoverConsts.MaxShiftSettingNameLength);

                n.Property(x => x.Status).HasComment("交班状态，0：未提交，1：提交交班");

                n.Property(x => x.CreationCode).HasComment("创建人编码").HasMaxLength(NurseHandoverConsts.MaxCreationCodeLength);

                n.Property(x => x.CreationName).HasComment("创建人名称").HasMaxLength(NurseHandoverConsts.MaxCreationNameLength);

                n.Property(x => x.TotalPatient).HasComment("查询的全部患者");
                #endregion

                n.HasIndex(x => x.PI_ID);
            });
            #endregion NurseHandover 护士交班





            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */
        }

        /// <summary>
        /// 字段生成添加注释
        /// </summary>
        /// <param name="b"></param>
        public static void ConfigureColumnComment(this EntityTypeBuilder b)
        {
            foreach (PropertyInfo property in b.Metadata.ClrType.GetProperties())
            {
                IsNeedCommentAttribute customAttribute1 = property.GetCustomAttribute<IsNeedCommentAttribute>();
                if (customAttribute1 == null || customAttribute1.IsNeed)
                {
                    DescriptionAttribute customAttribute2 = property.GetCustomAttribute<DescriptionAttribute>();
                    b.Property(property.Name).HasComment(customAttribute2?.Description);
                }
            }
        }
    }
}
