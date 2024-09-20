using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 生命体征评级输入项
    /// </summary>
    public class VitalSignGradeInput
    {
        /// <summary>
        /// 当前分级
        /// </summary>
        public string CurrentTriageLevel { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        public string Dbp { get; set; }
        
        /// <summary>
        /// 呼吸
        /// </summary>
        public string Breath { get; set; }
        
        /// <summary>
        /// 舒张压
        /// </summary>
        public string Sbp { get; set; }
        
        /// <summary>
        /// 体温
        /// </summary>
        public string Temp { get; set; }
        
        /// <summary>
        /// 心率
        /// </summary>
        public string HeartRate { get; set; }
        
        /// <summary>
        /// SPO2
        /// </summary>
        public string Spo2 { get; set; }
    }
}