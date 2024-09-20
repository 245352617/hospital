using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace YiJian.Health.Report.Enums
{
    /// <summary>
    /// 瞳孔评估(0=左眼，1=右眼)
    /// </summary>
    public enum EPupilType : int
    {
        /// <summary>
        /// 左眼
        /// </summary>
        [Description("左眼")] 
        Left = 0,

        /// <summary>
        /// 右眼
        /// </summary>
        [Description("右眼")] 
        Right = 1,

    }
}
