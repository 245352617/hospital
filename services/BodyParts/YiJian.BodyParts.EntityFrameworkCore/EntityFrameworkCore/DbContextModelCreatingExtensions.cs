using System;
using YiJian.BodyParts.Model;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace YiJian.BodyParts.EntityFrameworkCore
{
    public static class DbContextModelCreatingExtensions
    {
        public static void Configure(
            this ModelBuilder builder,
            Action<ModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ModelBuilderConfigurationOptions();

            optionsAction?.Invoke(options);

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


            builder.Entity<Dict>(entity =>
            {
                entity.HasComment("字典-参数字典");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(20)
                    .HasComment("科室代码");

                entity.Property(e => e.DictCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("字典代码");

                entity.Property(e => e.DictDesc).HasMaxLength(200);

                entity.Property(e => e.DictStandard)
                    .HasMaxLength(20)
                    .HasComment("字典标准（国标、自定义）");

                entity.Property(e => e.DictValue)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasComment("字典值");

                entity.Property(e => e.HisCode)
                    .HasMaxLength(20)
                    .HasComment("HIS对照代码");

                entity.Property(e => e.HisName)
                    .HasMaxLength(40)
                    .HasComment("HIS对照");

                entity.Property(e => e.IsDefault).HasComment("是否默认");

                entity.Property(e => e.IsEnable).HasComment("是否启用");

                entity.Property(e => e.ModuleCode)
                    .HasMaxLength(20)
                    .HasComment("模块代码");

                entity.Property(e => e.ParaCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("参数代码");

                entity.Property(e => e.ParaName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasComment("参数名称");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(20)
                    .HasComment("上级代码");

                entity.Property(e => e.SortNum).HasComment("排序");

                entity.Property(e => e.ValidState).HasComment("是否有效（1-是，0-否）");

                entity.HasIndex(e => e.ModuleCode).HasName("Index_ModuleCode");
                entity.HasIndex(e => e.DeptCode).HasName("Index_DeptCode");
                entity.HasIndex(e => e.ParaCode).HasName("Index_ParaCode");
                entity.HasIndex(e => e.ValidState).HasName("Index_ValidState");
            });

            builder.Entity<DictCanulaPart>(entity =>
            {
                entity.HasComment("人体图-编号字典");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DeptCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("科室代码");

                entity.Property(e => e.IsEnable).HasComment("是否可用");

                entity.Property(e => e.IsDeleted).HasComment("是否删除");

                entity.Property(e => e.ModuleCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("模块代码");

                entity.Property(e => e.PartName)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasComment("部位名称");

                entity.Property(e => e.PartNumber)
                    .HasMaxLength(20)
                    .HasComment("部位编号");

                entity.Property(e => e.SortNum).HasComment("排序");
                entity.Property(e => e.RiskLevel).HasComment("风险级别 默认空，1低危 2中危 3高危  皮肤分期 默认空  1-1期 2-2期 3-3期 4-4期 5-深部组织损伤 6-不可分期");
            });

            //builder.Entity<IcuCanula>(entity =>
            //{
            //    entity.HasComment("导管护理记录信息");

            //    entity.Property(e => e.Id)
            //        .HasComment("主键")
            //        .ValueGeneratedNever();

            //    entity.Property(e => e.CanulaId).HasComment("导管主表主键");

            //    entity.Property(e => e.CanulaLength)
            //        .HasMaxLength(20)
            //        .HasComment("置管长度");

            //    entity.Property(e => e.CanulaWay)
            //        .HasMaxLength(10)
            //        .HasComment("置入方式");

            //    entity.Property(e => e.NurseId)
            //        .HasMaxLength(10)
            //        .HasComment("护士Id");

            //    entity.Property(e => e.NurseName)
            //        .HasMaxLength(20)
            //        .HasComment("护士名称");

            //    entity.Property(e => e.NurseTime).HasComment("护理时间");

            //    entity.HasIndex(e => e.NurseTime).HasName("Index_NurseTime");
            //    entity.HasIndex(e => e.CanulaId).HasName("Index_CanulaId");
            //});

            builder.Entity<IcuDeptSchedule>(entity =>
            {
                entity.HasComment("科室班次");

                entity.Property(e => e.Id)
                    .HasComment("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Days)
                    .IsRequired()
                    .HasMaxLength(4)
                    
                    .HasComment("跨天(包含天数)");

                entity.Property(e => e.DeptCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    
                    .HasComment("科室代码");

                entity.Property(e => e.EndTime)
                    .IsRequired()
                    .HasMaxLength(10)
                    
                    .HasComment("结束时间");

                entity.Property(e => e.Hours)
                    .IsRequired()
                    .HasMaxLength(10)
                    
                    .HasComment("小时数");

                entity.Property(e => e.Period)
                    .IsRequired()
                    .HasMaxLength(100)
                    
                    .HasComment("周期");

                entity.Property(e => e.ScheduleCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    
                    .HasComment("班次代码");

                entity.Property(e => e.ScheduleName)
                    .HasMaxLength(20)
                    
                    .HasComment("班次名称");

                entity.Property(e => e.StartTime)
                    .IsRequired()
                    .HasMaxLength(10)
                    
                    .HasComment("开始时间");

                entity.Property(e => e.SortNum).HasComment("排序");

                entity.Property(e => e.ScheduleTimeTypeEnum).HasDefaultValue(ScheduleTimeTypeEnum.前闭后开).HasComment("前闭后开 = 1,前开后闭 = 2");

                entity.Property(e => e.Type).HasDefaultValue(DeptScheduleTypeEnum.观察项).HasComment("班次类别，观察项：0，出入量：1 血液净化：2，ECMO：3，PICCO：4");
            });

            builder.Entity<IcuNursingSkin>(entity =>
            {
                entity.HasComment("皮肤主表");

                entity.Property(e => e.Id)
                    .HasComment("主键")
                    .ValueGeneratedNever();

                entity.Property(e => e.ExudateAmount)
                    .HasMaxLength(50)
                    .HasComment("渗出液量");

                entity.Property(e => e.ExudateColor)
                    .HasMaxLength(50)
                    .HasComment("渗出液颜色");

                entity.Property(e => e.PI_ID)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("患者id");

                entity.Property(e => e.NurseTime).HasComment("发生时间");

                entity.Property(e => e.PressArea)
                    .HasMaxLength(50)
                    .HasComment("压疮面积");

                entity.Property(e => e.PressColor)
                    .HasMaxLength(50)
                    .HasComment("伤口颜色");

                entity.Property(e => e.PressPart)
                    .HasMaxLength(50)
                    .HasComment("压疮部位");

                entity.Property(e => e.PressSmell)
                    .HasMaxLength(50)
                    .HasComment("伤口气味");

                entity.Property(e => e.PressSource)
                    .HasMaxLength(50)
                    .HasComment("压疮来源");

                entity.Property(e => e.PressStage)
                    .HasMaxLength(255)
                    .HasComment("压疮分期");

                entity.Property(e => e.ModuleCode).HasMaxLength(20).HasComment("压疮分类编码");

                entity.Property(e => e.PressType)
                    .HasMaxLength(50)
                    .HasComment("压疮类型");

                entity.Property(e => e.NurseId)
                    .HasMaxLength(50)
                    .HasComment("护士Id");

                entity.Property(e => e.NurseName)
                    .HasMaxLength(50)
                    .HasComment("护士名称");

                entity.Property(e => e.Finished).HasDefaultValue(false).HasComment("是否结束");

                entity.Property(e => e.FinishTime).HasComment("结束时间");

                entity.Property(e => e.ValidState).HasComment("有效状态（1-有效，0-无效）");
            });

            builder.Entity<IcuParaItem>(entity =>
            {
                entity.HasComment("护理项目表");

                entity.Property(e => e.Id)
                    .HasComment("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ColorParaCode)
                    .HasMaxLength(20)
                    
                    .HasComment("关联颜色");

                entity.Property(e => e.ColorParaName)
                    .HasMaxLength(255)
                    
                    .HasComment("关联颜色名称");

                entity.Property(e => e.DataSource)
                    .HasMaxLength(10)
                    
                    .HasComment("默认值");

                entity.Property(e => e.DecimalDigits)
                    .HasMaxLength(20)
                    
                    .HasComment("小数位数");

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(20)
                    
                    .HasComment("科室代码");

                entity.Property(e => e.DeviceParaCode)
                    .HasMaxLength(40)
                    
                    .HasComment("设备参数代码");

                entity.Property(e => e.DeviceParaType)
                    .HasMaxLength(10)
                    
                    .HasComment("设备参数类型");

                entity.Property(e => e.DeviceTimePoint).HasComment("采集时间点");

                entity.Property(e => e.DictFlag)
                    .HasMaxLength(1)
                    
                    .HasComment("插管部位特殊标记");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(80)
                    
                    .HasComment("显示名称");

                entity.Property(e => e.StatisticalType)
                    .HasMaxLength(20)
                    
                    .HasComment("特护单统计参数分类");

                entity.Property(e => e.DrawChartFlag).HasComment("绘制趋势图形");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(40)
                    
                    .HasComment("导管分类");

                entity.Property(e => e.GuidFlag).HasComment("是否评分");

                entity.Property(e => e.GuidId)
                    .HasMaxLength(50)
                    
                    .HasComment("评分指引编号");

                entity.Property(e => e.HealthSign).HasComment("是否生命体征项目");

                entity.Property(e => e.HighValue)
                    .HasMaxLength(20)
                    
                    .HasComment("高值");

                entity.Property(e => e.IsEnable).HasComment("是否启用");

                entity.Property(e => e.IsUsage).HasComment("是否用药所属项目");

                entity.Property(e => e.KBCode)
                    .HasColumnName("KBCode")
                    .HasMaxLength(20)
                    
                    .HasComment("知识库代码");

                entity.Property(e => e.LowValue)
                    .HasMaxLength(20)
                    
                    .HasComment("低值");

                entity.Property(e => e.MaxValue)
                    .HasMaxLength(20)
                    
                    .HasComment("最大值");

                entity.Property(e => e.MinValue)
                    .HasMaxLength(20)
                    
                    .HasComment("最小值");

                entity.Property(e => e.ModuleCode)
                    .HasMaxLength(20)
                    
                    .HasComment("模块代码");

                entity.Property(e => e.NuringViewCode)
                    .HasMaxLength(20)
                    
                    .HasComment("护理概览参数");

                entity.Property(e => e.AbnormalSign)
                    .HasMaxLength(1)
                    
                    .HasComment("是否异常体征参数");

                entity.Property(e => e.ParaCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    
                    .HasComment("项目代码");

                entity.Property(e => e.ParaName)
                    .IsRequired()
                    .HasMaxLength(80)
                    
                    .HasComment("项目名称");

                entity.Property(e => e.PropertyParaCode)
                    .HasMaxLength(20)
                    
                    .HasComment("关联性状");

                entity.Property(e => e.PropertyParaName)
                    .HasMaxLength(255)
                    
                    .HasComment("关联性状名称");

                entity.Property(e => e.ScoreCode)
                    .HasMaxLength(50)
                    
                    .HasComment("评分代码");

                entity.Property(e => e.SortNum).HasComment("排序号");

                entity.Property(e => e.Style)
                    .HasMaxLength(20)
                    
                    .HasComment("文本类型");

                entity.Property(e => e.Threshold).HasComment("是否预警");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(20)
                    
                    .HasComment("单位名称");

                entity.Property(e => e.ValidState).HasComment("有效状态");

                entity.Property(e => e.ValueType)
                    .HasMaxLength(20)
                    
                    .HasComment("数据类型");

                entity.Property(e => e.AddSource)
                    .HasMaxLength(20)
                    
                    .HasComment("添加来源");

                entity.Property(e => e.ParaItemType).HasDefaultValue(DeviceTypeEnum.其它).HasComment("项目参数类型，用于区分监护仪： 3，呼吸机：2等");

                entity.HasIndex(e => e.ModuleCode).HasName("Index_ModuleCode");
                entity.HasIndex(e => e.DeptCode).HasName("Index_DeptCode");
                entity.HasIndex(e => e.ParaCode).HasName("Index_ParaCode");
                entity.HasIndex(e => e.ValidState).HasName("Index_ValidState");
            });

            builder.Entity<IcuParaModule>(entity =>
            {
                entity.HasComment("模块参数");

                entity.Property(e => e.Id)
                    .HasComment("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeptCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    
                    .HasComment("科室代码");

                entity.Property(e => e.Enname)
                    .HasMaxLength(40)
                    
                    .HasComment("拼音");

                entity.Property(e => e.IsEnable).HasComment("是否启用");

                entity.Property(e => e.ModuleCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    
                    .HasComment("模块代码");

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasMaxLength(80)
                    
                    .HasComment("模块名称");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(80)
                    
                    .HasComment("模块显示名称");

                entity.Property(e => e.ModuleType)
                    .HasMaxLength(20)
                    
                    .HasComment("模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）");

                entity.Property(e => e.IsBloodflow).HasDefaultValue(0).HasComment("是否血流内导管");

                entity.Property(e => e.SortNum).HasComment("排序");

                entity.Property(e => e.ValidState).HasComment("是否有效(1-有效，0-无效)");
                entity.Property(e => e.RiskLevel).HasComment("风险级别 默认空，1低危 2中危 3高危");
                entity.Property(e => e.PartNumber).HasComment("部位编号");

                entity.HasIndex(e => e.ModuleCode).HasName("Index_ModuleCode");
                entity.HasIndex(e => e.DeptCode).HasName("Index_DeptCode");
                entity.HasIndex(e => e.ValidState).HasName("Index_ValidState");
            });

            builder.Entity<IcuSkin>(entity =>
            {
                entity.HasComment("皮肤详细信息记录表");

                entity.Property(e => e.Id)
                    .HasComment("主键")
                    .ValueGeneratedNever();

                entity.Property(e => e.ExudateAmount)
                    .HasMaxLength(50)
                    .HasComment("渗出液量");

                entity.Property(e => e.ExudateColor)
                    .HasMaxLength(50)
                    .HasComment("渗出液颜色");

                entity.Property(e => e.NurseTime).HasComment("护理时间");

                entity.Property(e => e.NursingMeasure)
                    .HasMaxLength(255)
                    .HasComment("护理措施");

                entity.Property(e => e.PressArea)
                    .HasMaxLength(50)
                    .HasComment("压疮面积");

                entity.Property(e => e.PressColor)
                    .HasMaxLength(50)
                    .HasComment("伤口颜色");

                entity.Property(e => e.PressSmell)
                    .HasMaxLength(50)
                    .HasComment("伤口气味");

                entity.Property(e => e.PressStage)
                    .HasMaxLength(255)
                    .HasComment("压疮分期");

                entity.Property(e => e.SkinId).HasComment("压疮Id");

                entity.Property(e => e.NurseId)
                    .HasMaxLength(50)
                    .HasComment("护士Id");

                entity.Property(e => e.NurseName)
                    .HasMaxLength(50)
                    .HasComment("护士名称");
            });

            builder.Entity<SkinDynamic>(entity =>
            {
                entity.HasComment("人体图动态表");

                entity.Property(e => e.Id)
                    .HasComment("主键")
                    .ValueGeneratedNever();

                entity.Property(e => e.CanulaId).HasComment("主键");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(40)
                    .HasComment("皮肤分类");

                entity.Property(e => e.ParaCode)
                    .HasMaxLength(40)
                    .HasComment("参数代码");

                entity.Property(e => e.ParaName)
                    .HasMaxLength(255)
                    .HasComment("参数名称");

                entity.Property(e => e.ParaValue)
                    .HasMaxLength(255)
                    .HasComment("参数值");
            });

            builder.Entity<DictSource>(entity =>
            {
                entity.HasComment("字典-基础字典设置表");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ParaType).HasComment("参数类型(S-系统参数，D-科室参数)");

                entity.Property(e => e.DeptCode).HasComment("科室代码");

                entity.Property(e => e.ModuleName).HasComment("模板名称");

                entity.Property(e => e.Pid).HasComment("父级Id");

                entity.Property(e => e.ParaCode).HasComment("参数代码");

                entity.Property(e => e.ParaName).HasComment("参数名称");

                entity.Property(e => e.ParaValue).HasComment("参数值");

                entity.Property(e => e.IsEnable).HasComment("是否启用");

                entity.Property(e => e.SortNum).HasComment("排序");
            });

            builder.Entity<IcuSysPara>(entity =>
            {
                entity.HasComment("系统-参数设置表");

                entity.Property(e => e.Id)
                    .HasComment("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(20)
                    
                    .HasComment("科室代码");

                entity.Property(e => e.ModuleName)
                    .HasMaxLength(1000)
                    
                    .HasComment("模板名称");

                entity.Property(e => e.ParaCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    
                    .HasComment("参数代码");

                entity.Property(e => e.ParaName)
                    .IsRequired()
                    .HasMaxLength(1000)
                    
                    .HasComment("参数名称");

                entity.Property(e => e.ParaType)
                    .IsRequired()
                    .HasMaxLength(10)
                    
                    .HasComment("参数类型(S-系统参数，D-科室参数)");

                entity.Property(e => e.Type)
                    .HasComment("类型（系统参数 = 1,特护单参数 = 2）");

                entity.Property(e => e.ParaValue).HasComment("参数值");

                entity.Property(e => e.ValueType).HasDefaultValue(1).HasComment("值类型");
                entity.Property(e => e.DataSource).HasComment("数据源");
                entity.Property(e => e.ModuleSort).HasDefaultValue(1).HasComment("模块排序号");
                entity.Property(e => e.SortNum).HasDefaultValue(1).HasComment("排序号");
                entity.Property(e => e.IsAddiable).HasDefaultValue(false).HasComment("表格模式下是否可添加");
                entity.Property(e => e.Desc).HasComment("描述");

                entity.HasIndex(e => new { e.DeptCode, e.ParaCode })
                    .HasName("Unique_ParaCode")
                    .IsUnique();
            });

            builder.Entity<IcuNursingEvent>(entity =>
            {
                entity.HasComment("护理记录表");

                entity.Property(e => e.Id)
                    .HasComment("主键")
                    .ValueGeneratedNever();

                entity.Property(e => e.EventType).HasDefaultValue(EventTypeEnum.护理记录).HasComment("事件类型");

                entity.Property(e => e.AuditNurseCode)
                    .HasMaxLength(20)
                    .HasComment("审核人");

                entity.Property(e => e.AuditNurseName)
                    .HasMaxLength(20)
                    .HasComment("审核人名称");

                entity.Property(e => e.AuditState).HasComment("审核状态（0-未审核，1，已审核，2-取消审核）");

                entity.Property(e => e.AuditTime).HasComment("审核时间");

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .HasComment("内容");

                entity.Property(e => e.SkinDescription).HasComment("皮肤情况描述");

                entity.Property(e => e.Measure).HasComment("处理措施");

                entity.Property(e => e.PI_ID)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("患者id");

                entity.Property(e => e.NurseCode)
                    .HasMaxLength(20)
                    .HasComment("护士工号");

                entity.Property(e => e.NurseDate).HasComment("护理日期");

                entity.Property(e => e.NurseName)
                    .HasMaxLength(20)
                    .HasComment("护士名称");

                entity.Property(e => e.NurseTime).HasComment("护理时间");

                entity.Property(e => e.RecordTime).HasComment("记录时间");

                entity.Property(e => e.SignatureId).HasComment("签名记录编号对应icu_signature的id");
                entity.Property(e => e.AuditSignatureId).HasComment("审核者签名");

                entity.Property(e => e.SortNum).HasComment("排序");

                entity.Property(e => e.ValidState).HasComment("有效状态（1-有效，0-无效）");

                entity.HasIndex(e => e.PI_ID).HasName("Index_PI_ID");
                entity.HasIndex(e => e.NurseTime).HasName("Index_NurseTime");
                entity.HasIndex(e => e.ValidState).HasName("Index_ValidState");
            });

            builder.Entity<IcuSignature>(entity =>
            {
                entity.HasComment("签名");

                entity.Property(e => e.Id)
                    .HasComment("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PI_ID)
                    .HasMaxLength(50)
                    
                    .HasComment("患者id");

                entity.Property(e => e.NurseTime).HasComment("护理时间");

                entity.Property(e => e.SignNurseCode)
                    .HasMaxLength(20)
                    
                    .HasComment("签名工号");

                entity.Property(e => e.SignNurseCode2)
                    .HasMaxLength(20)
                    
                    .HasComment("二级签名工号");

                entity.Property(e => e.SignNurseCode3)
                    .HasMaxLength(20)
                    
                    .HasComment("三级签名工号");

                entity.Property(e => e.SignNurseName)
                    .HasMaxLength(20)
                    .HasComment("签名名称");

                entity.Property(e => e.SignNurseName2)
                    .HasMaxLength(40)
                    .HasComment("二级签名名称");

                entity.Property(e => e.SignNurseName3)
                    .HasMaxLength(20)
                    
                    .HasComment("三级签名名称");

                entity.Property(e => e.SignState)
                    .HasComment("签名标志");

                entity.Property(e => e.SignImage)
                    
                    .HasComment("签名图片");

                entity.Property(e => e.SignState2)
                    .HasComment("二级签名标志");

                entity.Property(e => e.SignImage2)
                    
                    .HasComment("二级签名图片");

                entity.Property(e => e.SignState3)
                    .HasComment("三级签名标志");

                entity.Property(e => e.SignTime)
                    .HasColumnType("datetime")
                    .HasComment("签名时间");

                entity.Property(e => e.SignTime2)
                    .HasColumnType("datetime")
                    .HasComment("二级签名时间");

                entity.Property(e => e.SignTime3)
                    .HasColumnType("datetime")
                    .HasComment("三级签名时间");

                entity.Property(e => e.SignImage3)
                    
                    .HasComment("三级签名图片");

                entity.Property(e => e.ReviewState)
                    
                    .HasComment("特护单审核状态（S:已审核）");

                entity.Property(e => e.IsDeleted).HasComment("是否有效（1-是，0-否）");
            });

            builder.Entity<FileRecord>(entity =>
            {
                entity.HasComment("文件表");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FileName).HasComment("文件名称");

                entity.Property(e => e.FileSuffix).HasComment("文件后缀（类型）");

                entity.Property(e => e.BucketName).HasComment("桶名称，如果文件同时在多个桶中，则英文逗号相隔");

                entity.Property(e => e.UploadTime).HasComment("上传时间");

                entity.Property(e => e.DeptCode).HasComment("科室代码");

                entity.Property(e => e.PI_ID).HasComment("患者ID");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.DelTime).HasComment("删除时间");

                entity.Property(e => e.Size).HasComment("文件大小");

                entity.Property(e => e.ModuleType).HasComment("regulation = ，literatur = 文献");
            });

            builder.Entity<IcuPhrase>(entity =>
            {
                entity.HasComment("常用语模板");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    
                    .HasComment("科室代码");

                entity.Property(e => e.StaffCode)
                    .HasMaxLength(20)
                    
                    .HasComment("员工代码");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(10)
                    
                    .HasComment("上级编号");

                entity.Property(e => e.PhraseText)
                    .HasMaxLength(4000)
                    
                    .HasComment("模板内容");

                entity.Property(e => e.TypeCode)
                    .HasMaxLength(10)
                    
                    .HasComment("类型代码");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    
                    .HasComment("类型名称");

                entity.Property(e => e.SortNum).HasComment("排序");
                entity.Property(e => e.ValidState).HasComment("是否有效(1-有效，0-无效)");
            });
        }
    }
}