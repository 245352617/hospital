using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class GroupInjuryInfoDto
    {
        /// <summary>
        /// Id 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 概要说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建群伤事件时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 群伤事件类型
        /// </summary>
        public string InjuryTypeName { get; set; }

    }
}