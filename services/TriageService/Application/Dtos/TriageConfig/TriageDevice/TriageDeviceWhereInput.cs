using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class TriageDeviceWhereInput
    {
        /// <summary>
        /// 分诊编号或者设备名称
        /// </summary>
        public string DeviceCodeOrName { get; set; }

    }
}
