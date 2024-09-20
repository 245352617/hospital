using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace YiJian.ECIS.Core
{
    public class DDPHelper<T>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<DDPHelper<T>> _log;
        public DDPHelper(IHttpClientFactory httpClientFactory, ILogger<DDPHelper<T>> log)
        {
            _httpClientFactory = httpClientFactory;
            _log = log;
        }
        public async Task<DDPResult<T>> CallAsync(DDPModel model, string url, string token = "")
        {
            var result = new DDPResult<T>();
            try
            {
                using (var client = _httpClientFactory.CreateClient(url))
                {
                    client.DefaultRequestHeaders.Add("Authorization", token);
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model));
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    httpContent.Headers.ContentType.CharSet = "utf-8";
                    var emrResponse = await client.PostAsync(new Uri(url), httpContent);
                    var content = await emrResponse.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DDPResult<T>>(content);
                    _log.LogInformation("DDP接口平台调用成功：" + model.path + result.msg);
                    return (result);
                }
            }
            catch (Exception e)
            {
                _log.LogError("DDP接口平台调用报错：" + model.path + e.Message);
                return result;
            }
        }
    }
}
