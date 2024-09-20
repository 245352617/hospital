using System.ComponentModel;
using System.Xml.Serialization;

namespace YiJian.DoctorsAdvices.Enums
{
    /// <summary>
    /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
    /// </summary>
    public enum EInsuranceCatalog : int
    {
        /// <summary>
        /// 自费
        /// </summary>
        [XmlEnum("0")]
        [Description("自费")]
        Self = 0,

        /// <summary>
        /// 甲类
        /// </summary>
        [XmlEnum("1")]
        [Description("甲类")]
        ClassA = 1,

        /// <summary>
        /// 乙类
        /// </summary>
        [XmlEnum("2")]
        [Description("乙类")]
        ClassB = 2,

        /// <summary>
        /// 其它
        /// </summary>
        [XmlEnum("3")]
        [Description("其它")]
        Other = 3
    }

    /// <summary>
    /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
    /// </summary>
    public static class EInsuranceCatalogExtend
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="insuranceCatalog"></param>
        /// <returns></returns>
        public static EInsuranceCatalog Parse(this string insuranceCatalog)
        {
            switch (insuranceCatalog.ToLower())
            {
                case "自費":
                case "self":
                case "0":
                    return EInsuranceCatalog.Self;
                case "甲类":
                case "甲":
                case "classa":
                case "1":
                    return EInsuranceCatalog.ClassA;
                case "乙类":
                case "乙":
                case "classb":
                case "2":
                    return EInsuranceCatalog.ClassB;
                case "其它":
                case "other":
                case "3":
                    return EInsuranceCatalog.Other;
                default:
                    return EInsuranceCatalog.Self;
            }
        }
    }

}
