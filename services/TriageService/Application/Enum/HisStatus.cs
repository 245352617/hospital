using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.TriageService.Enum
{
    /// <summary>
    /// HIS 状态 
    /// 0.暂停 1.等待 2.准备 3.受理 4.完成 5.放弃 6.退号 7.暂挂
    /// </summary>
    public enum HisStatus : int
    {
        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        Suspend = 0,

        /// <summary>
        /// 等待
        /// </summary>
        [Description("等待")]
        Waiting = 1,

        /// <summary>
        /// 准备
        /// </summary>
        [Description("准备")]
        Ready = 2,

        /// <summary>
        /// 受理
        /// </summary>
        [Description("受理")]
        Accepted = 3,

        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Finished = 4,

        /// <summary>
        /// 放弃
        /// </summary>
        [Description("放弃")]
        GiveUp = 5,

        /// <summary>
        /// 退号
        /// </summary>
        [Description("退号")]
        Refund = 6,

        /// <summary>
        /// 暂挂
        /// </summary>
        [Description("暂挂")]
        Pending = 7,
    }
}
