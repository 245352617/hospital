namespace YiJian.Recipe
{
    using System;
    using YiJian.ECIS.ShareModel.Models;

    /// <summary>
    /// 会诊纪要医生 分页排序输入
    /// </summary>
    [Serializable]
    public class GetDoctorSummaryPagedInput : PageBase
    {
        /// <summary>
        /// 过滤条件.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 排序字段.
        /// </summary>
        public string Sorting { get; set; }
    }
}
