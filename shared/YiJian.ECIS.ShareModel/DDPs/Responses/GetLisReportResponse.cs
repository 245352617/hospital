using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.WebApiClient.Model
{
    /// <summary>
    /// 检验报告详情查询从另外一个平台不走ddp 
    /// </summary>
    public class GetLisReportResponse
    {
        /// <summary>
        /// 细项代码
        /// </summary>
        [JsonProperty("examineSubitemsCode")]
        public string ItemCode { get; set; }

        /// <summary>
        /// 细项名称 [指标名称]
        /// </summary>
        [JsonProperty("examineSubitemsName")]
        public string ItemChiName { get; set; }

        /// <summary>
        /// 结果值
        /// </summary>
        [JsonProperty("examineSubitemsValue")]
        public string ItemResult { get; set; }

        /// <summary>
        /// 结果单位
        /// </summary>
        [JsonProperty("examineSubitemsUint")]
        public string ItemResultUnit { get; set; }

        /// <summary>
        /// 结果值标志 N正常;L偏低;H偏高
        /// </summary>
        [JsonProperty("abnormalvalue")]
        public string ItemResultFlag { get; set; }

        /// <summary>
        /// 参考值描述
        /// </summary>
        [JsonProperty("normalValue")]
        public string ReferenceDesc { get; set; }

        /// <summary>
        /// 参考值上限
        /// </summary>
        public string ReferenceHighLimit { get; set; }

        /// <summary>
        /// 参考值下限
        /// </summary>
        public string ReferenceLowLimit { get; set; }

    }

}
