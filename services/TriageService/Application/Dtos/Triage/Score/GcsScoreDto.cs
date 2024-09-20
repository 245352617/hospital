namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// GCS 评分
    /// </summary>
    public class GcsScoreDto
    {
        /// <summary>
        /// 分数
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 级别编码
        /// </summary>
        public string LevelCode { get; set; }

        /// <summary>
        /// 睁眼
        /// </summary>
        public string EyesVal { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string LangVal { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        public string MotionVal { get; set; }
    }
}
