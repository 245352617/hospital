using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class NarrationDto
    {
        /// <summary>
        /// 患者 ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        public DateTime? TriageTime { get; set; }

        /// <summary>
        /// 分诊人
        /// </summary>
        public string TriageUserCode { get; set; }

        /// <summary>
        /// 分诊人名称
        /// </summary>
        public string TriageUserName { get; set; }

        /// <summary>
        /// 主诉Code
        /// </summary>
        public string Narration { get; set; }

        /// <summary>
        /// 主诉名称
        /// </summary>
        public string NarrationName { get; set; }

        /// <summary>
        /// 主诉备注
        /// </summary>
        public string NarrationComments { get; set; }
    }
}
