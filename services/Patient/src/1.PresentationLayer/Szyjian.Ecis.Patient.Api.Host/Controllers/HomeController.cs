using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Szyjian.Ecis.Patient.Api.Host.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _log;
        public HomeController(ILogger<HomeController> log)
        {
            _log = log;
        }

        /// <summary>
        /// 重定向首页
        /// </summary>
        /// <returns></returns>
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }

        [HttpPost("/api/patientService/home/proxy")]
        public async Task<IActionResult> Proxy([FromBody] ProxyEntity proxy)
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.Timeout = System.TimeSpan.FromSeconds(5);
            var method = proxy.Method.ToUpper();
            try
            {
                switch (method)
                {
                    case "GET":
                        var getResponse = await _httpClient.GetAsync(proxy.Url);
                        if (getResponse == null || getResponse.Content == null)
                            return Ok();

                        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
                        return Ok(getResponseContent);

                    case "POST":
                        var formData = new MultipartFormDataContent();
                        //var json = JsonConvert.SerializeObject(data);
                        if (!string.IsNullOrWhiteSpace(proxy.Data))
                            formData.Add(new StringContent(proxy.Data, Encoding.Unicode, "application/json"));

                        var apiResponse = await _httpClient.PostAsync(proxy.Url, formData);
                        if (apiResponse == null || apiResponse.Content == null)
                            return Ok();

                        var apiResponseContent = await apiResponse.Content.ReadAsStringAsync();
                        return Ok(apiResponseContent);
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                _log.LogError(ex, $"Home异常：{ex.Message}");
                throw;
            }
            return Ok();

        }
    }

    public class ProxyEntity
    {
        public string Method { get; set; }
        public string Data { get; set; }
        public string Url { get; set; }

    }
}