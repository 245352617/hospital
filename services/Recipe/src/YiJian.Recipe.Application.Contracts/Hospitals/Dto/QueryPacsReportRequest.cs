using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 查询检查报告信息请求参数
    /// </summary>
    public class QueryPacsReportRequest
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        [Required]
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊类型 1门急诊;2住院; 3体检
        /// </summary> 
        [JsonProperty("patientType")]
        public int PatientType { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [Required]
        [JsonProperty("visitNo")]
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号  门诊流水号/住院流水号  就诊唯一号
        /// </summary> 
        [JsonProperty("visitSerialNo")]
        public string VisitSerialNo { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonProperty("visitType")]
        public string VisitType { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
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
        [JsonProperty("reportNo")]
        public string ReportNo { get; set; }

        /// <summary>
        /// 检查类型 1:RIS放射;2:US超声; 3:ES内镜;4:病理PAT; 5:心电ECG;
        /// </summary>
        [JsonProperty("examType")]
        public string ExamType { get; set; }

    }


}