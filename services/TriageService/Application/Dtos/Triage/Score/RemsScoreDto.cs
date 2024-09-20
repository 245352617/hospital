namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// REMS评分
    /// </summary>
    public class RemsScoreDto
    {
        /// <summary>
        /// 眼睛
        /// </summary>
        public string OpenEye { get; set; } 

        /// <summary>
        /// 语言交流
        /// </summary>
        public string Communication { get; set; }

        /// <summary>
        /// 运动
        /// </summary>
        public string Sport { get; set; }

        /// <summary>
        /// 脉搏
        /// </summary>
        public string P { get; set; }

        /// <summary>
        /// Spo2
        /// </summary>
        public string Spo2 { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        public string Sbp { get; set; }

        /// <summary>
        /// 呼吸
        /// </summary>
        public string R { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }
    }
}
