using System.Xml.Serialization;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 出科原因枚举
    /// </summary>
    public enum EOutDeptReason
    {
        [XmlEnum("0")]
        正常出科 = 0,

        [XmlEnum("1")]
        转住院 = 1,

        [XmlEnum("2")]
        死亡 = 2
    }

}
