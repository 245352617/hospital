using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 叫号查询状态
    /// -1：所有状态；
    /// 1：候诊中；
    /// 2：已就诊；
    /// 5：已过号；
    /// </summary>
    [Description("叫号查询状态")]
    public enum ECallingQueryStatus : int
    {
        [Description("所有状态")]
        All = -1,

        [Description("候诊中")]
        Waitting = 1,

        [Description("已过号")]
        UntreatedOver = 2,

        [Description("已就诊")]
        Treated = 5,
    }
}
