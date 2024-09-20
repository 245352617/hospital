using Abp.Application.Services.Dto;
using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class AgencyPeopleDto : EntityDto<Guid>
    {
        /// <summary>
        /// 代办人名称
        /// </summary>
        public string AgencyPeopleName { get; set; }

        /// <summary>
        /// 代办人证件号码
        /// </summary>
        public string AgencyPeopleCard { get; set; }

        /// <summary>
        /// 代办人联系电话
        /// </summary>
        public string AgencyPeopleMobile { get; set; }

        /// <summary>
        /// 代办人性别
        /// </summary>
        public string AgencyPeopleSex { get; set; }

        /// <summary>
        /// 代办人年龄
        /// </summary>

        public int AgencyPeopleAge { get; set; }

    }
}