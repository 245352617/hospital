using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using YiJian.AuditLogs.Entities;
using YiJian.Cases.Entities;
using YiJian.DoctorsAdvices.Entities;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Emrs.Entities;
using YiJian.OwnMedicines.Entities;
using YiJian.Recipe.Packages;
using YiJian.Recipes.Basic;
using YiJian.Recipes.DoctorsAdvices.Entities;
using YiJian.Recipes.GroupConsultation;
using YiJian.Recipes.InviteDoctor;
using YiJian.Recipes.Preferences.Entities;
using YiJian.Sequences.Entities;

namespace YiJian.Recipe.EntityFrameworkCore
{
    /// <summary>
    /// RecipeDbContext扩展
    /// </summary>
    public static class RecipeDbContextModelCreatingExtensions
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="optionsAction"></param>
        public static void ConfigureRecipe(
            this ModelBuilder builder,
            Action<RecipeModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new RecipeModelBuilderConfigurationOptions(
                RecipeDbProperties.DbTablePrefix,
                RecipeDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            // 医嘱项目
            builder.Entity<RecipeProject>(builder =>
            {
                builder.ToTable($"{options.TablePrefix}Project", options.Schema);
                builder.ConfigureByConvention();

                builder.Property(x => x.Price).HasPrecision(18, 4);
                builder.Property(x => x.OtherPrice).HasPrecision(18, 4);

                builder.HasOne(x => x.MedicineProp).WithOne(x => x.RecipeProject)
                    .HasForeignKey<RecipeMedicineProp>(x => x.Id)
                    .HasConstraintName("FK_Project_Medicine");
                builder.HasOne(x => x.ExamProp).WithOne(x => x.RecipeProject).HasForeignKey<RecipeExamProp>(x => x.Id)
                    .HasConstraintName("FK_Project_ExamProp");
                builder.HasOne(x => x.LabProp).WithOne(x => x.RecipeProject).HasForeignKey<RecipeLabProp>(x => x.Id)
                    .HasConstraintName("FK_Project_LabProp");
                builder.HasOne(x => x.TreatProp).WithOne(x => x.RecipeProject).HasForeignKey<RecipeTreatProp>(x => x.Id)
                    .HasConstraintName("FK_Project_TreatProp");
                builder.HasIndex(x => x.Code).IsUnique(false).HasDatabaseName("IDX_Project_Code");
            });
            // 药品属性
            builder.Entity<RecipeMedicineProp>(builder =>
            {
                builder.Property(x => x.Price).HasPrecision(18, 4);
                builder.Property(x => x.BigPackPrice).HasPrecision(18, 4);
                builder.Property(x => x.SmallPackPrice).HasPrecision(18, 4);
                builder.Property(x => x.DosageQty).HasPrecision(18, 4);

                builder.ToTable($"{options.TablePrefix}ProjectMedicineProp", options.Schema);
                builder.ConfigureByConvention();
            });
            // 处置属性
            builder.Entity<RecipeTreatProp>(builder =>
            {
                builder.ToTable($"{options.TablePrefix}ProjectTreatrop", options.Schema);
                builder.ConfigureByConvention();
            });
            // 检查属性
            builder.Entity<RecipeExamProp>(builder =>
            {
                builder.ToTable($"{options.TablePrefix}ProjectExamProp", options.Schema);
                builder.ConfigureByConvention();
            });
            // 检验属性
            builder.Entity<RecipeLabProp>(builder =>
            {
                builder.ToTable($"{options.TablePrefix}ProjectLabProp", options.Schema);
                builder.ConfigureByConvention();
            });
            // 医嘱套餐分组
            builder.Entity<PackageGroup>(builder =>
            {
                builder.ToTable($"{options.TablePrefix}PackageGroup", options.Schema);
                builder.ConfigureByConvention();
            });
            // 医嘱套餐
            builder.Entity<Package>(builder =>
            {
                builder.ToTable($"{options.TablePrefix}Package", options.Schema);
                builder.ConfigureByConvention();

                builder.HasMany(x => x.Projects).WithOne(x => x.Package).IsRequired(false)
                    .HasForeignKey(x => x.PackageId)
                    .HasConstraintName("FK_Package_PackageProject")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            // 医嘱套餐-医嘱
            builder.Entity<PackageProject>(builder =>
            {
                builder.ToTable($"{options.TablePrefix}PackageProject", options.Schema);
                builder.ConfigureByConvention();

                builder.Property(x => x.DosageQty).HasPrecision(18, 4);
                builder.Property(x => x.Qty).HasPrecision(18, 4);

                // 定义主键
                builder.HasKey(x => new { x.Id });
                builder.HasIndex(x => x.PackageId).HasDatabaseName("IX_PackageProject_PackageId");
            });


            #region OperationApply 手术申请

            // 分诊患者id
            builder.Entity<OperationApply>(o =>
            {
                o.ToTable("Oper_OperationApply");
                o.ConfigureByConvention();
                o.HasIndex(x => x.PI_Id).IsUnique(false);
            });

            #endregion OperationApply 手术申请

            #region 医嘱(新)

            //医嘱主表(聚合根)
            builder.Entity<DoctorsAdvice>(b =>
            {
                b.ToTable(options.TablePrefix + "DoctorsAdvice", options.Schema);
                b.HasIndex(x => x.PIID).IsUnique(false);
                b.HasIndex(x => x.PatientId).IsUnique(false);
                b.HasIndex(x => x.Code).IsUnique(false);
                b.HasIndex(x => x.Name).IsUnique(false);
                b.Property(x => x.PayStatus).HasDefaultValue(EPayStatus.NoPayment); //默认未支付
                b.Property(x => x.Price).HasPrecision(18, 4);
                b.Property(x => x.Amount).HasPrecision(18, 4);
                b.Property(x => x.RecieveQty).HasDefaultValue(1);
                //b.Property(x=>x.DetailId).ValueGeneratedOnAdd();
                b.ConfigureByConvention();
            });
            //药方
            builder.Entity<Prescribe>(b =>
            {
                b.ToTable(options.TablePrefix + "Prescribe", options.Schema);
                b.Property(b => b.BigPackPrice).HasPrecision(18, 4);
                b.Property(b => b.ChildrenPrice).HasPrecision(18, 4);
                b.Property(b => b.FixPrice).HasPrecision(18, 4);
                b.Property(b => b.MaterialPrice).HasPrecision(18, 4);
                b.Property(b => b.RetPrice).HasPrecision(18, 4);
                b.Property(b => b.SmallPackPrice).HasPrecision(18, 4);
                b.Property(x => x.DosageQty).HasPrecision(18, 4);
                b.Property(x => x.DefaultDosageQty).HasPrecision(18, 4);
                b.HasIndex(b => b.MedicineId).IsUnique(false);
                b.ConfigureByConvention();
            });
            //自定义规则药品一次剂量名单 (自己维护)
            builder.Entity<PrescribeCustomRule>(b =>
            {
                b.ToTable(options.TablePrefix + "PrescribeCustomRule", options.Schema);
                b.ConfigureByConvention();
            });
            builder.Entity<Toxic>(b =>
            {
                b.ToTable(options.TablePrefix + "Toxic", options.Schema);
                b.HasIndex(b => b.MedicineId).IsUnique(false);
                b.ConfigureByConvention();
            });
            //检验项
            builder.Entity<Lis>(b =>
            {
                b.ToTable(options.TablePrefix + "Lis", options.Schema);

                b.ConfigureByConvention();
            });
            //检查项
            builder.Entity<Pacs>(b =>
            {
                b.ToTable(options.TablePrefix + "Pacs", options.Schema);
                b.ConfigureByConvention();
            });
            //检验项子列表
            builder.Entity<LisItem>(b =>
            {
                b.ToTable(options.TablePrefix + "LisItem", options.Schema);
                b.Property(b => b.Price).HasPrecision(18, 4);
                b.ConfigureByConvention();
            });
            //检查项子列表
            builder.Entity<PacsItem>(b =>
            {
                b.ToTable(options.TablePrefix + "PacsItem", options.Schema);
                b.Property(b => b.Price).HasPrecision(18, 4);
                b.ConfigureByConvention();
            });
            //检查病理小项
            builder.Entity<PacsPathologyItem>(b =>
            {
                b.ToTable(options.TablePrefix + "PacsPathologyItem", options.Schema);
                b.ConfigureByConvention();
            });
            //检查病理小项序号
            builder.Entity<PacsPathologyItemNo>(b =>
            {
                b.ToTable(options.TablePrefix + "PacsPathologyItemNo", options.Schema);
                b.ConfigureByConvention();
            });
            //诊疗项
            builder.Entity<Treat>(b =>
            {
                b.ToTable(options.TablePrefix + "Treat", options.Schema);
                b.Property(b => b.OtherPrice).HasPrecision(18, 4);
                b.ConfigureByConvention();
            });
            //医嘱打印信息
            builder.Entity<PrintInfo>(b =>
            {
                b.ToTable(options.TablePrefix + "PrintInfo", options.Schema);
                b.Property(p => p.IsPrintAgain).HasDefaultValue(false); //默认是第一次打印
                b.ConfigureByConvention();
            });
            //医嘱审计
            builder.Entity<DoctorsAdviceAudit>(b =>
            {
                b.ToTable(options.TablePrefix + "DoctorsAdviceAudit", options.Schema);
                b.ConfigureByConvention();
            });

            //医嘱回调HIS返回的结果
            builder.Entity<MedDetailResult>(b =>
            {
                b.ToTable(options.TablePrefix + "MedDetailResult", options.Schema);
                b.ConfigureByConvention();
            });

            builder.Entity<DrugStockQuery>(b =>
            {
                b.ToTable(options.TablePrefix + "DrugStockQuery", options.Schema);
                b.ConfigureByConvention();
            });

            //医技类
            builder.Entity<MedicalTechnologyMap>(b =>
            {
                b.ToTable(options.TablePrefix + "MedicalTechnologyMap", options.Schema);
                b.ConfigureByConvention();
            });

            //医嘱主键映射
            builder.Entity<DoctorsAdviceMap>(b =>
            {
                b.ToTable(options.TablePrefix + "DoctorsAdviceMap", options.Schema);
                b.ConfigureByConvention();
            });

            builder.Entity<ImmunizationRecord>(b =>
            {
                b.ToTable(options.TablePrefix + "ImmunizationRecord", options.Schema);
                b.HasIndex(i => i.DoctorAdviceId).IsUnique(false);
                b.HasIndex(i => i.MedicineId).IsUnique(false);
                b.ConfigureByConvention();
            });

            #endregion

            #region 自备药-独立模块

            builder.Entity<OwnMedicine>(b =>
            {
                b.ToTable(options.TablePrefix + "OwnMedicine", options.Schema);
                b.ConfigureByConvention();
            });

            #endregion

            #region 快速开嘱

            builder.Entity<QuickStartCatalogue>(b =>
            {
                b.ToTable(options.TablePrefix + "QuickStartCatalogue", options.Schema);
                b.HasIndex(x => x.DoctorCode).IsUnique(false);
                b.ConfigureByConvention();
            });

            builder.Entity<QuickStartAdvice>(b =>
            {
                b.ToTable(options.TablePrefix + "QuickStartAdvice", options.Schema);
                b.ConfigureByConvention();
            });

            builder.Entity<QuickStartMedicine>(b =>
            {
                b.ToTable(options.TablePrefix + "QuickStartMedicine", options.Schema);
                b.Property(b => b.DosageQty).HasPrecision(18, 3);
                b.ConfigureByConvention();
            });

            #endregion

            #region 分方
            builder.Entity<Prescription>(b =>
            {
                b.ToTable(options.TablePrefix + "Prescription", options.Schema);
                b.HasIndex(i => i.MyPrescriptionNo).IsUnique(false);
                b.ConfigureByConvention();
            });
            #endregion

            #region 系列号管理器

            builder.Entity<MySequence>(b =>
            {
                b.ToTable(options.TablePrefix + "MySequence", options.Schema);
                b.ConfigureByConvention();
            });

            #endregion Treat 诊疗项

            #region InviteDoctor 会诊邀请医生

            // 会诊邀请医生
            builder.Entity<InviteDoctor>(i =>
            {
                i.ToTable(options.TablePrefix + "InviteDoctors", options.Schema);
                i.ConfigureByConvention();
                i.HasIndex(x => x.GroupConsultationId);
            });

            #endregion InviteDoctor 会诊邀请医生

            #region GroupConsultation 会诊管理

            // 会诊管理
            builder.Entity<GroupConsultation>(g =>
            {
                g.ToTable(options.TablePrefix + "GroupConsultations", options.Schema);
                g.ConfigureByConvention();
                g.HasIndex(x => x.PI_ID);
            });

            #endregion GroupConsultation 会诊管理

            #region DoctorSummary 会诊纪要医生

            // 会诊纪要医生
            builder.Entity<DoctorSummary>(i =>
            {
                i.ToTable(options.TablePrefix + "DoctorSummary", options.Schema);
                i.ConfigureByConvention();
                i.HasIndex(x => x.GroupConsultationId);
            });

            #endregion DoctorSummary 会诊纪要医生

            #region 会诊配置
            builder.Entity<SettingPara>(b =>
            {
                b.ToTable(options.TablePrefix + "SettingPara", options.Schema);
                b.ConfigureByConvention();
            });
            #endregion

            #region 新冠rna检测申请
            builder.Entity<NovelCoronavirusRna>(b =>
            {
                b.ToTable(options.TablePrefix + "NovelCoronavirusRna", options.Schema);
                b.ConfigureByConvention();
            });
            #endregion

            #region 病历信息

            builder.Entity<PatientCase>(b =>
            {
                b.ToTable(options.TablePrefix + "PatientCase", options.Schema);
                b.HasIndex(x => x.Piid).IsUnique(false);
                b.ConfigureByConvention();
            });

            #endregion

            #region HIS对接审计日志

            builder.Entity<AuditLog>(b =>
            {
                b.ToTable(options.TablePrefix + "AuditLog", options.Schema);
                b.ConfigureByConvention();
            });

            #endregion

            #region 用户设置
            builder.Entity<UserSetting>(b =>
            {
                b.ToTable(options.TablePrefix + "UserSetting", options.Schema);
                b.HasIndex(x => x.UserName);
                b.ConfigureByConvention();
            });
            #endregion

            #region 电子病历导入记录
            builder.Entity<EmrUsedAdviceRecord>(b =>
            {
                b.ToTable(options.TablePrefix + "EmrUsedAdviceRecord", options.Schema);
                b.ConfigureByConvention();
            });

            #endregion
        }
    }
}