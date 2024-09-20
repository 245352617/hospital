using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SamJan.MicroService.PreHospital.TriageService.LGHis;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using SamJan.MicroService.TriageService.Application.Dtos;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 通用 HIS Api
    /// </summary>
    public class CommonHisApi : IHisApi
    {
        private readonly IPatientInfoRepository _patientInfoRepository;
        private readonly IRegisterInfoRepository _registerInfoRepository;
        private readonly IRepository<TriageConfig> _triageConfigRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly ILogger<CommonHisApi> _log;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _accessor;

        public CommonHisApi(IPatientInfoRepository patientInfoRepository, IRegisterInfoRepository registerInfoRepository, IRepository<TriageConfig> triageConfigRepository,
            IConfiguration configuration, IHttpClientHelper httpClientHelper, ILogger<CommonHisApi> log, IHttpClientFactory httpClientFactory, IHttpContextAccessor accessor)
        {
            this._patientInfoRepository = patientInfoRepository;
            this._registerInfoRepository = registerInfoRepository;
            this._triageConfigRepository = triageConfigRepository;
            this._configuration = configuration;
            this._httpClientHelper = httpClientHelper;
            this._log = log;
            _httpClientFactory = httpClientFactory;
            _accessor = accessor;
        }

        public async Task<PatientInfo> RegisterPatientAsync(PatientInfo patient, string doctorId, string doctorName, bool isUpdated, bool hasChangedDoctor, bool isFirstTimePush)
        {
            try
            {
                // 暂存的患者不进行推送
                if (patient.TriageStatus != 1) return await Task.FromResult(patient);
                _log.LogInformation("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号开始】");
                var patientReadonly = await _patientInfoRepository.Include(c => c.ConsequenceInfo)
                    .Include(c => c.AdmissionInfo)
                    .Include(c => c.RegisterInfo)
                    .Include(c => c.ScoreInfo)
                    .Include(c => c.VitalSignInfo)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == patient.Id);
                // 分诊后自动挂号
                if (!_configuration.GetValue<bool>("HisApiSettings:AutoRegisterAfterSaveTriage"))
                {
                    return patientReadonly;
                }

                if (patientReadonly != null)
                {
                    var deptConfig = await this._triageConfigRepository.AsQueryable()
                        .Where(x => x.TriageConfigType == (int)TriageDict.TriageDepartment)
                        .Where(x => x.TriageConfigCode == patientReadonly.ConsequenceInfo.TriageDeptCode)
                        .FirstOrDefaultAsync();
                    var dto = patientReadonly.BuildAdapter().AdaptToType<PatientReqDto>();
                    dto.deptId = deptConfig.HisConfigCode ?? deptConfig.TriageConfigCode;
                    dto.deptName = deptConfig.TriageConfigName;
                    dto.beginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dto.endTime = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                    dto.seeDate = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                    dto.doctorId = doctorId;
                    var currentHour = DateTime.Now.Hour;
                    if (currentHour >= 8 && currentHour <= 12)
                    {
                        dto.noonId = "1";
                    }
                    else if (currentHour > 12 && currentHour <= 18)
                    {
                        dto.noonId = "2";
                    }
                    else
                    {
                        dto.noonId = "3";
                    }

                    dto.insurance = "0";

                    RegisterInfo registerInfo;

                    var sexConfig = await this._triageConfigRepository.AsQueryable()
                        .Where(x => x.TriageConfigType == (int)TriageDict.Sex)
                        .Where(x => x.TriageConfigCode == dto.sex)
                        .FirstOrDefaultAsync();
                    dto.sex = sexConfig?.HisConfigCode ?? sexConfig?.TriageConfigCode;

                    var msg = JsonSerializer.Serialize(dto);
                    var httpContent = new StringContent(msg);
                    httpContent.Headers.ContentType.MediaType = "application/json";
                    var uri = _configuration.GetSection("HisApiSettings:registerPatient").Value;
                    _log.LogInformation($"调用接口平台挂号，url: {uri}, request: {JsonSerializer.Serialize(dto)}");
                    var response = await _httpClientHelper.PostAsync(uri, httpContent);
                    _log.LogInformation($"调用接口平台挂号，reponse: {response}");
                    if (string.IsNullOrWhiteSpace(response))
                    {
                        _log.LogError(
                            "【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【Msg：调用挂号接口失败，响应为空】");
                        throw new Exception("患者挂号失败！请检查后重试！");
                    }

                    var json = JObject.Parse(response);
                    if (json["code"]?.ToString() == "0")
                    {
                        if (json["data"] != null && !string.IsNullOrWhiteSpace(json["data"].ToString()))
                        {
                            var resp = JsonSerializer.Deserialize<PatientRespDto>(json["data"].ToString());
                            registerInfo = resp.BuildAdapter().AdaptToType<RegisterInfo>();
                            registerInfo.PatientInfoId = patientReadonly.Id;
                            // 排队号
                            patient.CallingSn = resp.registerSequence;
                            patient.LogTime = DateTime.Parse(resp.registerDate);
                            // 限制患者基本信息为只读，挂号后不允许修改患者基本信息，这会导致ECIS的患者信息跟HIS的患者信息产生差异
                            patient.IsBasicInfoReadOnly = true;
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
                                   $"【挂号失败】【Msg：调用挂号接口失败。返回原因：{json["msg"]}】");
                        throw new Exception($"调用挂号接口失败！{json[msg]}");
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
                        _log.LogError("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【Msg：DB保存挂号数据报错】");
                        throw new Exception("挂号失败");
                    }

                    _log.LogInformation("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号结束】");

                    return patientReadonly;
                }

                _log.LogError("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【该患者尚未分诊】");
                throw new Exception("该患者尚未分诊，请先分诊！");
            }
            catch (Exception e)
            {
                _log.LogWarning($"【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号错误】【Msg：{e}】");
                throw new Exception(e.Message);
            }
        }

        public async Task<PatientInfo> BeforeSaveTriageRecordAsync(CreateOrUpdatePatientDto dto, PatientInfo patient, bool isUpdated, bool hasChangedDoctor)
        {
            if (dto.TriageStatus != 1) { return patient; }
            RegisterInfo registerInfo = await this._registerInfoRepository.OrderBy(x => x.CreationTime)
                .FirstOrDefaultAsync(x => x.PatientInfoId == dto.TriagePatientInfoId);

            // 先分诊后挂号模式，调用平台接口【预约确认】，推送分诊信息到 HIS
            string reservationConfirmUrl = _configuration["HisApiSettings:ReservationConfirm"];
            if (!string.IsNullOrWhiteSpace(reservationConfirmUrl))
            {
                var reservationResult = await this.HisReservationConfirmAsync(new ReservationConfirmDto
                {
                    DeptCode = dto.ConsequenceInfo.TriageDept,
                    DoctorCode = dto.ConsequenceInfo.DoctorCode,
                    DoctorName = dto.ConsequenceInfo.DoctorName,
                    GreenLogo = !string.IsNullOrEmpty(dto.GreenRoad) ? "1" : "0",
                    PatientId = dto.PatientId,
                    TriageLevel = dto.ConsequenceInfo.ActTriageLevel,
                    WorkDate = DateTime.Today,
                    WorkType = dto.ConsequenceInfo.WorkType,
                });
                patient.SeqNumber = reservationResult.SeqNumber;
            }

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
            var deptConfig = await this._triageConfigRepository.AsQueryable()
                .Where(x => x.TriageConfigType == (int)TriageDict.TriageDepartment)
                .Where(x => x.TriageConfigCode == deptCode)
                .FirstOrDefaultAsync();

            var uri = _configuration["HisApiSettings:GetDoctorSchedule"];
            string queryParas = "";
            if (!string.IsNullOrEmpty(deptConfig?.HisConfigCode))
            {
                queryParas += (string.IsNullOrEmpty(queryParas) ? "" : "&") + $"deptCode={deptConfig.HisConfigCode}";
            }
            if (regDate != null)
            {
                queryParas += (string.IsNullOrEmpty(queryParas) ? "" : "&") + $"regDate={regDate.Value:yyyy/MM/dd}";
            }
            if (!string.IsNullOrEmpty(queryParas)) uri += $"?{queryParas}";
            try
            {
                var response = await _httpClientHelper.GetAsync(uri);
                _log.LogInformation($"调用平台接口查询医生排班信息，url: {uri}，reponse: {response}");
                if (string.IsNullOrEmpty(response)) throw new Exception("调用接口返回结果为空");
                var json = JObject.Parse(response);
                if (json["code"].ToString() == "0" && json["data"] != null && !string.IsNullOrWhiteSpace(json["data"].ToString()))
                {
                    List<DoctorSchedule> doctorSchedules = JsonSerializer.Deserialize<List<DoctorSchedule>>(json["data"].ToString(),
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        });

                    return JsonResult<List<DoctorSchedule>>.Ok(data: doctorSchedules);
                }
                string message = json["msg"].ToString();

                return JsonResult<List<DoctorSchedule>>.Fail(msg: message);
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
        /// <param name="registerNo">挂号流水号</param>
        /// <returns></returns>
        public async Task<JsonResult<List<PatientInfoFromHis>>> GetPatientRecordAsync(string idType, string identityNo, string visitNo, string patientName, string phone = "", string registerNo = "")
        {
            List<PatientInfoFromHis> res = new List<PatientInfoFromHis>();
            var uri = _configuration["HisApiSettings:getPatientInfo"] + $"?idNo={identityNo}&idType={idType}&visitNo={visitNo}&name={patientName}&phone={phone}";
            try
            {
                string response = await _httpClientHelper.GetAsync(uri);
                _log.LogInformation("调用平台接口查询患者信息，url: {Url}，reponse: {Response}", uri, response);
                if (string.IsNullOrWhiteSpace(response))
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "查询患者信息接口响应为空");
                    return JsonResult<List<PatientInfoFromHis>>.Fail("查档失败，查询患者信息接口响应为空");
                }

                var json = JObject.Parse(response);
                if (json["code"]?.ToString() != "0")
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", json["msg"]);
                    return JsonResult<List<PatientInfoFromHis>>.Fail("查档失败，" + json["msg"]);
                }
                if (json["data"] == null || string.IsNullOrWhiteSpace(json["data"].ToString()))
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "调用查询患者信息接口失败。返回Data为null或空");
                    return JsonResult<List<PatientInfoFromHis>>.Fail("查档失败，调用查询患者信息接口返回数据为空");
                }

                try
                {
                    // 返回单条数据
                    var resp = JsonSerializer.Deserialize<PatientRespDto>(json["data"].ToString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    res = new List<PatientInfoFromHis>
                    {
                        resp.BuildAdapter().AdaptToType<PatientInfoFromHis>()
                    };
                }
                catch (Exception)
                {
                    // 返回多条数据
                    var resp = JsonSerializer.Deserialize<List<PatientRespDto>>(json["data"].ToString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    res = resp.BuildAdapter().AdaptToType<List<PatientInfoFromHis>>();
                }

                foreach (var item in res)
                {
                    item.Sex = item.Sex switch
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
        /// 患者建档接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult<PatientInfoFromHis>> CreatePatientRecordAsync(CreateOrGetPatientIdInput input)
        {
            // 证件类型
            //TriageConfig idType = await GetIdType(input);
            // 联系人证件类型
            //TriageConfig guardianIdType = await GetGuardianIdType(input);
            // 与联系人关系
            //var societyRelation = await GetSocietyRelation(input);
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
                //patIdType = idType?.HisConfigCode,
                cardNo = input.CardNo ?? "",
                crowdCode = input.CrowdCode,
                associationName = input.ContactsPerson ?? "",
                //associationIdType = guardianIdType?.HisConfigCode ?? "",
                associationIdNo = input.GuardianIdCardNo ?? "",
                associationPhone = input.GuardianPhone ?? "",
                associationAddress = input.ContactsAddress ?? "",
                //societyRelation = societyRelation?.HisConfigCode ?? "",
            };

            try
            {
                var res = await this.CreatePatientRecord(uri, req);
                if (string.IsNullOrWhiteSpace(res.PatientName))
                {
                    res.PatientName = input.PatientName;
                }

                if (string.IsNullOrWhiteSpace(res.Sex))
                {
                    res.Sex = input.Sex;
                }

                res.Birthday ??= input.Birthday;

                if (string.IsNullOrWhiteSpace(res.IdentityNo))
                {
                    res.IdentityNo = input.IdentityNo;
                }

                res.SetGenderAndBirthday(input.IdentityNo);

                return JsonResult<PatientInfoFromHis>.Ok(data: res);
            }
            catch (Exception ex)
            {
                return JsonResult<PatientInfoFromHis>.Fail(ex.Message, data: new PatientInfoFromHis { });
            }
        }

        /// <summary>
        /// 三无患者建档
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<PatientInfoFromHis>> CreateNoThreePatientRecordAsync(CreateOrGetPatientIdInput input)
        {
            var uri = _configuration["HisApiSettings:buildNoThreePatientArchives"];
            // 三无患者建档，是否不需要身份证号码
            var buildNoThreePatientWithoutIdCardNo = bool.TryParse(_configuration["HisApiSettings:buildNoThreePatientWithoutIdCardNo"], out bool buildNoThreePatientWithoutIdCardNoValue)
                                                     && buildNoThreePatientWithoutIdCardNoValue;
            var nowDate = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000)
                           / 10000000)
                .ToString();
            // 证件类型
            //TriageConfig idType = await GetIdType(input);
            // 联系人证件类型
            //TriageConfig guardianIdType = await GetGuardianIdType(input);
            // 与联系人关系
            //var societyRelation = await GetSocietyRelation(input);
            input.IdentityNo = buildNoThreePatientWithoutIdCardNo ? null : "Y" + nowDate;
            // 性别
            var sexConfig = await this._triageConfigRepository.AsQueryable()
                .Where(x => x.TriageConfigType == (int)TriageDict.Sex)
                .Where(x => x.TriageConfigCode == input.Sex)
                .FirstOrDefaultAsync();
            // var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.Sex.ToString());
            input.PatientName = input.PatientName.IsNullOrWhiteSpace()
                ? "无名氏_" + DateTime.Now.ToString("yyyyMMddHHmmss") +
                  sexConfig.TriageConfigName
                : input.PatientName;
            var req = new PatientReqDto
            {
                name = input.PatientName ?? "",
                idNo = input.IdentityNo ?? "",
                idType = input.CardType.ToString(),
                birthday = input.Birthday?.ToString("yyyy/MM/dd"),
                phone = input.ContactsPhone ?? "",
                homeAddress = input.Address ?? "",
                nationality = input.Nation,
                contactName = input.ContactsPerson,
                sex = input.Sex switch
                {
                    "Sex_Man" => "M",
                    "Sex_Woman" => "F",
                    _ => "U"
                },
                //patIdType = idType?.HisConfigCode,
                cardNo = input.CardNo ?? "",
                crowdCode = input.CrowdCode,
                associationName = input.ContactsPerson ?? "",
                //associationIdType = guardianIdType?.HisConfigCode ?? "",
                associationIdNo = input.GuardianIdCardNo ?? "",
                associationPhone = input.GuardianPhone ?? "",
                associationAddress = input.ContactsAddress ?? "",
                //societyRelation = societyRelation?.HisConfigCode ?? "",
            };

            try
            {
                var res = await this.CreatePatientRecord(uri, req);
                if (string.IsNullOrWhiteSpace(res.PatientName))
                {
                    res.PatientName = input.PatientName;
                }
                if (string.IsNullOrWhiteSpace(res.Sex))
                {
                    res.Sex = input.Sex;
                }

                res.Birthday ??= input.Birthday;

                if (string.IsNullOrWhiteSpace(res.IdentityNo))
                {
                    res.IdentityNo = input.IdentityNo;
                }

                return JsonResult<PatientInfoFromHis>.Ok(data: res);
            }
            catch (Exception ex)
            {
                return JsonResult<PatientInfoFromHis>.Fail(ex.Message, data: new PatientInfoFromHis { });
            }
        }

        private async Task<TriageConfig> GetIdType(CreateOrGetPatientIdInput input)
        {

            var idType = await _triageConfigRepository.AsNoTracking().OrderBy(x => x.Sort)
                            .FirstOrDefaultAsync(x => x.TriageConfigType == (int)TriageDict.IdType && x.TriageConfigCode == input.IdTypeCode);
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

        private async Task<TriageConfig> GetGuardianIdType(CreateOrGetPatientIdInput input)
        {
            var guardianIdType = await _triageConfigRepository.AsNoTracking().OrderBy(x => x.Sort)
                            .FirstOrDefaultAsync(x => x.TriageConfigType == (int)TriageDict.IdType && x.TriageConfigCode == input.GuardianIdTypeCode);
            if (!string.IsNullOrEmpty(input.GuardianIdTypeCode) && guardianIdType == null)
            {
                throw new Exception("联系人证件类型不存在");
            }
            if (!string.IsNullOrEmpty(input.GuardianIdTypeCode) && string.IsNullOrEmpty(guardianIdType?.HisConfigCode))
            {
                throw new Exception("联系人证件类型未设置对应的HIS编码");
            }

            return guardianIdType;
        }

        private async Task<TriageConfig> GetSocietyRelation(CreateOrGetPatientIdInput input)
        {
            // 联系人证件类型
            var societyRelation = await _triageConfigRepository.AsNoTracking().OrderBy(x => x.Sort)
                .FirstOrDefaultAsync(x => x.TriageConfigType == (int)TriageDict.SocietyRelation && x.TriageConfigCode == input.SocietyRelationCode);
            if (!string.IsNullOrEmpty(input.SocietyRelationCode) && societyRelation == null)
            {
                throw new Exception("与联系人关系不存在");
            }
            if (!string.IsNullOrEmpty(input.SocietyRelationCode) && string.IsNullOrEmpty(societyRelation?.HisConfigCode))
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
            var content = new StringContent(JsonSerializer.Serialize(req));
            //#if !DEBUG
            var response = await _httpClientHelper.PostAsync(uri, content);
            _log.LogInformation("根据输入项创建患者病历号，建档返回：{Json}", response);
            //#else
            //            var response = @"{""code"":0,""msg"":null,""data"":{""patientId"":""364039"",""patientName"":""孙春伟"",""identifyNo"":""371521198410264915"",""cardType"":""1"",""cardNum"":""0005070435"",""phoneNumberHome"":""13800138222"",""sex"":""M"",""accountBalance"":0.0,""setPassword"":1,""createDate"":""2022-01-14 10:01:29"",""useTime"":0}}";
            //#endif
            if (string.IsNullOrWhiteSpace(response))
            {
                throw new Exception("调用患者建档接口响应为null或空");
            }
            var json = JObject.Parse(response);
            if (json["code"]?.ToString() != "0")
            {
                throw new Exception("调用患者建档接口失败，" + json["msg"]);
            }
            if (json["data"] == null || string.IsNullOrWhiteSpace(json["data"].ToString()))
            {
                throw new Exception("返回Data为null或空");
            }

            var resp = JsonSerializer.Deserialize<PatientRespDto>(json["data"].ToString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var res = resp.BuildAdapter().AdaptToType<PatientInfoFromHis>();

            return res;
        }

        /// <summary>
        /// 分诊完成推送消息到 HIS，由 HIS 进行后续【预约确认】或其他流程
        /// </summary>
        /// <returns></returns>
        private async Task<ReservationConfirmResultDto> HisReservationConfirmAsync(ReservationConfirmDto dto)
        {
            var triageLevel = await this._triageConfigRepository.AsQueryable()
                .Where(x => x.TriageConfigType == (int)TriageDict.TriageLevel)
                .Where(x => x.TriageConfigCode == dto.TriageLevel)
                .FirstOrDefaultAsync();
            var dept = await this._triageConfigRepository.AsQueryable()
                .Where(x => x.TriageConfigType == (int)TriageDict.TriageDepartment)
                .Where(x => x.TriageConfigCode == dto.DeptCode)
                .FirstOrDefaultAsync();
            if (triageLevel is null)
            {
                throw new Exception("分诊级别不存在");
            }

            var uri = _configuration["HisApiSettings:ReservationConfirm"];
            var req = new
            {
                deptCode = dept.HisConfigCode ?? dept.TriageConfigCode,
                doctorCode = dto.DoctorCode,
                doctorName = dto.DoctorName,
                // 绿通转为 0/1 值 
                greenLogo = dto.GreenLogo,
                patientId = dto.PatientId,
                // 分诊等级转换
                triageLevel = triageLevel.HisConfigCode ?? triageLevel.TriageConfigCode,
                // 日期格式转换
                workDate = dto.WorkDate.ToString("yyyy/MM/dd"),
                workType = dto.WorkType,
            };

            _log.LogInformation("根据科室、医生预约确认，预约：{Json}", JsonHelper.SerializeObject(req));
            var content = new StringContent(JsonSerializer.Serialize(req));
            var response = await _httpClientHelper.PostAsync(uri, content);
            if (string.IsNullOrEmpty(response)) throw new Exception("调用接口返回结果为空");
            var json = JObject.Parse(response);

            if (json["code"].ToString() == "0" && json["data"] != null && !string.IsNullOrWhiteSpace(json["data"].ToString()))
            {
                ReservationConfirmResultDto result = JsonSerializer.Deserialize<ReservationConfirmResultDto>(json["data"].ToString(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });

                return result;
            }

            string message = json["msg"].ToString();
            throw new Exception($"调用接口返回错误：{message}");
        }

        public Task<JsonResult> ValidateBeforeCreatePatient(CreateOrGetPatientIdInput input, out bool isInfant)
        {
            isInfant = false;
            return Task.FromResult(JsonResult.Ok());

            // if (!input.IsNoThree && string.IsNullOrEmpty(input.ContactsPhone) && string.IsNullOrEmpty(input.Address))
            // {
            //     return Task.FromResult(JsonResult.Fail("电话号码和住址必须填写其中一项"));
            // }

            // return Task.FromResult(JsonResult.Ok());
        }

        public Task<JsonResult> CancelRegisterInfoAsync(string regSerialNo)
        {
            return Task.FromResult(JsonResult.Ok());
        }

        public Task<JsonResult<VitalSignsInfoByJinWan>> GetHisVitalSignsAsync(string serialNumber)
        {
            throw new NotImplementedException();
        }

        public Task<JsonResult> RevisePerson(PatientModifyDto input)
        {
            return Task.FromResult(JsonResult.Ok());
        }

        public async Task<JsonResult<List<EmployeeDto>>> GetNurseScheduleAsync()
        {
            var uri = _configuration["getNurseListUrl"];
            try
            {
                var httpClient = _httpClientFactory.CreateClient("HisApi");
                httpClient.DefaultRequestHeaders.Add("Authorization", _accessor.HttpContext.Request.Headers["Authorization"].ToString());
                var response = await httpClient.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("HIS 接口无法连接");
                }

                var responseText = await response.Content.ReadAsStringAsync();

                JsonResult<PagedResultDto<PlatformUser>> hisResponse = JsonSerializer.Deserialize<JsonResult<PagedResultDto<PlatformUser>>>(responseText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (hisResponse.Code != 0)
                {
                    return JsonResult<List<EmployeeDto>>.Fail(msg: hisResponse.Msg);
                }
                if (hisResponse.Data.TotalCount <= 0)
                {
                    return JsonResult<List<EmployeeDto>>.Ok();
                }
                var result = hisResponse.Data.Items.BuildAdapter()
                    .ForkConfig(forked =>
                    {
                        forked.ForType<PlatformUser, EmployeeDto>()
                             .Map(dest => dest.Code, src => src.UserName);
                    })
                    .AdaptToType<List<EmployeeDto>>();
                foreach (var item in result)
                {
                    item.NamePy = PyHelper.GetFirstPy(item.Name);
                }

                return JsonResult<List<EmployeeDto>>.Ok(data: result);
            }
            catch (Exception ex)
            {
                return JsonResult<List<EmployeeDto>>.Fail(msg: ex.Message);
            }
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
        /// 获取base64签名
        /// </summary>
        /// <param name="empCode">工号</param>
        /// <returns></returns>
        public Task<JsonResult<string>> GetStampBase64Async(string empCode)
        {
            return Task.FromResult(JsonResult<string>.Ok());
        }

        /// <summary>
        /// 门诊患者信息查询
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<PatientInfoFromHis> GetPatienInfoBytIdAsync(string patientId)
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

        /// <summary>
        /// 获取挂号患者列表
        /// 挂号超过24小时的不会被查询到结果中
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        public async Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterPagedListAsync(GetRegisterPagedListInput input)
        {
            RegisterPatientInfoResultDto result = new RegisterPatientInfoResultDto();
            var list = await GetQuery(input)
                .OrderByDescending(x =>
                    x.VisitStatus == VisitStatus.Treated
                        ? -1
                        : (x.VisitStatus != VisitStatus.NotTriageYet ? 1 : 0)) // 就诊状态排序、分诊状态排序
                .ThenBy(x => x.TriageStatus == 0 ? "" : x.ConsequenceInfo.ActTriageLevel) // 分诊等级排序
                .ThenByDescending(x => x.VisitStatus)
                .ThenBy(x => x.TriageTime)
                .ThenByDescending(x => x.RegisterInfo.Max(y => y.RegisterTime)) // 挂号时间排序
                .ToListAsync();
            // var covidExams = await this.Covid19ExamRepository.Where(x => list.Select(y => y.PatientId).Contains(x.PatientId)).ToListAsync();

            // 等候人数计算
            var waittingList = await GetCurrentWaitingList();

            var hisCode = _configuration.GetValue<string>("HospitalCode");
            IEnumerable<PatientInfo> totalItems = null;
            if (hisCode == "PekingUniversity")
            {
                totalItems = list.Where(x => !IsRefundRegister(x))
                    .Where(x => x.VisitStatus == VisitStatus.NotTriageYet || x.VisitStatus == VisitStatus.Suspend ||
                                x.VisitStatus == VisitStatus.WattingTreat || x.VisitStatus == VisitStatus.Treating
                                || (x.ConsequenceInfo != null && x.ConsequenceInfo.ChangeTriage));
            }
            else
            {
                totalItems = list.Where(x => !IsRefundRegister(x))
                    .Where(x => x.VisitStatus == VisitStatus.NotTriageYet || x.VisitStatus == VisitStatus.Suspend ||
                                x.VisitStatus == VisitStatus.WattingTreat || x.VisitStatus == VisitStatus.Treating
                                // 修改分诊患者需要显示在挂号列表中
                                || !string.IsNullOrWhiteSpace(x.ConsequenceInfo?.ChangeTriageReasonCode)
                                || !string.IsNullOrWhiteSpace(x.ConsequenceInfo?.ChangeDept));
            }


            result.Items = totalItems
                .Skip((input.Index - 1) * input.PageSize).Take(input.PageSize)
                .BuildAdapter().AdaptToType<List<RegisterPatientInfoDto>>();
            foreach (var item in result.Items)
            {
                // item.HasFinishedCovid19Exam = covidExams.Any(x => x.PatientId == item.PatientId);
                item.HasFinishedCovid19Exam = false;
                item.WaittingForNumber = GetWaitingForNumber(list.FirstOrDefault(x => x.Id == item.TriagePatientInfoId),
                    waittingList);
                item.RegisterNo = item.RegisterInfo.Count() > 0 ? item.RegisterInfo.FirstOrDefault().RegisterNo : null;
                item.RegisterTime = item.RegisterInfo.Count() > 0
                    ? (DateTime?)item.RegisterInfo.FirstOrDefault().RegisterTime
                    : null;
            }

            // 分页数据总数
            result.TotalCount = totalItems.Count();
            // 挂号总数包含已就诊的患者
            result.TotalRegisterCount = list.Count();
            result.TotalRegisterRefundCount = list
                .Where(x => IsRefundRegister(x)).Count(); // 查询挂号记录是否退号（只查询最近的一条挂号记录）
            result.TotalTreatedCount = list.Where(x => x.VisitStatus == VisitStatus.Treated).Count();
            result.TotalTreatingCount = list.Where(x => x.VisitStatus == VisitStatus.Treating).Count();
            result.TotalWaitingCount = list
                .Where(x => !IsRefundRegister(x)).Where(x =>
                    x.VisitStatus == VisitStatus.WattingTreat || x.VisitStatus == VisitStatus.Suspend).Count();

            return JsonResult<RegisterPatientInfoResultDto>.Ok(data: result);
        }

        #region private挂号患者列表
        /// <summary>
        /// 获取排队号
        /// </summary>
        /// <param name="item"></param>
        /// <param name="waittingList"></param>
        /// <returns></returns>
        private int GetWaitingForNumber(PatientInfo item, List<PatientInfo> waittingList)
        {
            if (item == null) return 0;
            if (item.TriageStatus == 1 && !string.IsNullOrEmpty(item.ConsequenceInfo?.TriageDeptCode))
            {
                var deptWaittingList = waittingList
                    .Where(x => x.ConsequenceInfo.TriageDeptCode == item.ConsequenceInfo.TriageDeptCode)
                    .OrderByDescending(x => x.TriageStatus) // 分诊状态
                    .ThenBy(x => x.ConsequenceInfo.ActTriageLevel ?? "TriageLevel_999") // 分诊等级排序
                    .ThenByDescending(x => x.VisitStatus)
                    .ThenBy(x => x.TriageTime)
                    .ToList();

                // 计算等待人数
                var findInWaitting = deptWaittingList.FirstOrDefault(x => x.Id == item.Id);
                int waittingForNumbe = 0;
                if (findInWaitting != null)
                {
                    waittingForNumbe = deptWaittingList.IndexOf(findInWaitting);
                }
                else if (item.VisitStatus == VisitStatus.NotTriageYet)
                {
                    waittingForNumbe = deptWaittingList.Count();
                }

                return waittingForNumbe;
            }
            else
            {
                return 0;
            }
        }

        private IQueryable<PatientInfo> GetQuery(GetRegisterPagedListInput input)
        {
            return _patientInfoRepository
                .Include(x => x.RegisterInfo)
                .Include(x => x.ConsequenceInfo)
                .Include(x => x.VitalSignInfo)
                .Where(x => x.RegisterInfo.Any(x => x.RegisterTime >= DateTime.Now.AddDays(-1))) // 只查询挂号时间在24小时以内的
                .WhereIf(!string.IsNullOrEmpty(input.DeptCode) && input.DeptCode != "全部患者" && input.DeptCode != "未分诊",
                    x => x.ConsequenceInfo.TriageDeptCode == input.DeptCode.Trim()) // 查询指定科室
                .WhereIf(!string.IsNullOrEmpty(input.SearchText),
                    x => x.PatientName.Contains(input.SearchText) || x.CallingSn.Contains(input.SearchText) ||
                         x.ContactsPhone.Contains(input.SearchText))
                .WhereIf(input.DeptCode == "未分诊",
                    x => x.VisitStatus == VisitStatus.NotTriageYet || x.VisitStatus == VisitStatus.Suspend);
        }

        /// <summary>
        /// 获取当前等候的患者列表
        /// </summary>
        /// <returns></returns>
        private async Task<List<PatientInfo>> GetCurrentWaitingList()
        {
            int.TryParse(_configuration["Settings:RegisterShowTime"], out int time);
            time = time > 0 ? -time : -24;
            // 等候人数计算
            var waitingList = await _patientInfoRepository.AsQueryable().AsNoTracking()
                .Include(x => x.ConsequenceInfo)
                .Where(x => x.RegisterInfo.Any(y =>
                    y.RegisterTime >= DateTime.Now.AddHours(time) && !y.IsCancelled)) // 只查询挂号时间在24小时以内的
                .Where(x => x.ConsequenceInfo != null)
                .Where(x => x.VisitStatus == VisitStatus.WattingTreat || x.VisitStatus == VisitStatus.Treating)
                .ToListAsync();

            return waitingList;
        }

        /// <summary>
        /// 是否退号的判断
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private bool IsRefundRegister(PatientInfo patient)
        {
            return patient.RegisterInfo.Count() > 0 &&
                   patient.RegisterInfo.OrderByDescending(y => y.RegisterTime).First().IsCancelled;
        }

        public Task<JsonResult> ReturnToNoTriage(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
