using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 区域枚举
    /// </summary>
    public enum Area
    {
        /// <summary>
        /// 就诊区
        /// </summary>
        [Description("就诊区")]
        OutpatientArea,

        /// <summary>
        /// 留观区
        /// </summary>
        [Description("留观区")]
        ObservationArea,

        /// <summary>
        /// 抢救区
        /// </summary>
        [Description("抢救区")]
        RescueArea
    }
}