using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 群伤事件接口
    /// </summary>
    [Auth("GroupInjury")]
    [Authorize]
    public class GroupInjuryAppService : ApplicationService, IGroupInjuryAppService
    {
        private readonly NLogHelper _log;
        private readonly IConfiguration _configuration;
        private readonly IGroupInjuryRepository _groupInjuryInfoRepository;

        private IHttpClientHelper _httpClientHelper;
        private IHttpClientHelper HttpClientHelper => LazyGetRequiredService(ref _httpClientHelper);

        private ICsEcisRepository _csEcisRepository;
        private ICsEcisRepository CsEcisRepository => LazyGetRequiredService(ref _csEcisRepository);

        private IPatientInfoRepository _patientInfoRepository;
        private IPatientInfoRepository PatientInfoRepository => LazyGetRequiredService(ref _patientInfoRepository);

        private ITriageConfigAppService _triageConfigService;
        private ITriageConfigAppService TriageConfigService => LazyGetRequiredService(ref _triageConfigService);

        private ICapPublisher _capPublisher;
        private ICapPublisher CapPublisher => LazyGetRequiredService(ref _capPublisher);

        private IPatientInfoAppService _patientInfoService;
        private IPatientInfoAppService PatientInfoService => LazyGetRequiredService(ref _patientInfoService);

        private IHisApi _hisApi;
        private IHisApi HisApi => LazyGetRequiredService(ref _hisApi);


        public GroupInjuryAppService(IGroupInjuryRepository groupInjuryInfoRepository, NLogHelper log,
            IConfiguration configuration)
        {
            _log = log;
            _configuration = configuration;
            _groupInjuryInfoRepository = groupInjuryInfoRepository;
        }

        /// <summary>
        /// 院前群伤事件分诊 TaskInfoId 有参数是院前分诊，无参数是急诊分诊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("GroupInjury" + PermissionDefinition.Separator + PermissionDefinition.Save)]
        public async Task<JsonResult<List<PatientOutput>>> SaveGroupInjuryTriageRecordAsync(
            CreateGroupInjuryTriageDto dto)
        {
            try
            {
                dto.StartTriageTime = DateTime.Now;
                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync(isEnable: -1, isDeleted: -1);
                if (dicts != null)
                {
                    // 由于前期急诊版本为CS端程序，所以院前分诊时需要往CS版急诊写入分诊数据
                    // 后期开发BS版急诊程序时，分诊服务代码未与院前分诊代码分离，所以采用此判断来区分是否需要往CS版急诊写入数据

                    #region 生成群伤病人患者分诊信息

                    var groupInjuryInfo = dto.BuildAdapter().AdaptToType<GroupInjuryInfo>().SetId(GuidGenerator.Create());
                    groupInjuryInfo.GroupInjuryName = dicts.GetNameByDictCode(TriageDict.GroupInjuryType, dto.GroupInjuryTypeCode);
                    var list = new List<PatientInfo>();

                    #region 生成群伤病人分诊去向信息

                    var relationService = ServiceProvider.GetRequiredService<ILevelTriageRelationDirectionAppService>();
                    var relationJr = await relationService.SelectLevelTriageRelationDirectionAsync();
                    list.AddRange(await GroupInjuryAutoSaveTriageInfoAsync(dto, TriageLevel.FirstLv, groupInjuryInfo.Id,
                        relationJr.Data.FirstOrDefault(x =>
                            x.TriageLevelCode == TriageLevel.FirstLv.GetDescriptionByEnum())));
                    list.AddRange(await GroupInjuryAutoSaveTriageInfoAsync(dto, TriageLevel.SecondLv,
                        groupInjuryInfo.Id, relationJr.Data.FirstOrDefault(x =>
                            x.TriageLevelCode == TriageLevel.FirstLv.GetDescriptionByEnum())));
                    list.AddRange(await GroupInjuryAutoSaveTriageInfoAsync(dto, TriageLevel.ThirdLv, groupInjuryInfo.Id,
                        relationJr.Data.FirstOrDefault(x =>
                            x.TriageLevelCode == TriageLevel.FirstLv.GetDescriptionByEnum())));
                    list.AddRange(await GroupInjuryAutoSaveTriageInfoAsync(dto, TriageLevel.FourthALv,
                        groupInjuryInfo.Id, relationJr.Data.FirstOrDefault(x =>
                            x.TriageLevelCode == TriageLevel.FirstLv.GetDescriptionByEnum())));
                    list.AddRange(await GroupInjuryAutoSaveTriageInfoAsync(dto, TriageLevel.FourthBLv,
                        groupInjuryInfo.Id, relationJr.Data.FirstOrDefault(x =>
                            x.TriageLevelCode == TriageLevel.FirstLv.GetDescriptionByEnum())));

                    #endregion

                    #endregion

                    // 若有患者patientId为空则视为失败
                    if (list.Count(x => string.IsNullOrWhiteSpace(x.PatientId)) > 0)
                    {
                        _log.Error(
                            "【GroupInjuryService】【SaveGroupInjuryTriageRecordAsync】【院前群伤事件分诊失败】【Msg：患者病历号创建失败！】");
                        return JsonResult<List<PatientOutput>>.Fail("创建群伤分诊失败！患者病历号创建失败");
                    }

                    var returnResult = await PatientInfoRepository.SaveTriageRecordAsync(list, dicts, groupInjuryInfo);

                    if (returnResult.Data)
                    {
                        foreach (var patient in list)
                        {

                            #region EFCore DBContext 保存数据时会把数据先加载到实体上，导致删除后数据也会出现，所以去除删除了的数据

                            if (Convert.ToBoolean(patient.AdmissionInfo?.IsDeleted))
                            {
                                patient.AdmissionInfo = null;
                            }

                            if (Convert.ToBoolean(patient.VitalSignInfo?.IsDeleted))
                            {
                                patient.VitalSignInfo = null;
                            }

                            if (Convert.ToBoolean(patient.ConsequenceInfo?.IsDeleted))
                            {
                                patient.ConsequenceInfo = null;
                            }

                            patient.RegisterInfo = patient.RegisterInfo?.Where(x => x.IsDeleted == false).ToList();
                            patient.ScoreInfo = patient.ScoreInfo?.Where(x => x.IsDeleted == false).ToList();

                            #endregion

                            #region 分诊后自动挂号

                            RegisterInfo registerInfo = null;
                            if (patient.TriageStatus == 1)
                            {
                                // 分诊保存后调用 HIS 预约医生/挂号接口
                                var registerAfterPatient = await HisApi.RegisterPatientAsync(patient, patient.ConsequenceInfo.DoctorCode,
                                    patient.ConsequenceInfo.DoctorName, false, false);
                                registerInfo = registerAfterPatient.RegisterInfo.OrderByDescending(o => o.RegisterTime)
                                    .FirstOrDefault();
                            }

                            #endregion

                            #region 院前分诊同步病患到CS版急诊分诊

                            if (Convert.ToBoolean(_configuration["Settings:IsSaveDataToCsEcis"]))
                            {
                                var csEcisSaveTriageDto = new CsEcisSaveTriageDto
                                {
                                    PvList = new List<PatientVisitDto>(),
                                    GroupInjury = groupInjuryInfo.BuildAdapter().AdaptToType<CsEcisGroupInjuryDto>()
                                };

                                var pv = patient.BuildAdapter().AdaptToType<PatientVisitDto>();

                                if (registerInfo != null)
                                {
                                    pv.RegisterNo = registerInfo.RegisterNo;
                                    pv.RegisterDT = registerInfo.RegisterTime;
                                    pv.VisitID = registerInfo.VisitNo;
                                }

                                // CS版急诊生成分诊记录
                                if (patient.ConsequenceInfo != null)
                                {
                                    pv.TriageRecord = patient.ConsequenceInfo.BuildAdapter()
                                        .AdaptToType<TriageRecordDto>();
                                    pv.TriageRecord.Id = GuidGenerator.Create();
                                    pv.TriageRecord.PVID = pv.PVID;
                                    pv.TriageRecord.TriageBy = patient.TriageUserName;
                                    pv.TriageRecord.TriageDT = patient.TriageTime;
                                    pv.TriageRecord.StartRecordDT = patient.StartTriageTime.Value;
                                }

                                // CS版急诊评分
                                if (pv.ScoreRecords != null && pv.ScoreRecords.Count > 0)
                                {
                                    pv.TriageRecord.HasScoreRecord =
                                        Convert.ToInt32(patient.ScoreInfo != null && patient.ScoreInfo.Count > 0);
                                    pv.ScoreRecords = patient.ScoreInfo.BuildAdapter()
                                        .AdaptToType<List<ScoreRecordDto>>();
                                    pv.ScoreRecords.ForEach(x =>
                                    {
                                        x.TID = pv.TriageRecord.Id;
                                        x.Id = GuidGenerator.Create();
                                        x.PVID = pv.PVID;
                                    });
                                }

                                // CS版急诊生命体征
                                if (patient.VitalSignInfo != null)
                                {
                                    pv.VitalSignRecord = patient.VitalSignInfo.BuildAdapter()
                                        .AdaptToType<TriageVitalSignRecordDto>();
                                    pv.VitalSignRecord.TID = pv.TriageRecord.Id;
                                    pv.VitalSignRecord.PVID = pv.PVID;
                                    pv.VitalSignRecord.Id = GuidGenerator.Create();
                                    pv.TriageRecord.HasVitalSign = Convert.ToInt32(patient.VitalSignInfo != null);
                                    pv.VitalSignRecord.VitalSignMemo = patient.VitalSignInfo.RemarkName;
                                }

                                csEcisSaveTriageDto.PvList.Add(pv);

                                await CsEcisRepository.SaveEcisTriageRecordAsync(csEcisSaveTriageDto);
                            }

                            #endregion

                            #region 院前分诊同步病患到急诊2.0分诊、演示环境

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

                            //是否启用发送数据到急诊分诊
                            if (Convert.ToBoolean(_configuration["Settings:ECISSettings:IsEnabled"]))
                            {
                                switch (Convert.ToInt32(_configuration["Settings:ECISSettings:Type"]))
                                {
                                    case 0:
                                        var capHeader = new Dictionary<string, string>
                                        {
                                            ["AppName"] = _configuration["ApplicationName"]
                                        };

                                        await CapPublisher.PublishAsync("patient.from.preHospital.triage", mqDto, capHeader);
                                        break;
                                    case 1:
                                        _log.Debug("【PatientInfoService】【SaveTriageRecordAsync】【开始调用接口同步病患到急诊分诊】");
                                        var url = _configuration["Settings:ECISSettings:ApiUrl"];
                                        var hisApiContent = new StringContent(JsonSerializer.Serialize(mqDto));
                                        var result = await _httpClientHelper.PostAsync(url, hisApiContent);
                                        _log.Debug(
                                            $"【PatientInfoService】【SaveTriageRecordAsync】【开始调用接口同步病患到急诊分诊结束】【Msg：{result}】");
                                        break;
                                }
                            }

                            #endregion

                            #region 同步病患到演示环境

                            if (Convert.ToBoolean(_configuration["Demo:IsEnabled"]))
                            {
                                var sex = "O";
                                if (!string.IsNullOrEmpty(patient.Sex))
                                {
                                    sex = patient.Sex switch
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
                                    patientId = patient.PatientId,
                                    patientClass = "O",
                                    identifyNo = patient.IdentityNo,
                                    patientName = patient.PatientName,
                                    sex = sex,
                                    admitSource = dicts.GetNameByDictCode(TriageDict.ToHospitalWay,
                                        patient.ToHospitalWayCode),
                                    reAdmissionIndicator = "0",
                                };

                                var content = new StringContent(JsonSerializer.Serialize(pe));
                                await HttpClientHelper.PostAsync(uri, content);
                            }

                            #endregion
                        }

                        var outPut = list.BuildAdapter().AdaptToType<List<PatientOutput>>();
                        return JsonResult<List<PatientOutput>>.Ok(data: outPut);
                    }

                    _log.Error("【GroupInjuryService】【SaveGroupInjuryTriageRecordAsync】【院前群伤事件分诊失败，仓储保存分诊失败】");
                    return JsonResult<List<PatientOutput>>.Fail(returnResult.Msg);
                }

                _log.Error("【GroupInjuryService】【SaveGroupInjuryTriageRecordAsync】【院前群伤事件分诊失败，不存在该群伤事件】");
                return JsonResult<List<PatientOutput>>.Fail("不存在该群伤事件");
            }
            catch (Exception e)
            {
                _log.Warning("【GroupInjuryService】【SaveGroupInjuryTriageRecordAsync】【院前群伤事件分诊错误】" +
                             $"【Msg：{e}】");
                return JsonResult<List<PatientOutput>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 急诊预检分诊群伤事件分诊
        /// 院前不使用该接口，该接口只使用与急诊先挂号后分诊模式
        /// by: ywlin 2021-12-31
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("GroupInjury" + PermissionDefinition.Separator + PermissionDefinition.Save)]
        public async Task<JsonResult> SaveGroupInjuryTriageRecordV2Async(CreateGroupInjuryTriageV2Dto dto)
        {
            try
            {
                dto.StartTriageTime = DateTime.Now;
                _log.Trace("【GroupInjuryService】【SaveGroupInjuryTriageRecordAsync】【院前群伤事件分诊开始】");
                var dicts = await TriageConfigService.GetTriageConfigByRedisAsync(isEnable: -1, isDeleted: -1);
                if (dicts == null)
                {
                    throw new Exception("从Redis缓存中获取字典");
                }

                // Step 1: 保存群伤事件 Begin
                var groupInjeryId = GuidGenerator.Create(); // 群伤事件 ID
                var groupInjuryInfo = dto.BuildAdapter().AdaptToType<GroupInjuryInfo>().SetId(groupInjeryId);
                groupInjuryInfo.GroupInjuryCode = dto.GroupInjuryTypeCode;
                groupInjuryInfo.GroupInjuryName =
                    dicts.GetNameByDictCode(TriageDict.GroupInjuryType, dto.GroupInjuryTypeCode);

                // Step 2: 患者关联群伤事件
                var patientInfos = await this.PatientInfoRepository.AsQueryable()
                    .Where(x => dto.TriagePatientInfoIds.Contains(x.Id))
                    .ToListAsync();
                patientInfos.ForEach(x => x.GroupInjuryInfoId = groupInjeryId);

                // Step 3: 持久化
                // 保存群伤事件
                await this._groupInjuryInfoRepository.InsertAsync(groupInjuryInfo);
                // 修改患者关联群伤事件
                await this.PatientInfoRepository.UpdateRangeAsync(patientInfos);

                _log.Info("【GroupInjuryService】【SaveGroupInjuryTriageRecordAsync】【院前群伤事件分诊结束】");
                return JsonResult.Ok();
            }
            catch (Exception ex)
            {
                _log.Warning("【GroupInjuryService】【SaveGroupInjuryTriageRecordV2Async】【院前群伤事件分诊错误】" +
                             $"【Msg：{ex}】");
                return JsonResult.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 修改关联群伤事件
        /// 院前不使用该接口，该接口只使用与急诊先挂号后分诊模式
        /// by: ywlin 2021-12-31
        /// </summary>
        /// <param name="groupInjuryId">群伤事件Id</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch]
        [Auth("GroupInjury" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> ModifyGroupInjuryRecordV2Async(Guid groupInjuryId, UpdateGroupInjuryV2Dto dto)
        {
            try
            {
                var model = await _groupInjuryInfoRepository.FirstOrDefaultAsync(x => x.Id == groupInjuryId);
                if (model != null)
                {
                    // Step 1: 修改群伤事件
                    model.Description = dto.Description;
                    model.Remark = dto.Remark;
                    model.HappeningTime = dto.HappeningTime;
                    model.ModUser = CurrentUser.UserName;
                    model.GroupInjuryCode = dto.GroupInjuryTypeCode;
                    var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
                    model.GroupInjuryName =
                        dicts.GetNameByDictCode(TriageDict.GroupInjuryType, dto.GroupInjuryTypeCode);

                    // Step 2: 患者关联群伤事件
                    var patientInfos = await this.PatientInfoRepository.AsQueryable()
                        .Where(x => dto.TriagePatientInfoIds.Contains(x.Id))
                        .ToListAsync();
                    // 从原有列表删除的患者，取消关联
                    var exceptPatientInfos = await this.PatientInfoRepository.AsQueryable()
                        .Where(x => x.GroupInjuryInfoId == groupInjuryId &&
                                    !dto.TriagePatientInfoIds.Contains(x.Id))
                        .ToListAsync();
                    patientInfos.ForEach(x => x.GroupInjuryInfoId = groupInjuryId);
                    exceptPatientInfos.ForEach(x => x.GroupInjuryInfoId = Guid.Empty);

                    // Step 3: 持久化
                    // 保存群伤事件
                    await _groupInjuryInfoRepository.UpdateAsync(model);
                    // 修改患者关联群伤事件
                    await this.PatientInfoRepository.UpdateRangeAsync(patientInfos);

                    return JsonResult.Ok();
                }

                _log.Error("【GroupInjuryService】【ModifyGroupInjuryRecordAsync】【修改关联群伤事件失败】" +
                           "【Msg：不存在此群伤事件】");
                return JsonResult.Fail("不存在此群伤事件");
            }
            catch (Exception e)
            {
                _log.Warning($"【GroupInjuryService】【ModifyGroupInjuryRecordAsync】【修改关联群伤事件错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 群伤自动生成分诊记录Model
        /// </summary>
        /// <param name="dto">群伤事件Model</param>
        /// <param name="triageLevel">级别名称</param>
        /// <param name="groupInjuryId"></param>
        /// <param name="relation"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<PatientInfo>> GroupInjuryAutoSaveTriageInfoAsync(CreateGroupInjuryTriageDto dto,
            TriageLevel triageLevel,
            Guid groupInjuryId, LevelTriageRelationDirectionDto relation)
        {
            var res = new List<PatientInfo>();
            _log.Trace("【GroupInjuryService】【GroupInjuryAutoSaveTriageInfoAsync】" +
                       "【群伤自动生成分诊记录Model开始】");
            var count = triageLevel switch
            {
                TriageLevel.FirstLv => dto.StLevelCount,
                TriageLevel.SecondLv => dto.NdLevelCount,
                TriageLevel.ThirdLv => dto.RdLevelCount,
                TriageLevel.FourthALv => dto.ThaLevelCount,
                TriageLevel.FourthBLv => dto.ThbLevelCount,
                _ => 0
            };

            TriageLevelPatientCountDto patientCountDto = null;

            for (var i = 0; i < count; i++)
            {
                var sex = "O";
                var level = triageLevel.GetDescriptionByEnum().Split('_').Length > 1
                    ? triageLevel.GetDescriptionByEnum().Split('_')[1]
                    : "000";
                var patientName = "群伤患者_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + (i + 1);
                _log.Info("【GroupInjuryService】【SaveTriageRecordAsync】【开始发送群伤患者建档HL7消息】");
                var input = new CreateOrGetPatientIdInput
                {
                    TaskInfoId = dto.TaskInfoId,
                    CarNum = dto.CarNum,
                    IsSyncToEmrService = false,
                    PatientName = patientName,
                    IsNoThree = true,
                    Sex = sex switch
                    {
                        "M" => "男",
                        "F" => "女",
                        _ => "未知"
                    },
                    Birthday = null,
                    IdTypeCode = "未知"
                };

                var result = await PatientInfoService.CreatePatientRecordByHl7MsgAsync(input);
                if (result.Code != 200)
                {
                    _log.Info($"群伤患者建档失败，原因：{result.Msg}");
                    throw new Exception(result.Msg);
                }

                var hl7Patient = result.Data.BuildAdapter().AdaptToType<Hl7PatientInfoDto>();
                var patientInfo = hl7Patient.BuildAdapter().AdaptToType<PatientInfo>();
                patientInfo.TaskInfoNum = dto.TaskInfoNum;
                patientInfo.TaskInfoId = dto.TaskInfoId;
                patientInfo.CarNum = dto.CarNum;
                patientInfo.GroupInjuryInfoId = groupInjuryId;
                patientInfo.Sex = Gender.Unknown.GetDescriptionByEnum();
                patientInfo.TriageUserCode = dto.TriageUserCode;
                patientInfo.TriageUserName = dto.TriageUserName;
                patientInfo.StartTriageTime = dto.StartTriageTime;
                patientInfo.TriageStatus = 1;
                patientInfo.TriageUserCode = string.IsNullOrWhiteSpace(dto.TriageUserCode)
                    ? CurrentUser.UserName
                    : dto.TriageUserCode;
                patientInfo.TriageUserName = string.IsNullOrWhiteSpace(dto.TriageUserName)
                    ? CurrentUser.UserName
                    : dto.TriageUserName;
                patientInfo.StartTriageTime = dto.StartTriageTime;
                patientInfo.TriageStatus = 1;

                // 分诊结果信息 
                patientInfo.ConsequenceInfo = new ConsequenceInfo
                {
                    ActTriageLevel = triageLevel.GetDescriptionByEnum(),
                    PatientInfoId = patientInfo.Id,
                    TriageTargetCode = relation.TriageDirectionCode,
                    TriageDeptCode = patientCountDto?.TriageDeptCode,
                    TriageDeptName = patientCountDto?.TriageDeptName,
                    DoctorName = patientCountDto?.RegisterDocName,
                    DoctorCode = patientCountDto?.RegisterDocCode
                };

                res.Add(patientInfo);
            }

            _log.Info("【GroupInjuryService】【GroupInjuryAutoSaveTriageInfoAsync】【群伤自动生成分诊记录Model结束】");
            return res;
        }

        /// <summary>
        /// 取消关联群伤事件
        /// </summary>
        /// <param name="triagePatientInfoId">分诊患者基本信息表ID</param>
        /// <returns></returns>
        [Auth("GroupInjury" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteGroupInjuryRecordAsync(Guid triagePatientInfoId)
        {
            try
            {
                var patient = await PatientInfoRepository.AsNoTracking().Include(i => i.ConsequenceInfo)
                    .FirstOrDefaultAsync(x => x.Id == triagePatientInfoId &&
                                              x.GroupInjuryInfoId != Guid.Empty);
                if (patient != null)
                {
                    patient.GroupInjuryInfoId = Guid.Empty;
                    await PatientInfoRepository.UpdateAsync(patient);

                    return JsonResult.Ok("取消关联成功");
                }

                _log.Error("【GroupInjuryService】【DeleteGroupInjuryRecordAsync】" +
                           "【取消关联群伤事件失败】【Msg：该患者群伤事件已被取消关联】");
                return JsonResult.Fail("该患者群伤事件已被取消关联");
            }
            catch (Exception e)
            {
                _log.Warning("【GroupInjuryService】【DeleteGroupInjuryRecordAsync】" +
                             $"【取消关联群伤事件错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 修改关联群伤事件
        /// </summary>
        /// <param name="groupInjuryId">群伤事件Id</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch]
        [Auth("GroupInjury" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> ModifyGroupInjuryRecordAsync(Guid groupInjuryId, UpdateGroupInjuryDto dto)
        {
            try
            {
                _log.Trace("【GroupInjuryService】【ModifyGroupInjuryRecordAsync】【修改关联群伤事件开始】");
                var model = await _groupInjuryInfoRepository.FirstOrDefaultAsync(x => x.Id == groupInjuryId);
                if (model != null)
                {
                    model.Description = dto.Description;
                    model.Remark = dto.Remark;
                    model.HappeningTime = dto.HappeningTime;
                    model.ModUser = CurrentUser.UserName;
                    model.GroupInjuryCode = dto.GroupInjuryTypeCode;
                    var dicts = await TriageConfigService.GetTriageConfigByRedisAsync();
                    model.GroupInjuryName =
                        dicts.GetNameByDictCode(TriageDict.GroupInjuryType, dto.GroupInjuryTypeCode);
                    await _groupInjuryInfoRepository.UpdateAsync(model);
                    _log.Info("【GroupInjuryService】【ModifyGroupInjuryRecordAsync】【修改关联群伤事件结束】");
                    return JsonResult.Ok();
                }

                _log.Error("【GroupInjuryService】【ModifyGroupInjuryRecordAsync】【修改关联群伤事件失败】" +
                           "【Msg：不存在此群伤事件】");
                return JsonResult.Fail("不存在此群伤事件");
            }
            catch (Exception e)
            {
                _log.Warning($"【GroupInjuryService】【ModifyGroupInjuryRecordAsync】【修改关联群伤事件错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 删除群伤事件
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [Auth("GroupInjury" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteGroupInjuryAsync(AddGroupInjuryForPatientDto where)
        {
            try
            {
                _log.Trace("【GroupInjuryService】【DeleteGroupInjuryRecordAsync】【删除群伤事件开始】");
                var model = await _groupInjuryInfoRepository.FirstOrDefaultAsync(x => x.Id == where.GroupInjuryId);
                if (await PatientInfoRepository.CountAsync(c => c.GroupInjuryInfoId == where.GroupInjuryId) > 0)
                {
                    _log.Error("【GroupInjuryService】【DeleteGroupInjuryRecordAsync】【删除伤事件失败】" +
                               "【Msg：群伤事件下有患者关联】");
                    return JsonResult.Fail("群伤事件下有患者关联");
                }

                if (model != null)
                {
                    await _groupInjuryInfoRepository.DeleteAsync(model);
                    _log.Info("【GroupInjuryService】【DeleteGroupInjuryRecordAsync】【删除群伤事件结束】");
                    return JsonResult.Ok();
                }

                _log.Error("【GroupInjuryService】【DeleteGroupInjuryRecordAsync】【删除伤事件失败】" +
                           "【Msg：不存在此群伤事件】");
                return JsonResult.Fail("不存在此群伤事件");
            }
            catch (Exception e)
            {
                _log.Warning($"【GroupInjuryService】【DeleteGroupInjuryRecordAsync】【删除群伤事件错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 获取群伤关联列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("GroupInjury" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<PagedResultDto<GroupInjuryOutput>>> GetGroupInjuryRecordListAsync(
            PagedGroupInjuryInput input)
        {
            try
            {
                _log.Trace("【GroupInjuryService】【GetGroupInjuryRecordListAsync】【获取群伤关联列表开始】");
                var list = await _groupInjuryInfoRepository.GetGroupInjuryPatientListAsync(input);
                _log.Info("【GroupInjuryService】【GetGroupInjuryRecordListAsync】【获取群伤关联列表结束】");
                return list;
            }
            catch (Exception e)
            {
                _log.Warning("【GroupInjuryService】【GetGroupInjuryRecordListAsync】" +
                             $"【获取群伤关联列表错误】【Msg：{e}】");
                return JsonResult<PagedResultDto<GroupInjuryOutput>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 获取单个患者的群伤事件
        /// </summary>
        /// <param name="triagePatientInfoId"></param>
        /// <returns></returns>
        [Auth("GroupInjury" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<GroupInjuryAndPatientInfoDto>> GetGroupInjuryRecordAsync(
            Guid triagePatientInfoId)
        {
            try
            {
                var dto = await _groupInjuryInfoRepository.GetGroupInjuryPatientAsync(triagePatientInfoId);
                return JsonResult<GroupInjuryAndPatientInfoDto>.Ok(data: dto);
            }
            catch (Exception e)
            {
                _log.Warning($"【GroupInjuryService】【GetGroupInjuryRecordAsync】【获取单个患者的群伤事件错误】【Msg{e}】");
                return JsonResult<GroupInjuryAndPatientInfoDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 关联群伤事件
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("GroupInjury" + PermissionDefinition.Separator + PermissionDefinition.Create)]
        public async Task<JsonResult> AddGroupInjuryForPatientAsync(AddGroupInjuryForPatientDto dto)
        {
            try
            {
                _log.Trace("【GroupInjuryService】【AddGroupInjuryForPatientAsync】【关联群伤事件开始】");
                if (string.IsNullOrWhiteSpace(dto.TriagePatientInfoIds))
                {
                    _log.Error("【GroupInjuryService】【AddGroupInjuryForPatientAsync】【关联群伤事件失败】【Msg：未选中要关联群伤事件的患者】");
                    return JsonResult.Fail("未选中要关联群伤事件的患者");
                }

                Expression<Func<PatientInfo, bool>> first = x => false;
                var param = Expression.Parameter(typeof(PatientInfo), "x");
                Expression body = Expression.Invoke(first, param);
                body = dto.TriagePatientInfoIds.Split(',').Select(s =>
                        (Expression<Func<PatientInfo, bool>>)(x => x.Id == Guid.Parse(s)))
                    .Aggregate(body, (current, exp) => Expression.OrElse(current, Expression.Invoke(exp, param)));
                var lambda = Expression.Lambda<Func<PatientInfo, bool>>(body, param);
                var patients = await PatientInfoRepository.AsNoTracking().Where(lambda)
                    .Include(i => i.ConsequenceInfo)
                    .ToListAsync();
                if (patients != null)
                {
                    var patientNameStr = "";
                    patients.ForEach(item =>
                    {
                        if (item.GroupInjuryInfoId != Guid.Empty)
                        {
                            patientNameStr += item.PatientName + ",";
                            return;
                        }

                        item.GroupInjuryInfoId = dto.GroupInjuryId;
                    });

                    if (patientNameStr != "")
                    {
                        return JsonResult.Fail("患者：" + patientNameStr + "已是群伤患者");
                    }
                    else
                    {
                        await PatientInfoRepository.UpdateRangeAsync(patients);
                        _log.Info("【GroupInjuryService】【AddGroupInjuryForPatientAsync】【关联群伤事件结束】");
                        return JsonResult.Ok();
                    }
                }

                _log.Error("【GroupInjuryService】【AddGroupInjuryForPatientAsync】【关联群伤事件失败】【Msg：选中的患者无分诊记录】");
                return JsonResult.Fail("选中的患者无分诊记录");
            }
            catch (Exception e)
            {
                _log.Warning($"【GroupInjuryService】【AddGroupInjuryForPatientAsync】【关联群伤事件错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }
    }
}