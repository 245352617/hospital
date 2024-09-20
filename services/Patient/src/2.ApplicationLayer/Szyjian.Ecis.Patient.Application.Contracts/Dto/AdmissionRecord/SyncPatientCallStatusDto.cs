using System;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 同步病患叫号状态Dto
    /// </summary>
    public class SyncPatientCallStatusDto
    {
        /// <summary>
        /// 患者分诊Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 叫号状态 0：未叫号  1：叫号中  2：已叫号  3: 已退号
        /// </summary>
        public CallStatus CallStatus { get; set; }

        /// <summary>
        /// 叫号时间
        /// </summary>
        public DateTime? CallTime { get; set; }

        /// <summary>
        /// 叫号诊室编码
        /// </summary>
        public string CallConsultingRoomCode { get; set; }

        /// <summary>
        /// 叫号诊室编码
        /// </summary>
        public string CallConsultingRoomName { get; set; }

        /// <summary>
        /// 叫号医生工号
        /// </summary>
        public string CallingDoctorId { get; set; }

        /// <summary>
        /// 叫号医生名称
        /// </summary>
        public string CallingDoctorName { get; set; }
    }
}