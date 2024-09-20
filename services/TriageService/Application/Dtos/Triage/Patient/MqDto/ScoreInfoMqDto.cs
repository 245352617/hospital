namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 推送患者评分信息队列Dto
    /// </summary>
    public class ScoreInfoMqDto
    {
        /// <summary>
        /// 等级编码
        /// </summary>
        public string LevelCode { get; set; }
        /// <summary>
        /// 评分类型
        /// </summary>
        public string ScoreType { get; set; }

        /// <summary>
        /// 评分数值
        /// </summary>
        public string ScoreValue { get; set; }

        /// <summary>
        /// 评分等级
        /// </summary>
        public string ScoreDescription { get; set; }

        /// <summary>
        /// 评分内容Json
        /// </summary>
        public string ScoreContent { get; set; }
    }
}