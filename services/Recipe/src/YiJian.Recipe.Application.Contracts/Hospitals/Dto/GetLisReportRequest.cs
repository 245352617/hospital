using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 检验报告详情查询
    /// </summary>
    public class GetLisReportRequest
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        [Required]
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊类型		1门急诊2住院 3体检
        /// </summary>
        [JsonProperty("patientType")]
        public string PatientType { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [JsonProperty("visitNo")]
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号 就诊唯一号
        /// </summary>
        [JsonProperty("visitSerialNo")]
        public string VisitSerialNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [JsonProperty("patientName")]
        public string PatientName { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        [JsonProperty("applyNo")]
        public string ApplyNo { get; set; }

        /// <summary>
        /// 报告单号
        /// </summary>
        [Required]
        [JsonProperty("reportNo")]
        public string ReportNo { get; set; }

    }

}