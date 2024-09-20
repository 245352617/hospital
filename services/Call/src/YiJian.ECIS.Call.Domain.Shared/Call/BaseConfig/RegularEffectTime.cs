using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 叫号模式生效时间
    /// </summary>
    [Flags]
    public enum RegularEffectTime : int
    {
        /// <summary>
        /// 立即生效（默认）
        /// </summary>
        [Description("立即生效")]
        Immediate = 0,

        /// <summary>
        /// 次日生效
        /// </summary>
        [Description("次日生效")]
        Tomorrow = 1,
    }
}
