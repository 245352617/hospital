using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using YiJian.ECIS.Call.Domain;
using YiJian.ECIS.Call.Domain.CallConfig;
using YiJian.ECIS.Call.Domain.CallCenter;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.ECIS.Call.CallConfig;

namespace YiJian.ECIS.Call.EntityFrameworkCore
{
    public static class CallDbContextModelCreatingExtensions
    {
        public static void ConfigureCall(
            this ModelBuilder builder,
            Action<CallModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new CallModelBuilderConfigurationOptions(
                CallDbProperties.DbTablePrefix,
                CallDbProperties.DbSchema
            );

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
            #region 科室表
            builder.Entity<Department>(b =>
            {
                b.HasComment("科室表");
                b.ToTable(options.TablePrefix + "Department", options.Schema);

                b.ConfigureByConvention();

                b.Property(d => d.Name).HasComment("名称").IsRequired().HasMaxLength(DepartmentConsts.MaxNameLength);
                b.Property(c => c.Code).HasComment("编码").IsRequired().HasMaxLength(50);
                b.Property(d => d.IsActived).HasComment("是否启用");

                //b.HasMany(d => d.ConsultingRooms).WithOne().HasForeignKey(c => c.DeptID).HasConstraintName("FK_Dept_ConsultingRoom");
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

            #region 诊室固定表
            builder.Entity<ConsultingRoomRegular>(b =>
            {
                b.HasComment("诊室固定表");
                b.ToTable(options.TablePrefix + "ConsultingRoomRegular", options.Schema);

                b.ConfigureByConvention();

                b.Property(c => c.ConsultingRoomId).HasComment("诊室id").IsRequired();
                b.Property(c => c.IsActived).HasComment("是否使用");
            });
            #endregion

            #region 医生变动表
            builder.Entity<DoctorRegular>(b =>
            {
                b.HasComment("医生变动表");
                b.ToTable(options.TablePrefix + "DoctorRegular", options.Schema);

                b.Property(c => c.DoctorId).HasComment("医生id").IsRequired();
                b.Property(c => c.DoctorName).HasComment("医生名称");
                b.Property(c => c.DoctorDepartmentId).HasComment("医生所属科室id").IsRequired();
                b.Property(c => c.DoctorDepartmentName).HasComment("医生所属科室名称");
                b.Property(c => c.DepartmentId).HasComment("对应急诊科室id").IsRequired();
                b.Property(c => c.IsActived).HasComment("是否使用").IsRequired();

                b.ConfigureByConvention();
            });
            #endregion

            #region 叫号设置基础设置
            builder.Entity<BaseConfig>(b =>
            {
                b.HasComment("叫号设置-基础设置");
                b.ToTable(options.TablePrefix + "BaseConfig", options.Schema);

                b.ConfigureByConvention();

                b.Property(c => c.TomorrowCallMode).HasComment("当前叫号模式").IsRequired();
                b.Property(c => c.RegularEffectTime).HasComment("模式生效时间").HasDefaultValue(RegularEffectTime.Immediate);
                b.Property(c => c.TomorrowUpdateNoHour).HasComment("每日更新号码时间（小时）").IsRequired();
                b.Property(c => c.TomorrowUpdateNoMinute).HasComment("每日更新号码时间（分钟）").IsRequired();
                b.Property(c => c.CurrentCallMode).HasComment("当前叫号模式").IsRequired();
                b.Property(c => c.CurrentUpdateNoHour).HasComment("当前的 每日更新号码时间（小时）（0-23）").IsRequired();
                b.Property(c => c.CurrentUpdateNoMinute).HasComment("每日更新号码时间（分钟）").IsRequired();
                b.Property(c => c.FriendlyReminder).HasComment("温馨提醒（大屏叫号端）").HasMaxLength(1000);

            });
            #endregion

            #region 排队号规则
            builder.Entity<SerialNoRule>(b =>
            {
                b.HasComment("排队号规则");
                b.ToTable(options.TablePrefix + "SerialNoRule", options.Schema);

                b.ConfigureByConvention();

                b.Property(c => c.DepartmentId).HasComment("科室id").IsRequired();
                b.Property(c => c.Prefix).HasComment("开头字母").IsRequired();
                b.Property(c => c.SerialLength).HasComment("流水号位数").IsRequired().HasDefaultValue(3);
                b.Property(c => c.CurrentNo).HasComment("当前流水号").IsRequired().HasDefaultValue(0);
            });
            #endregion

            #region 叫号患者列表
            builder.Entity<CallInfo>(b =>
            {
                b.ToTable(options.TablePrefix + "CallInfo", options.Schema);
                // 从实体配置表（包括表备注、字段备注、字段默认值）
                b.ConfigureByEntity();
                //b.Property(x => x.LogDate).HasColumnType("date");

                b.ConfigureByConvention();

                // 配置表关联信息
                b.HasMany(d => d.CallingRecords).WithOne().HasForeignKey(c => c.CallInfoId).HasConstraintName("FK_CallInfo_CallingRecord");
            });
            builder.Entity<CallingRecord>(b =>
            {
                b.ToTable(options.TablePrefix + "CallingRecord", options.Schema);
                // 从实体配置表（包括表备注、字段备注、字段默认值）
                b.ConfigureByEntity();

                b.ConfigureByConvention();

                b.HasOne(d => d.CallInfo).WithMany(c => c.CallingRecords).HasForeignKey(c => c.CallInfoId).HasConstraintName("FK_CallInfo_CallingRecord");
            });
            #endregion

            #region 列表配置

            builder.Entity<RowConfig>(b =>
            {
                b.ToTable(options.TablePrefix + "RowConfig", options.Schema);
                // 从实体配置表（包括表备注、字段备注、字段默认值）
                b.ConfigureByEntity();

                b.ConfigureByConvention();

                b.Property(x => x.Order).HasColumnType("smallint");
                b.Property(x => x.DefaultOrder).HasColumnType("smallint");
                b.Property(x => x.Width).HasColumnType("smallint");
                b.Property(x => x.DefaultWidth).HasColumnType("smallint");
            });

            #endregion
        }
    }
}