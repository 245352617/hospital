using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Validation;

namespace YiJian.Handover
{
    public class ShiftHandoverSettingCreationOrUpdate
    {
        /// <summary>
        /// 主键id,有修改，无则新增
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！")]
        [Display(Name = "类别编码")]
        [MaxLength(50)]
        public string CategoryCode { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！")]
        [Display(Name = "类别名称")]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！")]
        [Display(Name = "班次名称")]
        [MaxLength(100)]
        public string ShiftName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！")]
        [Display(Name = "开始时间")]
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！")]
        [Display(Name = "结束时间")]
        public string EndTime { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 匹配颜色
        /// </summary>
        public string MatchingColor { get; set; }

        // /// <summary>
        // /// 排序
        // /// </summary>
        // public int Sort { get; set; }

        /// <summary>
        /// 类型，医生1，护士0
        /// </summary>
        public int Type { get; set; } = 0;
    }
}