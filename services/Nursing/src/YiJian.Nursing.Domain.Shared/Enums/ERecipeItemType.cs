using System.ComponentModel;

namespace YiJian.Nursing
{
    /// <summary>
    /// 医嘱各项分类: 0=药品,1=检查,2=检验,3=诊疗
    /// </summary>
    public enum ERecipeItemType : int
    {
        /// <summary>
        /// 药品
        /// </summary>
        [Description("药品")]
        Prescribe = 0,

        /// <summary>
        /// 检查
        /// </summary>
        [Description("检查")]
        Pacs = 1,

        /// <summary>
        /// 检验
        /// </summary>
        [Description("检验")]
        Lis = 2,

        /// <summary>
        /// 诊疗
        /// </summary>
        [Description("诊疗")]
        Treat = 3,

    }
}
