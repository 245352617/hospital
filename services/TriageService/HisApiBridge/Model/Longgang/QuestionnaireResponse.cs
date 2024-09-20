using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 问卷调查 接口返回
    /// </summary>
    /// <example>{"data": null,"error_code": "0","error_msg": "记录不存在","result_code": 0}</example>
    public class QuestionnaireResponse<T>
    {
        /// <summary>
        /// 类型：Number  必有字段  备注：1 ok, 0 error
        /// </summary>
        [JsonProperty(PropertyName = "result_code")]
        public int ResultCode { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：200成功
        /// </summary>
        [JsonProperty(PropertyName = "error_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：错误信息
        /// </summary>
        [JsonProperty(PropertyName = "error_msg")]
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 类型：Object  必有字段  备注：数据
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }
}
