using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class CreateOrUpdateFastTrackSettingDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 快速通道名称
        /// </summary>
        public string FastTrackName { get; set; }

        /// <summary>
        /// 快速通道电话
        /// </summary>
        public string FastTrackPhone { get; set; }

        /// <summary>
        /// 快速通道电话和名称
        /// </summary>
        public string PhoneAndName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

    }
}
