using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.DoctorsAdvices.Enums
{
    /// <summary>
    /// 医嘱各项分类: 0=药品,1=检查项,2=检验项,3=诊疗项
    /// </summary>
    public enum EDoctorsAdviceItemType : int
    {
        /// <summary>
        /// 药品
        /// </summary>
        [XmlEnum("0")]
        [Description("药品项")]
        Prescribe = 0,

        /// <summary>
        /// 检查项
        /// </summary>
        [XmlEnum("1")]
        [Description("检查项")]
        Pacs = 1,

        /// <summary>
        /// 检验项
        /// </summary>
        [XmlEnum("2")]
        [Description("检验项")]
        Lis = 2,

        /// <summary>
        /// 诊疗项
        /// </summary>
        [XmlEnum("3")]
        [Description("诊疗项")]
        Treat = 3,

    }
}
