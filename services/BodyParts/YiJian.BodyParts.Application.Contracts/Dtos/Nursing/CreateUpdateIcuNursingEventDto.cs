using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:护理记录表
    /// </summary>
    public class CreateUpdateIcuNursingEventDto : EntityDto<Guid>
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public EventTypeEnum EventType { get; set; } = EventTypeEnum.护理记录;

        /// <summary>
        /// 是否确认弹窗调用
        /// </summary>
        /// <example></example>
        public bool IsPopup { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        /// <example></example>
        public string Context { get; set; }

        /// <summary>
        /// 皮肤情况描述
        /// </summary>
        public string SkinDescription { get; set; }

        /// <summary>
        /// 处理措施
        /// </summary>
        public string Measure { get; set; }

        /// <summary>
        /// 护士工号
        /// </summary>
        /// <example></example>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        /// <example></example>
        public string NurseName { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        /// <example></example>
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 记录时间(去除秒)
        /// </summary>
        /// <example></example>
        public DateTime RecordTime2
        {
            get
            {
                return Convert.ToDateTime(RecordTime.ToString("yyyy-MM-dd HH:mm"));
            }
        }

        /// <summary>
        /// 签名内容
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        /// <example></example>
        public string AuditNurseCode { get; set; }

        /// <summary>
        /// 审核人名称
        /// </summary>
        /// <example></example>
        public string AuditNurseName { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        /// <example></example>
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 审核状态（0-未审核，1，已审核，2-取消审核）
        /// </summary>
        public int? AuditState { get; set; }

        /// <summary>
        /// 审核者签名
        /// </summary>
        public Guid? AuditSignatureId { get; set; }
    }
}
