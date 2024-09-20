using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// CS版急诊群伤事件Dto
    /// </summary>
    public class CsEcisGroupInjuryDto
    {
        /// <summary>
        /// 群伤事件Id
        /// </summary>
        public Guid BulkinjuryId { get; set; }

        /// <summary>
        /// 群伤事件描述
        /// </summary>
        public string RecordTitle { get; set; }

        /// <summary>
        /// 群伤事件名称
        /// </summary>
        public string InjuryType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 发生日期
        /// </summary>
        public string HappenDate { get; set; }
    }
}