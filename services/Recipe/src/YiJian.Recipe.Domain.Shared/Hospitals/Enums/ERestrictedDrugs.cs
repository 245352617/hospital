using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.Hospitals.Enums
{
    /// <summary>
    /// 限制标志 默认值：1.医保用药  -1.未审批、全自费 2.地补目录属性记账 3.重疾目录属性记账
    /// </summary>
    public enum ERestrictedDrugs : int
    {
        /**
        -1：审批不通过、全自费
        默认值：1：记账
        2.地补目录属性记账
        3.重疾目录属性记账
        4：进口属性记账
        5：国产属性记账
        */

        /// <summary>
        /// -1.未审批、全自费 
        /// </summary>
        [XmlEnum("-1")]
        [Description("自费")]
        QuanZifei = -1,

        /// <summary>
        /// 默认
        /// </summary>
        [XmlEnum("0")]
        [Description("默认")]
        Default = 0,

        /// <summary>
        /// 1.医保用药  (默认值：1：记账)
        /// </summary>
        [XmlEnum("1")]
        [Description("医保记账")]
        YibaoYongyao = 1,

        /// <summary>
        /// 2.地补目录属性记账 
        /// </summary>
        [XmlEnum("2")]
        [Description("地补记账")]
        DibuMuluShuxingJizhang = 2,

        /// <summary>
        /// 3.重疾目录属性记账
        /// </summary>
        [XmlEnum("3")]
        [Description("重疾记账")]
        ZhongjiMuluShuxingJizhang = 3,

        /// <summary>
        /// 4：进口属性记账
        /// </summary>
        [XmlEnum("4")]
        [Description("进口记账")]
        JinkouShuxinJizhang = 4,

        /// <summary>
        /// 5：国产属性记账
        /// </summary>
        [XmlEnum("5")]
        [Description("国产记账")]
        GuocanShuxinJizhang = 5,

    }

}
