using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.TriageService.Enum
{
    /// <summary>
    /// 叫号状态
    /// </summary>
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
        /// 已叫号
        /// </summary>
        [Description("已叫号")]
        Over = 2,

        /// <summary>
        /// 已退号
        /// </summary>
        [Description("已退号")]
        Refund = 3,
    }
}
