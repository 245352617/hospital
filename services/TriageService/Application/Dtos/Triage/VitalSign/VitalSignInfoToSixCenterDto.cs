namespace SamJan.MicroService.PreHospital.TriageService
{
    public class VitalSignInfoToSixCenterDto
    {
        /// <summary>
        /// 左上支最低血压收缩压，mmHg
        /// </summary>
        public string LSBloodPressureMin{get; set; }
        /// <summary>
        /// 左上支最高血压收缩压，mmHg
        /// </summary>
        public string LSBloodPressureMax{get; set; }
        /// <summary>
        /// 右上支最低血压舒张压，mmHg
        /// </summary>
        public string RSBloodPressureMin{get; set; }
        /// <summary>
        /// 右上支最高血压舒张压，mmHg
        /// </summary>
        public string RSBloodPressureMax{get; set; }
        /// <summary>
        /// 心率(次/min)
        /// </summary>
        public string HeartRate{get; set; }
        /// <summary>
        /// 体温
        /// </summary>
        public string Temperature{get; set; }
        /// <summary>
        /// 呼吸
        /// </summary>
        public string Breathing{get; set; }
        /// <summary>
        /// 血氧饱和度
        /// </summary>
        public string SPO2{get; set; }

    }
}