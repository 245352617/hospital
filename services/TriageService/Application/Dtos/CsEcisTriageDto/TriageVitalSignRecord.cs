using System;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// CS版急诊分诊生命体征Dto
    /// </summary>
    public class TriageVitalSignRecordDto
    {
        public Guid Id { get; set; }

        public Guid PVID { get; set; }

        public Guid TID { get; set; }

        public DateTime? RecordDT { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        public string SBP { get; set; }

        public string SDP { get; set; }

        public string SPO2 { get; set; }

        public string BreathRate { get; set; }

        public string Temp { get; set; }

        public string HeartRate { get; set; }

        public string Operator { get; set; }

        public string VitalSignMemo { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }
    }
}