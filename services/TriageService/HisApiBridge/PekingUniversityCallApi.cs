using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.TriageService.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TriageService.HisApiBridge.Model;
using Volo.Abp.Uow;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>    
    /// 对接叫号系统相关方法
    /// </summary>
    public partial class PekingUniversityCallApi : ICallApi
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PekingUniversityCallApi> _log;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true // Optional: Make the JSON output indented for readability
        };
        public PekingUniversityCallApi(IConfiguration configuration, IUnitOfWorkManager unitOfWorkManager,
                                        IHttpClientHelper httpClientHelper, ILogger<PekingUniversityCallApi> log)
        {
            this._configuration = configuration;
            this._unitOfWorkManager = unitOfWorkManager;
            this._log = log;
            this._httpClientHelper = httpClientHelper;
        }

        /// <summary>
        /// 根据新dto与数据库对比，确定调用的Call接口
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dtoPatient"></param>
        /// <param name="currentDBPatient"></param>
        /// <returns></returns>
        public async Task<CommonHttpResult<PatientInfo>> GetFromCallServeic(CreateOrUpdatePatientDto dto, PatientInfo dtoPatient, PatientInfo currentDBPatient)
        {
            if ((currentDBPatient?.TriageStatus == (int)TriageStatus.Suspend) && (dto.TriageStatus == (int)TriageStatus.Triage))  //分诊
            {
                await InQueueAsync(dto, dtoPatient, currentDBPatient);
            }
            else if (
                    (currentDBPatient?.ConsequenceInfo.TriageDeptCode != dto.ConsequenceInfo.TriageDept)  // 分诊科室变更
                 || (dto.ConsequenceInfo.ChangeTriageReasonName == "过号重排" && currentDBPatient?.ConsequenceInfo.ChangeTriageReasonName != "过号重排" || currentDBPatient?.ConsequenceInfo.ActTriageLevel != dto.ConsequenceInfo.ActTriageLevel)  // 过号重排
              )
            {
                return await UpdateNewCallSnAsync(dto, dtoPatient, currentDBPatient);
            }
            else if ((!string.IsNullOrWhiteSpace(dto?.ConsequenceInfo.DoctorCode) && dto.ConsequenceInfo.DoctorCode != currentDBPatient?.ConsequenceInfo?.DoctorCode) || (!string.IsNullOrWhiteSpace(dto?.ConsequenceInfo.TriageTarget) && dto.ConsequenceInfo.TriageTarget != currentDBPatient?.ConsequenceInfo?.TriageTargetCode) || (!string.IsNullOrWhiteSpace(dto?.ConsequenceInfo.ActTriageLevel) && dto.ConsequenceInfo.ActTriageLevel != currentDBPatient?.ConsequenceInfo?.ActTriageLevel))
            {
                return await UpdateDoctorAsync(dto, dtoPatient, currentDBPatient);
            }
            else if (string.IsNullOrWhiteSpace(dto?.ConsequenceInfo.DoctorCode) && dto.ConsequenceInfo.DoctorCode != currentDBPatient?.ConsequenceInfo?.DoctorCode)
            {
                //清空医生需同步至call
                return await UpdateCallAsync(dto, dtoPatient, currentDBPatient);
            }

            return new CommonHttpResult<PatientInfo>() { Data = dtoPatient };
        }

        /// <summary>
        /// 调用Call接口，患者新入队列
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dtoPatient"></param>
        /// <param name="currentPatient"></param>
        /// <returns></returns>
        private async Task<bool> InQueueAsync(CreateOrUpdatePatientDto dto, PatientInfo dtoPatient, PatientInfo currentPatient)
        {
            string msgComent = "患者新入队列";
            string uri = $"{_configuration["PekingUniversity:Call:InQueueUrl"]}";
            bool.TryParse($"{_configuration["PekingUniversity:Call:UseHisCallingSn"]}", out bool isUseHisCalligSn);
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                var content = new StringContent(JsonSerializer.Serialize(new
                {
                    RegisterNo = currentPatient.RegisterInfo?.FirstOrDefault()?.RegisterNo,
                    PatientID = dtoPatient.PatientId,
                    PatientName = dto.PatientName,
                    DoctorCode = dto.ConsequenceInfo.DoctorCode,
                    DoctorName = dto.ConsequenceInfo.DoctorName,
                    TriageDeptCode = dto.ConsequenceInfo.TriageDept,
                    TriageDeptName = dto.ConsequenceInfo.TriageDeptName,
                    ActTriageLevel = dto.ConsequenceInfo.ActTriageLevel,
                    ActTriageLevelName = dto.ConsequenceInfo.ActTriageLevelName,
                    TriageTarget = dto.ConsequenceInfo.TriageTarget,
                    TriageTargetName = dto.ConsequenceInfo.TriageTargetName
                }, options));
                var response = await _httpClientHelper.PostAsync(uri, content);
                sw.Stop();
                _log.LogInformation($"调用Call接口，{msgComent}，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {response}");
                if (string.IsNullOrEmpty(response))
                {
                    var msg = $"调用Call接口，{msgComent}失败，无返回，url：{uri}";
                    _log.LogError(msg);
                    throw new Exception(msg);
                }

                dynamic callResponse = JsonConvert.DeserializeObject<dynamic>(response);
                if (callResponse.code != 200)
                {
                    //throw new Exception($"调用Call接口，{msgComent}失败，url：{uri}，原因：{callResponse.data}");
                    _log.LogWarning("调用Call接口，{MsgComent}失败，url：{Uri}，原因：{@CallResponse}", msgComent, uri, (object)callResponse.data);
                }

                if (callResponse.data != null && !string.IsNullOrWhiteSpace((string)callResponse.data.callingSn))
                {
                    dtoPatient.CallNo = callResponse.data.callingSn;
                    if (!isUseHisCalligSn)
                    {
                        dtoPatient.CallingSn = callResponse.data.callingSn;
                    }
                }
                dtoPatient.VisitStatus = VisitStatus.WattingTreat;
                dtoPatient.LogTime = DateTime.Now;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 调用Call接口，患者获得新CallSn，患者更新就诊医生
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dtoPatient"></param>
        /// <param name="currentPatient"></param>
        /// <returns></returns>
        private async Task<CommonHttpResult<PatientInfo>> UpdateNewCallSnAsync(CreateOrUpdatePatientDto dto, PatientInfo dtoPatient, PatientInfo currentPatient)
        {
            string msgComent = "患者获得新CallSn";
            string uri = $"{_configuration["PekingUniversity:Call:UpdateNewCallSnUrl"]}";
            bool.TryParse($"{_configuration["PekingUniversity:Call:UseHisCallingSn"]}", out bool isUseHisCalligSn);
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                string jsonContent = JsonSerializer.Serialize(new
                {
                    RegisterNo = currentPatient.RegisterInfo?.FirstOrDefault()?.RegisterNo,
                    PatientID = dtoPatient.PatientId,
                    PatientName = dto.PatientName,
                    DoctorId = dto.ConsequenceInfo.DoctorCode,
                    DoctorName = dto.ConsequenceInfo.DoctorName,
                    DeptCode = dto.ConsequenceInfo.TriageDept,
                    DeptName = dto.ConsequenceInfo.TriageDeptName,
                    TriageLevel = dto.ConsequenceInfo.ActTriageLevel,
                    TriageLevelName = dto.ConsequenceInfo.ActTriageLevelName,
                    TriageTarget = dto.ConsequenceInfo.TriageTarget,
                    TriageTargetName = dto.ConsequenceInfo.TriageTargetName
                }, options);

                var response = await _httpClientHelper.PostAsync(uri, new StringContent(jsonContent));
                sw.Stop();
                _log.LogInformation($"调用Call接口，{msgComent}，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {response}");
                if (string.IsNullOrEmpty(response))
                {
                    var msg = $"调用Call接口，{msgComent}失败，无返回，url：{uri}";
                    _log.LogError(msg);
                    throw new Exception(msg);
                }

                dynamic callResponse = JsonConvert.DeserializeObject<dynamic>(response);
                if (callResponse.code != 200)
                {
                    //throw new Exception($"调用Call接口，{msgComent}失败，url：{uri}，原因：{callResponse.data}");
                    _log.LogWarning("调用Call接口，{MsgComent}失败，url：{Uri}，原因：{@CallResponse}", msgComent, uri, (object)callResponse.data);
                    return new CommonHttpResult<PatientInfo> { Data = dtoPatient, Code = callResponse.code, Msg = callResponse.message };
                }

                if (callResponse.data != null && !string.IsNullOrWhiteSpace((string)callResponse.data.callingSn))
                {
                    dtoPatient.CallNo = callResponse.data.callingSn;
                    if (!isUseHisCalligSn)
                    {
                        dtoPatient.CallingSn = callResponse.data.callingSn;
                    }
                }
                dtoPatient.VisitStatus = VisitStatus.WattingTreat;
                dtoPatient.LogTime = DateTime.Now;
                return new CommonHttpResult<PatientInfo>() { Data = dtoPatient };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 调用Call接口，患者更新就诊医生
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dtoPatient"></param>
        /// <param name="currentPatient"></param>
        /// <returns></returns>
        private async Task<CommonHttpResult<PatientInfo>> UpdateDoctorAsync(CreateOrUpdatePatientDto dto, PatientInfo dtoPatient, PatientInfo currentPatient)
        {
            string msgComent = "患者更新就诊医生";
            string uri = $"{_configuration["PekingUniversity:Call:UpdateDoctorUrl"]}";
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                string jsonContent = JsonSerializer.Serialize(new
                {
                    RegisterNo = currentPatient.RegisterInfo?.FirstOrDefault()?.RegisterNo,
                    DoctorId = dto.ConsequenceInfo.DoctorCode,
                    DoctorName = dto.ConsequenceInfo.DoctorName,
                    DeptCode = dto.ConsequenceInfo.TriageDept,
                    DeptName = dto.ConsequenceInfo.TriageDeptName,
                    TriageLevel = dto.ConsequenceInfo.ActTriageLevel,
                    TriageLevelName = dto.ConsequenceInfo.ActTriageLevelName,
                    TriageTarget = dto.ConsequenceInfo.TriageTarget,
                    TriageTargetName = dto.ConsequenceInfo.TriageTargetName
                }, options);

                var response = await _httpClientHelper.PostAsync(uri, new StringContent(jsonContent));
                sw.Stop();
                _log.LogInformation($"调用Call接口，{msgComent}，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {response}");
                if (string.IsNullOrEmpty(response))
                {
                    var msg = $"调用Call接口，{msgComent}失败，无返回，url：{uri}";
                    _log.LogError(msg);
                    throw new Exception(msg);
                }

                dynamic callResponse = JsonConvert.DeserializeObject<dynamic>(response);
                if (callResponse.code != 200)
                {
                    return new CommonHttpResult<PatientInfo> { Data = dtoPatient, Code = callResponse.code, Msg = callResponse.message };
                    //throw new Exception($"调用Call接口，{msgComent}失败，url：{uri}，原因：{callResponse.data},code:{callResponse.code}");
                }

                dtoPatient.LogTime = DateTime.Now;
                return new CommonHttpResult<PatientInfo> { Data = dtoPatient };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 更新Call服务
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dtoPatient"></param>
        /// <param name="currentPatient"></param>
        /// <returns></returns>
        private async Task<CommonHttpResult<PatientInfo>> UpdateCallAsync(CreateOrUpdatePatientDto dto, PatientInfo dtoPatient, PatientInfo currentPatient)
        {
            string msgComent = "患者更新就诊医生";
            string uri = $"{_configuration["PekingUniversity:Call:UpdateCallUrl"]}";
            Stopwatch sw = Stopwatch.StartNew();
            string jsonContent = JsonSerializer.Serialize(new
            {
                currentPatient.RegisterInfo?.FirstOrDefault()?.RegisterNo,
                dto.ConsequenceInfo.DoctorCode,
                dto.ConsequenceInfo.DoctorName
            }, options);

            var response = await _httpClientHelper.PostAsync(uri, new StringContent(jsonContent));
            sw.Stop();
            string message = $"调用Call接口，{msgComent}，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {response}";
            _log.LogInformation(message);
            if (string.IsNullOrEmpty(response))
            {
                var msg = $"调用Call接口，{msgComent}失败，无返回，url：{uri}";
                _log.LogError(msg);
                throw new Exception(msg);
            }

            dynamic callResponse = JsonConvert.DeserializeObject<dynamic>(response);
            if (callResponse.code != 200)
            {
                return new CommonHttpResult<PatientInfo> { Data = dtoPatient, Code = callResponse.code, Msg = callResponse.message };
            }

            dtoPatient.LogTime = DateTime.Now;
            return new CommonHttpResult<PatientInfo> { Data = dtoPatient };
        }

        /// <summary>
        /// 调用Call接口，获得患者列表
        /// </summary>
        /// <param name="triageDept"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<CallPatientInfo>> GetOrderListFromCallAsync(string triageDept, int pageSize)
        {
            string msgComent = "获得患者列表";
            string uri = $"{_configuration["PekingUniversity:Call:GetOrderListUrl"]}";
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                uri = $"{uri}?TriageDept={triageDept}";

                var response = await _httpClientHelper.GetAsync(uri);
                sw.Stop();
                _log.LogInformation($"调用Call接口，{msgComent}，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {response}");
                if (string.IsNullOrEmpty(response))
                {
                    var msg = $"调用Call接口，{msgComent}失败，无返回，url：{uri}";
                    _log.LogError(msg);
                    throw new Exception(msg);
                }

                dynamic callResponse = JsonConvert.DeserializeObject<dynamic>(response);
                if (callResponse.code != 200)
                {
                    throw new Exception($"调用Call接口，{msgComent}失败，url：{uri}，原因：{callResponse.data}");
                }

                string data = JsonConvert.SerializeObject(callResponse.data);
                List<CallPatientInfo> callPatientInfos = JsonConvert.DeserializeObject<List<CallPatientInfo>>(data);
                return callPatientInfos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}