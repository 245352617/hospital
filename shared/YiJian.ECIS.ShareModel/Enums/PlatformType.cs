using System.ComponentModel;

namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 平台标识
/// </summary>
public enum PlatformType
{
    /// <summary>
    /// 急诊
    /// </summary>
    [Description("急诊")]
    EmergencyTreatment = 0,

    /// <summary>
    /// 院前
    /// </summary>
    [Description("院前")]
    PreHospital = 1,


    /// <summary>
    /// 全部
    /// </summary>
    [Description("全部")]
    All = -1,
}