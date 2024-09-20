using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 诊断类型编码
    /// </summary>
    public enum DiagnoseType
    {
        /// <summary>
        /// 一般诊断
        /// </summary>
        [Description("一般诊断")]
        Commonly = 0,

        /// <summary>
        /// 疑似诊断
        /// </summary>
        [Description("疑似诊断")]
        Suspected = 1,

        /// <summary>
        /// 主要诊断
        /// </summary>
        [Description("主要诊断")]
        Main = 2
    }
}