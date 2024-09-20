using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class GroupInjurySimpleInfoDto
    {
        /// <summary>
        /// Id 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 群伤名称
        /// </summary>
        public string GroupInjuryName { get; set; }

    }
}