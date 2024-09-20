using System.ComponentModel;

namespace YiJian.DoctorsAdvices.Enums
{
    /// <summary>
    /// 接种针法 0=四针法, 1=五针法
    /// </summary>
    public enum EAcupunctureManipulation
    {
        /// <summary>
        /// 四针法
        /// </summary>
        [Description("四针法")]
        FourTimes = 0,

        /// <summary>
        /// 五针法
        /// </summary>
        [Description("五针法")]
        FiveTimes = 1,
    }
}
