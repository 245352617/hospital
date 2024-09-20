namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 评分字典输入项
    /// </summary>
    public class ScoreDictInput
    {
        /// <summary>
        /// 评分类型
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 是否启用，-1：全部，0：未启用，1：启用
        /// </summary>
        public int IsEnabled { get; set; }
    }
}