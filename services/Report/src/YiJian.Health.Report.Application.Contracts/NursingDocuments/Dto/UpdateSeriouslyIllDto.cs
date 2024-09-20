using System;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 更新病危病重信息Dto
    /// </summary>
    public class UpdateSeriouslyIllDto
    {
        /// <summary>
        /// 护理记录单Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 病危
        /// </summary>
        public bool? IsCriticallyIll { get; set; }

        /// <summary>
        /// 病重
        /// </summary>
        public bool? IsSeriouslyIll { get; set; }

        /// <summary>
        /// 病危病重时间
        /// </summary>
        public DateTime? SeriouslyTime { get; set; }

        /// <summary>
        /// 绿通时间
        /// </summary>
        public DateTime? GreenTime { get; set; }

        /// <summary>
        /// 绿色通道
        /// </summary>
        public bool IsGreen { get; set; }
    }
}
