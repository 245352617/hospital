using FreeSql.DataAnnotations;
using System;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 时间轴记录表
    /// </summary>
    [Table(Name = "Pat_TimeAxisRecord")]
    public class TimeAxisRecord
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public int PT_ID { get; set; }

        /// <summary>
        /// Triage_PatientInfo表ID
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 节点名称编码
        /// </summary>
        public string TimePointCode { get; set; }

        /// <summary>
        /// 时间节点名称
        /// </summary>
        public string TimePointName { get; private set; }

        /// <summary>
        /// 节点时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 设定时间节点名称
        /// </summary>
        /// <returns></returns>
        public TimeAxisRecord SetTimePointName()
        {
            TimePoint timePoint = Enum.Parse<TimePoint>(TimePointCode);
            TimePointName = timePoint.GetDescription();
            return this;
        }
    }
}