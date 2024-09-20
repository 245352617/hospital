using System;

namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 描    述 ：医嘱Dto
    /// 创 建 人 ：yangkai
    /// 创建时间 ：2023/6/16 9:32:02
    /// </summary>
    public class RecipeDto
    {
        /// <summary>
        /// 医嘱主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary>
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱编码
        /// </summary> 
        public string Code { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string AliasPyCode { get; set; }

        /// <summary>
        /// 包装规格（规格
        /// </summary> 
        public string Specification { get; set; }

        /// <summary>
        /// 每次剂量(剂量）
        /// </summary> 
        public decimal? DosageQty { get; set; }

        /// <summary>
        /// 每次剂量单位（单位）
        /// </summary> 
        public string DosageUnit { get; set; }

        /// <summary>
        /// 用法（途径）编码
        /// </summary> 
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法（途径）名称
        /// </summary> 
        public string UsageName { get; set; }

        /// <summary>
        /// 开嘱时间
        /// </summary>
        public DateTime ApplyTime { get; set; }
    }
}
