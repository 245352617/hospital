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
    public enum TriageStatus : int
    {
        /// <summary>
        /// 暂存
        /// </summary>
        [Description("暂存")]
        Suspend = 0,

        /// <summary>
        /// 分诊
        /// </summary>
        [Description("分诊")]
        Triage = 1,
    }
}
