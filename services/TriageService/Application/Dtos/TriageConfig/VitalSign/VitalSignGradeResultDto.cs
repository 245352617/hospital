namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 生命体征评级结果Dto
    /// </summary>
    public class VitalSignGradeResultDto
    {
        /// <summary>
        /// 分诊级别编码
        /// </summary>
        public string TriageLevel { get; set; }

        /// <summary>
        /// 分诊级别名称
        /// </summary>
        public string TriageLevelName { get; set; }

        /// <summary>
        /// 收缩压颜色值
        /// </summary>
        public string DbpColor { get; set; }

        /// <summary>
        /// 呼吸颜色值
        /// </summary>
        public string BreathColor { get; set; }

        /// <summary>
        /// 舒张压颜色值
        /// </summary>
        public string SbpColor { get; set; }

        /// <summary>
        /// 体温颜色值
        /// </summary>
        public string TempColor { get; set; }
        
        /// <summary>
        /// 心率颜色值
        /// </summary>
        public string HeartRateColor { get; set; }

        /// <summary>
        /// Spo2颜色值
        /// </summary>
        public string Spo2Color { get; set; }
    }
    
    
}