using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.DoctorsAdvices.Enums
{
    /// <summary>
    /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
    /// </summary>
    public enum EMedicineUnPack : int
    {
        /// <summary>
        /// 最小单位总量取整
        /// </summary>
        [XmlEnum("0")]
        [Description("最小单位总量取整")]
        RoundByMinUnitAmount = 0,

        /// <summary>
        /// 包装单位总量取整
        /// </summary>
        [XmlEnum("1")]
        [Description("包装单位总量取整")]
        RoundByPackUnitAmount = 1,

        /// <summary>
        /// 最小单位每次取整
        /// </summary>
        [XmlEnum("2")]
        [Description("最小单位每次取整")]
        RoundByMinUnitTime = 2,

        /// <summary>
        /// 包装单位每次取整
        /// </summary>
        [XmlEnum("3")]
        [Description("包装单位每次取整")]
        RoundByPackUnitTime = 3,

        /// <summary>
        /// 最小单位可拆分
        /// </summary>
        [XmlEnum("4")]
        [Description("最小单位可拆分")]
        RoundByMinUnit = 4,

    }
}
