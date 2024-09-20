using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class UpdateAdmissionRecordByViewDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string PatientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PatientName { get; set; }


        /// <summary>
        /// 就诊状态 
        /// </summary>
        public int? VisitStatus { get; set; }

        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string Bed { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? InDeptTime { get; set; }

        /// <summary>
        /// 首诊医生编码
        /// </summary>
        public string FirstDoctorCode { get; set; }

        /// <summary>
        /// 首诊医生名称
        /// </summary>
        public string FirstDoctorName { get; set; }

        /// <summary>
        /// 责任医生编码
        /// </summary>
        public string DutyDoctorCode { get; set; }

        /// <summary>
        /// 责任医生名称
        /// </summary>
        public string DutyDoctorName { get; set; }

        /// <summary>
        /// 挂号序号
        /// </summary>
        public string RegisterNo { get; set; }


        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CallingDoctorName { get; set; }

        /// <summary>
        /// 出科原因
        /// </summary>
        public string OutDeptReasonCode { get; set; }

        /// <summary>
        /// 出科原因描述
        /// </summary>
        public string OutDeptReasonName { get; set; }
        public object OutDeptReason { get; set; }
    }
}
