using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class ObservationData
    {
        /// <summary>
        /// 测量类别
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// 测量结果
        /// </summary>
        public string Result { get; set; }
        
        /// <summary>
        /// 测量单位
        /// </summary>
        public string Unit { get; set; }
        
        /// <summary>
        /// 报警值
        /// </summary>
        public string AlarmValue { get; set; }
        
        /// <summary>
        /// 测量时间
        /// </summary>
        public DateTime TakeTime { get; set; }
    }
}