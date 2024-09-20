using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 叫号模式
    /// </summary>
    [Flags]
    public enum CallMode : int
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("未定义")]
        None = 0,

        /// <summary>
        /// 诊室固定
        /// </summary>
        [Description("诊室固定")]
        ConsultingRoomRegular = 1,

        /// <summary>
        /// 医生变动
        /// </summary>
        [Description("医生变动")]
        DoctorRegular = 2,
    }
}
