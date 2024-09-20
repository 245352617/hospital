using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Timeout;
using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SamJan.MicroService.PreHospital.TriageService.LGHis;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using SamJan.MicroService.TriageService.Application.Dtos;
using Volo.Abp.Application.Dtos;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 通用 HIS Api
    /// </summary>
    public class JinwanHisApi : IHisApi
    {
        private readonly IRegisterInfoRepository _registerInfoRepository;
        private readonly IRepository<TriageConfig> _triageConfigRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly ILogger<JinwanHisApi> _log;
        private readonly IDapperRepository _dapperRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly CommonHisApi _commonHisApi;

        // 超时重试策略，重试时间：2、4、8
        private static readonly AsyncRetryPolicy<HttpResponseMessage> TransientErrorRetryPolicy =
            Policy.HandleResult<HttpResponseMessage>(
                    message => ((int)message.StatusCode == 429 || (int)message.StatusCode >= 500))
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                })
            ;
        // 5秒超时策略
        private static readonly AsyncTimeoutPolicy TimeoutPolicy = Policy.TimeoutAsync(5);

        //private static readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> CircuitBreakerPolicy =
        //    Policy.HandleResult<HttpResponseMessage>(message => (int)message.StatusCode == 503)
        //        .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10));

        public JinwanHisApi(IRegisterInfoRepository registerInfoRepository,
            IDapperRepository dapperRepository, IRepository<TriageConfig> triageConfigRepository,
            IConfiguration configuration, IHttpClientFactory httpClientFactory,
            IHttpClientHelper httpClientHelper, ILogger<JinwanHisApi> log, IHttpContextAccessor accessor, CommonHisApi commonHisApi)
        {
            _registerInfoRepository = registerInfoRepository;
            _dapperRepository = dapperRepository;
            _triageConfigRepository = triageConfigRepository;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpClientHelper = httpClientHelper;
            _log = log;
            _accessor = accessor;
            _commonHisApi = commonHisApi;
        }

        /// <summary>
        /// 挂号/预约/分诊
        /// 保存分诊信息后调用
        /// </summary>
        /// <param name="patient">确认分诊后的患者信息</param>
        /// <param name="doctorId">医生代码</param>
        /// <param name="doctorName">医生名称</param>
        /// <param name="isUpdated">是否修改分诊信息（false：新增；true：修改）</param>
        /// <param name="hasChangedDoctor">科室、医生信息修改</param>
        /// <param name="isFirstTimePush">是否第一次分诊</param>
        /// <returns></returns>
        public async Task<PatientInfo> RegisterPatientAsync(PatientInfo patient, string doctorId, string doctorName, bool isUpdated, bool hasChangedDoctor, bool isFirstTimePush)
        {
            return await Task.FromResult(patient);
        }

        public async Task<PatientInfo> BeforeSaveTriageRecordAsync(CreateOrUpdatePatientDto dto, PatientInfo patient, bool isUpdated, bool hasChangedDoctor)
        {
            if (dto.TriageStatus != 1 || dto.SkipRegister) { return patient; }
            RegisterInfo registerInfo = await this._registerInfoRepository.OrderBy(x => x.CreationTime)
                .FirstOrDefaultAsync(x => x.PatientInfoId == dto.TriagePatientInfoId);

            // 先分诊后挂号模式，调用平台接口【预约确认】，推送分诊信息到 HIS
            string reservationConfirmUrl = _configuration["HisApiSettings:ReservationConfirm"];
            if (string.IsNullOrEmpty(reservationConfirmUrl))
            {
                throw new Exception("请检查配置文件，未配置[HisApiSettings:ReservationConfirm]");
            }
            if (!isUpdated || (isUpdated && hasChangedDoctor))
            {// 首次新增分诊，修改分诊的时候改变科室、医生
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
                patient.SeqNumber = reservationResult?.SeqNumber;

                return patient;
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
                queryParas += (string.IsNullOrEmpty(queryParas) ? "" : "&") + $"deptCode={deptConfig?.HisConfigCode}";
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
                _log.LogInformation($"调用平台接口查询患者信息，url: {uri}，reponse: {response}");
                if (string.IsNullOrWhiteSpace(response))
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "查询患者信息接口响应为空");
                    return JsonResult<List<PatientInfoFromHis>>.Fail("调用查询患者信息接口失败！请检查后重试");
                }
                var json = JObject.Parse(response);
                if (json["code"]?.ToString() != "0")
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", json["msg"]);
                    return JsonResult<List<PatientInfoFromHis>>.Fail(msg: "调用查询患者信息接口失败，" + json["msg"]);
                }
                if (json["data"] == null || string.IsNullOrWhiteSpace(json["data"].ToString()))
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "调用查询患者信息接口失败。返回Data为null或空");
                    return JsonResult<List<PatientInfoFromHis>>.Fail("查询患者信息失败！请检查后重试");
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
            TriageConfig idType = await GetIdType(input.IdTypeCode);
            // 联系人证件类型
            TriageConfig guardianIdType = await GetGuardianIdType(input.GuardianIdTypeCode);
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
            TriageConfig idType = await GetIdType(input.IdTypeCode);
            // 联系人证件类型
            TriageConfig guardianIdType = await GetGuardianIdType(input.GuardianIdTypeCode);
            // 与联系人关系
            var societyRelation = await GetSocietyRelation(input.SocietyRelationCode);
            // 性别
            var sexConfig = await this._triageConfigRepository.AsQueryable()
                .Where(x => x.TriageConfigType == (int)TriageDict.Sex)
                .Where(x => x.TriageConfigCode == input.Sex)
                .FirstOrDefaultAsync();
            // var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.Sex.ToString());
            input.IdentityNo = buildNoThreePatientWithoutIdCardNo ? null : "Y" + nowDate;
            input.PatientName = input.PatientName.IsNullOrWhiteSpace()
                ? "无名氏_" + DateTime.Now.ToString("yyyyMMddHHmmss") +
                  sexConfig?.TriageConfigName
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

        private async Task<TriageConfig> GetIdType(string idTypeCode)
        {
            var idType = await _triageConfigRepository.AsNoTracking().OrderBy(x => x.Sort)
                            .FirstOrDefaultAsync(x => x.TriageConfigType == (int)TriageDict.IdType && x.TriageConfigCode == idTypeCode);
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

        private async Task<TriageConfig> GetGuardianIdType(string guardianIdTypeCode)
        {
            if (string.IsNullOrEmpty(guardianIdTypeCode)) return null;
            var guardianIdType = await _triageConfigRepository.AsNoTracking().OrderBy(x => x.Sort)
                            .FirstOrDefaultAsync(x => x.TriageConfigType == (int)TriageDict.IdType && x.TriageConfigCode == guardianIdTypeCode);
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

        private async Task<TriageConfig> GetSocietyRelation(string societyRelationCode)
        {
            if (string.IsNullOrEmpty(societyRelationCode)) { return null; }
            // 联系人证件类型
            var societyRelation = await _triageConfigRepository.AsNoTracking().OrderBy(x => x.Sort)
                .FirstOrDefaultAsync(x => x.TriageConfigType == (int)TriageDict.SocietyRelation && x.TriageConfigCode == societyRelationCode);
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
            var httpClient = _httpClientFactory.CreateClient("HisApi");

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

            _log.LogInformation("根据科室、医生预约确认，url: {Url}, 预约：{Json}", httpClient.BaseAddress + _configuration["HisApiSettings:ReservationConfirm"], JsonHelper.SerializeObject(req));
            var content = new StringContent(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");
            //var response = await _httpClientHelper.PostAsync(uri, content);
            //if (string.IsNullOrEmpty(response)) throw new Exception("调用接口返回结果为空");
            //var json = JObject.Parse(response);
            //if (json["code"].ToString() == "0" && json["data"] != null && !string.IsNullOrWhiteSpace(json["data"].ToString()))
            //{
            //    ReservationConfirmResultDto result = JsonSerializer.Deserialize<ReservationConfirmResultDto>(json["data"].ToString(),
            //        new JsonSerializerOptions
            //        {
            //            PropertyNameCaseInsensitive = true,
            //        });

            //    return result;
            //}

            //string message = json["msg"].ToString();
            //throw new Exception($"调用接口返回错误：{message}");
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                // 暂时去掉重试机制，HIS接口不返回，经常2分钟后才处理完成
                // response = await TransientErrorRetryPolicy.ExecuteAsync(() => httpClient.PostAsync(uri, content));
                httpClient.Timeout = TimeSpan.FromSeconds(5);
                response = await TimeoutPolicy.ExecuteAsync(() => httpClient.PostAsync(uri, content));
            }
            catch (TimeoutRejectedException)
            {// 接口调用超时，当做成功处理，因为HIS接口可能2分钟后会处理成功
                _log.LogInformation("根据科室、医生预约确认超时");
                return default;
            }
            catch (Exception exception)
            {
                _log.LogInformation("根据科室、医生预约确认失败, message: {Message}", exception.Message);
                throw new Exception("HIS 接口无法正常连接");
            }
            // 请求超时特殊处理流程
            if (response.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
            {
                _log.LogInformation("根据科室、医生预约确认超时");
                return default;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("HIS 接口无法连接");
            }
            var responseText = await response.Content.ReadAsStringAsync();
            _log.LogInformation("根据科室、医生预约确认，Url: {Url}, Request：{Request}, Response: {Response}", httpClient.BaseAddress + _configuration["HisApiSettings:ReservationConfirm"], JsonHelper.SerializeObject(req), responseText);

            CommonHttpResult<ReservationConfirmResultDto> result;
            try
            {
                result = JsonSerializer.Deserialize<CommonHttpResult<ReservationConfirmResultDto>>(responseText,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        });
            }
            catch (Exception)
            {
                _log.LogInformation("根据科室、医生预约确认，Url: {Url}, Request：{Request}, Response: {Response}", httpClient.BaseAddress + _configuration["HisApiSettings:cancelRegisterInfo"], JsonHelper.SerializeObject(req), responseText);
                throw new Exception("无法正常处理 HIS 预约接口无法正常处理");
            }
            if (result.Code != 0)
            {
                throw new Exception(result.Msg);
            }

            return result.Data;
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
            //if (isInfant && !string.IsNullOrEmpty(input.IdentityNo))
            //{// 如果标识儿童，但是却有身份证号码，则不走儿童的建档流程，走普通建档流程
            //    isInfant = false;
            //}
            if (input.CrowdCode == "Crowd_Child")
            {
                isInfant = true;
            }
            List<string> validateErrors = new List<string>();
            if (input.IsNoThree)
            {// 三无建档
                if (string.IsNullOrEmpty(input.CardNo))
                {
                    return Task.FromResult(JsonResult.Fail("三无人员必须绑定就诊卡"));
                }
            }
            else if (isInfant)
            {// 婴幼儿建档
                if ((string.IsNullOrEmpty(input.GuardianPhone) || string.IsNullOrEmpty(input.ContactsPerson) || string.IsNullOrEmpty(input.SocietyRelationCode) || string.IsNullOrEmpty(input.ContactsAddress)))
                {
                    validateErrors.Add("婴幼儿建档需输入联系人姓名、电话、地址、与联系人关系");
                }
                if (string.IsNullOrEmpty(input.CardNo))
                {
                    return Task.FromResult(JsonResult.Fail("婴幼儿建档必须绑定就诊卡"));
                }
            }
            else
            {// 普通建档
                if (string.IsNullOrEmpty(input.ContactsPhone))
                {
                    validateErrors.Add("电话号码必填");
                }
                if (string.IsNullOrEmpty(input.Address))
                {
                    validateErrors.Add("住址必填");
                }
                if (input.IdentityNo.Length != 15 && input.IdentityNo.Length != 18)
                {// 身份证位数校验
                    validateErrors.Add("身份证号码只允许15或18位");
                }
            }
            if (validateErrors.Count() > 0)
            {
                return Task.FromResult(JsonResult.Fail(string.Join(", ", validateErrors)));
            }

            return Task.FromResult(JsonResult.Ok());
        }
        /// <summary>
        /// 获取金湾生命体征信息
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<VitalSignsInfoByJinWan>> GetHisVitalSignsAsync(string serialNumber)
        {
            string sql = $"SELECT top 1 * FROM V_SZYJ_SIGNS where SerialNumber='{serialNumber}' and Status=1 ORDER BY DATE DESC";
            var connectionStringKey = _configuration.GetConnectionString("JWHospitalHIS");
            var hisVatal = await this._dapperRepository.QueryFirstOrDefaultAsync<VitalSignsInfoByJinWan>(sql, dbKey: "JWHospitalHIS", connectionStringKey: connectionStringKey);
            return JsonResult<VitalSignsInfoByJinWan>.Ok(data: hisVatal);
        }
        public Task<JsonResult> CancelRegisterInfoAsync(string regSerialNo)
        {
            return Task.FromResult(JsonResult.Ok());
        }

        public Task<JsonResult> RevisePerson(PatientModifyDto input)
        {
            return Task.FromResult(JsonResult.Ok());
        }

        /// <summary>
        /// 获取护士列表
        /// </summary>
        /// <returns></returns>
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
