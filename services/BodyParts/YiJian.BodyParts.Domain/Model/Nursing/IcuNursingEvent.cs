using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表:护理记录表
    /// </summary>
    public class IcuNursingEvent : Entity<Guid>
    {
        public IcuNursingEvent() { }

        public IcuNursingEvent(Guid id) : base(id) { }

        /// <summary>
        /// 事件类型
        /// </summary>
        public EventTypeEnum EventType { get; set; }

        /// <summary>
        /// 护理日期
        /// </summary>
        [Required]
        public DateTime NurseDate { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        [Required]
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [StringLength(50)]
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(4000)]
        [Required]
        public string Context { get; set; }

        /// <summary>
        /// 皮肤情况描述
        /// </summary>
        [StringLength(4000)]
        [CanBeNull]
        public string SkinDescription { get; set; }

        /// <summary>
        /// 处理措施
        /// </summary>
        [StringLength(4000)]
        [CanBeNull]
        public string Measure { get; set; }

        /// <summary>
        /// 护士工号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string NurseName { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? RecordTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 审核人
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string AuditNurseCode { get; set; }

        /// <summary>
        /// 审核人名称
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string AuditNurseName { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>

        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 审核状态（0-未审核，1，已审核，2-取消审核）
        /// </summary>
        public int? AuditState { get; set; }

        /// <summary>
        /// 签名记录编号对应icu_signature的id
        /// </summary>

        public Guid? SignatureId { get; set; }

        /// <summary>
        /// 审核者签名
        /// </summary>
        public Guid? AuditSignatureId { get; set; }

        /// <summary>
        /// 护理记录表
        /// </summary>
        public int? SortNum { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        [Required]
        public int ValidState { get; set; } = 1;
    }
}
