using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 龙岗医院 RFID API
    /// </summary>
    public class LonggangRfidApi:IRfidApi
    {
        private readonly ILogger<LonggangRfidApi> _log;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientHelper _httpClientHelper;

        public LonggangRfidApi(ILogger<LonggangRfidApi> log, IConfiguration configuration, IHttpClientHelper httpClientHelper)
        {
            _log = log;
            _configuration = configuration;
            _httpClientHelper = httpClientHelper;
        }
        
        /// <summary>
        /// RFID 绑定
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="registerInfo"></param>
        public async Task BindAsync(PatientInfo patientInfo, RegisterInfo registerInfo)
        {
            try
            {
                _log.LogInformation("RFID 绑定开始");
                var apiKey = _configuration["Settings:RFID:Longgang:ApiKey"];
                var uri = "http://" + _configuration["Settings:RFID:Longgang:Ip"] + ":" +
                          _configuration["Settings:RFID:Longgang:Port"] +
                          _configuration["Settings:RFID:Longgang:BindUri"];
                var req = new LonggangBindReq
                {
                    RegisterName = patientInfo.PatientName,
                    TagId = patientInfo.RFID,
                    VisitNumber = registerInfo.RegisterNo
                };
                
                switch (patientInfo.GreenRoadName)
                {
                    case "卒中":
                        req.Triage = 1;
                        break;
                    case "胸痛":
                        req.Triage = 2;
                        break;
                    case "创伤":
                        req.Triage = 4;
                        break;
                    case "急性呼吸衰竭":
                        req.Triage = 8;
                        break;
                    case "急性心衰":
                        req.Triage = 16;
                        break;
                    case "重型颅脑损伤":
                        req.Triage = 32;
                        break;
                }

                var reqStr = JsonSerializer.Serialize(req, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                
                _log.LogInformation("RFID 绑定接口调用请求参数：{Req}",reqStr);
                var content = new StringContent(reqStr);
                var resp = await _httpClientHelper.PostAsync(uri, content, new Dictionary<string, string>
                    {{"api-key", apiKey}});
                _log.LogInformation("RFID 绑定接口调用结束，返回结果：{Resp}", resp);
            }
            catch (Exception e)
            {
                _log.LogError("RFID 绑定错误，原因：{Msg}",e);
            }
        }
    }
}