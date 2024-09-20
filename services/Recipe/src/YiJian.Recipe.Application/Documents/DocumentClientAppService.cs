using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Documents.Dto;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Documents
{
    /// <summary>
    /// 客户端调用服务
    /// </summary>
    public partial class DocumentsAppService
    {
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
                var token = await GetTokenAsync();
                token = token.IsNullOrEmpty() ? input_token : token;

                using (var client = _httpClientFactory.CreateClient("patient"))
                {
                    client.DefaultRequestHeaders.Add("Authorization", token);
                    string uri = _configuration["PatientInfoUri"] + piid;
                    var patientInfo = await client.GetAsync(uri);
                    if (patientInfo.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return null;
                    }

                    var content = await patientInfo.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        return null;
                    }

                    var data = JsonConvert.DeserializeObject<ResponseResult<AdmissionRecordDto>>(content);
                    if (data.Code != EHttpStatusCodeEnum.Ok)
                    {
                        return null;
                    }

                    var patient = data.Data;
                    var patientCase = await _caseRepository.FirstOrDefaultAsync(x => x.Piid == patient.PI_ID);
                    if (patientCase != null)
                    {
                        patient.PastMedicalHistory = patientCase.Pastmedicalhistory;
                        patient.NarrationName = patientCase.Narrationname;
                        patient.PresentMedicalHistory = patientCase.Presentmedicalhistory;
                        patient.PhysicalExamination = patientCase.Physicalexamination;
                    }

                    return patient;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取原则信息PI_ID={piid}异常：{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetTokenAsync()
        {
            if (_httpContext.HttpContext == null)
            {
                return null;
            }

            var token = _httpContext.HttpContext.Request.Headers["Authorization"];
            return await Task.FromResult(token.ToString());
        }
    }
}