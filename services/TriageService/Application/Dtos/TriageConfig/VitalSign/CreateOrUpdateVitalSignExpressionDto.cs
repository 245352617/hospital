namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 创建或更新生命体征表达式Dto
    /// </summary>
    public class CreateOrUpdateVitalSignExpressionDto
    {
        /// <summary>
        /// 评分项
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Ⅰ级评分表达式
        /// </summary>
        public string StLevelExpression { get; set; }
        
        /// <summary>
        /// Ⅰ级评分表达式
        /// </summary>
        public string NdLevelExpression { get; set; }
        
        /// <summary>
        /// Ⅰ级评分表达式
        /// </summary>
        public string RdLevelExpression { get; set; }
        
        /// <summary>
        /// Ⅰ级评分表达式
        /// </summary>
        public string ThALevelExpression { get; set; }
        
        /// <summary>
        /// Ⅰ级评分表达式
        /// </summary>
        public string ThBLevelExpression { get; set; }
    }
}