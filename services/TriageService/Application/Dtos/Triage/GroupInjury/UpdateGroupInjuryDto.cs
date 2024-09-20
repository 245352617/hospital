using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 修改病人群伤事件信息
    /// </summary>
    public class UpdateGroupInjuryDto
    {
        /// <summary>
        /// 事件类型Code
        /// </summary>
        public string GroupInjuryTypeCode { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 概要说明
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 事件发生事件
        /// </summary>
        public DateTime HappeningTime { get; set; }
    }
}