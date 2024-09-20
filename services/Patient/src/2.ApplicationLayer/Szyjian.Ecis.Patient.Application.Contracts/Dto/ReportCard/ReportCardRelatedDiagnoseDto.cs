using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 报卡关联诊断Dto
    /// </summary>
    public class ReportCardRelatedDiagnoseDto : EntityDto<Guid>
    {
        #region Property
        /// <summary>
        /// 报卡Guid
        /// </summary>
        [Required]
        public Guid ReportCardID { get; set; }

        /// <summary>
        /// 诊断代码
        /// </summary>
        [Required, StringLength(50)]
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        [Required, StringLength(200)]
        public string DiagnoseName { get; set; }
        #endregion
    }
}
