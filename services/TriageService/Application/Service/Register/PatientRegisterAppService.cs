using DotNetCore.CAP;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TriageService.HisApiBridge.Model;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using YJHealth.MedicalInsurance.Services;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 患者退挂号
    /// </summary>
    [Auth("PatientRegister")]
    [Authorize]
    [Route("/api/TriageService/patientRegister")]
    public class PatientRegisterAppService : ApplicationService, IPatientRegisterAppService
    {
        private readonly ILogger<PatientRegisterAppService> _log;
        private readonly IConfiguration _configuration;
        private readonly IHisApi _hisApi;
        private readonly ICallApi _callApi;
        private readonly IRegisterInfoRepository _registerInfoRepository;
        private readonly IPatientInfoRepository _patientInfoRepository;

        private ICapPublisher _capPublisher;
        private ICapPublisher CapPublisher => LazyGetRequiredService(ref _capPublisher);


        private IRabbitMqAppService _rabbitMqAppService;
        private IRabbitMqAppService RabbitMqAppService => LazyGetRequiredService(ref _rabbitMqAppService);


        private IHttpClientHelper _httpClientHelper;
        private IHttpClientHelper HttpClientHelper => LazyGetRequiredService(ref _httpClientHelper);


        private ITriageConfigAppService _triageConfigService;
        private ITriageConfigAppService TriageConfigService => LazyGetRequiredService(ref _triageConfigService);

        private IRepository<Covid19Exam> _covid19ExamRepository;
        private IRepository<Covid19Exam> Covid19ExamRepository => LazyGetRequiredService(ref _covid19ExamRepository);

        private IDapperRepository _dapperRepository;
        private IDapperRepository DapperRepository => LazyGetRequiredService(ref _dapperRepository);

        IRepository<TriageConfig> _triageConfigRepository;

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true // Optional: Make the JSON output indented for readability
        };

        public PatientRegisterAppService(ILogger<PatientRegisterAppService> log
            , IRegisterInfoRepository registerInfoRepository
            , IPatientInfoRepository patientInfoRepository
            , IConfiguration configuration
            , IHisApi hisApi
            , IRepository<TriageConfig> triageConfigRepository
            , ICallApi callApi)
        {
            _log = log;
            _registerInfoRepository = registerInfoRepository;
            _patientInfoRepository = patientInfoRepository;
            _configuration = configuration;
            _hisApi = hisApi;
            _callApi = callApi;
            _triageConfigRepository = triageConfigRepository;
        }

        /// <summary>
        /// 分页查询分诊患者基本信息以及挂号信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientRegister" + PermissionDefinition.Separator + PermissionDefinition.List)]
        [HttpGet("patientRegisterInfoList")]
        public async Task<JsonResult<PagedResultDto<PatientRegisterInfoDto>>> GetPatientRegisterInfoListAsync(
            PagedPatientRegisterInput input)
        {
            var res = await _registerInfoRepository.GetPatientRegisterInfoListAsync(input);
            return JsonResult<PagedResultDto<PatientRegisterInfoDto>>.Ok(data: res);
        }

        /// <summary>
        /// 挂号
        /// </summary>
        /// <param name="triagePatientInfoId"></param>
        /// <returns></returns>
        [Auth("PatientRegister" + PermissionDefinition.Separator + PermissionDefinition.Create)]
        [HttpPost("registerNoForPatient/{triagePatientInfoId}")]
        public async Task<JsonResult> CreateRegisterNoForPatientAsync(Guid triagePatientInfoId)
        {
            try
            {
                var patient = await _patientInfoRepository.Include(c => c.ConsequenceInfo)
                    .Include(c => c.AdmissionInfo)
                    .Include(c => c.RegisterInfo)
                    .Include(c => c.ScoreInfo)
                    .Include(c => c.VitalSignInfo)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == triagePatientInfoId);

                if (patient != null)
                {
                    var dto = patient.BuildAdapter().AdaptToType<PatientReqDto>();
                    dto.deptId = _configuration["SixCenter:DepId"];
                    dto.deptName = _configuration["SixCenter:DepName"];
                    dto.beginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dto.endTime = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                    dto.seeDate = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                    dto.doctorId = CurrentUser.UserName;
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
                    if (!Convert.ToBoolean(_configuration["Settings:IsEnabledCreatePatientIdExe"]))
                    {
                        var dict = await TriageConfigService.GetTriageConfigByRedisAsync(
                            TriageDict.Sex.ToString());
                        var sex = dict.GetNameByDictCode(TriageDict.Sex, dto.sex);
                        dto.sex = sex switch
                        {
                            "男" => "M",
                            "女" => "F",
                            _ => "未知"
                        };

                        var msg = JsonSerializer.Serialize(dto);
                        var httpContent = new StringContent(msg);
                        httpContent.Headers.ContentType.MediaType = "application/json";
                        var uri = _configuration.GetSection("HisApiUrl").Value + "/api/ecis/registerPatient";
                        _log.LogInformation($"调用接口平台挂号，url: {uri}, request: {JsonSerializer.Serialize(dto)}");
                        var response = await HttpClientHelper.PostAsync(uri, httpContent);
                        if (string.IsNullOrWhiteSpace(response))
                        {
                            _log.LogError(
                                "【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【Msg：调用挂号接口失败，响应为空】");
                            return JsonResult.Fail("患者挂号失败！请检查后重试！");
                        }

                        var json = JObject.Parse(response);
                        if (json["code"]?.ToString() == "0")
                        {
                            if (json["data"] != null && !string.IsNullOrWhiteSpace(json["data"].ToString()))
                            {
                                var resp = JsonSerializer.Deserialize<PatientRespDto>(json["data"].ToString());
                                registerInfo = resp.BuildAdapter().AdaptToType<RegisterInfo>();
                            }
                            else
                            {
                                _log.LogError("【PatientInfoService】【GetPatientRecordByHl7MsgAsync】【挂号失败】" +
                                           "【Msg：调用挂号接口失败。返回Data为null或空】");
                                return JsonResult.Fail("查询患者信息失败！请检查后重试");
                            }
                        }
                        else
                        {
                            _log.LogError("【PatientRegisterService】【CreateRegisterNoForPatientAsync】" +
                                       $"【挂号失败】【Msg：调用挂号接口失败。返回原因：{json["msg"]}】");
                            return JsonResult.Fail($"患者挂号失败！{json[msg]}");
                        }
                    }
                    else
                    {
                        registerInfo = new RegisterInfo
                        {
                            RegisterDeptCode = "2000",
                            RegisterDoctorCode = "001115",
                            RegisterNo = DateTime.Now.ToString("ffffff") + new Random().Next(0, 9),
                            RegisterTime = DateTime.Now,
                            AddUser = CurrentUser?.UserName
                        }.SetId(GuidGenerator.Create());
                        var maxVisitNo = patient.RegisterInfo.Max(m => m.VisitNo);
                        if (string.IsNullOrWhiteSpace(maxVisitNo))
                        {
                            maxVisitNo = "0";
                        }

                        if (!int.TryParse(maxVisitNo, out var visitNo))
                        {
                            visitNo = 0;
                        }

                        registerInfo.VisitNo = (visitNo + 1).ToString();
                    }

                    if (string.IsNullOrWhiteSpace(registerInfo.RegisterNo))
                    {
                        _log.LogError("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【Msg：调用挂号接口失败】");
                        return JsonResult.Fail("患者挂号失败！请检查后重试！");
                    }

                    registerInfo.PatientInfoId = patient.Id;
                    var dbContext = _registerInfoRepository.GetDbContext();
                    dbContext.Entry(registerInfo).State = EntityState.Added;
                    if (await dbContext.SaveChangesAsync() <= 0)
                    {
                        _log.LogError("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【Msg：DB保存挂号数据报错】");
                        return JsonResult.Fail("挂号失败");
                    }

                    // 限制患者基本信息为只读，挂号后不允许修改患者基本信息，这会导致ECIS的患者信息跟HIS的患者信息产生差异
                    patient.IsBasicInfoReadOnly = true;
                    await this._patientInfoRepository.UpdateAsync(patient);

                    #region 急诊分诊发送队列消息到叫号、诊疗微服务

                    var mqDto = new PatientInfoMqDto
                    {
                        PatientInfo = patient.BuildAdapter().AdaptToType<PatientInfoDto>(),
                        ConsequenceInfo = patient.ConsequenceInfo?.BuildAdapter().AdaptToType<ConsequenceInfoDto>(),
                        VitalSignInfo = patient.VitalSignInfo?.BuildAdapter().AdaptToType<VitalSignInfoDto>(),
                        ScoreInfo = patient.ScoreInfo?.BuildAdapter().AdaptToType<List<ScoreInfoDto>>(),
                        RegisterInfo = registerInfo?.BuildAdapter().AdaptToType<RegisterInfoDto>(),
                        AdmissionInfo = patient.AdmissionInfo?.BuildAdapter().AdaptToType<AdmissionInfoDto>()
                    };

                    var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
                    if (mqDto.ConsequenceInfo != null)
                    {
                        mqDto.ConsequenceInfo.HisDeptCode = dicts[TriageDict.TriageDepartment.ToString()]
                            .FirstOrDefault(x => x.TriageConfigCode == mqDto.ConsequenceInfo.TriageDept)
                            ?.HisConfigCode;
                    }

                    var mqDtoList = new List<PatientInfoMqDto>
                    {
                        mqDto
                    };

                    await RabbitMqAppService.PublishEcisPatientSyncPatientAsync(mqDtoList);

                    #endregion

                    #region 同步病患到六大中心

                    var eto = patient.BuildAdapter().AdaptToType<SyncPatientEventBusEto>();
                    eto.RegisterNo = registerInfo.RegisterNo;
                    eto.VisitNo = registerInfo.VisitNo;
                    var etoList = new List<SyncPatientEventBusEto> { eto };
                    await RabbitMqAppService.PublishSixCenterSyncPatientAsync(etoList, dicts);

                    #endregion

                    return JsonResult.Ok("挂号成功");
                }

                _log.LogError("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【该患者尚未分诊】");
                return JsonResult.Fail("该患者尚未分诊，请先分诊！");
            }
            catch (Exception e)
            {
                _log.LogWarning($"【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 取消挂号
        /// </summary>
        /// <param name="patientInfoId">分诊患者Id</param>
        /// <returns></returns>
        [UnitOfWork]
        [Auth("PatientRegister" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        [HttpPost("cancelRegisterNo/{patientInfoId}")]
        public async Task<JsonResult> CancelRegisterNoAsync(Guid patientInfoId)
        {
            try
            {
                var entity = await _registerInfoRepository.FirstOrDefaultAsync(x => x.PatientInfoId == patientInfoId);

                return await this.CancelRegisterNoAsync(entity);
            }
            catch (Exception e)
            {
                await CurrentUnitOfWork.RollbackAsync();
                _log.LogWarning($"【PatientRegisterService】【CancelRegisterNoAsync】【取消挂号错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        #region 挂号 --> 分诊

        /// <summary>
        /// 挂号（分诊前）（适用于 挂号 --> 分诊 的模式）
        /// </summary>
        /// <param name="input">挂号参数（来自HIS）</param>
        /// <returns></returns>
        [HttpPost("register/before-triage")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<JsonResult> RegisterBeforeTriageAsync(RegisterInfoBeforeTriageInput input)
        {
            try
            {
                // 通过挂号流水号判断挂号信息是否已经存在
                var existsPatient = await this._patientInfoRepository
                    .FirstOrDefaultAsync(x => x.RegisterInfo.Any(y => y.RegisterNo == input.RegisterNo));
                if (existsPatient != null)
                {
                    return JsonResult.Fail("挂号流水号已存在，无法添加相同的流水号.");
                }

                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync(isEnable: -1, isDeleted: -1);
                // 病患基本信息
                var patient = input.BuildAdapter().AdaptToType<PatientInfo>();
                patient = dicts.SetPatientInfo(patient)
                    .SetId(GuidGenerator.Create());
                patient.TriageStatus = 0; // 暂存
                patient.VisitStatus = VisitStatus.NotTriageYet;
                // 限制患者基本信息为只读
                patient.IsBasicInfoReadOnly = true;
                // 当从外部获取新冠问卷时，不在ECIS中录入个人史信息
                patient.IsCovidExamFromOuterSystem = true;

                // 挂号信息
                var registerInfo = new RegisterInfo(GuidGenerator.Create(), patient.Id);
                registerInfo = input.BuildAdapter().AdaptTo(registerInfo);
                patient.RegisterInfo = new List<RegisterInfo>
                {
                    registerInfo
                };

                await this._patientInfoRepository.InsertAsync(patient);

                _log.LogInformation("【PatientRegisterService】【RegisterBeforeTriageAsync】【挂号完成】");
                return JsonResult.Ok("挂号成功");
            }
            catch (Exception e)
            {
                _log.LogWarning($"【PatientRegisterService】【CancelRegisterNoAsync】【取消挂号错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 获取挂号患者列表
        /// 挂号超过24小时的不会被查询到结果中
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        [HttpGet("register")]
        public async Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterListAsync(GetRegisterListInput input)
        {
            int.TryParse(_configuration["Settings:RegisterShowTime"], out int time);
            time = time > 0 ? -time : -24;
            RegisterPatientInfoResultDto result = new RegisterPatientInfoResultDto();
            var list = await _patientInfoRepository
                .Include(x => x.RegisterInfo)
                .Include(x => x.ConsequenceInfo)
                .Include(x => x.VitalSignInfo)
                .Where(x => x.RegisterInfo.Any(x => x.RegisterTime >= DateTime.Now.AddHours(time))) // 只查询挂号时间在24小时以内的
                .WhereIf(!string.IsNullOrEmpty(input.DeptCode) && input.DeptCode != "全部患者" && input.DeptCode != "未分诊",
                    x => x.ConsequenceInfo.TriageDeptCode.Trim() == input.DeptCode.Trim()) // 查询指定科室
                .WhereIf(!string.IsNullOrEmpty(input.SearchText),
                    x => x.PatientName.Contains(input.SearchText) || x.CallingSn.Contains(input.SearchText) ||
                         x.ContactsPhone.Contains(input.SearchText))
                .WhereIf(input.DeptCode == "未分诊",
                    x => x.VisitStatus == VisitStatus.NotTriageYet || x.VisitStatus == VisitStatus.Suspend)
                .OrderByDescending(x => x.VisitStatus != VisitStatus.NotTriageYet ? 1 : 0) // 就诊状态排序、分诊状态排序
                .ThenBy(x => x.ConsequenceInfo.ActTriageLevel) // 分诊等级排序
                .ThenByDescending(x => x.VisitStatus)
                .ThenBy(x => x.TriageTime)
                .ThenBy(x => x.RegisterInfo.Max(y => y.RegisterTime)) // 挂号时间排序
                .ToListAsync();
            var covidExams = await this.Covid19ExamRepository
                .Where(x => list.Select(y => y.PatientId).Contains(x.PatientId)).ToListAsync();
            // 等候人数计算
            var waittingList = await GetCurrentWaitingList();
            // 是否退号的判断
            Func<PatientInfo, bool> isRefundRegister = (PatientInfo patient) =>
            {
                return patient.RegisterInfo.OrderByDescending(y => y.RegisterTime).First().IsCancelled;
            };
            result.Items = list.Where(x => !isRefundRegister(x)).BuildAdapter()
                .AdaptToType<List<RegisterPatientInfoDto>>();
            foreach (var item in result.Items)
            {
                item.HasFinishedCovid19Exam = covidExams.Any(x => x.PatientId == item.PatientId);
                item.WaittingForNumber = GetWaitingForNumber(list.FirstOrDefault(x => x.Id == item.TriagePatientInfoId),
                    waittingList);
            }

            result.TotalRegisterCount = list.Where(x => !isRefundRegister(x)).Count();
            result.TotalRegisterRefundCount = list.Where(x => isRefundRegister(x)).Count(); // 查询挂号记录是否退号（只查询最近的一条挂号记录）
            result.TotalTreatedCount = list.Where(x => !isRefundRegister(x))
                .Where(x => x.VisitStatus == VisitStatus.Treated).Count();
            result.TotalTreatingCount = list.Where(x => !isRefundRegister(x))
                .Where(x => x.VisitStatus == VisitStatus.Treating).Count();
            result.TotalWaitingCount = list.Where(x => !isRefundRegister(x))
                .Where(x => x.VisitStatus == VisitStatus.WattingTreat).Count();

            return JsonResult<RegisterPatientInfoResultDto>.Ok(data: result);
        }

        /// <summary>
        /// 获取挂号患者列表
        /// 挂号超过24小时的不会被查询到结果中
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        [HttpGet("register/paged-list")]
        [AllowAnonymous]
        public async Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterPagedListAsync(
            GetRegisterPagedListInput input)
        {
            if (_configuration["PekingUniversity:PageListVersion"].ParseToInt() == 2)
                return await _hisApi.GetRegisterPagedListV2Async(input);
            return await _hisApi.GetRegisterPagedListAsync(input);
        }


        /// <summary>
        /// 获取挂号患者信息（包含排队号、提供给打印）
        /// 会在保存分诊后调用该接口
        /// </summary>
        /// <param name="id">患者Id</param>
        /// <returns></returns>
        [HttpGet("register/{id}")]
        public async Task<JsonResult<RegisterPatientInfoDto>> GetRegisterInfoAsync(Guid id)
        {
            RegisterPatientInfoResultDto result = new RegisterPatientInfoResultDto();
            var currentPatient = await _patientInfoRepository
                                        .Include(x => x.RegisterInfo)
                                        .Include(x => x.ConsequenceInfo)
                                        .Include(x => x.VitalSignInfo)
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();
            if (currentPatient == null)
            {
                _log.LogError($"提供的GUID：{id} 无法查询到对应的患者");
                return JsonResult<RegisterPatientInfoDto>.Fail(msg: "无法查询到对应的患者");
            }

            var dto = currentPatient.BuildAdapter().AdaptToType<RegisterPatientInfoDto>();
            var covidExams = await this.Covid19ExamRepository.Where(x => x.PatientId == currentPatient.PatientId).ToListAsync();

            // 等候人数计算
            if (!string.IsNullOrEmpty(currentPatient.ConsequenceInfo.TriageDeptCode))
            {
                var waittingList = await PKUGetCurrentWaitingList(currentPatient.ConsequenceInfo.TriageDeptCode);
                if (_configuration["PekingUniversity:PageListVersion"].ParseToInt() == 2)
                {
                    dto.WaittingForNumber = await PKUGetWaitingForNumberV2Async(currentPatient, waittingList);
                }
                else
                {
                    dto.WaittingForNumber = PKUGetWaitingForNumber(currentPatient, waittingList);
                }
                    
            }
            else
            {
                dto.WaittingForNumber = 0;
            }
            // 特殊处理，如果时3级患者且前面等候人数为0，则前面等候人数为1
            // 这是为了避免3级患者来了发现前面等候人数为0，立即去科室，结果发现前面还有患者，导致患者不满意
            if (dto.WaittingForNumber == 0 && dto.ActTriageLevel == "TriageLevel_003")
            {
                dto.WaittingForNumber = 1;
            }

            dto.HasFinishedCovid19Exam = covidExams.Any(x => x.PatientId == currentPatient.PatientId);
            dto.RegisterNo = currentPatient.RegisterInfo.Count() > 0 ? currentPatient.RegisterInfo.FirstOrDefault().RegisterNo : null;
            dto.RegisterTime = currentPatient.RegisterInfo.Count() > 0
                ? (DateTime?)currentPatient.RegisterInfo.FirstOrDefault().RegisterTime
                : null;

            return JsonResult<RegisterPatientInfoDto>.Ok(data: dto);
        }

        #region 当前等候的患者列表
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
                //.Where(x => x.VisitStatus == VisitStatus.WattingTreat || x.VisitStatus == VisitStatus.Treating)
                .Where(x => x.VisitStatus == VisitStatus.WattingTreat)
                .ToListAsync();

            return waitingList;
        }

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
        #endregion

        #region 当前等候的患者列表 PKU
        /// <summary>
        /// 获取当前等候的患者列表
        /// 北大医院规则
        /// </summary>
        /// <returns></returns>
        private async Task<List<PatientInfo>> PKUGetCurrentWaitingList(string deptCode)
        {
            int.TryParse(_configuration["Settings:RegisterShowTime"], out int time);
            time = time > 0 ? -time : -24;
            // 等候人数计算
            var waitingList = await _patientInfoRepository.AsNoTracking()
                .Include(x => x.ConsequenceInfo)
                .Include(x => x.RegisterInfo)
                .Where(x => x.IsDeleted == false)
                .Where(x => x.RegisterInfo.Any(y =>
                    y.RegisterTime >= DateTime.Now.AddHours(time) && !y.IsCancelled)) // 只查询挂号时间在24小时以内的
                .Where(x => x.ConsequenceInfo != null)
                .Where(x => x.VisitStatus == VisitStatus.WattingTreat
                                            || x.VisitStatus == VisitStatus.Suspend
                                            || x.VisitStatus == VisitStatus.Treating)
                .Where(x => x.ConsequenceInfo.TriageDeptCode == deptCode)
                .ToListAsync();

            return waitingList;
        }

        /// <summary>
        /// 获取排队号
        /// 北大医院规则
        /// 同PekingUniversityHisApi.cs里的GetWaitingForNumber方法
        /// </summary>
        /// <param name="currentPatient"></param>
        /// <param name="waittingList"></param>
        /// <returns></returns>
        private int PKUGetWaitingForNumber(PatientInfo currentPatient, List<PatientInfo> waittingList)
        {
            if (currentPatient == null)
                return 0;
            // 未分诊的患者不需要计算等候人数
            if (currentPatient.TriageStatus == 1 && !string.IsNullOrEmpty(currentPatient.ConsequenceInfo?.TriageDeptCode))
            {
                int outValue = int.MaxValue;
                var deptWaittingList = waittingList
                    .Where(x => x.ConsequenceInfo.TriageDeptCode == currentPatient.ConsequenceInfo.TriageDeptCode)
                    .OrderByDescending(x =>
                    {
                        var retVal = 0;
                        switch (x.VisitStatus)
                        {
                            case VisitStatus.NotTriageYet:
                                retVal = -1;
                                break;
                            case VisitStatus.Treated:
                                retVal = -3;
                                break;
                            case VisitStatus.Suspend:
                                retVal = 0;
                                break;
                            default:
                                retVal = 1;
                                break;
                        }
                        return retVal;
                    })
                    .ThenBy(x => x.ConsequenceInfo.ActTriageLevel ?? "TriageLevel_999") // 分诊等级排序
                    .ThenByDescending(x => x.VisitStatus)
                    .ThenBy(x =>
                    {
                        outValue = int.MaxValue;
                        if (string.IsNullOrEmpty(x.CallingSn))
                            return outValue;
                        else if (x.CallingSn == "暂无")
                            return outValue;
                        else
                        {
                            int.TryParse(x.CallingSn.Substring(1), out outValue);
                            return outValue;
                        }
                    })
                    .ThenBy(x => x.RegisterInfo == null ? DateTime.MaxValue : x.RegisterInfo?.Max(y => y?.RegisterTime))  // 挂号时间排序，越早排序越前
                    .ToList();

                // 计算等待人数
                var findInWaitting = deptWaittingList.FirstOrDefault(x => x.Id == currentPatient.Id);
                int waittingForNumbe = 0;
                if (findInWaitting != null)
                {
                    waittingForNumbe = deptWaittingList.IndexOf(findInWaitting);
                }
                else if (currentPatient.VisitStatus == VisitStatus.NotTriageYet)
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

        /// <summary>
        /// 获取排队号
        /// 叫号服务规则
        /// </summary>
        /// <param name="currentPatient"></param>
        /// <param name="waittingList"></param>
        /// <returns></returns>
        private async Task<int> PKUGetWaitingForNumberV2Async(PatientInfo currentPatient, List<PatientInfo> waittingList)
        {
            if (currentPatient == null)
                return 0;
            // 未分诊的患者不需要计算等候人数
            if (currentPatient.TriageStatus == 1 && !string.IsNullOrEmpty(currentPatient.ConsequenceInfo?.TriageDeptCode))
            {
                var deptWaittingList = waittingList
                    .Where(x => x.ConsequenceInfo.TriageDeptCode == currentPatient.ConsequenceInfo.TriageDeptCode).ToList();
                List<CallPatientInfo> callPatientInfos = await _callApi.GetOrderListFromCallAsync(currentPatient.ConsequenceInfo.TriageDeptCode, pageSize: 100);
                deptWaittingList = HandledOrderList(callPatientInfos, deptWaittingList);

                // 计算等待人数
                var findInWaitting = deptWaittingList.FirstOrDefault(x => x.Id == currentPatient.Id);
                int waittingForNumbe = 0;
                if (findInWaitting != null)
                {
                    waittingForNumbe = deptWaittingList.IndexOf(findInWaitting);
                }
                else if (currentPatient.VisitStatus == VisitStatus.NotTriageYet)
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

        /// <summary>
        /// 根据 callPatientInfo 结合预检分诊列表进行排序
        /// </summary>
        /// <param name="callPatientInfos"></param>
        /// <param name="deptCurrentList"></param>
        /// <returns></returns>
        private List<PatientInfo> HandledOrderList(List<CallPatientInfo> callPatientInfos, IEnumerable<PatientInfo> deptCurrentList)
        {
            List<PatientInfo> orderedList = new List<PatientInfo>();
            IEnumerable<RegisterInfo> registerInfos = deptCurrentList.SelectMany(x => x.RegisterInfo);
            foreach (CallPatientInfo callPatientInfo in callPatientInfos)
            {
                RegisterInfo registerInfo = registerInfos.FirstOrDefault(x => x.RegisterNo == callPatientInfo.RegisterNo);
                if (registerInfo == null) continue;
                PatientInfo current = deptCurrentList.FirstOrDefault(x => x.Id == registerInfo.PatientInfoId);
                if (current != null)
                {
                    orderedList.Add(current);
                }
            }

            IEnumerable<PatientInfo> treatingList = deptCurrentList.Where(x => x.VisitStatus == VisitStatus.Treating);
            int outValue = int.MaxValue;
            treatingList.OrderBy(x => x.TriageStatus == 0 ? "" : x.ConsequenceInfo.ActTriageLevel).ThenBy(x =>
            {
                outValue = int.MaxValue;
                if (string.IsNullOrEmpty(x.CallingSn))
                    return outValue;
                else if (x.CallingSn == "暂无")
                    return outValue;
                else
                {
                    int.TryParse(x.CallingSn.Substring(1), out outValue);
                    return outValue;
                }
            })
             .ThenBy(x => x.TriageTime);
            treatingList = treatingList.Reverse();
            foreach (PatientInfo item in treatingList)
            {
                orderedList.Prepend(item);
            }

            return orderedList;
        }
        #endregion

        /// <summary>
        /// 查询挂号列表
        /// </summary>
        /// <param name="patientIds"></param>
        /// <returns></returns>
        [HttpGet("register/list")]
        public async Task<JsonResult<List<RegisterPatientInfoDto>>> GetRegisterListAsync(List<Guid> patientIds)
        {
            try
            {
                var list = await _patientInfoRepository
                    .Include(x => x.RegisterInfo)
                    .Include(x => x.ConsequenceInfo)
                    .Include(x => x.VitalSignInfo)
                    .AsNoTracking()
                    .Where(x => x.VisitStatus == VisitStatus.WattingTreat)
                    .Where(x => x.RegisterInfo.Any(x => x.RegisterTime >= DateTime.Now.AddDays(-1))) // 只查询挂号时间在24小时以内的
                    .Where(x => x.ConsequenceInfo != null && !string.IsNullOrEmpty(x.ConsequenceInfo.TriageDeptCode))
                    .OrderByDescending(x => x.VisitStatus != VisitStatus.NotTriageYet ? 1 : 0) // 就诊状态排序、分诊状态排序
                    .ThenBy(x => x.ConsequenceInfo.ActTriageLevel) // 分诊等级排序
                    .ThenByDescending(x => x.VisitStatus)
                    .ThenBy(x => x.TriageTime)
                    .ThenBy(x => x.RegisterInfo.Max(y => y.RegisterTime)) // 挂号时间排序
                    .ToListAsync();
                var results = (await _patientInfoRepository
                        .Include(x => x.RegisterInfo)
                        .Include(x => x.ConsequenceInfo)
                        .Include(x => x.VitalSignInfo)
                        .AsNoTracking()
                        .Where(x => patientIds.Contains(x.Id))
                        .OrderByDescending(x => x.VisitStatus != VisitStatus.NotTriageYet ? 1 : 0) // 就诊状态排序、分诊状态排序
                        .ThenBy(x => x.ConsequenceInfo.ActTriageLevel) // 分诊等级排序
                        .ThenByDescending(x => x.VisitStatus)
                        .ThenBy(x => x.TriageTime)
                        .ThenBy(x => x.RegisterInfo.Max(y => y.RegisterTime)) // 挂号时间排序
                        .ToListAsync())
                    .BuildAdapter().AdaptToType<List<RegisterPatientInfoDto>>();

                // 等候人数计算
                var waittingList = await GetCurrentWaitingList();

                // 查询是否填写新冠问卷
                var covidExams = await this.Covid19ExamRepository
                    .Where(x => results.Select(y => y.PatientId).Contains(x.PatientId)).ToListAsync();
                foreach (var item in results)
                {
                    item.HasFinishedCovid19Exam = covidExams.Any(x => x.PatientId == item.PatientId);
                    item.WaittingForNumber =
                        GetWaitingForNumber(list.FirstOrDefault(x => x.Id == item.TriagePatientInfoId), waittingList);
                }

                return JsonResult<List<RegisterPatientInfoDto>>.Ok(data: results);
            }
            catch (Exception ex)
            {
                return JsonResult<List<RegisterPatientInfoDto>>.Fail($"查询失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 挂号列表打印
        /// </summary>
        /// <param name="patientIds"></param>
        /// <returns></returns>
        [HttpGet("register/print")]
        public async Task<JsonResult<List<RegisterPatientInfoDto>>> GetRegisterByPrintAsync(string patientIds)
        {
            try
            {
                var list = await _patientInfoRepository
                    .Include(x => x.RegisterInfo)
                    .Include(x => x.ConsequenceInfo)
                    .Include(x => x.VitalSignInfo)
                    .AsNoTracking()
                    .Where(x => x.VisitStatus == VisitStatus.WattingTreat)
                    .Where(x => x.RegisterInfo.Any(x => x.RegisterTime >= DateTime.Now.AddDays(-1))) // 只查询挂号时间在24小时以内的
                    .Where(x => x.ConsequenceInfo != null && !string.IsNullOrEmpty(x.ConsequenceInfo.TriageDeptCode))
                    .OrderByDescending(x => x.VisitStatus != VisitStatus.NotTriageYet ? 1 : 0) // 就诊状态排序、分诊状态排序
                    .ThenBy(x => x.ConsequenceInfo.ActTriageLevel) // 分诊等级排序
                    .ThenByDescending(x => x.VisitStatus)
                    .ThenBy(x => x.TriageTime)
                    .ThenBy(x => x.RegisterInfo.Max(y => y.RegisterTime)) // 挂号时间排序
                    .ToListAsync();
                var results = (await _patientInfoRepository
                        .Include(x => x.RegisterInfo)
                        .Include(x => x.ConsequenceInfo)
                        .Include(x => x.VitalSignInfo)
                        .AsNoTracking()
                        .Where(x => patientIds.Contains(x.Id.ToString()))
                        .OrderByDescending(x => x.VisitStatus != VisitStatus.NotTriageYet ? 1 : 0) // 就诊状态排序、分诊状态排序
                        .ThenBy(x => x.ConsequenceInfo.ActTriageLevel) // 分诊等级排序
                        .ThenByDescending(x => x.VisitStatus)
                        .ThenBy(x => x.TriageTime)
                        .ThenBy(x => x.RegisterInfo.Max(y => y.RegisterTime)) // 挂号时间排序
                        .ToListAsync())
                    .BuildAdapter().AdaptToType<List<RegisterPatientInfoDto>>();

                // 等候人数计算
                var waittingList = await GetCurrentWaitingList();
                // 查询是否填写新冠问卷
                var covidExams = await this.Covid19ExamRepository
                    .Where(x => results.Select(y => y.PatientId).Contains(x.PatientId)).ToListAsync();
                foreach (var item in results)
                {
                    item.HasFinishedCovid19Exam = covidExams.Any(x => x.PatientId == item.PatientId);
                    item.WaittingForNumber =
                        GetWaitingForNumber(list.FirstOrDefault(x => x.Id == item.TriagePatientInfoId), waittingList);
                }

                return JsonResult<List<RegisterPatientInfoDto>>.Ok(data: results);
            }
            catch (Exception ex)
            {
                return JsonResult<List<RegisterPatientInfoDto>>.Fail($"查询失败：{ex.Message}");
            }
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
        /// 获取科室挂号统计列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("register/statistics")]
        public async Task<JsonResult<IEnumerable<DeptStatisticsDto>>> GetDeptStatistics()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<DeptStatisticsDto> result = new List<DeptStatisticsDto>();
            //var query = this._patientInfoRepository
            //    .Include(x => x.RegisterInfo)
            //    .Include(x => x.ConsequenceInfo)
            //    .GroupJoin(_triageConfigRepository, x => x.ConsequenceInfo.TriageDeptCode, y => y.TriageConfigCode,
            //            (x, y) => new { x.Id, x.VisitStatus, x.TriageStatus, x.ConsequenceInfo, x.RegisterInfo, Dept = y.DefaultIfEmpty() })
            //    .Where(x => x.RegisterInfo.Any(x => x.RegisterTime >= DateTime.Now.AddDays(-1) && !x.IsCancelled))  // 只查询挂号时间在24小时以内的，且未取消挂号的患者
            //    .SelectMany(x => x.Dept.DefaultIfEmpty(), (x, y) => new { x.ConsequenceInfo.TriageDeptCode, x.ConsequenceInfo.TriageDeptName, x.Id, x.VisitStatus, x.TriageStatus, y.Sort });
            var query = from pat in _patientInfoRepository.Include(x => x.RegisterInfo).Include(x => x.ConsequenceInfo)
                        join dept in _triageConfigRepository on pat.ConsequenceInfo.TriageDeptCode equals dept.TriageConfigCode
                            into Statistics
                        from m in Statistics.DefaultIfEmpty()
                        where pat.RegisterInfo.Any(x => x.RegisterTime >= DateTime.Now.AddDays(-1) && !x.IsCancelled)
                        select new
                        {
                            pat.ConsequenceInfo.TriageDeptCode,
                            pat.ConsequenceInfo.TriageDeptName,
                            pat.Id,
                            pat.VisitStatus,
                            pat.TriageStatus,
                            Sort = m != null ? m.Sort : 0
                        };
            var list = await query.ToListAsync();
            var unTriageTotalWaitingCount = list
                .Where(x => x.TriageStatus <= 0)
                .Where(x => x.VisitStatus == VisitStatus.NotTriageYet || x.VisitStatus == VisitStatus.NotTriageYet ||
                            x.VisitStatus == VisitStatus.WattingTreat)
                .Count();
            result.Add(new DeptStatisticsDto
            {
                Dept = "未分诊",
                DeptName = "未分诊",
                TotalWaitingCount = unTriageTotalWaitingCount,
                // 未分诊的患者不可能是已就诊状态。如果有，那就是Bug（狗头保命）  by: ywlin 2021-12-08
                TotalTreatedCount = 0,
            });

            var depts = list
                .Where(x => !string.IsNullOrEmpty(x.TriageDeptCode) && !string.IsNullOrEmpty(x.TriageDeptName))
                .GroupBy(x => new { x.TriageDeptCode, x.TriageDeptName, x.Sort })
                .Select(x => new { x.Key.TriageDeptCode, x.Key.TriageDeptName, x.Key.Sort, Count = x.Count() })
                .OrderBy(x => x.Sort)
                .ToList();
            //未包含分诊患者的科室
            var triageConfigDtoList = new List<TriageConfigDto>();
            var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
            var departmentList = dicts[TriageDict.TriageDepartment.ToString()]?.Where(p => p.IsDisable == 1);
            foreach (var item in departmentList)
            {
                if (!depts.Any(p => p.TriageDeptCode == item.TriageConfigCode))
                {
                    triageConfigDtoList.Add(item);
                }
            }

            //转诊科室
            var referralDepts = depts.Where(p => p.TriageDeptCode.Contains("OutpatientDepartment_"))
                .OrderBy(p => p.Sort).ToList();
            var newldepts = depts.Except(referralDepts).ToList();
            foreach (var dept in newldepts)
            {
                var deptTreatedCount = list
                    .Where(x => x.TriageDeptCode == dept.TriageDeptCode)
                    .Where(x => x.VisitStatus == VisitStatus.Treated)
                    .Count();
                var deptWaitingCount = list
                    .Where(x =>
                        (x.VisitStatus == VisitStatus.NotTriageYet || x.VisitStatus == VisitStatus.NotTriageYet) ||
                        x.TriageDeptCode == dept.TriageDeptCode)
                    .Where(x => x.VisitStatus == VisitStatus.WattingTreat)
                    .Count();
                result.Add(new DeptStatisticsDto
                {
                    Dept = dept.TriageDeptCode,
                    DeptName = dept.TriageDeptName,
                    TotalTreatedCount = deptTreatedCount,
                    TotalWaitingCount = deptWaitingCount,
                    Sort = dept.Sort
                });
            }

            //未包含分诊患者的科室
            foreach (var dept in triageConfigDtoList)
            {
                result.Add(new DeptStatisticsDto
                {
                    Dept = dept.TriageConfigCode,
                    DeptName = dept.TriageConfigName,
                    TotalTreatedCount = 0,
                    TotalWaitingCount = 0,
                    Sort = dept.Sort
                });
            }

            result = result.OrderBy(x => x.Sort).ToList();
            //转诊科室
            //foreach (var dept in referralDepts)
            //{
            //    var deptTreatedCount = list
            //        .Where(x => x.TriageDeptCode == dept.TriageDeptCode)
            //        .Where(x => x.VisitStatus == VisitStatus.Treated)
            //        .Count();
            //    var deptWaitingCount = list
            //        .Where(x =>
            //            (x.VisitStatus == VisitStatus.NotTriageYet || x.VisitStatus == VisitStatus.NotTriageYet) ||
            //            x.TriageDeptCode == dept.TriageDeptCode)
            //        .Where(x => x.VisitStatus == VisitStatus.WattingTreat)
            //        .Count();
            //    result.Add(new DeptStatisticsDto
            //    {
            //        Dept = dept.TriageDeptCode,
            //        DeptName = dept.TriageDeptName,
            //        TotalTreatedCount = deptTreatedCount,
            //        TotalWaitingCount = deptWaitingCount,
            //    });
            //}
            result.Add(new DeptStatisticsDto
            {
                Dept = "全部患者",
                DeptName = "全部患者",
                TotalWaitingCount = list.Where(x =>
                    x.VisitStatus == VisitStatus.NotTriageYet || x.VisitStatus == VisitStatus.Suspend ||
                    x.VisitStatus == VisitStatus.WattingTreat).Count(),
                TotalTreatedCount = list.Where(x => x.VisitStatus == VisitStatus.Treated).Count()
            });

            stopwatch.Stop();
            _log.LogInformation($"获取科室挂号统计列表完成，耗时：{stopwatch.ElapsedMilliseconds}ms");

            return JsonResult<IEnumerable<DeptStatisticsDto>>.Ok(data: result);
        }

        /// <summary>
        /// 添加新冠问卷信息
        /// </summary>
        /// <param name="patientId">患者ID</param>
        /// <param name="input">新冠问卷信息</param>
        /// <returns></returns>
        [HttpPost("{patientId}/covid-19")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<JsonResult<Covid19Exam>> AddCovid19ExamAsync(string patientId, PatientCovid19ExamInput input)
        {
            Covid19Exam entity = new Covid19Exam(GuidGenerator.Create());
            var patient = await this._patientInfoRepository.OrderBy(x => x.CreationTime)
                .FirstOrDefaultAsync(x => x.PatientId == patientId);
            if (patient == null)
            {
                return JsonResult<Covid19Exam>.Fail("找不到对应的患者信息");
            }

            input.BuildAdapter().AdaptTo(entity);
            entity.PatientId = patient.PatientId;
            entity.PatientName = patient.PatientName;
            entity.ContactsPhone = patient.ContactsPhone;
            await this.Covid19ExamRepository.InsertAsync(entity);

            return JsonResult<Covid19Exam>.Ok("添加成功.", entity);
        }

        /// <summary>
        /// 查询新冠问卷信息
        /// </summary>
        /// <param name="patientId">患者ID</param>
        /// <returns></returns>
        [HttpGet("{patientId}/covid-19")]
        public async Task<JsonResult<PatientCovid19ExamDto>> GetCovid19ExamAsync(string patientId)
        {
            Covid19Exam entity = await this.Covid19ExamRepository.OrderByDescending(x => x.CreationTime)
                .FirstOrDefaultAsync(x => x.PatientId == patientId);
            if (entity == null)
            {
                return JsonResult<PatientCovid19ExamDto>.Fail("找不到对应的患者信息");
            }

            PatientCovid19ExamDto result = entity.BuildAdapter().AdaptToType<PatientCovid19ExamDto>();

            return JsonResult<PatientCovid19ExamDto>.Ok(data: result);
        }

        #endregion

        /// <summary>
        /// 同步 HIS 挂号列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("sync-register-patient-from-his")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<JsonResult> SyncRegisterPatientFromHis()
        {
            if (_configuration["PekingUniversity:SyncRegisterPatientVersion"].ParseToInt() == 2)
                return await this._hisApi.SyncRegisterPatientFromHisV2();
            return await this._hisApi.SyncRegisterPatientFromHis();
        }

        #region Private Method

        /// <summary>
        /// 取消挂号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task<JsonResult> CancelRegisterNoAsync(RegisterInfo entity)
        {
            entity.DeleteUser = CurrentUser?.UserName;
            // Begin by: ywlin 2021-11-25------------------------------------------------------------------------------
            // await _registerInfoRepository.DeleteAsync(entity, true);
            // 取消挂号不应该使用删除/软删除，这会导致取消挂号的记录无法正常查询，虽然这很方便，但实际上有些业务场景是需要查询取消挂号的信息的
            // 这甚至会导致主表（患者信息）也无法查询到数据
            // End by: ywlin 2021-11-25------------------------------------------------------------------------------
            entity.IsCancelled = true;
            entity.CancellationTime = DateTime.Now;
            await _registerInfoRepository.UpdateAsync(entity);

            var taskInfoId = await _patientInfoRepository.AsNoTracking()
                .Where(x => x.Id == entity.PatientInfoId)
                .Select(s => s.TaskInfoId)
                .FirstOrDefaultAsync();

            var patient = await _patientInfoRepository.Include(c => c.ConsequenceInfo)
                .Include(c => c.VitalSignInfo)
                .Include(c => c.RegisterInfo)
                .Include(c => c.AdmissionInfo)
                .Include(c => c.ScoreInfo)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.PatientInfoId);
            if (patient != null)
            {
                var eto = patient.BuildAdapter().AdaptToType<SyncPatientEventBusEto>();
                eto.VisitNo = entity.VisitNo;
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
                        RegisterNo = "已退号",
                        VisitNo = entity.VisitNo
                    },

                    AdmissionInfo = patient.AdmissionInfo?.BuildAdapter().AdaptToType<AdmissionInfoDto>()
                };

                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
                if (mqDto.ConsequenceInfo != null)
                {
                    mqDto.ConsequenceInfo.HisDeptCode = dicts[TriageDict.TriageDepartment.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == mqDto.ConsequenceInfo.TriageDept)
                        ?.HisConfigCode;
                }

                if (mqDto.RegisterInfo != null)
                {
                    mqDto.RegisterInfo.RegisterNo = "已退号";
                }

                await CapPublisher.PublishAsync("patient.from.preHospital", mqDto);

                #endregion

                var etoList = new List<SyncPatientEventBusEto> { eto };
                await RabbitMqAppService.PublishSixCenterSyncPatientAsync(etoList, dicts);
                await this._capPublisher.PublishAsync("sync.patient.register.cancel", new { PI_ID = patient.Id });
            }

            _log.LogInformation("【PatientRegisterService】【CancelRegisterNoAsync】【取消挂号结束】");
            return JsonResult.Ok("取消挂号成功");
        }

        #endregion

        /// <summary>
        /// 获取挂号患者信息（包含排队号、提供给打印）
        /// </summary>
        /// <param name="queryDto">查询挂号入参</param>
        /// <returns></returns>
        [HttpGet("his-register")]
        public async Task<JsonResult<RegisterPatientInfoDto>> GetHISRegisterInfoAsync(HisRegisterInfoQueryDto queryDto)
        {
            try
            {
                List<PatientRespDto> hisRegister = await _hisApi.GetRegisterInfoAsync(queryDto);
                var result = hisRegister.FirstOrDefault()?.BuildAdapter().AdaptToType<RegisterPatientInfoDto>();

                //获取配置字典
                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();

                //his接口数据转换
                {
                    //费别
                    {
                        var chageType = dicts[TriageDict.Faber.ToString()]
                            ?.FirstOrDefault(x => x.HisConfigCode == result.ChargeType);
                        result.ChargeType = chageType?.TriageConfigCode;
                        result.ChargeTypeName = chageType?.TriageConfigName;
                    }
                    //科室
                    {
                        var dept = dicts[TriageDict.TriageDepartment.ToString()]
                            ?.FirstOrDefault(x => x.TriageConfigCode == result.TriageDeptCode);
                        result.TriageDeptCode = dept?.TriageConfigCode;
                        result.TriageDeptName = dept?.TriageConfigName;
                    }
                    //证件类型
                    var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                        ?.FirstOrDefault(x => x.HisConfigCode == result.IdTypeCode);
                    if (idTypeConfig != null)
                    {
                        result.IdTypeCode = idTypeConfig.TriageConfigCode;
                        result.IdTypeName = idTypeConfig.TriageConfigName;
                    }

                    var guardianIdType = dicts[TriageDict.IdType.ToString()]
                        ?.FirstOrDefault(x => x.HisConfigCode == result.GuardianIdTypeCode);
                    if (guardianIdType != null)
                    {
                        result.GuardianIdTypeCode = guardianIdType.TriageConfigCode;
                    }
                    result.Sex = result.Sex switch
                    {
                        "M" => "Sex_Man",
                        "F" => "Sex_Woman",
                        _ => "Sex_Unknown"
                    };
                    result.SexName = result.Sex switch
                    {
                        "Sex_Man" => "男",
                        "Sex_Woman" => "女",
                        _ => "未知"
                    };
                }

                //组装挂号数据
                {
                    var registerInfos = new List<RegisterInfo>();
                    var patient = await this._patientInfoRepository.OrderByDescending(x => x.CreationTime)
                        .FirstOrDefaultAsync(x => x.PatientId == result.PatientId);
                    //科室编码转换
                    foreach (var item in hisRegister)
                    {
                        var dept = dicts[TriageDict.TriageDepartment.ToString()]
                            ?.FirstOrDefault(x => x.TriageConfigCode == item.deptId);
                        registerInfos.Add(new RegisterInfo()
                        {
                            RegisterNo = item.registerId,
                            RegisterDeptCode = dept?.TriageConfigCode,
                            RegisterDoctorCode = item.doctorCode,
                            PatientInfoId = patient?.Id ?? Guid.Empty,
                            RegisterTime = Convert.ToDateTime(item.registerDate),
                            VisitNo = item.visitNo,
                            IsCancelled = item.isCancel == "0" ? false : true,
                        });
                    }

                    result.RegisterInfo = registerInfos;
                }
                return JsonResult<RegisterPatientInfoDto>.Ok(data: result);
            }
            catch (HisResponseException e)
            {
                return JsonResult<RegisterPatientInfoDto>.Fail(e.Message);
            }
        }
    }
}