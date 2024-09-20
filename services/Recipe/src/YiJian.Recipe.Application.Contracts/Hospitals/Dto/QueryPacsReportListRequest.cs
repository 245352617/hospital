using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace YiJian.Hospitals.Dto
{

    /// <summary>
    /// 查询检查报告列表
    /// </summary>
    public class QueryPacsReportListRequest
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        [Required]
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊类型 1门急诊2住院 3体检
        /// </summary> 
        [JsonProperty("patientType")]
        public string PatientType { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [Required]
        [JsonProperty("visitNo")]
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号 就诊唯一号
        /// </summary>
        [JsonProperty("visitSerialNo")]
        public string VisitSerialNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [JsonProperty("patientName")]
        public string PatientName { get; set; }

        /// <summary>
        /// 检查类型 1:RIS放射;2:US超声; 3:ES内镜;4:病理PAT; 5:心电ECG;
        /// </summary>
        [JsonProperty("examType")]
        public string ExamType { get; set; }

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
        /// 查询类型 空或者0：院内号；1：证件号
        /// </summary>
        [JsonProperty("queryType")]
        public string QueryType { get; set; }

        /// <summary>
        /// 证件类型
        /// <![CDATA[
        /// 标准：CV02.01.101
        /// 01 居民身份证
        /// 02 居民户口簿
        /// 03 护照
        /// 04 军官证
        /// 05 驾驶证
        /// 06 港澳居民来往内地通行证
        /// 07 台湾居民来往内地通行证
        /// 109 深圳市居民健康卡
        /// 110 健康档案
        /// 111 出生医学证明
        /// 112 深圳市社会保障卡电脑号
        /// 99 其他法定有效证件
        /// ]]>
        /// </summary>
        [JsonProperty("idType")]
        public string IdType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [JsonProperty("idCode")]
        public string IdCode { get; set; }

    }

}