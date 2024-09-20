using System.ComponentModel;

namespace YiJian.Nursing
{
    /// <summary>
    /// 描述：护士站执行单状态枚举
    /// 创建人： yangkai
    /// 创建时间：2023/3/8 17:37:57
    /// </summary>
    public enum ExecuteStatusEnum
    {
        /// <summary>
        /// 未缴费
        /// </summary>
        [Description("未缴费")]
        NoPay = 0,

        /// <summary>
        /// 未核对
        /// </summary>
        [Description("未核对")]
        UnCheck = 1,

        /// <summary>
        /// 待执行
        /// </summary>
        [Description("待执行")]
        PreExec = 2,

        /// <summary>
        /// 已执行
        /// </summary>
        [Description("已执行")]
        Exec = 3
    }
}
