using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.Health.Report.Enums;


namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 入量出量
    /// </summary> 
    public class IntakeDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 入量出量类型（0=入量，1=出量）
        /// </summary> 
        [Required]
        public EIntakeType IntakeType { get; set; }

        /// <summary>
        /// 方式
        /// </summary>
        [StringLength(50)]
        public string InputMode { get; set; }

        /// <summary>
        /// 出入量的代码
        /// </summary>
        [Required, StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 内容
        /// </summary> 
        [Required, StringLength(50)]
        public string Content { get; set; }

        /// <summary>
        /// 内容拼接单位
        /// </summary>
        public string ContentUnit { get; set; } = string.Empty;

        /// <summary>
        /// 量
        /// </summary> 
        [Required, StringLength(20)]
        public string Quantity { get; set; }

        /// <summary>
        /// 医嘱药品剂量
        /// </summary>
        public string RecipeQty { get; set; }

        /// <summary>
        /// 单位
        /// </summary> 
        [Required, StringLength(20)]
        public string Unit { get; set; }


        /// <summary>
        /// 单位编码
        /// </summary> 
        [Required, StringLength(20)]
        public string UnitCode { get; set; }

        /// <summary>
        /// 性状编码
        /// </summary> 
        [StringLength(20)]
        public string TraitsCode { get; set; }

        /// <summary>
        /// 性状
        /// </summary> 
        [StringLength(20)]
        public string Traits { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        [StringLength(20)]
        public string Color { get; set; }

        /// <summary>
        /// 来源(0：护理单录入,1：医嘱导入)
        /// </summary>
        public int Source { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        public Guid NursingRecordId { get; set; }

        /// <summary>
        /// 执行单Id
        /// </summary>
        public Guid RecipeExecId { get; set; }

        /// <summary>
        /// 医嘱Id
        /// </summary>
        public Guid RecipeId { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary>
        public string RecipeNo { get; set; }

        /// <summary>
        /// 护理记录时间
        /// </summary>
        public DateTime? RecordTime { get; set; }
    }
}


