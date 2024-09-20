using System.ComponentModel;

namespace YiJian.DoctorsAdvices.Enums
{
    /// <summary>
    /// 查询医嘱的时间类型分类: 0=24小时内,1=48小时内,2=72小时内,-1=所有
    /// </summary>
    public enum EApplyType
    {
        /// <summary>
        /// 24小时内
        /// </summary>
        [Description("24小时内")]
        H24 = 0,

        /// <summary>
        /// 48小时内
        /// </summary>
        [Description("48小时内")]
        H48 = 1,

        /// <summary>
        /// 72小时内
        /// </summary>
        [Description("72小时内")]
        H72 = 2,

        /// <summary>
        /// 所有
        /// </summary>
        [Description("所有")]
        All = -1,

    }
}
