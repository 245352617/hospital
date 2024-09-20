using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.PreHospital.TriageService.LGHis;
using SamJan.MicroService.TriageService.Application.Dtos;
using StackExchange.Redis;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class ShandaHisApi : IHisApi
    {
        private readonly ILogger<ShandaHisApi> _log;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITriageConfigAppService _triageConfigAppService;
        private readonly IPatientInfoRepository _patientInfoRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IRegisterInfoRepository _registerInfoRepository;
        private readonly IDatabase _redis;
        private readonly CommonHisApi _commonHisApi;

        public ShandaHisApi(IConfiguration configuration, IHttpClientFactory httpClientFactory,
            ILogger<ShandaHisApi> log,
            ITriageConfigAppService triageConfigAppService,
            IPatientInfoRepository patientInfoRepository,
            ICurrentUser currentUser,
            IRegisterInfoRepository registerInfoRepository,
            RedisHelper redisHelper, CommonHisApi commonHisApi)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _log = log;
            _triageConfigAppService = triageConfigAppService;
            _patientInfoRepository = patientInfoRepository;
            _currentUser = currentUser;
            _registerInfoRepository = registerInfoRepository;
            _redis = redisHelper.GetDatabase();
            _commonHisApi = commonHisApi;
        }

        public async Task<PatientInfo> BeforeSaveTriageRecordAsync(CreateOrUpdatePatientDto dto, PatientInfo patient,
            bool isUpdated,
            bool hasChangedDoctor)
        {
            return await Task.FromResult(patient);
        }

        /// <summary>
        /// 挂号
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="doctorId"></param>
        /// <param name="doctorName"></param>
        /// <param name="isModify"></param>
        /// <param name="hasChangedDoctor"></param>
        /// <param name="isFirstTimePush">是否第一次分诊</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PatientInfo> RegisterPatientAsync(PatientInfo patientInfo, string doctorId, string doctorName,
            bool isModify,
            bool hasChangedDoctor, bool isFirstTimePush)
        {
            var patientReadonly = await _patientInfoRepository.Include(c => c.AdmissionInfo)
                .Include(c => c.ConsequenceInfo)
                .Include(c => c.ScoreInfo)
                .Include(c => c.VitalSignInfo)
                .Include(c => c.RegisterInfo)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == patientInfo.Id);

            // 分诊后自动挂号
            if (!_configuration.GetValue<bool>("HisApiSettings:AutoRegisterAfterSaveTriage"))
            {
                return patientReadonly;
            }

            if (patientReadonly is null)
            {
                throw new Exception($"调用预约确认接口失败，患者信息不存在，患者id: {patientInfo.Id}");
            }

            if (isModify)
            {
                return patientReadonly;
            }

            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync();

            RegisterInfo registerInfo = null;
            // 新增挂号信息
            var deptConfig = dicts[TriageDict.TriageDepartment.ToString()].FirstOrDefault(x =>
                x.TriageConfigCode == patientReadonly.ConsequenceInfo.TriageDeptCode);
            var dto = patientReadonly.BuildAdapter().AdaptToType<PatientReqDto>();

            dto.emergFlag = "1";
            dto.deptId = deptConfig.HisConfigCode ?? deptConfig.TriageConfigCode;
            dto.deptName = deptConfig.TriageConfigName;
            dto.seeDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dto.operatorCode = _currentUser.UserName;
            dto.operatorName = _currentUser.GetFullName();
            dto.returnVisitFlag = "0";
            dto.cardNo = patientInfo.PatientId;

            var msg = JsonSerializer.Serialize(dto);
            var httpContent = new StringContent(msg);
            httpContent.Headers.ContentType.MediaType = "application/json";
            var uri = _configuration.GetSection("HisApiSettings:registerPatient").Value;
            _log.LogInformation("调用接口平台挂号，url: {Uri}, request: {Serialize}", uri, JsonSerializer.Serialize(dto));
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var response = await httpClient.PostAsync(uri, httpContent);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            _log.LogInformation("调用接口平台挂号，response: {Response}", responseText);
            if (string.IsNullOrWhiteSpace(responseText))
            {
                _log.LogError(
                    "【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【Msg：调用挂号接口失败，响应为空】");
                throw new Exception("调用挂号接口失败，响应为空！");
            }

            var json = JObject.Parse(responseText);
            if (json["code"]?.ToString() == "0")
            {
                if (json["data"] != null && !string.IsNullOrWhiteSpace(json["data"].ToString()))
                {
                    var resp = JsonSerializer.Deserialize<PatientRespDto>(json["data"].ToString());
                    registerInfo = resp.BuildAdapter().AdaptToType<RegisterInfo>();
                    registerInfo.PatientInfoId = patientReadonly.Id;
                    // 挂号流水号
                    registerInfo.RegisterNo = resp.visitNo;
                    // 已挂号
                    patientInfo.VisitStatus = VisitStatus.WattingTreat;
                    // 排队号
                    patientInfo.CallingSn = resp.registerSequence;
                    patientInfo.LogTime = DateTime.Parse(resp.registerDate ?? dto.seeDate);
                    patientInfo.VisitStatus = VisitStatus.WattingTreat;
                    // 挂号流水号
                    // 限制患者基本信息为只读，挂号后不允许修改患者基本信息，这会导致ECIS的患者信息跟HIS的患者信息产生差异
                    patientInfo.IsBasicInfoReadOnly = true;
                }
                else
                {
                    _log.LogError("【PatientInfoService】【GetPatientRecordByHl7MsgAsync】【挂号失败】" +
                                  "【Msg：调用挂号接口失败。返回Data为null或空】");
                    throw new Exception("调用挂号接口失败！接口未返回data");
                }
            }
            else
            {
                _log.LogError("【PatientRegisterService】【CreateRegisterNoForPatientAsync】" +
                              "【挂号失败】【Msg：调用挂号接口失败。返回原因：{Msg}】", json["msg"]);
                throw new Exception($"调用挂号接口失败！{json["msg"]}");
            }

            if (string.IsNullOrWhiteSpace(registerInfo.RegisterNo))
            {
                _log.LogError("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【Msg：调用挂号接口失败】");
                throw new Exception("调用挂号接口失败！HIS未返回挂号流水号！");
            }

            var dbContext = _registerInfoRepository.GetDbContext();
            dbContext.Entry(registerInfo).State = EntityState.Added;
            if (await dbContext.SaveChangesAsync() <= 0)
            {
                _log.LogError(
                    "【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【Msg：DB保存挂号数据报错】");
                throw new Exception("挂号失败");
            }

            dbContext.Entry(registerInfo).State = EntityState.Detached;

            return patientInfo;
        }

        public async Task<JsonResult> SyncRegisterPatientFromHis()
        {
            return await Task.FromResult(JsonResult.Ok());
        }

        public async Task<JsonResult<List<DoctorSchedule>>> GetDoctorScheduleAsync(string deptCode, DateTime? regDate)
        {
            return await Task.FromResult(JsonResult<List<DoctorSchedule>>.Ok());
        }

        public async Task<JsonResult<List<EmployeeDto>>> GetNurseScheduleAsync()
        {
            return await Task.FromResult(JsonResult<List<EmployeeDto>>.Ok());
        }

        /// <summary>
        /// 查询档案
        /// </summary>
        /// <param name="idType"></param>
        /// <param name="identityNo"></param>
        /// <param name="visitNo"></param>
        /// <param name="patientName"></param>
        /// <param name="phone"></param>
        /// <param name="regSerialNo"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<JsonResult<List<PatientInfoFromHis>>> GetPatientRecordAsync(string idType, string identityNo,
            string visitNo, string patientName, string phone = "",
            string regSerialNo = "")
        {
            var uri = _configuration["HisApiSettings:getPatientInfo"] +
                      $"?idNo={identityNo}&idType={idType}&visitNo={visitNo}&name={patientName}&phone={phone}&regSerialNo={regSerialNo}";
            try
            {
                _log.LogInformation("根据输入项创建患者病历号，查档Url：{Url}", uri);
                var httpClient = _httpClientFactory.CreateClient("HisApi");
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var responseText = await response.Content.ReadAsStringAsync();
                _log.LogInformation("调用平台接口查询患者信息，url: {Uri}，response: {ResponseText}", uri, responseText);
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

                    if (!string.IsNullOrWhiteSpace(patientOutput.Country))
                    {
                        var country = dicts[TriageDict.Country.ToString()]
                            .FirstOrDefault(x => x.HisConfigCode == patientOutput.Country);
                        if (country != null)
                        {
                            patientOutput.CountryCode = country.TriageConfigCode;
                        }
                    }

                    if (!patientOutput.Nation.IsNullOrWhiteSpace())
                    {
                        var nation = dicts[TriageDict.Nation.ToString()]
                            .FirstOrDefault(x => x.TriageConfigName == patientOutput.Nation);
                        if (nation != null)
                        {
                            patientOutput.Nation = nation.TriageConfigCode;
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
                        "男" => "Sex_Man",
                        "女" => "Sex_Woman",
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

            if (string.IsNullOrEmpty(societyRelation.HisConfigCode))
            {
                throw new Exception("与联系人关系未设置对应的HIS编码");
            }

            return societyRelation;
        }

        /// <summary>
        /// 创建档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
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
                birthday = input.Birthday?.ToString("yyyy-MM-dd"),
                phone = input.ContactsPhone ?? "",
                homeAddress = input.Address ?? "",
                nationality = input.Nation,
                contactName = input.ContactsPerson ?? "",
                sex = input.Sex switch
                {
                    "Sex_Man" => "男",
                    "Sex_Woman" => "女",
                    _ => "未知"
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
                var res = await CreatePatientRecord(uri, req);
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
                        "男" => "Sex_Man",
                        "女" => "Sex_Woman",
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

            if (string.IsNullOrEmpty(guardianIdType.HisConfigCode))
            {
                throw new Exception("联系人证件类型未设置对应的HIS编码");
            }

            return guardianIdType;
        }

        /// <summary>
        /// 三无患者创建档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult<PatientInfoFromHis>> CreateNoThreePatientRecordAsync(
            CreateOrGetPatientIdInput input)
        {
            var nowDate = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000)
                           / 10000000)
                .ToString();
            input.IdentityNo = "Y" + nowDate;
            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.Sex.ToString());
            input.PatientName = input.PatientName.IsNullOrWhiteSpace()
                ? "无名氏_" + DateTime.Now.ToString("yyyyMMddHHmmss") +
                  dicts.GetNameByDictCode(TriageDict.Sex, input.Sex)
                : input.PatientName;

            return await CreatePatientRecordAsync(input);
        }

        /// <summary>
        /// 普通建档，三无建档通用接口
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        private async Task<PatientInfoFromHis> CreatePatientRecord(string uri, PatientReqDto req)
        {
            _log.LogInformation("根据输入项创建患者病历号，建档Url：{Url}", uri);
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

        public async Task<JsonResult> RevisePerson(PatientModifyDto input)
        {
            return await Task.FromResult(JsonResult.Ok());
        }

        public Task<JsonResult> ValidateBeforeCreatePatient(CreateOrGetPatientIdInput input, out bool isInfant)
        {
            isInfant = false;
            return Task.FromResult(JsonResult.Ok());
        }

        public async Task<JsonResult> CancelRegisterInfoAsync(string regSerialNo)
        {
            return await Task.FromResult(JsonResult.Ok());
        }

        public async Task<JsonResult<VitalSignsInfoByJinWan>> GetHisVitalSignsAsync(string serialNumber)
        {
            return await Task.FromResult(JsonResult<VitalSignsInfoByJinWan>.Ok());
        }

        public async Task<JsonResult<string>> GetStampBase64Async(string empCode)
        {
            return await Task.FromResult(JsonResult<string>.Ok());
        }

        public async Task<JsonResult> SuspendCalling(string patientId, bool isSuspend)
        {
            return await Task.FromResult(JsonResult.Ok());
        }

        public Task<PatientInfoFromHis> GetPatienInfoBytIdAsync(string patientId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PatientRespDto>> GetRegisterInfoAsync(HisRegisterInfoQueryDto hisRegisterInfoQueryDto)
        {
            throw new NotImplementedException();
        }

        public Task<JsonResult<List<RegisterInfoHisDto>>> GetRegisterInfoListAsync(RegisterInfoInput input)
        {
            throw new NotImplementedException();
        }

        public Task<HisResponseDto> payCurRegisterAsync(string visitNum)
        {
            throw new NotImplementedException();
        }

        public Task<InsuranceDto> GetInsuranceInfoByElectronCert(string electronCertNo, string extraCode)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterPagedListAsync(GetRegisterPagedListInput input)
        {
            return await _commonHisApi.GetRegisterPagedListAsync(input);
        }

        public Task<JsonResult> ReturnToNoTriage(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}