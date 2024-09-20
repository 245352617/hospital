using Newtonsoft.Json;

namespace YiJian.Hospitals.Dto
{

    /// <summary>
    /// 检验报告列表查询
    /// </summary>
    public class GetLisReportListRequest
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        /// <summary>
        /// 1门急诊 2住院 3体检
        /// </summary>  
        [JsonProperty("patientType")]
        public string PatientType { get; set; }

        /// <summary>
        /// 就诊号 住院号/门诊号
        /// </summary>
        [JsonProperty("visitNo")]
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        [JsonProperty("visitSerialNo")]
        public string VisitSerialNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty("patientName")]
        public string PatientName { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        [JsonProperty("endDate")]
        public string EndDate { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        [JsonProperty("applyNo")]
        public string ApplyNo { get; set; }

        /// <summary>
        /// 查询方式  空或者0：院内编号；1：身份证号
        /// </summary>
        [JsonProperty("queryType")]
        public string QueryType { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [JsonProperty("idCardNo")]
        public string IdCardNo { get; set; }

    }


}