using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 设备服务返回数据
    /// </summary>
    public class IotDataDto
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public string time { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public Guid iot_device_id { get; set; }

        /// <summary>
        /// 测量数据
        /// </summary>
        public IotSignsDataToInflux signs_data { get; set; }
    }
}