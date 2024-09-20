using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.DoctorsAdvices.Enums
{
    /// <summary>
    /// 医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费
    /// </summary>
    public enum ERecipeStatus : int
    {
        /// <summary>
        /// 0:保存(未提交)
        /// </summary>
        [XmlEnum("0")]
        [Description("未提交")]
        Saved = 0,

        /// <summary>
        /// 1:已提交
        /// </summary>
        [XmlEnum("1")]
        [Description("已提交")]
        Submitted = 1,

        /// <summary>
        /// 2:已确认
        /// </summary>
        [XmlEnum("2")]
        [Description("已确认")]
        Confirmed = 2,

        /// <summary>
        /// 3:已作废
        /// </summary>
        [XmlEnum("3")]
        [Description("已作废")]
        Cancelled = 3,

        /// <summary>
        /// 4:已停止
        /// </summary>
        [XmlEnum("4")]
        [Description("已停止")]
        Stopped = 4,

        /// <summary>
        /// 6:已驳回
        /// </summary>
        [XmlEnum("6")]
        [Description("已驳回")]
        Rejected = 6,

        /// <summary>
        /// 7:已执行
        /// </summary>
        [XmlEnum("7")]
        [Description("已执行")]
        Executed = 7,


        /// <summary>
        /// 已缴费
        /// </summary>
        [XmlEnum("18")]
        [Description("已缴费")]
        PayOff = 18,

        /// <summary>
        /// 已退费
        /// </summary>
        [XmlEnum("19")]
        [Description("已退费")]
        ReturnPremium = 19,

    }
}
