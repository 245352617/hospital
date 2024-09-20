using DotNetCore.CAP;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.OpenXmlFormats.Wordprocessing;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.PreHospital.TriageService.LGHis;
using SamJan.MicroService.TriageService.Enum;
using SamJan.MicroService.TriageService.MqDto;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TriageService.Application.Dtos.Triage.Patient;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using YJHealth.MedicalInsurance.Services;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 患者分诊信息接口
    /// </summary>
    [Auth("PatientInfo", "分诊患者")]
    [Authorize]
    public class PatientInfoAppService : ApplicationService, IPatientInfoAppService, ICapSubscribe
    {
        /// <summary>
        /// 自费
        /// </summary>
        private const string DEFAULTCHARGETYPE = "Faber_012";
        private readonly ILogger<PatientInfoAppService> _log;
        private readonly IRepository<TriageConfig> _triageConfigRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDatabase _redis;
        private readonly IConfiguration _configuration;
        private readonly IPatientInfoRepository _patientInfoRepository;
        private readonly IChangeRecordRepository _changeRecordRepository;

        private readonly IPatientInformRepository _patientInformRepository;


        private ICsEcisRepository _csEcisRepository;
        private ICsEcisRepository CsEcisRepository => LazyGetRequiredService(ref _csEcisRepository);

        private ITriageConfigAppService _triageConfigService;
        private ITriageConfigAppService TriageConfigService => LazyGetRequiredService(ref _triageConfigService);


        private IGroupInjuryRepository _groupInjuryRepository;
        private IGroupInjuryRepository GroupInjuryRepository => LazyGetRequiredService(ref _groupInjuryRepository);


        private IRegisterInfoRepository _registerInfoRepository;
        private IRegisterInfoRepository RegisterInfoRepository => LazyGetRequiredService(ref _registerInfoRepository);


        private readonly ICapPublisher _capPublisher;
        private readonly IHisApi _hisApi;
        private readonly IPekingUniversityHisApi _pekingUniversityhisApi;
        private readonly IServiceProvider _serviceProvider;
        private SocketHelper _socketHelper;
        private SocketHelper SocketHelper => LazyGetRequiredService(ref _socketHelper);


        private IRabbitMqAppService _rabbitMqAppService;
        private IRabbitMqAppService RabbitMqAppService => LazyGetRequiredService(ref _rabbitMqAppService);


        private IHttpClientHelper _httpClientHelper;
        private IHttpClientHelper HttpClientHelper => LazyGetRequiredService(ref _httpClientHelper);


        private IUnitOfWorkManager _unitOfWork;
        private IUnitOfWorkManager UnitOfWork => LazyGetRequiredService(ref _unitOfWork);


        private IScoreManageAppService _scoreManageAppService;
        private IScoreManageAppService ScoreManageAppService => LazyGetRequiredService(ref _scoreManageAppService);


        private IRepository<ScoreInfo, Guid> _scoreInfoRepository;
        private IRepository<ScoreInfo, Guid> ScoreInfoRepository => LazyGetRequiredService(ref _scoreInfoRepository);


        private ConsulHttpClient _consulHttpClient;
        private ConsulHttpClient ConsulHttpClient => LazyGetRequiredService(ref _consulHttpClient);

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true // Optional: Make the JSON output indented for readability
        };

        public PatientInfoAppService(IPatientInfoRepository patientInfoRepository, ILogger<PatientInfoAppService> log,
            IRepository<TriageConfig> triageConfigRepository, IHttpClientFactory httpClientFactory,
            RedisHelper redisHelper, IConfiguration configuration, ICapPublisher capPublisher,
            IHisApi hisApi, IServiceProvider serviceProvider, IPekingUniversityHisApi pekingUniversityhisApi, IChangeRecordRepository changeRecordRepository, IPatientInformRepository patientInformRepository)
        {
            _patientInfoRepository = patientInfoRepository;
            _patientInformRepository = patientInformRepository;
            _log = log;
            _triageConfigRepository = triageConfigRepository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _capPublisher = capPublisher;
            _hisApi = hisApi;
            _pekingUniversityhisApi = pekingUniversityhisApi;
            _serviceProvider = serviceProvider;
            _redis = redisHelper.GetDatabase();
            _changeRecordRepository = changeRecordRepository;
        }

        /// <summary>
        /// 根据输入项获取患者分诊数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<PatientInfoDto>> GetPatientInfoByInputAsync(PatientInfoInput input)
        {
            try
            {
                var res = await _patientInfoRepository.Include(c => c.ScoreInfo)
                    .Include(c => c.ConsequenceInfo)
                    .Include(c => c.VitalSignInfo)
                    .Include(c => c.AdmissionInfo)
                    .Include(c => c.RegisterInfo)
                    .WhereIf(input.Id != Guid.Empty, x => x.Id == input.Id)
                    .WhereIf(input.TaskInfoId != Guid.Empty, x => x.TaskInfoId == input.TaskInfoId)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.CarNum), x => x.CarNum == input.CarNum)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.IdentityNo), x => x.IdentityNo == input.IdentityNo)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.PatientId), x => x.PatientId == input.PatientId)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.PatientName), x => x.PatientName == input.PatientName)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.ContactsPhone),
                        x => x.ContactsPhone == input.ContactsPhone)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.ToHospitalWayCode),
                        x => x.ToHospitalWayCode == input.ToHospitalWayCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.ActTriageLevel),
                        x => input.ActTriageLevel.Contains(x.ConsequenceInfo.ActTriageLevel))
                    .OrderByDescending(o => o.CreationTime).AsNoTracking()
                    .FirstOrDefaultAsync();


                _log.LogInformation("根据输入项获取患者建档信息，未查询到患者分诊数据，开始查询Redis缓存信息");
                if (res == null)
                {
                    if (!input.IdentityNo.IsNullOrWhiteSpace() && await _redis.KeyExistsAsync(
                            $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.IdentityNo}"))
                    {
                        var json = await _redis.StringGetAsync(
                            $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.IdentityNo}");
                        var output = JsonSerializer.Deserialize<PatientInfoFromHis>(json);
                        res = output.BuildAdapter().AdaptToType<PatientInfo>();
                        _log.LogInformation("根据输入项获取患者建档信息，根据患者身份证号从Redis查询到缓存信息");
                    }
                    else if (!input.PatientId.IsNullOrWhiteSpace() && await _redis.KeyExistsAsync(
                                 $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.PatientId}"))
                    {
                        var json = await _redis.StringGetAsync(
                            $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.PatientId}");
                        var output = JsonSerializer.Deserialize<PatientInfoFromHis>(json);
                        res = output.BuildAdapter().AdaptToType<PatientInfo>();
                        _log.LogInformation("根据输入项获取患者建档信息，根据患者PatientId从Redis查询到缓存信息");
                    }
                }
                else
                {
                    var lastres = await _patientInfoRepository
                         .Include(c => c.VitalSignInfo)
                         .Include(c => c.AdmissionInfo)
                         .Where(p => p.TriageStatus == 1)
                         .Where(p => p.PatientId == res.PatientId).OrderByDescending(p => p.TriageTime)
                         .FirstOrDefaultAsync();
                    if (lastres != null)
                    {
                        //既往史
                        if (lastres?.AdmissionInfo != null && string.IsNullOrWhiteSpace(res.AdmissionInfo?.PastMedicalHistory))
                        {
                            if (res.AdmissionInfo == null)
                                res.AdmissionInfo = new AdmissionInfo();
                            res.AdmissionInfo.PastMedicalHistory = lastres?.AdmissionInfo?.PastMedicalHistory;
                        }
                        //再次分诊需取到当天6小时内的血压值记录
                        if (lastres?.TriageTime != null)
                        {
                            var ts = DateTime.Now - lastres?.TriageTime;
                            if (ts.HasValue && ts.Value.TotalHours <= 6)
                            {
                                if (lastres?.VitalSignInfo != null && string.IsNullOrWhiteSpace(res.VitalSignInfo?.Sbp) && string.IsNullOrWhiteSpace(res.VitalSignInfo?.Sdp))
                                {
                                    if (res.VitalSignInfo == null)
                                        res.VitalSignInfo = new VitalSignInfo();
                                    res.VitalSignInfo.Sbp = lastres?.VitalSignInfo?.Sbp;
                                    res.VitalSignInfo.Sdp = lastres?.VitalSignInfo?.Sdp;
                                }
                            }
                        }

                    }
                }

                var dto = res.BuildAdapter().AdaptToType<PatientInfoDto>();
                return JsonResult<PatientInfoDto>.Ok(msg: dto == null ? "未查询到该患者数据" : "操作成功", data: dto);
            }
            catch (Exception e)
            {
                _log.LogError("根据输入项获取患者建档信息错误！原因：{Msg}", e);
                return JsonResult<PatientInfoDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据患者id获取患者分诊数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<PatientInfoDto>> GetPatientInfoByIdAsync(PatientInfoInput input)
        {
            try
            {
                var res = await _patientInfoRepository.Include(c => c.ScoreInfo)
                    .Include(c => c.ConsequenceInfo)
                    .Include(c => c.VitalSignInfo)
                    .Include(c => c.AdmissionInfo)
                    .Include(c => c.RegisterInfo)
                    .WhereIf(input.Id != Guid.Empty, x => x.Id == input.Id)
                    .OrderByDescending(o => o.CreationTime)
                    .FirstOrDefaultAsync();

                _log.LogInformation("根据输入项获取患者建档信息，未查询到患者分诊数据，开始查询Redis缓存信息");
                if (res == null)
                {
                    if (!input.IdentityNo.IsNullOrWhiteSpace() && await _redis.KeyExistsAsync(
                            $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.IdentityNo}"))
                    {
                        var json = await _redis.StringGetAsync(
                            $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.IdentityNo}");
                        var output = JsonSerializer.Deserialize<PatientInfoFromHis>(json);
                        res = output.BuildAdapter().AdaptToType<PatientInfo>();
                        _log.LogInformation("根据输入项获取患者建档信息，根据患者身份证号从Redis查询到缓存信息");
                    }
                    else if (!input.PatientId.IsNullOrWhiteSpace() && await _redis.KeyExistsAsync(
                                 $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.PatientId}"))
                    {
                        var json = await _redis.StringGetAsync(
                            $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.PatientId}");
                        var output = JsonSerializer.Deserialize<PatientInfoFromHis>(json);
                        res = output.BuildAdapter().AdaptToType<PatientInfo>();
                        _log.LogInformation("根据输入项获取患者建档信息，根据患者PatientId从Redis查询到缓存信息");
                    }
                }

                var dto = res.BuildAdapter().AdaptToType<PatientInfoDto>();
                return JsonResult<PatientInfoDto>.Ok(msg: dto == null ? "未查询到该患者数据" : "操作成功", data: dto);
            }
            catch (Exception e)
            {
                _log.LogError("根据输入项获取患者建档信息错误！原因：{Msg}", e);
                return JsonResult<PatientInfoDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 修改绿色通道
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + "UpdatePatientGreenRoad", "开启绿通")]
        public async Task<JsonResult> UpdatePatientGreenRoadAsync(UpdatePatientGreenRoadDto dto)
        {
            try
            {
                var patient = await _patientInfoRepository.Include(c => c.ConsequenceInfo)
                    .Include(c => c.AdmissionInfo)
                    .Include(c => c.ScoreInfo)
                    .Include(c => c.VitalSignInfo)
                    .Include(c => c.RegisterInfo)
                    .FirstOrDefaultAsync(x => x.Id == dto.TriagePatientInfoId);
                if (patient == null)
                {
                    _log.LogError("修改绿色通道失败，原因：{Msg} PatientInfoId：{Id}", "不存在该患者", dto.TriagePatientInfoId);
                    return JsonResult.Fail("不存在该患者");
                }

                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
                patient.ModUser = CurrentUser.UserName;
                patient.GreenRoadCode = dto.GreenRoadCode;
                patient.GreenRoadName = dicts.GetNameByDictCode(TriageDict.GreenRoad, dto.GreenRoadCode);
                await _patientInfoRepository.UpdateAsync(patient);

                var registerInfo = patient.RegisterInfo.OrderByDescending(o => o.RegisterTime).FirstOrDefault();

                var mqDto = new PatientInfoMqDto
                {
                    PatientInfo = patient.BuildAdapter().AdaptToType<PatientInfoDto>(),
                    ConsequenceInfo = patient.ConsequenceInfo?.BuildAdapter().AdaptToType<ConsequenceInfoDto>(),
                    VitalSignInfo = patient.VitalSignInfo?.BuildAdapter().AdaptToType<VitalSignInfoDto>(),
                    ScoreInfo = patient.ScoreInfo?.BuildAdapter().AdaptToType<List<ScoreInfoDto>>(),
                    RegisterInfo = registerInfo?.BuildAdapter().AdaptToType<RegisterInfoDto>(),
                    AdmissionInfo = patient.AdmissionInfo?.BuildAdapter().AdaptToType<AdmissionInfoDto>()
                };

                if (mqDto.ConsequenceInfo != null)
                {
                    mqDto.ConsequenceInfo.HisDeptCode = dicts[TriageDict.TriageDepartment.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == mqDto.ConsequenceInfo.TriageDept)
                        ?.HisConfigCode;
                }

                #region 推送院前分诊患者信息到急诊分诊，目前北大 Settings:ECISSettings:IsEnabled 值为 false，不走该逻辑

                // 是否启用发送数据到急诊分诊
                if (Convert.ToBoolean(_configuration["Settings:ECISSettings:IsEnabled"]))
                {
                    switch (Convert.ToInt32(_configuration["Settings:ECISSettings:Type"]))
                    {
                        case 0:
                            var capHeader = new Dictionary<string, string>
                            {
                                ["AppName"] = _configuration["ApplicationName"]
                            };

                            await _capPublisher.PublishAsync("patient.from.preHospital.triage", mqDto, capHeader);
                            break;

                        case 1:
                            _log.LogInformation("开始调用接口同步病患到急诊分诊");
                            var url = _configuration["Settings:ECISSettings:ApiUrl"];
                            var content = new StringContent(JsonSerializer.Serialize(mqDto));
                            var result = await _httpClientHelper.PostAsync(url, content);
                            _log.LogInformation("开始调用接口同步病患到急诊分诊结束，返回结果：{Result}", result);
                            break;
                    }
                }

                #endregion

                #region 南方医群伤开通绿通挂账

                var greenChannelRegisterInfo = await OpenGreenChannelChargeAsync(patient, dicts, "", dto.GreenRoadCode);

                #endregion

                // 急诊分诊发送队列消息到叫号、诊疗微服务
                // 推送MQ消息到医生站
                await RabbitMqAppService.PublishEcisPatientSyncPatientAsync(new List<PatientInfoMqDto> { mqDto });

                #region 同步病患到CS版急诊分诊

                await SyncPatientToCsEcisAsync(patient, greenChannelRegisterInfo, dicts, true);

                #endregion

                #region 推送患者到六大中心

                var eto = patient.BuildAdapter().AdaptToType<SyncPatientEventBusEto>();
                var etoList = new List<SyncPatientEventBusEto> { eto };
                if (patient.RegisterInfo != null && patient.RegisterInfo.Count > 0)
                {
                    if (registerInfo != null)
                    {
                        eto.RegisterNo = registerInfo.RegisterNo;
                        eto.VisitNo = registerInfo.VisitNo;
                    }
                }

                await RabbitMqAppService.PublishSixCenterSyncPatientAsync(etoList, dicts);

                #endregion

                _log.LogInformation("修改绿色通道成功");
                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.LogError("修改绿色通道错误！原因：{Msg}", e);
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 修改发病时间
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + "ModifyOnsetTime", "修改发病时间")]
        public async Task<JsonResult> ModifyOnsetTimeAsync(ModifyOnsetTimeDto dto)
        {
            try
            {
                _log.LogInformation("修改发病时间开始");
                var patient = await _patientInfoRepository.FirstOrDefaultAsync(x => x.Id == dto.TriagePatientInfoId);
                if (patient == null)
                {
                    _log.LogError("修改发病时间失败，原因：不存在此患者");
                    return JsonResult.Fail("该患者可能被其他人删除");
                }

                patient.OnsetTime = dto.OnsetTime;
                await _patientInfoRepository.UpdateAsync(patient);

                #region 推送队列消息到六大中心同步病患

                // 是否启用发送数据到六大中心
                if (Convert.ToBoolean(_configuration["Settings:IsEnabledSixCenterMQ"]))
                {
                    var eto = patient.BuildAdapter().AdaptToType<SyncPatientEventBusEto>();
                    var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
                    await RabbitMqAppService.PublishSixCenterSyncPatientAsync(new List<SyncPatientEventBusEto> { eto },
                        dicts);
                }

                #endregion

                _log.LogInformation("修改发病时间成功");
                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.LogError("修改发病时间错误，原因：{Msg}", e);
                return JsonResult.Fail(e.Message);
            }
        }


        /// <summary>
        /// 修改绿色通道
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + "OpenGreenRoad", "开启绿通")]
        public async Task<JsonResult> OpenGreenRoadAsync(UpdatePatientGreenRoadDto dto)
        {
            return await UpdatePatientGreenRoadAsync(dto);
        }


        /// <summary>
        /// 根据电子医保凭证项获取患者病历号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult<PatientInfoByElectronCertDto>> GetPatientRecordByElectronCertAsync(
            PatientInfoByElectronCertInput input)
        {
            try
            {
                // 医保区划
                var insuplcAdmdv = await this._triageConfigRepository.AsNoTracking()
                    .Where(x => x.TriageConfigType == (int)TriageDict.InsuplcAdmdv)
                    // 传递了医保区划编码，则查询医保区划编码
                    .WhereIf(!string.IsNullOrWhiteSpace(input.InsuplcAdmdvCode), x => x.TriageConfigCode == input.InsuplcAdmdvCode)
                    // 如果没有传递医保区划编码，则查询默认的“深圳市”
                    .WhereIf(string.IsNullOrWhiteSpace(input.InsuplcAdmdvCode), x => x.ExtraCode == "440300")
                    .FirstOrDefaultAsync();

                //获取医保电子凭证信息
                var res = await this._hisApi.GetInsuranceInfoByElectronCert(input.ElectronCertNo, insuplcAdmdv.ExtraCode);
                _log.LogInformation($"电子医保凭证获取信息,凭证号：{input.ElectronCertNo},参数：{JsonConvert.SerializeObject(res)}");
                if (res == null || string.IsNullOrEmpty(res.certno))
                {
                    _log.LogError("电子医保凭证获取信息失败！");
                    return JsonResult<PatientInfoByElectronCertDto>.Fail("电子医保凭证获取信息失败");
                }

                var item = new PatientInfoByElectronCertDto();
                item.IdentityNo = res.certno;
                item.PatientName = res.psn_name;
                item.Age = res.age + "岁";
                item.ElectronCertNo = input.ElectronCertNo;
                item.ExtraCode = insuplcAdmdv.ExtraCode;
                if (!string.IsNullOrEmpty(item.IdentityNo) && item.IdentityNo.Length == 18)
                {
                    var sexFlag = Convert.ToInt32(item.IdentityNo.Substring(16, 1));
                    item.Sex = Convert.ToBoolean(sexFlag & 1)
                        ? Gender.Male.GetDescriptionByEnum()
                        : Gender.Female.GetDescriptionByEnum();

                    item.Birthday = DateTime.ParseExact(item.IdentityNo.Substring(6, 8), "yyyyMMdd",
                         CultureInfo.CurrentCulture);
                }

                return JsonResult<PatientInfoByElectronCertDto>.Ok(data: item);
            }
            catch (Exception e)
            {
                _log.LogError("根据电子医保凭证获取患者病历号失败！原因：{Msg}", e);
                return JsonResult<PatientInfoByElectronCertDto>.Fail(e.Message);
            }
        }


        /// <summary>
        /// 根据电子医保凭证项获取患者病历号--该方法查询医保后又查了His，一起返回数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<JsonResult<List<PatientInfoFromHis>>> GetPatientRecordByElectronCertBakAsync(
            PatientInfoByElectronCertInput input)
        {
            try
            {
                // 医保区划
                var insuplcAdmdv = await this._triageConfigRepository.AsNoTracking()
                    .Where(x => x.TriageConfigType == (int)TriageDict.InsuplcAdmdv)
                    // 传递了医保区划编码，则查询医保区划编码
                    .WhereIf(!string.IsNullOrWhiteSpace(input.InsuplcAdmdvCode), x => x.TriageConfigCode == input.InsuplcAdmdvCode)
                    // 如果没有传递医保区划编码，则查询默认的“深圳市”
                    .WhereIf(string.IsNullOrWhiteSpace(input.InsuplcAdmdvCode), x => x.ExtraCode == "440300")
                    .FirstOrDefaultAsync();

                //获取医保电子凭证信息
                var res = await this._hisApi.GetInsuranceInfoByElectronCert(input.ElectronCertNo, insuplcAdmdv.ExtraCode);
                _log.LogInformation($"电子医保凭证获取信息,凭证号：{input.ElectronCertNo},参数：{JsonConvert.SerializeObject(res)}");
                if (res == null || string.IsNullOrEmpty(res.certno))
                {
                    _log.LogError("电子医保凭证获取信息失败！");
                    return JsonResult<List<PatientInfoFromHis>>.Fail("电子医保凭证获取信息失败");
                }

                var idType = "2"; // 默认类型身份证
                // 查询患者信息，返回多条记录
                var response = await _hisApi.GetPatientRecordAsync(idType, res.certno, "", res.psn_name);
                if (response.Code != 200)
                {
                    _log.LogError("根据身份证获取患者病历号失败！GetPatientRecordAsync 原因：{Msg}", response.Msg);
                }

                var patientList = response.Data;

                // 默认来院方式
                var defaultToHospitalWay = await this._triageConfigRepository.AsQueryable().AsNoTracking()
                    .Where(x => x.TriageConfigType == (int)TriageDict.ToHospitalWay && x.IsDisable == 1)
                    .OrderBy(x => x.Sort)
                    .FirstOrDefaultAsync();
                if (patientList != null && patientList.Any())
                {
                    foreach (var item in patientList)
                    {
                        item.ToHospitalWay ??= defaultToHospitalWay?.TriageConfigCode;
                        item.StartTriageTime = DateTime.Now;
                        item.IdentityNo = res.certno;
                        item.PatientName = res.psn_name;
                        item.SetGenderAndBirthday(item.IdentityNo);

                        // 从HIS查档无法查到患者社保卡号，社保卡号从读卡器程序读取
                        item.ElectronCertNo = input.ElectronCertNo;
                    }
                }
                else
                {
                    var item = new PatientInfoFromHis();
                    item.ToHospitalWay ??= defaultToHospitalWay?.TriageConfigCode;
                    item.IdentityNo = res.certno;
                    item.PatientName = res.psn_name;
                    //  "naty":"01",
                    item.SetGenderAndBirthday(item.IdentityNo);
                    item.ElectronCertNo = input.ElectronCertNo;
                    patientList = new List<PatientInfoFromHis>
                    {
                        item
                    };
                }
                return JsonResult<List<PatientInfoFromHis>>.Ok(data: patientList);

            }
            catch (Exception e)
            {
                _log.LogError("根据电子医保凭证获取患者病历号失败！原因：{Msg}", e);
                return JsonResult<List<PatientInfoFromHis>>.Fail(e.Message);
            }
        }


        /// <summary>
        /// 根据输入项获取患者病历号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + "GetPatientId")]
        public async Task<JsonResult<List<PatientInfoFromHis>>> GetPatientRecordByHl7MsgAsync(
            CreateOrGetPatientIdInput input)
        {
            try
            {
                _log.LogInformation("根据输入项获取患者病历号开始");
                if (!string.IsNullOrWhiteSpace(input.IdentityNo) &&
                    await _redis.KeyExistsAsync(
                        $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.IdentityNo}"))
                {
                    var json = await _redis.StringGetAsync(
                        $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.IdentityNo}");
                    var dto = JsonSerializer.Deserialize<PatientInfoFromHis>(json);
                    dto.IsAdd = 1;
                    // 兼容旧数据，社保卡号从读卡器程序读取
                    if (string.IsNullOrWhiteSpace(dto.MedicalNo) && !string.IsNullOrWhiteSpace(input.MedicalNo))
                    {
                        dto.MedicalNo = input.MedicalNo;
                        await _redis.StringSetAsync(
                            $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.IdentityNo}",
                            JsonSerializer.Serialize(dto));
                    }
                    _log.LogInformation("根据输入项获取患者病历号成功，从缓存获取，Key: {RedisKey}",
                        $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.IdentityNo}");
                    var admissionInfo = await GetAdmissionInfoByInputAsync(input);
                    if (admissionInfo != null)
                    {
                        dto.PastMedicalHistory = admissionInfo.PastMedicalHistory;
                        dto.AllergyHistory = admissionInfo.AllergyHistory;
                    }
                    return JsonResult<List<PatientInfoFromHis>>.Ok(data: new List<PatientInfoFromHis> { dto });
                }

                if (!string.IsNullOrEmpty(input.CardNo) &&
                    await _redis.KeyExistsAsync($"{_configuration["ServiceName"]}:PatientNoThree:{input.CardNo}"))
                {
                    var json = await _redis.StringGetAsync(
                        $"{_configuration["ServiceName"]}:PatientNoThree:{input.CardNo}");
                    var dto = JsonSerializer.Deserialize<PatientInfoFromHis>(json);
                    _log.LogInformation("根据输入项获取患者病历号成功，从缓存获取，Key: {RedisKey}",
                        $"{_configuration["ServiceName"]}:PatientNoThree:{input.CardNo}");
                    var admissionInfo = await GetAdmissionInfoByInputAsync(input);
                    if (admissionInfo != null)
                    {
                        dto.PastMedicalHistory = admissionInfo.PastMedicalHistory;
                        dto.AllergyHistory = admissionInfo.AllergyHistory;
                    }
                    return JsonResult<List<PatientInfoFromHis>>.Ok(data: new List<PatientInfoFromHis> { dto });
                }

                input.CheckNationIsFull();
                List<PatientInfoFromHis> res;
                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync(TriageDict.Nation.ToString());
                input.Nation = dicts.GetCodeByDictName(TriageDict.Nation, input.Nation);
                // 是否启用测试生成病历号程序
                if (Convert.ToBoolean(_configuration["Settings:IsEnabledCreatePatientIdExe"]))
                {
                    var jr = await GetDemoHl7Response(input);
                    if (jr.Code != 200)
                    {
                        _log.LogError("查询患者档案号失败，原因：{Msg}", jr.Msg);
                        jr.Code = 600;
                        return jr;
                    }

                    res = jr.Data;
                }
                else
                {
                    var patientOutputs = await GetPatientRecordByIdentityNo(input);
                    if (patientOutputs.Code != 200)
                    {
                        _log.LogError("查询患者档案号失败，原因：{Msg}", patientOutputs.Msg);
                        var jr = new JsonResult<List<PatientInfoFromHis>>
                        {
                            Code = 600,
                            Msg = patientOutputs.Msg
                        };

                        return jr;
                    }
                    res = await GetPatientInfoByHisInfoAsync(patientOutputs.Data);
                }

                if (res != null && res.Count > 0)
                {
                    foreach (var patient in res)
                    {
                        patient.GetAge();
                        // 建档患者缓存，当在 HIS 建档后，预检分诊并没有存储患者信息。
                        await _redis.StringSetAsync(
                            $"{_configuration["ServiceName"]}:PatientBuilt:{patient.TaskInfoId}:{patient.PatientId}",
                            JsonSerializer.Serialize(patient));
                        await _redis.StringSetAsync(
                            $"{_configuration["ServiceName"]}:PatientBuilt:{patient.TaskInfoId}:{patient.IdentityNo}",
                            JsonSerializer.Serialize(patient));
                    }

                    string identityNo = res.First().IdentityNo;
                    string insuplcAdmdvCode = res.First().InsuplcAdmdvCode;
                    if (!string.IsNullOrEmpty(identityNo))
                    {
                        bool isCheckIdentityNo = _configuration.GetSection("Settings:IsCheckIdentityNo").Get<bool>();
                        if (isCheckIdentityNo)
                        {
                            try
                            {
                                var idcard = IDCard.IDCard.Verify(identityNo);
                                if (idcard.IsVerifyPass)
                                {
                                    PreGetInsuranceInfos(identityNo, insuplcAdmdvCode);
                                }
                            }
                            catch (Exception ex)
                            {
                                _log.LogError("根据输入项获取患者病历号错误！原因：{Msg}", ex.Message);
                                return JsonResult<List<PatientInfoFromHis>>.Fail("患者证件号码异常，请到HIS中检查患者档案");
                            }
                        }
                        else
                        {
                            PreGetInsuranceInfos(identityNo, insuplcAdmdvCode);
                        }
                    }
                }

                // websocket 推送通知前端调用查询患者信息接口
                await _capPublisher.PublishAsync("executing.task.from.emrservice", "");
                _log.LogInformation("查询患者档案号，websocket 推送通知前端调用查询患者信息接口完成");

                return JsonResult<List<PatientInfoFromHis>>.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogError("根据输入项获取患者病历号错误！原因：{Msg}", e);
                return JsonResult<List<PatientInfoFromHis>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 查询医保信息放缓存
        /// </summary>
        /// <param name="identityNo"></param>
        /// <param name="insuplcAdmdvCode"></param>
        private async void PreGetInsuranceInfos(string identityNo, string insuplcAdmdvCode)
        {
            if (string.IsNullOrEmpty(identityNo)) return;
            TriageConfig insuplcAdmdv = null;
            if (!string.IsNullOrEmpty(insuplcAdmdvCode))
            {
                insuplcAdmdv = await _triageConfigRepository.AsNoTracking()
                    .FirstOrDefaultAsync(x =>
                        x.TriageConfigType == (int)TriageDict.InsuplcAdmdv &&
                        x.TriageConfigCode == insuplcAdmdvCode);
            }

            await GetInsuranceInfos(identityNo, insuplcAdmdv?.ExtraCode ?? "440300", insuplcAdmdv?.TriageConfigCode ?? "InsuplcAdmdv_217"); // 医保参保地，默认深圳市
        }

        /// <summary>
        /// 根据his信息获取病人既往史
        /// </summary>
        /// <param name="patientInfoFromHis"></param>
        /// <returns></returns>
        private async Task<List<PatientInfoFromHis>> GetPatientInfoByHisInfoAsync(List<PatientInfoFromHis> patientInfoFromHis)
        {
            foreach (var item in patientInfoFromHis)
            {
                if (!string.IsNullOrWhiteSpace(item.PatientId))
                {
                    var res = await _patientInfoRepository
                    .Include(c => c.AdmissionInfo)
                    .Where(x => x.PatientId == item.PatientId)
                    .OrderByDescending(o => o.CreationTime).Select(p => new AdmissionInfo()
                    {
                        PastMedicalHistory = p.AdmissionInfo.PastMedicalHistory,
                        AllergyHistory = p.AdmissionInfo.AllergyHistory
                    })
                    .FirstOrDefaultAsync();
                    if (res != null)
                    {
                        item.PastMedicalHistory = res.PastMedicalHistory;
                        item.AllergyHistory = res.AllergyHistory;
                    }
                }
            }
            return patientInfoFromHis;
        }

        /// <summary>
        /// 根据病人基础信息获取入院情况信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<AdmissionInfo> GetAdmissionInfoByInputAsync(CreateOrGetPatientIdInput input)
        {
            return await _patientInfoRepository
                .Include(c => c.AdmissionInfo)
                .WhereIf(!string.IsNullOrWhiteSpace(input.VisitNo), x => x.VisitNo == input.VisitNo)
                .WhereIf(!string.IsNullOrWhiteSpace(input.IdentityNo), x => x.IdentityNo == input.IdentityNo)
                .WhereIf(!string.IsNullOrWhiteSpace(input.PatientName), x => x.PatientName == input.PatientName)
                .WhereIf(!string.IsNullOrWhiteSpace(input.ContactsPhone), x => x.ContactsPhone == input.ContactsPhone)
                .OrderByDescending(o => o.CreationTime).Select(p => new AdmissionInfo()
                {
                    PastMedicalHistory = p.AdmissionInfo.PastMedicalHistory,
                    AllergyHistory = p.AdmissionInfo.AllergyHistory
                })
                .FirstOrDefaultAsync();
        }

        private async Task<List<AdmissionInfo>> GetAdmissionInfoByIdentityNoAsync(string IdentityNo)
        {
            return await _patientInfoRepository
                .Include(c => c.AdmissionInfo)
                .Where(x => x.IdentityNo == IdentityNo)
                .OrderByDescending(o => o.CreationTime).Select(p => new AdmissionInfo()
                {
                    PastMedicalHistory = p.AdmissionInfo.PastMedicalHistory,
                    AllergyHistory = p.AdmissionInfo.AllergyHistory
                })
                .ToListAsync();
        }


        /// <summary>
        /// 获取收费类型
        /// </summary>
        /// <param name="idTypeCode">证件类型代码</param>
        /// <param name="identityNo">证件号码</param>
        /// <param name="insuplcAdmdvCode">参保地代码（预检分诊）</param>
        /// <returns></returns>
        [Auth("TriageConfig" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<InsuranceFaberDto>> GetFaberListAsync(string idTypeCode, string identityNo,
            string insuplcAdmdvCode)
        {
            try
            {
                // 获取费别
                var list = await _triageConfigRepository.AsNoTracking()
                    .Where(x => x.TriageConfigType == (int)TriageDict.Faber)
                    .Where(x => x.IsDisable == 1)
                    .OrderBy(o => o.Sort).IgnoreQueryFilters().ToListAsync();
                var fabers = list.BuildAdapter().AdaptToType<List<TriageConfigDto>>();
                // 获取医保信息
                InsuranceDto insuranceDto = null;
                string defaultInsuplcAdmdvCode = string.Empty;
                string patentName = string.Empty;
                var filterFabers = fabers;

                var insuranceFaber = new InsuranceFaberDto
                {
                    InsuplcAdmdvCode = defaultInsuplcAdmdvCode,
                    Fabers = filterFabers,
                    DefaultChargeType = DEFAULTCHARGETYPE
                };

                if (string.IsNullOrEmpty(identityNo))
                {
                    return JsonResult<InsuranceFaberDto>.Ok(msg: "获取费别成功", data: insuranceFaber);
                }

                bool isCheckIdentityNo = _configuration.GetSection("Settings:IsCheckIdentityNo").Get<bool>();
                if (isCheckIdentityNo)
                {
                    var idcard = IDCard.IDCard.Verify(identityNo);
                    if (!idcard.IsVerifyPass)
                    {
                        return JsonResult<InsuranceFaberDto>.Ok(msg: "获取费别成功", data: insuranceFaber);
                    }
                }

                if (!string.IsNullOrEmpty(identityNo))
                {
                    TriageConfig insuplcAdmdv = null;
                    if (!string.IsNullOrEmpty(insuplcAdmdvCode))
                    {
                        insuplcAdmdv = await _triageConfigRepository.AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                x.TriageConfigType == (int)TriageDict.InsuplcAdmdv &&
                                x.TriageConfigCode == insuplcAdmdvCode);
                    }

                    _log.LogInformation("医保参保地：{InsuplcAdmdvCode}", insuplcAdmdv?.ExtraCode ?? "440300"); // 医保参保地，默认深圳市
                    var insuranceResponse =
                        await this.GetInsuranceInfos(identityNo, insuplcAdmdv?.ExtraCode ?? "440300", insuplcAdmdv?.TriageConfigCode ?? "InsuplcAdmdv_217"); // 医保参保地，默认深圳市
                    if (insuranceResponse.Code != 200)
                    {
                        var defaultResult = new InsuranceFaberDto
                        {
                            PatientName = insuranceResponse.Data?.psn_name,
                            InsuplcAdmdvCode = defaultInsuplcAdmdvCode,
                            Fabers = fabers.Where(x => string.IsNullOrEmpty(x.ExtraCode)).ToList(),
                            DefaultChargeType = DEFAULTCHARGETYPE //自费
                        };
                        return JsonResult<InsuranceFaberDto>.Ok(msg: "获取费别成功", data: defaultResult);
                    }

                    insuranceDto = insuranceResponse.Data;
                    _log.LogInformation("医保缴费类型：{Json}", JsonConvert.SerializeObject(insuranceDto));
                    // 默认医保参保地
                    if (!string.IsNullOrEmpty(insuranceDto?.insuplc_admdvs))
                    {
                        var defaultInsuplcAdmdv = await this._triageConfigRepository.AsNoTracking()
                            .Where(x => x.TriageConfigType == (int)TriageDict.InsuplcAdmdv)
                            .FirstOrDefaultAsync(x => x.ExtraCode == insuranceDto.insuplc_admdvs);
                        if (defaultInsuplcAdmdv == null)
                        {
                            // 参保地过于详细的情况下，比如深圳市南山区，去掉南山区（后2位）只查深圳市
                            defaultInsuplcAdmdv = await this._triageConfigRepository.AsNoTracking()
                                .Where(x => x.TriageConfigType == (int)TriageDict.InsuplcAdmdv)
                                .FirstOrDefaultAsync(x =>
                                    x.ExtraCode == (insuranceDto.insuplc_admdvs.SubstringByLength(4) + "00"));
                        }

                        defaultInsuplcAdmdvCode = defaultInsuplcAdmdv?.TriageConfigCode;
                        patentName = insuranceDto.psn_name;
                    }

                    // 返回非医保费别项，或者患者可用的医保费别项
                    filterFabers = fabers.Where(x =>
                            string.IsNullOrEmpty(x.ExtraCode) || x.ExtraCode == "SpecialAccount" ||
                            insuranceDto.InsuranceTypes.Any(y => y.clctstd_crtf_rule_codg == x.ExtraCode))
                        .ToList();
                }

                var result = new InsuranceFaberDto
                {
                    PatientName = patentName,
                    InsuplcAdmdvCode = defaultInsuplcAdmdvCode,
                    Fabers = filterFabers,
                    DefaultChargeType = filterFabers.FirstOrDefault(x => !string.IsNullOrEmpty(x.ExtraCode) && x.ExtraCode != "SpecialAccount")  // 特约记账不是医保费别，不做默认选择
                        ?.TriageConfigCode,
                };

                return JsonResult<InsuranceFaberDto>.Ok(msg: "获取费别成功", data: result);
            }
            catch (Exception e)
            {
                return JsonResult<InsuranceFaberDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 查询医保信息（省医保）
        /// </summary>
        /// <param name="identityNo">身份证号码</param>
        /// <param name="insuplcAdmdvs">参保地医保区划</param>
        /// <param name="insuplcAdmdvCode"></param>
        /// <returns></returns>
        private async Task<JsonResult<InsuranceDto>> GetInsuranceInfos([Required] string identityNo,
            string insuplcAdmdvs, string insuplcAdmdvCode)
        {
            if (Convert.ToBoolean(_configuration["Demo:IsMockInsurance"]))
            {
                return new JsonResult<InsuranceDto>
                {
                    Data = new InsuranceDto
                    {
                        psn_no = "44030000000001368661",
                        insuplc_admdvs = "440300", // 深圳市
                        InsuranceTypes = new[]
                        {
                            new InsuranceType { clctstd_crtf_rule_codg = "A31001", insutype = "310" },
                            new InsuranceType { clctstd_crtf_rule_codg = "A51001", insutype = "510" }
                        }.ToList(),
                    }
                };
            }
            else
            {
                try
                {
                    _log.LogInformation("开始获取医保信息");
                    var cacheString = (await _redis.StringGetAsync(new RedisKey($"{_configuration["ServiceName"]}:Insurance:{identityNo}:{insuplcAdmdvCode}"))).ToString();
                    // 从缓存中读取医保信息
                    if (!string.IsNullOrEmpty(cacheString))
                    {
                        var insuranceDto = JsonSerializer.Deserialize<InsuranceDto>(cacheString);
                        _log.LogInformation("从缓存读取医保信息：{Insurance}", insuranceDto);

                        return new JsonResult<InsuranceDto>
                        {
                            Code = (int)HttpStatusCode.OK,
                            Data = insuranceDto
                        };
                    }

                    // 调用省医保接口获取人员信息、医保档次
                    var basicInfo = await Insurance.GetBasicInfo("02", identityNo, insuplcAdmdvs);
                    _log.LogInformation("人员信息：{BasicInfo}", JsonSerializer.Serialize(basicInfo));
                    if (!string.IsNullOrWhiteSpace(basicInfo.err_msg))
                    {
                        return JsonResult<InsuranceDto>.Fail(basicInfo.err_msg);
                    }

                    if (string.IsNullOrWhiteSpace(basicInfo.err_msg))
                    {
                        if (basicInfo.output.insuinfo.Any())
                        {
                            insuplcAdmdvs = basicInfo.output.insuinfo[0].insuplc_admdvs;
                        }

                        List<InsuranceType> insuranceTypes = new List<InsuranceType>();

                        string currentInsuplcAdmdvs = "440300"; // 深圳市
                                                                // 医保区划号省级前缀
                        string provinceInsuplcAdmdvs = currentInsuplcAdmdvs[..2];
                        // 医保区划号市级前缀
                        string cityInsuplcAdmdvs = currentInsuplcAdmdvs[..4];
                        // 判断是否跨省异地医保
                        if (!insuplcAdmdvs.StartsWith(provinceInsuplcAdmdvs))
                        {
                            foreach (var insurance in basicInfo.output.insuinfo)
                            {
                                // 龙岗中心HIS要求，使用9008作为跨省异地医保标识
                                insuranceTypes.Add(new InsuranceType
                                { clctstd_crtf_rule_codg = "9008", insutype = insurance.insutype });
                            }
                        }
                        // 判断是否省内异地医保
                        else if (!insuplcAdmdvs.StartsWith(cityInsuplcAdmdvs))
                        {
                            foreach (var insurance in basicInfo.output.insuinfo)
                            {
                                // 龙岗中心HIS要求，使用9000作为省内异地医保标识
                                insuranceTypes.Add(new InsuranceType
                                { clctstd_crtf_rule_codg = "9000", insutype = insurance.insutype });
                            }
                        }
                        // 查询医保缴费信息
                        else
                        {
                            var payQuery =
                                await Insurance.GetPayQueryInfo(basicInfo.output.baseinfo.psn_no, insuplcAdmdvs);
                            _log.LogInformation("医保信息：{BasicInfo}", JsonSerializer.Serialize(payQuery.output));
                            if (!string.IsNullOrWhiteSpace(payQuery.err_msg))
                            {
                                return JsonResult<InsuranceDto>.Fail(payQuery.err_msg);
                            }

                            if (payQuery.output != null)
                            {
                                foreach (var insurance in payQuery.output)
                                {
                                    if (!insuranceTypes.Exists(x =>
                                            x.clctstd_crtf_rule_codg == insurance.clctstd_crtf_rule_codg &&
                                            x.insutype == insurance.insutype))
                                    {
                                        insuranceTypes.Add(new InsuranceType
                                        {
                                            clctstd_crtf_rule_codg = insurance.clctstd_crtf_rule_codg,
                                            insutype = insurance.insutype
                                        });
                                    }
                                }
                            }
                        }

                        InsuranceDto data = new InsuranceDto
                        {
                            psn_no = basicInfo.output.baseinfo.psn_no,
                            psn_name = basicInfo.output.baseinfo.psn_name,
                            insuplc_admdvs = insuplcAdmdvs,
                            InsuranceTypes = insuranceTypes,
                        };

                        if (!string.IsNullOrEmpty(identityNo))
                        {
                            // 缓存数据
                            await _redis.StringSetAsync(
                                $"{_configuration["ServiceName"]}:Insurance:{identityNo}:{insuplcAdmdvCode}",
                                JsonSerializer.Serialize(data), expiry: TimeSpan.FromDays(10));
                        }

                        return new JsonResult<InsuranceDto>
                        {
                            Data = data
                        };
                    }
                    else
                    {
                        return JsonResult<InsuranceDto>.Fail("未查到医保信息");
                    }
                }
                catch (Exception exception)
                {
                    _log.LogError(exception, "GetPayQueryInfoAsync Error： {Msg}", exception);
                    return JsonResult<InsuranceDto>.Fail(exception.Message);
                }
            }
        }

        /// <summary>
        /// 身份证号码查询，返回单个患者信息
        /// 姓名或电话号码查询，返回多个患者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<JsonResult<List<PatientInfoFromHis>>> GetPatientRecordByIdentityNo(
            CreateOrGetPatientIdInput input)
        {
            var idType = "2"; // 默认类型身份证
            if (!string.IsNullOrEmpty(input.CardNo))
            {
                // 证件类型就诊卡
                idType = "1";
            }
            else if (!string.IsNullOrEmpty(input.IdTypeCode) /*&& !string.IsNullOrEmpty(input.IdentityNo)*/)
            {
                // 证件类型对应 idType
                var idTypeConfig = await this._triageConfigRepository
                    .FirstOrDefaultAsync(x =>
                        x.TriageConfigType == (int)TriageDict.IdType && x.TriageConfigCode == input.IdTypeCode);
                if (idTypeConfig != null) idType = idTypeConfig.HisConfigCode;
            }

            var cardNo = input.IdentityNo ?? input.CardNo;
            // 查询患者信息，返回多条记录
            var response = await _hisApi.GetPatientRecordAsync(idType, cardNo, input.VisitNo, input.PatientName,
                input.ContactsPhone, input.RegisterNo);
            if (response.Code != 200)
            {
                _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", response.Msg);
                return JsonResult<List<PatientInfoFromHis>>.Fail(response.Msg);
            }

            var res = response.Data;
            // 默认来院方式
            var defaultToHospitalWay = await this._triageConfigRepository.AsQueryable().AsNoTracking()
                .Where(x => x.TriageConfigType == (int)TriageDict.ToHospitalWay && x.IsDisable == 1)
                .OrderBy(x => x.Sort)
                .FirstOrDefaultAsync();

            foreach (var item in res)
            {
                item.ToHospitalWay ??= defaultToHospitalWay?.TriageConfigCode;
                item.StartTriageTime = DateTime.Now;
                item.CarNum = input.CarNum;
                item.TaskInfoId = input.TaskInfoId;
                item.StartTriageTime = DateTime.Now;
                item.ContactsPerson = item.ContactsPerson.IsNullOrWhiteSpace()
                    ? input.ContactsPerson
                    : item.ContactsPerson;
                item.ContactsPerson = item.ContactsPerson.IsNullOrWhiteSpace()
                    ? input.ContactsPerson
                    : item.ContactsPerson;
                item.ContactsPhone = item.ContactsPhone.IsNullOrWhiteSpace()
                    ? input.ContactsPhone
                    : item.ContactsPhone;
                item.IdentityNo = item.IdentityNo.IsNullOrWhiteSpace() ? input.IdentityNo : item.IdentityNo;
                item.Address = item.Address.IsNullOrWhiteSpace() ? input.Address : item.Address;
                item.SetGenderAndBirthday(item.IdentityNo);
                // 从HIS查档无法查到患者社保卡号，社保卡号从读卡器程序读取
                item.MedicalNo ??= input.MedicalNo;

                // 推送更改EmrService患者病历信息队列消息
                await SendPatientToEmrService(input, item);
            }


            _log.LogInformation("根据输入项获取患者病历号成功");
            return JsonResult<List<PatientInfoFromHis>>.Ok(data: res);
        }

        /// <summary>
        /// 测试生成病历号程序
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<JsonResult<List<PatientInfoFromHis>>> GetDemoHl7Response(CreateOrGetPatientIdInput input)
        {
            var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
            var res = await SocketHelper.GetPatientRecordAsync(new Hl7PatientInput
            {
                IdNo = input.IdentityNo,
                Name = input.PatientName,
                Sex = dicts.GetNameByDictCode(TriageDict.Sex, input.Sex)
            });

            _log.LogInformation("SocketHelper 查询结果：{Json}", JsonHelper.SerializeObject(res));
            if (!string.IsNullOrEmpty(res.ErrorMsg))
            {
                _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", res.ErrorMsg);
                return JsonResult<List<PatientInfoFromHis>>.Fail(res.ErrorMsg);
            }

            res.Sex = dicts.GetCodeByDictName(TriageDict.Sex, res.Sex);
            PatientInfoFromHis dto = await GetHl7PatientOutput(input, res);
            // 推送更改EmrService患者病历信息队列消息
            await SendPatientToEmrService(input, dto);

            _log.LogInformation("根据输入项获取患者病历号成功");
            return JsonResult<List<PatientInfoFromHis>>.Ok(data: new List<PatientInfoFromHis> { dto });
        }

        private async Task SendPatientToEmrService(CreateOrGetPatientIdInput input, PatientInfoFromHis dto)
        {
            _log.LogTrace("开始推送更改EmrService患者病历信息队列消息");

            // 后期开发BS版急诊程序时，分诊服务代码未与院前分诊代码分离，所以采用此判断来区分是否需要往CS版急诊写入数据
            // 此代码段为发布院前病历服务新增患者RabbitMQ消息
            if (input.TaskInfoId != Guid.Empty && input.IsSyncToEmrService)
            {
                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
                // 分诊时更新EmrService病历患者基本信息
                var emrDto = new CreateOrUpdateEmrPatientDto
                {
                    TaskInfoId = input.TaskInfoId,
                    PatientId = dto.PatientId,
                    PatientName = dto.PatientName,
                    DocumentNum = dto.IdentityNo,
                    DocumentType = "身份证",
                    Age = dto.Age,
                    Id = input.EmrPatientInfoId,
                    Sex = dicts.GetNameByDictCode(TriageDict.Sex, dto.Sex),
                    Nation = dicts.GetNameByDictCode(TriageDict.Nation, dto.Nation),
                    Nationality = dto.Country,
                    Contacts = dto.ContactsPerson,
                    Telephone = dto.ContactsPhone,
                    IsNoThree = false,
                };

                _log.LogInformation("emrDto：{Json}", JsonHelper.SerializeObject(emrDto));
                await _capPublisher.PublishAsync("SaveEmrPatientInfoByHL7PatientInfo", emrDto);
                await _capPublisher.PublishAsync("executing.task.from.emrservice", "");
            }

            _log.LogTrace("推送更改EmrService患者病历信息队列消息结束");
        }

        private async Task<PatientInfoFromHis> GetHl7PatientOutput(CreateOrGetPatientIdInput input,
            Hl7PatientInfoDto res)
        {
            if (input.IdentityNo.Length == 18)
            {
                res.Birthday = DateTime.ParseExact(input.IdentityNo.Substring(6, 8), "yyyyMMdd",
                    CultureInfo.CurrentCulture);
                var sexFlag = Convert.ToInt32(input.IdentityNo.Substring(16, 1));
                res.Sex = Convert.ToBoolean(sexFlag & 1)
                    ? Gender.Male.GetDescriptionByEnum()
                    : Gender.Female.GetDescriptionByEnum();
                res.GetAge();
            }

            //若接口身份证号返回为空则使用前端传入
            if (res.IdentityNo.IsNullOrWhiteSpace())
            {
                res.IdentityNo = input.IdentityNo;
            }

            var dto = res.BuildAdapter().AdaptToType<PatientInfoFromHis>();
            // 默认来院方式
            var defaultToHospitalWay = await this._triageConfigRepository.AsQueryable().AsNoTracking()
                .Where(x => x.TriageConfigType == (int)TriageDict.ToHospitalWay && x.IsDisable == 1)
                .OrderBy(x => x.Sort)
                .FirstOrDefaultAsync();
            dto.TaskInfoId = input.TaskInfoId;
            dto.CarNum = input.CarNum;
            dto.Sex ??= input.Sex;
            dto.ContactsPerson = string.IsNullOrWhiteSpace(dto.ContactsPerson)
                ? input.ContactsPerson
                : dto.ContactsPerson;
            dto.ContactsPhone = string.IsNullOrWhiteSpace(dto.ContactsPhone)
                ? input.ContactsPhone
                : dto.ContactsPhone;
            dto.StartTriageTime = DateTime.Now;
            dto.ToHospitalWay ??= defaultToHospitalWay?.TriageConfigName;
            dto.Address = string.IsNullOrWhiteSpace(dto.Address) ? input.Address : dto.Address;
            dto.Nation = input.Nation;
            return dto;
        }


        /// <summary>
        /// 根据输入项查询患者已有的档案信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<List<PatientInfoDto>>> GetPatientInfoListByInputAsync(SelectAlreadyTriageDto input)
        {
            try
            {
                var res = await _patientInfoRepository
                    .Where(x => x.PatientName == input.PatientName
                                || x.ContactsPhone == input.Phone
                                || x.IdentityNo == input.IdentityNo)
                    .ToListAsync();


                var dtos = res.BuildAdapter().AdaptToType<List<PatientInfoDto>>();

                return JsonResult<List<PatientInfoDto>>.Ok(data:
                    res.BuildAdapter().AdaptToType<List<PatientInfoDto>>());
            }
            catch (Exception e)
            {
                _log.LogError("根据输入项查询患者已有的档案信息错误！原因：{Msg}", e);
                return JsonResult<List<PatientInfoDto>>.Fail(e.Message);
            }
        }


        /// <summary>
        /// 根据输入项获取患者病历号列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<List<PatientOutput>>> GetPatientRecordListAsync(CreateOrGetPatientIdInput input)
        {
            var list = new List<PatientOutput>();
            try
            {
                _log.LogInformation("根据输入项获取患者病历号列表");
                var res = new Hl7PatientInfoDto();
                if (await _redis.KeyExistsAsync(
                        $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.IdentityNo}"))
                {
                    var json = await _redis.StringGetAsync(
                        $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.IdentityNo}");
                    var dto = JsonSerializer.Deserialize<PatientOutput>(json);
                    _log.LogInformation("根据输入项获取患者病历号列表");
                    list.Add(dto);
                    return JsonResult<List<PatientOutput>>.Ok(data: list);
                }

                if (!Convert.ToBoolean(_configuration["Settings:IsEnabledCreatePatientIdExe"]))
                {
                    var uri = _configuration["HisApiUrl"] + "/api/ecis/getPatientInfo";
                    uri +=
                        $"?idNo={input.IdentityNo}&idType={input.CardType}&name={input.PatientName}&phone={input.ContactsPhone}";
                    var response = await HttpClientHelper.GetAsync(uri);
                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        var json = JObject.Parse(response);
                        if (json["code"]?.ToString() == "0")
                        {
                            if (json["data"] != null && !string.IsNullOrWhiteSpace(json["data"].ToString()))
                            {
                                var resp = JsonSerializer.Deserialize<PatientRespDto>(json["data"].ToString());
                                res = resp.BuildAdapter().AdaptToType<Hl7PatientInfoDto>();
                            }
                            else
                            {
                                _log.LogError("根据输入项获取患者病历号，调用查询患者信息接口失败！原因：{Msg}", "返回Data为null或空");
                                res.ErrorMsg = "查询患者信息失败！请检查后重试";
                            }
                        }
                        else
                        {
                            _log.LogError("根据输入项获取患者病历号，调用查询患者信息接口失败！原因：{Msg}", json["msg"]);
                            res.ErrorMsg = "调用查询患者信息接口失败" + json["msg"];
                        }
                    }
                    else
                    {
                        _log.LogError("根据输入项获取患者病历号，调用查询患者信息接口失败！原因：{Msg}", "接口响应为空");
                        res.ErrorMsg = "调用查询患者信息接口失败！请检查后重试";
                    }
                }
                else
                {
                    res = await SocketHelper.GetPatientRecordAsync(new Hl7PatientInput
                    {
                        IdNo = input.IdentityNo,
                        Name = input.PatientName,
                        Sex = input.Sex
                    });
                }

                if (string.IsNullOrEmpty(res.ErrorMsg))
                {
                    if (input.IdentityNo.Length == 18)
                    {
                        res.Birthday = DateTime.ParseExact(input.IdentityNo.Substring(6, 8), "yyyyMMdd",
                            CultureInfo.CurrentCulture);
                        res.GetAge();
                    }

                    var dicts = await TriageConfigService.GetTriageConfigByRedisAsync(TriageDict.Sex.ToString());
                    var dto = res.BuildAdapter().AdaptToType<PatientOutput>();
                    dto.TaskInfoId = input.TaskInfoId;
                    dto.CarNum = input.CarNum;
                    dto.Sex = dicts.GetCodeByDictName(TriageDict.Sex, dto.Sex);
                    dto.MedicalNo = input.MedicalNo;
                    await _redis.StringSetAsync(
                        $"{_configuration["ServiceName"]}:PatientBuilt:{input.TaskInfoId}:{input.IdentityNo}",
                        JsonSerializer.Serialize(dto));

                    _log.LogInformation("根据输入项获取患者病历号成功");
                    list.Add(dto);
                    return JsonResult<List<PatientOutput>>.Ok(data: list);
                }

                _log.LogError("根据输入项获取患者病历号错误！原因：{Msg}", res.ErrorMsg);
                return JsonResult<List<PatientOutput>>.Fail(res.ErrorMsg);
            }
            catch (Exception e)
            {
                _log.LogError("根据输入项获取患者病历号错误！原因：{Msg}", e);
                return JsonResult<List<PatientOutput>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据输入项创建患者病历号（建档）. 三无患者传入的身份证号值为空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + "CreatePatientId")]
        public async Task<JsonResult<PatientInfoFromHis>> CreatePatientRecordByHl7MsgAsync(
            CreateOrGetPatientIdInput input)
        {
            try
            {
                _log.LogInformation("根据输入项创建患者病历号开始");

                var isNothree = input.IsNoThree; // 是否三无
                bool isInfant; // 是否婴幼儿

                // 建当前校验，不同医院校验条件不同
                var validateResult = await _hisApi.ValidateBeforeCreatePatient(input, out isInfant);
                if (validateResult.Code != 200) return JsonResult<PatientInfoFromHis>.Fail(validateResult.Msg);

                if (string.IsNullOrEmpty(input.ContactsPhone))
                {
                    input.ContactsPhone = "无";
                }

                if (string.IsNullOrEmpty(input.Address))
                {
                    input.Address = "无";
                }

                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();

                input.CheckNationIsFull();
                input.Nation = dicts.GetCodeByDictName(TriageDict.Nation, input.Nation);

                if (isNothree)
                {
                    if (string.IsNullOrWhiteSpace(input.IdentityNo))
                    {
                        var nowDate = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000)
                                       / 10000000)
                            .ToString();
                        input.IdentityNo = "Y" + nowDate;
                    }

                    input.PatientName = input.PatientName.IsNullOrWhiteSpace() || input.PatientName == "无名氏"
                        ? "无名氏_" + DateTime.Now.ToString("yyyyMMddHHmmss") +
                          dicts.GetNameByDictCode(TriageDict.Sex, input.Sex)
                        : input.PatientName;
                }

                PatientInfoFromHis res;
                //是否启用测试生成病历号程序
                if (Convert.ToBoolean(_configuration["Settings:IsEnabledCreatePatientIdExe"]))
                {
                    var response = await SocketHelper.CreatePatientRecordAsync(new Hl7PatientInput
                    {
                        IdNo = input.IdentityNo,
                        Name = input.PatientName,
                        Sex = dicts.GetNameByDictCode(TriageDict.Sex, input.Sex),
                    });

                    if (!string.IsNullOrEmpty(response.ErrorMsg))
                    {
                        _log.LogInformation("根据输入项创建患者病历号失败！原因：{Msg}", response.ErrorMsg);
                        return JsonResult<PatientInfoFromHis>.Fail(response.ErrorMsg);
                    }

                    if (input.Birthday != null)
                    {
                        response.Birthday = Convert.ToDateTime(input.Birthday);
                        response.GetAge();
                    }

                    res = response.BuildAdapter().AdaptToType<PatientInfoFromHis>();
                    res.Sex = dicts.GetCodeByDictName(TriageDict.Sex, res.Sex);
                }
                else
                {
                    JsonResult<PatientInfoFromHis> result;
                    if (isNothree)
                    {
                        // 调用三无建档接口
                        result = await _hisApi.CreateNoThreePatientRecordAsync(input);
                    }
                    else if (isInfant)
                    {
                        // 调用婴幼儿建档接口
                        result = await _hisApi.CreateNoThreePatientRecordAsync(input);
                    }
                    else
                    {
                        // 调用建档接口
                        result = await _hisApi.CreatePatientRecordAsync(input);
                        if (result.Msg.Contains("已存在门诊号"))
                        {
                            result.Code = 200;
                            result.Data = new PatientInfoFromHis
                            {
                                IdentityNo = input.IdentityNo,
                                PatientName = input.PatientName,
                                Birthday = input.Birthday
                            }
                                .GetPatientInfoByRegexMsg(result.Msg).SetGenderAndBirthday(input.IdentityNo);

                            if (result.Data.PatientId.IsNullOrWhiteSpace())
                            {
                                result.Code = 500;
                            }
                        }
                    }

                    if (result.Code != 200)
                    {
                        _log.LogError("根据输入项创建患者病历号失败！原因：{Msg}", result.Msg);
                        return JsonResult<PatientInfoFromHis>.Fail("建档失败，" + result.Msg);
                    }

                    res = result.Data;
                }

                var dto = res.GetAge();
                dto.TaskInfoId = input.TaskInfoId;
                dto.CarNum = input.CarNum;
                dto.IsNoThree = isNothree;
                dto.ContactsPerson = string.IsNullOrWhiteSpace(dto.ContactsPerson)
                    ? input.ContactsPerson
                    : dto.ContactsPerson;
                dto.ContactsPhone = string.IsNullOrWhiteSpace(dto.ContactsPhone)
                    ? input.ContactsPhone
                    : dto.ContactsPhone;
                dto.Identity = input.Identity;
                dto.Address = input.Address;
                dto.Age = res.Age.IsNullOrWhiteSpace() ? input.Age : res.Age;
                dto.Nation = string.IsNullOrWhiteSpace(dto.Nation) ? input.Nation : dto.Nation;
                dto.StartTriageTime = DateTime.Now;
                dto.IdentityNo = string.IsNullOrWhiteSpace(dto.IdentityNo) ? input.IdentityNo : dto.IdentityNo;
                dto.ContactsPerson = input.ContactsPerson;
                dto.ContactsPhone = input.ContactsPhone;
                dto.GuardianIdCardNo = input.GuardianIdCardNo;
                dto.GuardianIdTypeCode = input.GuardianIdTypeCode;
                dto.SocietyRelationCode = input.GuardianIdTypeCode;
                dto.VisitNo ??= input.VisitNo;
                dto.IdTypeCode ??= input.IdTypeCode;
                dto.GuardianIdTypeCode ??= input.GuardianIdTypeCode;
                dto.GestationalWeeks = input.GestationalWeeks;
                dto.CrowdCode = input.CrowdCode;
                dto.CrowdName = input.CrowdName;
                dto.ChargeType = input.ChargeType;
                if (string.IsNullOrEmpty(dto.ChargeType))
                {
                    //默认自费
                    dto.ChargeType = DEFAULTCHARGETYPE;
                }

                // 建档患者缓存，当在 HIS 建档后，预检分诊并没有存储患者信息。
                await _redis.StringSetAsync(
                    $"{_configuration["ServiceName"]}:PatientBuilt:{dto.TaskInfoId}:{dto.PatientId}",
                    JsonSerializer.Serialize(dto));
                await _redis.StringSetAsync(
                    $"{_configuration["ServiceName"]}:PatientBuilt:{dto.TaskInfoId}:{dto.IdentityNo}",
                    JsonSerializer.Serialize(dto));
                if (!string.IsNullOrEmpty(dto.CardNo))
                {
                    // 三无患者建档后进行缓存
                    await _redis.StringSetAsync(
                        $"{_configuration["ServiceName"]}:PatientNoThree:{dto.CardNo}",
                        JsonSerializer.Serialize(dto));
                }

                string identityNo = res.IdentityNo;
                string insuplcAdmdvCode = res.InsuplcAdmdvCode;
                if (!string.IsNullOrEmpty(identityNo))
                {
                    bool isCheckIdentityNo = _configuration.GetSection("Settings:IsCheckIdentityNo").Get<bool>();
                    if (isCheckIdentityNo)
                    {
                        var idcard = IDCard.IDCard.Verify(identityNo);
                        if (idcard.IsVerifyPass)
                        {
                            PreGetInsuranceInfos(identityNo, insuplcAdmdvCode);
                        }
                    }
                    else
                    {
                        PreGetInsuranceInfos(identityNo, insuplcAdmdvCode);
                    }
                }

                #region 同步到急救病历

                if (input.IsSyncToEmrService)
                {
                    // 后期开发BS版急诊程序时，分诊服务代码未与院前分诊代码分离，所以采用此判断来区分是否需要往CS版急诊写入数据
                    // 此代码段为发布院前病历服务新增患者RabbitMQ消息
                    _log.LogInformation("开始推送更改EmrService患者病历信息队列消息");
                    // 分诊时更新EmrService病历患者基本信息
                    var emrDto = new CreateOrUpdateEmrPatientDto
                    {
                        TaskInfoId = input.TaskInfoId,
                        PatientId = dto.PatientId,
                        PatientName = dto.PatientName,
                        DocumentNum = dto.IdentityNo,
                        DocumentType = "身份证",
                        Age = dto.Age,
                        Id = input.EmrPatientInfoId,
                        Sex = dicts.GetNameByDictCode(TriageDict.Sex, input.Sex),
                        Nation = dicts.GetNameByDictCode(TriageDict.Nation, dto.Nation),
                        Nationality = dto.Country,
                        IsNoThree = isNothree,
                        Contacts = dto.ContactsPerson,
                        Telephone = dto.ContactsPhone
                    };

                    await _capPublisher.PublishAsync("SaveEmrPatientInfoByHL7PatientInfo", emrDto);
                    _log.LogInformation("推送更改EmrService患者病历信息队列消息结束");

                    // websocket推送通知前端调用查询患者信息接口
                    await _capPublisher.PublishAsync("executing.task.from.emrservice", "");
                    _log.LogInformation("创建档案，websocket推送通知前端调用查询患者信息接口完成");
                }

                #endregion

                _log.LogInformation("根据输入项创建患者病历号成功");
                return JsonResult<PatientInfoFromHis>.Ok(data: dto);
            }

            catch (Exception e)
            {
                _log.LogError("根据输入项获取或者创建患者病历号错误！原因：{Msg}", e);
                return JsonResult<PatientInfoFromHis>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 修改患者信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + "CreatePatientId")]
        public async Task<JsonResult> UpdatePatientRecordAsync(PatientModifyDto dto)
        {
            var patient = await this._patientInfoRepository
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (patient == null)
            {
                return JsonResult.Fail("患者信息不存在或已删除");
            }

            if (patient.IsReferral)
            {
                return JsonResult.Fail("患者已转普通门诊科室");
            }

            var uow = UnitOfWorkManager.Begin();
            try
            {
                dto.SexName = dto.Sex switch
                {
                    "Sex_Man" => "男",
                    "Sex_Woman" => "女",
                    _ => "未知"
                };
                // 修改本地患者信息
                dto.BuildAdapter().AdaptTo(patient);
                if (!string.IsNullOrEmpty(dto.IdTypeCode))
                {
                    // 证件类型
                    var idTypeConfig = await this._triageConfigRepository.AsNoTracking()
                        .FirstOrDefaultAsync(x =>
                            x.TriageConfigType == (int)TriageDict.IdType && x.TriageConfigCode == dto.IdTypeCode);
                    patient.IdTypeCode = idTypeConfig?.TriageConfigCode;
                    patient.IdTypeName = idTypeConfig?.TriageConfigName;
                }

                dto.VisitNo = patient.VisitNo;
                await this._patientInfoRepository.UpdateAsync(patient);
                var result = await _hisApi.RevisePerson(dto);
                if (result.Code != 200)
                {
                    await uow.RollbackAsync();
                    return JsonResult.Fail(result.Msg);
                }

                var patientMq = dto.BuildAdapter().AdaptToType<PatientModifyMqDto>();
                await _capPublisher.PublishAsync("sync.patient.modifypatient.from.triageservice", patientMq);
                await uow.CompleteAsync();

                return JsonResult.Ok("修改患者信息成功");
            }
            catch (Exception exception)
            {
                await uow.RollbackAsync();
                return JsonResult.Fail(exception.Message);
            }
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> Cancel(Guid id)
        {
            var patientInfo = await this._patientInfoRepository
                .Include(x => x.RegisterInfo)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (patientInfo is null)
            {
                return JsonResult.Fail("患者信息不存在");
            }

            if (patientInfo.IsCancelled)
            {
                return JsonResult.Fail("已作废的患者信息无法重复作废");
            }

            if (patientInfo.VisitStatus == VisitStatus.Treating || patientInfo.VisitStatus == VisitStatus.Treated)
            {
                return JsonResult.Fail("该患者已开始就诊，无法作废");
            }

            if (patientInfo.IsReferral)
            {
                return JsonResult.Fail("患者已转普通门诊科室");
            }

            patientInfo.IsCancelled = true;
            patientInfo.CancellationUser = CurrentUser?.UserName;
            patientInfo.CancellationTime = DateTime.Now;

            using var uow = UnitOfWork.Begin();
            try
            {
                _ = await this._patientInfoRepository.UpdateAsync(patientInfo);
                if (patientInfo.RegisterInfo.Count > 0 &&
                    !string.IsNullOrEmpty(patientInfo.RegisterInfo.FirstOrDefault()?.RegisterNo))
                {
                    // 调用 HIS 接口取消挂号
                    _ = await this._hisApi.CancelRegisterInfoAsync(
                        patientInfo.RegisterInfo.FirstOrDefault()?.RegisterNo);

                    //同步作废状态到patient服务
                    await _capPublisher.PublishAsync("sync.patient.visitstatus.from.triageservice",
                        new SyncVisitStatusDto(id, EVisitStatus.RefundNo));
                }

                await uow.CompleteAsync();
            }
            catch (Exception exception)
            {
                _log.LogError("作废失败！原因：{Msg}", exception.Message);
                await uow.RollbackAsync();
                return JsonResult.Fail(msg: $"作废失败, {exception.Message}");
            }

            return JsonResult.Ok(msg: "作废成功");
        }

        /// <summary>
        /// 分诊确认保存
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [UnitOfWork]
        [Auth("PatientInfo" + PermissionDefinition.Separator + "SaveTriage")]
        public async Task<JsonResult<PatientInfoDto>> SaveTriageRecordAsync(CreateOrUpdatePatientDto dto)
        {
            try
            {
                _log.LogTrace($"分诊确认保存，患者-{dto.PatientName}-，CreateOrUpdatePatientDto对象为：{JsonSerializer.Serialize(dto, options)}");

                #region 获取配置

                // 科室配置
                var triageDept = await _triageConfigRepository.AsQueryable().AsNoTracking()
                    .FirstOrDefaultAsync(x => x.TriageConfigCode == dto.ConsequenceInfo.TriageDept);
                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync(isEnable: -1, isDeleted: -1);

                #endregion

                // 是否修改分诊状态
                bool triageStatus = false;

                #region 患者是否已分诊
                PatientInfo currentDBPatient = null;

                // 在PKU，无论修改或插入实际 TriagePatientInfoId 一直都有传
                if (dto.TriagePatientInfoId != Guid.Empty)
                {
                    currentDBPatient = await _patientInfoRepository.AsNoTracking()
                                                                    .Include(x => x.ConsequenceInfo)
                                                                    .Include(x => x.VitalSignInfo)
                                                                    .Include(x => x.ScoreInfo)
                                                                    .Include(x => x.AdmissionInfo)
                                                                    .Include(x => x.RegisterInfo)
                                                                    .FirstOrDefaultAsync(x => x.Id == dto.TriagePatientInfoId);
                    if (currentDBPatient is { IsReferral: true })
                    {
                        _log.LogError("保存分诊失败，患者已转普通门诊科室");
                        return JsonResult<PatientInfoDto>.Fail("保存分诊失败，患者已转普通门诊科室");
                    }
                }
                else
                {
                    _log.LogWarning($"确认分诊的患者TriagePatientInfoId为Guid.Empty，其existsPatient为null");
                }

                triageStatus = (currentDBPatient?.TriageStatus == (int)TriageStatus.Triage)
                                && (dto.TriageStatus == (int)TriageStatus.Triage);

                // 是否修改分诊科室和医生编码
                // 修改了的话，那需要重新排队，是为了解决类似患者5点在A医生看了去检查，然后回来A医生下班了，那就需要转到B医生继续看病
                // 实际现在流程，一个患者检查后，也是需要指定其他医生，触发重新排队才可以
                bool hasChangedDoctor =
                    dto.ConsequenceInfo?.TriageDept != currentDBPatient?.ConsequenceInfo?.TriageDeptCode ||
                    dto.ConsequenceInfo.DoctorCode != currentDBPatient?.ConsequenceInfo?.DoctorCode;
                #endregion

                #region dto赋值
                // 生日
                dto.Birthday ??= dto.BirthDate?.ToString("yyyy-MM-dd");
                if (DateTime.TryParse(dto.Birthday, out DateTime birthDate))
                {
                    dto.BirthDate ??= birthDate;
                }
                //挂号类型为空赋默认值1
                if (string.IsNullOrEmpty(dto.RegType))
                {
                    dto.RegType = "1";
                }
                dto.TriageUserCode = dto.TriageUserCode.IsNullOrWhiteSpace() ? CurrentUser.UserName : dto.TriageUserCode;
                dto.TriageUserName = dto.TriageUserName.IsNullOrWhiteSpace() ? CurrentUser.GetFullName() : dto.TriageUserName;
                dto.InvoiceNum = currentDBPatient?.InvoiceNum;  //前端未传，暂从后端取
                var dtoPatient = dto.BuildAdapter().AdaptToType<PatientInfo>().GetNamePy();
                #endregion

                // 校验数据
                var checkData = SaveTriageCheck(dto, currentDBPatient, triageDept);
                if (!checkData.isPass)
                {
                    return JsonResult<PatientInfoDto>.Fail(checkData.message);
                }

                //医保电子凭证
                dtoPatient.ElectronCertNo = dto.ElectronCertNo;

                // 分诊科室名称
                dto.ConsequenceInfo.TriageDeptName = dicts.GetNameByDictCode(TriageDict.TriageDepartment, dto.ConsequenceInfo.TriageDept);
                // 实际分诊级别名称
                dto.ConsequenceInfo.ActTriageLevelName = dicts.GetNameByDictCode(TriageDict.TriageLevel, dto.ConsequenceInfo.ActTriageLevel);

                // 分诊保存前调用 HIS 或 Call服务 相关接口，主要为了得到排队号 CallingSn
                CommonHttpResult<PatientInfo> callRetult = await this._hisApi.BeforeSaveTriageRecordAsync(dto, dtoPatient, currentDBPatient, true, hasChangedDoctor);

                if (callRetult.Code != 0 && callRetult.Code != 200)
                {
                    return JsonResult<PatientInfoDto>.Fail(callRetult.Msg);
                }
                #region 保存病人信息变更记录
                if (triageStatus)
                {
                    SaveChangeRecord(currentDBPatient.ConsequenceInfo, dtoPatient.ConsequenceInfo, dtoPatient.TriageUserCode, dtoPatient.TriageUserName, dicts);
                }
                #endregion

                // 实际保存分诊信息
                var returnResult = await _patientInfoRepository.SaveTriageRecordAsync(new List<PatientInfo> { dtoPatient }, dicts);
                if (!returnResult.Data)
                {
                    _log.LogError("分诊确认保存失败！原因：{Msg}", "仓储保存分诊失败");
                    return JsonResult<PatientInfoDto>.Fail(returnResult.Msg);
                }

                // 保存患者信息，以备当前接口后续的查询使用
                await this.CurrentUnitOfWork.SaveChangesAsync();

                #region EFCore DBContext 保存数据时会把数据先加载到实体上，导致删除后数据也会出现，所以去除删除了的数据

                if (Convert.ToBoolean(dtoPatient.AdmissionInfo?.IsDeleted))
                {
                    dtoPatient.AdmissionInfo = null;
                }

                if (Convert.ToBoolean(dtoPatient.VitalSignInfo?.IsDeleted))
                {
                    dtoPatient.VitalSignInfo = null;
                }

                if (Convert.ToBoolean(dtoPatient.ConsequenceInfo?.IsDeleted))
                {
                    dtoPatient.ConsequenceInfo = null;
                }

                dtoPatient.RegisterInfo = dtoPatient.RegisterInfo?.Where(x => x.IsDeleted == false).ToList();
                dtoPatient.ScoreInfo = dtoPatient.ScoreInfo?.Where(x => x.IsDeleted == false).ToList();

                #endregion

                RegisterInfo registerInfo = currentDBPatient.RegisterInfo.OrderBy(x => x.CreationTime).FirstOrDefault();

                #region 调用RFID绑定接口

                if (Convert.ToBoolean(_configuration["Settings:RFID:IsEnabled"]) && !dtoPatient.RFID.IsNullOrWhiteSpace())
                {
                    var api = ServiceProvider.GetRequiredService<IRfidApi>();
                    dtoPatient.GreenRoadName = dicts.GetNameByDictCode(TriageDict.GreenRoad, dtoPatient.GreenRoadCode);
                    await api.BindAsync(dtoPatient, registerInfo);
                }

                #endregion

                var patientMqDto = new PatientInfoMqDto
                {
                    PatientInfo = dtoPatient.BuildAdapter().AdaptToType<PatientInfoDto>(),
                    ConsequenceInfo = dtoPatient.ConsequenceInfo?.BuildAdapter().AdaptToType<ConsequenceInfoDto>(),
                    VitalSignInfo = dtoPatient.VitalSignInfo?.BuildAdapter().AdaptToType<VitalSignInfoDto>(),
                    ScoreInfo = dtoPatient.ScoreInfo?.BuildAdapter().AdaptToType<List<ScoreInfoDto>>(),
                    RegisterInfo = registerInfo?.BuildAdapter().AdaptToType<RegisterInfoDto>(),
                    AdmissionInfo = dtoPatient.AdmissionInfo?.BuildAdapter().AdaptToType<AdmissionInfoDto>()
                };

                if (patientMqDto.ConsequenceInfo != null)
                {
                    patientMqDto.ConsequenceInfo.HisDeptCode = dicts[TriageDict.TriageDepartment.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == patientMqDto.ConsequenceInfo.TriageDept)
                        ?.HisConfigCode;
                }

                // 添加医保控费相关
                // 数据由数据库拿而不是由前端传
                if (currentDBPatient != null)
                {
                    patientMqDto.PatientInfo.OutSetlFlag = currentDBPatient.OutSetlFlag;
                    patientMqDto.PatientInfo.PatnId = currentDBPatient.PatnId;
                    patientMqDto.PatientInfo.PoolArea = currentDBPatient.PoolArea;
                    patientMqDto.PatientInfo.CurrMDTRTId = currentDBPatient.CurrMDTRTId;
                    patientMqDto.PatientInfo.InsureType = currentDBPatient.InsureType;
                }

                // 急诊分诊发送队列消息到叫号、诊疗微服务
                // 推送MQ消息到医生站
                await RabbitMqAppService.PublishEcisPatientSyncPatientAsync(new List<PatientInfoMqDto> { patientMqDto });

                #region 绿通患者推送队列消息到六大中心同步病患

                // 是否启用发送数据到六大中心
                if (Convert.ToBoolean(_configuration["Settings:IsEnabledSixCenterMQ"]))
                {
                    if (!dtoPatient.GreenRoadCode.IsNullOrWhiteSpace())
                    {
                        var eto = dtoPatient.BuildAdapter().AdaptToType<SyncPatientEventBusEto>();
                        eto.RegisterNo = registerInfo?.RegisterNo;
                        await RabbitMqAppService.PublishSixCenterSyncPatientAsync(
                            new List<SyncPatientEventBusEto> { eto }, dicts);
                        await RabbitMqAppService.PublishScoreRecordToSixCenterAsync(dtoPatient.ScoreInfo.ToList());
                    }
                }

                #endregion

                await CurrentUnitOfWork.SaveChangesAsync();

                var patientInfoDto = await this.GetPatientInfoByInputAsync(new PatientInfoInput { Id = dtoPatient.Id });
                _log.LogInformation("分诊完成，查询患者信息: {Id}", dtoPatient.Id);
                return patientInfoDto;
            }
            catch (Exception e)
            {
                await CurrentUnitOfWork.RollbackAsync();
                _log.LogError("分诊确认保存错误！原因：{Msg}", e);
                return JsonResult<PatientInfoDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 确认分诊校验、暂存不需要
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="triageDept"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private (bool isPass, string message) SaveTriageCheck(CreateOrUpdatePatientDto dto, PatientInfo patientInfo, TriageConfig triageDept)
        {
            /**** 
                龙岗中心医院--预检分诊-新增分诊界面：
                【必填项说明】
                三无：患者编号、姓名、性别； 
                儿童（少于14岁）：患者编号、姓名、性别、联系人、联系电话、联系人证件； 
                普通病人：患者编号、姓名、性别、出生日期、证件类型、证件号码、电话、地址； 
                儿童（人群选了儿童）：患者编号、姓名、性别、联系人、联系电话、联系人证件； 
                孕妇（人群选了孕妇）：普通病人项+孕周；
            ***/
            if (dto.TriageStatus != (int)TriageStatus.Triage) return (true, "");
            if (string.IsNullOrWhiteSpace(dto.ConsequenceInfo?.TriageDept))
            {
                _log.LogError("保存分诊失败，原因：分诊科室不能为空");
                return (false, "分诊科室不能为空");
            }

            if (string.IsNullOrWhiteSpace(dto.ConsequenceInfo?.ActTriageLevel))
            {
                _log.LogError("保存分诊失败，原因：分诊级别不能为空");
                return (false, "分诊级别不能为空");
            }

           // 【深圳市规定】血压和年龄关联，35 岁以上分诊一定要测血压，否则无法分诊
           var requiredBPAgeString = _configuration.GetSection("Settings:RequiredBPAge").Value;
            if (// VitalSignRemark_002: 拒测生命体征
                dto.VitalSignInfo.Remark != "VitalSignRemark_002" &&
                dto.VitalSignInfo.RemarkName != "拒测血压"&&
                // 分诊级别 1~2 级不限制测生命体征
                dto.ConsequenceInfo.ActTriageLevel != TriageLevel.FirstLv.GetDescriptionByEnum() &&
                dto.ConsequenceInfo.ActTriageLevel != TriageLevel.SecondLv.GetDescriptionByEnum() &&
                // 分诊科室抢救室、观察区不限制测生命体征
                !triageDept.TriageConfigName.Contains("抢救室") && !triageDept.TriageConfigName.Contains("观察区") &&
                dto.BirthDate.HasValue && !string.IsNullOrWhiteSpace(requiredBPAgeString) &&
                int.TryParse(requiredBPAgeString, out int requiredBPAge) && requiredBPAge >= 0)
            {
                var totalDiffMonths = (DateTime.Today.Year - dto.BirthDate.Value.Year) * 12 +
                                      (DateTime.Today.Month - dto.BirthDate.Value.Month) +
                                      (DateTime.Today.Day >= dto.BirthDate.Value.Day ? 0 : -1) /*是否不足1月*/;
                if (totalDiffMonths > requiredBPAge * 12 &&
                    (string.IsNullOrWhiteSpace(dto.VitalSignInfo?.Sbp) ||
                     string.IsNullOrWhiteSpace(dto.VitalSignInfo?.Sdp)))
                {
                    throw new Exception($"{requiredBPAge} 岁以上分诊一定要测血压，否则无法分诊");
                }
            }

            // 群体为孕妇的，必须填写孕周
            if (dto.CrowdCode == "Crowd_Pregnant" && dto.GestationalWeeks <= 0)
            {
                throw new Exception($"孕妇必须填写孕周");
            }
            //配置不允许修改分诊设置
            var denyReTriage = _configuration["PekingUniversity:DenyReTriage"] ?? "true";
            //患者被医生接诊后，预检分诊的去向和级别就不能修改了。
            if (denyReTriage.ParseToBool()
                && (patientInfo.VisitStatus == VisitStatus.Treating || patientInfo.VisitStatus == VisitStatus.Treated)
                && !patientInfo.IsUntreatedOver
                && (dto.ConsequenceInfo?.ActTriageLevel != patientInfo.ConsequenceInfo.ActTriageLevel || dto.ConsequenceInfo?.TriageTarget != patientInfo.ConsequenceInfo.TriageTargetCode || dto.ConsequenceInfo?.TriageAreaCode != patientInfo.ConsequenceInfo.TriageAreaCode || dto.ConsequenceInfo?.TriageDept != patientInfo.ConsequenceInfo.TriageDeptCode || dto.ConsequenceInfo?.DoctorCode != patientInfo.ConsequenceInfo.DoctorCode || dto.ConsequenceInfo?.ChangeTriageReasonCode != patientInfo.ConsequenceInfo.ChangeTriageReasonCode))
            {
                _log.LogError("患者已被医生接诊，检分诊去向或级别不能修改");
                return (false, "患者已被医生接诊，检分诊去向或级别不能修改");
            }
            return (true, "");
        }

        /// <summary>
        /// 保存变更字段 目前只保存分诊结果
        /// 记录信息到 PatientInfoChangeRecord 表
        /// </summary>
        /// <param name="oldConsequenceInfo"></param>
        /// <param name="newConsequenceInfo"></param>
        /// <param name="operatedCode"></param>
        /// <param name="operatedName"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SaveChangeRecord(ConsequenceInfo oldConsequenceInfo, ConsequenceInfo newConsequenceInfo, string operatedCode, string operatedName, Dictionary<string, List<TriageConfigDto>> dicts)
        {
            // 分诊科室名称
            newConsequenceInfo.TriageDeptName = string.IsNullOrWhiteSpace(dicts.GetNameByDictCode(
                     TriageDict.TriageDepartment,
                     newConsequenceInfo.TriageDeptCode))
                     ? newConsequenceInfo.TriageDeptName
                     : dicts.GetNameByDictCode(TriageDict.TriageDepartment,
                         newConsequenceInfo.TriageDeptCode);

            // 实际分诊级别名称
            newConsequenceInfo.ActTriageLevelName = string.IsNullOrWhiteSpace(
                    dicts.GetNameByDictCode(
                        TriageDict.TriageLevel,
                        newConsequenceInfo.ActTriageLevel))
                    ? newConsequenceInfo.ActTriageLevelName
                    : dicts.GetNameByDictCode(TriageDict.TriageLevel,
                        newConsequenceInfo.ActTriageLevel);

            // 分诊去向名称
            newConsequenceInfo.TriageTargetName = string.IsNullOrWhiteSpace(
                dicts.GetNameByDictCode(TriageDict.TriageDirection,
                    newConsequenceInfo.TriageTargetCode))
                ? newConsequenceInfo.TriageTargetName
                : dicts.GetNameByDictCode(TriageDict.TriageDirection,
                    newConsequenceInfo.TriageTargetCode);

            #region 分诊结果变更记录 记录信息到 PatientInfoChangeRecord 表
            List<string> fileNames = new List<string>() { "ActTriageLevelName", "TriageDeptName", "TriageTargetName" };
            PropertyInfo[] propertyInfos = typeof(ConsequenceInfo).GetProperties();
            var dbContext = _patientInfoRepository.GetDbContext();
            foreach (string item in fileNames)
            {
                PropertyInfo propertyInfo = propertyInfos.FirstOrDefault(x => x.Name.ToLower() == item.ToLower());
                if (propertyInfo == null) continue;
                string oldValue = propertyInfo.GetValue(oldConsequenceInfo)?.ToString();
                string newValue = propertyInfo.GetValue(newConsequenceInfo)?.ToString();
                if (oldValue != newValue)
                {
                    PatientInfoChangeRecord patientInfoChangeRecord = new PatientInfoChangeRecord(Guid.NewGuid());
                    patientInfoChangeRecord.PI_Id = oldConsequenceInfo.PatientInfoId;
                    patientInfoChangeRecord.ChangeField = propertyInfo.Name;
                    patientInfoChangeRecord.BeforeValue = oldValue;
                    patientInfoChangeRecord.AfterValue = newValue;
                    patientInfoChangeRecord.ChangeReason = newConsequenceInfo.ChangeTriageReasonName;
                    patientInfoChangeRecord.OperatedCode = operatedCode;
                    patientInfoChangeRecord.OperatedName = operatedName;
                    dbContext.Entry(patientInfoChangeRecord).State = EntityState.Added;
                }
            }
            dbContext.SaveChanges();
            #endregion
        }

        /// <summary>
        /// 获取患者信息修改记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<JsonResult<PagedResultDto<PatientInfoChangeDto>>> GetChangeRecord(PatientInfoChangeInput dto)
        {
            List<PatientInfoChangeDto> patientInfoChangeDtoList = await _changeRecordRepository.AsQueryable().Join(_patientInfoRepository, x => x.PI_Id, y => y.Id, (x, y) => new PatientInfoChangeDto()
            {
                Id = x.Id,
                PI_Id = x.PI_Id,
                CreationTime = x.CreationTime,
                VisitNo = y.VisitNo,
                PatientName = y.PatientName,
                Sex = y.SexName,
                Age = y.Age,
                ChangeField = x.ChangeField,
                ChangeReason = x.ChangeReason,
                BeforeValue = x.BeforeValue,
                AfterValue = x.AfterValue,
                OperatedCode = x.OperatedCode,
                OperatedName = x.OperatedName
            }).WhereIf(!string.IsNullOrEmpty(dto.PatientName), x => x.PatientName.Contains(dto.PatientName))
             .WhereIf(!string.IsNullOrEmpty(dto.VisitNo), x => x.VisitNo == dto.VisitNo)
             .OrderByDescending(x => x.CreationTime)
             .ToListAsync();

            List<PatientInfoChangeDto> patientInfoChangeDtos = new List<PatientInfoChangeDto>();
            patientInfoChangeDtoList.ForEach(x => x.CreateTime = x.CreationTime.ToString("G"));

            var groupList = patientInfoChangeDtoList.GroupBy(x => new { x.PatientName, x.VisitNo, x.CreateTime });
            foreach (var group in groupList)
            {
                PatientInfoChangeDto maxDto = patientInfoChangeDtoList.Where(x => x.PatientName == group.Key.PatientName && x.VisitNo == group.Key.VisitNo).Max();
                PatientInfoChangeDto patientInfoChangeDto = new PatientInfoChangeDto();
                patientInfoChangeDto.Id = maxDto.Id;
                patientInfoChangeDto.PI_Id = maxDto.PI_Id;
                patientInfoChangeDto.CreationTime = maxDto.CreationTime;
                patientInfoChangeDto.VisitNo = maxDto.VisitNo;
                patientInfoChangeDto.PatientName = maxDto.PatientName;
                patientInfoChangeDto.Sex = maxDto.Sex;
                patientInfoChangeDto.Age = maxDto.Age;
                patientInfoChangeDto.ChangeReason = group.ToList().First().ChangeReason;
                patientInfoChangeDto.BeforeValue = string.Join(",", group.ToList().Select(x => x.BeforeValue));
                patientInfoChangeDto.AfterValue = string.Join(",", group.ToList().Select(x => x.AfterValue));
                patientInfoChangeDto.OperatedCode = group.ToList().First().OperatedCode;
                patientInfoChangeDto.OperatedName = group.ToList().First().OperatedName;
                patientInfoChangeDtos.Add(patientInfoChangeDto);
            }

            int totalCount = patientInfoChangeDtos.Count;
            List<PatientInfoChangeDto> pageRecord = patientInfoChangeDtos.Skip((dto.PageIndex - 1) * dto.PageSize)
             .Take(dto.PageSize).ToList();

            var pageList = new PagedResultDto<PatientInfoChangeDto>
            {
                TotalCount = totalCount,
                Items = pageRecord
            };

            return JsonResult<PagedResultDto<PatientInfoChangeDto>>.Ok(data: pageList);
        }


        /// <summary>
        /// 同步病患到CS版急诊分诊
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="registerInfo"></param>
        /// <param name="dicts"></param>
        /// <param name="isSaveGreenChannelRecord">是否需要保存CS版急诊绿通审核纪律</param>
        private async Task SyncPatientToCsEcisAsync(PatientInfo patient, RegisterInfo registerInfo,
            Dictionary<string, List<TriageConfigDto>> dicts, bool isSaveGreenChannelRecord)
        {
            if (Convert.ToBoolean(_configuration["Settings:IsSaveDataToCsEcis"]))
            {
                // 院前分诊修改分诊信息后不同步患者到急诊分诊2.0
                var pv = patient.BuildAdapter().AdaptToType<PatientVisitDto>();

                if (registerInfo != null)
                {
                    //南方医不赋值挂号流水号，急诊系统从平台消息中获取
                    if (Convert.ToBoolean(_configuration["Settings:IsSyncRegisterInfoToECIS1.0"]))
                    {
                        pv.RegisterNo = registerInfo.RegisterNo;
                        pv.RegisterDT = registerInfo.RegisterTime;
                    }

                    pv.VisitID = registerInfo.VisitNo;
                }

                if (pv.RegisterFrom.IsNullOrWhiteSpace() && !patient.ToHospitalWayCode.IsNullOrWhiteSpace())
                {
                    pv.RegisterFrom = dicts.GetNameByDictCode(TriageDict.ToHospitalWay, patient.ToHospitalWayCode);
                }

                pv.ContactPerson = patient.ContactsPerson;
                pv.ContactPhone = patient.ContactsPhone;
                pv.Nation = patient.NationName;
                pv.IsDelete = 1;
                // CS版急诊生成分诊记录
                if (patient.ConsequenceInfo != null)
                {
                    pv.TriageRecord = patient.ConsequenceInfo.BuildAdapter().AdaptToType<TriageRecordDto>();

                    pv.TriageRecord.TriageDepartmentCode = dicts[TriageDict.TriageDepartment.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == patient.ConsequenceInfo.TriageDeptCode)
                        ?.HisConfigCode;

                    pv.TriageRecord.TriageTargetCode = dicts[TriageDict.TriageDirection.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == patient.ConsequenceInfo.TriageTargetCode)
                        ?.HisConfigCode;

                    pv.TriageRecord.TriageDepartment = patient.ConsequenceInfo.TriageDeptName;
                    pv.TriageRecord.TriageTarget = patient.ConsequenceInfo.TriageTargetName;
                    pv.Examination = patient.NarrationName.IsNullOrWhiteSpace()
                        ? patient.NarrationComments
                        : patient.NarrationName + "," + patient.NarrationComments;

                    pv.TriageRecord.Id = patient.ConsequenceInfo.Id;
                    pv.TriageRecord.PVID = pv.PVID;
                    pv.TriageRecord.TriageBy = patient.TriageUserName;
                    pv.TriageRecord.TriageDT = patient.TriageTime;
                    if (patient.StartTriageTime.HasValue)
                        pv.TriageRecord.StartRecordDT = patient.StartTriageTime.Value;
                }

                // CS版急诊评分
                if (pv.ScoreRecords != null && pv.ScoreRecords.Count > 0)
                {
                    pv.TriageRecord.HasScoreRecord =
                        Convert.ToInt32(patient.ScoreInfo != null && patient.ScoreInfo.Count > 0);
                    pv.ScoreRecords = patient.ScoreInfo.BuildAdapter().AdaptToType<List<ScoreRecordDto>>();
                    pv.ScoreRecords.ForEach(x =>
                    {
                        x.TID = pv.TriageRecord.Id;
                        x.Id = x.Id;
                        x.PVID = pv.PVID;
                    });
                }

                // CS版急诊生命体征
                if (patient.VitalSignInfo != null && !patient.VitalSignInfo.CheckVitalSignIsNullOrEmpty())
                {
                    pv.VitalSignRecord = patient.VitalSignInfo.BuildAdapter()
                        .AdaptToType<TriageVitalSignRecordDto>();
                    pv.VitalSignRecord.TID = pv.TriageRecord.Id;
                    pv.VitalSignRecord.PVID = pv.PVID;
                    pv.VitalSignRecord.Id = patient.VitalSignInfo.Id;
                    pv.TriageRecord.HasVitalSign = Convert.ToInt32(patient.VitalSignInfo != null);
                    pv.VitalSignRecord.VitalSignMemo = patient.VitalSignInfo.RemarkName;
                }

                var csEcisSaveTriageDto = new CsEcisSaveTriageDto
                {
                    PvList = new List<PatientVisitDto> { pv },
                    GroupInjury = null
                };

                await CsEcisRepository.SaveEcisTriageRecordAsync(csEcisSaveTriageDto);
                if (isSaveGreenChannelRecord)
                {
                    if (Convert.ToBoolean(_configuration["Settings:GreenChannel:IsEnabled"]))
                    {
                        if (await CsEcisRepository.AutoExamineGreenChannelAsync(new ExamineGreenChannelDto
                        {
                            PatientId = patient.PatientId,
                            PVID = patient.Id,
                            ExamineDoctor = CurrentUser.GetFullName(),
                            ExamineDT = DateTime.Now
                        }))
                        {
                            _log.LogInformation("同步病患到CS版急诊分诊，保存审核绿通记录成功");
                        }
                    }

                    //是否需要让急诊1.0患者自动入科接诊
                    if (Convert.ToBoolean(_configuration["Settings:IsECIS1.0PatientAutoInDept"]))
                    {
                        var inHouseDto = pv.BuildAdapter().AdaptToType<CsEcisPatientInDeptDto>();
                        inHouseDto.Status = 1;
                        inHouseDto.OperatorCode = CurrentUser.UserName;
                        inHouseDto.OperatorName = CurrentUser.GetFullName();
                        switch (inHouseDto.Additional2)
                        {
                            case "Ⅰ 级":
                            case "Ⅱ 级":
                                inHouseDto.WardArea = "红区";
                                break;
                            case "Ⅲ 级":
                                inHouseDto.WardArea = "黄区";
                                break;
                            case "Ⅳ 级":
                            case "Ⅳa 级":
                            case "Ⅳb 级":
                                inHouseDto.WardArea = "绿区";
                                break;
                        }

                        await CsEcisRepository.CreatePatientInHouseAsync(inHouseDto);
                    }
                }
            }
        }

        /// <summary>
        /// 南方医开通绿通挂账
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="dicts"></param>
        /// <param name="oldGreenChannel"></param>
        /// <param name="newGreenChannel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<RegisterInfo> OpenGreenChannelChargeAsync(PatientInfo patient,
            Dictionary<string, List<TriageConfigDto>> dicts, string oldGreenChannel, string newGreenChannel)
        {
            RegisterInfo greenChannelRegisterInfo = null;
            if (!oldGreenChannel.IsNullOrWhiteSpace())
            {
                greenChannelRegisterInfo =
                    await RegisterInfoRepository.FirstOrDefaultAsync(x => x.PatientInfoId == patient.Id);
            }

            // 是否测试开通绿通发送通知消息
            if (oldGreenChannel.IsNullOrWhiteSpace() && Convert.ToBoolean(_configuration["Settings:TestNotify"]))
            {
                var notifyDto = new NotifyDto
                {
                    PatientName = patient.PatientName,
                    PatientId = patient.PatientId,
                    GreenRoadName = patient.GreenRoadName,
                    TriageUserName = patient.TriageUserName,
                    TriageUserDeptName = CurrentUser.GetDeptName(),
                    Gender = patient.SexName,
                    TaskInfoId = patient.TaskInfoId,
                    CarNum = patient.CarNum,
                    OpenGreenRoadTime = DateTime.Now,
                    TaskNo = patient.TaskInfoNum,
                    Age = patient.Age,
                    IsSaveToDB = true
                };

                var redisKey = AppSettings.TriageService + ":GreenRoadPatient:" + patient.TaskInfoId;
                await _redis.HashSetAsync(redisKey, patient.PatientId, JsonHelper.SerializeObject(notifyDto));
                await _redis.KeyExpireAsync(redisKey, TimeSpan.FromHours(5));
                var jr = await ConsulHttpClient.Post(
                    _configuration["ConsulRegistry:ServiceRegistryName:EmrService:Scheme"],
                    _configuration["ConsulRegistry:ServiceRegistryName:EmrService:Name"],
                    "/api/EmrService/notify/sendMq", notifyDto);
                _log.LogInformation("保存分诊调用绿通挂账接口，调用分诊(开绿通)、院前急救创建任务单，出车的的时候，发mq消息给pc端结束，返回结果：{Result}",
                    JsonHelper.SerializeObject(jr));

                greenChannelRegisterInfo = new RegisterInfo
                {
                    PatientInfoId = patient.Id,
                    VisitNo = "1",
                    RegisterNo = DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                    RegisterTime = DateTime.Now
                }.SetId(GuidGenerator.Create());

                await RegisterInfoRepository.InsertAsync(greenChannelRegisterInfo);
                _log.LogInformation("保存分诊调用绿通挂账接口成功");
            }

            return greenChannelRegisterInfo;
        }

        /// <summary>
        /// 演示环境
        /// </summary>
        /// <param name="dicts"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task SendPatientToDemoApi(Dictionary<string, List<TriageConfigDto>> dicts,
            CreateOrUpdatePatientDto dto)
        {
            var sex = "O";
            if (!string.IsNullOrEmpty(dto.Sex))
            {
                sex = dto.Sex switch
                {
                    "男" => "M",
                    "女" => "F",
                    _ => sex
                };
            }

            var uri = _configuration["Demo:DemoApiUrl"];
            var pe = new PatientEvent
            {
                patientEvent = "10",
                patientEventName = "",
                patientId = dto.PatientId,
                patientClass = "O",
                identifyNo = dto.IdentityNo,
                patientName = dto.PatientName,
                sex = sex,
                admitSource = dicts.GetNameByDictCode(TriageDict.ToHospitalWay, dto.ToHospitalWay),
                reAdmissionIndicator = "0"
            };

            var content = new StringContent(JsonSerializer.Serialize(pe));
            await _httpClientHelper.PostAsync(uri, content);
        }


        /// <summary>
        /// 保存挂号信息（挂号 -> 分诊 模式）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<RegisterInfo> SaveRegisterInfoBeforeTriage(CreateOrUpdatePatientDto dto)
        {
            _log.LogTrace("开始查询患者是否存在挂号信息");
            //如果开通绿通则在挂号接口挂号
            if (!dto.GreenRoad.IsNullOrWhiteSpace())
                return await Task.FromResult(new RegisterInfo());
            var patient = dto.BuildAdapter().AdaptToType<PatientInfo>().GetNamePy();
            RegisterInfo registerInfo = await this.RegisterInfoRepository.Where(x => x.PatientInfoId == patient.Id)
                .FirstOrDefaultAsync();
            // 若保存分诊时此患者尚无挂号数据则查询挂号接口获取挂号信息
            if (registerInfo == null)
            {
                _log.LogInformation("患者不存在挂号信息，开始查询挂号信息接口");
                var url = _configuration["HisApiSettings:getRegisterInfoList"] +
                          $"?idNo={dto.IdentityNo}&patientId={patient.PatientId}";
                var response = await HttpClientHelper.GetAsync(url);
                _log.LogInformation("获取挂号信息响应：{Response}", response);
                if (response.IsNullOrWhiteSpace())
                {
                    await CurrentUnitOfWork.RollbackAsync();
                    _log.LogError("分诊确认保存失败！原因：{Msg}", "调用查询挂号信息接口失败！响应为空！");
                    throw new Exception("确认分诊失败！尚未查询到挂号信息，请稍后！");
                }

                var json = JObject.Parse(response);
                if (json["code"]?.ToString() == "0" && json["data"] != null && json["data"].ToString() != "")
                {
                    var respListDto =
                        JsonHelper.DeserializeObject<List<PatientRespDto>>(json["data"].ToString());
                    if (respListDto != null && respListDto.Count > 0)
                    {
                        var lastResp = respListDto.OrderByDescending(o => o.seeDate).First();
                        registerInfo = new RegisterInfo
                        {
                            PatientInfoId = patient.Id,
                            RegisterDeptCode = lastResp.deptId,
                            RegisterNo = lastResp.visitNum,
                            VisitNo = lastResp.visitNo,
                            RegisterTime = Convert.ToDateTime(lastResp.registerDate),
                            RegisterDoctorCode = lastResp.doctorCode,
                            RegisterDoctorName = "",
                            AddUser = lastResp.@operator
                        }.SetId(GuidGenerator.Create());
                        var dbContext = _patientInfoRepository.GetDbContext();
                        patient.RegisterInfo.Add(registerInfo);
                        dbContext.Entry(patient.RegisterInfo).State = EntityState.Added;
                        if (await dbContext.SaveChangesAsync() <= 0)
                        {
                            await CurrentUnitOfWork.RollbackAsync();
                            _log.LogError("分诊确认保存失败！原因：保存挂号信息失败！请检查后重试！");
                            throw new Exception("保存挂号信息失败！请检查后重试！");
                        }

                        _log.LogInformation("分诊确认保存获取挂号信息成功");
                    }
                }
            }

            return registerInfo;
        }


        /// <summary>
        /// 根据输入项获取患者分诊分页信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + PermissionDefinition.List)]
        //  [AllowAnonymous]
        public async Task<JsonResult<PagedResultDto<PatientInfoDto>>> GetPatientInfoListAsync(PatientInfoWhereInput input)
        {
            try
            {
                #region EFCore查询
                // 拿到的配置信息毫无意义，浪费运行时间，注释 -- by：hushitong 2023-07-06
                //var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();

                // 搜索符合条件的患者列表
                var resList = _patientInfoRepository.OrderByDescending(p => p.TriageTime)
                    .Include(c => c.ScoreInfo)
                    .Include(c => c.ConsequenceInfo)
                    .Include(c => c.VitalSignInfo)
                    .Include(c => c.AdmissionInfo)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.PatientSearch), x =>
                        x.PatientName.Contains(input.PatientSearch)
                        || x.CarNum.Contains(input.PatientSearch)
                        || x.Py.Contains(input.PatientSearch)
                        || x.CallingSn.Contains(input.PatientSearch))
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DirectionCode),
                        x => x.ConsequenceInfo.TriageTargetCode == input.DirectionCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageUserCode),
                        x => input.TriageUserCode == x.TriageUserCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.LevelAreaCode),
                        x => input.LevelAreaCode.Contains(x.ConsequenceInfo.ActTriageLevel) &&
                             !string.IsNullOrEmpty(x.ConsequenceInfo.ActTriageLevel))
                    .WhereIf(!string.IsNullOrWhiteSpace(input.LevelCode),
                        x => input.LevelCode == x.ConsequenceInfo.ActTriageLevel)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.ToHospitalWayCode),
                        x => input.ToHospitalWayCode == x.ToHospitalWayCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.ToHospitalWayFrom120Code),
                        x => input.ToHospitalWayFrom120Code.Contains(x.ToHospitalWayCode) &&
                             !string.IsNullOrEmpty(x.ToHospitalWayCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DeptCode),
                        x => x.ConsequenceInfo.TriageDeptCode == input.DeptCode)
                    .WhereIf(!string.IsNullOrEmpty(input.NarrationCode), x => x.Narration.Contains(input.NarrationCode))
                    .WhereIf(!string.IsNullOrEmpty(input.NarrationName), x => x.NarrationName.Contains(input.NarrationName))
                    .WhereIf(!string.IsNullOrEmpty(input.GreenRoadCode),
                        x => x.GreenRoadCode.Contains(input.GreenRoadCode))
                    .WhereIf(!string.IsNullOrEmpty(input.IdentityCode), x => x.Identity == input.IdentityCode)
                    .WhereIf(!string.IsNullOrEmpty(input.ChargeTypeCode), x => x.ChargeType == input.ChargeTypeCode)
                    .WhereIf(!string.IsNullOrEmpty(input.DiseaseCode), x => x.DiseaseCode == input.DiseaseCode)
                    // 院前标识发热，或者体温超过 37.2℃
                    .WhereIf(!string.IsNullOrEmpty(input.IsHot),
                        x => x.AdmissionInfo.IsHot == input.IsHot ||
                             Convert.ToDouble(x.VitalSignInfo.Temp ?? "0") >= 37.3)
                    .WhereIf(input.TriageStatus != -1, x => x.TriageStatus == input.TriageStatus)
                    .WhereIf(!string.IsNullOrEmpty(input.GroupInjuryCode), x => x.GroupInjuryInfoId != Guid.Empty)
                    .WhereIf(
                        input.StartTime != null && input.EndTime != null && input.StartTime < input.EndTime &&
                        input.TriageStatus != 0,
                        t => t.TriageTime >= input.StartTime && t.TriageTime <= input.EndTime)
                    .WhereIf(
                        input.StartTime != null && input.EndTime != null && input.StartTime < input.EndTime &&
                        input.TriageStatus == 0,
                        t => t.CreationTime >= input.StartTime && t.CreationTime <= input.EndTime)
                    .WhereIf(!string.IsNullOrEmpty(input.CarNum), x => input.CarNum.Contains(x.CarNum))
                    // 评分主诉查询
                    .WhereIf(!string.IsNullOrEmpty(input.JudgmentMasterId),
                        t => t.ScoreInfo.Any(x => x.ScoreContent.Contains(input.JudgmentMasterId)))
                    .WhereIf(!string.IsNullOrEmpty(input.CallingSn), _ => _.CallingSn.Contains(input.CallingSn))
                    .Where(x => !x.IsCancelled)//分诊记录不显示作废的患者
                    ;
                #endregion

                List<GroupInjurySimpleInfoDto> groupInjuries;
                int totalCount;
                List<PatientInfo> res;
                if (string.IsNullOrWhiteSpace(input.GroupInjuryCode))
                {
                    //获取分页总行数
                    totalCount = await resList.CountAsync();
                    res = await resList
                        .OrderByDescending(p => p.TriageTime)
                        .Skip((input.SkipCount - 1) * input.MaxResultCount)
                        .Take(input.MaxResultCount)
                        .ToListAsync();

                    #region Lambda表达式拼接查询群伤事件

                    Expression<Func<GroupInjuryInfo, bool>> first = x => false;
                    var param = Expression.Parameter(typeof(GroupInjuryInfo), "x");
                    Expression body = Expression.Invoke(first, param);
                    body = res.Where(x => x.GroupInjuryInfoId != Guid.Empty)
                        .Select(s => (Expression<Func<GroupInjuryInfo, bool>>)(x => x.Id == s.GroupInjuryInfoId))
                        .Aggregate(body,
                            (current, expression) => Expression.OrElse(current, Expression.Invoke(expression, param)));
                    var lambda = Expression.Lambda<Func<GroupInjuryInfo, bool>>(body, param);
                    groupInjuries = await GroupInjuryRepository.Where(lambda).Select(p => new GroupInjurySimpleInfoDto()
                    { Id = p.Id, GroupInjuryName = p.GroupInjuryName })
                        .ToListAsync();

                    #endregion
                }
                else
                {
                    groupInjuries = await GroupInjuryRepository.Where(t => t.GroupInjuryCode == input.GroupInjuryCode)
                        .Select(p => new GroupInjurySimpleInfoDto() { Id = p.Id, GroupInjuryName = p.GroupInjuryName })
                        .ToListAsync();
                    resList = resList.Where(t => groupInjuries.Select(s => s.Id).Contains(t.GroupInjuryInfoId));
                    //获取分页总行数
                    totalCount = await resList.CountAsync();
                    res = await resList
                        .OrderByDescending(p => p.TriageTime)
                        .Skip((input.SkipCount - 1) * input.MaxResultCount)
                        .Take(input.MaxResultCount)
                        .ToListAsync();
                }

                //var patientInfoIds = string.Join(",", res.Select(s => s.Id).ToList());
                //var registerList = await RegisterInfoRepository
                //    .Where(t => !t.IsCancelled)
                //    .Where(t => patientInfoIds.Contains(t.PatientInfoId.ToString()))
                //    .ToListAsync();
                // 优化查询速度  by: ywlin 2022-03-30
                var registerList = await RegisterInfoRepository
                    .Where(t => !t.IsCancelled)
                    .Where(t => res.Select(y => y.Id).Contains(t.PatientInfoId))
                    // 需要注意这里new了，需要添加自己需要的字段
                    .Select(p => new RegisterInfo() { PatientInfoId = p.PatientInfoId, RegisterNo = p.RegisterNo, RegisterTime = p.RegisterTime })
                    .ToListAsync();

                List<PatientInfoDto> items = res.BuildAdapter().AdaptToType<List<PatientInfoDto>>();

                //if (dicts.Count > 0)
                {
                    items.ForEach(patientInfoDto =>
                    {
                        if (patientInfoDto.ConsequenceInfo != null)
                        {
                            if (!string.IsNullOrEmpty(patientInfoDto.ConsequenceInfo.TriageDept))
                            {
                                patientInfoDto.ConsequenceInfo.TriageDept = patientInfoDto.ConsequenceInfo.TriageDeptName;
                            }

                            if (!string.IsNullOrEmpty(patientInfoDto.ConsequenceInfo.TriageTarget))
                            {
                                patientInfoDto.ConsequenceInfo.TriageTarget = patientInfoDto.ConsequenceInfo.TriageTargetName;
                            }

                            if (!string.IsNullOrEmpty(patientInfoDto.ConsequenceInfo.AutoTriageLevel))
                            {
                                patientInfoDto.ConsequenceInfo.AutoTriageLevel = patientInfoDto.ConsequenceInfo.AutoTriageLevelName;
                            }

                            if (!string.IsNullOrEmpty(patientInfoDto.ConsequenceInfo.ActTriageLevel))
                            {
                                patientInfoDto.ConsequenceInfo.ActTriageLevel = patientInfoDto.ConsequenceInfo.ActTriageLevelName;
                            }
                        }

                        if (!string.IsNullOrEmpty(patientInfoDto.ToHospitalWay))
                        {
                            patientInfoDto.ToHospitalWay = patientInfoDto.ToHospitalWayName;
                        }

                        if (!string.IsNullOrEmpty(patientInfoDto.Nation))
                        {
                            patientInfoDto.Nation = patientInfoDto.NationName;
                        }

                        // 添加sexName字段，去除此处容易引起歧义的转换  by：ywlin 2022-03-17
                        //if (!string.IsNullOrEmpty(t.Sex))
                        //{
                        //    t.Sex = t.SexName;
                        //}

                        if (!string.IsNullOrEmpty(patientInfoDto.Consciousness))
                        {
                            patientInfoDto.Consciousness = patientInfoDto.ConsciousnessName;
                        }

                        if (!string.IsNullOrEmpty(patientInfoDto.ChargeType))
                        {
                            patientInfoDto.ChargeType = patientInfoDto.ChargeTypeName;
                        }

                        if (!string.IsNullOrEmpty(patientInfoDto.DiseaseCode))
                        {
                            patientInfoDto.DiseaseCode = patientInfoDto.DiseaseName;
                        }

                        if (!string.IsNullOrEmpty(patientInfoDto.TypeOfVisitCode))
                        {
                            patientInfoDto.TypeOfVisitCode = patientInfoDto.TypeOfVisitName;
                        }

                        if (!string.IsNullOrEmpty(patientInfoDto.Narration))
                        {
                            patientInfoDto.Narration = patientInfoDto.NarrationName;
                        }

                        if (!string.IsNullOrEmpty(patientInfoDto.GreenRoad))
                        {
                            patientInfoDto.GreenRoad = patientInfoDto.GreenRoadName;
                        }

                        // 使用GroupInjuryName接收群伤事件Id，需判断是狗为空Guid;为空赋默认值无，否则转换为对应群伤事件类型名称
                        if (Guid.Parse(patientInfoDto.GroupInjuryName) != Guid.Empty)
                        {
                            var groupInjury = groupInjuries.FirstOrDefault(x => x.Id == Guid.Parse(patientInfoDto.GroupInjuryName));
                            if (groupInjury != null)
                            {
                                patientInfoDto.GroupInjuryName = groupInjury.GroupInjuryName;
                            }
                        }
                        else
                        {
                            patientInfoDto.GroupInjuryName = "";
                        }

                        var entity = registerList.FirstOrDefault(registerInfo => registerInfo.PatientInfoId == patientInfoDto.TriagePatientInfoId);
                        patientInfoDto.RegisterNo = entity?.RegisterNo;
                        patientInfoDto.RegisterTime = entity?.RegisterTime;

                        if (patientInfoDto.VitalSignInfo != null)
                        {
                            patientInfoDto.VitalSignInfo.Remark = patientInfoDto.VitalSignInfo.RemarkName;
                        }
                    });

                    var pageList = new PagedResultDto<PatientInfoDto>
                    {
                        TotalCount = totalCount,
                        Items = items.OrderByDescending(o => o.TriageTime).ToList()
                    };

                    return JsonResult<PagedResultDto<PatientInfoDto>>.Ok(data: pageList);
                }

                //_log.LogError("根据输入项获取患者建档分页信息结束！原因：{Msg}", "获取缓存数据失败");
                //return JsonResult<PagedResultDto<PatientInfoDto>>.Fail("获取缓存数据失败");
            }
            catch (Exception e)
            {
                _log.LogError("根据输入项获取患者分诊分页信息错误！原因：{Msg}", e);
                return JsonResult<PagedResultDto<PatientInfoDto>>.Fail(e.Message);
            }
        }


        /// <summary>
        /// 获得分诊记录信息列表
        /// 分诊记录 Tab 主方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + "PatientInfoBySettingList", "获得分诊记录信息列表")]
        public async Task<JsonResult<PagedResultDto<PatientInfoExportExcelDto>>> GetPatientInfoBySettingListAsync(PatientInfoWhereInput input)
        {
            try
            {
                _log.LogInformation("【PatientInfoService】【GetPatientInfoBySettingListAsync】【获得分诊记录信息列表开始】");

                JsonResult<PagedResultDto<PatientInfoDto>> list = await GetPatientInfoListAsync(input);
                var pageList = new PagedResultDto<PatientInfoExportExcelDto>
                {
                    TotalCount = 0,
                    Items = new List<PatientInfoExportExcelDto>()
                };

                if (list.Code == 200)
                {
                    List<PatientInfoExportExcelDto> resultList = list.Data.Items.BuildAdapter().AdaptToType<List<PatientInfoExportExcelDto>>();
                    pageList.TotalCount = list.Data.TotalCount;
                    pageList.Items = resultList;
                }

                _log.LogInformation("获得分诊记录信息列表成功");
                return JsonResult<PagedResultDto<PatientInfoExportExcelDto>>.Ok(data: pageList);
            }
            catch (Exception e)
            {
                _log.LogError("获得分诊记录信息列表错误！原因：{Msg}", e);
                return JsonResult<PagedResultDto<PatientInfoExportExcelDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据输入项获取患者状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<List<TriagePatientInfoDto>>> GetPatientStatusAsync(PatientStatusInput input)
        {
            try
            {
                _log.LogInformation("根据输入项获取患者状态开始");

                var ids = input.TaskInfoIds?.Split(',').Select(Guid.Parse).ToList();
                var identityNos = input.IdentityNos?.Split(',').ToList();
                var list = await _patientInfoRepository.Include(c => c.RegisterInfo)
                    .WhereIf(!input.TaskInfoIds.IsNullOrWhiteSpace(), x => ids.Contains(x.TaskInfoId))
                    .WhereIf(!input.IdentityNos.IsNullOrWhiteSpace(), x => identityNos.Contains(x.IdentityNo))
                    .WhereIf(Guid.Empty != input.TaskInfoId,
                        x => x.TaskInfoId == input.TaskInfoId)
                    .WhereIf(Guid.Empty != input.TriagePatientInfoId,
                        x => x.Id == input.TriagePatientInfoId)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.CarNum),
                        x => x.CarNum == input.CarNum)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.PatientId),
                        x => x.PatientId == input.PatientId)
                    .Select(s => new
                    {
                        s.TaskInfoId,
                        s.PatientId,
                        TriagePatientInfoId = s.Id,
                        s.RegisterInfo,
                        s.GreenRoadCode,
                        s.GreenRoadName,
                        s.GroupInjuryInfoId,
                        TriageLevel = s.ConsequenceInfo.ActTriageLevelName,
                        ToHospitalWay = s.ToHospitalWayName,
                        IsTriaged = 1,
                        s.Age,
                        s.Birthday,
                        s.SexName,
                        s.RFID,
                        s.OnsetTime,
                        s.Address,
                        s.ContactsPhone
                    })
                    .ToListAsync();

                var res = list.Select(item => new TriagePatientInfoDto
                {
                    TriagePatientInfoId = item.TriagePatientInfoId,
                    TaskInfoId = item.TaskInfoId,
                    PatientId = item.PatientId,
                    IsRegister = Convert.ToInt32(item.RegisterInfo != null && item.RegisterInfo.Count > 0),
                    IsGreenRode = Convert.ToInt32(!item.GreenRoadCode.IsNullOrWhiteSpace()),
                    IsGroupInjury = Convert.ToInt32(item.GroupInjuryInfoId != Guid.Empty),
                    IsTriaged = 1,
                    TriageLevel = item.TriageLevel,
                    ToHospitalWay = item.ToHospitalWay,
                    GreenRoadName = item.GreenRoadName,
                    Birthday = item.Birthday,
                    Gender = item.SexName,
                    Age = item.Birthday != null ? item.Birthday.Value.GetAgeString() : item.Age,
                    RFID = item.RFID,
                    OnsetTime = item.OnsetTime,
                    Address = item.Address,
                    ContactsPhone = item.ContactsPhone
                })
                    .ToList();


                _log.LogInformation("根据输入项获取患者状态成功");
                return JsonResult<List<TriagePatientInfoDto>>.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogError("根据输入项获取患者状态错误！原因：{Msg}", e);
                return JsonResult<List<TriagePatientInfoDto>>.Fail(e.Message);
            }
        }


        /// <summary>
        /// 六大中心获取生命体征信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<JsonResult<VitalSignInfoToSixCenterDto>> GetVitalSignInfoToSixCenterAsync(Guid pid)
        {
            try
            {
                var patient = await _patientInfoRepository.Include(i => i.VitalSignInfo)
                    .FirstOrDefaultAsync(f => f.Id == pid);
                if (patient == null)
                {
                    _log.LogError("六大中心获取生命体征信息错误！原因：{Msg}", "患者不存在");
                    return JsonResult<VitalSignInfoToSixCenterDto>.Fail(data: "患者不存在");
                }

                var vital = patient.VitalSignInfo.BuildAdapter().AdaptToType<VitalSignInfoToSixCenterDto>();
                _log.LogInformation("根据输入项获取患者状态成功");
                return JsonResult<VitalSignInfoToSixCenterDto>.Ok(data: vital);
            }
            catch (Exception e)
            {
                _log.LogError("六大中心获取生命体征信息错误！原因：{Msg}", e);
                return JsonResult<VitalSignInfoToSixCenterDto>.Fail(data: e.Message);
            }
        }


        /// <summary>
        /// 急诊根据id获取患者信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<JsonResult<PatientInfoDto>> GetPatientInfoByEcisAsync(Guid pid)
        {
            var res = _patientInfoRepository.Include(c => c.ScoreInfo)
                .Include(c => c.ConsequenceInfo)
                .Include(c => c.VitalSignInfo)
                .FirstOrDefaultAsync(f => f.Id == pid).Result;
            var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
            if (res != null)
            {
                res.VitalSignInfo.Remark =
                    dicts.GetNameByDictCode(TriageDict.VitalSignRemark, res.VitalSignInfo.Remark);
                res.Consciousness = dicts.GetNameByDictCode(TriageDict.Mind, res.Consciousness);
            }

            var patient = res.BuildAdapter().AdaptToType<PatientInfoDto>();
            return JsonResult<PatientInfoDto>.Ok(data: patient);
        }

        /// <summary>
        /// 院前分诊获取生命体征
        /// </summary>
        /// <param name="patientInfoId">患者Id</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Auth("PatientInfo" + PermissionDefinition.Separator + "VitalSignFromIotServer", "院前分诊获取生命体征")]
        public async Task<JsonResult<IotDataDto>> GetVitalSignFromIotServerAsync(Guid patientInfoId)
        {
            try
            {
                _log.LogTrace("院前分诊获取生命体征开始");
                //InfluxDB时间与服务器时间差八小时三分钟
                var queryTime = DateTime.Now.AddHours(-8).AddMinutes(-3).AddMinutes(-1)
                    .ToString("yyyy-MM-ddTHH:mm:00.000Z");
                //select * from iot_signs_data where iot_device_id='6b32a2e0-c16b-4ef4-b582-8c2cbd0791f7' and time>='2021-11-11T12:25:00.000Z' and time <='2021-11-11T12:27:00.000Z' and signs_data=~/NIBP_PR/
                //获取监护仪体温数据

                var jr = await ConsulHttpClient.GetAsync(
                    _configuration["ConsulRegistry:ServiceRegistryName:EmrService:Scheme"],
                    _configuration["ConsulRegistry:ServiceRegistryName:EmrService:Name"],
                    $"/api/EmrService/monitor/bindDeviceRecords?Pid={patientInfoId}&DeviceType=0&IsDeleted=0");
                if (jr == null)
                {
                    _log.LogError("院前分诊获取生命体征失败，原因：该患者尚未绑定监护仪");
                    return JsonResult<IotDataDto>.Fail("该患者尚未绑定监护仪");
                }

                var deviceJson = (JArray)JArray.Parse(jr.Data.ToString());

                var iotDeviceId = "";
                if (!string.IsNullOrWhiteSpace(deviceJson[0]["deviceInfo"]?.ToString()))
                {
                    iotDeviceId = deviceJson[0]["deviceInfo"]?.ToString();
                }
                else
                {
                    iotDeviceId = deviceJson[0]["iotDeviceId"]?.ToString();
                }

                var url = _configuration["IotServerSettings:IotDeviceUrl"] +
                          $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{iotDeviceId}' " +
                          $" and signs_data=~/{IotCategory.Temp}/ " +
                          $" and time>='{queryTime}' " +
                          $" order by time desc limit 1";
                var result = await HttpClientHelper.GetAsync(url);
                var temp = new ObservationData();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var json = JObject.Parse(result);
                    if (json["code"]?.ToString() == "200" && json["data"] != null &&
                        !json["data"].ToString().IsNullOrWhiteSpace())
                    {
                        var iotData = JsonHelper.DeserializeObject<IotDataDto>(json["data"]?.ToString())
                            ?.signs_data;
                        temp = iotData?.ObservationDatas?.FirstOrDefault(x => x.Category == IotCategory.Temp);
                    }
                }

                //获取监护仪收缩压数据
                url = _configuration["IotServerSettings:IotDeviceUrl"] +
                      $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{iotDeviceId}' " +
                      $" and signs_data=~/{IotCategory.Sbp}/" +
                      $" and time>='{queryTime}' " +
                      $" order by time desc limit 1";
                result = await HttpClientHelper.GetAsync(url);
                var sbp = new ObservationData();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var json = JObject.Parse(result);
                    if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                    {
                        var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                        sbp = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                            ?.FirstOrDefault(x => x.Category == IotCategory.Sbp);
                    }
                }

                //获取监护仪舒张压数据
                url = _configuration["IotServerSettings:IotDeviceUrl"] +
                      $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{iotDeviceId}' " +
                      $" and signs_data=~/{IotCategory.Sdp}/" +
                      $" and time>='{queryTime}' " +
                      $" order by time desc limit 1";
                result = await HttpClientHelper.GetAsync(url);
                var sdp = new ObservationData();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var json = JObject.Parse(result);
                    if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                    {
                        var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                        sdp = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                            ?.FirstOrDefault(x => x.Category == IotCategory.Sdp);
                    }
                }

                //获取监护仪血氧饱和度数据
                url = _configuration["IotServerSettings:IotDeviceUrl"] +
                      $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{iotDeviceId}' " +
                      $" and signs_data=~/{IotCategory.SpO2}/" +
                      $" and time>='{queryTime}' " +
                      $" order by time desc limit 1";
                result = await HttpClientHelper.GetAsync(url);
                var spo2 = new ObservationData();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var json = JObject.Parse(result);
                    if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                    {
                        var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                        spo2 = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                            ?.FirstOrDefault(x => x.Category == IotCategory.SpO2);
                    }
                }

                //获取监护仪血氧饱和度数据
                url = _configuration["IotServerSettings:IotDeviceUrl"] +
                      $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{iotDeviceId}' " +
                      $" and signs_data=~/{IotCategory.BreathRate}/" +
                      $" and time>='{queryTime}' " +
                      $" order by time desc limit 1";
                result = await HttpClientHelper.GetAsync(url);
                var breathRate = new ObservationData();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var json = JObject.Parse(result);
                    if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                    {
                        var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                        breathRate = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                            ?.FirstOrDefault(x => x.Category == IotCategory.BreathRate);
                    }
                }

                //获取监护仪血氧饱和度数据
                url = _configuration["IotServerSettings:IotDeviceUrl"] +
                      $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{iotDeviceId}' " +
                      $" and signs_data=~/{IotCategory.HeartRate}/" +
                      $" and time>='{queryTime}' " +
                      $" order by time desc limit 1";
                result = await HttpClientHelper.GetAsync(url);
                var heartRate = new ObservationData();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var json = JObject.Parse(result);
                    if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                    {
                        var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                        heartRate = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                            ?.FirstOrDefault(x => x.Category == IotCategory.HeartRate);
                    }
                }

                //获取监护仪无创平均血压数据
                url = _configuration["IotServerSettings:IotDeviceUrl"] +
                      $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{iotDeviceId}' " +
                      $" and signs_data=~/{IotCategory.NIBP_MAP}/" +
                      $" and time>='{queryTime}' " +
                      $" order by time desc limit 1";
                result = await HttpClientHelper.GetAsync(url);
                var nibpMap = new ObservationData();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var json = JObject.Parse(result);
                    if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                    {
                        var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                        nibpMap = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                            ?.FirstOrDefault(x => x.Category == IotCategory.NIBP_MAP);
                    }
                }

                var dto = new IotDataDto
                {
                    signs_data = new IotSignsDataToInflux
                    {
                        ObservationDatas = new List<ObservationData>
                        {
                            sbp,
                            sdp,
                            spo2,
                            temp,
                            heartRate,
                            breathRate,
                            nibpMap
                        }
                    }
                };

                _log.LogInformation("院前分诊获取生命体征");
                return JsonResult<IotDataDto>.Ok(data: dto);
            }
            catch (Exception e)
            {
                _log.LogError("根据车牌号获取患者生命体征数据错误！原因：{Msg}", e);
                return JsonResult<IotDataDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 查询患者评分
        /// </summary>
        /// <param name="triagePatientInfoId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Auth("PatientInfo" + PermissionDefinition.Separator + "Score", "查询患者评分")]
        public async Task<JsonResult<IEnumerable<ScoreDto>>> GetScoreAsync(Guid triagePatientInfoId)
        {
            var res = new List<ScoreDto>();
            try
            {
                _log.LogInformation("查询患者评分开始");
                _log.LogInformation("开始获取启用的评分项");
                var jr = await ScoreManageAppService.GetScoreManageListAsync(new ScoreManageWhereInput
                {
                    IsEnable = "true"
                });

                if (jr.Code == 200 && jr.Data != null)
                {
                    res = jr.Data.GroupBy(g => g.ScoreType).Select(s => new ScoreDto
                    {
                        ScoreName = s.FirstOrDefault()?.ScoreName,
                        ScoreType = s.Key,
                    }).ToList();

                    var patientScores = await ScoreInfoRepository.Where(x => x.PatientInfoId == triagePatientInfoId)
                        .Select(s => new
                        {
                            s.ScoreType,
                            s.ScoreValue
                        }).ToListAsync();

                    _log.LogInformation("根据评分类型赋值评分值");
                    foreach (var scoreDto in res)
                    {
                        var score = patientScores.FirstOrDefault(x => x.ScoreType == scoreDto.ScoreType);
                        if (score != null)
                        {
                            scoreDto.ScoreValue = score.ScoreValue;
                        }
                    }
                }

                _log.LogInformation("查询患者评分成功！");
                return JsonResult<IEnumerable<ScoreDto>>.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogError("查询患者评分错误！原因：{Msg}", e);
                return JsonResult<IEnumerable<ScoreDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 获取历史主诉
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<IEnumerable<NarrationDto>>> GetHistoryNarrations(string patientId)
        {
            var historyInfos = await this._patientInfoRepository.AsQueryable()
                .Where(x => x.PatientId == patientId)
                .Where(x => !string.IsNullOrEmpty(x.NarrationName) || !string.IsNullOrEmpty(x.NarrationComments))
                .ToListAsync();
            var narrationList = historyInfos.BuildAdapter().AdaptToType<IEnumerable<NarrationDto>>();

            return JsonResult<IEnumerable<NarrationDto>>.Ok(data: narrationList);
        }

        /// <summary>
        /// 删除（仅测试阶段使用）
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<JsonResult> DeletePatientInfo(Guid pid)
        {
            await this._patientInfoRepository.DeleteAsync(x => x.Id == pid);

            return JsonResult.Ok("删除成功.");
        }

        /// <summary>
        /// 暂停/开启 叫号
        /// 目前只有北大要求
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="isSuspend">是否暂停（0：暂停，1：开启）</param>
        /// <returns></returns>
        public async Task<JsonResult> SuspendAsync(Guid pid, bool isSuspend)
        {
            _log.LogInformation($"请求 暂停/开启 患者ID：{pid}，是否暂停：{(isSuspend == false ? "暂停" : "开启")}");
            var patientInfo = await this._patientInfoRepository.FirstOrDefaultAsync(x => x.Id == pid);
            if (patientInfo == null)
            {
                return JsonResult.Fail("患者信息不存在");
            }

            if (patientInfo.VisitStatus == VisitStatus.NotTriageYet)
            {
                return JsonResult.Fail("该患者尚未分诊，无法暂停");
            }

            if (patientInfo.VisitStatus == VisitStatus.Treating || patientInfo.VisitStatus == VisitStatus.Treated)
            {
                return JsonResult.Fail("该患者尚已开始就诊，无法暂停");
            }

            using var uow = this.UnitOfWorkManager.Begin();
            try
            {
                // 暂停状态
                patientInfo.VisitStatus = isSuspend ? VisitStatus.Suspend : VisitStatus.WattingTreat;
                await this._patientInfoRepository.UpdateAsync(patientInfo);

                if (_configuration["PekingUniversity:SuspendVersion"].ParseToInt() == 2)
                    await _hisApi.SuspendCallingV2(patientInfo.PatientId, isSuspend);
                await _hisApi.SuspendCalling(patientInfo.PatientId, isSuspend);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                return JsonResult.Fail(ex.Message);
            }

            return JsonResult.Ok(isSuspend ? "暂停成功." : "开启成功.");
        }


        /// <summary>
        /// Api同步病患
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<JsonResult> SyncPatientFromPreHospitalAsync(PatientInfoMqDto dto,
            CancellationToken cancellationToken = default)
        {
            return await HandlerPatientMqDtoAsync(dto);
        }

        /// <summary>
        /// 通过证件类型查找新冠问卷
        /// </summary>
        /// <param name="idTypeCode">证件类型编码</param>
        /// <param name="idCardNo">证件号码</param>
        /// <param name="barcode">二维码内容</param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<object>> GetQuestionnaireAsync(string idTypeCode, string idCardNo, string barcode)
        {
            var hisCode = _configuration.GetValue<string>("HospitalCode");
            if (hisCode == "Longgang")
            {
                try
                {
                    var hisApi = _serviceProvider.GetService<LonggangHisApi>();
                    if (!string.IsNullOrEmpty(barcode))
                    {
                        // 二维码扫码
                        var questionnarie = await hisApi.GetQuestionnaireAsync(barcode);
                        return JsonResult<object>.Ok(data: questionnarie);
                    }
                    else
                    {
                        // 证件号码查询
                        if (string.IsNullOrEmpty(idTypeCode))
                        {
                            return JsonResult<object>.Fail("证件类型不能为空");
                        }

                        if (string.IsNullOrEmpty(idCardNo))
                        {
                            return JsonResult<object>.Fail("证件号码不能为空");
                        }

                        // 证件类型，获取到HIS对应的类型（01 居民身份证  03 护照  04 军官证  06 港澳居民来往内地通行证  07 台湾居民来往内地通行证）
                        var idType = await _triageConfigRepository.AsNoTracking().OrderBy(x => x.Sort)
                            .FirstOrDefaultAsync(x =>
                                x.TriageConfigType == (int)TriageDict.IdType && x.TriageConfigCode == idTypeCode);
                        string showType = idType.HisConfigCode switch
                        {
                            "03" => "1", // 护照
                            "111" => "2", // 出生医学证明 
                            "04" => "3", // 军官证
                            "06" => "4", // 港澳台身份证
                                         //"06" => "5", // 港澳台居民居住证
                                         //"06" => "6", // 港澳人员回乡证
                                         //"06" => "7", // 港澳居民来往内地通行证
                            "999" => "8", // 其他
                            "01" => "9", // 身份证
                                         //"" => "10", // 门诊号/住院号
                                         //"" => "11", // 体检号码
                                         //"" => "12", // Empi号，患者唯一号
                            _ => "",
                        };
                        var questionnarie = await hisApi.GetQuestionnaireAsync(showType, idCardNo);

                        return JsonResult<object>.Ok(data: questionnarie);
                    }
                }
                catch (Exception ex)
                {
                    return JsonResult<object>.Fail(ex.Message);
                }
            }

            if (hisCode == "Mock")
            {
                // Mock 数据
                var path = Path.Combine(Directory.GetCurrentDirectory(), "mockQuestionnaire.json");
                using StreamReader sr = File.OpenText(path);
                using JsonReader jr = new JsonTextReader(sr);
                JObject o = (JObject)JToken.ReadFrom(jr);
                var result = o["data"]?.ToString();

                return JsonResult<object>.Ok(data: result);
            }

            return JsonResult<object>.Fail(msg: "暂无对接流调表");
        }

        /// <summary>
        /// 队列同步病患
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="capHeader"></param>
        [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe("patient.from.preHospital.triage")]
        public async Task SyncPatientFromRabbitMqAsync(PatientInfoMqDto dto, [FromCap] CapHeader capHeader)
        {
            using var uow = UnitOfWork.Begin();
            try
            {
                //急诊队列才接收此消息
                if (capHeader["AppName"] == _configuration["ApplicationName"])
                {
                    _log.LogInformation("{Application} 不接收此条队列消息", _configuration["ApplicationName"]);
                    return;
                }

                _log.LogInformation("接收到同步病患队列消息");
                //院前分诊患者，需要加标识
                dto.PatientInfo.IsFirstAid = true;
                dto.PatientInfo.VisitStatus = VisitStatus.WattingTreat;
                var result = await HandlerPatientMqDtoAsync(dto);
                if (result.Code != (int)HttpStatusCode.OK)
                {
                    throw new Exception(message: result.Msg);
                }

                await uow.CompleteAsync();
            }
            catch (Exception e)
            {
                _log.LogError("接收院前分诊患者错误！原因：{Msg}", e);
                await uow.RollbackAsync();
                throw e;
            }
        }


        /// <summary>
        /// 同步病患数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [UnitOfWork]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<JsonResult> HandlerPatientMqDtoAsync(PatientInfoMqDto dto)
        {
            try
            {
                var oldPatient = await _patientInfoRepository.Include(c => c.AdmissionInfo)
                    .Include(c => c.VitalSignInfo)
                    .Include(c => c.ScoreInfo)
                    .Include(c => c.ConsequenceInfo)
                    .Include(c => c.RegisterInfo)
                    .Include(c => c.Inform)
                    .FirstOrDefaultAsync(x => x.Id == dto.PatientInfo.TriagePatientInfoId && x.IsFirstAid);

                if (oldPatient == null)
                {
                    var patient = dto.PatientInfo.BuildAdapter().AdaptToType<PatientInfo>();
                    patient.SetId(dto.PatientInfo.TriagePatientInfoId);
                    if (dto.RegisterInfo != null)
                    {
                        patient.RegisterInfo = new List<RegisterInfo>
                        {
                            dto.RegisterInfo.BuildAdapter().AdaptToType<RegisterInfo>().SetId(GuidGenerator.Create())
                        };
                        //如果院前已挂号，默认将就诊状态设置为待就诊
                        patient.VisitStatus = VisitStatus.WattingTreat;
                    }

                    var groupInjury = dto.GroupInjuryInfo.BuildAdapter().AdaptToType<GroupInjuryInfo>();
                    if (groupInjury != null)
                    {
                        await GroupInjuryRepository.InsertAsync(groupInjury);
                    }
                    var informPatInfo = dto.Inform.BuildAdapter().AdaptToType<InformPatInfo>();

                    if (informPatInfo != null)
                    {
                        informPatInfo.PatientInfoId = dto.PatientInfo.TriagePatientInfoId;
                        patient.Inform = informPatInfo;
                    }

                    await _patientInfoRepository.InsertAsync(patient);
                }
                else
                {
                    var patient = dto.PatientInfo.BuildAdapter().AdaptToType<PatientInfo>();

                    patient.SetId(dto.PatientInfo.TriagePatientInfoId);

                    if (patient.ConsequenceInfo != null) patient.ConsequenceInfo.PatientInfoId = dto.PatientInfo.TriagePatientInfoId;
                    if (patient.AdmissionInfo != null) patient.AdmissionInfo.PatientInfoId = dto.PatientInfo.TriagePatientInfoId;

                    var consequenceInfo = dto.ConsequenceInfo.BuildAdapter().AdaptToType<ConsequenceInfo>();
                    if (consequenceInfo != null)
                    {
                        consequenceInfo.PatientInfoId = dto.PatientInfo.TriagePatientInfoId;
                        //院前过来的医生不是就诊医生，默认为空
                        consequenceInfo.DoctorCode = "";
                        consequenceInfo.DoctorName = "";
                        patient.ConsequenceInfo = consequenceInfo;
                    }

                    var informPatInfo = dto.Inform.BuildAdapter().AdaptToType<InformPatInfo>();
                    if (informPatInfo != null)
                    {
                        informPatInfo.PatientInfoId = dto.PatientInfo.TriagePatientInfoId;
                        patient.Inform = informPatInfo;
                    }
                    if (patient.ScoreInfo != null && patient.ScoreInfo.Count > 0)
                    {
                        foreach (var scoreInfo in patient.ScoreInfo)
                        {
                            scoreInfo.PatientInfoId = dto.PatientInfo.TriagePatientInfoId;
                        }
                    }
                    if (dto.RegisterInfo != null)
                    {
                        patient.RegisterInfo = new List<RegisterInfo>
                        {
                            dto.RegisterInfo.BuildAdapter().AdaptToType<RegisterInfo>()
                        };
                        //如果院前已挂号，默认将ecis就诊状态设置为待就诊
                        patient.VisitStatus = patient.VisitStatus == 0 ? VisitStatus.WattingTreat : patient.VisitStatus;
                    }

                    var ignoreList = new List<string>() { "Id", "CreationTime" };
                    if (patient.RegisterInfo != null && patient.RegisterInfo.Count > 0)
                    {
                        foreach (var registerInfo in patient.RegisterInfo)
                        {
                            registerInfo.PatientInfoId = dto.PatientInfo.TriagePatientInfoId;
                        }
                        //院前分诊同步过来的数据，有可能存在his同步接口数据先到的情况,所以先根据his的挂号信息及病人id查询数据库是否已存在该数据
                        if (patient.PatientId != null)
                        {
                            PatientInfo hisPatient = null;
                            if (patient.RegisterInfo.Count > 0 && patient.RegisterInfo.Any(p => p != null))
                            {
                                //从his视图中获取当前患者信息，如果redis中不存在该患者将其信息存入redis
                                var registerInfo = patient.RegisterInfo.OrderByDescending(p => p.RegisterTime).FirstOrDefault();
                                var hisPatientInfo = await RegisterInfoRepository.GetHisPatientInfoAsync(registerInfo.RegisterNo);
                                string cacheKey = $"{_configuration["ServiceName"]}:RegisterPatients";
                                // 从缓存获取上一次同步的数据
                                var json = await _redis.StringGetAsync(cacheKey);
                                List<HisRegisterPatient> patientCache = json.HasValue ? JsonSerializer.Deserialize<List<HisRegisterPatient>>(json) : new List<HisRegisterPatient>();
                                if (patientCache.Any((y => patient.PatientId == y.PatientId && patient.RegisterInfo.Any(z => z.RegisterNo.Contains(y.RegisterNo)))))
                                {
                                    //1先将his同步过来的数据更新到未分诊前的那条数据 
                                    hisPatient = await this._patientInfoRepository.AsQueryable()
                                   .Include(c => c.ConsequenceInfo)
                                   .Include(c => c.VitalSignInfo)
                                   .Include(c => c.RegisterInfo)
                                   .Include(c => c.AdmissionInfo)
                                   .Include(c => c.ScoreInfo)
                                   .Where(x => !x.IsFirstAid && x.PatientId == patient.PatientId && x.RegisterInfo.Any(
                                       y => patient.RegisterInfo.Select(z => z.RegisterNo).Contains(y.RegisterNo)))
                                   .FirstOrDefaultAsync();
                                    if (hisPatient != null)
                                    {
                                        // ObjectCoalesceExtension<PatientInfo>.CoalesceTo(hisPatient, oldPatient, ignoreList);
                                        //2.将删除his同步过来那条数据删除
                                        hisPatient.IsDeleted = true;
                                        foreach (var item in hisPatient.RegisterInfo)
                                        {
                                            item.IsDeleted = true;
                                        }
                                        await _patientInfoRepository.UpdateAsync(hisPatient);
                                    }
                                }
                                else
                                {
                                    if (hisPatientInfo != null)
                                    {
                                        // 缓存当前同步的数据
                                        patientCache.Add(hisPatientInfo);
                                        await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(patientCache));
                                    }

                                }
                            }
                        }
                    }
                    //3.将院前分诊病人信息更新
                    ObjectCoalesceExtension<PatientInfo>.CoalesceTo(patient, oldPatient, ignoreList);
                    await _patientInfoRepository.UpdateAsync(oldPatient);
                    await this.CurrentUnitOfWork.SaveChangesAsync();

                }

                switch (_configuration["ApplicationName"])
                {
                    case "ECIS":

                        await RabbitMqAppService.PublishEcisPatientSyncPatientAsync(new List<PatientInfoMqDto> { dto });

                        break;

                    case "PreHospital":

                        break;
                }

                _log.LogInformation("同步病患数据成功");
                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.LogError("同步病患数据错误！原因：{Msg}", e);
                return JsonResult.Fail();
            }
        }


        /// <summary>
        /// 同步 EmrService 急救病历患者基本信息
        /// </summary>
        /// <param name="dto"></param>
        // [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe("sync.patientInfo.from.emrservice")]
        public async Task SyncEmergencyMedicalRecordAsync(CreateOrUpdateEmrPatientDto dto)
        {
            try
            {
                using var uow = UnitOfWorkManager.Begin();
                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
                var patientInfo = await _patientInfoRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (patientInfo != null)
                {
                    patientInfo.PatientName = dto.PatientName;
                    patientInfo.Sex = dicts.GetCodeByDictName(TriageDict.Sex, dto.Sex);
                    patientInfo.SexName = dto.Sex;
                    patientInfo.Age = dto.Age;
                    patientInfo.IdentityNo = dto.DocumentNum;
                    patientInfo.Nation = dicts.GetCodeByDictName(TriageDict.Nation, dto.Nation);
                    patientInfo.NationName = dto.Nation;
                    patientInfo.ContactsPerson = dto.Contacts;
                    patientInfo.ContactsPhone = dto.Telephone;
                    patientInfo.Identity = dicts.GetCodeByDictName(TriageDict.IdentityType, dto.Occupation);
                    patientInfo.NarrationName = dto.NarrationName;
                    /*var narration = dto.NarrationName.Split(',').Aggregate("",
                        (current, item) => current + (dicts.GetCodeByDictName(TriageDict.Narration, item) + ","));

                    patientInfo.Narration = narration.TrimEnd(',');*/

                    await _patientInfoRepository.UpdateAsync(patientInfo);
                }

                _log.LogInformation("同步 EmrService 急救病历患者基本信息成功");
                await Task.CompletedTask;
                await uow.CompleteAsync();
            }
            catch (Exception e)
            {
                _log.LogError("同步 EmrService 急救病历患者基本信息错误，原因：{Msg}", e);
            }
        }


        /// <summary>
        /// 队列同步病患信息
        /// </summary>
        /// <param name="dto"></param>
        [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe("modify.patient.info.from.patient.service")]
        public async Task UpdatePatientInfoFromMqAsync(UpdatePatientInfoMqDto dto)
        {
            try
            {
                using var uow = this.UnitOfWorkManager.Begin();

                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
                dto.Sex = dicts.GetCodeByDictName(TriageDict.Sex, dto.Sex);
                await _patientInfoRepository.UpdatePatientInfoFromMqAsync(dto);
                await Task.CompletedTask;

                await uow.CompleteAsync();
            }
            catch (Exception e)
            {
                _log.LogError("接收病患微服务队列消息更新病患信息错误！原因：{Msg}", e);
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 同步叫号信息
        /// </summary>
        /// <param name="dto"></param>
        [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe("call.callstatus.changed")]
        public async Task UpdatePatientInfoWhenCallSnGenerated(CallingSnGeneratedMessageDto dto)
        {
            try
            {
                using var uow = UnitOfWorkManager.Begin();

                // 同步叫号信息
                var triageInfo = await _patientInfoRepository.FirstOrDefaultAsync(x => x.Id == dto.PI_ID);
                if (triageInfo != null)
                {
                    triageInfo.CallingSn = dto.CallingSn;
                    triageInfo.LogTime = dto.LogTime;
                    await _patientInfoRepository.UpdateAsync(triageInfo);
                }

                await uow.CompleteAsync();
            }
            catch (Exception e)
            {
                _log.LogError("接收叫号微服务队列消息更新叫号信息错误！原因：{Msg}", e);
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 同步就诊状态
        /// </summary>
        /// <param name="dto"></param>
        [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe("patient.visitstatus.changed")]
        public async Task UpdatePatientInfoWhenVisitStatusChanged(VisitStatusChangedMessageDto dto)
        {
            try
            {
                using var uow = this.UnitOfWorkManager.Begin();

                // 同步就诊状态
                PatientInfo patientInfo = await this._patientInfoRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (patientInfo != null)
                {
                    patientInfo.VisitStatus = dto.VisitStatus switch
                    {
                        EVisitStatus.WaittingTreat => VisitStatus.WattingTreat,
                        EVisitStatus.UntreatedOver => VisitStatus.Treated,
                        EVisitStatus.Treating => VisitStatus.Treating,
                        EVisitStatus.Treated => VisitStatus.Treated,
                        EVisitStatus.OutDepartment => VisitStatus.Treated,
                        _ => patientInfo.VisitStatus,
                    };
                    if (dto.VisitStatus == EVisitStatus.WaittingTreat)
                    { 
                        patientInfo.CallDoctorCode = string.Empty;
                        patientInfo.CallDoctorName = string.Empty;
                    }
                    else  if (dto.VisitStatus == EVisitStatus.UntreatedOver)
                    {
                        patientInfo.IsUntreatedOver = true;
                    }

                    // 保存最终去向
                    patientInfo.LastDirectionCode = dto.LastDirectionCode;
                    patientInfo.LastDirectionName = dto.LastDirectionName;
                    // 保存首诊医生信息
                    patientInfo.FirstDoctorCode = dto.FirstDoctorCode;
                    patientInfo.FirstDoctorName = dto.FirstDoctorName;
                    // 兼容北大就诊医生逻辑
                    patientInfo.DoctorName = dto.FirstDoctorName;

                    patientInfo.VisitDate = dto.VisitDate ?? patientInfo.VisitDate;
                    patientInfo.FinishVisitTime = dto.FinishVisitTime ?? patientInfo.FinishVisitTime;

                    //是否流转
                    if (!string.IsNullOrWhiteSpace(dto.TransferArea))
                    {
                        patientInfo.TransferArea = dto.TransferArea;
                    }
                    await this._patientInfoRepository.UpdateAsync(patientInfo);
                }

                await uow.CompleteAsync();
            }
            catch (Exception e)
            {
                _log.LogError("接收叫号微服务队列消息更新叫号信息错误！原因：{Msg}", e);
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 从 HIS 同步其他终端挂号、取号的患者信息
        /// 同步患者到本地后默认未分诊状态，此时急诊医生站无法接诊该患者，患者必须到分诊台进行分诊后才能看诊
        /// 此业务流程需要医院确认后按要求执行，若后期需要直接同步到医生站而无需分诊台进行分诊，则TriageStatus默认为1
        /// </summary>
        [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe("job.check.patient.exist")]
        public async Task SyncPatientFromHis(string jobTime)
        {
            _log.LogInformation($"作业发起时间 {jobTime}");
            var uow = UnitOfWorkManager.Begin();
            try
            {
                // 同步患者到诊疗的消息
                var mqDtoList = new List<PatientInfoMqDto>();
                // 获取科室列表
                var triageDepts = await _triageConfigRepository.AsQueryable().AsNoTracking()
                    .Where(x => x.TriageConfigType == (int)TriageDict.TriageDepartment).ToListAsync();
                // TODO: 暂无相关字段，默认普通诊室
                var targetConfig = await this._triageConfigRepository.AsNoTracking()
                        .Where(x => x.TriageConfigType == (int)TriageDict.TriageDirection)
                        .FirstOrDefaultAsync(x => x.TriageConfigCode == "TriageDirection_004");
                // TODO: 暂无相关字段，默认四级
                var triageLevelConfig = await this._triageConfigRepository.AsNoTracking()
                        .Where(x => x.TriageConfigType == (int)TriageDict.TriageLevel)
                        .FirstOrDefaultAsync(x => x.TriageConfigCode == "TriageLevel_004");
                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync(isEnable: -1, isDeleted: -1);
                IEnumerable<QueueEto> queueList = await this.GetCallList(); // 获取HIS队列
                                                                            // 如果不是等候中的患者，不同步；如果不是急诊科的队列，不同步
                var syncQueueList = queueList.Where(x =>
                    // x.PatientState == "0" && 
                    triageDepts.Select(y => y.HisConfigCode).Contains(x.DepartmentCode));
                // 查询24小时内的患者信息
                var patientsToday = await _patientInfoRepository
                    .Include(x => x.RegisterInfo)
                    .AsQueryable()
                    .Where(x => x.CreationTime >= DateTime.Now.AddDays(-1))
                    .ToListAsync();
                foreach (var item in syncQueueList)
                {
                    // 判断患者信息存在则不同步
                    var existsItem = patientsToday
                        .Where(x => x.PatientId == item.PatientId)
                        .WhereIf(!string.IsNullOrWhiteSpace(item.RegSerialNo),
                            x => x.RegisterInfo != null && x.RegisterInfo.Any(y => y.RegisterNo == item.RegSerialNo))
                        .FirstOrDefault();
                    if (existsItem != null) continue;

                    // 科室
                    var triageDept = triageDepts.FirstOrDefault(x => x.HisConfigCode == item.DepartmentCode);
                    PatientInfoFromHis hisPatientInfo = await this._hisApi.GetPatienInfoBytIdAsync(item.PatientId);
                    hisPatientInfo.GetAge();
                    var patientInfo = hisPatientInfo.BuildAdapter().AdaptToType<PatientInfo>();
                    patientInfo.CallingSn = item.QueueNo;

                    ConsequenceInfo consequenceInfo = new ConsequenceInfo
                    {
                        CreationTime = DateTime.Now,
                        PatientInfoId = patientInfo.Id,
                        TriageDeptCode = triageDept.TriageConfigCode,
                        TriageDeptName = triageDept.TriageConfigName,
                        TriageTargetCode = targetConfig.TriageConfigCode,
                        TriageTargetName = targetConfig.TriageConfigName,
                        ActTriageLevel = triageLevelConfig.TriageConfigCode,
                        ActTriageLevelName = triageLevelConfig.TriageConfigName,
                        DoctorCode = item.DoctorCode,
                        DoctorName = item.DoctorName,
                        TriageAreaCode = null,
                        TriageAreaName = null,
                    };
                    var registerInfoList = await _hisApi.GetRegisterInfoListAsync(new RegisterInfoInput()
                    {
                        PatientId = patientInfo.PatientId,
                        VisitNum = item.RegSerialNo,
                        StartDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                        EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")
                    });
                    if (registerInfoList.Code != 0 && registerInfoList.Data.Any())
                    {
                        var register = registerInfoList.Data.FirstOrDefault();
                        patientInfo.ChargeTypeName = register?.PatientType;
                        var triageFaberConfig = await this._triageConfigRepository.AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                x.TriageConfigType == (int)TriageDict.Faber &&
                                x.TriageConfigName == register.PatientType);
                        if (triageFaberConfig != null)
                        {
                            patientInfo.ChargeType = triageFaberConfig.TriageConfigCode;
                        }
                    }
                    patientInfo.ConsequenceInfo = consequenceInfo;
                    patientInfo.VitalSignInfo = new VitalSignInfo();
                    patientInfo.ScoreInfo = new List<ScoreInfo>();
                    patientInfo.AdmissionInfo = new AdmissionInfo();
                    patientInfo.TriageUserCode = ""; // 分诊护士，挂号同步过来的没有对应数据
                    patientInfo.TriageUserName = ""; // 分诊护士，挂号同步过来的没有对应数据
                    patientInfo.TriageStatus = 1;
                    patientInfo.IsSyncRegister = true;
                    var list = new List<PatientInfo> { patientInfo };
                    var returnResult = await _patientInfoRepository.SaveTriageRecordAsync(list, dicts);
                    if (!returnResult.Data)
                    {
                        _log.LogError("分诊确认保存失败！原因：{Msg}", "仓储保存分诊失败");
                        throw new Exception(returnResult.Msg);
                    }
                    var registerInfo = new RegisterInfo
                    {
                        PatientInfoId = patientInfo.Id,
                        Sort = 1,
                        Remark = null,
                        RegisterNo = item.RegSerialNo, // 挂号流水号
                        RegisterDeptCode = triageDept.TriageConfigCode,
                        RegisterDoctorCode = item.DoctorCode,
                        RegisterDoctorName = item.DoctorName,
                        RegisterTime = item.ListingTime ?? DateTime.Now,
                        VisitNo = hisPatientInfo.VisitNo,
                    };
                    registerInfo.SetId(GuidGenerator.Create());
                    var dbContext = _patientInfoRepository.GetDbContext();
                    dbContext.Entry(registerInfo).State = EntityState.Added;
                    patientInfo.RegisterInfo = new List<RegisterInfo>
                    {
                        registerInfo
                    };

                    var patientMqDto = new PatientInfoMqDto
                    {
                        PatientInfo = patientInfo.BuildAdapter().AdaptToType<PatientInfoDto>(),
                        ConsequenceInfo = patientInfo.ConsequenceInfo?.BuildAdapter().AdaptToType<ConsequenceInfoDto>(),
                        VitalSignInfo = patientInfo.VitalSignInfo?.BuildAdapter().AdaptToType<VitalSignInfoDto>(),
                        ScoreInfo = patientInfo.ScoreInfo?.BuildAdapter().AdaptToType<List<ScoreInfoDto>>(),
                        RegisterInfo = registerInfo?.BuildAdapter().AdaptToType<RegisterInfoDto>(),
                        AdmissionInfo = patientInfo.AdmissionInfo?.BuildAdapter().AdaptToType<AdmissionInfoDto>()
                    };

                    if (patientMqDto.ConsequenceInfo != null)
                    {
                        patientMqDto.ConsequenceInfo.HisDeptCode = dicts[TriageDict.TriageDepartment.ToString()]
                            .FirstOrDefault(x => x.TriageConfigCode == patientMqDto.ConsequenceInfo.TriageDept)
                            ?.HisConfigCode;
                    }

                    patientMqDto.PatientInfo.RegisterNo = item.RegSerialNo;

                    // 急诊分诊发送队列消息到叫号、诊疗微服务
                    mqDtoList.Add(patientMqDto);
                }

                await RabbitMqAppService.PublishEcisPatientSyncPatientAsync(mqDtoList);
            }
            catch (HisResponseException)
            {
                await uow.RollbackAsync();
                throw;
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                throw;
            }

            await uow.CompleteAsync();
        }



        /// <summary>
        /// 从HIS同步患者信息，更新患者状态
        /// </summary>
        [CapSubscribe("job.update.patientstatus.fromhis")]
        //  [ApiExplorerSettings(IgnoreApi = true)]
        public async Task SyncUpdatePatientStatusFromHis(string jobTime)
        {
            _log.LogInformation($"HIS同步同步患者信息，更新患者状态 {jobTime}");
            try
            {
                // 获取HIS患者状态数据
                var response = await this.GetQueryPatientStatus(DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                if (response.Code != 200)
                {
                    _log.LogError("从HIS同步患者信息，更新患者状态！SyncUpdatePatientFromHis 原因：{Msg}", response.Msg);
                }
                if (response.Data == null) return;

                //只筛选HIS退号状态
                //var hisResult = response.Data.Where(p => !string.IsNullOrEmpty(p.visSerialNo) || p.visitStatus == "3").ToList();
                var hisResult = response.Data.Where(p => p.visitStatus == "3").ToList();
                var hisRegSerialNos = hisResult.Select(y => y.regSerialNo);
                var patientlist = await _patientInfoRepository.GetPatientInfoByHisRegSerialNoAsync(hisRegSerialNos);

                var patients = new List<PatientInfo>();
                if (patientlist.Any())
                {
                    foreach (var item in hisResult)
                    {
                        var patient = patientlist.Where(p => p.registerNo == item.regSerialNo).Select(p => p.patientInfo).FirstOrDefault();
                        //判断His退号状态是否和系统一致，不一致则更新
                        if (!patient.IsCancelled && int.Parse(item.visitStatus) == (int)EVisitStatus.RefundNo)
                        {
                            patient.IsCancelled = true;
                            patient.CancellationUser = CurrentUser?.UserName;
                            patient.CancellationTime = DateTime.Now;

                            patients.Add(patient);
                        }
                    }

                    if (patients.Any() && patients.Count > 0)
                    {
                        _ = await _patientInfoRepository.UpdateRangeAsync(patients);
                    }

                    if (hisResult.Any())
                    {
                        //His患者状态判断验证更新到急诊系统
                        await RabbitMqAppService.PublishEcisPatientStatusAsync(hisResult);
                    }
                }
            }
            catch (HisResponseException)
            {
                throw;
            }

        }

        /// <summary>
        /// 龙岗中心获取队列表
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<QueueEto>> GetCallList()
        {
            var msg = JsonSerializer.Serialize(new
            {
                department = 0,
                doctor = "",
            });
            var httpContent = new StringContent(msg);
            httpContent.Headers.ContentType.MediaType = "application/json";
            var uri = _configuration.GetSection("HisApiSettings:callList").Value;
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var response = await httpClient.PostAsync(uri, httpContent);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CommonHttpResult<List<QueueEto>>>(responseText,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
            if (result.Code != 0)
            {
                throw new Exception("调用队列查询接口失败，" + result);
            }

            return result.Data;
        }


        /// <summary>
        /// 龙岗中心获取患者状态信息
        /// </summary>
        /// <returns></returns>
        private async Task<JsonResult<List<PatientInfoFromHisStatusDto>>> GetQueryPatientStatus(string beginTime, string endTime)
        {
            List<PatientInfoFromHisStatusDto> res = new List<PatientInfoFromHisStatusDto>();
            var uri = _configuration.GetSection("HisApiSettings:queryPatientStatus").Value + $"?querytype=1&begdate={beginTime}&enddate={endTime}";

            try
            {
                var httpClient = _httpClientFactory.CreateClient("HisApi");
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var responseText = await response.Content.ReadAsStringAsync();
                _log.LogInformation("调用平台接口查询患者信息，url: {Uri}，response: {ResponseText}", uri, responseText);
                if (string.IsNullOrWhiteSpace(responseText))
                {
                    return JsonResult<List<PatientInfoFromHisStatusDto>>.Fail("调用患者状态信息接口失败！请检查后重试");
                }

                var json = JObject.Parse(responseText);
                if (json["code"]?.ToString() != "0")
                {
                    return JsonResult<List<PatientInfoFromHisStatusDto>>.Fail("调用患者状态信息为空");
                }

                if (json["data"] == null || string.IsNullOrWhiteSpace(json["data"].ToString()))
                {
                    return JsonResult<List<PatientInfoFromHisStatusDto>>.Fail("调用患者状态信息为空");
                }

                if (json["data"]["body"]["data"] == null || string.IsNullOrWhiteSpace(json["data"]["body"]["data"].ToString()))
                {
                    return JsonResult<List<PatientInfoFromHisStatusDto>>.Fail("调用患者状态信息为空！");
                }

                // 返回多条数据
                var resp = JsonSerializer.Deserialize<List<PatientInfoFromHisStatusDto>>(json["data"]["body"]["data"].ToString(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                res = resp.BuildAdapter().AdaptToType<List<PatientInfoFromHisStatusDto>>();

                return JsonResult<List<PatientInfoFromHisStatusDto>>.Ok(data: res);
            }
            catch (Exception ex)
            {
                return JsonResult<List<PatientInfoFromHisStatusDto>>.Fail(ex.Message);
            }
        }


        /// <summary>
        /// 转诊
        /// </summary>
        /// <param name="id"></param>
        /// <param name="triageConfig"></param>
        /// <returns></returns>
        public async Task<JsonResult> ReferralAsync(Guid id, TriageConfig triageConfig)
        {
            _log.LogInformation($"调用转诊接口，患者ID：{id}，TriageConfig对象为：{JsonConvert.SerializeObject(triageConfig)}");

            var patientInfo = await _patientInfoRepository.Include(c => c.ConsequenceInfo).Include(r => r.RegisterInfo)
                .OrderByDescending(p => p.CreationTime).FirstOrDefaultAsync(x => x.Id == id);
            if (patientInfo == null)
            {
                return JsonResult.Fail("患者信息不存在");
            }

            var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
            var sex = dicts.GetNameByDictCode(TriageDict.Sex, patientInfo.Sex);
            //if (sex == "男")
            //{
            //    var isLimit = dicts[TriageDict.ReferralLimit.ToString()]
            //        ?.Any(x => x.HisConfigCode == triageConfig.HisConfigCode);
            //    if (isLimit.HasValue && isLimit.Value)
            //    {
            //        return JsonResult.Fail("患者转诊受限");
            //    }
            //}
            if (sex == "男" && (triageConfig.TriageConfigName.Contains("妇科") ||
                                triageConfig.TriageConfigName.Contains("产科") ||
                                triageConfig.TriageConfigName.Contains("妇产科"))
              )
            {
                return JsonResult.Fail("转诊失败，患者为男性，不能转诊到 妇科、产科、妇产科");
            }

            //转诊
            {
                using var uow = this.UnitOfWorkManager.Begin();
                try
                {
                    //更新病人信息为已转诊
                    patientInfo.IsReferral = true;

                    var targetConfig = await this._triageConfigRepository.AsNoTracking()
                        .FirstOrDefaultAsync(x =>
                            x.TriageConfigType == (int)TriageDict.TriageDirection &&
                            x.TriageConfigCode == "TriageDirection_005");
                    //更新分诊记录信息
                    patientInfo.ConsequenceInfo.TriageDeptCode = triageConfig?.TriageConfigCode; //科室变更Code
                    patientInfo.ConsequenceInfo.TriageDeptName = triageConfig?.TriageConfigName; //科室变更名称
                    patientInfo.ConsequenceInfo.TriageTargetCode = targetConfig.TriageConfigCode; //分诊去向编码
                    patientInfo.ConsequenceInfo.TriageTargetName = targetConfig.TriageConfigName; //分诊去向名称
                    patientInfo.TriageStatus = 1;
                    patientInfo.VisitStatus = VisitStatus.WattingTreat; //重置就诊状态为待就诊
                    patientInfo.TriageTime = DateTime.Now;

                    await _patientInfoRepository.UpdateAsync(patientInfo);

                    //调用接口平台
                    var result = await this._pekingUniversityhisApi.ReferralAsync(patientInfo, triageConfig);

                    string registerNo = patientInfo.RegisterInfo?.FirstOrDefault()?.RegisterNo;
                    await _capPublisher.PublishAsync("sync.refundpatient.to.callservice", new HashSet<string> { registerNo });

                    VisitStatusChangedMessageDto visitStatusChangedMessageDto = new VisitStatusChangedMessageDto(patientInfo.Id, EVisitStatus.UntreatedOver, true);
                    await _capPublisher.PublishAsync("triage.visitstatus.requeue", visitStatusChangedMessageDto);

                    await uow.CompleteAsync();
                    return JsonResult.Ok();
                }
                catch (Exception ex)
                {
                    await uow.RollbackAsync();
                    return JsonResult.Fail(ex.StackTrace + ex.Message);
                }
            }
        }



        /// <summary>
        /// 根据条件获取患者告知信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<PagedResultDto<PatientInformExportExcelDto>>> GetPatientInformListAsync(
            PatientInformQueryDto input)
        {
            try
            {
                return await GetPatientInformListByInputAsync(input);
            }
            catch (Exception e)
            {
                _log.LogError("根据条件获取患者告知信息错误！原因：{Msg}", e);
                return JsonResult<PagedResultDto<PatientInformExportExcelDto>>.Fail(e.Message);
            }
        }


        /// <summary>
        /// 根据输入项获取患者告知分页信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientInfo" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<PagedResultDto<PatientInformExportExcelDto>>> GetPatientInformListByInputAsync(
            PatientInformQueryDto input)
        {
            try
            {
                _log.LogInformation("根据输入项获取患者告知分页信息开始");

                #region EFCore查询
                var resList = (from pr in _patientInformRepository
                    .WhereIf(!string.IsNullOrWhiteSpace(input.WarningLv),
                        x => input.WarningLv == x.WarningLv)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DiseaseIdentification),
                        x => input.DiseaseIdentification == x.DiseaseIdentification)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.Source),
                        x => input.Source == x.Source)
                      .WhereIf(!string.IsNullOrWhiteSpace(input.CarNum),
                        x => x.CarNum.Contains(input.CarNum))
                               join d in _patientInfoRepository
                     on pr.PatientInfoId equals d.Id into temp
                               from p in temp.DefaultIfEmpty()
                               orderby pr.InformTime ascending
                               select new PatientInformExportExcelDto
                               {
                                   #region DoctorsAdvicesDto

                                   Id = pr.Id,
                                   TaskInfoNum = p.TaskInfoNum,
                                   CarNum = pr.CarNum,
                                   PatientId = p.PatientId,
                                   PatientName = pr.PatientName,
                                   Gender = pr.Gender,
                                   Age = pr.Age,
                                   IdTypeName = p.IdTypeName,
                                   IdentityNo = p.IdentityNo,
                                   WarningLv = pr.WarningLv,
                                   DiseaseIdentification = pr.DiseaseIdentification,
                                   InformTime = pr.InformTime,
                                   Source = pr.Source,
                                   #endregion
                               })
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PatientSearch), x =>
                        x.PatientName.Contains(input.PatientSearch)
                        || x.IdentityNo.Contains(input.PatientSearch)
                        || x.CarNum.Contains(input.PatientSearch)
                        || x.PatientId.Contains(input.PatientSearch))
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TaskInfoNum),
                        x => x.TaskInfoNum.Contains(input.TaskInfoNum));
                #endregion

                int totalCount;
                List<PatientInformExportExcelDto> res;

                //获取分页总行数
                totalCount = await resList.CountAsync();
                res = await resList
                    .Skip((input.SkipCount - 1) * input.MaxResultCount)
                    .Take(input.MaxResultCount)
                    .ToListAsync();

                var pageList = new PagedResultDto<PatientInformExportExcelDto>
                {
                    TotalCount = totalCount,
                    Items = res
                };

                _log.LogInformation("根据输入项获取患者告知分页信息成功");
                return JsonResult<PagedResultDto<PatientInformExportExcelDto>>.Ok(data: pageList);

            }
            catch (Exception e)
            {
                _log.LogError("根据输入项获取患者告知分页信息错误！原因：{Msg}", e);
                return JsonResult<PagedResultDto<PatientInformExportExcelDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 取消分诊（已分诊患者，返回到未分诊队列）
        /// </summary>
        public async Task<JsonResult> ReturnToNoTriage(Guid id)
        {
            return await _hisApi.ReturnToNoTriage(id);
        }

        /// <summary>
        /// 优先
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        public async Task<JsonResult> MoveToTop(string registerNo)
        {
            RegisterInfo registerInfo = RegisterInfoRepository.FirstOrDefault(x => x.RegisterNo == registerNo);
            if (registerInfo == null)
            {
                return JsonResult.Fail(msg: "没有找到挂号信息");
            }
            try
            {
                bool isTop = await _hisApi.MoveToTop(registerNo);

                PatientInfo patientInfo = await _patientInfoRepository.GetAsync(x => x.Id == registerInfo.PatientInfoId);
                patientInfo.IsTop = isTop;
                await _patientInfoRepository.UpdateAsync(patientInfo);
                return JsonResult.Ok();

            }
            catch (Exception ex)
            {
                return JsonResult.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 取消优先
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        public async Task<JsonResult> CancelMoveToTop(string registerNo)
        {
            RegisterInfo registerInfo = RegisterInfoRepository.FirstOrDefault(x => x.RegisterNo == registerNo);
            if (registerInfo == null)
            {
                return JsonResult.Fail(msg: "没有找到挂号信息");
            }
            bool isTop = await _hisApi.CancelMoveToTop(registerNo);

            PatientInfo patientInfo = await _patientInfoRepository.GetAsync(x => x.Id == registerInfo.PatientInfoId);
            patientInfo.IsTop = isTop;
            await _patientInfoRepository.UpdateAsync(patientInfo);
            return JsonResult.Ok();
        }

        /// <summary>
        /// 过号重排
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        public async Task<JsonResult> ReQueue(string registerNo)
        {
            RegisterInfo registerInfo = RegisterInfoRepository.FirstOrDefault(x => x.RegisterNo == registerNo);
            if (registerInfo == null)
            {
                return JsonResult.Fail(msg: "没有找到挂号信息");
            }

            PatientInfo patientInfo = await _patientInfoRepository
                .Include(c => c.ConsequenceInfo)
                .FirstOrDefaultAsync(x => x.Id == registerInfo.PatientInfoId);
            try
            {
                await _hisApi.ReturnToQueue(registerNo);
                VisitStatusChangedMessageDto visitStatusChangedMessageDto = new VisitStatusChangedMessageDto(patientInfo.Id, EVisitStatus.WaittingTreat);
                await _capPublisher.PublishAsync("triage.visitstatus.requeue", visitStatusChangedMessageDto);
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(ex.Message);
            }

            //现场要求重排后就诊状态改为待就诊
            patientInfo.VisitStatus = VisitStatus.WattingTreat;
            patientInfo.IsUntreatedOver = false;
            patientInfo.ConsequenceInfo.DoctorCode = string.Empty;
            patientInfo.ConsequenceInfo.DoctorName = string.Empty;
            await _patientInfoRepository.UpdateAsync(patientInfo);
            return JsonResult.Ok();
        }

        /// <summary>
        /// 同步患者就诊状态
        /// </summary>
        /// <param name="dto"></param>
        [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe("sync.visitstatus.from.patientservice")]
        public async Task UpdatePatientVisitStatusChanged(VisitStatusChangedMessageDto dto)
        {
            using var uow = this.UnitOfWorkManager.Begin();
            try
            {
                // 同步就诊状态
                PatientInfo patientInfo = await this._patientInfoRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (patientInfo != null)
                {
                    patientInfo.VisitStatus = dto.VisitStatus switch
                    {
                        EVisitStatus.WaittingTreat => VisitStatus.WattingTreat,
                        EVisitStatus.UntreatedOver => VisitStatus.Treated,
                        EVisitStatus.Treating => VisitStatus.Treating,
                        EVisitStatus.Treated => VisitStatus.Treated,
                        EVisitStatus.OutDepartment => VisitStatus.Treated,
                        _ => patientInfo.VisitStatus,
                    };
                    await this._patientInfoRepository.UpdateAsync(patientInfo);
                }

                await uow.CompleteAsync();
            }
            catch (Exception e)
            {
                _log.LogError("接收paitent服务消息更新预检分诊信息错误！原因：{Msg}", e);
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 同步就诊信息到预检分诊
        /// </summary>
        /// <param name="dto"></param>
        [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe("sync.visitinfo.from.patient.to.triage")]
        public async Task SyncVisitInfo(PatientVisitInfoMqDto dto)
        {
            using var uow = this.UnitOfWorkManager.Begin();
            try
            {
                // 同步就诊状态
                PatientInfo patientInfo = await this._patientInfoRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (patientInfo != null)
                {
                    patientInfo.DiagnoseCode = dto.DiagnoseCode ?? default;
                    patientInfo.DiagnoseName = dto.DiagnoseName ?? default;
                    await this._patientInfoRepository.UpdateAsync(patientInfo);
                }

                await uow.CompleteAsync();
            }
            catch (Exception e)
            {
                _log.LogError("接收paitent服务诊断消息更新预检分诊信息错误！原因：{Msg}", e);
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 同步患者叫号医生
        /// </summary>
        /// <param name="dto"></param>
        [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe("sync.calldoctor.from.patientservice")]
        public async Task UpdatePatientCallDoctor(PatientCallInfoMqDto dto)
        {
            using var uow = this.UnitOfWorkManager.Begin();
            try
            {
                // 同步就诊状态
                PatientInfo patientInfo = await this._patientInfoRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (patientInfo != null)
                {
                    patientInfo.CallDoctorCode =dto.DoctorCode ?? default;
                    patientInfo.CallDoctorName =dto.DoctorName ?? default;
                    await this._patientInfoRepository.UpdateAsync(patientInfo);
                }

                await uow.CompleteAsync();
            }
            catch (Exception e)
            {
                _log.LogError(
                    "接收paitent服务消息更新预检分诊叫号医生信息错误！原因：{Msg}",
                    e);
                await Task.CompletedTask;
            }
        }
    }
}