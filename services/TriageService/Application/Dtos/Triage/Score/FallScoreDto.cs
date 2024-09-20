namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 跌倒评分
    /// </summary>
    public class FallScoreDto
    {
        /// <summary>
        /// 是否有跌倒风险
        /// </summary>
        public bool IsFallRisk { get; set; } 

        /// <summary>
        /// 跌倒风险明细
        /// </summary>
        public string FallRiskDetail { get; set; }

        /// <summary>
        /// 预防跌倒措施
        /// </summary>
        public string FallRiskPrevention { get; set; }
    }
}

