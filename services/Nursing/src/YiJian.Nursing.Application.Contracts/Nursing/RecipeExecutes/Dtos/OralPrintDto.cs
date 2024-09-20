using System.Collections.Generic;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 描述：口服卡打印数据
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:35:16
    /// </summary>
    public class OralPrintDto
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
        /// 口服卡详情
        /// </summary>
        public List<OralDetail> OralDetails { get; set; }
    }

    /// <summary>
    /// 口服卡详情
    /// </summary>
    public class OralDetail
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
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 用法途径
        /// </summary>
        public string UsageName { get; set; }

        /// <summary>
        /// 给药时间
        /// </summary>
        public string FrequencyExecDayTimes { get; set; }
    }
}
