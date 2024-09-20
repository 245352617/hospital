using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 当前任务设备Dto
    /// </summary>
    public class CurrentTaskAmbulanceDeviceDto
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public Guid IotDeviceId { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string DeviceTypeName { get; set; }
    }
}
