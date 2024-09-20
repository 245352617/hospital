using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 就诊状态
    /// </summary>
    [Description("就诊状态")]
    public enum ETreatStatus : int
    {
        /// <summary>
        /// 未就诊
        /// </summary>
        [Description("未就诊")]
        UnTreated = 0,

        /// <summary>
        /// 已就诊
        /// </summary>
        [Description("已就诊")]
        Treated = 1,

        /// <summary>
        /// 过号
        /// </summary>
        [Description("已过号")]
        UnTreatedOver = 2,
    }
}
