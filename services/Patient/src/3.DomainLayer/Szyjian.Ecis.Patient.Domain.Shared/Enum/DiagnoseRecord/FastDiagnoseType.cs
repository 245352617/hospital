using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 快速诊断类型枚举
    /// </summary>
    public enum FastDiagnoseType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = -1,

        /// <summary>
        /// 常用
        /// </summary>
        [Description("常用")]
        CommonlyUsed = 0,

        /// <summary>
        /// 收藏
        /// </summary>
        [Description("收藏")]
        Collection = 1,

        /// <summary>
        /// 最近历史
        /// </summary>
        [Description("最近历史")]
        RecentHistory = 2

    }
}