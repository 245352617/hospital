using System;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 患者就诊信息同步
    /// </summary>
    public class PatientVisitInfoMqDto
    {
        /// <summary>
        /// PatientInfo表Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 诊断代码
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string DiagnoseName { get; set; }
    }
}