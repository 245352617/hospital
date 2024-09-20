using System;
using YiJian.Nursing.Recipes.Dtos;

namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 描述：一键执行和核对列表返回Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/9 12:01:14
    /// </summary>
    public class CheckExecDto : BaseRequestDto
    {
        /// <summary>
        /// 医嘱号
        /// </summary>
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱子号
        /// </summary>
        public int RecipeGroupNo { get; set; }

        /// <summary>
        /// 开立时间(开嘱时间)
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public DateTime PlanExcuteTime { get; set; }

        /// <summary>
        /// 执行单状态
        /// </summary>
        public int ExecuteStatus { get; set; }

        /// <summary>
        /// 执行单状态
        /// </summary>
        public string ExecuteStatusText { get; set; }

        /// <summary>
        ///  医嘱项目分类编码 
        /// </summary> 
        public string CategoryCode { get; set; }

        /// <summary>
        ///  医嘱项目分类名称
        /// </summary> 
        public string CategoryName { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary> 
        public string Name { get; set; }

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
        /// 频次码
        /// </summary> 
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次名称
        /// </summary> 
        public string FrequencyName { get; set; }

        /// <summary>
        /// 备用量
        /// </summary>
        public decimal ReserveDosage { get; set; }

        /// <summary>
        /// 总执行量
        /// </summary>
        public decimal TotalExecDosage { get; set; }

        /// <summary>
        /// 总余量
        /// </summary>
        public decimal TotalRemainDosage { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string Unit { get; set; }
    }
}
