using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.EMR.CloudSign.Entities
{
    /// <summary>
    /// 描述：云签信息表
    /// 创建人： yangkai
    /// 创建时间：2022/12/19 11:36:34
    /// </summary>
    public class CloudSignInfo : CreationAuditedAggregateRoot<Guid>
    {
        public CloudSignInfo(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 病历Id
        /// </summary>
        [Required, StringLength(32)]
        public Guid EMRId { get; set; }

        /// <summary>
        /// 深圳市CA业务类型编码
        /// </summary>
        [Comment("深圳市CA业务类型编码")]
        [Required, StringLength(32)]
        public string BusinessTypeCode { get; set; }

        /// <summary>
        /// 病人Id
        /// </summary>
        [Comment("病人Id")]
        public string PatientId { get; set; }

        /// <summary>
        /// 业务系统id
        /// </summary>
        [Comment("业务系统id")]
        public string BizId { get; set; }

        /// <summary>
        /// 是否进行时间戳签名
        /// </summary>
        [Comment("是否进行时间戳签名")]
        public bool WithTsa { get; set; }

        /// <summary>
        /// 云签结果状态码
        /// </summary>
        [Comment("云签结果状态码")]
        public int StatusCode { get; set; }

        /// <summary>
        /// 状态信息
        /// </summary>
        [Comment("状态信息")]
        public string EventMsg { get; set; }

        /// <summary>
        /// 签名值
        /// </summary>
        [Comment("签名值")]
        public string SignedData { get; set; }

        /// <summary>
        /// 时间戳签名值
        /// </summary>
        [Comment("时间戳签名值")]
        public string Timestamp { get; set; }
    }
}
