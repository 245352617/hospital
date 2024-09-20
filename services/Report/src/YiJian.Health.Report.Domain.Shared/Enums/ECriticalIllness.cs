using System.ComponentModel;

namespace YiJian.Health.Report.Enums
{
    /// <summary>
    /// 危重枚举(0 = 病危, 1=病重)
    /// </summary>
    public enum ECriticalIllness : int
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("一般")]
        General = 0,

        /// <summary>
        /// 病重
        /// </summary>
        [Description("病重")]
        Ill = 1,

        /// <summary>
        /// 病危
        /// </summary>
        [Description("病危")]
        Critical = 2,

    }
}
