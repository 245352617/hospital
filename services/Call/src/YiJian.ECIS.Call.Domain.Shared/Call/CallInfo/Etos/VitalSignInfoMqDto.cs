namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 推送患者分诊生命体征信息队列Dto
    /// </summary>
    public class VitalSignInfoMqDto
    {
        /// <summary>
        /// 收缩压
        /// </summary>
        public string Sbp { get; set; }

        /// <summary>
        /// 舒张压
        /// </summary>
        public string Sdp { get; set; }

        /// <summary>
        /// 血氧饱和度
        /// </summary>
        public string SpO2 { get; set; }

        /// <summary>
        /// 呼吸
        /// </summary>
        public string BreathRate { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        public string Temp { get; set; }

        /// <summary>
        /// 心率
        /// </summary>
        public string HeartRate { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 备注Code
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 备注名称
        /// </summary>
        public string RemarkName { get; set; }
    }
}