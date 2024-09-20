using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 挂号信息Dto
    /// </summary>
    public class RegisterInfoMqDto
    {
        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 挂号科室编码
        /// </summary>
        public string RegisterDeptCode { get; set; }

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        public string RegisterDoctorCode { get; set; }

        /// <summary>
        /// 挂号医生姓名
        /// </summary>
        public string RegisterDoctorName { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 是否已取消挂号
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}