using SamJan.MicroService.PreHospital.Core.BaseEntities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class FastTrackSetting : BaseEntity<Guid>
    {
        public FastTrackSetting SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 快速通道名称
        /// </summary>
        [StringLength(100)]
        [Description("快速通道名称")]
        public string FastTrackName { get; set; }

        /// <summary>
        /// 快速通道电话
        /// </summary>
        [StringLength(20)]
        [Description("快速通道电话")]
        public string FastTrackPhone { get; set; }

        /// <summary>
        /// 快速通道电话和名称
        /// </summary>
        [StringLength(150)]
        [Description("快速通道电话和名称")]
        public string PhoneAndName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public bool IsEnable { get; set; }
    }
}