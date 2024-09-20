using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 叫号状态枚举
    /// </summary>
    public enum ECallStatus
    {
        /// <summary>
        /// 未叫号
        /// </summary>
        [XmlEnum("0")]
        [Description("未叫号")]
        NotCalled = 0,

        /// <summary>
        /// 叫号中
        /// </summary>
        [XmlEnum("1")]
        [Description("叫号中")]
        Calling = 1,

        /// <summary>
        /// 已叫号
        /// </summary>
        [XmlEnum("2")]
        [Description("已叫号")]
        Called = 2,
    }

}
