using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingDocuments.Entities
{
    /// <summary>
    /// 危重情况记录
    /// </summary>
    [Comment("危重情况记录")]
    public class CriticalIllness : FullAuditedAggregateRoot<Guid>
    {
        private CriticalIllness()
        {

        }

        public CriticalIllness(
            Guid id,
            Guid PiId,
            ECriticalIllness status,
            DateTime begintime,
            DateTime? endtime,
            [NotNull] string patientId,
            [NotNull] string patientName,
            Guid nursingDocumentId)
        {
            Id = id;
            Status = status;
            Remark = status.GetDescription();
            Begintime = begintime;
            Endtime = endtime;
            PatientId = Check.NotNullOrWhiteSpace(patientId, nameof(patientId), maxLength: 32);
            PatientName = Check.NotNullOrWhiteSpace(patientName, nameof(patientName), maxLength: 32);
            NursingDocumentId = nursingDocumentId;
        }

        /// <summary>
        /// 危重枚举(0 = 病危, 1=病重)
        /// </summary> 
        [Comment(" 危重枚举(0 = 病危, 1=病重)")]
        [Required]
        public ECriticalIllness Status { get; set; }

        /// <summary>
        /// 危重描述
        /// </summary>
        private string _remark;

        /// <summary>
        /// 危重描述
        /// </summary>
        [Comment("危重描述")]
        [Required, StringLength(20)]
        public string Remark
        {
            get { return _remark; }
            set
            {
                _remark = Status.GetDescription();
            }
        }

        /// <summary>
        /// 危重开始记录时间
        /// </summary>
        [Comment("危重开始记录时间")]
        public DateTime Begintime { get; set; }

        /// <summary>
        /// 危重结束时间
        /// </summary>
        [Comment("危重结束时间")]
        public DateTime? Endtime { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        [Comment("患者Id")]
        [Required, StringLength(32, ErrorMessage = "患者Id需在32字内")]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        [Comment("患者名称")]
        [Required, StringLength(50, ErrorMessage = "患者名称需在50字内")]
        public string PatientName { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        [Comment("护理记录Id")]
        public Guid NursingDocumentId { get; set; }

        /// <summary>
        /// 全过程唯一ID
        /// </summary>
        [Comment("PI_ID")]
        [Required]
        public Guid PI_ID { get; set; }
    }
}
