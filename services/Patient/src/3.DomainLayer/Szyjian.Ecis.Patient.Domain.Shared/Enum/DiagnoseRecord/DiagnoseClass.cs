using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 诊断分类编码
    /// </summary>
    public enum DiagnoseClass
    {
        /// <summary>
        /// 开立
        /// </summary>
        [Description("开立")]
        开立 = 1,

        /// <summary>
        /// 收藏
        /// </summary>
        [Description("收藏")]
        收藏 = 2
    }
}