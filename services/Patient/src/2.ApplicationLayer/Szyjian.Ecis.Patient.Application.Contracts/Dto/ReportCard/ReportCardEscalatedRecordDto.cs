using System;
using System.ComponentModel.DataAnnotations;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 报卡上报记录表
    /// </summary>
    public class ReportCardEscalatedRecordDto
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        [Required]
        public string PatientID { get; set; }

        ///// <summary>
        ///// 患者分诊id
        ///// </summary>
        //public Guid PIID { get; set; }

        /// <summary>
        /// 对应报卡编码
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ReportCardCode { get; set; }

        ///// <summary>
        ///// 对应报卡名
        ///// </summary>
        //[MaxLength(50)]
        //public string ReportCardName { get; set; }

        ///// <summary>
        ///// 是否已上报
        ///// </summary>
        //public bool IsEscalated { get; set; } = true;

        /// <summary>
        /// 报卡上报时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}