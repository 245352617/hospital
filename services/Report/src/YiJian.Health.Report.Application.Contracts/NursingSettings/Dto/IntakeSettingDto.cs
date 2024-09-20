using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingSettings.Dto
{
    /// <summary>
    /// 新增/更新出入量配置
    /// </summary>
    public class IntakeSettingDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 入量出量类型（0=入量，1=出量）
        /// </summary>
        [Required]
        public EIntakeType IntakeType { get; set; }

        /// <summary>
        /// 出入量的代码
        /// </summary>
        [Required, StringLength(50, ErrorMessage = "编码需在50字内")]
        public string Code { get; set; }

        /// <summary>
        /// 出入量的名称
        /// </summary>
        [Required, StringLength(50, ErrorMessage = "名称需在50字内")]
        public string Content { get; set; }

        /// <summary>
        /// 输入类型 (0:文本输入 1：单选 2：多选)
        /// </summary>
        public int InputType { get; set; }

        /// <summary>
        /// 入量-方式（数组形式：前端用，|分割）
        /// </summary>
        public string InputMode { get; set; }

        /// <summary>
        /// 出量-颜色（数组形式：前端用，|分割）
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 出量-性状（数组形式：前端用，|分割）
        /// </summary>
        public string Traits { get; set; }

        /// <summary>
        /// 单位（数组形式：前端用，|分割）
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 默认入量-方式
        /// </summary>
        public string DefaultInputMode { get; set; }

        /// <summary>
        /// 默认出量-颜色
        /// </summary>
        public string DefaultColor { get; set; }

        /// <summary>
        /// 默认出量-性状
        /// </summary>
        public string DefaultTraits { get; set; }

        /// <summary>
        /// 默认单位
        /// </summary>
        public string DefaultUnit { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

    }
}
