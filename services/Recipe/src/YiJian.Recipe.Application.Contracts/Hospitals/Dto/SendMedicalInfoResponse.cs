using Newtonsoft.Json;
using System.Collections.Generic;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 医嘱信息节点、回传成功数据
    /// </summary>
    public class SendMedicalInfoResponse
    {
        /// <summary>
        /// 医嘱信息节点、回传成功数据
        /// </summary>
        [JsonProperty("medDetail")]
        public List<MedDetailResponse> MedDetail { get; set; }

        /// <summary>
        /// 病历号 
        /// <![CDATA[
        /// 患者主索引id、用于条形码展示
        /// ]]>
        /// </summary>
        [JsonProperty("medicalNo")]
        public string MedicalNo { get; set; }
    }
}
