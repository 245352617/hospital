using System;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 患者叫号信息同步
    /// </summary>
    public class PatientCallInfoMqDto
    {
        /// <summary>
        /// PatientInfo表Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 叫号医生编码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 叫号医生名称
        /// </summary>
        public string DoctorName { get; set; }
    }
}