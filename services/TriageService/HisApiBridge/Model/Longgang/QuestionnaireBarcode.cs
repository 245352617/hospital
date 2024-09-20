using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class QuestionnaireBarcode
    {
        /// <summary>
        /// 健康码 编号
        /// </summary>
        [JsonProperty(PropertyName = "answer_id")]
        public string AnswerId { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [JsonProperty(PropertyName = "card")]
        public string Card { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// xx类型
        /// </summary>
        [JsonProperty(PropertyName = "show_type")]
        public string ShowType { get; set; }

        /// <summary>
        /// 医院ID
        /// </summary>
        [JsonProperty(PropertyName = "unit_id")]
        public string UnitId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        [JsonProperty(PropertyName = "validy_date")]
        public string ValidityDate { get; set; }
    }
}
