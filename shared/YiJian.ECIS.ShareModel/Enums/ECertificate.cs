using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 证件类型
/// </summary>
public enum ECertificate : int
{
    /// <summary>
    /// 身份证
    /// </summary>
    [XmlEnum("0")]
    [Description("身份证")]
    IDCard = 0,

    /// <summary>
    /// 其他证件(非中国大陆身份证)
    /// </summary>
    [XmlEnum("1")]
    [Description("其他证件")]
    OtherCard = 1,

}
