using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;


namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 药理
    /// </summary> 
    public class ToxicDto : EntityDto<Guid>
    {
        /// <summary>
        /// 皮试药
        /// </summary> 
        public bool? IsSkinTest { get; set; }

        /// <summary>
        /// 复方药
        /// </summary> 
        public bool? IsCompound { get; set; }

        /// <summary>
        /// 麻醉药
        /// </summary> 
        public bool? IsDrunk { get; set; }

        /// <summary>
        /// 精神药  0非精神药,1一类精神药,2二类精神药
        /// </summary> 
        [StringLength(20)]
        public int? ToxicLevel { get; set; }

        /// <summary>
        /// 高危药
        /// </summary> 
        public bool? IsHighRisk { get; set; }

        /// <summary>
        /// 冷藏药
        /// </summary> 
        public bool? IsRefrigerated { get; set; }

        /// <summary>
        /// 肿瘤药
        /// </summary> 
        public bool? IsTumour { get; set; }

        /// <summary>
        /// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
        /// </summary> 
        public int? AntibioticLevel { get; set; }

        /// <summary>
        /// 贵重药
        /// </summary> 
        public bool? IsPrecious { get; set; }

        /// <summary>
        /// 胰岛素
        /// </summary> 
        public bool? IsInsulin { get; set; }

        /// <summary>
        /// 兴奋剂
        /// </summary> 
        public bool? IsAnaleptic { get; set; }

        /// <summary>
        /// 试敏药
        /// </summary> 
        public bool? IsAllergyTest { get; set; }

        /// <summary>
        /// 限制性用药标识
        /// </summary> 
        public bool? IsLimited { get; set; }

        /// <summary>
        /// 限制性用药描述
        /// </summary> 
        [StringLength(200)]
        public string LimitedNote { get; set; }

        /// <summary>
        /// 药方Id
        /// </summary>
        public Guid? PrescribeId { get; set; }
    }
}
