using System;
using System.Text.Json.Serialization;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 护理单请求参数
    /// </summary>
    public class PatientNursingDocumentRequestDto
    {
        /// <summary>
        /// 全程唯一患者Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 当前入院护理单的开始时间
        /// </summary>
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? Begintime { get; set; }

        /// <summary>
        /// 当前入院护理单的结束时间
        /// </summary>
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? Endtime { get; set; }

        /// <summary>
        /// 护理记录单SheetIndex,第一页=0
        /// </summary>
        public int? SheetIndex { get; set; }

        /// <summary>
        /// 请求来源 ,交接班或会诊："HandoverOrGroupConsultation"，为空则是护理
        /// </summary>
        public string QueryFrom { get; set; }
    }
}