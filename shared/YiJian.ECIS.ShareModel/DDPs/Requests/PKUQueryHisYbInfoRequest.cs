using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Requests
{
    /// <summary>
    /// 描    述 ：查询his药品医保信息
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/8/18 17:50:36
    /// </summary>
    public class PKUQueryHisYbInfoRequest
    {
        /// <summary>
        /// 药品名称
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
