using System.IO;
using Newtonsoft.Json;

namespace YiJian.BodyParts
{
        /// <summary>
    /// json转换帮助类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary> 
        /// 序列化为JSON
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeObject(object value)
        {
            var timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            return JsonConvert.SerializeObject(value, Formatting.Indented, timeConverter);
        }

        /// <summary> 
        /// JSON对象反序列化 
        /// </summary> 
        /// <param name="JSON"></param>  
        /// <returns></returns> 
        public static T DeserializeObject<T>(string JSON)
        {
            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            StringReader sr = new StringReader(JSON);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
            T result = (T) json.Deserialize(reader, typeof(T));
            reader.Close();
            return result;
        }

        public static object DeserializeObject(string JSON, System.Type type)
        {
            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            StringReader sr = new StringReader(JSON);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
            object result = json.Deserialize(reader, type);
            reader.Close();
            return result;
        }

        public static T DeserializeObjectToObject<T>(object obj)
        {
            return DeserializeObject<T>(SerializeObject(obj));
        }

        /// <summary>
        /// obj to T
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T To<T>(this object obj)
        {
           return DeserializeObjectToObject<T>(obj);
        }

        #region json转T对象

        public static T FromJson<T>(this string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default(T);
            
            return JsonConvert.DeserializeObject<T>(json);
        }

        #endregion

    }
}