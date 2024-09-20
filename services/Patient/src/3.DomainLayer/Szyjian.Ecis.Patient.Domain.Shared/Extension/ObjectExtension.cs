using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 描述：object扩展序列化json
    /// 创建人： yangkai
    /// 创建时间：2023/2/21 9:34:38
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
        /// 获取等待时长（xx小时xx分）
        /// </summary>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        public static string GetWaitingTimeString(this DateTime beginTime)
        {
            return GetWaitingTimeString(beginTime, DateTime.Now);
        }

        /// <summary>
        /// 获取等待时长（xx小时xx分）
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        private static string GetWaitingTimeString(this DateTime beginTime, DateTime endTime)
        {
            var timespan = endTime.Subtract(beginTime);
            if (timespan.TotalMinutes >= 60)
            {
                return $"{((int)timespan.TotalMinutes) / 60}小时{((int)timespan.TotalMinutes) % 60}分";
            }
            return $"{((int)timespan.TotalMinutes) % 60}分";
        }

        /// <summary>
        /// object扩展序列化json
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dateTimeformat">时间格式</param>
        /// <returns></returns>
        public static string ToJson(this object obj, string dateTimeformat)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
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
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
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
