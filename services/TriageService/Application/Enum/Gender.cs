using System.ComponentModel;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = -1,
        
        /// <summary>
        /// 男
        /// </summary>
        [Description("Sex_Man")]
        Male = 0,
        
        /// <summary>
        /// 女
        /// </summary>
        [Description("Sex_Woman")]
        Female = 1,
        
        /// <summary>
        /// 未知
        /// </summary>
        [Description("Sex_Unknown")]
        Unknown = 2,
    }
}