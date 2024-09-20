using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 院前分诊群伤事件信息
    /// </summary>
    public class GroupInjuryInfo : BaseEntity<Guid>
    {
        public GroupInjuryInfo SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 事件发生时间
        /// </summary>
        [Description("事件发生时间")]
        public DateTime HappeningTime { get; set; }

        /// <summary>
        /// 概要说明
        /// </summary>
        [Description("概要说明")]
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// 群伤事件类型编码
        /// </summary>
        [Description("群伤事件类型")]
        [StringLength(500)]
        public string GroupInjuryCode { get; set; }

        /// <summary>
        /// 群伤事件类型名称
        /// </summary>
        [StringLength(500)]
        public string GroupInjuryName { get; set; }

    }
}