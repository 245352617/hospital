using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YiJian.CardReader.Domain;
using YiJian.CardReader.WebServer;

namespace YiJian.Tray
{

    public class HttpReq
    {
        /// <summary>
        /// 发起http请求，获得请求返回
        /// </summary>
        /// <param name="reqUrl"> 请求地址，如：http://example.com/api/GetReportCard?patient=123&&type=4 </param>
        /// <returns></returns>
        public async Task<string> GetApiResultAsync(string reqUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                // 发送GET请求，并等待响应
                HttpResponseMessage response = await client.GetAsync(reqUrl);

                // 读取响应内容
                string responseContent = await response.Content.ReadAsStringAsync();

                // 返回响应内容
                return responseContent;
            }
        }

        public object GetObjectAsync(string content)
        {
            var jResult = JsonConvert.DeserializeObject<JsonResult>(content, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            if (jResult?.Code != 200) return new List<PrintTemplate>();

            return jResult.Data;
        }
    }
}
