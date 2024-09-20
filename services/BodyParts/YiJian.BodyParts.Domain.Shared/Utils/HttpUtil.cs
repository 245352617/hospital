using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog;

namespace YiJian.BodyParts.Domain.Shared.Utils
{
    public class HttpUtil
    {
        private static HttpClient _client;

        static HttpUtil()
        {
            _client = new HttpClient();
        }
        
        
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns>Json</returns>
        public static string HttpPost(string Url, string postDataStr, Encoding encoding)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Url))
                    return string.Empty;

                var content = new StringContent(postDataStr, encoding, "application/json");
                var response = _client.PostAsync(Url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }

                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return null;
            }
        }
        
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns>Json</returns>
        public static string HttpPostForm(string Url, List<KeyValuePair<string, string>> values)
        {
            try
            {
                var res = "";
                var postContent = new MultipartFormDataContent();
                string boundary = string.Format("--{0}", DateTime.Now.Ticks.ToString("x"));
                postContent.Headers.Add("ContentType", $"multipart/form-data, boundary={boundary}");

                foreach (var keyValuePair in values)
                {
                    postContent.Add(new StringContent(keyValuePair.Value),
                        String.Format("\"{0}\"", keyValuePair.Key));
                }

                Task<HttpResponseMessage> response = _client.PostAsync(Url, postContent);
                //浏览器出参返回入res
                if (response.Result.IsSuccessStatusCode)
                {
                    res = response.Result.Content.ReadAsStringAsync().Result;
                }

                return res;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return null;
            }
        }
    }
}