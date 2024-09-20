using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using YiJian.EMR.CloudSign.Contracts;
using YiJian.EMR.CloudSign.Dto;
using YiJian.EMR.CloudSign.Entities;
using YiJian.EMR.Enums;

namespace YiJian.EMR.CloudSign
{
    /// <summary>
    /// 描述：云签服务
    /// 创建人： yangkai
    /// 创建时间：2022/12/19 11:26:22
    /// </summary>
    [RemoteService(false)]
    public class CloudSignAppService : EMRAppService
    {
        private ICloudSignInfoRepository _cloudSignInfoRepository;
        private IHttpClientFactory _httpClientFactory;
        private ILogger<CloudSignAppService> _logger;
        private CloudSign _cloudSign;

        public CloudSignAppService(ICloudSignInfoRepository cloudSignInfoRepository
            , IHttpClientFactory httpClientFactory
            , ILogger<CloudSignAppService> logger
            , IOptionsMonitor<CloudSign> cloudSign)
        {
            _cloudSignInfoRepository = cloudSignInfoRepository;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _cloudSign = cloudSign.CurrentValue;
        }

        /// <summary>
        /// 云签证书数字签名
        /// </summary>
        /// <param name="eMRId">病历Id</param>
        /// <param name="sourceData">签名原文</param>
        /// <param name="businessTypeCode">业务类型编码</param>
        /// <param name="withTsa">是否进行时间戳签名</param>
        /// <param name="patientId">病人id</param>
        /// <param name="bizId">业务系统id</param>
        /// <returns></returns>
        public async Task<bool> SigndataAsync(Guid eMRId, string sourceData, EBusinessTypeCode businessTypeCode, bool withTsa = true, Guid? patientId = null, string bizId = "ECIS")
        {
            if (!_cloudSign.UseCloudSign) return true;

            SigndataDto signdataDto = new SigndataDto();
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(sourceData);
            signdataDto.Base64SourceData = Convert.ToBase64String(bytes);
            signdataDto.BusinessTypeCode = GetBusinessTypeCode(businessTypeCode);
            signdataDto.WithTsa = withTsa;
            if (patientId != null)
            {
                signdataDto.PatientId = patientId.ToString();
            }
            signdataDto.BizId = bizId;

            signdataDto.BusinessOrgCode = _cloudSign.BusinessOrgCode;
            signdataDto.BusinessSystemCode = _cloudSign.BusinessSystemCode;
            signdataDto.BusinessSystemAppID = _cloudSign.BusinessSystemAppID;
            signdataDto.EncryptedToken = string.Empty;//等待对接平台获取

            CloudSignResultDto<SigndataResultDto> cloudSignResultDto = await GetCloudSignInfoAsync<SigndataResultDto>(signdataDto, _cloudSign.SignData);
            if (cloudSignResultDto == null) return false;

            CloudSignInfo cloudSignInfo = await _cloudSignInfoRepository.FindAsync(x => x.EMRId == eMRId);
            if (cloudSignInfo == null)
            {
                cloudSignInfo = new CloudSignInfo(Guid.NewGuid());
                cloudSignInfo.EMRId = eMRId;
                cloudSignInfo.BusinessTypeCode = signdataDto.BusinessTypeCode;
                cloudSignInfo.PatientId = signdataDto.PatientId;
                cloudSignInfo.BizId = bizId;
                cloudSignInfo.WithTsa = withTsa;
                cloudSignInfo.StatusCode = cloudSignResultDto.StatusCode;
                cloudSignInfo.EventMsg = cloudSignResultDto.EventMsg;
                if (cloudSignResultDto.StatusCode == 0 && cloudSignResultDto.EventValue != null)
                {
                    cloudSignInfo.SignedData = cloudSignResultDto.EventValue.SignedData;
                    cloudSignInfo.Timestamp = cloudSignResultDto.EventValue.Timestamp;
                }
                await _cloudSignInfoRepository.InsertAsync(cloudSignInfo);
            }
            else
            {
                cloudSignInfo.BusinessTypeCode = signdataDto.BusinessTypeCode;
                cloudSignInfo.PatientId = signdataDto.PatientId;
                cloudSignInfo.BizId = bizId;
                cloudSignInfo.WithTsa = withTsa;
                cloudSignInfo.StatusCode = cloudSignResultDto.StatusCode;
                cloudSignInfo.EventMsg = cloudSignResultDto.EventMsg;
                if (cloudSignResultDto.StatusCode == 0 && cloudSignResultDto.EventValue != null)
                {
                    cloudSignInfo.SignedData = cloudSignResultDto.EventValue.SignedData;
                    cloudSignInfo.Timestamp = cloudSignResultDto.EventValue.Timestamp;
                }
                await _cloudSignInfoRepository.UpdateAsync(cloudSignInfo);
            }

            return cloudSignResultDto.StatusCode == 0;
        }

        /// <summary>
        /// 云签证书数字签名
        /// </summary>
        /// <param name="eMRId">病历Id</param>
        /// <param name="sourceData">签名原文</param>
        /// <param name="businessTypeCode">业务类型编码</param>
        /// <returns></returns>
        public async Task<bool> SigndataAsync(Guid eMRId, string sourceData, EBusinessTypeCode businessTypeCode)
        {
            return await SigndataAsync(eMRId, sourceData, businessTypeCode, true, null, "ECIS");
        }

        /// <summary>
        /// 云签证书数字签名验证
        /// </summary>
        /// <param name="eMRId"></param>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        public async Task<bool> VerifySigndataAsync(Guid eMRId, string sourceData)
        {
            if (!_cloudSign.UseCloudSign) return true;

            CloudSignInfo cloudSignInfo = await _cloudSignInfoRepository.FindAsync(x => x.EMRId == eMRId);
            if (cloudSignInfo == null || cloudSignInfo.StatusCode != 0 || string.IsNullOrEmpty(cloudSignInfo.SignedData)) return true;

            VerifySignDataDto verifySignDataDto = new VerifySignDataDto();
            verifySignDataDto.BusinessOrgCode = _cloudSign.BusinessOrgCode;
            verifySignDataDto.RelBizNo = string.Empty;//等待对接平台获取
            verifySignDataDto.SignCert = string.Empty;//等待对接平台获取
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(sourceData);
            verifySignDataDto.SourceData = Convert.ToBase64String(bytes);
            verifySignDataDto.SignedData = cloudSignInfo.SignedData;

            CloudSignResultDto<string> cloudSignResultDto = await GetCloudSignInfoAsync<string>(verifySignDataDto, _cloudSign.Verify);
            if (cloudSignResultDto != null && cloudSignResultDto.StatusCode == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 删除云签证书数字签名
        /// </summary>
        /// <param name="eMRId"></param>
        /// <returns></returns>
        public async Task DeleteSigndataAsync(Guid eMRId)
        {
            await _cloudSignInfoRepository.DeleteAsync(x => x.EMRId == eMRId);
        }

        private string GetBusinessTypeCode(EBusinessTypeCode businessTypeCode)
        {
            switch (businessTypeCode)
            {
                case EBusinessTypeCode.Login: return "001";
                case EBusinessTypeCode.Prescription: return "002";
                case EBusinessTypeCode.Recipe: return "003";
                case EBusinessTypeCode.Lis: return "004";
                case EBusinessTypeCode.Exam: return "005";
                case EBusinessTypeCode.EMR: return "006";
                case EBusinessTypeCode.Approval: return "007";
                case EBusinessTypeCode.Test: return "998";
                case EBusinessTypeCode.Other: return "999";
                default: return "999";
            }
        }

        /// <summary>
        /// 调用云签接口获取结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private async Task<CloudSignResultDto<T>> GetCloudSignInfoAsync<T>(object data, string uri) where T : class
        {
            try
            {
                using (var client = _httpClientFactory.CreateClient("CloudSign"))
                {
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                    string jsonData = JsonConvert.SerializeObject(data, jsonSerializerSettings);
                    _logger.LogInformation($"请求云签请求信息:请求地址:{uri}    请求参数:{jsonData}");
                    StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await client.PostAsync(uri, stringContent);

                    if (result.StatusCode != System.Net.HttpStatusCode.OK) return null;
                    string content = await result.Content.ReadAsStringAsync();
                    _logger.LogInformation($"请求云签响应信息:请求地址:{uri}    响应结果:{content}");
                    if (content.IsNullOrWhiteSpace()) return null;

                    CloudSignResultDto<T> cloudSignResultDto = JsonConvert.DeserializeObject<CloudSignResultDto<T>>(content);
                    return cloudSignResultDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"调用云签接口获取结果异常：{ex.Message}");
                return null;
            }
        }
    }
}
