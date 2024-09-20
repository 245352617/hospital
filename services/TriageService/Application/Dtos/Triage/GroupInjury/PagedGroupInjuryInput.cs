using System;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分页查询群伤事件输入项
    /// </summary>
    public class PagedGroupInjuryInput
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        /// <example>1</example>
        [Required]
        public int SkipCount { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        /// <example>20</example>
        [Required]
        public int MaxResultCount { get; set; }

        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        
        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        
        /// <summary>
        /// 群伤事件类型Code
        /// </summary>
        public string GroupInjuryTypeCode { get; set; }

        /// <summary>
        /// 是否包含患者信息
        /// </summary>
        public bool IsIncludePatient { get; set; } = true;
    }
}