using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    public static class JsonHelper
    {
        public static string SerializeObject(object value)
        {
            IsoDateTimeConverter isoDateTimeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss"
            };
            return JsonConvert.SerializeObject(value, Formatting.Indented, isoDateTimeConverter);
        }

        public static T DeserializeObject<T>(string text)
        {
            JsonSerializer obj = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(text));
            T result = (T)obj.Deserialize(jsonTextReader, typeof(T));
            jsonTextReader.Close();
            return result;
        }

        public static object DeserializeObject(string text, Type type)
        {
            JsonSerializer obj = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(text));
            object? result = obj.Deserialize(jsonTextReader, type);
            jsonTextReader.Close();
            return result;
        }

        public static T DeserializeObjectToObject<T>(object obj)
        {
            return DeserializeObject<T>(SerializeObject(obj));
        }

        public static T To<T>(this object obj)
        {
            if (obj == null)
            {
                return default(T);
            }

            if (!(obj is string))
            {
                return DeserializeObjectToObject<T>(obj);
            }

            return obj.ToString().FromJson<T>();
        }

        public static T FromJson<T>(this string json)
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            return default(T);
        }
    }
}
