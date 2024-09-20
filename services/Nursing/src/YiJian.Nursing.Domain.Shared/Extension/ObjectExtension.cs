using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace YiJian.Nursing
{
    /// <summary>
    /// 描述：object扩展序列化json
    /// 创建人： yangkai
    /// 创建时间：2022/11/9 20:05:01
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// object扩展序列化json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// object扩展序列化json
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dateTimeformat">时间格式</param>
        /// <returns></returns>
        public static string ToJson(this object obj, string dateTimeformat)
        {
            JsonSerializerSettings jsonSerializerSettings = new()
            {
                DateFormatString = dateTimeformat,
            };
            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }

        /// <summary>
        /// object扩展序列化json
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isCamelCase">是否使用小写驼峰</param>
        /// <returns></returns>
        public static string ToJson(this object obj, bool isCamelCase)
        {
            if (isCamelCase)
            {
                JsonSerializerSettings jsonSerializerSettings = new()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
            }
            else
            {
                return ToJson(obj);
            }
        }
    }
}
