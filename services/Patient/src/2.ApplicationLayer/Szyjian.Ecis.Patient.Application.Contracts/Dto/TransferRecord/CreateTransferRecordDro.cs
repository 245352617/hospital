using System;
using System.ComponentModel.DataAnnotations;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class CreateTransferRecordDro
    {
        /// <summary>
        /// Triage_PatientInfo表ID
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientID { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string OperatorCode { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 流转类型编码
        /// </summary>
        public TransferType TransferTypeCode { get; set; } = TransferType.NoInput;
        /// <summary>
        /// 转向区域编码
        /// </summary>
        [MaxLength(50, ErrorMessage = "转向区域最大长度50"), Required(ErrorMessage = "转向区域必填")]
        public string ToAreaCode { get; set; }

        /// <summary>
        /// 来自区域编码
        /// </summary>
        public string FromAreaCode { get; set; }

        /// <summary>
        /// 转向区域
        /// </summary>
        public string ToArea { get; set; }

        /// <summary>
        /// 来自科室编码
        /// </summary>
        public string FromDeptCode { get; set; }

        /// <summary>
        /// 转向科室编码
        /// </summary>
        [MaxLength(50, ErrorMessage = "转向科室最大长度50"), Required(ErrorMessage = "转向科室必填")]
        public string ToDeptCode { get; set; }

        /// <summary>
        /// 转向科室
        /// </summary>
        public string ToDept { get; set; }

        /// <summary>
        /// 转床信息
        /// </summary>
        public string ToBedNo { get; set; }

        /// <summary>
        /// 流转原因编码
        /// </summary>
        [MaxLength(50, ErrorMessage = "流转原因最大长度50"), Required(ErrorMessage = "流转原因必填")]
        public string TransferReasonCode { get; set; }

        /// <summary>
        /// 流转原因
        /// </summary>
        public string TransferReason { get; set; }
        /// <summary>
        /// 补充说明
        /// </summary>
        public string SupplementaryNotes { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string TriageLevel { get; set; }

        /// <summary>
        /// 级别名称
        /// </summary>
        public string TriageLevelName { get; set; }



    }
}