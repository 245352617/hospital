using Newtonsoft.Json;

namespace SamJan.MicroService.PreHospital.TriageService.LGHis
{
    public class RegisterInfoInput
    {
        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string IdNo { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisitNum { get; set; }
    }
}