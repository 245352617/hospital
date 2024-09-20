using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel;
using System.Reflection;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.Nursing.Config;
using YiJian.Nursing.RecipeExecutes.Entities;
using YiJian.Nursing.Recipes.Entities;
using YiJian.Nursing.Settings;
using YiJian.Nursing.Temperatures;

namespace YiJian.Nursing.EntityFrameworkCore
{
    public static class NursingDbContextModelCreatingExtensions
    {
        public static void ConfigureNursing(
            this ModelBuilder builder,
            Action<NursingModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new NursingModelBuilderConfigurationOptions(
                NursingDbProperties.DbTablePrefix,
                NursingDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            #region 医嘱执行相关表
            //医嘱表
            builder.Entity<Recipe>(b =>
            {
                b.ToTable(options.TablePrefix + "Recipe", options.Schema);
                b.HasIndex(x => x.Code).IsUnique(false);
                b.HasIndex(x => x.Name).IsUnique(false);
                b.ConfigureByConvention();
                b.HasComment("医嘱");
            });

            //医嘱操作历史表
            builder.Entity<RecipeHistory>(b =>
            {
                b.ToTable(options.TablePrefix + "RecipeHistory", options.Schema);
                b.HasIndex(x => x.RecipeId).IsUnique(false);
                b.ConfigureByConvention();
                b.HasComment("医嘱操作历史");
            });

            //药物处方表
            builder.Entity<Prescribe>(b =>
            {
                b.ToTable(options.TablePrefix + "Prescribe", options.Schema);
                b.ConfigureByConvention();
                b.HasComment("药物处方");
            });

            //检查表
            builder.Entity<Pacs>(b =>
            {
                b.ToTable(options.TablePrefix + "Pacs", options.Schema);
                b.ConfigureByConvention();
                b.HasComment("检查");
            });

            //检验表
            builder.Entity<Lis>(b =>
            {
                b.ToTable(options.TablePrefix + "Lis", options.Schema);
                b.ConfigureByConvention();
                b.HasComment("检验");
            });

            //诊疗表
            builder.Entity<Treat>(b =>
            {
                b.ToTable(options.TablePrefix + "Treat", options.Schema);
                b.ConfigureByConvention();
                b.HasComment("诊疗");
            });

            //自备药
            builder.Entity<OwnMedicine>(b =>
            {
                b.ToTable(options.TablePrefix + "OwnMedicine", options.Schema);
                b.ConfigureByConvention();
                b.Property(x => x.DosageQty).HasPrecision(18, 4);
                b.Property(x => x.RecieveQty).HasPrecision(18, 4);
                b.HasComment("自备药");
            });

            //执行记录表
            builder.Entity<RecipeExecRecord>(b =>
            {
                b.ToTable(options.TablePrefix + "RecipeExecRecord", options.Schema);
                b.ConfigureByConvention();
                b.Property(x => x.DosageQty).HasPrecision(18, 4);
                b.Property(x => x.RemainDosage).HasPrecision(18, 4);
                b.Property(x => x.DiscardDosage).HasPrecision(18, 4);
                b.HasComment("执行记录表");
            });

            //拆分记录表(执行单)
            builder.Entity<RecipeExec>(b =>
            {
                b.ToTable(options.TablePrefix + "RecipeExec", options.Schema);
                b.ConfigureByConvention();
                b.Property(x => x.TotalDosage).HasPrecision(18, 4);
                b.Property(x => x.TotalExecDosage).HasPrecision(18, 4);
                b.Property(x => x.TotalRemainDosage).HasPrecision(18, 4);
                b.Property(x => x.TotalDiscardDosage).HasPrecision(18, 4);
                b.Property(x => x.ReserveDosage).HasPrecision(18, 4);
                b.HasComment("拆分记录表(执行单)");
            });

            //医嘱执行历史记录
            builder.Entity<RecipeExecHistory>(b =>
            {
                b.ToTable(options.TablePrefix + "RecipeExecHistory", options.Schema);
                b.ConfigureByConvention();
                b.HasComment("医嘱执行历史记录");
            });
            #endregion 医嘱执行相关表

            #region 导管相关表

            #region ParaModule 表:模块参数

            // 表:模块参数
            builder.Entity<ParaModule>(p =>
            {
                p.ToTable("Duct_ParaModule");

                p.ConfigureByConvention();

                p.HasComment("表:导管模块参数");

                #region ParaModule Properties

                p.Property(x => x.ModuleCode).HasComment("模块代码").HasMaxLength(ParaModuleConsts.MaxModuleCodeLength);

                p.Property(x => x.ModuleName).HasComment("模块名称").HasMaxLength(ParaModuleConsts.MaxModuleNameLength);

                p.Property(x => x.DisplayName).HasComment("模块显示名称").HasMaxLength(ParaModuleConsts.MaxDisplayNameLength);

                p.Property(x => x.DeptCode).HasComment("科室代码").HasMaxLength(ParaModuleConsts.MaxDeptCodeLength);

                p.Property(x => x.ModuleType)
                    .HasComment("模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）")
                    .HasMaxLength(ParaModuleConsts.MaxModuleTypeLength);

                p.Property(x => x.IsBloodFlow).HasComment("是否血流内导管");

                p.Property(x => x.Py).HasComment("模块拼音").HasMaxLength(ParaModuleConsts.MaxPyLength);

                p.Property(x => x.Sort).HasComment("排序");

                p.Property(x => x.IsEnable).HasComment("是否启用");

                #endregion

                p.HasIndex(x => x.ModuleCode);
            });

            #endregion ParaModule 表:模块参数

            #region ParaItem 表:护理项目表

            // 表:护理项目表
            builder.Entity<ParaItem>(p =>
            {
                p.ToTable("Duct_ParaItem");
                p.ConfigureByConvention();
                p.HasComment("表:导管护理项目表");

                #region ParaItem Properties

                p.Property(x => x.DeptCode).HasComment("科室编号").HasMaxLength(ParaItemConsts.MaxDeptCodeLength);

                p.Property(x => x.ModuleCode).HasComment("参数所属模块").HasMaxLength(ParaItemConsts.MaxModuleCodeLength);

                p.Property(x => x.ParaCode).HasComment("项目代码").HasMaxLength(ParaItemConsts.MaxParaCodeLength);

                p.Property(x => x.ParaName).HasComment("项目名称").HasMaxLength(ParaItemConsts.MaxParaNameLength);

                p.Property(x => x.DisplayName).HasComment("显示名称").HasMaxLength(ParaItemConsts.MaxDisplayNameLength);

                p.Property(x => x.ScoreCode).HasComment("评分代码").HasMaxLength(ParaItemConsts.MaxScoreCodeLength);

                p.Property(x => x.GroupName).HasComment("导管分类").HasMaxLength(ParaItemConsts.MaxGroupNameLength);

                p.Property(x => x.UnitName).HasComment("单位名称").HasMaxLength(ParaItemConsts.MaxUnitNameLength);

                p.Property(x => x.ValueType).HasComment("数据类型").HasMaxLength(ParaItemConsts.MaxValueTypeLength);

                p.Property(x => x.Style).HasComment("文本类型").HasMaxLength(ParaItemConsts.MaxStyleLength);

                p.Property(x => x.DecimalDigits).HasComment("小数位数").HasMaxLength(ParaItemConsts.MaxDecimalDigitsLength);

                p.Property(x => x.MaxValue).HasComment("最大值").HasMaxLength(ParaItemConsts.MaxMaxValueLength);

                p.Property(x => x.MinValue).HasComment("最小值").HasMaxLength(ParaItemConsts.MaxMinValueLength);

                p.Property(x => x.HighValue).HasComment("高值").HasMaxLength(ParaItemConsts.MaxHighValueLength);

                p.Property(x => x.LowValue).HasComment("低值").HasMaxLength(ParaItemConsts.MaxLowValueLength);

                p.Property(x => x.Threshold).HasComment("是否预警");

                p.Property(x => x.Sort).HasComment("排序号");

                p.Property(x => x.DataSource).HasComment("默认值").HasMaxLength(ParaItemConsts.MaxDataSourceLength);

                p.Property(x => x.DictFlag).HasComment("导管项目是否静态显示").HasMaxLength(ParaItemConsts.MaxDictFlagLength);

                p.Property(x => x.GuidFlag).HasComment("是否评分");

                p.Property(x => x.GuidId).HasComment("评分指引编号").HasMaxLength(ParaItemConsts.MaxGuidIdLength);

                p.Property(x => x.StatisticalType).HasComment("特护单统计参数分类")
                    .HasMaxLength(ParaItemConsts.MaxStatisticalTypeLength);

                p.Property(x => x.DrawChartFlag).HasComment("绘制趋势图形");

                p.Property(x => x.ColorParaCode).HasComment("关联颜色").HasMaxLength(ParaItemConsts.MaxColorParaCodeLength);

                p.Property(x => x.ColorParaName).HasComment("关联颜色名称")
                    .HasMaxLength(ParaItemConsts.MaxColorParaNameLength);

                p.Property(x => x.PropertyParaCode).HasComment("关联性状")
                    .HasMaxLength(ParaItemConsts.MaxPropertyParaCodeLength);

                p.Property(x => x.PropertyParaName).HasComment("关联性状名称")
                    .HasMaxLength(ParaItemConsts.MaxPropertyParaNameLength);

                p.Property(x => x.DeviceParaCode).HasComment("设备参数代码")
                    .HasMaxLength(ParaItemConsts.MaxDeviceParaCodeLength);

                p.Property(x => x.DeviceParaType).HasComment("设备参数类型（1-测量值，2-设定值）")
                    .HasMaxLength(ParaItemConsts.MaxDeviceParaTypeLength);

                p.Property(x => x.HealthSign).HasComment("是否生命体征项目");

                p.Property(x => x.KBCode).HasComment("知识库代码").HasMaxLength(ParaItemConsts.MaxKBCodeLength);

                p.Property(x => x.NuringViewCode).HasComment("护理概览参数")
                    .HasMaxLength(ParaItemConsts.MaxNuringViewCodeLength);

                p.Property(x => x.AbnormalSign).HasComment("是否异常体征参数")
                    .HasMaxLength(ParaItemConsts.MaxAbnormalSignLength);

                p.Property(x => x.IsUsage).HasComment("是否用药所属项目");

                p.Property(x => x.AddSource).HasComment("添加来源").HasMaxLength(ParaItemConsts.MaxAddSourceLength);

                p.Property(x => x.IsEnable).HasComment("是否启用");

                p.Property(x => x.ParaItemType).HasComment("项目参数类型，用于区分监护仪或者呼吸机等");

                #endregion

                p.HasIndex(x => x.DeptCode);
            });

            #endregion ParaItem 表:护理项目表

            #region Dict 表:导管字典-通用业务

            // 表:导管字典-通用业务
            builder.Entity<Dict>(d =>
            {
                d.ToTable("Duct_Dict");

                d.ConfigureByConvention();

                d.HasComment("表:导管字典-通用业务");

                #region Dict Properties

                d.Property(x => x.ParaCode).HasComment("参数代码").HasMaxLength(DictConsts.MaxParaCodeLength);

                d.Property(x => x.ParaName).HasComment("参数名称").HasMaxLength(DictConsts.MaxParaNameLength);

                d.Property(x => x.DictCode).HasComment("字典代码").HasMaxLength(DictConsts.MaxDictCodeLength);

                d.Property(x => x.DictValue).HasComment("字典值").HasMaxLength(DictConsts.MaxDictValueLength);

                d.Property(x => x.DictDesc).HasComment("字典值说明").HasMaxLength(DictConsts.MaxDictDescLength);

                d.Property(x => x.ParentId).HasComment("上级代码").HasMaxLength(DictConsts.MaxParentIdLength);

                d.Property(x => x.DictStandard).HasComment("字典标准（国标、自定义）")
                    .HasMaxLength(DictConsts.MaxDictStandardLength);

                d.Property(x => x.HisCode).HasComment("HIS对照代码").HasMaxLength(DictConsts.MaxHisCodeLength);

                d.Property(x => x.HisName).HasComment("HIS对照").HasMaxLength(DictConsts.MaxHisNameLength);

                d.Property(x => x.DeptCode).HasComment("科室代码").HasMaxLength(DictConsts.MaxDeptCodeLength);

                d.Property(x => x.ModuleCode).HasComment("模块代码").HasMaxLength(DictConsts.MaxModuleCodeLength);

                d.Property(x => x.Sort).HasComment("排序");

                d.Property(x => x.IsDefault).HasComment("是否默认");

                d.Property(x => x.IsEnable).HasComment("是否启用");

                #endregion

                d.HasIndex(x => x.ParaCode);
            });

            #endregion Dict 表:导管字典-通用业务

            #region CanulaPart 表:人体图-编号字典

            // 表:人体图-编号字典
            builder.Entity<CanulaPart>(c =>
            {
                c.ToTable("Duct_CanulaPart");

                c.ConfigureByConvention();

                c.HasComment("表:人体图-编号字典");

                #region CanulaPart Properties

                c.Property(x => x.DeptCode).HasComment("科室代码");

                c.Property(x => x.ModuleCode).HasComment("模块代码");

                c.Property(x => x.PartName).HasComment("部位名称");

                c.Property(x => x.PartNumber).HasComment("部位编号");

                c.Property(x => x.Sort).HasComment("排序");

                c.Property(x => x.IsEnable).HasComment("是否可用");
                c.ConfigureColumnComment();

                #endregion
            });

            #endregion CanulaPart 表:人体图-编号字典

            #region NursingCanula 表:导管护理信息

            // 表:导管护理信息
            builder.Entity<NursingCanula>(c =>
            {
                c.ToTable("Duct_NursingCanula");

                c.ConfigureByConvention();

                c.HasComment("表:导管护理信息");

                c.Property(x => x.PI_ID).HasComment("患者id");

                c.Property(x => x.ModuleCode).HasComment("导管分类").HasMaxLength(20);

                c.Property(x => x.StartTime).HasComment("插管时间");

                c.Property(x => x.StopTime).HasComment("拔管时间");

                c.Property(x => x.ModuleName).HasComment("排序").HasMaxLength(20);

                c.Property(x => x.CanulaName).HasComment("管道名称").HasMaxLength(40);

                c.Property(x => x.CanulaPart).HasComment("插管部位").HasMaxLength(40);
                c.Property(x => x.CanulaNumber).HasComment("插管次数");
                c.Property(x => x.CanulaPosition).HasComment("插管地点").HasMaxLength(40);
                c.Property(x => x.DoctorId).HasComment("置管人代码").HasMaxLength(50);
                c.Property(x => x.DoctorName).HasComment("置管人名称").HasMaxLength(100);

                c.Property(x => x.CanulaWay).HasComment("置入方式").HasMaxLength(10);
                c.Property(x => x.CanulaLength).HasComment("置管长度").HasMaxLength(20);
                c.Property(x => x.DrawReason).HasComment("拔管原因").HasMaxLength(255);
                c.Property(x => x.TubeDrawState).HasComment("管道状态");
                c.Property(x => x.UseFlag).HasComment("使用标志：（Y在用，N已拔管）").HasMaxLength(4);
                c.Property(x => x.NurseId).HasComment("护士Id").HasMaxLength(50);
                c.Property(x => x.NurseName).HasComment("护士名称").HasMaxLength(100);
                c.Property(x => x.NurseTime).HasComment("护理时间");
            });

            #endregion NursingCanula 表:导管护理信息

            #region Canula 表:导管护理记录信息

            // 表:导管护理记录信息
            builder.Entity<Canula>(c =>
            {
                c.ToTable("Duct_Canula");
                c.ConfigureByConvention();
                c.HasComment("表:导管护理记录信息");
                c.Property(x => x.CanulaId).HasComment("导管主表主键");
                c.Property(x => x.NurseTime).HasComment("护理时间");
                c.Property(x => x.CanulaWay).HasComment("置入方式").HasMaxLength(10);
                c.Property(x => x.CanulaLength).HasComment("置管长度").HasMaxLength(20);
                c.Property(x => x.NurseId).HasComment("护士Id").HasMaxLength(50);
                c.Property(x => x.NurseName).HasComment("护士名称").HasMaxLength(100);
                c.Property(x => x.NurseTime).HasComment("护理时间");
            });

            #endregion Canula 表:导管护理记录信息

            #region CanulaDynamic 表:导管参数动态

            // 表:导管护理记录信息
            builder.Entity<CanulaDynamic>(c =>
            {
                c.ToTable("Duct_CanulaDynamic");
                c.ConfigureByConvention();
                c.HasComment("表:导管参数动态");
                c.Property(x => x.CanulaId).HasComment("导管主表主键");
                c.Property(x => x.GroupName).HasComment("管道分类").HasMaxLength(40);
                c.Property(x => x.ParaCode).HasComment("参数代码").HasMaxLength(40);
                c.Property(x => x.ParaName).HasComment("参数名称").HasMaxLength(255);
                c.Property(x => x.ParaValue).HasComment("参数值").HasMaxLength(255);
            });

            #endregion CanulaDynamic 表:导管参数动态
            #endregion 导管相关表

            //体温单
            builder.Entity<Temperature>(b =>
            {
                b.ToTable(options.TablePrefix + "Temperature", options.Schema);
                b.ConfigureByConvention();
                b.HasComment("体温单表");
            });

            //体温表动态属性
            builder.Entity<TemperatureDynamic>(b =>
            {
                b.ToTable(options.TablePrefix + "TemperatureDynamic", options.Schema);
                b.ConfigureByConvention();
                b.HasComment("体温表动态属性");
            });

            //体温表记录
            builder.Entity<TemperatureRecord>(b =>
            {
                b.ToTable(options.TablePrefix + "TemperatureRecord", options.Schema);
                b.ConfigureByConvention();
                b.Property(x => x.RetestTemperature).HasPrecision(18, 4);
                b.Property(x => x.Temperature).HasPrecision(18, 4);
                b.HasComment("体温表记录");
            });

            //临床事件表
            builder.Entity<ClinicalEvent>(b =>
            {
                b.ToTable(options.TablePrefix + "ClinicalEvent", options.Schema);
                b.ConfigureByConvention();
                b.HasComment("临床事件表");
            });

            //护士站通用配置表
            builder.Entity<NursingConfig>(b =>
            {
                b.ToTable(options.TablePrefix + "NursingConfig", options.Schema);
                b.ConfigureByConvention();
                b.HasComment("护士站通用配置表");
            });
        }

        /// <summary>
        /// 字段生成添加注释
        /// </summary>
        /// <param name="b"></param>
        private static void ConfigureColumnComment(this EntityTypeBuilder b)
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