using System;
using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class IotSignsDataToInflux
    {
        /// <summary>
        /// 测量时间
        /// </summary>
        public DateTime ObservationDate { get;set;}

        /// <summary>
        /// 测量数据
        /// </summary>
        public List<ObservationData> ObservationDatas { get; set; }
    }
}