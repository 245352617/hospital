using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace YiJian.ECIS.ShareModel.Utils;
/// <summary>
/// 简单的httpClient 请求
/// </summary>
public class ApiClient
{
    private HttpClient _httpClient;


    /// <summary>
    /// 获取签名
    /// </summary>
    /// <returns></returns>
    public string GetSignature(string timeStamp, string userid, string key)
    {
        string source = userid + timeStamp + key;
        byte[] b = Encoding.UTF8.GetBytes(source);
        using (var md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(b);
            string sign = Convert.ToBase64String(hash).ToUpper();
            return sign;
        }
    }

    /// <summary>
    /// 获取当前时间搓
    /// </summary>
    /// <returns></returns>
    public long GetTimestamp()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    /// <summary>
    /// 去除转义 （his返回的是 序列化之后的str字符串 ， 需要去转义之后再反序列化）
    /// </summary>
    /// <returns></returns>
    public string UnEscape(string str)
    {
        if (!str.IsNullOrWhiteSpace())
        {
            str = str.ReplaceFirst("\"", "");
            str = str[0..(str.Length - 1)];
            str = str.Replace("\\\"", "\"");
        }
        return str;
    }

    public ApiClient(string baseAddress)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(baseAddress);
    }

    public ApiClient(string baseAddress, int timeoutSeconds)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(baseAddress);
        _httpClient.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
    }

    public void AddHeader(string name, string value)
    {
        _httpClient.DefaultRequestHeaders.Add(name, value);
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            T result = await response.Content.ReadFromJsonAsync<T>();
            return result;
        }
        else
        {
            throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

        if (response.IsSuccessStatusCode)
        {
            T result = await response.Content.ReadFromJsonAsync<T>();
            return result;
        }
        else
        {
            throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
    }

    /// <summary>
    /// http post 请求获取数据
    /// </summary>
    /// <param name="endpoint"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<string> PostToStringAsync(string endpoint, object data)
    {
        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        else
        {
            throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
    }
}

