using System.ComponentModel;

namespace YiJian.Hospitals.Enums
{
    /// <summary>
    /// 附加项类型
    /// </summary>
    public enum EAdditionalType
    {
        /// <summary>
        /// 用药途径附加项
        /// </summary>
        [Description("用药途径附加项")]
        UsageAdditional = 0,

        /// <summary>
        /// 皮试附加项
        /// </summary>
        [Description("皮试附加项")]
        SkinAdditional = 1


    }

}
