using System.ComponentModel;

namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 流转类型
/// </summary>
public enum TransferType : int
{
    /// <summary>
    /// 流转时未传参
    /// </summary>
    [Description("流转时未传参")]
    NoInput = -1,

    /// <summary>
    /// 患者入科
    /// </summary>
    [Description("患者入科")]
    InDept = 0,

    /// <summary>
    /// 转抢救区
    /// </summary>
    [Description("转抢救区")]
    RescueArea = 1,

    /// <summary>
    /// 转留观区
    /// </summary>
    [Description("转留观区")]
    ObservationArea = 2,

    /// <summary>
    /// 转就诊区
    /// </summary>
    [Description("转就诊区")]
    OutpatientArea = 3,

    /// <summary>
    /// 转住院
    /// </summary>
    [Description("转住院")]
    ToHospital = 4,

    /// <summary>
    /// 出科
    /// </summary>
    [Description("出科")]
    OutDept = 5,

    /// <summary>
    /// 死亡
    /// </summary>
    [Description("死亡")]
    Death = 6,

    // 召回 = 9,
}
