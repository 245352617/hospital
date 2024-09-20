using System;
using System.Runtime.Serialization;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// CS版急诊分诊判定依据Dto
    /// </summary>
    public class TriageAccordingRecordDto
    {

        public Guid Id { get; set; }
        
        
        /// <summary>
        /// 分诊：MH_TriageRecord 唯一标识
        /// </summary>
        [DataMember]
        public Guid TID { get; set; }

        /// <summary>
        /// MH_PatientVisit 唯一标识
        /// </summary>
        [DataMember]
        public Guid PVID { get; set; }

        /// <summary>
        /// 分诊记录时间
        /// </summary>
        [DataMember]
        public DateTime RecordDT { get; set; }

        /// <summary>
        /// 系统:ID
        /// </summary>
        [DataMember]
        public string SystemID { get; set; }

        /// <summary>
        /// 主诉：ID
        /// </summary>
        [DataMember]
        public string SymptomID { get; set; }

        /// <summary>
        /// 判定依据ID
        /// </summary>
        [DataMember]
        public string AccordingItemID { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [DataMember]
        public string Operator { get; set; }
    }
}