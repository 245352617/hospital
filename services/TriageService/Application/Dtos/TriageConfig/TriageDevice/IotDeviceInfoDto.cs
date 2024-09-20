using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class IotResultDto
    {

        public int count { get; set; }

        public List<IotDeviceInfoDto> datas { get; set; }
    }

    public class IotDeviceInfoDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 理邦监护仪测试设备
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 理邦
        /// </summary>
        public string vendor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public string netAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int purpose { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}