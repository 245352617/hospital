using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.ECIS.ShareModel.Utils;

/// <summary>
/// HttpClient整合工具
/// </summary>
public class HttpClientUtil
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    /// <summary>
    /// HttpClient整合工具
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="logger"></param>
    public HttpClientUtil(HttpClient httpClient, ILogger logger)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(5);
        _httpClient.CancelPendingRequests();
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _logger = logger;
    }

    /// <summary>
    /// GET请求并且返回指定的结果就
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url">拼接好的完整URL</param>
    /// <param name="param">不想拼接的内容可以放这里</param>
    /// <param name="token">没有不需要填写</param>
    /// <returns></returns>
    public async Task<T> GetAsync<T>(string url, Dictionary<string, string> param = null, string token = "")
    {
        try
        {
            var index = url.IndexOf('?');
            var uri = string.Empty;
            if (param != null)
                uri = index > 0 ? (url + "&" + JoinDic(param)) : (url + "?" + JoinDic(param));
            else
                uri = url;

            if (!token.IsNullOrEmpty()) _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Convert.ToBase64String(Encoding.Default.GetBytes(token)));
            var requestUri = _httpClient.BaseAddress + uri;
            _logger.LogInformation($"请求Url：{requestUri}");
            var response = await _httpClient.GetAsync(requestUri);
            if (response.StatusCode == System.Net.HttpStatusCode.OK) return await BuilderResponseAsync<T>(url, response);

            throw new HttpPollyExceiption();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[GET]请求接口异常,url={url},error:{ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// DELETE请求并且返回指定的结果就
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url">拼接好的完整URL</param>
    /// <param name="param">不想拼接的内容可以放这里</param>
    /// <param name="token">没有不需要填写</param>
    /// <returns></returns>
    public async Task<T> DleteAsync<T>(string url, Dictionary<string, string> param = null, string token = "")
    {
        try
        {
            var index = url.IndexOf('?');
            var uri = index > 0 ? (url + "&" + JoinDic(param)) : (url + "?" + JoinDic(param));
            if (!token.IsNullOrEmpty()) _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Convert.ToBase64String(Encoding.Default.GetBytes(token)));
            var response = await _httpClient.DeleteAsync(uri);
            return await BuilderResponseAsync<T>(url, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[DELETE]请求接口异常,url={url},error:{ex.Message}");
            throw;
        }
    }


    /// <summary>
    /// POST请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url">请求的地址</param>
    /// <param name="postData">系列化后的json请求参数</param>
    /// <param name="token">没有不需要填写</param>
    /// <returns></returns>
    public async Task<T> PostAsync<T>(string url, string postData, string token = "")
    {
        try
        {
            if (!token.IsNullOrEmpty()) _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Convert.ToBase64String(Encoding.Default.GetBytes(token)));

            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpContent.Headers.ContentType.CharSet = "utf-8";

            var response = await _httpClient.PostAsync(url, httpContent);
            return await BuilderResponseAsync<T>(url, response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[GET]请求接口异常,url={url},error:{ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// PUT请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url">请求的地址</param>
    /// <param name="postData">系列化后的json请求参数</param>
    /// <param name="token">没有不需要填写</param>
    /// <returns></returns>
    public async Task<T> PutAsync<T>(string url, string postData, string token = "")
    {
        try
        {
            if (!token.IsNullOrEmpty()) _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Convert.ToBase64String(Encoding.Default.GetBytes(token)));

            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpContent.Headers.ContentType.CharSet = "utf-8";

            var response = await _httpClient.PutAsync(url, httpContent);
            return await BuilderResponseAsync<T>(url, response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[PUT]请求接口异常,url={url},error:{ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 构建返回的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="response"></param>
    /// <returns></returns>
    private async Task<T> BuilderResponseAsync<T>(string url, HttpResponseMessage response)
    {
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            _logger.LogInformation($"请求未成功，HttpStatusCode={response.StatusCode}");
            return default(T);
        }

        var content = await response.Content.ReadAsStringAsync();
        if (content.IsNullOrWhiteSpace())
        {
            _logger.LogInformation($"返回内容为空");
            return default(T);
        }

        var data = JsonConvert.DeserializeObject<T>(content);

        _logger.LogInformation($"请求url={url},返回结果：{content}");
        return data;
    }

    /// <summary>
    /// 拼接GET url的地址
    /// </summary>
    /// <param name="dic"></param>
    /// <returns></returns>
    private string JoinDic(Dictionary<string, string> dic)
    {
        if (dic == null) return "";
        string url = string.Empty;
        var kv = dic.Select(s => s.Key + "=" + s.Value).ToList();
        return string.Join("&", kv);
    }

}
