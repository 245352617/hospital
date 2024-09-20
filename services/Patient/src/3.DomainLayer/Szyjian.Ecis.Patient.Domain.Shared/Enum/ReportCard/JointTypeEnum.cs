using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 对接类型，1 Web， 2 桌面程序
    /// </summary>
    public enum JointTypeEnum
    {
        /// <summary>
        /// Web
        /// </summary>
        [Description("Web")]
        Web = 1,

        /// <summary>
        /// 桌面程序
        /// </summary>
        [Description("桌面程序")]
        Exe = 2
    }
}
