using System;
using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 当前任务单救护车设备Dto
    /// </summary>
    public class CurrentTaskAmbulanceDto
    {
        /// <summary>
        /// 任务单Id
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 车牌编号
        /// </summary>
        public string CarCard { get; set; }

        /// <summary>
        /// 设备列表
        /// </summary>
        public IEnumerable<CurrentTaskAmbulanceDeviceDto> Devices { get; set; }
    }
}
