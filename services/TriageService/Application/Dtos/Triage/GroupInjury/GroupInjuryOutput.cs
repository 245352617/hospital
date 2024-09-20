using System;
using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 群伤列表Dto
    /// </summary>
    public class GroupInjuryOutput
    {
        /// <summary>
        /// 群伤事件Id
        /// </summary>
        public Guid GroupInjuryInfoId { get; set; }
        
        /// <summary>
        /// 群伤事件类型
        /// </summary>
        public string GroupInjuryTypeName { get; set; }
        
        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime HappeningTime { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 概要说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 患者人数
        /// </summary>
        public string PatientCount { get; set; }
        /// <summary>
        /// Ⅰ级患者人数
        /// </summary>
        public int StLevelCount { get; set; }

        /// <summary>
        /// Ⅱ级患者人数
        /// </summary>
        public int NdLevelCount { get; set; }

        /// <summary>
        /// Ⅲ级患者人数
        /// </summary>
        public int RdLevelCount { get; set; }

        /// <summary>
        /// Ⅳa级患者人数
        /// </summary>
        public int ThaLevelCount { get; set; }

        /// <summary>
        /// Ⅳb级患者人数
        /// </summary>
        public int ThbLevelCount { get; set; }
        /// <summary>
        /// 群伤患者集合
        /// </summary>
        public ICollection<GroupInjuryPatientDto> GroupInjuryPatients { get; set; }
    }
}