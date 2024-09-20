using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 生命体征评分标准Dto
    /// </summary>
    public class VitalSignExpressionDto
    {
        /// <summary>
        /// Id 新增时无需传入
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 评分项
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Ⅰ级评分表达式
        /// </summary>
        public string StLevelExpression { get; set; }
        
        /// <summary>
        /// Ⅱ级评分表达式
        /// </summary>
        public string NdLevelExpression { get; set; }
        
        /// <summary>
        /// Ⅲ级评分表达式
        /// </summary>
        public string RdLevelExpression { get; set; }
        
        /// <summary>
        /// Ⅳa级评分表达式
        /// </summary>
        public string ThALevelExpression { get; set; }
        
        /// <summary>
        /// Ⅳb级评分表达式
        /// </summary>
        public string ThBLevelExpression { get; set; }
    }
}