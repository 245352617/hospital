using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace YiJian.Health.Report.Enums
{
    /// <summary>
    /// 餐前餐后(0=餐前，1=餐后)
    /// </summary>
    public enum EMealTimeType:int
    {
        /// <summary>
        /// 餐前
        /// </summary>
        [Description("前")] 
        Before = 0,

        /// <summary>
        /// 餐后
        /// </summary>
        [Description("后")]
        After = 1,
    }
}
