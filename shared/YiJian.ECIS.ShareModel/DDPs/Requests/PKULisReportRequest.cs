using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Requests
{
    /// <summary>
    /// 描    述:获取检验报告
    /// 创 建 人:杨凯
    /// 创建时间:2023/12/28 14:09:06
    /// </summary>
    public class PKULisReportRequest
    {
        /// <summary>
        /// 申请单号
        /// </summary>
        [JsonProperty("reportNo")]
        public string ReportNo { get; set; }
    }
}
