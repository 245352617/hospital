using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.Health.Report.Enums
{
    /// <summary>
    /// 报表时间维度
    /// </summary>
    public enum EVeidoo : int
    {
        /// <summary>
        /// 月度
        /// </summary>
        [Description("月度")]
        Month = 0,

        /// <summary>
        /// 季度
        /// </summary>
        [Description("季度")]
        Quarter = 1,

        /// <summary>
        /// 年度
        /// </summary> 
        [Description("年度")]
        Year = 2,

    }
}
