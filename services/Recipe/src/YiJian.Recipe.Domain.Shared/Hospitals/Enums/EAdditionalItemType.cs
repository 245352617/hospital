using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.Hospitals.Enums
{

    public enum EAdditionalItemType
    {

        /// <summary>
        /// 否
        /// </summary>
        [XmlEnum("0")]
        [Description("否")]
        No = 0,

        /// <summary>
        /// 用法附加处置
        /// </summary>
        [XmlEnum("1")]
        [Description("用法附加处置")]
        UsageAdditional = 1,

        /// <summary>
        /// 皮试附加处置
        /// </summary>
        [XmlEnum("2")]
        [Description("皮试附加处置")]
        SkinAdditional = 2,

        /// <summary>
        /// 频次附加处置
        /// </summary>
        [XmlEnum("3")]
        [Description("频次附加处置")]
        FrequencyAdditional = 3,

        /// <summary>
        /// 检验附加处置
        /// </summary>
        [XmlEnum("4")]
        [Description("检验附加处置")]
        LisAdditional = 4
    }
}