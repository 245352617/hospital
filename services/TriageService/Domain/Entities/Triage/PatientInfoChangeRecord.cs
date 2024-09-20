using SamJan.MicroService.PreHospital.Core.BaseEntities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 病人信息变更记录表
    /// </summary>
    public class PatientInfoChangeRecord : BaseEntity<Guid>
    {
        public PatientInfoChangeRecord(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 患者Id Triage_PatientInfo表主键
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 变更字段
        /// </summary>
        [Description("变更字段")]
        [StringLength(500)]
        public string ChangeField { get; set; }

        /// <summary>
        /// 变更之前的值
        /// </summary>
        [StringLength(500)]
        [Description("变更之前的值")]
        public string BeforeValue { get; set; }

        /// <summary>
        /// 变更之后的值
        /// </summary>
        [StringLength(500)]
        [Description("变更之后的值")]
        public string AfterValue { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        [StringLength(500)]
        [Description("变更原因")]
        public string ChangeReason { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        [StringLength(500)]
        [Description("操作人编码")]
        public string OperatedCode { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        [StringLength(500)]
        [Description("操作人名称")]
        public string OperatedName { get; set; }
    }
}
