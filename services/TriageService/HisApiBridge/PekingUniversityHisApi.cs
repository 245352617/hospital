using DotNetCore.CAP;
using FreeSql;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.PreHospital.Core.Help;
using SamJan.MicroService.PreHospital.TriageService.LGHis;
using SamJan.MicroService.TriageService.Application.Dtos;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>    
    /// 北大医院 HIS Api 部分方法的主部分
    /// 除 同步HIS挂号列表及相关方法 以外的方法
    /// </summary>
    public partial class PekingUniversityHisApi : IHisApi, IPekingUniversityHisApi
    {
        private readonly CommonHisApi _commonHisApi;
        private readonly IPatientInfoRepository _patientInfoRepository;
        private readonly ICallApi _callApi;
        private readonly IRegisterInfoRepository _registerInfoRepository;
        private readonly ITriageConfigAppService _triageConfigService;
        private readonly IDapperRepository _dapperRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICurrentUser _currentUser;
        private readonly ICapPublisher _capPublisher;
        private readonly IDatabase _redis;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly ILogger<PekingUniversityHisApi> _log;
        private readonly IRabbitMqAppService _rabbitMqAppService;
        private readonly BdQueueConfigs _bdQueueConfigs;
        private readonly IHttpContextAccessor _accessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private static readonly AsyncRetryPolicy<HttpResponseMessage> TransientErrorRetryPolicy =
            Policy.HandleResult<HttpResponseMessage>(
                    message => ((int)message.StatusCode == 429 || (int)message.StatusCode >= 500))
                .WaitAndRetryAsync(2, retryAttempt => { return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)); });
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true // Optional: Make the JSON output indented for readability
        };

        public PekingUniversityHisApi(CommonHisApi commonHisApi, IPatientInfoRepository patientInfoRepository,
            IRegisterInfoRepository registerInfoRepository,
            ITriageConfigAppService triageConfigService, IDapperRepository dapperRepository,
            IGuidGenerator guidGenerator,
            IConfiguration configuration, IOptions<BdQueueConfigs> bdQueueConfigs, IUnitOfWorkManager unitOfWorkManager,
            ICurrentUser currentUser,
            ICapPublisher capPublisher, RedisHelper redisHelper,
            IHttpClientHelper httpClientHelper, ILogger<PekingUniversityHisApi> log,
            IRabbitMqAppService rabbitMqAppService, IHttpContextAccessor accessor, IHttpClientFactory httpClientFactory,
            ICallApi callApi)
        {
            this._commonHisApi = commonHisApi;
            this._patientInfoRepository = patientInfoRepository;
            this._registerInfoRepository = registerInfoRepository;
            this._triageConfigService = triageConfigService;
            this._dapperRepository = dapperRepository;
            this._guidGenerator = guidGenerator;
            this._configuration = configuration;
            this._unitOfWorkManager = unitOfWorkManager;
            this._currentUser = currentUser;
            this._capPublisher = capPublisher;
            this._redis = redisHelper.GetDatabase();
            this._httpClientHelper = httpClientHelper;
            this._log = log;
            this._rabbitMqAppService = rabbitMqAppService;
            _accessor = accessor;
            _httpClientFactory = httpClientFactory;
            this._bdQueueConfigs = bdQueueConfigs.Value;
            this._callApi = callApi;
        }

        /// <summary>
        /// 保存分诊前数据操作
        /// 根据设定，看CallSn该由HIS或叫号系统提供
        /// </summary>
        /// <param name="dto">保存分诊dto</param>
        /// <param name="dtoPatient">保存分诊dto转换的PatientInfo对象</param>
        /// <param name="currentDBPatient">现有数据库Patient对象</param>
        /// <param name="isNeedNewCallSn">是否需要获得新排队号</param>
        /// <param name="hasChangedDoctor"></param>
        /// <returns></returns>
        public async Task<CommonHttpResult<PatientInfo>> BeforeSaveTriageRecordAsync(CreateOrUpdatePatientDto dto, PatientInfo dtoPatient, PatientInfo currentDBPatient, bool isNeedNewCallSn, bool hasChangedDoctor)
        {
            var result = new CommonHttpResult<PatientInfo>();
            try
            {
                if (dto.TriageStatus != 1)
                {
                    return result;
                }
                RegisterInfo registerInfo = currentDBPatient.RegisterInfo.OrderBy(x => x.CreationTime).FirstOrDefault();
                //如果是绿通并且没有挂号,则为绿通挂号,推送分诊信息放到挂号后执行(推送分诊信息依赖挂号序号)
                if (!dtoPatient.GreenRoadCode.IsNullOrWhiteSpace() && registerInfo?.RegisterNo == null)
                {
                    //北大patient.VisitNo 数据为建档返回的就诊号，挂号时接口平台需要将就诊号存入CardNo做为入参
                    dtoPatient.CardNo = dto.VisitNo;
                    return result;
                }

                string CallSnFrom = _configuration["PekingUniversity:Call:CallSnFrom"];  //叫号相关配置
                if (CallSnFrom == "HisOnly")
                {
                    await GetCallSnFromHisAsync(dto, dtoPatient, registerInfo, hasChangedDoctor);
                    await this.PushVitalSignsToHis(dtoPatient.PatientId, dtoPatient.VitalSignInfo.Sbp, dtoPatient.VitalSignInfo.Sdp);  //推送生命体征到HIS
                }
                else if (CallSnFrom == "CallServiceOnly")
                {
                    result = await _callApi.GetFromCallServeic(dto, dtoPatient, currentDBPatient);
                }
                else if (CallSnFrom == "Both")
                {
                    await GetCallSnFromHisAsync(dto, dtoPatient, registerInfo, hasChangedDoctor);
                    result = await _callApi.GetFromCallServeic(dto, dtoPatient, currentDBPatient);

                    await this.PushVitalSignsToHis(dtoPatient.PatientId, dtoPatient.VitalSignInfo.Sbp, dtoPatient.VitalSignInfo.Sdp);  //推送生命体征到HIS
                }
                else
                {
                    throw new Exception("BeforeSaveTriageRecordAsync方法错误，PekingUniversity:Call:CallSnFrom未设定值");
                }
            }
            catch (Exception e)
            {
                _log.LogError("推送分诊信息到北大HIS系统进入叫号:", e.Message);
                throw;
            }
            return result;
        }

        // 推送分诊信息到北大HIS系统进入叫号，获得CallSn
        private async Task<bool> GetCallSnFromHisAsync(CreateOrUpdatePatientDto dto, PatientInfo patient, RegisterInfo registerInfo, bool hasChangedDoctor)
        {
            bool shouldPushTriageInfo = (_configuration["PekingUniversity:shouldPushTriageInfo"] ?? "true").ParseToBool();
            // 配置推送到HIS，且有挂号信息
            if (shouldPushTriageInfo && !string.IsNullOrEmpty(registerInfo?.RegisterNo))
            {
                // 移除旧队列
                (bool isTriageInfoUpdated, string queueId) queueResult = await RemoveQueue(dto, patient, registerInfo?.RegisterNo, hasChangedDoctor);

                // 推送分诊信息到 HIS
                int triageLevel = dto.ConsequenceInfo.ActTriageLevel == TriageLevel.ThirdLv.GetDescriptionByEnum() ? 3 : 4;
                var patientName = dto.PatientName.Length > 4 ? dto.PatientName.Substring(0, 4) : dto.PatientName;
                var response = await PushTriageInfoToHis(registerInfo?.RegisterNo, queueResult.queueId, dto.PatientId, patientName,
                                                        dto.TriageUserCode, dto.TriageUserName, triageLevel, dto.ConsequenceInfo.DoctorCode,
                                                        dto.VitalSignInfo.Temp,
                                                        dto.VitalSignInfo.HeartRate, dto.VitalSignInfo.BreathRate, dto.VitalSignInfo.Sbp,
                                                        dto.VitalSignInfo.Sdp, dto.VitalSignInfo.SpO2, queueResult.isTriageInfoUpdated);
                if (response.code != 0)
                {
                    throw new Exception($"调用HIS接口推送分诊数据返回错误:{response.msg}");
                }
                if (!string.IsNullOrEmpty(response.data.jlxh))
                {
                    // 获取真正排队号，实际就是在排队数字前加科室相应的字母：如内科A，外科B
                    var callingSn = this.GetRealSn(queueResult.queueId, response.data.jlxh);
                    _log.LogInformation($"原接口排队号：" + patient.CallingSn + ";新排队号:" + callingSn);
                    patient.CallingSn = callingSn;
                    patient.LogTime = DateTime.Now;
                }
                return true;
            }
            else
            {
                patient.LogTime = DateTime.Now;
                return false;
            }
        }

        /// <summary>
        /// 移除队列
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="patient"></param>
        /// <param name="registerNo"></param>
        /// <param name="hasChangedDoctor"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<(bool isTriageInfoUpdated, string queueId)> RemoveQueue(CreateOrUpdatePatientDto dto, PatientInfo patient, string registerNo, bool hasChangedDoctor)
        {
            try
            {
                string oldTriageDeptCode = string.Empty; // 原科室编码
                bool isFirstTimePush = true; // 是否首次分诊
                bool reQueueByActTriageLevel = false; // 是否重新排队（已分诊且待就诊情况下，修改分诊级别）
                bool reQueueByChangedDoctor = false; //是否重新排队（已经就诊或者就诊中，并且改变就诊医生）
                bool isTriageInfoUpdated = false; // 是否修改分诊信息（不修改科室，医生，级别的时候为true）
                if (dto.TriagePatientInfoId != Guid.Empty)
                {
                    var oldPatientInfo = await this._patientInfoRepository.AsQueryable().AsNoTracking()
                        .Include(x => x.ConsequenceInfo)
                        .FirstOrDefaultAsync(x => x.Id == dto.TriagePatientInfoId);
                    if (oldPatientInfo != null && oldPatientInfo.ConsequenceInfo != null)
                    {
                        oldTriageDeptCode = oldPatientInfo.ConsequenceInfo.TriageDeptCode;
                        var oldDoctorCode = oldPatientInfo.ConsequenceInfo.DoctorCode;
                        var oldActTriageLevel = oldPatientInfo.ConsequenceInfo.ActTriageLevel;
                        // 如果是修改分诊科室，则病人重新回到候诊队列
                        if (oldPatientInfo.TriageStatus == 0)
                        {
                            patient.VisitStatus = VisitStatus.WattingTreat;
                        }
                        else if ((oldTriageDeptCode != null && oldTriageDeptCode != patient.ConsequenceInfo?.TriageDeptCode) || (oldDoctorCode != null && oldDoctorCode != patient.ConsequenceInfo?.DoctorCode))  //TODO:xuwei
                        {
                            patient.VisitStatus = VisitStatus.WattingTreat;
                        }
                        else
                        {
                            patient.VisitStatus = oldPatientInfo.VisitStatus;
                        }

                        if (!string.IsNullOrEmpty(oldActTriageLevel) && oldActTriageLevel != patient.ConsequenceInfo?.ActTriageLevel)
                        {
                            reQueueByActTriageLevel = true;
                        }
                        isFirstTimePush = oldPatientInfo.TriageStatus == 0;
                        reQueueByChangedDoctor = (oldPatientInfo.VisitStatus == VisitStatus.Treated || oldPatientInfo.VisitStatus == VisitStatus.Treating) && hasChangedDoctor;
                        _log.LogInformation($"推送分诊信息到北大HIS系统信息：oldDoctorCode: '{oldDoctorCode}'，oldTriageDeptCode: '{oldTriageDeptCode}'，oldActTriageLevel: '{oldActTriageLevel}',VisitStatus:{patient.VisitStatus}" + JsonConvert.SerializeObject(patient.ConsequenceInfo));
                    }
                }

                // 队列 ID，回写HIS时使用队列ID，HIS通过队列ID判断分诊到哪个诊室
                var depts = await this._triageConfigService.GetTriageConfigByRedisAsync(TriageDict.TriageDepartment
                    .ToString());
                // 查找对应的队列ID
                var deptConfig = depts[TriageDict.TriageDepartment.ToString()]
                    .FirstOrDefault(x => x.TriageConfigCode == dto.ConsequenceInfo.TriageDept);
                string queueId = deptConfig?.HisConfigCode;
                if (string.IsNullOrEmpty(queueId))
                {
                    throw new Exception($"当前科室{deptConfig.TriageConfigName}没有配置对应的HIS队列ID");
                }

                if (!isFirstTimePush && !string.IsNullOrEmpty(oldTriageDeptCode) && !string.IsNullOrEmpty(registerNo))
                {
                    // 已有分诊信息，判断是召回（修改科室）还是修改分诊信息
                    // 查找对应的旧队列ID 
                    var oldDeptConfig = depts[TriageDict.TriageDepartment.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == oldTriageDeptCode);
                    string oldQueueId = oldDeptConfig?.HisConfigCode;
                    _log.LogInformation($"是否移除叫号系统旧队列: 科室改变：{oldQueueId != queueId},医生改变：{reQueueByChangedDoctor},级别改变：{reQueueByActTriageLevel}");
                    if (oldQueueId != queueId || reQueueByChangedDoctor || reQueueByActTriageLevel)
                    {
                        // 调用HIS接口，移除旧队列
                        await this.MoveQueueInfoToHis(registerNo, oldQueueId, dto.PatientId);
                    }
                    else
                    {
                        // 不修改科室，则只修改分诊信息
                        isTriageInfoUpdated = true;
                    }
                }
                return (isTriageInfoUpdated, queueId);
            }
            catch (Exception e)
            {
                _log.LogError("推送分诊信息到北大HIS系统-移除队列:", e.Message);
                throw;
            }
        }

        /// <summary>
        /// 挂号/预约/分诊
        /// </summary>
        /// <param name="patient">患者信息</param>
        /// <param name="doctorCode">医生代码</param>
        /// <param name="doctorName">医生名称</param>
        /// <param name="isUpdated">是否修改分诊信息（false：新增；true：修改）</param>
        /// <param name="hasChangedDoctor">修改预约科室、医生</param>
        /// <param name="isFirstTimePush">是否第一次分诊</param>
        /// <returns></returns>
        public async Task<PatientInfo> RegisterPatientAsync(PatientInfo patient, string doctorCode, string doctorName,
            bool isUpdated, bool hasChangedDoctor, bool isFirstTimePush)
        {
            try
            {
                // 暂存的患者不进行跳出
                if (patient.TriageStatus != 1)
                    return await Task.FromResult(patient);
                // 重要：如果不是绿通则跳出
                // 疑问点：为什么这么做 2023-07-21 by: hushitong 
                if (patient.GreenRoadCode.IsNullOrWhiteSpace())
                    return await Task.FromResult(patient);

                var patientInfoAfterRegister = await _patientInfoRepository.Include(c => c.ConsequenceInfo)
                    .Include(c => c.AdmissionInfo)
                    .Include(c => c.RegisterInfo)
                    .Include(c => c.ScoreInfo)
                    .Include(c => c.VitalSignInfo)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == patient.Id);

                if (patientInfoAfterRegister is null)
                {
                    throw new Exception($"患者信息不存在，患者id: {patient.Id}");
                }

                RegisterInfo registerInfo = null;
                // 如果未挂号，则自动挂号（用RegisterNo挂号流水号进行判断）
                if (patientInfoAfterRegister.RegisterInfo?.Count() <= 0 ||
                    string.IsNullOrWhiteSpace(patientInfoAfterRegister.RegisterInfo?.FirstOrDefault()?.RegisterNo))
                {
                    var dicts = await _triageConfigService.GetTriageConfigByRedisAsync();
                    // 新增挂号信息
                    var deptConfig = dicts[TriageDict.TriageDepartment.ToString()].FirstOrDefault(x =>
                        x.TriageConfigCode == patientInfoAfterRegister.ConsequenceInfo.TriageDeptCode);
                    var dto = patientInfoAfterRegister.BuildAdapter().AdaptToType<PatientReqDto>();
                    dto.deptId = "99";
                    dto.safetyType = "0";
                    dto.name = dto.name.Length > 4 ? dto.name.Substring(0, 4) : dto.name;
                    var msg = JsonSerializer.Serialize(dto);
                    var httpContent = new StringContent(msg);
                    httpContent.Headers.ContentType.MediaType = "application/json";
                    var uri = _configuration.GetSection("HisApiSettings:registerPatient").Value;
                    _log.LogInformation("调用接口平台挂号，url: {Uri}, request: {Serialize}", uri,
                        JsonSerializer.Serialize(dto, options));
                    // var response = await _httpClientHelper.PostAsync(uri, httpContent);
                    var httpClient = _httpClientFactory.CreateClient("HisApi");
                    var response = await httpClient.PostAsync(uri, httpContent);
                    response.EnsureSuccessStatusCode();
                    var responseText = await response.Content.ReadAsStringAsync();
                    _log.LogInformation("调用接口平台挂号，reponse: {Response}", responseText);
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
                            registerInfo.PatientInfoId = patientInfoAfterRegister.Id;

                            // 已挂号
                            patient.VisitStatus = VisitStatus.WattingTreat;

                            // 限制患者基本信息为只读，挂号后不允许修改患者基本信息，这会导致ECIS的患者信息跟HIS的患者信息产生差异
                            patient.IsBasicInfoReadOnly = true;

                            //挂号支付
                            await this.payCurRegisterAsync(resp.visitNum);
                            // 由 HIS 获取挂号序号
                            var hisRegister = await this.GetHisPatientInfoByVisitNoAsync(resp.visitNum);
                            if (hisRegister != null)
                            {
                                registerInfo.RegisterNo = hisRegister.RegisterNo;
                            }
                            //推送分诊信息到HIS并返回排队号
                            patient = await PushTriageInfoToHis(patient, registerInfo.RegisterNo, isFirstTimePush, hasChangedDoctor);
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
                }

                if (patient.VitalSignInfo != null && (!string.IsNullOrWhiteSpace(patient.VitalSignInfo.Temp) ||
                                                      !string.IsNullOrWhiteSpace(patient.Weight) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.Sbp) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.Sdp) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.HeartRate) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.BreathRate) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.SpO2) ||
                                                      patient.VitalSignInfo.BloodGlucose.HasValue ||
                                                      !string.IsNullOrWhiteSpace(
                                                          patient.VitalSignInfo.ConsciousnessName) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.CardiogramName)))
                {
                    // 登记生命体征
                    _ = await this.PatientVitalSign(patient,
                            registerInfo ?? patientInfoAfterRegister.RegisterInfo.OrderBy(x => x.RegisterTime)
                                .FirstOrDefault())
                        .ConfigureAwait(false);
                }


                _log.LogInformation("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号结束】");

                if (registerInfo != null && !registerInfo.RegisterNo.IsNullOrWhiteSpace())
                {
                    patientInfoAfterRegister.RegisterInfo ??= new List<RegisterInfo>();
                    patientInfoAfterRegister.RegisterInfo.Add(registerInfo);
                }

                return patientInfoAfterRegister;
            }
            catch (Exception e)
            {
                _log.LogWarning("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号错误】【Msg：{Msg}】", e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 推送分诊信息并返回排队号
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="registerNo"></param>
        /// <param name="isFirstTimePush"></param>
        /// <param name="hasChangedDoctor"></param>
        public async Task<PatientInfo> PushTriageInfoToHis(PatientInfo patient, string registerNo, bool isFirstTimePush, bool hasChangedDoctor)
        {
            string oldTriageDeptCode = string.Empty; // 原科室编码
            string oldDoctorCode = string.Empty; // 原医生编码
            VisitStatus visitStatus = default;
            var reQueue = false;
            if (patient.Id != Guid.Empty)
            {
                var oldPatientInfo = await this._patientInfoRepository.AsNoTracking()
                    .Include(x => x.ConsequenceInfo)
                    .FirstOrDefaultAsync(x => x.Id == patient.Id);
                oldTriageDeptCode = oldPatientInfo?.ConsequenceInfo?.TriageDeptCode;
                oldDoctorCode = oldPatientInfo?.ConsequenceInfo?.DoctorCode;
                var oldActTriageLevel = oldPatientInfo?.ConsequenceInfo?.ActTriageLevel;

                // 如果是修改分诊科室，则病人重新回到候诊队列
                if (oldTriageDeptCode != null && oldTriageDeptCode != patient.ConsequenceInfo?.TriageDeptCode)
                {
                    patient.VisitStatus = VisitStatus.WattingTreat;
                    _log.LogInformation("修改分诊科室或分诊医生，召回叫号队列: {PatientName},{VisitStatus}", patient.PatientName, patient.VisitStatus);
                }
                visitStatus = oldPatientInfo.VisitStatus;

                if (patient.VisitStatus == VisitStatus.WattingTreat && !string.IsNullOrEmpty(oldActTriageLevel) && oldActTriageLevel != patient.ConsequenceInfo?.ActTriageLevel)
                {
                    reQueue = true;
                }
            }
            // 推送分诊信息到北大HIS系统进入叫号，急诊屏幕显示
            var dto = patient.BuildAdapter().AdaptToType<CreateOrUpdatePatientDto>();
            var queueInfo =
                await PushTrigeInfoToPekingUniversityHospitalHis(registerNo, oldTriageDeptCode, dto,
                    isFirstTimePush, hasChangedDoctor, reQueue, visitStatus);
            _log.LogInformation("【PushTriageInfoToHis】推送分诊信息到北大HIS系统进入叫号:" + JsonConvert.SerializeObject(queueInfo));

            if (!string.IsNullOrEmpty(queueInfo?.CallingSn))
            {
                // 修改排队号信息
                patient.CallingSn = queueInfo.CallingSn;
                patient.LogTime = queueInfo.InQueueTime;
            }
            return patient;
        }

        /// <summary>
        /// 取消分诊（已分诊患者，返回到未分诊队列）
        /// </summary>
        public async Task<JsonResult> ReturnToNoTriage(Guid id)
        {
            _log.LogInformation($"调用取消分诊接口，患者ID：{id}");

            var patientInfo = await _patientInfoRepository
                                        .Include(c => c.ConsequenceInfo)
                                        .Include(r => r.RegisterInfo)
                                        .OrderByDescending(p => p.CreationTime)
                                        .FirstOrDefaultAsync(x => x.Id == id);
            if (patientInfo == null)
            {
                return JsonResult.Fail("患者信息不存在");
            }

            using var uow = this._unitOfWorkManager.Begin();
            try
            {
                // 提前获得患者分诊到的科室Code，以便后续获得HIS对应队列ID使用
                string triageDeptCode = patientInfo.ConsequenceInfo?.TriageDeptCode;

                #region 患者属性设置
                patientInfo.StartTriageTime = null; //开始分诊时间
                patientInfo.TriageTime = null; //分诊时间
                patientInfo.TriageStatus = 0; //分诊状态,0-未分诊，1-已分诊
                patientInfo.VisitStatus = VisitStatus.NotTriageYet; //重置就诊状态为未分诊
                patientInfo.TriageUserCode = null;
                patientInfo.TriageUserName = null;
                patientInfo.CallingSn = null; //排队号
                patientInfo.LogTime = null;
                patientInfo.FirstDoctorCode = null;
                patientInfo.FirstDoctorName = null;
                patientInfo.DoctorName = null;
                patientInfo.BeginTime = null;
                patientInfo.EndTime = null;

                //更新分诊记录信息
                if (patientInfo.ConsequenceInfo != null)
                {
                    patientInfo.ConsequenceInfo.TriageAreaCode = null; //分诊区域编码
                    patientInfo.ConsequenceInfo.TriageAreaName = null; //分诊区域名称
                    patientInfo.ConsequenceInfo.TriageDeptCode = null; //科室变更Code
                    patientInfo.ConsequenceInfo.TriageDeptName = null; //科室变更名称
                    patientInfo.ConsequenceInfo.TriageTargetCode = null; //分诊去向编码
                    patientInfo.ConsequenceInfo.TriageTargetName = null; //分诊去向名称
                    patientInfo.ConsequenceInfo.ActTriageLevel = null;
                    patientInfo.ConsequenceInfo.ActTriageLevelName = null;
                    patientInfo.ConsequenceInfo.AutoTriageLevel = null;
                    patientInfo.ConsequenceInfo.AutoTriageLevelName = null;
                    patientInfo.ConsequenceInfo.ChangeLevel = null;
                    patientInfo.ConsequenceInfo.ChangeLevelName = null;
                    patientInfo.ConsequenceInfo.ChangeDept = null;
                    patientInfo.ConsequenceInfo.ChangeDeptName = null;
                    patientInfo.ConsequenceInfo.ChangeTriageReasonCode = null;
                    patientInfo.ConsequenceInfo.ChangeTriageReasonName = null;
                    patientInfo.ConsequenceInfo.DoctorName = null;
                    patientInfo.ConsequenceInfo.DoctorCode = null;
                    patientInfo.ConsequenceInfo.WorkType = null;
                }
                #endregion

                await _patientInfoRepository.UpdateAsync(patientInfo);

                #region 调用HIS接口，移除队列信息
                // 队列 ID，回写HIS时使用队列ID，HIS通过队列ID判断分诊到哪个诊室
                var depts = await this._triageConfigService.GetTriageConfigByRedisAsync(TriageDict.TriageDepartment.ToString());
                // 查找对应的旧队列ID 
                var oldDeptConfig = depts[TriageDict.TriageDepartment.ToString()]
                    .FirstOrDefault(x => x.TriageConfigCode == triageDeptCode);
                string oldQueueId = oldDeptConfig?.HisConfigCode;
                //调用接口平台移除队列信息
                var result = await this.MoveQueueInfoToHis(ghxh: patientInfo.RegisterInfo?.First()?.RegisterNo, dlid: oldQueueId, brid: patientInfo.PatientId);
                #endregion

                #region 推送MQ消息到医生站
                var patientMqDto = new PatientInfoMqDto
                {
                    PatientInfo = patientInfo.BuildAdapter().AdaptToType<PatientInfoDto>(),
                    ConsequenceInfo = patientInfo.ConsequenceInfo?.BuildAdapter().AdaptToType<ConsequenceInfoDto>(),
                    VitalSignInfo = patientInfo.VitalSignInfo?.BuildAdapter().AdaptToType<VitalSignInfoDto>(),
                    ScoreInfo = patientInfo.ScoreInfo?.BuildAdapter().AdaptToType<List<ScoreInfoDto>>(),
                    RegisterInfo = patientInfo.RegisterInfo?.OrderByDescending(o => o.RegisterTime).FirstOrDefault().BuildAdapter().AdaptToType<RegisterInfoDto>(),
                    AdmissionInfo = patientInfo.AdmissionInfo?.BuildAdapter().AdaptToType<AdmissionInfoDto>()
                };

                // 推送MQ消息到医生站
                await _rabbitMqAppService.PublishEcisPatientSyncPatientAsync(new List<PatientInfoMqDto> { patientMqDto });
                #endregion

                await uow.CompleteAsync();
                return JsonResult.Ok();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                return JsonResult.Fail(ex.StackTrace + ex.Message);
            }
        }

        public async Task<bool> MoveToTop(string registerNo)
        {
            string msgComent = "置顶";
            string uri = $"{_configuration["PekingUniversity:Call:MoveToTopUrl"]}";
            uri = string.Format(uri, registerNo);
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                string jsonContent = JsonSerializer.Serialize(new
                {
                    RegisterNo = registerNo
                }, options);
                StringContent stringContent = new StringContent(jsonContent);

                HttpClient httpClient = _httpClientFactory.CreateClient();
                HttpResponseMessage response = await httpClient.PutAsync(uri, stringContent);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
                sw.Stop();

                _log.LogInformation($"调用Call接口，{msgComent}，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {responseText}");
                if (string.IsNullOrEmpty(responseText))
                {
                    var msg = $"调用Call接口，{msgComent}失败，无返回，url：{uri}";
                    _log.LogError(msg);
                    throw new Exception(msg);
                }

                dynamic callResponse = JsonConvert.DeserializeObject<dynamic>(responseText);
                if (callResponse.code != 200)
                {
                    throw new Exception($"调用Call接口，{msgComent}失败，url：{uri}，原因：{callResponse.message}");
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CancelMoveToTop(string registerNo)
        {
            string msgComent = "取消置顶";
            string uri = $"{_configuration["PekingUniversity:Call:CancelMoveToTopUrl"]}";
            uri = string.Format(uri, registerNo);
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                string jsonContent = JsonSerializer.Serialize(new
                {
                    RegisterNo = registerNo
                }, options);
                StringContent stringContent = new StringContent(jsonContent);

                HttpClient httpClient = _httpClientFactory.CreateClient();
                HttpResponseMessage response = await httpClient.PutAsync(uri, stringContent);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
                sw.Stop();

                _log.LogInformation($"调用Call接口，{msgComent}，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {responseText}");
                if (string.IsNullOrEmpty(responseText))
                {
                    var msg = $"调用Call接口，{msgComent}失败，无返回，url：{uri}";
                    _log.LogError(msg);
                    throw new Exception(msg);
                }

                dynamic callResponse = JsonConvert.DeserializeObject<dynamic>(responseText);
                if (callResponse.code != 200)
                {
                    throw new Exception($"调用Call接口，{msgComent}失败，url：{uri}，原因：{callResponse.message}");
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ReturnToQueue(string registerNo)
        {
            string msgComent = "过号患者恢复候诊";
            string uri = $"{_configuration["PekingUniversity:Call:ReturnToQueueUrl"]}";
            uri = string.Format(uri, registerNo);
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                string jsonContent = JsonSerializer.Serialize(new
                {
                    RegisterNo = registerNo
                }, options);
                StringContent stringContent = new StringContent(jsonContent);

                HttpClient httpClient = _httpClientFactory.CreateClient();
                HttpResponseMessage response = await httpClient.PostAsync(uri, stringContent);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
                sw.Stop();

                _log.LogInformation($"调用Call接口，{msgComent}，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {responseText}");
                if (string.IsNullOrEmpty(responseText))
                {
                    var msg = $"调用Call接口，{msgComent}失败，无返回，url：{uri}";
                    _log.LogError(msg);
                    throw new Exception(msg);
                }

                dynamic callResponse = JsonConvert.DeserializeObject<dynamic>(responseText);
                if (callResponse.code != 200)
                {
                    throw new Exception($"调用Call接口，{msgComent}失败，url：{uri}，原因：{callResponse.message}");
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #region 没使用
        /// <summary>
        /// 获取医保信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<(bool, InsuranceDto, InsuranceType)> GetInsuInfo(PatientInfo patient)
        {
            var dicts = await _triageConfigService.GetTriageConfigByRedisAsync();
            // 获取费别信息
            var faber = dicts[TriageDict.Faber.ToString()].FirstOrDefault(x =>
                x.TriageConfigType == (int)TriageDict.Faber && x.TriageConfigCode == patient.ChargeType);
            // 医保相关信息
            _log.LogInformation("开始挂号, 费别: {Faber}, 医保代码：{ExtraCode}", patient.ChargeType,
                faber?.TriageConfigName, faber?.ExtraCode);
            InsuranceDto insuranceDto = null; // 医保信息
            // 如果开启绿色通道则是先诊疗后付费的流程，在预检分诊如果开通绿通，则不记录费别及医保信息
            if (!patient.GreenRoadCode.IsNullOrWhiteSpace()) return (false, null, null);
            if (string.IsNullOrEmpty(faber?.ExtraCode)) return (false, null, null);
            if (!_redis.KeyExists($"{_configuration["ServiceName"]}:Insurance:{patient.IdentityNo}:{patient.InsuplcAdmdvCode}"))
                return (false, null, null);

            // 缓存数据
            var cacheString = (await _redis.StringGetAsync(
                $"{_configuration["ServiceName"]}:Insurance:{patient.IdentityNo}:{patient.InsuplcAdmdvCode}")).ToString();
            // 从缓存中读取医保信息      
            if (!string.IsNullOrEmpty(cacheString))
            {
                insuranceDto = JsonSerializer.Deserialize<InsuranceDto>(cacheString);
                _log.LogInformation("从缓存读取医保信息：{Insurance}", insuranceDto);

                if (!insuranceDto.InsuranceTypes.Any(x => x.clctstd_crtf_rule_codg == faber.ExtraCode))
                {
                    throw new Exception($"该患者不可使用医保费别【{faber.TriageConfigName}】");
                }
            }

            InsuranceType insuranceType =
                insuranceDto?.InsuranceTypes?.FirstOrDefault(x =>
                    x.clctstd_crtf_rule_codg == faber.ExtraCode);

            return (insuranceDto != null && insuranceType != null, insuranceDto, insuranceType);
        }

        private async Task ModifyRegInformations(string doctorCode, string doctorName, PatientInfo patientReadonly)
        {
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var uri = _configuration["HisApiSettings:ModifyRegInfo"];
            var dicts =
                await _triageConfigService.GetTriageConfigByRedisAsync(TriageDict.TriageDepartment.ToString());
            var deptConfig = dicts[TriageDict.TriageDepartment.ToString()].FirstOrDefault(x =>
                x.TriageConfigCode == patientReadonly.ConsequenceInfo.TriageDeptCode);
            ModifyRegInfoReq request = new ModifyRegInfoReq
            {
                visitNo = patientReadonly.VisitNo,
                regSerialNo = patientReadonly.RegisterInfo.OrderBy(x => x.RegisterTime).FirstOrDefault()?.RegisterNo,
                //regSerialNo = "",
                deptCode = deptConfig.HisConfigCode,
                deptName = deptConfig.TriageConfigName,
                doctorCode = doctorCode,
                doctorName = doctorName,
                roomCode = "",
                roomName = "",
            };
            var responseText = await PostAsync(httpClient, uri, request, "调用HIS接口修改挂号信息");
            CommonHttpResult result;
            try
            {
                result = JsonSerializer.Deserialize<CommonHttpResult>(responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
            }
            catch (Exception)
            {
                _log.LogInformation("调用HIS接口修改挂号信息，Url: {Url}, Request：{Request}, Response: {Response}",
                    httpClient.BaseAddress + uri, JsonHelper.SerializeObject(request), responseText);
                throw new Exception("无法正常处理 HIS 接口返回数据");
            }

            if (result.Code != 0)
            {
                throw new Exception($"HIS接口返回错误：{result.Msg}");
            }
        }

        /// <summary>
        /// 发送退号消息
        /// </summary>
        /// <param name="existsRegisterInfo"></param>
        /// <returns></returns>
        private async Task SendRefundRegisterNoMq(RegisterInfo existsRegisterInfo)
        {
            var taskInfoId = await _patientInfoRepository.AsNoTracking()
                .Where(x => x.Id == existsRegisterInfo.PatientInfoId)
                .Select(s => s.TaskInfoId)
                .FirstOrDefaultAsync();

            var patient = await _patientInfoRepository.Include(c => c.ConsequenceInfo)
                .Include(c => c.VitalSignInfo)
                .Include(c => c.RegisterInfo)
                .Include(c => c.AdmissionInfo)
                .Include(c => c.ScoreInfo)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == existsRegisterInfo.PatientInfoId);
            if (patient != null)
            {
                var eto = patient.BuildAdapter().AdaptToType<SyncPatientEventBusEto>();
                eto.VisitNo = existsRegisterInfo.VisitNo;
                eto.RegisterNo = "已退号";

                #region 推送院前分诊患者信息到急诊分诊

                var mqDto = new PatientInfoMqDto
                {
                    PatientInfo = patient.BuildAdapter().AdaptToType<PatientInfoDto>(),
                    ConsequenceInfo = patient.ConsequenceInfo?.BuildAdapter().AdaptToType<ConsequenceInfoDto>(),
                    VitalSignInfo = patient.VitalSignInfo?.BuildAdapter().AdaptToType<VitalSignInfoDto>(),
                    ScoreInfo = patient.ScoreInfo?.BuildAdapter().AdaptToType<List<ScoreInfoDto>>(),
                    RegisterInfo = new RegisterInfoDto
                    {
                        RegisterNo = eto.RegisterNo,
                        VisitNo = eto.VisitNo
                    },
                    AdmissionInfo = patient.AdmissionInfo?.BuildAdapter().AdaptToType<AdmissionInfoDto>()
                };

                await _capPublisher.PublishAsync("patient.from.preHospital", mqDto);

                #endregion

                var etoList = new List<SyncPatientEventBusEto> { eto };
                var dicts = await _triageConfigService.GetTriageConfigByRedisAsync();
                await _rabbitMqAppService.PublishSixCenterSyncPatientAsync(etoList, dicts);
                await this._capPublisher.PublishAsync("sync.patient.register.cancel", new { PI_ID = patient.Id });
            }
        }
        #endregion

        /// <summary>
        /// 调用HIS接口登记生命体征信息
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="registerInfo"></param>
        /// <returns></returns>
        private async Task<JsonResult> PatientVitalSign(PatientInfo patient, RegisterInfo registerInfo)
        {
            // 未开启登记生命体体征时不做处理
            if (!_configuration.GetValue<bool>("HisApiSettings:EnablePatientVitalSign"))
                return JsonResult.Ok(msg: "不推送生命体征信息");
            if (registerInfo == null) return JsonResult.Ok(msg: "患者未挂号，不推送生命体征信息");

            var dicts =
                await _triageConfigService.GetTriageConfigByRedisAsync(TriageDict.TriageDepartment.ToString());
            var deptConfig = dicts[TriageDict.TriageDepartment.ToString()]
                .FirstOrDefault(x => x.TriageConfigCode == patient.ConsequenceInfo.TriageDeptCode);
            if (deptConfig == null) return JsonResult.Ok(msg: "科室不存在");

            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var uri = _configuration["HisApiSettings:PatientVitalSign"];
            var signDetailReqs = new List<SignDetailReq>
            {
                // 体温
                new SignDetailReq
                {
                    itemCode = "1",
                    itemName = "体温",
                    itemValue = patient.VitalSignInfo.Temp,
                    unit = "℃",
                    signId = "1",
                },
                // 体重
                new SignDetailReq
                {
                    itemCode = "2",
                    itemName = "体重",
                    itemValue = patient.Weight,
                    unit = "kg",
                    signId = "2",
                },
                // 收缩压
                new SignDetailReq
                {
                    itemCode = "3",
                    itemName = "收缩压",
                    itemValue = patient.VitalSignInfo.Sbp,
                    unit = "mmHg",
                    signId = "3",
                },
                // 舒张压
                new SignDetailReq
                {
                    itemCode = "4",
                    itemName = "舒张压",
                    itemValue = patient.VitalSignInfo.Sdp,
                    unit = "mmHg",
                    signId = "4",
                },
                // 心率
                new SignDetailReq
                {
                    itemCode = "5",
                    itemName = "心率",
                    itemValue = patient.VitalSignInfo.HeartRate,
                    unit = "次/分",
                    signId = "5",
                },
                // 脉搏
                new SignDetailReq
                {
                    itemCode = "6",
                    itemName = "脉搏",
                    itemValue = patient.VitalSignInfo.HeartRate,
                    unit = "次/分",
                    signId = "6",
                },
                // 呼吸
                new SignDetailReq
                {
                    itemCode = "7",
                    itemName = "呼吸",
                    itemValue = patient.VitalSignInfo.BreathRate,
                    unit = "次/分",
                    signId = "7",
                },
                // 血氧
                new SignDetailReq
                {
                    itemCode = "8",
                    itemName = "血氧",
                    itemValue = patient.VitalSignInfo.SpO2,
                    unit = "%",
                    signId = "8",
                },
                // 血糖
                new SignDetailReq
                {
                    itemCode = "9",
                    itemName = "血糖",
                    itemValue = patient.VitalSignInfo.BloodGlucose?.ToString(),
                    unit = "mmol/L",
                    signId = "9",
                },
                // 意识
                new SignDetailReq
                {
                    itemCode = "10",
                    itemName = "意识",
                    itemValue = patient.ConsciousnessName,
                    unit = "",
                    signId = "10",
                },
                // 病情等级
                new SignDetailReq
                {
                    itemCode = "11",
                    itemName = "病情等级",
                    itemValue = "",
                    unit = "",
                    signId = "11",
                },
                // 分区
                new SignDetailReq
                {
                    itemCode = "12",
                    itemName = "分区",
                    itemValue = "",
                    unit = "",
                    signId = "12",
                },
                // 血氧饱和度
                new SignDetailReq
                {
                    itemCode = "13",
                    itemName = "血氧饱和度",
                    itemValue = patient.VitalSignInfo.SpO2,
                    unit = "%",
                    signId = "13",
                },
                // 是否已做心电图
                new SignDetailReq
                {
                    itemCode = "14",
                    itemName = "是否已做心电图",
                    itemValue = patient.VitalSignInfo.CardiogramName,
                    unit = "",
                    signId = "14",
                },
            };
            PatientVitalSign request = new PatientVitalSign
            {
                deptCode = deptConfig?.HisConfigCode,
                deptName = deptConfig?.TriageConfigCode,
                patientGender = patient.Sex switch
                {
                    "Sex_Man" => 1,
                    "Sex_Woman" => 2,
                    _ => 3
                },
                patientName = patient.PatientName,
                patientType = "1",
                recorderCode = _currentUser.UserName,
                recorderName = _currentUser.GetFullName(),
                regDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                regSerialNo = registerInfo.RegisterNo,
                visitNo = patient.VisitNo,
                visitSerialNo = "",
                signDetailReqs = signDetailReqs
            };

            var responseText = await PostAsync(httpClient, uri, request, "调用HIS接口登记生命体征信息");
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
                _log.LogInformation("调用HIS接口登记生命体征信息，Url: {Url}, Request：{Request}, Response: {Response}",
                    httpClient.BaseAddress + uri, JsonHelper.SerializeObject(request), responseText);
                //throw new Exception("无法正常处理 HIS 接口返回数据");
                return JsonResult.Fail("无法正常处理 HIS 接口返回数据");
            }
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

        /// <summary>
        /// 推送分诊信息到北大 HIS 进行叫号系统的排队
        /// </summary>
        /// <param name="registerNo"></param>
        /// <param name="oldDeptCode">修改前的科室</param>
        /// <param name="dto"></param>
        /// <param name="isFirstTimePush">是否首次推送分诊信息（暂存 -> 确认分诊 的情况该参数为true）</param>
        /// <param name="hasChangedDoctor">是否改变医生</param>
        /// <param name="reQueueByActTriageLevel">是否重新排队（改变了分诊级别）</param>
        /// /// <param name="visitStatus">就诊状态</param>
        /// <returns></returns>
        private async Task<HisQueueInfo> PushTrigeInfoToPekingUniversityHospitalHis(string registerNo,
            string oldDeptCode, CreateOrUpdatePatientDto dto, bool isFirstTimePush, bool hasChangedDoctor, bool reQueueByActTriageLevel, VisitStatus visitStatus)
        {
            HisQueueInfo queueInfo = new HisQueueInfo();
            // 配置文件设置是否推送分诊信息到 HIS
            bool shouldPushTriageInfo =
                (_configuration["PekingUniversity:shouldPushTriageInfo"] ?? "true").ParseToBool();
            // 配置推送到HIS，且有挂号信息，且分诊级别是3~4级
            if (shouldPushTriageInfo && !string.IsNullOrEmpty(registerNo))
            {
                // 队列 ID，回写HIS时使用队列ID，HIS通过队列ID判断分诊到哪个诊室
                var depts = await this._triageConfigService.GetTriageConfigByRedisAsync(TriageDict.TriageDepartment
                    .ToString());
                // 查找对应的队列ID
                var deptConfig = depts[TriageDict.TriageDepartment.ToString()]
                    .FirstOrDefault(x => x.TriageConfigCode == dto.ConsequenceInfo.TriageDept);
                string queueId = deptConfig?.HisConfigCode;
                if (string.IsNullOrEmpty(queueId))
                {
                    throw new Exception($"当前科室{deptConfig.TriageConfigName}没有配置对应的HIS队列ID");
                }

                int triageLevel = dto.ConsequenceInfo.ActTriageLevel == TriageLevel.ThirdLv.GetDescriptionByEnum()
                    ? 3
                    : 4;

                bool isTriageInfoUpdated = false; // 是否修改分诊信息（不修改科室、修改分诊信息的时候为true）
                if (!isFirstTimePush && !string.IsNullOrEmpty(oldDeptCode) && !string.IsNullOrEmpty(registerNo))
                {
                    // 已有分诊信息，判断是召回（修改科室）还是修改分诊信息
                    // 查找对应的旧队列ID 
                    var oldDeptConfig = depts[TriageDict.TriageDepartment.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == oldDeptCode);
                    string oldQueueId = oldDeptConfig?.HisConfigCode;

                    //如果当前患者已经就诊，并且改变就诊医生则创建一条新队列重新排队
                    var reQueue = (visitStatus == VisitStatus.Treated && hasChangedDoctor);

                    // reQueueByActTriageLevel: 如果当前患者待就诊，并且改变分诊等级则创建一条新队列重新排队
                    if (oldQueueId != queueId || reQueue || reQueueByActTriageLevel)
                    {
                        // 只有修改科室的，需要移除原科室的队列号
                        // 移除旧队列
                        await this.MoveQueueInfoToHis(registerNo, oldQueueId, dto.PatientId);
                        _log.LogInformation($"叫号系统的排队:移除旧队列");
                    }
                    else
                    {
                        // 不修改科室，则只修改分诊信息
                        isTriageInfoUpdated = true;
                    }
                }
                _log.LogInformation($"准备调用HIS接口推送分诊数据，visitStatus: {visitStatus}，hasChangedDoctor: {hasChangedDoctor},isFirstTimePush:{isFirstTimePush},oldDeptCode:{oldDeptCode},registerNo:{registerNo}");
                HisResponseDto<HisPLHDto> response;
                try
                {
                    var patientName = dto.PatientName.Length > 4 ? dto.PatientName.Substring(0, 4) : dto.PatientName;
                    response = await PushTriageInfoToHis(registerNo, queueId, dto.PatientId, patientName,
                    dto.TriageUserCode, dto.TriageUserName, triageLevel, dto.ConsequenceInfo.DoctorCode,
                    dto.VitalSignInfo.Temp,
                    dto.VitalSignInfo.HeartRate, dto.VitalSignInfo.BreathRate, dto.VitalSignInfo.Sbp,
                    dto.VitalSignInfo.Sdp, dto.VitalSignInfo.SpO2, isTriageInfoUpdated);
                    if (response.code != 0)
                    {
                        throw new Exception($"{response.msg}");
                    }

                    // 修改分诊信息的时候不会返回排队号，所以不判断排队号码  by: ywlin 2022-05-27
                    //if (string.IsNullOrEmpty(response.data.jlxh))
                    //{
                    //    throw new Exception($"HIS接口返回排队号码为空");
                    //}
                    if (!string.IsNullOrEmpty(response.data.jlxh))
                    {
                        // 获取排队号
                        queueInfo.CallingSn = this.GetRealSn(queueId, response.data.jlxh);
                        queueInfo.InQueueTime = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"调用HIS接口推送分诊数据错误！原因：{ex.Message}");
                }

                return queueInfo;
            }

            return null;
        }

        private string GetRealSn(string queueId, string callingSn)
        {
            var config = this._bdQueueConfigs[queueId];

            return config?.SnPrefix + callingSn;
        }

        #region 同步接口平台，调用HIS接口
        /// <summary>
        /// 北大队列移除（分诊改变科室时需要移除旧的排队信息）
        /// </summary>
        /// <param name="ghxh">挂号序号</param>
        /// <param name="dlid">队列ID</param>
        /// <param name="brid">病人ID</param>
        /// <returns></returns>
        private async Task<HisResponseDto> MoveQueueInfoToHis(string ghxh, string dlid, string brid)
        {
            var uri = $"{_configuration["PekingUniversity:pushTriageInfoUrl"]}?" +
                      $"ghxh={ghxh}" +
                      $"&brid={brid}" +
                      $"&dlid={dlid}" +
                      $"&flag=1"; // flag = 1 代表移除队列
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                var response = await _httpClientHelper.GetAsync(uri);
                sw.Stop();
                _log.LogInformation($"调用HIS接口,移除队列，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {response}");
                if (string.IsNullOrEmpty(response))
                {
                    _log.LogError($"调用HIS接口，移除队列失败，无返回，url：{uri}");
                    throw new Exception($"调用HIS接口移除队列失败，无返回，url：{uri}");
                }

                HisResponseDto hisResponse = JsonSerializer.Deserialize<HisResponseDto>(response);
                if (hisResponse.code != 0)
                {
                    throw new Exception($"调用HIS接口，移除队列失败，原因：{hisResponse.msg}");
                }

                return hisResponse;
            }
            catch (Exception)
            {
                _log.LogError($"调用HIS接口，移除队列失败，url：{uri}");
                throw new Exception($"调用HIS接口，移除队列失败，url：{uri}");
            }
        }

        /// <summary>
        /// 推送分诊信息到 HIS
        /// </summary>
        /// <param name="ghxh">挂号序号</param>
        /// <param name="dlid">队列ID</param>
        /// <param name="brid">病人ID</param>
        /// <param name="brxm">病人姓名</param>
        /// <param name="hsqm">分诊护士工号</param>
        /// <param name="hsxm">分诊护士姓名</param>
        /// <param name="fzjb">分诊级别（数值型，现在的值有3、4）</param>
        /// <param name="ysdm">医生代码</param>
        /// <param name="isUpdated">是否修改患者信息，新增flag传0，修改flag传2</param>
        /// <returns></returns>
        private async Task<HisResponseDto<HisPLHDto>> PushTriageInfoToHis(string ghxh, string dlid, string brid,
            string brxm, string hsqm, string hsxm, int fzjb, string ysdm, string tw,
            string mb, string hx, string ssy, string szy, string sp02, bool isUpdated)
        {
            /* ---------参数说明-------------
            ghxh 字符串; // 挂号序号
            dlid 字符串;// 队列ID
            brid 字符串;// 病人ID
            brxm 字符串;// 病人姓名
            hsqm 字符串;// 分诊护士工号
            hsxm 字符串;// 分诊护士姓名
            fzjb 整型;// 分诊级别（数值型，现在的值有3、4）
            ysdm 字符串;// 医生代码
            tw 字符串;// 体温
            flag 整型;// 标志（0正常，1删除，2修改）
            yz 整型;// 0普通 1预约 2优诊
            ---------参数说明------------- */
            var uri = $"{_configuration["PekingUniversity:pushTriageInfoUrl"]}?" +
                      $"ghxh={ghxh}" +
                      $"&dlid={dlid}" +
                      $"&brid={brid}" +
                      $"&brxm={brxm}" +
                      $"&hsqm={hsqm}" +
                      $"&hsxm={hsxm}" +
                      $"&fzjb={fzjb}" +
                      $"&ysdm={ysdm}" +
                      (!string.IsNullOrEmpty(tw) ? $"&tw={tw}" : "") +
                      (!string.IsNullOrEmpty(mb) ? $"&mb={mb}" : "") +
                      (!string.IsNullOrEmpty(hx) ? $"&hx={hx}" : "") +
                      (!string.IsNullOrEmpty(ssy) ? $"&ssy={ssy}" : "") +
                      (!string.IsNullOrEmpty(szy) ? $"&szy={szy}" : "") +
                      (!string.IsNullOrEmpty(sp02) ? $"&sp02={sp02}" : "") +
                      $"&flag={(isUpdated ? 2 : 0)}" +
                      $"&yz=0";
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                var response = await _httpClientHelper.GetAsync(uri);
                sw.Stop();
                _log.LogInformation($"调用HIS接口,推送分诊数据，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {response}");
                if (string.IsNullOrEmpty(response))
                {
                    _log.LogError($"调用HIS接口,推送分诊数据失败，无返回，url：{uri}");
                    throw new Exception($"调用HIS接口,推送分诊数据失败，无返回，url：{uri}");
                }

                HisResponseDto<object> hisResponse = JsonSerializer.Deserialize<HisResponseDto<object>>(response);
                if (hisResponse.code != 0)
                {
                    throw new Exception($"调用HIS接口，返回排队失败，url：{uri}，原因：{hisResponse.data}");
                }

                HisPLHDto data = JsonSerializer.Deserialize<HisPLHDto>(hisResponse.data.ToString());
                //排队号补零到3位数
                if (!string.IsNullOrWhiteSpace(data.jlxh))
                {
                    data.jlxh = data.jlxh.PadLeft(3, '0');
                }
                HisResponseDto<HisPLHDto> result = new HisResponseDto<HisPLHDto>
                {
                    code = hisResponse.code,
                    data = data,
                };

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 调用HIS接口，推送生命体征到HIS
        /// </summary>
        /// <returns></returns>
        private async Task PushVitalSignsToHis(string brid, string ssy, string szy)
        {
            var uri = $"{_configuration["PekingUniversity:pushVitalSignsUrl"]}?" +
                      $"brid={brid}" +
                      $"&ssy={ssy}" +
                      $"&szy={szy}";
            try
            {
                var requestBody = JsonSerializer.Serialize(new { brid, ssy, szy });
                var httpContent = new StringContent(requestBody);
                httpContent.Headers.ContentType.MediaType = "application/json";

                Stopwatch sw = Stopwatch.StartNew();
                var response = await _httpClientHelper.PostAsync(uri, httpContent);
                sw.Stop();

                _log.LogInformation(
                    $"调用HIS接口，同步生命体征，共耗时：{sw.ElapsedMilliseconds}，url: {uri}, body: {JsonSerializer.Serialize(requestBody, options)}，返回: {response}");
                if (string.IsNullOrEmpty(response))
                {
                    //经测试，接口平台无返回数据，同步生命体征成功
                    //_log.LogError($"调用HIS接口同步生命体征成功，无返回，url：{uri}");
                    //throw new Exception($"调用HIS接口同步生命体征失败，无返回，url：{uri}");
                }
                else
                {
                    HisResponseDto hisResponse = JsonSerializer.Deserialize<HisResponseDto>(response);
                    if (hisResponse.code != 0)
                    {
                        throw new Exception($"调用HIS接口，同步生命体征失败，原因：{hisResponse.msg}");
                    }
                }

            }
            catch (Exception ex)
            {
                _log.LogError($"调用HIS接口，推送生命体征接口到北大HIS系统 Error：url{uri}_", ex.Message);
            }
        }

        /// <summary>
        /// 调用HIS接口，获取医生列表
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
                Stopwatch sw = Stopwatch.StartNew();
                var response = await httpClient.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("平台接口无法连接");
                }
                sw.Stop();
                var responseText = await response.Content.ReadAsStringAsync();

                _log.LogInformation($"调用HIS接口，获取医生列表，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {responseText}");

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
                    //foreach (char nameWord in item.DoctorName)
                    //{
                    //    string py = PyHelper.GetFirstPy(nameWord.ToString());
                    //    item.DoctorNamePy += py.FirstOrDefault();
                    //}
                    item.DoctorNamePy = PyHelper.GetFirstPy(item.DoctorName);
                }

                return JsonResult<List<DoctorSchedule>>.Ok(data: result);
            }
            catch (Exception ex)
            {
                _log.LogError($"调用HIS接口，获取医生列表 Error：url{uri}_", ex.Message);
                return JsonResult<List<DoctorSchedule>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 暂停/恢复叫号（挂起状态，医生站不能呼叫、接诊患者）
        /// </summary>
        /// <param name="patientId">患者Id</param>
        /// <param name="isSuspend">是否暂停（0：暂停，1：开启）</param>
        /// <returns></returns>
        public async Task<JsonResult> SuspendCalling(string patientId, bool isSuspend)
        {
            // 获取患者信息
            var patientInfo = await this._patientInfoRepository.AsNoTracking()
                .Include(x => x.RegisterInfo)
                .Include(x => x.ConsequenceInfo)
                .OrderByDescending(x => x.CreationTime)
                .FirstAsync(_ => _.PatientId == patientId);
            if (patientInfo.RegisterInfo.Count <= 0)
            {
                return JsonResult.Fail(msg: "该患者不存在挂号信息");
            }



            // 获取 挂号序号（ghxh）
            var ghxh = patientInfo.RegisterInfo.OrderByDescending(x => x.RegisterTime).First().RegisterNo;
            // 获取对应科室的队列ID
            var depts =
                await this._triageConfigService.GetTriageConfigByRedisAsync(TriageDict.TriageDepartment.ToString());
            var deptConfig = depts[TriageDict.TriageDepartment.ToString()]
                .FirstOrDefault(x => x.TriageConfigCode == patientInfo.ConsequenceInfo.TriageDeptCode);
            // 获取 队列Id（dlid）
            string dlid = deptConfig?.HisConfigCode;
            int pdzt = isSuspend ? 0 : 1;
            // 使用 ghxh、dlid、pdzt 调用接口            
            var uri = $"{_configuration["HisApiSettings:suspendCalling"]}?" +
                      $"ghxh={ghxh}" +
                      $"&dlid={dlid}" +
                      $"&pdzt={pdzt}";
            var requestBody = JsonSerializer.Serialize(new { ghxh, dlid, pdzt });
            var httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType.MediaType = "application/json";
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                var response = await _httpClientHelper.PostAsync(uri, httpContent);
                sw.Stop();
                _log.LogInformation(
                    $"调用HIS接口暂停排队，共耗时：{sw.ElapsedMilliseconds}，url: {uri}, body: {JsonSerializer.Serialize(requestBody, options)}，返回: {response}");
                if (string.IsNullOrEmpty(response))
                {
                    _log.LogError($"调用HIS接口暂停排队失败，无返回，url：{uri}");
                    //throw new Exception($"调用HIS接口暂停排队失败，无返回，url：{uri}");
                }

                HisResponseDto hisResponse = JsonSerializer.Deserialize<HisResponseDto>(response);
                if (hisResponse.code != 0)
                {
                    _log.LogError($"调用HIS接口暂停排队，url：{uri}，原因：{hisResponse.msg}");
                    // throw new Exception($"调用HIS接口暂停排队，url：{uri}，原因：{hisResponse.msg}");
                }
            }
            catch (Exception ex)
            {
                _log.LogError($"调用HIS接口暂停排队异常，原因：{ex}");
            }

            return JsonResult.Ok();
        }

        public async Task SuspendCallingV2(string patientId, bool isSuspend)
        {
            // 获取患者信息
            var patientInfo = await this._patientInfoRepository.AsNoTracking()
                .Include(x => x.RegisterInfo)
                .Include(x => x.ConsequenceInfo)
                .OrderByDescending(x => x.CreationTime)
                .FirstAsync(_ => _.PatientId == patientId);
            if (patientInfo.RegisterInfo.Count <= 0)
            {
                throw new Exception("该患者不存在挂号信息");
            }
            string registerNo = patientInfo.RegisterInfo.OrderByDescending(x => x.RegisterTime).First().RegisterNo;

            if (isSuspend)
            {
                await Pause(registerNo);
            }
            else
            {
                await CancelPause(registerNo);
            }
        }

        /// <summary>
        /// 设置暂停
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        private async Task Pause(string registerNo)
        {
            string msgComent = "设置暂停";
            string uri = $"{_configuration["PekingUniversity:Call:PauseUrl"]}";
            uri = string.Format(uri, registerNo);
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                string jsonContent = JsonSerializer.Serialize(new
                {
                    RegisterNo = registerNo
                }, options);
                StringContent stringContent = new StringContent(jsonContent);

                HttpClient httpClient = _httpClientFactory.CreateClient();
                HttpResponseMessage response = await httpClient.PutAsync(uri, stringContent);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
                sw.Stop();

                _log.LogInformation($"调用Call接口，{msgComent}，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {responseText}");
                if (string.IsNullOrEmpty(responseText))
                {
                    var msg = $"调用Call接口，{msgComent}失败，无返回，url：{uri}";
                    _log.LogError(msg);
                    throw new Exception(msg);
                }

                dynamic callResponse = JsonConvert.DeserializeObject<dynamic>(responseText);
                if (callResponse.code != 200)
                {
                    throw new Exception($"调用Call接口，{msgComent}失败，url：{uri}，原因：{callResponse.message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 取消暂停
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        private async Task CancelPause(string registerNo)
        {
            string msgComent = "置顶";
            string uri = $"{_configuration["PekingUniversity:Call:CancelPauseUrl"]}";
            uri = string.Format(uri, registerNo);
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                string jsonContent = JsonSerializer.Serialize(new
                {
                    RegisterNo = registerNo
                }, options);
                StringContent stringContent = new StringContent(jsonContent);

                HttpClient httpClient = _httpClientFactory.CreateClient();
                HttpResponseMessage response = await httpClient.PutAsync(uri, stringContent);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
                sw.Stop();

                _log.LogInformation($"调用Call接口，{msgComent}，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {responseText}");
                if (string.IsNullOrEmpty(responseText))
                {
                    var msg = $"调用Call接口，{msgComent}失败，无返回，url：{uri}";
                    _log.LogError(msg);
                    throw new Exception(msg);
                }

                dynamic callResponse = JsonConvert.DeserializeObject<dynamic>(responseText);
                if (callResponse.code != 200)
                {
                    throw new Exception($"调用Call接口，{msgComent}失败，url：{uri}，原因：{callResponse.message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 调用HIS接口，转诊接口
        /// </summary>
        /// <param name="patientInfo">病人信息</param>
        /// <param name="triageConfig"></param>
        /// <returns></returns>
        public async Task<HisResponseDto> ReferralAsync(PatientInfo patientInfo, TriageConfig triageConfig)
        {
            var uri = "";
            try
            {
                var dlb = triageConfig.ExtensionField3;
                var ghxh = patientInfo.RegisterInfo?.FirstOrDefault()?.RegisterNo;
                var brxm = patientInfo.PatientName;
                var ksid = triageConfig.HisConfigCode;
                var ghlb = triageConfig.ExtensionField1;
                var ksmc = triageConfig.TriageConfigName;
                var brid = patientInfo.PatientId;
                var dlid = triageConfig.ExtensionField2;
                var ysdm = "";
                var jzxh = 0;
                var hmid = 19;
                uri = $"http://172.16.175.219:8383/socket/hisMz/referralOperation?" +
                      $"dlb={dlb}" +
                      $"&ghxh={ghxh}" +
                      $"&brxm={brxm}" +
                      $"&ksid={ksid}" +
                      $"&ghlb={ghlb}" +
                      $"&ksmc={ksmc}" +
                      $"&brid={brid}" +
                      $"&dlid={dlid}" +
                      $"&ysdm={ysdm}" +
                      $"&jzxh={jzxh}" +
                      $"&hmid={hmid}";
                var requestBody = JsonSerializer.Serialize(new { dlb, ghxh, brxm, ksid, ghlb, ksmc, brid, dlid, ysdm, jzxh, hmid });
                var httpContent = new StringContent(requestBody);
                httpContent.Headers.ContentType.MediaType = "application/json";
                Stopwatch sw = Stopwatch.StartNew();
                var response = await _httpClientHelper.PostAsync(uri, httpContent);
                sw.Stop();
                _log.LogInformation($"调用HIS接口，转诊接口，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {response}");
                if (string.IsNullOrEmpty(response))
                {
                    _log.LogError($"调用HIS接口，转诊接口失败，无返回，url：{uri}");
                    throw new Exception($"调用HIS接口，转诊接口失败，无返回，url：{uri}");
                }

                HisResponseDto hisResponse = JsonSerializer.Deserialize<HisResponseDto>(response);
                if (hisResponse.code != 0)
                {
                    throw new Exception($"调用HIS接口，转诊接口失败，原因：{hisResponse.msg}");
                }

                return hisResponse;
            }
            catch (Exception ex)
            {
                _log.LogError($"调用接口平台转诊接口失败，url：{uri + "------------------" + ex.Message + ex.StackTrace}");
                throw new Exception($"调用接口平台转诊接口失败，url：{uri}");
            }
        }

        /// <summary>
        /// 调用接口平台支付接口
        /// </summary>
        /// <param name="visitNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<HisResponseDto> payCurRegisterAsync(string visitNum)
        {
            var uri = "";
            try
            {
                uri = $"{_configuration["HisApiSettings:payCurRegister"]}?" +
                      $"visitNum={visitNum}";
                _log.LogInformation($"调用接口平台支付接口，url: {uri}");
                var requestBody = JsonSerializer.Serialize(new { visitNum });
                var httpContent = new StringContent(requestBody);
                httpContent.Headers.ContentType.MediaType = "application/json";
                Stopwatch sw = Stopwatch.StartNew();
                var response = await _httpClientHelper.PostAsync(uri, httpContent);
                sw.Stop();
                _log.LogInformation($"调用接口平台支付接口，共耗时：{sw.ElapsedMilliseconds}，url: {uri}，返回: {response}");
                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception($"调用接口平台支付接口失败，无返回，url：{uri}");
                }

                HisResponseDto hisResponse = JsonSerializer.Deserialize<HisResponseDto>(response);
                if (hisResponse.code != 0)
                {
                    throw new Exception($"调用接口平台支付接口失败，原因：{hisResponse.msg}");
                }

                return hisResponse;
            }
            catch (Exception ex)
            {
                _log.LogError($"调用接口平台支付接口报错，url：{uri + "--------" + ex.Message + ex.StackTrace}");
                throw new Exception($"调用接口平台支付接口报错，url：{uri}");
            }
        }
        #endregion

        /// <summary>
        /// 根据发票号获取北大 HIS 急诊科挂号患者
        /// </summary>
        /// <returns></returns>
        private async Task<HisRegisterPatient> GetHisPatientInfoByVisitNoAsync(string visitNo)
        {
            string sql =
                $"Select PatientID, PatientName, GHXH RegisterNo" +
                $" from v_jhjk_hzlb Where JZHM='" + visitNo + "'";
            var connectionStringKey = _configuration.GetConnectionString("PekingUniversityHIS");
            _log.LogError("根据发票号获取北大HIS急诊科挂号患者" + sql + connectionStringKey);
            var hisPatientInfo = await this._dapperRepository.QueryFirstOrDefaultAsync<HisRegisterPatient>(sql,
                dbKey: "PekingUniversityHIS", connectionStringKey: connectionStringKey);

            return hisPatientInfo;
        }

        /// <summary>
        /// 获取护士列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<List<EmployeeDto>>> GetNurseScheduleAsync()
        {
            return await _commonHisApi.GetNurseScheduleAsync();
        }

        public async Task<JsonResult<List<PatientInfoFromHis>>> GetPatientRecordAsync(string cardType,
            string identityNo, string visitNo, string patientName, string phone, string registerNo)
        {
            return await this._commonHisApi.GetPatientRecordAsync(cardType, identityNo, visitNo, patientName, phone,
                registerNo);
        }

        public async Task<JsonResult<PatientInfoFromHis>> CreatePatientRecordAsync(CreateOrGetPatientIdInput input)
        {
            return await this._commonHisApi.CreatePatientRecordAsync(input);
        }

        public async Task<JsonResult<PatientInfoFromHis>> CreateNoThreePatientRecordAsync(CreateOrGetPatientIdInput input)
        {
            return await this._commonHisApi.CreateNoThreePatientRecordAsync(input);
        }

        public Task<JsonResult> ValidateBeforeCreatePatient(CreateOrGetPatientIdInput input, out bool isInfant)
        {
            isInfant = false;
            if (!input.IsNoThree && string.IsNullOrEmpty(input.ContactsPhone) && string.IsNullOrEmpty(input.Address))
            {
                return Task.FromResult(JsonResult.Fail("电话号码和住址必须填写其中一项"));
            }

            return Task.FromResult(JsonResult.Ok());
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

        public Task<InsuranceDto> GetInsuranceInfoByElectronCert(string electronCertNo, string extraCode)
        {
            throw new NotImplementedException();
        }
    }
}