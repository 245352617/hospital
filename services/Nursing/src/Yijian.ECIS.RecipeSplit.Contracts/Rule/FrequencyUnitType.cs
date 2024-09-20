using System;
using System.Collections.Generic;
using System.Text;

namespace Yijian.ECIS.RecipeSplit.Contracts
{
    /// <summary>
    /// 频次类型分类
    /// </summary>
    public enum FrequencyUnitType : byte
    {
        /// <summary>
        /// 空
        /// </summary>
        Null,

        /// <summary>
        /// 立刻，立即执行
        /// </summary>
        ST,

        /// <summary>
        /// 每小时
        /// </summary>
        Hour,

        /// <summary>
        /// N小时为周期，目前暂无相关code对应
        /// </summary>
        NHour,

        /// <summary>
        /// 每天
        /// </summary>
        Daily,

        /// <summary>
        /// N天为周期
        /// </summary>
        NDay,

        /// <summary>
        /// 每周
        /// </summary>
        Weekly,

        /// <summary>
        /// N周为周期
        /// </summary>
        NWeek
    }
}
