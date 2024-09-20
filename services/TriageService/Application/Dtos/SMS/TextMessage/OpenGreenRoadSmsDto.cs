using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 开通绿通短信Dto
    /// </summary>
    public class OpenGreenRoadSmsDto
    {
        /// <summary>
        /// 病历号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊人
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime? StartTriageTime { get; set; }

        /// <summary>
        /// 预计到达
        /// </summary>
        public DateTime? ExpectTime { get; set; }

        /// <summary>
        /// 绿色通道
        /// </summary>
        public string GreenRoadName { get; set; }

        /// <summary>
        /// 开通人员
        /// </summary>
        public string TriageUserName { get; set; }

        /// <summary>
        /// 任务单Id
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 任务单号
        /// </summary>
        public string TaskInfoNum { get; set; }
    }
}