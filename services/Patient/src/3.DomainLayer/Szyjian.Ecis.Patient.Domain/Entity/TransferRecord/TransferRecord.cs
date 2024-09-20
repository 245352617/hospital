using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 流转记录
    /// </summary>
    [Table(Name = "Pat_TransferRecord")]
    public class TransferRecord
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(Name = "PT_ID", IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        /// <summary>
        /// Triage_PatientInfo表ID
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        [Column(IsNullable = false)]
        [MaxLength(20)]
        public string PatientID { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 流转时间
        /// </summary>
        public DateTime TransferTime { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        [MaxLength(20)]
        public string OperatorCode { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        [MaxLength(50)]
        public string OperatorName { get; set; }

        /// <summary>
        /// 流转类型编码
        /// </summary>
        [MaxLength(50)]
        public TransferType TransferTypeCode { get; set; }

        /// <summary>
        /// 流转类型
        /// </summary>
        [MaxLength(100)]
        public string TransferType { get; set; }

        /// <summary>
        /// 来自区域编码
        /// </summary>
        [MaxLength(50)]
        public string FromAreaCode { get; set; }

        /// <summary>
        /// 转向区域编码
        /// </summary>
        [MaxLength(50)]
        public string ToAreaCode { get; set; }

        /// <summary>
        /// 转向区域
        /// </summary>
        [MaxLength(100)]
        public string ToArea { get; set; }

        /// <summary>
        /// 来自科室编码
        /// </summary>
        [MaxLength(50)]
        public string FromDeptCode { get; set; }

        /// <summary>
        /// 转向科室编码
        /// </summary>
        [MaxLength(50)]
        public string ToDeptCode { get; set; }

        /// <summary>
        /// 转向科室
        /// </summary>
        [MaxLength(100)]
        public string ToDept { get; set; }

        /// <summary>
        /// 转床信息
        /// </summary>
        public string ToBedNo { get; set; }

        /// <summary>
        /// 流转原因编码
        /// </summary>
        [MaxLength(50)]
        public string TransferReasonCode { get; set; }

        /// <summary>
        /// 流转原因
        /// </summary>
        [MaxLength(100)]
        public string TransferReason { get; set; }

        /// <summary>
        /// 补充说明
        /// </summary>
        [MaxLength(500)]
        public string SupplementaryNotes { get; set; }
    }
}