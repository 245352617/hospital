using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 付费类型: 0=自费,1=医保,2=其它
/// </summary>
public enum ERecipePayType
{
    /// <summary>
    /// 自费
    /// </summary>
    [XmlEnum("0")]
    [Description("自费")]
    Self = 0,

    /// <summary>
    /// 医保
    /// </summary>
    [XmlEnum("1")]
    [Description("医保")]
    Insurance = 1,

    /// <summary>
    /// 其它
    /// </summary>
    [XmlEnum("2")]
    [Description("其它")]
    Other = 2
}
