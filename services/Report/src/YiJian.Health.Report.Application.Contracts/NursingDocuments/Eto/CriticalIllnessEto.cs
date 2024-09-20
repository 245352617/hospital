using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using YiJian.ECIS.ShareModel.Models;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingDocuments.Eto
{
    /// <summary>
    /// 病危病重信息Dto
    /// </summary>
    public class CriticalIllnessEto
    {
        /// <summary>
        /// 全过程唯一ID
        /// </summary> 
        [Required]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 危重枚举(0 = 病危, 1=病重)
        /// </summary>
        [Required]
        public ECriticalIllness Status { get; set; }

        /// <summary>
        /// 危重开始记录时间
        /// </summary>
        [Required]
        [JsonConverter(typeof(DatetimeJsonConvert))]
        public DateTime Begintime { get; set; }

        /// <summary>
        /// 危重结束时间
        /// </summary>
        [JsonConverter(typeof(DatetimeJsonConvert))]
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

    }
}
