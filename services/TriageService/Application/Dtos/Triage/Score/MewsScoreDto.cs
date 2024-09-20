namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// MEWS 评分
    /// </summary>
    public class MewsScoreDto
    {
        /// <summary>
        /// 心率
        /// </summary>
        public string Hr { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        public string Sbp { get; set; }

        /// <summary>
        /// 呼吸
        /// </summary>
        public string R { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        public string T { get; set; }

        /// <summary>
        /// 意识
        /// </summary>
        public string Sense { get; set; }
    }
}
