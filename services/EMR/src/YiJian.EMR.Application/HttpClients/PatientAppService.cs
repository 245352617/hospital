using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.HospitalClients;
using YiJian.EMR.HttpClients.Dto;

namespace YiJian.EMR.HttpClients;

/// <summary>
/// 
/// </summary>
public class PatientAppService : ApplicationService, IPatientAppService
{
    private readonly ILogger<PatientAppService> _logger;
    private readonly IOptionsMonitor<RemoteServices> _remoteServices;

    /// <summary>
    /// 
    /// </summary> 
    public PatientAppService(
        ILogger<PatientAppService> logger,
        IOptionsMonitor<RemoteServices> remoteServices)
    {
        _logger = logger;
        _remoteServices = remoteServices;
    }

    static readonly HttpClient Client = new HttpClient();

    /// <summary>
    /// 更新诊断被电子病历引用的标记
    /// </summary>
    /// <param name="pdid"></param>
    /// <returns></returns>
    public async Task<bool> ModifyDiagnoseRecordEmrUsedAsync(IList<int> pdid)
    {
        var remoteServices = _remoteServices.CurrentValue; 
        try
        {  
            var url = remoteServices.Patient.BaseUrl.Trim('/')+"/"+remoteServices.Patient.ModifyDiagnoseRecordEmrUsed;
             
            string content = JsonConvert.SerializeObject(pdid);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PostAsync(url, byteContent).ConfigureAwait(false);
            string result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError($"PostAsync End, url:{url}, HttpStatusCode:{response.StatusCode}, result:{result}");
                return false;
            }
            _logger.LogInformation($"PostAsync End, url:{url}, result:{result}");
            return true;
        }
        catch (WebException ex)
        {
            if (ex.Response != null)
            {
                string responseContent = await new StreamReader(ex.Response.GetResponseStream()).ReadToEndAsync();
                _logger.LogError($"更新诊断被电子病历引用的标记异常：{responseContent}");
                throw ex;
            }
            throw;
        } 
      
    } 
}

/// <summary>
/// 医嘱服务
/// </summary>
public class RecipeAppService : ApplicationService, IRecipeAppService
{
    private readonly ILogger<RecipeAppService> _logger;
    private readonly IOptionsMonitor<RemoteServices> _remoteServices;

    /// <summary>
    /// 
    /// </summary> 
    public RecipeAppService(
        ILogger<RecipeAppService> logger,
        IOptionsMonitor<RemoteServices> remoteServices)
    {
        _logger = logger;
        _remoteServices = remoteServices;
    }

    static readonly HttpClient Client = new HttpClient();

    /// <summary>
    /// 已打印则将所有的未设为导入的设为导入，方便下次导入不再重复
    /// </summary>
    /// <param name="eto"></param>
    /// <returns></returns>
    public async Task<bool> PrintedAsync(PrintedAdviceEto eto)
    {
         var remoteServices = _remoteServices.CurrentValue; 
        try
        {  
            var url = remoteServices.Recipe.BaseUrl.Trim('/')+"/"+remoteServices.Recipe.Printed; 
            string content = JsonConvert.SerializeObject(eto);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PostAsync(url, byteContent).ConfigureAwait(false);
            string result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {  
                _logger.LogError($"PostAsync End, url:{url}, HttpStatusCode:{response.StatusCode}, result:{result}");
                return false;
            }
            _logger.LogInformation($"PostAsync End, url:{url}, result:{result}");
            var jobj = JObject.Parse(result);
            return bool.Parse(jobj["data"].ToString());
           
        }
        catch (WebException ex)
        {
            if (ex.Response != null)
            {
                string responseContent = await new StreamReader(ex.Response.GetResponseStream()).ReadToEndAsync();
                _logger.LogError($"更新诊断被电子病历引用的标记异常：{responseContent}");
                throw ex;
            }
            throw;
        } 
    }
}

