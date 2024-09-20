namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 评分Dto
    /// </summary>
    public class ScoreDto
    {
        /// <summary>
        /// 评分类型名称
        /// </summary>
        public string ScoreName { get; set; }

        /// <summary>
        /// 评分类型
        /// </summary>
        public string ScoreType { get; set; }

        /// <summary>
        /// 评分值
        /// </summary>
        public int ScoreValue { get; set; }
    }
}