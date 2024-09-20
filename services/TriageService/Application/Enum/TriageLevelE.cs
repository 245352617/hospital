using System.ComponentModel;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊级别
    /// </summary>
    public enum TriageLevel
    {
        /// <summary>
        /// Ⅰ 级
        /// </summary>
        [Description("TriageLevel_001")] 
        FirstLv = 0,
        
        /// <summary>
        /// Ⅱ 级
        /// </summary>
        [Description("TriageLevel_002")] 
        SecondLv = 2,
        
        /// <summary>
        /// Ⅲ 级
        /// </summary>
        [Description("TriageLevel_003")] 
        ThirdLv = 4,
        
        /// <summary>
        /// Ⅳa 级
        /// </summary>
        [Description("TriageLevel_004")] 
        FourthALv = 8,
        
        /// <summary>
        /// Ⅳb 级
        /// </summary>
        [Description("TriageLevel_005")] 
        FourthBLv = 16,
    }
}