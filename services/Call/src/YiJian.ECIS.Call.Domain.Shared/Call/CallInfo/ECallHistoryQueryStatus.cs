using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 就诊状态
    /// 0 = 未挂号
    /// 1 = 待就诊
    /// 2 = 过号 （医生已经叫号）
    /// 3 = 已退号 （退挂号）
    /// 4 = 正在就诊
    /// 5 = 已就诊（就诊区患者）
    /// 6 = 出科（抢救区、留观区患者）
    /// </summary>
    [Description("叫号历史查询状态")]
    public enum EHistoryVisitStatus : int
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = -1,

        /// <summary>
        /// 未挂号
        /// </summary>
        [Description("未挂号")]
        NotRegister = 0,

        /// <summary>
        /// 过号
        /// </summary>
        [Description("过号")]
        UntreatedOver = 2,

        /// <summary>
        /// 已退号
        /// </summary>
        [Description("已退号")]
        RefundNo = 3,

        /// <summary>
        /// 已就诊
        /// </summary>
        [Description("已就诊")]
        Treated = 5,

        /// <summary>
        /// 出科
        /// </summary>
        [Description("出科")]
        OutDepartment = 6,
    }
}
