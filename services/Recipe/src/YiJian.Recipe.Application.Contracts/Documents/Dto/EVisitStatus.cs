using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 就诊状态枚举
    /// </summary>
    public enum EVisitStatus
    {
        /// <summary>
        /// 全部
        /// </summary>
        [XmlEnum("-1")]
        [Description("全部")]
        全部 = -1,

        /// <summary>
        /// 未挂号
        /// </summary>
        [XmlEnum("0")]
        [Description("未挂号")]
        未挂号 = 0,

        /// <summary>
        /// 已挂号、未入科
        /// </summary>
        [XmlEnum("1")]
        [Description("待就诊")]
        待就诊 = 1,

        /// <summary>
        /// 排队叫号过号
        /// </summary>
        [XmlEnum("2")]
        [Description("过号")]
        过号 = 2,

        /// <summary>
        /// 挂号已退号
        /// </summary>
        [XmlEnum("3")]
        [Description("已退号")]
        已退号 = 3,

        /// <summary>
        /// 正在就诊
        /// </summary>
        [XmlEnum("4")]
        [Description("正在就诊")]
        正在就诊 = 4,

        /// <summary>
        /// 可看作结束就诊
        /// </summary>
        [XmlEnum("5")]
        [Description("已就诊")]
        已就诊 = 5,

        /// <summary>
        /// 针对留观区、抢救区患者
        /// </summary>
        [XmlEnum("6")]
        [Description("出科")]
        出科 = 6,
    }

}
