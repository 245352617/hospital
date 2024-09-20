namespace TriageService.Application.Dtos.TriageConfig.TriageConfig
{
    /// <summary>
    /// 同步His费别列表使用
    /// </summary>
    public class FaberSyncDto
    {
        /// <summary>
        /// his费别代码
        /// </summary>
        public string FeibieCode { get; set; }

        /// <summary>
        /// His费别名称
        /// </summary>
        public string FeibieName { get; set; }

        /// <summary>
        /// 深圳医保费别代码
        /// </summary>
        public string MedFeibieCode { get; set; }

        /// <summary>
        /// 深圳医保费别名称
        /// 现同步到TriageConfig没有字段能存，因此舍弃保存
        /// </summary>
        public string MedFeibieName { get; set; }
    }
}
