using System;
using System.ComponentModel.DataAnnotations;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 顺呼、重呼
    /// Directory: input
    /// </summary>
    public class TerminalCallDto
    {
        /// <summary>
        /// 医生 ID
        /// </summary>
        [Required]
        public string DoctorId { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 诊室编码
        /// </summary>
        [Required]
        public string ConsultingRoomCode { get; set; }

        /// <summary>
        /// Ip 诊室固定模式根据Ip 反查诊室编码
        /// </summary>
        public string IpAddr { get; set; }

        /// <summary>
        /// 诊室 Name
        /// </summary>
        public string ConsultingRoomName { get; set; }
        /// <summary>
        /// 叫号操作（0顺呼，1重呼, 2过号, 3取消呼叫）
        /// </summary>
        public int CallOperator { get; set; }

        /// <summary>
        /// 就诊号
        /// 挂号之后生成就诊号
        /// </summary>
        public string RegisterNo { get; set; }
    }

    /// <summary>
    /// 语音重呼Dto
    /// </summary>
    public class TerminalReCallDto
    {
        /// <summary>
        /// 患者Id
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 医生 ID
        /// </summary>
        [Required]
        public string DoctorId { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 诊室编码
        /// </summary>
        [Required]
        public string ConsultingRoomCode { get; set; }
    }
}