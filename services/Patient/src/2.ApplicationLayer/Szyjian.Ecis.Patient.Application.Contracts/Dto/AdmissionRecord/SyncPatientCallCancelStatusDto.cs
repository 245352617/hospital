using System;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 同步病患取消叫号状态Dto
    /// </summary>
    public class SyncPatientCallCancelStatusDto
    {
        /// <summary>
        /// 患者分诊Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 叫号状态 0：未叫号  1：叫号中  2：已叫号
        /// </summary>
        public CallStatus CallStatus { get; set; }

        /// <summary>
        /// 取消叫号时间
        /// </summary>
        public DateTime CancelTime { get; set; }
    }
}