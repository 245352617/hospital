using Newtonsoft.Json;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 报卡同步
    /// </summary>
    public class CardReportingEto
    {
        /// <summary>
        /// 卡片类型
        /// 1.普通传染病 
        ///2.性病
        ///3.结核病
        /// 4.肝炎
        /// 5.艾滋病
        /// 7.健康状况询问与医学建议卡片
        /// 11.肿瘤
        /// 12.脑卒中
        ///15.狂犬病
        /// 20.急性心肌梗死
        ///  27.食源性急病类
        /// </summary>
        [JsonProperty("cardRepType")]
        public string CardRepType { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        [JsonProperty("visSerialNo")]
        public string VisSerialNo { get; set; }
        /// <summary>
        /// 报卡内容xml
        /// </summary>
        [JsonProperty("cardDataXml")]
        public string CardDataXml { get; set; }

    }
}