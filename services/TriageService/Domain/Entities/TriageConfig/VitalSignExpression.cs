using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 生命体征评分标准
    /// </summary>
    public class VitalSignExpression : BaseEntity<Guid>
    {
        public VitalSignExpression SetId(Guid id)
        {
            Id = id;
            return this;
        }
        
        /// <summary>
        /// 评分项
        /// </summary>
        [Description("评分项")]
        [StringLength(200)]
        public string ItemName { get; set; }

        /// <summary>
        /// Ⅰ级评分表达式
        /// </summary>
        [Description("Ⅰ级评分表达式")]
        [StringLength(200)]
        public string StLevelExpression { get; set; }
        
        /// <summary>
        /// Ⅱ级评分表达式
        /// </summary>
        [Description("Ⅱ级评分表达式")]
        [StringLength(200)]
        public string NdLevelExpression { get; set; }
        
        /// <summary>
        /// Ⅲ级评分表达式
        /// </summary>
        [Description("Ⅲ级评分表达式")]
        [StringLength(200)]
        public string RdLevelExpression { get; set; }
        
        /// <summary>
        /// Ⅳa级评分表达式
        /// </summary>
        [Description("Ⅳa级评分表达式")]
        [StringLength(200)]
        public string ThALevelExpression { get; set; }
        
        /// <summary>
        /// Ⅳb级评分表达式
        /// </summary>
        [Description("Ⅳb级评分表达式")]
        [StringLength(200)]
        public string ThBLevelExpression { get; set; }

        /// <summary>
        /// 默认Ⅰ级评分表达式
        /// </summary>
        [Description("默认Ⅰ级评分表达式")]
        [StringLength(200)]
        public string DefaultStLevelExpression { get; set; }

        /// <summary>
        /// 默认Ⅱ级评分表达式
        /// </summary>
        [Description("默认Ⅱ级评分表达式")]
        [StringLength(200)]
        public string DefaultNdLevelExpression { get; set; }

        /// <summary>
        /// 默认Ⅲ级评分表达式
        /// </summary>
        [Description("默认Ⅲ级评分表达式")]
        [StringLength(200)]
        public string DefaultRdLevelExpression { get; set; }

        /// <summary>
        /// 默认Ⅳa级评分表达式
        /// </summary>
        [Description("默认Ⅳa级评分表达式")]
        [StringLength(200)]
        public string DefaultThALevelExpression { get; set; }

        /// <summary>
        /// 默认Ⅳb级评分表达式
        /// </summary>
        [Description("默认Ⅳb级评分表达式")]
        [StringLength(300)]
        public string DefaultThBLevelExpression { get; set; }
    }
}