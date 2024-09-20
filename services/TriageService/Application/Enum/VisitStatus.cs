using System.ComponentModel;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 就诊状态 0:未分诊, 1:待就诊, 2:正在就诊, 3:已就诊, 4:暂停
    /// </summary>
    public enum VisitStatus : int
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
    public enum EVisitStatus : int
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        None = -1,

        /// <summary>
        /// 未挂号
        /// </summary>
        [Description("未挂号")]
        NotRegister = 0,

        /// <summary>
        /// 待就诊
        /// </summary>
        [Description("待就诊")]
        WaittingTreat = 1,

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
        /// 正在就诊
        /// </summary>
        [Description("正在就诊")]
        Treating = 4,

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
