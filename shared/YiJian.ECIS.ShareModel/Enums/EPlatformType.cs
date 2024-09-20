using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 系统标识， 0= 急诊， 1=院前急救
/// </summary>
public enum EPlatformType
{
    /// <summary>
    /// 急诊
    /// </summary>
    [XmlEnum("0")]
    [Description("急诊")]
    EmergencyTreatment = 0,

    /// <summary>
    /// 院前急救
    /// </summary>
    [XmlEnum("1")]
    [Description("院前急救")]
    PreHospital = 1,

}

/// <summary>
/// 添加药品医嘱来源
/// </summary>
public enum EAddPrescribeSource
{
    /// <summary>
    /// 单个提交
    /// </summary>
    [XmlEnum("0")]
    Single = 0,

    /// <summary>
    /// 成组提交
    /// </summary>
    [XmlEnum("1")]
    Group = 1,

}
