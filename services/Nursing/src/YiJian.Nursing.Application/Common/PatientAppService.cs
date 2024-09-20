using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Patient;

namespace YiJian.Common
{
    /// <summary>
    /// 描述：获取患者信息
    /// 创建人： yangkai
    /// 创建时间：2022/11/30 13:54:27
    /// </summary>
    [RemoteService(false)]
    public class PatientAppService : ApplicationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PatientAppService> _logger;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public PatientAppService(IHttpClientFactory httpClientFactory
            , IConfiguration configuration
            , ILogger<PatientAppService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="piid"></param>
        /// <returns></returns>
        public async Task<AdmissionRecordDto> GetPatientInfoAsync(Guid piid)
        {
            try
            {
                using (HttpClient client = _httpClientFactory.CreateClient("patient"))
                {
                    string uri = _configuration["PatientInfoUri"] + piid;
                    HttpResponseMessage patientInfo = await client.GetAsync(uri);
                    if (patientInfo.StatusCode != System.Net.HttpStatusCode.OK) return null;

                    var content = await patientInfo.Content.ReadAsStringAsync();
                    if (content.IsNullOrWhiteSpace()) return null;

                    ResponseResult<AdmissionRecordDto> data = JsonConvert.DeserializeObject<ResponseResult<AdmissionRecordDto>>(content);
                    if (data.Code == EHttpStatusCodeEnum.Ok)
                    {
                        AdmissionRecordDto patient = data.Data;
                        return patient;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取患者信息PI_ID={piid}异常：{ex.Message}");
                throw;
            }
        }
    }
}
