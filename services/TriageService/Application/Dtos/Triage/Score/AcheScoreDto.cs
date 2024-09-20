namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 疼痛评分
    /// </summary>
    public class AcheScoreDto
    {

        /// <summary>
        /// NRS分数值
        /// </summary>
        public int NrsScore { get; set; }

        /// <summary>
        /// 面部表情分数
        /// </summary>
        public int Face { get; set; }

        /// <summary>
        /// 腿部活动分数
        /// </summary>
        public int Leg { get; set; }

        /// <summary>
        /// 体位分数
        /// </summary>
        public int Activity { get; set; }

        /// <summary>
        /// 哭闹分数
        /// </summary>
        public int Cry { get; set; }

        /// <summary>
        /// 可安慰度分数
        /// </summary>
        public int Com { get; set; }

    }
}
