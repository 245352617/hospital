using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.WebApiClient.Model
{
    /// <summary>
    /// 从另一个平台获取以下数据不走ddp 
    /// </summary>
    public class GetLisReportListResponse
    {
        /// <summary>
        /// 检验项目代码
        /// </summary>
        [JsonProperty("reportId")]
        public string MasterItemCode { get; set; }

        /// <summary>
        /// 检验项目名称[展示]
        /// </summary>
        [JsonProperty("examineName")]
        public string MasterItemName { get; set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        [JsonProperty("examineTime")]
        //[System.Text.Json.Serialization.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? LabTime { get; set; }

        /// <summary>
        /// 检验项
        /// </summary> 
        //public List<DdpGetLisReportResponse> LisReportList { get; set; }
    }
}
