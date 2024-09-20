using System.Collections.Generic;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 描述：注射卡打印数据
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:24:46
    /// </summary>
    public class InjectionPrintDto
    {
        /// <summary>
        /// 科室
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 执行日期
        /// </summary>
        public string ExcuteDate { get; set; }

        /// <summary>
        /// 打印日期
        /// </summary>
        public string PrintDate { get; set; }

        /// <summary>
        /// 注射卡详情
        /// </summary>
        public List<InjectionDetail> InjectionDetails { get; set; }
    }

    /// <summary>
    /// 注射卡详情
    /// </summary>
    public class InjectionDetail
    {
        /// <summary>
        /// 床号
        /// </summary>
        public string Bed { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 医嘱项目
        /// </summary>
        public string RecipeName { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public string DosageQty { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 用法
        /// </summary>
        public string UsageName { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public string ExecuteTime { get; set; }

        /// <summary>
        /// 执行人
        /// </summary>
        public string ExecuteName { get; set; }

        /// <summary>
        /// 核对时间
        /// </summary>
        public string CheckTime { get; set; }

        /// <summary>
        /// 核对人
        /// </summary>
        public string CheckName { get; set; }
    }
}
