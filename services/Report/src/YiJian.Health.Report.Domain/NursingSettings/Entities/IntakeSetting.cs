using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingDocuments.Entities
{
    /// <summary>
    /// 入量出量配置
    /// </summary>
    [Comment("入量出量配置")]
    public class IntakeSetting : FullAuditedAggregateRoot<Guid>
    {
        private IntakeSetting()
        {

        }

        /// <summary>
        /// 入量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="intakeType"></param>
        /// <param name="code"></param>
        /// <param name="content"></param>
        /// <param name="inputType"></param>
        /// <param name="inputMode"></param>
        /// <param name="unit"></param>
        /// <param name="defaultInputMode"></param>
        /// <param name="defaultUnit"></param>
        /// <param name="isEnabled"></param>
        /// <param name="sort"></param>
        public IntakeSetting(
            Guid id,
            EIntakeType intakeType,
            [NotNull] string code,
            [NotNull] string content,
            InputTypeEnum inputType,
            string inputMode,
            string unit,
            string defaultInputMode,
            string defaultUnit,
            bool isEnabled,
            int sort = 0)
        {
            Id = id;
            IntakeType = intakeType;
            Code = Check.NotNullOrEmpty(code, nameof(code), maxLength: 50);
            Content = Check.NotNullOrEmpty(content, nameof(code), maxLength: 50);
            InputType = inputType;
            InputMode = inputMode;
            Unit = unit;
            DefaultUnit = defaultUnit;
            DefaultInputMode = defaultInputMode;
            Sort = sort;
            IsEnabled = isEnabled;
        }

        /// <summary>
        /// 出量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="intakeType"></param>
        /// <param name="code"></param>
        /// <param name="content"></param>
        /// <param name="inputType"></param>
        /// <param name="color"></param>
        /// <param name="traits"></param>
        /// <param name="unit"></param>
        /// <param name="defaultColor"></param>
        /// <param name="defaultTraits"></param>
        /// <param name="defaultUnit"></param>
        /// <param name="isEnabled"></param>
        /// <param name="sort"></param>
        public IntakeSetting(
            Guid id,
            EIntakeType intakeType,
            [NotNull] string code,
            [NotNull] string content,
            InputTypeEnum inputType,
            string color,
            string traits,
            string unit,
            string defaultColor,
            string defaultTraits,
            string defaultUnit,
            bool isEnabled,
            int sort = 0)
        {
            Id = id;
            IntakeType = intakeType;
            Code = Check.NotNullOrEmpty(code, nameof(code), maxLength: 50);
            Content = Check.NotNullOrEmpty(content, nameof(code), maxLength: 50);
            InputType = inputType;
            Color = color;
            Traits = traits;
            Unit = unit;
            DefaultColor = defaultColor;
            DefaultTraits = defaultTraits;
            DefaultUnit = defaultUnit;
            Sort = sort;
            IsEnabled = isEnabled;
        }


        /// <summary>
        /// 入量出量类型（0=入量，1=出量）
        /// </summary>
        [Comment("入量出量类型（0=入量，1=出量）")]
        [Required]
        public EIntakeType IntakeType { get; set; }

        /// <summary>
        /// 出入量的代码
        /// </summary>
        [Comment("出入量的代码")]
        [Required, StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 出入量的名称
        /// </summary>
        [Comment("出入量的名称")]
        [Required, StringLength(50)]
        public string Content { get; set; }

        /// <summary>
        /// 输入类型
        /// </summary>
        [Comment("输入类型")]
        [StringLength(50)]
        public InputTypeEnum InputType { get; set; }

        /// <summary>
        /// 方式
        /// </summary>
        [Comment("方式")]
        [StringLength(500)]
        public string InputMode { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        [Comment("颜色")]
        [StringLength(500)]
        public string Color { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Comment("单位")]
        [StringLength(500)]
        public string Unit { get; set; }

        /// <summary>
        /// <summary>
        /// 性状
        /// </summary>
        [Comment("性状")]
        [StringLength(500)]
        public string Traits { get; set; }

        /// <summary>
        /// 默认方式
        /// </summary>
        [Comment("方式")]
        [StringLength(50)]
        public string DefaultInputMode { get; set; }

        /// <summary>
        /// 默认颜色
        /// </summary>
        [Comment("颜色")]
        [StringLength(50)]
        public string DefaultColor { get; set; }

        /// <summary>
        /// 默认单位
        /// </summary>
        [Comment("单位")]
        [StringLength(50)]
        public string DefaultUnit { get; set; }

        /// <summary>
        /// <summary>
        /// 默认性状
        /// </summary>
        [Comment("性状")]
        [StringLength(50)]
        public string DefaultTraits { get; set; }

        /// <summary>
        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 入量更新
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="inputMode"></param>
        /// <param name="unit"></param>
        /// <param name="defaultInputMode"></param>
        /// <param name="defaultunit"></param>
        /// <param name="isEnabled"></param>
        /// <param name="sort"></param>
        public void Update(
            InputTypeEnum inputType,
            string inputMode,
            string unit,
            string defaultInputMode,
            string defaultunit,
            bool isEnabled,
            int sort = 0)
        {
            InputType = inputType;
            InputMode = inputMode;
            Unit = unit;
            DefaultInputMode = defaultInputMode;
            DefaultUnit = defaultunit;
            Sort = sort;
            IsEnabled = isEnabled;
        }

        /// <summary>
        /// 出量更新
        /// </summary>
        /// <param name="inputMode"></param>
        /// <param name="color"></param>
        /// <param name="traits"></param>
        /// <param name="unit"></param>
        /// <param name="defaultColor"></param>
        /// <param name="defaultTraits"></param>
        /// <param name="defaultUnit"></param>
        /// <param name="isEnabled"></param>
        /// <param name="sort"></param>
        public void Update(
            InputTypeEnum inputMode,
            string color,
            string traits,
            string unit,
            string defaultColor,
            string defaultTraits,
            string defaultUnit,
            bool isEnabled,
            int sort = 0)
        {
            InputType = inputMode;
            Color = color;
            Traits = traits;
            Unit = unit;
            DefaultColor = defaultColor;
            DefaultTraits = defaultTraits;
            DefaultUnit = defaultUnit;
            IsEnabled = isEnabled;
            Sort = sort;
        }
    }

    /// <summary>
    /// 出入量输入类型
    /// </summary>
    public enum InputTypeEnum
    {
        /// <summary>
        /// 文本输入
        /// </summary>
        Text,
        /// <summary>
        /// 单选
        /// </summary>
        Radio,
        /// <summary>
        /// 多选
        /// </summary>
        Multi,

    }
}
