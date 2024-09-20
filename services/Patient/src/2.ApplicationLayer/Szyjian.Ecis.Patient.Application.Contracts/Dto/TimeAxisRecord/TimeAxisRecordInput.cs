using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 查询时间点输入项
    /// </summary>
    public class TimeAxisRecordInput
    {
        /// <summary>
        /// 患者分诊信息Id（Triage_PatientInfo表ID）
        /// </summary>
        public Guid PI_ID { get; set; }
    }
}