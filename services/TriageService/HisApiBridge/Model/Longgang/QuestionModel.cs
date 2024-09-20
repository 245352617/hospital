using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 必有字段  备注：问卷问题列表
    /// </summary>
    public class QuestionModel
    {
        /// <summary>
        /// 类型：String  必有字段  备注：问题名称
        /// </summary>
        [JsonProperty(PropertyName = "qName")]
        public string QuestionName { get; set; }
    }
}
