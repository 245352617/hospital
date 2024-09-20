using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Requests
{
    /// <summary>
    /// 描述：查询his医嘱
    /// 创建人： yangkai
    /// 创建时间：2023/5/4 13:52:47
    /// </summary>
    public class PKUQueryHisRecipeRequest
    {
        /// <summary>
        /// 患者Id
        /// </summary>
        [JsonProperty("patientId")]
        public string PatientId { get; set; } = string.Empty;

        /// <summary>
        /// 就诊流水号
        /// </summary>
        [JsonProperty("visitNo")]
        public string VisitNo { get; set; } = string.Empty;
    }
}
