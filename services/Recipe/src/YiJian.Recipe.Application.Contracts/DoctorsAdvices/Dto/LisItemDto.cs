using System;
using System.ComponentModel.DataAnnotations;
using YiJian.DoctorsAdvices.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 检验小项
    /// </summary>
    public class LisItemDto
    {
        /// <summary>
        /// 小项编码
        /// </summary> 
        [Required, StringLength(50)]
        public string TargetCode { get; set; }

        /// <summary>
        /// 小项名称
        /// </summary>  
        [Required, StringLength(200)]
        public string TargetName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Required]
        [StringLength(20)]
        public string TargetUnit { get; set; }

        /// <summary>
        /// 单价
        /// </summary> 
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary> 
        public decimal Qty { get; set; }

        /// <summary>
        /// 医保目录编码
        /// </summary>
        [StringLength(20)]
        public string InsuranceCode { get; set; }

        /// <summary>
        /// 医保目录：0-自费项目1-医保项目(甲、乙)
        /// </summary>
        public EInsuranceCatalog InsuranceType { get; set; }


        /// <summary>
        /// 项目编码
        /// </summary> 
        public string ProjectCode { get; set; }

        /// <summary>
        /// 其它价格
        /// </summary> 
        public decimal OtherPrice { get; set; }

        /// <summary>
        /// 规格
        /// </summary> 
        public string Specification { get; set; }

        /// <summary>
        /// 排序号
        /// </summary> 
        public int Sort { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary> 
        public string PyCode { get; set; }

        /// <summary>
        /// 五笔
        /// </summary> 
        public string WbCode { get; set; }

        /// <summary>
        /// 特殊标识
        /// </summary> 
        public string SpecialFlag { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary> 
        public bool IsActive { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary> 
        public string ProjectType { get; set; }

        /// <summary>
        /// 项目归类
        /// </summary>  
        public string ProjectMerge { get; set; }

        /// <summary>
        /// 医保机构编码
        /// </summary>
        public string MeducalInsuranceCode { get; set; }

        /// <summary>
        /// 医保二级编码
        /// </summary>
        public string YBInneCode { get; set; }

        /// <summary>
        /// 检验项Id
        /// </summary>
        public Guid LisId { get; set; }

    }

}
