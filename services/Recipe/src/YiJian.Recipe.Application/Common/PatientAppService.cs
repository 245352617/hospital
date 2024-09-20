using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.Documents.Dto;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Common
{
    /// <summary>
    /// 描述：Patient公共服务查询Patient基础信息
    /// 创建人： yangkai
    /// 创建时间：2022/11/22 15:30:29
    /// </summary>
    [NonController]
    public class PatientAppService : ApplicationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PatientAppService> _logger;

        /// <summary>
        /// Patient公共服务查询Patient基础信息
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public PatientAppService(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration
            , ILogger<PatientAppService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _httpContext = httpContextAccessor;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="piid"></param>
        /// <param name="input_token"></param>
        /// <returns></returns>  
        public async Task<AdmissionRecordDto> GetPatientInfoAsync(Guid piid, string input_token = "")
        {
            try
            {
                using (HttpClient client = _httpClientFactory.CreateClient("patient"))
                {
                    string uri = _configuration["PatientInfoUri"] + piid;
                    var patientInfo = await client.GetAsync(uri);
                    if (patientInfo.StatusCode != System.Net.HttpStatusCode.OK) return null;

                    var content = await patientInfo.Content.ReadAsStringAsync();
                    if (content.IsNullOrWhiteSpace()) return null;

                    var data = JsonConvert.DeserializeObject<ResponseResult<AdmissionRecordDto>>(content);
                    if (data.Code == EHttpStatusCodeEnum.Ok)
                    {
                        var patient = data.Data;
                        return patient;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取原则信息PI_ID={piid}异常：{ex.Message}");
                return null;
            }
        }
    }
}
