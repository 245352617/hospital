using System;
using System.ComponentModel;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 叫号状态
    /// </summary>
    [Flags]
    public enum CallStatus : int
    {
        /// <summary>
        /// 未叫号
        /// </summary>
        [Description("未叫号")]
        NotYet = 0,

        /// <summary>
        /// 叫号中
        /// </summary>
        [Description("叫号中")]
        Calling = 1,

        /// <summary>
        /// 暂停中
        /// </summary>
        [Description("暂停中")]
        Pause = 2,

        /// <summary>
        /// 已叫号/已经接诊之后的状态
        /// </summary>
        [Description("已叫号")]
        Over = 3,

        /// <summary>
        /// 已经过号
        /// </summary>
        [Description("已经过号")]
        Exceed = 4,

        /// <summary>
        /// 已退号/已经作废
        /// </summary>
        [Description("已退号/已经作废")]
        Refund = 5,

    }
}
