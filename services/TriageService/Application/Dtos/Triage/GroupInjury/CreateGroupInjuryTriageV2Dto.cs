using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 院前群伤分诊Dto
    /// </summary>
    public class CreateGroupInjuryTriageV2Dto
    {
        /// <summary>
        /// 群伤事件类型Code
        /// </summary>
        public string GroupInjuryTypeCode { get; set; }

        /// <summary>
        /// 事件发生时间
        /// </summary>
        public DateTime HappeningTime { get; set; }

        /// <summary>
        /// 开始分诊时间
        /// </summary>
        public DateTime? StartTriageTime { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 详细说明
        /// </summary>
        [MaxLength(500, ErrorMessage = "详细说明超过最大长度!")]
        public string Remark { get; set; }

        /// <summary>
        /// 概要描述
        /// </summary>
        [MaxLength(500, ErrorMessage = "概要描述超过最大长度!")]
        public string Description { get; set; }

        /// <summary>
        /// 分诊人
        /// </summary>
        public string TriageUserCode { get; set; }

        /// <summary>
        /// 分诊人名称
        /// </summary>
        public string TriageUserName { get; set; }

        /// <summary>
        /// 患者分诊记录 ID 列表
        /// </summary>
        public List<Guid> TriagePatientInfoIds { get; set; }
    }
}