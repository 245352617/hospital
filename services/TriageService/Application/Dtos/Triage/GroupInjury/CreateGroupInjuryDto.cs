using System;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 关联群伤事件Dto
    /// </summary>
    public class CreateGroupInjuryDto
    {
        /// <summary>
        /// 群伤事件Code
        /// </summary>
        public string GroupInjuryTypeCode { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 概要说明
        /// </summary>
        [MaxLength(10, ErrorMessage = "超过最大长度!")]
        public string Description { get; set; }
        /// <summary>
        /// 事件发生事件
        /// </summary>
        public DateTime HappeningTime { get; set; }
    }
}