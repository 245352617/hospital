using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.Hospitals.Enums
{
    /// <summary>
    /// 皮试选择枚举
    /// </summary>
    public enum ESkinTestSignChoseResult : int
    {
        /// <summary>
        /// 否
        /// </summary>
        [XmlEnum("0")]
        [Description("否")]
        No = 0,

        /// <summary>
        /// 是
        /// </summary>
        [XmlEnum("1")]
        [Description("是")]
        Yes = 1,

        /// <summary>
        /// 续用
        /// </summary>
        [XmlEnum("2")]
        [Description("续用")]
        KeepUp = 2,

    }

}
