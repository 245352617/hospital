using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
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

public static class MasterDataDbContextModelCreatingExtensions
{
    public static void ConfigureMasterData(
        this ModelBuilder builder,
        Action<MasterDataModelBuilderConfigurationOptions> optionsAction = null)
    {
        Check.NotNull(builder, nameof(builder));

        // Dict_
        var options = new MasterDataModelBuilderConfigurationOptions(
            "Dict_",
            null
        );
        optionsAction?.Invoke(options);

        // Sys_
        var sysOptions = new MasterDataModelBuilderConfigurationOptions(
            "Sys_",
            null);
        optionsAction?.Invoke(options);

        #region Dictionaries

        // 平台字典表
        builder.Entity<Dictionaries>(d =>
        {
            d.ToTable(options.TablePrefix + "Dictionaries", options.Schema);

            d.ConfigureByConvention();

            d.HasComment("平台字典表");
        });

        #endregion

        #region Operation

        // 手术字典表
        builder.Entity<Operation>(o =>
        {
            o.ToTable(options.TablePrefix + "Operation", options.Schema);

            o.ConfigureByConvention();

            o.HasComment("手术字典表");
        });

        #endregion

        #region Medicine

        #region Medicine 药品字典

        // 药品字典
        builder.Entity<Medicine>(m =>
        {
            m.ToTable(options.TablePrefix + "Medicine", options.Schema);

            m.ConfigureByConvention();

            m.HasComment("药品字典");
            m.Property(x => x.DosageQty).HasPrecision(18, 3);
            m.HasIndex(x => x.MedicineCode);

        });

        #endregion Medicine 药品字典

        #endregion Medicine

        #region LabCatalog 检验目录

        // 检验目录
        builder.Entity<LabCatalog>(l =>
        {
            l.ToTable(options.TablePrefix + "LabCatalog", options.Schema);

            l.ConfigureByConvention();

            l.HasComment("检验目录");

            l.HasIndex(x => x.CatalogCode).IsUnique(false);
        });

        #endregion LabCatalog 检验目录

        #region LabProject 检验项目

        // 检验项目
        builder.Entity<LabProject>(l =>
        {
            l.ToTable(options.TablePrefix + "LabProject", options.Schema);

            l.ConfigureByConvention();

            l.HasComment("检验项目");

            l.HasIndex(x => x.ProjectCode);
        });

        #endregion LabProject 检验项目

        #region LabTarget 检验明细项

        // 检验明细项
        builder.Entity<LabTarget>(l =>
        {
            l.ToTable(options.TablePrefix + "LabTarget", options.Schema);

            l.ConfigureByConvention();

            l.HasComment("检验明细项");

            l.HasIndex(x => x.TargetCode);
        });

        #endregion LabTarget 检验明细项


        #region DictionariesType

        // 字典类型编码
        builder.Entity<DictionariesTypes.DictionariesType>(d =>
        {
            d.ToTable(options.TablePrefix + "DictionariesType", options.Schema);

            d.ConfigureByConvention();

            d.HasComment("字典类型编码");

            #region DictionariesTypeProperty

            d.Property(x => x.DictionariesTypeCode).HasComment("字典类型编码")
                .HasMaxLength(DictionariesTypeConsts.MaxDictionariesTypeCodeLength);

            d.Property(x => x.DictionariesTypeName).HasComment("字典类型名称")
                .HasMaxLength(DictionariesTypeConsts.MaxDictionariesTypeNameLength);

            d.Property(x => x.Remark).HasComment("备注");

            #endregion

            d.HasIndex(x => x.DictionariesTypeCode);
        });

        #endregion DictionariesType

        #region LabSpecimen 检验标本

        // 检验标本
        builder.Entity<LabSpecimen>(l =>
        {
            l.ToTable(options.TablePrefix + "LabSpecimen", options.Schema);

            l.ConfigureByConvention();

            l.HasComment("检验标本");

            l.HasIndex(x => x.SpecimenCode);
        });

        #endregion LabSpecimen 检验标本


        #region ExamCatalog 检查目录

        // 检查目录
        builder.Entity<ExamCatalog>(e =>
        {
            e.ToTable(options.TablePrefix + "ExamCatalog", options.Schema);
            e.ConfigureByConvention();
            e.HasComment("检查目录");
            e.HasIndex(x => x.CatalogCode);
        });

        #endregion ExamCatalog 检查目录

        #region ExamNote 检查申请注意事项

        // 检查申请注意事项
        builder.Entity<ExamNote>(e =>
        {
            e.ToTable(options.TablePrefix + "ExamNote", options.Schema);

            e.ConfigureByConvention();

            e.HasComment("检查申请注意事项");

            e.HasIndex(x => x.NoteCode);
        });

        #endregion ExamNote 检查申请注意事项

        #region ExamPart 检查部位

        // 检查部位
        builder.Entity<ExamPart>(e =>
        {
            e.ToTable(options.TablePrefix + "ExamPart", options.Schema);

            e.ConfigureByConvention();

            e.HasComment("检查部位");
            e.HasIndex(x => x.PartCode);
        });

        #endregion ExamPart 检查部位

        #region ExamProject 检查申请项目

        // 检查申请项目
        builder.Entity<ExamProject>(e =>
        {
            e.ToTable(options.TablePrefix + "ExamProject", options.Schema);

            e.ConfigureByConvention();

            e.HasComment("检查申请项目");

            e.HasIndex(x => x.ProjectCode);
        });

        #endregion ExamProject 检查申请项目

        #region ExamAttachItem 检查附加项

        // 检查申请项目
        builder.Entity<ExamAttachItem>(e =>
        {
            e.ToTable(options.TablePrefix + "ExamAttachItem", options.Schema);

            e.ConfigureByConvention();

            e.HasComment("检查附加项");

            e.HasIndex(x => x.ProjectCode);
        });

        #endregion ExamAttachItem 检查附加项

        #region ExamTarget 检查明细项

        // 检查明细项
        builder.Entity<ExamTarget>(e =>
        {
            e.ToTable(options.TablePrefix + "ExamTarget", options.Schema);

            e.ConfigureByConvention();

            e.HasComment("检查明细项");

            e.HasIndex(x => x.TargetCode);
        });

        #endregion ExamTarget 检查明细项


        #region Sequence 序列

        // 序列
        builder.Entity<Sequence>(s =>
        {
            s.ToTable(sysOptions.TablePrefix + "Sequence", sysOptions.Schema);

            s.ConfigureByConvention();

            s.HasComment("序列");

            #region Sequence Properties

            s.Property(x => x.Code).HasComment("编码").HasMaxLength(SequenceConsts.MaxCodeLength);

            s.Property(x => x.Name).HasComment("名称").HasMaxLength(SequenceConsts.MaxNameLength);

            s.Property(x => x.Value).HasComment("序列值");

            s.Property(x => x.Format).HasComment("格式").HasMaxLength(SequenceConsts.MaxFormatLength);

            s.Property(x => x.Length).HasComment("序列值长度");

            s.Property(x => x.Date).HasComment("日期");

            s.Property(x => x.Memo).HasComment("备注").HasMaxLength(SequenceConsts.MaxMemoLength);

            #endregion

            s.HasIndex(x => x.Code);
        });

        #endregion Sequence 序列


        #region MedicineFrequency 药品频次字典

        // 药品频次字典     FrequencyUnit :0T   1D   1W  2D  1H  2H  3H  4H  st     eg:FrequencyCode	FrequencyName	FrequencyTimes	FrequencyUnit	ExecTimes	                FrequencyWeek	     Catalog          qd	             1次/天	             1	            1D	            08:00       	                  	           0        FrequencyCode	FrequencyName	FrequencyTimes	FrequencyUnit	ExecuteDayTime	            FrequencyWeek        Catalog          q6h	         1次/6小时	         4	            1D	        08:00,14:00,20:00,02:00	                           1        FrequencyCode	FrequencyName	FrequencyTimes	FrequencyUnit	ExecuteDayTime	            FrequencyWeek        Catalog          tiw135	         3次/周	             3	            1W          08：00,08：00,08：00；	     周一,周三,周五        1
        builder.Entity<MedicineFrequency>(m =>
        {
            m.ToTable(options.TablePrefix + "MedicineFrequency", options.Schema);

            m.ConfigureByConvention();

            m.HasComment("药品频次字典");

            m.HasIndex(x => x.FrequencyCode);
        });

        #endregion MedicineFrequency 药品频次字典

        #region MedicineUsage 药品用法字典

        // 药品用法字典
        builder.Entity<MedicineUsage>(m =>
        {
            m.ToTable(options.TablePrefix + "MedicineUsage", options.Schema);

            m.ConfigureByConvention();

            m.HasComment("药品用法字典");

            m.HasIndex(x => x.UsageCode);
        });

        #endregion MedicineUsage 药品用法字典


        #region LabContainer 容器编码

        // 容器编码
        builder.Entity<LabContainer>(l =>
        {
            l.ToTable(options.TablePrefix + "LabContainer", options.Schema);

            l.ConfigureByConvention();

            l.HasComment("容器编码");

            l.HasIndex(x => x.ContainerCode);
        });

        #endregion LabContainer 容器编码


        #region LabSpecimenPosition 检验标本采集部位

        // 检验标本采集部位
        builder.Entity<LabSpecimenPosition>(l =>
        {
            l.ToTable(options.TablePrefix + "LabSpecimenPosition", options.Schema);

            l.ConfigureByConvention();

            l.HasComment("检验标本采集部位");

            l.HasIndex(x => x.SpecimenCode);
        });

        #endregion LabSpecimenPosition 检验标本采集部位

        #region Treat 诊疗项目字典

        // 诊疗项目字典
        builder.Entity<Treat>(t =>
        {
            t.ToTable(options.TablePrefix + "Treat", options.Schema);

            t.ConfigureByConvention();

            t.HasComment("诊疗项目字典");


            t.HasIndex(x => x.TreatCode);
        });

        #endregion Treat 诊疗项目字典


        #region ViewSetting 视图配置

        // 视图配置
        builder.Entity<ViewSetting>(v =>
        {
            v.ToTable(options.TablePrefix + "ViewSetting", options.Schema);

            v.ConfigureByConvention();

            v.HasComment("视图配置");

            #region Properties

            #endregion

            #region ViewSetting Properties

            v.Property(x => x.Prop).HasComment("属性").HasMaxLength(ViewSettingConsts.MaxPropLength);

            v.Property(x => x.DefaultLabel).HasComment("默认标头")
                .HasMaxLength(ViewSettingConsts.MaxDefaultLabelLength);

            v.Property(x => x.Label).HasComment("标头").HasMaxLength(ViewSettingConsts.MaxLabelLength);

            v.Property(x => x.DefaultHeaderAlign).HasComment("默认标头对齐")
                .HasMaxLength(ViewSettingConsts.MaxDefaultHeaderAlignLength);

            v.Property(x => x.HeaderAlign).HasComment("标头对齐").HasMaxLength(ViewSettingConsts.MaxHeaderAlignLength);

            v.Property(x => x.DefaultAlign).HasComment("默认对齐")
                .HasMaxLength(ViewSettingConsts.MaxDefaultAlignLength);

            v.Property(x => x.Align).HasComment("对齐").HasMaxLength(ViewSettingConsts.MaxAlignLength);

            v.Property(x => x.DefaultWidth).HasComment("默认宽度");

            v.Property(x => x.Width).HasComment("宽度");

            v.Property(x => x.DefaultMinWidth).HasComment("默认最小宽度");

            v.Property(x => x.MinWidth).HasComment("最小宽度");

            v.Property(x => x.DefaultVisible).HasComment("默认显示");

            v.Property(x => x.Visible).HasComment("是否显示");

            v.Property(x => x.DefaultShowTooltip).HasComment("默认是否提示");

            v.Property(x => x.ShowTooltip).HasComment("是否提示");

            v.Property(x => x.DefaultIndex).HasComment("默认序号");

            v.Property(x => x.Index).HasComment("序号");

            v.Property(x => x.View).HasComment("视图").HasMaxLength(ViewSettingConsts.MaxViewLength);

            v.Property(x => x.Comment).HasComment("注释").HasMaxLength(ViewSettingConsts.MaxCommentLength);

            v.Property(x => x.ParentID).HasComment("父级ID");

            #endregion

            v.HasIndex(x => x.Prop);
        });

        #endregion ViewSetting 视图配置

        #region AllItem 诊疗检查检验药品项目合集

        // 诊疗检查检验药品项目合集
        builder.Entity<AllItem>(a =>
        {
            a.ToTable(options.TablePrefix + "AllItem", options.Schema);

            a.ConfigureByConvention();

            a.HasComment("诊疗检查检验药品项目合集");

            #region AllItem Properties

            a.Property(x => x.CategoryCode).HasComment("分类编码").HasMaxLength(AllItemConsts.MaxCategoryCodeLength);

            a.Property(x => x.CategoryName).HasComment("分类名称").HasMaxLength(AllItemConsts.MaxCategoryNameLength);

            a.Property(x => x.Code).HasComment("编码").HasMaxLength(AllItemConsts.MaxCodeLength);

            a.Property(x => x.Name).HasComment("名称").HasMaxLength(AllItemConsts.MaxNameLength);

            a.Property(x => x.Unit).HasComment("单位").HasMaxLength(AllItemConsts.MaxUnitLength);

            a.Property(x => x.Price).HasComment("价格");

            a.Property(x => x.IndexNo).HasComment("排序");

            a.Property(x => x.TypeCode).HasComment("类型编码").HasMaxLength(AllItemConsts.MaxTypeCodeLength);

            a.Property(x => x.TypeName).HasComment("类型名称").HasMaxLength(AllItemConsts.MaxTypeNameLength);

            a.Property(x => x.ChargeCode).HasComment("收费分类编码").HasMaxLength(AllItemConsts.MaxChargeCodeLength);

            a.Property(x => x.ChargeName).HasComment("收费分类名称").HasMaxLength(AllItemConsts.MaxChargeNameLength);

            #endregion

            a.HasIndex(x => x.CategoryCode);
        });

        #endregion AllItem 诊疗检查检验药品项目合集

        #region VitalSignExpression 生命体征表达式

        // 评分项
        builder.Entity<VitalSignExpression>(v =>
        {
            v.ToTable(options.TablePrefix + "VitalSignExpression", options.Schema);

            v.ConfigureByConvention();

            v.HasComment("生命体征表达式");

            #region VitalSignExpression Properties

            v.Property(x => x.ItemName).HasComment("评分项").HasMaxLength(VitalSignExpressionConsts.MaxItemNameLength);

            v.Property(x => x.StLevelExpression).HasComment("Ⅰ级评分表达式")
                .HasMaxLength(VitalSignExpressionConsts.MaxStLevelExpressionLength);

            v.Property(x => x.NdLevelExpression).HasComment("Ⅱ级评分表达式")
                .HasMaxLength(VitalSignExpressionConsts.MaxNdLevelExpressionLength);

            v.Property(x => x.RdLevelExpression).HasComment("Ⅲ级评分表达式")
                .HasMaxLength(VitalSignExpressionConsts.MaxRdLevelExpressionLength);

            v.Property(x => x.ThALevelExpression).HasComment("Ⅳa级评分表达式")
                .HasMaxLength(VitalSignExpressionConsts.MaxThALevelExpressionLength);

            v.Property(x => x.ThBLevelExpression).HasComment("Ⅳb级评分表达式")
                .HasMaxLength(VitalSignExpressionConsts.MaxThBLevelExpressionLength);

            v.Property(x => x.DefaultStLevelExpression).HasComment("默认Ⅰ级评分表达式")
                .HasMaxLength(VitalSignExpressionConsts.MaxDefaultStLevelExpressionLength);

            v.Property(x => x.DefaultNdLevelExpression).HasComment("默认Ⅱ级评分表达式")
                .HasMaxLength(VitalSignExpressionConsts.MaxDefaultNdLevelExpressionLength);

            v.Property(x => x.DefaultRdLevelExpression).HasComment("默认Ⅲ级评分表达式")
                .HasMaxLength(VitalSignExpressionConsts.MaxDefaultRdLevelExpressionLength);

            v.Property(x => x.DefaultThALevelExpression).HasComment("默认Ⅳa级评分表达式")
                .HasMaxLength(VitalSignExpressionConsts.MaxDefaultThALevelExpressionLength);

            v.Property(x => x.DefaultThBLevelExpression).HasComment("默认Ⅳb级评分表达式")
                .HasMaxLength(VitalSignExpressionConsts.MaxDefaultThBLevelExpressionLength);

            #endregion

            v.HasIndex(x => x.ItemName);
        });

        #endregion VitalSignExpression 生命体征表达式

        #region 分方管理

        // 诊疗项目字典
        builder.Entity<Separation>(t =>
        {
            t.ToTable(options.TablePrefix + "Separation", options.Schema);
            t.ConfigureByConvention();
        });

        //用药途经
        builder.Entity<Usage>(t =>
        {
            t.ToTable(options.TablePrefix + "Usage", options.Schema);
            t.ConfigureByConvention();
        });

        #endregion

        #region 药房配置

        builder.Entity<Pharmacy>(t =>
        {
            t.ToTable(options.TablePrefix + "Pharmacy", options.Schema);
            t.Property(p => p.IsDefault).HasDefaultValue(false);
            t.ConfigureByConvention();
        });

        #endregion

        #region 嘱托配置

        builder.Entity<Entrust>(t =>
        {
            t.ToTable(options.TablePrefix + "Entrust", options.Schema);
            t.ConfigureByConvention();
        });

        #endregion

        #region 诊疗分组

        builder.Entity<TreatGroup>(t =>
        {
            t.ToTable(options.TablePrefix + "TreatGroup", options.Schema);
            t.ConfigureByConvention();
        });

        #endregion

        #region 外部消息接收 日志存储

        builder.Entity<ReceivedLog>(t =>
        {
            t.ToTable(sysOptions.TablePrefix + "ReceivedLog", options.Schema);
            t.ConfigureByConvention();
        });

        #endregion
        #region 地区
        builder.Entity<Region>(t =>
        {
            t.ToTable(sysOptions.TablePrefix + "Region", options.Schema);
            t.ConfigureByConvention();
        });

        builder.Entity<Area>(t =>
        {
            t.ToTable(sysOptions.TablePrefix + "Area", options.Schema);
            t.ConfigureByConvention();
        });
        #endregion

        #region 科室表
        builder.Entity<Department>(b =>
        {
            b.HasComment("科室表");
            b.ToTable(options.TablePrefix + "Department", options.Schema);

            b.ConfigureByConvention();

            b.Property(d => d.Name).HasComment("名称").IsRequired().HasMaxLength(50);
            b.Property(c => c.Code).HasComment("编码").IsRequired().HasMaxLength(50);
            b.Property(d => d.IsActived).HasComment("是否启用");

            //b.HasMany(d => d.ConsultingRooms).WithOne().HasForeignKey(c => c.DeptID).HasConstraintName("FK_Dept_ConsultingRoom");
        });
        //执行科室规则字典
        builder.Entity<ExecuteDepRuleDic>(b =>
        {
            b.ToTable(options.TablePrefix + "ExecuteDepRuleDic", options.Schema);
            b.ConfigureByConvention();
        });

        #endregion

        #region 诊室表
        builder.Entity<ConsultingRoom>(b =>
        {
            b.HasComment("诊室表");
            b.ToTable(options.TablePrefix + "ConsultingRoom", options.Schema);

            b.ConfigureByConvention();

            b.Property(c => c.Name).HasComment("名称").IsRequired().HasMaxLength(50);
            b.Property(c => c.Code).HasComment("编码").IsRequired().HasMaxLength(50);
            b.Property(c => c.IP).HasComment("IP");
            b.Property(c => c.IsActived).HasComment("是否启用");
            //b.Property(c => c.DeptID).HasComment("科室ID");
        });
        #endregion
        #region 医生表
        builder.Entity<Doctor>(b =>
        {
            b.HasComment("医生表");
            b.ToTable(options.TablePrefix + "Doctor", options.Schema);
            b.ConfigureByConvention();
        });
        #endregion

        #region 字典多类型表
        builder.Entity<DictionariesMultitype>(b =>
        {
            b.HasComment("字典多类型表");
            b.ToTable(options.TablePrefix + "DictionariesMultitype", options.Schema);
            b.ConfigureByConvention();
        });
        #endregion

        #region 检验单信息
        // 诊疗项目字典
        builder.Entity<LabReportInfo>(t =>
        {
            t.ToTable(options.TablePrefix + "LabReportInfo", options.Schema);

            t.ConfigureByConvention();

            t.HasComment("检验单信息");

            t.HasIndex(x => x.Id);
        });

        #endregion

        //检查树形结构
        builder.Entity<ExamTree>(t =>
        {
            t.ToTable(options.TablePrefix + "ExamTree", options.Schema);

            t.ConfigureByConvention();

            t.HasComment("检查树形结构表格");

            t.HasIndex(x => x.Id);
        });

        //检查树形结构
        builder.Entity<LabTree>(t =>
        {
            t.ToTable(options.TablePrefix + "LabTree", options.Schema);

            t.ConfigureByConvention();

            t.HasComment("检验树形结构表格");

            t.HasIndex(x => x.Id);
        });

        builder.Entity<NursingRecipeType>(t =>
        {
            t.ToTable(options.TablePrefix + "NursingRecipeType", options.Schema);

            t.ConfigureByConvention();

            t.HasComment("护士医嘱类别");

            t.HasIndex(x => x.Id);
        });
    }


    public static void ConfigureHISData(this ModelBuilder builder)
    {
        #region HIS药品列表

        builder.Entity<HISMedicine>(m =>
        {
            m.ToView("V_Emergency_DrugStockQuery");
            // m.ConfigureByConvention();
            m.HasNoKey();
            m.HasComment("HIS药品列表");
        });

        #endregion 
    }
}