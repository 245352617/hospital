using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using YiJian.ECIS.Core.Utils;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.HisDto;
using YiJian.ECIS.ShareModel.Utils;

namespace YiJian.Apis;
/// <summary>
/// 对接深圳北大医院HIS 检验检查去重等接口
/// </summary>
public class PUKHISClientAppService : IPUKHISClientAppService, ITransientDependency
{
    private string _url { get; set; }
    private const int timeoutSeconds = 10;
    private const string userid = "1009";
    private const string key = "896CFDA2-961B-4E8D-B771-E39B2CAD4734";
    /// <summary>
    /// 对接深圳北大医院 检验检查去重等接口
    /// </summary>
    /// <param name="configuration"></param>
    public PUKHISClientAppService(IConfiguration configuration)
    {
        _url = configuration["HisUrl:PUKHospital"];
    }

    /// <summary>
    /// 检查项目重复校验
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<CheckPacsXmcfResponseDto> CheckPacsXmcfAsync(CheckPacsXmcfRequestDto request)
    {
        if (request == null)
            Oh.Error("请求参数为空");

        try
        {
            string jsonString = request.ToJsonString();
            ApiClient apiClient = new ApiClient(_url, timeoutSeconds);
            var timestamp = apiClient.GetTimestamp().ToString();
            apiClient.AddHeader("timestamp", timestamp);
            apiClient.AddHeader("signature", apiClient.GetSignature(timestamp, userid, key));
            apiClient.AddHeader("userid", userid);
            var str = await apiClient.PostToStringAsync("/API/HisLis/Check_PacsXmcf", jsonString);
            str = apiClient.UnEscape(str);
            var data = JsonConvert.DeserializeObject<CheckPacsXmcfResponseDto>(str);
            Console.WriteLine($"/API/HisLis/Check_PacsXmcf Received data: {data}");
            return data;
        }
        catch (Exception ex)
        {
            Oh.Error($"检查项目重复校验/API/HisLis/Check_PacsXmcf ERROR: {ex.Message}");
        }

        return default;
    }

    /// <summary>
    /// 检验项目重复校验
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<CheckLisXmcfResponseDto> CheckLisXmcfAsync(CheckLisXmcfRequestDto request)
    {
        if (request == null)
            Oh.Error("请求参数为空");

        try
        {
            string jsonString = request.ToJsonString();
            ApiClient apiClient = new ApiClient(_url, timeoutSeconds);
            var timestamp = apiClient.GetTimestamp().ToString();
            apiClient.AddHeader("timestamp", timestamp);
            apiClient.AddHeader("signature", apiClient.GetSignature(timestamp, userid, key));
            apiClient.AddHeader("userid", userid);

            var str = await apiClient.PostToStringAsync("/API/HisLis/Check_lisxmcf", jsonString);
            str = apiClient.UnEscape(str);
            CheckLisXmcfResponseDto data = JsonConvert.DeserializeObject<CheckLisXmcfResponseDto>(str);
            Console.WriteLine($"Received data: {data}");
            return data;
        }
        catch (Exception ex)
        {
            Oh.Error($"检验项目重复校验/API/HisLis/Check_lisxmcf ERROR: {ex.Message}");
        }
        return default;
    }

}
