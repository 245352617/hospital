using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 预检分诊就诊状态枚举：0：待分诊，1：待就诊，2：正在就诊，3：已就诊，4：暂停
    /// </summary>
    public enum TriageVisitStatus
    {
        /// <summary>
        /// 未分诊
        /// </summary>
        [Description("未分诊")]
        NotTriageYet = 0,

        /// <summary>
        /// 待就诊（已挂号、已分诊、未入科）
        /// </summary>
        [Description("待就诊")]
        WattingTreat = 1,

        /// <summary>
        /// 正在就诊
        /// </summary>
        [Description("正在就诊")]
        Treating = 2,

        /// <summary>
        /// 已就诊
        /// </summary>
        [Description("已就诊")]
        Treated = 3,

        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        Suspend = 4,
    }
}