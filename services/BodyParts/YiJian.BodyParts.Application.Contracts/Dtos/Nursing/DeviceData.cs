using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YiJian.BodyParts.Dtos
{
    public class DeviceData
    {
        /// <summary>
        /// 
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string breath_data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string data_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string iot_device_id { get; set; }
    }
}
