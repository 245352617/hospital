using Nancy.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using YiJian.BodyParts.Domain.Shared.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Volo.Abp.MultiTenancy;

namespace YiJian.BodyParts.Repository
{
    public static class HttpGetPost
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static List<T> JSONStringToList<T>(this string JsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
            return objs;
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// 反序列化xml字符为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param> 
        /// <returns></returns>
        public static T DeSerialize<T>(string xml)
            where T : new()
        {
            try
            {
                T cloneObject = default(T);

                XmlSerializer serializer = new XmlSerializer(typeof(T));

                StringBuilder buffer = new StringBuilder();
                buffer.Append(xml);
                using (TextReader reader = new StringReader(buffer.ToString()))
                {
                    cloneObject = (T)serializer.Deserialize(reader);
                }

                return cloneObject;
            }
            catch (Exception e)
            {
                return default(T);
            }
        }


        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns>Json</returns>
        public static string HttpGet(string Url, string postDataStr, string contentType = "json;charset=UTF-8")
        {
            try
            {
                Log.Information(Url);
                Log.Information(postDataStr); //Uri.EscapeDataString(postDataStr)
                if (string.IsNullOrWhiteSpace(Url))
                    return string.Empty;

                HttpWebRequest request =
                    (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr /*Uri.EscapeDataString(postDataStr)//考虑到URL中的时间格式含有空格转义会请求不到的情况，故加此转义逻辑*/);
                request.Method = "GET";
                request.ContentType = contentType;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns>Json</returns>
        public static string HttpPost(string Url, string postDataStr)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Url))
                    return string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = postDataStr.Length;
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write(postDataStr);
                writer.Flush();
                writer.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码 
                }

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string retString = reader.ReadToEnd();
                reader.Close();
                return retString;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return null;
            }
        }

        public static string HttpPost(string Url, string postDataStr, Encoding encoding)
        {
            return HttpUtil.HttpPost(Url, postDataStr, encoding);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns>Json</returns>
        public static string HttpPostForm(string Url, List<KeyValuePair<string, string>> values)
        {
            return HttpUtil.HttpPostForm(Url, values);
        }

    }
}