using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 危重情况记录
    /// </summary> 
    public class CriticalIllnessDto : EntityDto<Guid>
    {
        /// <summary>
        /// 危重枚举(0 = 病危, 1=病重)
        /// </summary>  
        [Required]
        public ECriticalIllness Status { get; set; }

        /// <summary>
        /// 危重描述
        /// </summary> 
        [StringLength(20)]
        public string Remark { get; set; }

        /// <summary>
        /// 危重开始记录时间
        /// </summary> 
        public DateTime Begintime { get; set; }

        /// <summary>
        /// 危重结束时间
        /// </summary>

        public DateTime? Endtime { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary> 
        [Required, StringLength(32, ErrorMessage = "患者Id需在32字内")]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary> 
        [Required, StringLength(50, ErrorMessage = "患者名称需在50字内")]
        public string PatientName { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary> 
        public Guid NursingDocumentId { get; set; }
    }
}
