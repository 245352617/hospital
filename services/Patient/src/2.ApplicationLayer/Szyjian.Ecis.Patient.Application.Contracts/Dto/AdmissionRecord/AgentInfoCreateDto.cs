using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 代办人信息
    /// </summary>
    public class AgentInfoCreateDto : EntityDto<Guid>
    {
        /// <summary>
        /// 分诊库患者基本信息表主键ID
        /// </summary>
        public Guid PiId { get; set; }

        /// <summary>
        /// 代办人名称
        /// </summary>
        [Required, StringLength(50)]
        public string AgencyPeopleName { get; set; }

        /// <summary>
        /// 代办人证件号码
        /// </summary>
        [Required, StringLength(20)]
        public string AgencyPeopleCard { get; set; }

        /// <summary>
        /// 代办人联系电话
        /// </summary>
        public string AgencyPeopleMobile { get; set; }

    }
}