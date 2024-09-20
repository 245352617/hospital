using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{

    public class IotMoniterDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 无此设备！
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Data> data { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string iot_device_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Signs_data signs_data { get; set; }
    }


    public class Signs_data
    {
        /// <summary>
        /// 
        /// </summary>
        public string ObservationDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ObservationDatasItem> ObservationDatas { get; set; }
    }


    //如果好用，请收藏地址，帮忙分享。
    public class ObservationDatasItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AlarmValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TakeTime { get; set; }
    }

   

}
