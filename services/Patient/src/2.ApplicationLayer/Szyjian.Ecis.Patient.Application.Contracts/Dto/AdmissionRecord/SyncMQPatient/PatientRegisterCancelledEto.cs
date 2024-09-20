using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 患者取消挂号消息体
    /// </summary>
    public class PatientRegisterCancelledEto
    {
        /// <summary>
        /// 患者分诊 ID
        /// </summary>
        public Guid PI_ID { get; set; }
    }
}
