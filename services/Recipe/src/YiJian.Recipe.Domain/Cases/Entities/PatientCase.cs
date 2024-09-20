using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Cases.Entities
{
    /// <summary>
    /// 患者的病例信息
    /// </summary>
    [Comment("患者的病例信息")]
    public class PatientCase : FullAuditedAggregateRoot<int>
    {

        private PatientCase()
        {

        }

        public PatientCase(Guid piid, string patientId, string patientName, string pastmedicalhistory, string presentmedicalhistory, string physicalexamination, string narrationname)
        {
            Piid = piid;
            PatientId = patientId;
            PatientName = patientName;
            Pastmedicalhistory = pastmedicalhistory;
            Presentmedicalhistory = presentmedicalhistory;
            Physicalexamination = physicalexamination;
            Narrationname = narrationname;
        }

        /// <summary>
        /// 患者唯一uuid
        /// </summary>
        [Comment("患者唯一标识")]
        public Guid Piid { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        [Comment("患者Id")]
        [StringLength(20)]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        [Comment("患者名称")]
        [StringLength(30)]
        public string PatientName { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        [Comment("既往史")]
        [StringLength(1000)]
        public string Pastmedicalhistory { get; set; }

        /// <summary>
        /// 现病史
        /// </summary>
        [Comment("现病史")]
        [StringLength(1000)]
        public string Presentmedicalhistory { get; set; }

        /// <summary>
        /// 体格检查
        /// </summary>
        [Comment("体格检查")]
        [StringLength(4000)]
        public string Physicalexamination { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        [Comment("主诉")]
        [StringLength(1000)]
        public string Narrationname { get; set; }

    }

}
