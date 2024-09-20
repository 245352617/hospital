using System.ComponentModel;

namespace YiJian.DoctorsAdvices.Enums
{
    /// <summary>
    /// 来源类型
    /// </summary>
    public enum EOriginType
    {
        /// <summary>
        /// 医生操作
        /// </summary>
        [Description("医生操作")]
        Doctor = 0,

        /// <summary>
        /// 护士操作
        /// </summary>
        [Description("护士操作")]
        Nurse = 1

    }
}
