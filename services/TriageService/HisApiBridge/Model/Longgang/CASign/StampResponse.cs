using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 云签【305 获取手写签名图片】接口返回
    /// </summary>
    public class StampResponse
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        [JsonProperty(PropertyName = "eventMsg")]
        public string EventMsg { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [JsonProperty(PropertyName = "eventValue")]
        public StampResponseData EventValue { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }
    }

    public class StampResponseData
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        [JsonProperty(PropertyName = "stampBase64")]
        public string StampBase64 { get; set; }
    }
}
