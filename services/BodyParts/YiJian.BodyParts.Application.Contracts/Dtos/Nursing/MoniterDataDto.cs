using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{

    public class MoniterDataDto
    {
        List<MoniterData> moniterDatas { get; set; }
    }


    public class MoniterData
    {
        /// <summary>
        /// 
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string iot_device_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signs_data { get; set; }
    }




}
