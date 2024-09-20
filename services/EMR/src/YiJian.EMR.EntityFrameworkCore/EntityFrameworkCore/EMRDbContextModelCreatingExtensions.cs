using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using YiJian.EMR.ApplicationSettings.Entities;
using YiJian.EMR.Characters.Entities;
using YiJian.EMR.DailyExpressions.Entities;
using YiJian.EMR.DataBinds.Entities;
using YiJian.EMR.DataElements.Entities;
using YiJian.EMR.Enums;
using YiJian.EMR.Libs.Entities;
using YiJian.EMR.EmrPermissions.Entities;
using YiJian.EMR.Props.Entities;
using YiJian.EMR.Templates;
using YiJian.EMR.Templates.Entities;
using YiJian.EMR.Writes.Entities;
using YiJian.EMR.XmlHistories.Entities;
using YiJian.EMR.CloudSign.Entities;

namespace YiJian.EMR.EntityFrameworkCore
{
    public static class EMRDbContextModelCreatingExtensions
    {
        public static void ConfigureEMR(
            this ModelBuilder builder,
            Action<EMRModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new EMRModelBuilderConfigurationOptions(
                EMRDbProperties.DbTablePrefix,
                EMRDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            //目录树结构
            builder.Entity<Catalogue>(b =>
            {
                b.ToTable(options.TablePrefix + "Catalogue", options.Schema);
                b.HasIndex(x => x.Title).IsUnique(false);
                b.Property(x => x.Classify).HasDefaultValue(EClassify.EMR);
                b.ConfigureByConvention();
            });
            //XML电子病历模板（电子病例库的）
            builder.Entity<XmlTemplate>(b =>
            {
                b.ToTable(options.TablePrefix + "XmlTemplate", options.Schema);
                b.HasIndex(x => x.CatalogueId);
                b.ConfigureByConvention();
            });


            //通用模板
            builder.Entity<TemplateCatalogue>(b =>
            {
                b.ToTable(options.TablePrefix + "TemplateCatalogue", options.Schema);
                b.HasIndex(x => x.Title);
                b.HasIndex(x => x.DeptCode);
                b.HasIndex(x => x.DoctorCode);
                b.HasIndex(x => x.Lv);
                b.Property(x => x.DoctorCode).HasDefaultValue("");
                b.Property(x => x.DoctorName).HasDefaultValue("");
                b.Property(x => x.DoctorCode).HasDefaultValue("");
                b.Property(x => x.IsEnabled).HasDefaultValue(false);
                b.Property(x => x.Classify).HasDefaultValue(EClassify.EMR);
                b.ConfigureByConvention();
            });
            //XML电子病历模板
            builder.Entity<MyXmlTemplate>(b =>
            {
                b.ToTable(options.TablePrefix + "MyXmlTemplate", options.Schema);
                b.HasIndex(x => x.TemplateCatalogueId);
                b.ConfigureByConvention();
            });
            //病区管理
            builder.Entity<InpatientWard>(b =>
            {
                b.ToTable(options.TablePrefix + "InpatientWard", options.Schema);
                b.ConfigureByConvention();
            });
            //科室历史记录
            builder.Entity<Department>(b =>
            {
                b.ToTable(options.TablePrefix + "Department", options.Schema);
                b.ConfigureByConvention();
            });

            //患者电子病历
            builder.Entity<PatientEmr>(b =>
            {
                b.ToTable(options.TablePrefix + "PatientEmr", options.Schema);
                b.HasIndex(x => x.DoctorCode);
                b.Property(x => x.Classify).HasDefaultValue(EClassify.EMR);
                b.ConfigureByConvention();
            });
            //患者的电子病历xml文档
            builder.Entity<PatientEmrXml>(b =>
            {
                b.ToTable(options.TablePrefix + "PatientEmrXml", options.Schema);
                b.HasIndex(x => x.PatientEmrId);
                b.ConfigureByConvention();
            });
            //患者的电子病历xml文档
            builder.Entity<XmlHistory>(b =>
            {
                b.ToTable(options.TablePrefix + "XmlHistory", options.Schema);
                b.HasIndex(x => x.XmlId);
                b.HasIndex(x => x.XmlCategory);
                b.ConfigureByConvention();
            });
            /*
            //患者电子病历数据存档
            builder.Entity<PatientEmrData>(b =>
            {
                b.ToTable(options.TablePrefix + "PatientEmrData", options.Schema);
                b.HasIndex(x => x.PatientEmrXmlId);
                b.ConfigureByConvention();
            });
            */
            //电子病历属性
            builder.Entity<CategoryProperty>(b =>
            {
                b.ToTable(options.TablePrefix + "CategoryProperty", options.Schema);
                b.ConfigureByConvention();
            });

            builder.Entity<DataBindContext>(b =>
            {
                b.ToTable(options.TablePrefix + "DataBindContext", options.Schema);
                b.Property(x => x.Classify).HasDefaultValue(EClassify.EMR);

                //应付老数据迁移问题
                b.Property(x => x.VisitNo).HasDefaultValue("");
                b.Property(x => x.RegisterSerialNo).HasDefaultValue("");
                b.Property(x => x.OrgCode).HasDefaultValue("");

                b.HasIndex(x => x.PI_ID).IsUnique(false);
                b.ConfigureByConvention();
            });
            builder.Entity<DataBindMap>(b =>
            {
                b.ToTable(options.TablePrefix + "DataBindMap", options.Schema);
                b.ConfigureByConvention();
            });

            builder.Entity<EmrBaseInfo>(b =>
            {
                b.ToTable(options.TablePrefix + "EmrBaseInfo", options.Schema);
                b.Property(x => x.OrgCode).HasDefaultValue("H7110");
                b.Property(x => x.Channel).HasDefaultValue("SZKJ");
                b.ConfigureByConvention();
            });


            #region 数据元

            builder.Entity<DataElement>(b =>
            {
                b.ToTable(options.TablePrefix + "DataElement", options.Schema);
                b.ConfigureByConvention();
            });
            builder.Entity<DataElementItem>(b =>
            {
                b.ToTable(options.TablePrefix + "DataElementItem", options.Schema);
                b.HasIndex(x => x.DataElementId);
                b.ConfigureByConvention();
            });
            builder.Entity<DataElementDropdown>(b =>
            {
                b.ToTable(options.TablePrefix + "DataElementDropdown", options.Schema);
                b.HasIndex(x => x.DataElementItemId);
                b.ConfigureByConvention();
            });
            builder.Entity<DataElementDropdownItem>(b =>
            {
                b.ToTable(options.TablePrefix + "DataElementDropdownItem", options.Schema);
                b.HasIndex(x => x.DataElementDropdownId);
                b.ConfigureByConvention();
            });

            #endregion
            //应用配置
            builder.Entity<AppSetting>(b =>
            {
                b.ToTable(options.TablePrefix + "AppSetting", options.Schema);
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

            builder.Entity<UniversalCharacter>(b =>
            {
                b.ToTable(options.TablePrefix + "UniversalCharacter", options.Schema);
                b.ConfigureByConvention();
            });

            builder.Entity<UniversalCharacterNode>(b =>
            {
                b.ToTable(options.TablePrefix + "UniversalCharacterNode", options.Schema);
                b.ConfigureByConvention();
            });

            builder.Entity<Permission>(b =>
            {
                b.ToTable(options.TablePrefix + "Permission", options.Schema);
                b.ConfigureByConvention();
            });

            builder.Entity<OperatingAccount>(b =>
            {
                b.ToTable(options.TablePrefix + "OperatingAccount", options.Schema);
                b.ConfigureByConvention();
            });

            builder.Entity<CloudSignInfo>(b =>
            {
                b.ToTable(options.TablePrefix + "CloudSignInfo", options.Schema);
                b.ConfigureByConvention();
            });

            builder.Entity<MergeTemplateWhiteList>(b =>
            {
                b.ToTable(options.TablePrefix + "MergeTemplateWhiteList", options.Schema);
                b.ConfigureByConvention();
            });

            //就诊流水号对应的病历信息
            builder.Entity<ViewVisitSerialEmr>().ToView("v_visitSerial_emrs").HasNoKey();

            builder.Entity<MinioEmrInfo>(b =>
            {
                b.ToTable(options.TablePrefix + "MinioEmrInfo", options.Schema);
                b.HasIndex(idx => idx.PatientEmrId).IsUnique(false);
                b.ConfigureByConvention();
            });

            //就诊流水号对应的病历信息
            builder.Entity<ViewVisitSerialEmr>().ToView("v_visitSerial_emrs").HasNoKey();
        }
    }
}