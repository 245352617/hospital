using SamJan.MicroService.TriageService.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 更新叫号状态实体
    /// </summary>
    public class CallStatusUpdateDto
    {
        /// <summary>
        /// 患者唯一 ID
        /// </summary>
        [Required(ErrorMessage = "")]
        public string PatientId { get; set; }

        /// <summary>
        /// 叫号系统的排队号
        /// </summary>
        public string CallingSn { get; set; }

        /// <summary>
        /// HIS 状态 
        /// 0.暂停 1.等待 2.准备 3.受理 4.完成 5.放弃 6.退号 7.暂挂
        /// </summary>
        public HisStatus CallStatus { get; set; }

        /// <summary>
        /// 叫号时间
        /// </summary>
        /// <example>2021-11-24 12:00:22</example>
        [Required(ErrorMessage = "叫号时间不能为空")]
        public string LastCalledTime { get; set; }
    }
}
