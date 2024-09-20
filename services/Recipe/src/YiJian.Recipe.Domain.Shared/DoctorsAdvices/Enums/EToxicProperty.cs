using System.ComponentModel;

namespace YiJian.Recipes
{
    /// <summary>
    /// 药理属性
    /// </summary>
    public enum EToxicProperty
    {
        /// <summary>
        /// 普通
        /// </summary>
        [Description("普通")]
        Common = 0,

        /// <summary>
        /// </summary>
        [Description("麻")]
        Anaesthesia = 1,

        /// <summary>
        /// 毒性药品
        /// </summary>
        [Description("毒")]
        Toxicity = 2,

        /// <summary>
        /// 一类精神药品
        /// </summary>
        [Description("精一")]
        ClassI = 3,

        /// <summary>
        /// 二类精神药品
        /// </summary>
        [Description("精二")]
        ClassII = 4,

        /// <summary>
        /// 放射性药品
        /// </summary>
        [Description("放射")]
        Radiation = 5,

        /// <summary>
        /// 高危药品
        /// </summary>
        [Description("高危")]
        HighRisk = 6,

        /// <summary>
        /// 麻、精一
        /// </summary>
        [Description("麻、精一")]
        AnaesthesiaAndClassI = 7,

        /// <summary>
        /// 毒、麻
        /// </summary>
        [Description("毒、麻")]
        ToxicityAndAnaesthesia = 8,

        /// <summary>
        /// 其它
        /// </summary>
        [Description("其它")]
        Other = -1
    }
}