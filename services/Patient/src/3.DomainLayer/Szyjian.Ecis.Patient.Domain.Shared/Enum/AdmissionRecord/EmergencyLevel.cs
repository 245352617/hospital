using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 危重等级枚举
    /// </summary>
    public enum EmergencyLevel
    {
        /// <summary>
        /// 一般
        /// </summary>
        [Description("一般")]
        一般 = 0,

        /// <summary>
        /// 病重
        /// </summary>
        [Description("病重")]
        病重 = 1,

        /// <summary>
        /// 濒危
        /// </summary>
        [Description("濒危")]
        濒危 = 2,
    }
}