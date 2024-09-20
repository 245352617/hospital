using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.Emrs.Entities
{
    /// <summary>
    /// 使用过的医嘱记录信息
    /// </summary>
    [Comment("使用过的医嘱记录信息")]
    public class EmrUsedAdviceRecord : Entity<Guid>
    {
        private EmrUsedAdviceRecord()
        {

        }

        public EmrUsedAdviceRecord(Guid id, Guid doctorsAdviceId, Guid piid, DateTime usedAt, string doctorCode, string docktorName)
        {
            Id = id;
            DoctorsAdviceId = doctorsAdviceId;
            PIID = piid;
            UsedAt = usedAt;
            DoctorCode = doctorCode;
            DoctorName = docktorName;
        }

        /// <summary>
        /// 医嘱Id
        /// </summary>
        [Comment("医嘱Id")]
        public Guid DoctorsAdviceId { get; set; }

        /// <summary>
        /// 患者唯一ID
        /// </summary>
        [Comment("患者唯一ID")]
        public Guid PIID { get; set; }

        /// <summary>
        /// 使用时间（导入时间）
        /// </summary>
        [Comment("使用时间（导入时间）")]
        public DateTime UsedAt { get; set; }

        /// <summary>
        /// 使用人（导入人）打印电子病历的人
        /// </summary>
        [Comment("使用人（导入人）")]
        [StringLength(50)]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 使用人（导入人）打印电子病历的人
        /// </summary>
        [Comment("使用人（导入人）")]
        public string DoctorName { get; set; }

    }
}
