using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 支付状态 , 0=未支付,1=已支付,2=部分支付,3=已退费
/// </summary>
public enum EPayStatus : int
{
    /// <summary>
    /// 未支付
    /// </summary>
    [XmlEnum("0")]
    [Description("未支付")]
    NoPayment = 0,

    /// <summary>
    /// 已支付
    /// </summary>
    [XmlEnum("1")]
    [Description("已支付")]
    HavePaid = 1,

    /// <summary>
    /// 部分支付
    /// </summary>
    [XmlEnum("2")]
    [Description("部分支付")]
    PartialPayment = 2,

    /// <summary>
    /// 已退费
    /// </summary>
    [XmlEnum("3")]
    [Description("已退费")]
    HaveRefund = 3,

}
