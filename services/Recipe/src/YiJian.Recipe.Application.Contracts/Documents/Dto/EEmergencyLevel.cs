using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 危重等级枚举
    /// </summary>
    public enum EEmergencyLevel
    {
        /// <summary>
        /// 一般
        /// </summary>
        [XmlEnum("0")]
        [Description("一般")]
        一般 = 0,

        /// <summary>
        /// 病重
        /// </summary>
        [XmlEnum("1")]
        [Description("病重")]
        病重 = 1,

        /// <summary>
        /// 濒危
        /// </summary>
        [XmlEnum("2")]
        [Description("濒危")]
        濒危 = 2,
    }

}
