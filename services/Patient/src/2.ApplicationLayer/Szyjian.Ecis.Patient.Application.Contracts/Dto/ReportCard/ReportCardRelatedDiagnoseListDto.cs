using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 报卡关联诊断列表Dto
    /// </summary>
    public class ReportCardRelatedDiagnoseListDto
    {
        /// <summary>
        /// 报卡Guid
        /// </summary>
        [Required]
        public Guid ReportCardID { get; set; }

        /// <summary>
        /// 报卡关联诊断列表
        /// </summary>
        public List<ReportCardRelatedDiagnoseDto> RelatedDiagnoseList { get; set; } = new List<ReportCardRelatedDiagnoseDto>();
    }
}
