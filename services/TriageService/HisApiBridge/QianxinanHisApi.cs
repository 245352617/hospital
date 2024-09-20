using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.PreHospital.Core.Help;
using SamJan.MicroService.PreHospital.TriageService.Application.Dtos.Triage.Patient;
using SamJan.MicroService.TriageService.Application.Dtos;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SamJan.MicroService.PreHospital.TriageService.LGHis;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 黔西南 HIS Api
    /// </summary>
    public class QianxinanHisApi : IHisApi
    {
        private readonly CommonHisApi _commonHisApi;
        private readonly IPatientInfoRepository _patientInfoRepository;
        private readonly IRegisterInfoRepository _registerInfoRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CommonHisApi> _log;
        private readonly ICurrentUser _currentUser;
        private readonly IDatabase _redis;
        private readonly IHttpContextAccessor _accessor;

        private readonly ITriageConfigAppService _triageConfigAppService;


        private static readonly AsyncRetryPolicy<HttpResponseMessage> TransientErrorRetryPolicy =
            Policy.HandleResult<HttpResponseMessage>(
                    message => ((int)message.StatusCode == 429 || (int)message.StatusCode >= 500))
                .WaitAndRetryAsync(2, retryAttempt => { return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)); });

        private static readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> CircuitBreakerPolicy =
            Policy.HandleResult<HttpResponseMessage>(message => (int)message.StatusCode == 503)
                //.AdvancedCircuitBreakerAsync(0.5,
                //    TimeSpan.FromMilliseconds(1),
                //    100,
                //    TimeSpan.FromMinutes(1))
                .CircuitBreakerAsync(2, TimeSpan.FromMilliseconds(1));

        public QianxinanHisApi(CommonHisApi commonHisApi, IPatientInfoRepository patientInfoRepository,
            IRegisterInfoRepository registerInfoRepository,
            IConfiguration configuration, IHttpClientFactory httpClientFactory, RedisHelper redisHelper,
            IHttpClientHelper httpClientHelper, ILogger<CommonHisApi> log, ICurrentUser currentUser,
            ITriageConfigAppService triageConfigAppService, IHttpContextAccessor accessor)
        {
            this._commonHisApi = commonHisApi;
            this._patientInfoRepository = patientInfoRepository;
            this._registerInfoRepository = registerInfoRepository;
            this._configuration = configuration;
            this._httpClientFactory = httpClientFactory;
            // this._httpClientHelper = httpClientHelper;
            this._log = log;
            this._currentUser = currentUser;
            _triageConfigAppService = triageConfigAppService;
            _accessor = accessor;
            this._redis = redisHelper.GetDatabase();
        }

        /// <summary>
        /// 挂号/预约/分诊
        /// </summary>
        /// <param name="patient">患者信息</param>
        /// <param name="doctorCode">医生代码</param>
        /// <param name="doctorName">医生名称</param>
        /// <param name="isUpdated">是否修改分诊信息（false：新增；true：修改）</param>
        /// <param name="hasChangedDoctor">科室、医生信息修改</param>
        /// <param name="isFirstTimePush">是否第一次分诊</param>
        /// <returns></returns>
        public async Task<PatientInfo> RegisterPatientAsync(PatientInfo patient, string doctorCode, string doctorName,
            bool isUpdated, bool hasChangedDoctor, bool isFirstTimePush)
        {
            // 黔西南是先挂号后分诊，对于三无人员，因为无挂号信息，故而不需要回写数据
            if (patient.IsNoThree)
            {
                return patient;
            }
            // 推送分诊信息
            try
            {
                StringBuilder url = new StringBuilder(_configuration["HisApiSettings:pushTriageInfo"]);
                #region 推送参数
                var registerInfo = patient.RegisterInfo?.OrderByDescending(p => p.RegisterTime).FirstOrDefault();
                var registerDeptName = "";
                //获取科室名称
                if (registerInfo != null && !string.IsNullOrWhiteSpace(registerInfo.RegisterDeptCode))
                {
                    var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.TriageDepartment.ToString());
                    registerDeptName = dicts[TriageDict.TriageDepartment.ToString()]?.FirstOrDefault(x => x.TriageConfigCode == registerInfo.RegisterDeptCode)?.TriageConfigName;
                }
                PushTriageInfoToHisDto dto = new PushTriageInfoToHisDto();
                dto.patientId = patient.PatientId;
                dto.deptCode = registerInfo?.RegisterDeptCode;
                dto.deptName = registerDeptName;
                dto.visitNo = patient.VisitNo;
                dto.regSerialNo = registerInfo?.RegisterNo;
                dto.triageLevel = patient.ConsequenceInfo.ActTriageLevel;
                dto.triageLevelName = patient.ConsequenceInfo.ActTriageLevelName;
                dto.recorderCode = _currentUser.UserName;
                dto.recorderName = _currentUser.GetFullName();
                dto.greenLogo = !string.IsNullOrWhiteSpace(patient.GreenRoadCode) ? "1" : "0";
                dto.sbp = patient.VitalSignInfo?.Sbp;
                dto.sdp = patient.VitalSignInfo?.Sdp;
                dto.spO2 = patient.VitalSignInfo?.SpO2;
                dto.breathRate = patient.VitalSignInfo?.BreathRate;
                dto.temp = patient.VitalSignInfo?.Temp;
                dto.heartRate = patient.VitalSignInfo?.HeartRate;
                dto.bloodGlucose = patient.VitalSignInfo?.BloodGlucose.ToString();
                dto.weight = patient.Weight;
                dto.consciousness = patient.VitalSignInfo?.ConsciousnessName;
                #endregion
                var response = await HisPostAsync(url, dto);
                _log.LogInformation($"调用接口平台推送分诊信息接口，url: {url}，返回: {response}。");
                if (string.IsNullOrWhiteSpace(response))
                {
                    _log.LogError("推送分诊信息失败！原因：接口响应为空");
                    throw new HisResponseException("调用推送分诊信息接口失败！请检查后重试");
                }
                var hisResponse = JsonSerializer.Deserialize<CommonHttpResult>(response);
                if (hisResponse.Code != 0)
                {
                    _log.LogError($"调用接口平台推送分诊信息失败，原因：{hisResponse.Msg}。");
                }
                return patient;
            }
            catch (Exception e)
            {
                _log.LogWarning("【QianxinanHisApi】【RegisterPatientAsync】【推送分诊信息错误】【Msg：{Msg}】", e);
                throw new Exception("调用推送分诊信息接口失败！请检查后重试");
            }
        }

        public async Task<PatientInfo> BeforeSaveTriageRecordAsync(CreateOrUpdatePatientDto dto, PatientInfo patient,
            bool isUpdated, bool hasChangedDoctor)
        {
            // 黔西南是先挂号后分诊，对于三无人员，因为无挂号信息，故而不需要回写数据
            if (dto.IsNoThree)
            {
                patient.RegisterInfo = new List<RegisterInfo>();
                return patient;
            }

            var registerInfo = await GetRegisterInfoAsync(new HisRegisterInfoQueryDto { visitNo = patient.VisitNo });
            var item = registerInfo.Count > 0 ? registerInfo[0] : throw new HisResponseException("未能获取到挂号信息");
            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.TriageDepartment.ToString());
            var dept = dicts[TriageDict.TriageDepartment.ToString()]
                ?.FirstOrDefault(x => x.TriageConfigCode == item.deptId);
            patient.RegisterInfo = new List<RegisterInfo>
            {
                new RegisterInfo
                {
                    RegisterNo = item.registerId,
                    RegisterDeptCode = dept?.TriageConfigCode,
                    RegisterDoctorCode = item.doctorCode,
                    PatientInfoId = patient?.Id ?? Guid.Empty,
                    RegisterTime = Convert.ToDateTime(item.registerDate),
                    VisitNo = item.visitNo,
                    IsCancelled = item.isCancel == "0" ? false : true,
                }
            };

            return patient;
        }

        public Task<JsonResult> SyncRegisterPatientFromHis()
        {
            return Task.FromResult(JsonResult.Ok());
        }

        /// <summary>
        /// 获取医生列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        public async Task<JsonResult<List<DoctorSchedule>>> GetDoctorScheduleAsync(string deptCode, DateTime? regDate)
        {
            var uri = _configuration["getDoctorListUrl"];
            try
            {
                var httpClient = _httpClientFactory.CreateClient("HisApi");
                httpClient.DefaultRequestHeaders.Add("Authorization",
                    _accessor.HttpContext.Request.Headers["Authorization"].ToString());
                var response = await httpClient.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("平台接口无法连接");
                }

                var responseText = await response.Content.ReadAsStringAsync();

                JsonResult<PagedResultDto<PlatformUser>> hisResponse =
                    JsonSerializer.Deserialize<JsonResult<PagedResultDto<PlatformUser>>>(responseText,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                if (hisResponse.Code != 0)
                {
                    return JsonResult<List<DoctorSchedule>>.Fail(msg: hisResponse.Msg);
                }

                if (hisResponse.Data.TotalCount <= 0)
                {
                    return JsonResult<List<DoctorSchedule>>.Ok();
                }

                var result = hisResponse.Data.Items.BuildAdapter()
                    .ForkConfig(forked =>
                    {
                        forked.ForType<PlatformUser, DoctorSchedule>()
                            .Map(dest => dest.DoctorName, src => src.Name)
                            .Map(dest => dest.DoctorCode, src => src.UserName)
                            .Map(dest => dest.DoctorTitle, src => src.Position);
                    })
                    .AdaptToType<List<DoctorSchedule>>();
                foreach (var item in result)
                {
                    item.DoctorNamePy = PyHelper.GetFirstPy(item.DoctorName);
                }

                return JsonResult<List<DoctorSchedule>>.Ok(data: result);
            }
            catch (Exception ex)
            {
                return JsonResult<List<DoctorSchedule>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="idType">
        /// 证件类型 
        /// 1: 就诊卡
        /// 2: 居民身份证
        /// </param>
        /// <param name="identityNo">身份证号码</param>
        /// <param name="visitNo">就诊号</param>
        /// <param name="patientName">姓名</param>
        /// <param name="phone">电话号码</param>
        /// <param name="regSerialNo">挂号流水号</param>
        /// <returns></returns>
        public async Task<JsonResult<List<PatientInfoFromHis>>> GetPatientRecordAsync(string idType, string identityNo,
            string visitNo, string patientName, string phone = "", string regSerialNo = "")
        {
            List<PatientInfoFromHis> res = new List<PatientInfoFromHis>();
            var uri = _configuration["HisApiSettings:getPatientInfo"] +
                      $"?idNo={identityNo}&idType={idType}&visitNo={visitNo}&name={patientName}&phone={phone}&regSerialNo={regSerialNo}";
            try
            {
                var httpClient = _httpClientFactory.CreateClient("HisApi");
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var responseText = await response.Content.ReadAsStringAsync();
                _log.LogInformation("调用平台接口查询患者信息，url: {Uri}，reponse: {ResponseText}", uri, responseText);
                if (string.IsNullOrWhiteSpace(responseText))
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "查询患者信息接口响应为空");
                    return JsonResult<List<PatientInfoFromHis>>.Fail("调用查询患者信息接口失败！请检查后重试");
                }

                var json = JObject.Parse(responseText);
                if (json["code"]?.ToString() != "0")
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", json["msg"]);
                    return JsonResult<List<PatientInfoFromHis>>.Fail("调用查询患者信息接口失败，" + json["msg"]);
                }

                if (json["data"] == null || string.IsNullOrWhiteSpace(json["data"].ToString()))
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "调用查询患者信息接口失败。返回Data为null或空");
                    return JsonResult<List<PatientInfoFromHis>>.Fail("查询患者信息失败！请检查后重试");
                }

                // // 返回单条数据
                // var resp = JsonSerializer.Deserialize<PatientRespDto>(json["data"].ToString(),
                //     new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                // 返回多条数据
                var resp = JsonSerializer.Deserialize<List<PatientRespDto>>(json["data"].ToString(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                res = resp.BuildAdapter().AdaptToType<List<PatientInfoFromHis>>();

                foreach (var patientOutput in res)
                {
                    var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync();
                    if (!string.IsNullOrWhiteSpace(patientOutput.IdTypeCode))
                    {
                        var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                            .FirstOrDefault(x => x.HisConfigCode == patientOutput.IdTypeCode);
                        if (idTypeConfig != null)
                        {
                            patientOutput.IdTypeCode = idTypeConfig.TriageConfigCode;
                            patientOutput.IdTypeName = idTypeConfig.TriageConfigName;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(patientOutput.GuardianIdTypeCode))
                    {
                        var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                            .FirstOrDefault(x => x.HisConfigCode == patientOutput.GuardianIdTypeCode);
                        if (idTypeConfig != null)
                        {
                            patientOutput.GuardianIdTypeCode = idTypeConfig.TriageConfigCode;
                            patientOutput.GuardianIdTypeName = idTypeConfig.TriageConfigName;
                        }
                    }

                    // // 信息科约定 patientId 返回的值就是 visitNo
                    // patientOutput.VisitNo = patientOutput.PatientId;
                    // 默认医保参保地【深圳】
                    var insuplcAdmdv = dicts[TriageDict.InsuplcAdmdv.ToString()]
                        .FirstOrDefault(x => x.ExtraCode == "440300");
                    patientOutput.InsuplcAdmdvCode = insuplcAdmdv?.TriageConfigCode;

                    // res = new List<PatientInfoFromHis>
                    // {
                    //     patientOutput
                    // };
                    patientOutput.Sex = patientOutput.Sex switch
                    {
                        "M" => "Sex_Man",
                        "F" => "Sex_Woman",
                        _ => "Sex_Unknown"
                    };
                }

                return JsonResult<List<PatientInfoFromHis>>.Ok(data: res);
            }
            catch (Exception ex)
            {
                return JsonResult<List<PatientInfoFromHis>>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 查询门诊患者信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<PatientInfoFromHis> GetPatienInfoBytIdAsync(string patientId)
        {
            var uri = _configuration["HisApiSettings:getPatientInfo"] +
                      $"?patientId={patientId}";
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            _log.LogInformation("调用平台接口查询患者信息，url: {Uri}，reponse: {ResponseText}", uri, responseText);
            if (string.IsNullOrWhiteSpace(responseText))
            {
                _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "查询患者信息接口响应为空");
                throw new HisResponseException("调用查询患者信息接口失败！请检查后重试");
            }

            var json = JObject.Parse(responseText);
            if (json["code"]?.ToString() != "0")
            {
                _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", json["msg"]);
                throw new HisResponseException("调用查询患者信息接口失败，" + json["msg"]);
            }

            if (json["data"] == null || string.IsNullOrWhiteSpace(json["data"].ToString()))
            {
                _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "调用查询患者信息接口失败。返回Data为null或空");
                throw new HisResponseException("查询患者信息失败！请检查后重试");
            }

            // 返回多条数据
            var resp = JsonSerializer.Deserialize<List<PatientRespDto>>(json["data"].ToString(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var res = resp.BuildAdapter().AdaptToType<List<PatientInfoFromHis>>();

            foreach (var patientOutput in res)
            {
                var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync();
                if (!string.IsNullOrWhiteSpace(patientOutput.IdTypeCode))
                {
                    var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                        .FirstOrDefault(x => x.HisConfigCode == patientOutput.IdTypeCode);
                    if (idTypeConfig != null)
                    {
                        patientOutput.IdTypeCode = idTypeConfig.TriageConfigCode;
                        patientOutput.IdTypeName = idTypeConfig.TriageConfigName;
                    }
                }

                if (!string.IsNullOrWhiteSpace(patientOutput.GuardianIdTypeCode))
                {
                    var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                        .FirstOrDefault(x => x.HisConfigCode == patientOutput.GuardianIdTypeCode);
                    if (idTypeConfig != null)
                    {
                        patientOutput.GuardianIdTypeCode = idTypeConfig.TriageConfigCode;
                        patientOutput.GuardianIdTypeName = idTypeConfig.TriageConfigName;
                    }
                }

                // 默认医保参保地【深圳】
                var insuplcAdmdv = dicts[TriageDict.InsuplcAdmdv.ToString()]
                    .FirstOrDefault(x => x.ExtraCode == "440300");
                patientOutput.InsuplcAdmdvCode = insuplcAdmdv?.TriageConfigCode;
                patientOutput.Sex = patientOutput.Sex switch
                {
                    "M" => "Sex_Man",
                    "F" => "Sex_Woman",
                    _ => "Sex_Unknown"
                };
            }

            return res.FirstOrDefault();
        }

        /// <summary>
        /// 患者建档接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult<PatientInfoFromHis>> CreatePatientRecordAsync(CreateOrGetPatientIdInput input)
        {
            // 证件类型
            TriageConfigDto idType = await GetIdTypeByTriageConfigCode(input.IdTypeCode);
            // 联系人证件类型
            TriageConfigDto guardianIdType = await GetGuardianIdTypeByTriageConfigCode(input.GuardianIdTypeCode);
            // 与联系人关系
            var societyRelation = await GetSocietyRelation(input.SocietyRelationCode);
            var uri = _configuration["HisApiSettings:buildPatientArchives"];
            var req = new PatientReqDto
            {
                name = input.PatientName,
                idNo = input.IdentityNo ?? "",
                idType = input.CardType.ToString(),
                birthday = input.Birthday?.ToString("yyyy/MM/dd"),
                phone = input.ContactsPhone ?? "",
                homeAddress = input.Address ?? "",
                nationality = input.Nation,
                contactName = input.ContactsPerson ?? "",
                sex = input.Sex switch
                {
                    "Sex_Man" => "M",
                    "Sex_Woman" => "F",
                    _ => "U"
                },
                patIdType = idType?.HisConfigCode,
                cardNo = input.CardNo ?? "",
                crowdCode = input.CrowdCode,
                associationName = input.ContactsPerson ?? "",
                associationIdType = guardianIdType?.HisConfigCode ?? "",
                associationIdNo = input.GuardianIdCardNo ?? "",
                associationPhone = input.GuardianPhone ?? "",
                associationAddress = input.ContactsAddress ?? "",
                societyRelation = societyRelation?.HisConfigCode ?? "",
            };

            try
            {
                var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.IdType.ToString());
                var res = await this.CreatePatientRecord(uri, req);
                if (string.IsNullOrWhiteSpace(res.PatientName))
                {
                    res.PatientName = input.PatientName;
                }

                if (string.IsNullOrWhiteSpace(res.Sex))
                {
                    res.Sex = input.Sex;
                }
                else
                {
                    res.Sex = res.Sex switch
                    {
                        "M" => "Sex_Man",
                        "F" => "Sex_Woman",
                        _ => "Sex_Unknown"
                    };
                }

                res.Birthday ??= input.Birthday;
                res.IdTypeCode = input.IdTypeCode;
                if (string.IsNullOrWhiteSpace(res.IdentityNo))
                {
                    res.IdentityNo = input.IdentityNo;
                }

                // // 信息科约定 patientId 返回的值就是 visitNo
                // res.VisitNo = res.PatientId;
                //if (input.CardType == 2)
                // 建档默认身份证
                {
                    var idTypeConfig = dicts[TriageDict.IdType.ToString()].FirstOrDefault(x => x.HisConfigCode == "02");
                    if (idTypeConfig != null)
                    {
                        res.IdTypeCode = idTypeConfig.TriageConfigCode;
                        res.IdTypeName = idTypeConfig.TriageConfigName;
                    }
                }
                // 查询本地对应的监护人身份证号码
                if (!string.IsNullOrWhiteSpace(input.GuardianIdTypeCode))
                {
                    var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                        .FirstOrDefault(x => x.HisConfigCode == res.GuardianIdTypeCode);
                    if (idTypeConfig != null)
                    {
                        res.GuardianIdTypeCode = idTypeConfig.TriageConfigCode;
                        res.GuardianIdTypeName = idTypeConfig.TriageConfigName;
                    }
                }

                return JsonResult<PatientInfoFromHis>.Ok(data: res);
            }
            catch (Exception ex)
            {
                return JsonResult<PatientInfoFromHis>.Fail(ex.Message, new PatientInfoFromHis());
            }
        }

        /// <summary>
        /// 三无患者建档
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<PatientInfoFromHis>> CreateNoThreePatientRecordAsync(
            CreateOrGetPatientIdInput input)
        {
            PatientInfoFromHis patInfo = new PatientInfoFromHis
            {
                PatientId = "N" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                PatientName = input.PatientName.IsNullOrWhiteSpace()
                ? "无名氏_" + DateTime.Now.ToString("yyyyMMddHHmmss")
                : input.PatientName
            };

            return JsonResult<PatientInfoFromHis>.Ok(data: patInfo);
        }

        /// <summary>
        /// 建档前验证
        /// </summary>
        /// <param name="input"></param>
        /// <param name="isInfant"></param>
        /// <returns></returns>
        public Task<JsonResult> ValidateBeforeCreatePatient(CreateOrGetPatientIdInput input, out bool isInfant)
        {
            isInfant = input.IsInfant;
            return Task.FromResult(JsonResult.Ok());
        }

        /// <summary>
        /// 取消挂号信息
        /// </summary>
        /// <param name="regSerialNo">挂号流水号</param>
        /// <returns></returns>
        public async Task<JsonResult> CancelRegisterInfoAsync(string regSerialNo)
        {
            throw new NotImplementedException();
        }

        public Task<JsonResult<VitalSignsInfoByJinWan>> GetHisVitalSignsAsync(string serialNumber)
        {
            throw new NotImplementedException();
        }

        public Task<JsonResult<string>> GetStampBase64Async(string empCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改患者档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult> RevisePerson(PatientModifyDto input)
        {
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var uri = _configuration["HisApiSettings:RevisePerson"];
            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync();
            // 证件类型
            var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                .FirstOrDefault(x => x.TriageConfigCode == input.IdTypeCode);
            RevisePersonRequest request = new RevisePersonRequest
            {
                patientId = input.PatientId,
                visitNo = input.VisitNo,
                name = input.PatientName,
                gender = input.Sex switch
                {
                    "Sex_Man" => "1",
                    "Sex_Woman" => "2",
                    _ => "0"
                },
                genderName = input.Sex switch
                {
                    "Sex_Man" => "男性",
                    "Sex_Woman" => "女性",
                    _ => "未知"
                },
                addrDetail = input.Address,
                birthDate = DateTime.TryParse(input.BirthDay, out DateTime birthDate)
                    ? birthDate.ToString("yyyy-MM-dd")
                    : null,
                contactType = "01",
                content = input.ContactsPhone,
                idType = idTypeConfig?.HisConfigCode,
                idTypeName = idTypeConfig?.TriageConfigName,
                idCode = input.IdentityNo,
            };
            var responseText = await PostAsync(httpClient, uri, request, "调用HIS接口修改患者信息");
            CommonHttpResult result;
            try
            {
                result = JsonSerializer.Deserialize<CommonHttpResult>(responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
                if (result.Code == 0)
                {
                    return JsonResult.Ok();
                }
                else
                {
                    return JsonResult.Fail(msg: result.Msg);
                }
            }
            catch (Exception)
            {
                _log.LogInformation("调用HIS接口修改患者信息，Url: {Url}, Request：{Request}, Response: {Response}",
                    httpClient.BaseAddress + uri, JsonHelper.SerializeObject(request), responseText);
                throw new Exception("无法正常处理 HIS 接口返回数据");
            }
        }

        /// <summary>
        /// 获取护士列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<List<EmployeeDto>>> GetNurseScheduleAsync()
        {
            return await _commonHisApi.GetNurseScheduleAsync();
        }

        /// <summary>
        /// 暂停/恢复叫号（挂起状态，医生站不能呼叫、接诊患者）
        /// </summary>
        /// <param name="patientId">患者Id</param>
        /// <param name="isSuspend">是否暂停（0：暂停，1：开启）</param>
        /// <returns></returns>
        public Task<JsonResult> SuspendCalling(string patientId, bool isSuspend)
        {
            return Task.FromResult(JsonResult.Ok());
        }

        /// <summary>
        /// 获取流调表
        /// </summary>
        /// <param name="showType">HIS证件类型（01 居民身份证  03 护照  04 军官证  06 港澳居民来往内地通行证  07 台湾居民来往内地通行证）</param>
        /// <param name="idCardNo">证件号码</param>
        /// <returns></returns>
        public async Task<QuestionnaireData> GetQuestionnaireAsync(string showType, string idCardNo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过二维码查询流调表
        /// </summary>
        /// <param name="barcode">二维码字符串</param>
        /// <returns></returns>
        public async Task<QuestionnaireData> GetQuestionnaireAsync(string barcode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询挂号信息
        /// </summary>
        /// <param name="hisRegisterInfoQueryDto"></param>
        /// <returns></returns>
        public async Task<List<PatientRespDto>> GetRegisterInfoAsync(HisRegisterInfoQueryDto hisRegisterInfoQueryDto)
        {
            try
            {
                StringBuilder url = new StringBuilder(_configuration["HisApiSettings:getRegisterInfoList"]);

                hisRegisterInfoQueryDto.beginTime ??= DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd HH:mm:ss");
                hisRegisterInfoQueryDto.endTime ??= DateTime.Now.AddDays(1).ToString("yyyy/MM/dd HH:mm:ss");

                url.Append("?startDate=" + hisRegisterInfoQueryDto.beginTime); // 开始时间（查询挂号时间范围）
                url.Append("&endDate=" + hisRegisterInfoQueryDto.endTime); // 结束时间（查询挂号时间范围）
                url.Append("&idNo=" + hisRegisterInfoQueryDto.icCardId);
                url.Append("&patientId=" + hisRegisterInfoQueryDto.patientId);
                url.Append("&visitNum=" + hisRegisterInfoQueryDto.visitNo);
                var httpClient = _httpClientFactory.CreateClient("HisApi");
                var response = await httpClient.GetAsync(url.ToString());
                var responseText = await response.Content.ReadAsStringAsync();

                _log.LogInformation($"调用接口平台查询挂号信息接口，url: {url}，返回: {responseText}");
                if (string.IsNullOrWhiteSpace(responseText))
                {
                    _log.LogError("根据输入项获取挂号信息失败！原因：{Msg}", "查询挂号信息接口响应为空");
                    throw new HisResponseException("调用查询挂号信息接口失败！请检查后重试");
                }

                var hisResponse = JsonSerializer.Deserialize<HisResponseDto<List<PatientRespDto>>>(responseText);
                if (hisResponse.code != 0)
                {
                    _log.LogError($"调用接口平台获取挂号信息接口失败，无返回，url：{url}");
                    throw new HisResponseException(hisResponse.msg);
                }

                if (!hisResponse.data.Any())
                {
                    _log.LogError("根据输入项获取挂号信息失败！原因：{Msg}", "调用查询挂号信息接口失败。返回Data为null或空");
                    throw new HisResponseException(hisResponse.msg);
                }

                return hisResponse.data;
            }
            catch (HisResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _log.LogInformation("根据输入项获取挂号信息失败！代码错误：{Msg}", ex.Message);
                throw new HisResponseException("调用查询挂号信息接口失败！请检查后重试");
            }
        }

        public Task<JsonResult<List<RegisterInfoHisDto>>> GetRegisterInfoListAsync(RegisterInfoInput input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// HIS系统post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="dto"></param>
        /// <param name="splicingNull"></param>
        /// <returns></returns>
        public async Task<string> HisPostAsync<T>(StringBuilder url, T dto, bool splicingNull = true)
        {
            var isFirst = true;
            foreach (var item in dto.GetType().GetProperties())
            {
                if (splicingNull)
                {
                    url.Append((isFirst ? "" : "&") + item.Name + "=" + item.GetValue(dto));
                    isFirst = false;
                }
                else if (!string.IsNullOrEmpty(item.GetValue(dto).ToString()))
                {
                    url.Append((isFirst ? "" : "&") + item.Name + "=" + item.GetValue(dto));
                    isFirst = false;
                }
            }
            var requestBody = JsonSerializer.Serialize(dto);
            var httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType.MediaType = "application/json";
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var response = await httpClient.PostAsync(url.ToString(), httpContent);
            return await response.Content.ReadAsStringAsync();
        }

        public Task<HisResponseDto> payCurRegisterAsync(string visitNum)
        {
            throw new NotImplementedException();
        }

        private async Task<string> PostAsync<T>(HttpClient httpClient, string uri, T request, string apiComment)
        {
            _log.LogInformation("，Url: {Url}, Request：{Json}", httpClient.BaseAddress + uri,
                JsonHelper.SerializeObject(request));
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await TransientErrorRetryPolicy.ExecuteAsync(() => httpClient.PostAsync(uri, content));
            }
            catch (Exception exception)
            {
                _log.LogInformation("{ApiComment}, Message: {Message}", apiComment, exception.Message);
                throw new Exception("HIS 接口无法正常连接");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("HIS 接口无法连接");
            }

            var responseText = await response.Content.ReadAsStringAsync();
            _log.LogInformation("{ApiComment}，Url: {Url}, Request：{Request}, Response: {Response}", apiComment,
                httpClient.BaseAddress + uri, JsonHelper.SerializeObject(request), responseText);

            return responseText;
        }

        private async Task<TriageConfigDto> GetIdTypeByTriageConfigCode(string idTypeCode)
        {
            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.IdType.ToString());
            var idType = dicts[TriageDict.IdType.ToString()].FirstOrDefault(x => x.TriageConfigCode == idTypeCode);
            if (idType == null)
            {
                throw new Exception("证件类型不存在");
            }

            if (string.IsNullOrEmpty(idType.HisConfigCode))
            {
                throw new Exception("证件类型未设置对应的HIS编码");
            }

            return idType;
        }

        private async Task<TriageConfigDto> GetGuardianIdTypeByTriageConfigCode(string guardianIdTypeCode)
        {
            if (string.IsNullOrEmpty(guardianIdTypeCode)) return null;
            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.IdType.ToString());
            var guardianIdType = dicts[TriageDict.IdType.ToString()]
                .FirstOrDefault(x => x.TriageConfigCode == guardianIdTypeCode);
            if (guardianIdType == null)
            {
                throw new Exception("联系人证件类型不存在");
            }

            if (string.IsNullOrEmpty(guardianIdType?.HisConfigCode))
            {
                throw new Exception("联系人证件类型未设置对应的HIS编码");
            }

            return guardianIdType;
        }

        private async Task<TriageConfigDto> GetSocietyRelation(string societyRelationCode)
        {
            if (string.IsNullOrEmpty(societyRelationCode))
            {
                return null;
            }

            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync();
            // 联系人证件类型
            var societyRelation = dicts[TriageDict.SocietyRelation.ToString()]
                .FirstOrDefault(x => x.TriageConfigCode == societyRelationCode);
            if (societyRelation == null)
            {
                throw new Exception("与联系人关系不存在");
            }

            if (string.IsNullOrEmpty(societyRelation?.HisConfigCode))
            {
                throw new Exception("与联系人关系未设置对应的HIS编码");
            }

            return societyRelation;
        }

        /// <summary>
        /// 普通建档，三无建档通用接口
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        private async Task<PatientInfoFromHis> CreatePatientRecord(string uri, PatientReqDto req)
        {
            _log.LogInformation("根据输入项创建患者病历号，建档入参：{Json}", JsonHelper.SerializeObject(req));
            // var content = new StringContent(JsonSerializer.Serialize(req));
            var content = new StringContent(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");
            //#if !DEBUG
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var response = await httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            _log.LogInformation("根据输入项创建患者病历号，建档返回：{Json}", responseText);
            //#else
            //            var responseText = @"{""code"":0,""msg"":null,""data"":{""patientId"":""364039"",""patientName"":""孙春伟"",""identifyNo"":""371521198410264915"",""cardType"":""1"",""cardNum"":""0005070435"",""phoneNumberHome"":""13800138222"",""sex"":""M"",""accountBalance"":0.0,""setPassword"":1,""createDate"":""2022-01-14 10:01:29"",""useTime"":0}}";
            //#endif
            if (string.IsNullOrWhiteSpace(responseText))
            {
                throw new Exception("调用患者建档接口响应为null或空");
            }

            var json = JObject.Parse(responseText);
            if (json["code"]?.ToString() != "0")
            {
                throw new Exception("调用患者建档接口失败，" + json["msg"]);
            }

            if (json["data"] == null || string.IsNullOrWhiteSpace(json["data"].ToString()))
            {
                throw new Exception("返回Data为null或空");
            }

            var resp = JsonSerializer.Deserialize<PatientRespDto>(json["data"].ToString(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var res = resp.BuildAdapter().AdaptToType<PatientInfoFromHis>();
            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.InsuplcAdmdv.ToString());
            // 默认医保参保地【深圳】
            var insuplcAdmdv = dicts[TriageDict.InsuplcAdmdv.ToString()].FirstOrDefault(x => x.ExtraCode == "440300");
            res.InsuplcAdmdvCode = insuplcAdmdv?.TriageConfigCode;

            return res;
        }

        public Task<InsuranceDto> GetInsuranceInfoByElectronCert(string electronCertNo, string extraCode)
        {
            throw new NotImplementedException();
        }


        public async Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterPagedListAsync(GetRegisterPagedListInput input)
        {
            return await this._commonHisApi.GetRegisterPagedListAsync(input);
        }

        public Task<JsonResult> ReturnToNoTriage(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}