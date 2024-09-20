using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace YiJian.Health.Report.Enums
{
    /// <summary>
    /// 入量出量类型（0=入量，1=出量）
    /// </summary>
    public enum EIntakeType:int
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("入量")]
        In = 0,

        /// <summary>
        /// 出量
        /// </summary>
        [Description("出量")]
        Out = 1,

    }
}
