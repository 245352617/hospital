using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace YiJian.EMR.Templates.Entities
{
    /// <summary>
    /// 就诊流水号对应的病历信息
    /// </summary>
    [Comment("就诊流水号对应的病历信息")]
    public class ViewVisitSerialEmr
    {
        /// <summary>
        /// 病历Id
        /// </summary>
        [Comment("病历Id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 病历标题
        /// </summary>
        [Comment("病历标题")]
        [StringLength(500)]
        public string EmrTitle { get; set; }

        /// <summary>
        /// 就诊患者名称
        /// </summary>
        [Comment("就诊患者名称")]
        [StringLength(50)]
        public string PatientName { get; set; }

        /// <summary>
        /// 接诊医生名称
        /// </summary>
        [Comment("接诊医生名称")]
        [StringLength(50)]
        public string DoctorName { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        [Comment("就诊流水号")]
        [StringLength(20)]
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        [Comment("门诊号")]
        [StringLength(50)]
        public string VisitNo { get; set; }
    }
}
