using System.Collections.Generic;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 描述：输液巡视卡打印数据
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:38:11
    /// </summary>
    public class InfusionPrintDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string Bed { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 执行日期
        /// </summary>
        public string ExcuteDate { get; set; }

        /// <summary>
        /// 输液详情列表
        /// </summary>
        public List<InfusionDetail> InfusionDetails { get; set; }
    }

    /// <summary>
    /// 输液详情列表
    /// </summary>
    public class InfusionDetail
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string RecipeName { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public string DosageQty { get; set; }

        /// <summary>
        /// 用法(频次)
        /// </summary>
        public string FrequencyName { get; set; }

        /// <summary>
        /// 滴速
        /// </summary>
        public string Speed { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public string ExecuteTime { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string ExecuteName { get; set; }
    }
}
