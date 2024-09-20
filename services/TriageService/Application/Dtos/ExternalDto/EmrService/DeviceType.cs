using System.ComponentModel;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 设备类型枚举
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = -1,
        
        /// <summary>
        /// 监护仪
        /// </summary>
        [Description("监护仪")]
        Monitor = 0,
        
        /// <summary>
        /// 摄像头
        /// </summary>
        [Description("监护仪")]
        Camera = 1,
        
        /// <summary>
        /// 读卡器
        /// </summary>
        [Description("读卡器")]
        CardReader = 2
    }
}