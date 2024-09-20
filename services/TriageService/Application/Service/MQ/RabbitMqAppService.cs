using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;
using TriageService.Application.Dtos.Triage.Patient;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace SamJan.MicroService.PreHospital.TriageService
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RabbitMqAppService : ApplicationService, IRabbitMqAppService
    {
        private readonly ILogger<RabbitMqAppService> _log;
        private readonly IDistributedEventBus _eventBus;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true // Optional: Make the JSON output indented for readability
        };

        private ICapPublisher _capPublisher;
        private ICapPublisher CapPublisher => LazyGetRequiredService(ref _capPublisher);

        public RabbitMqAppService(ILogger<RabbitMqAppService> log, IDistributedEventBus eventBus, IConfiguration configuration,
            IHttpClientHelper httpClientHelper)
        {
            _log = log;
            _eventBus = eventBus;
            _configuration = configuration;
            _httpClientHelper = httpClientHelper;
        }

        /// <summary>
        /// 发布六大中心同步病患队列消息
        /// </summary>
        /// <param name="dtos"></param>
        /// <param name="dicts"></param>
        /// <returns></returns>
        public async Task PublishSixCenterSyncPatientAsync(List<SyncPatientEventBusEto> dtos, Dictionary<string, List<TriageConfigDto>> dicts)
        {
            try
            {
                _log.LogInformation("发布六大中心同步病患队列消息开始");
                dtos = dtos.FindAll(x => !string.IsNullOrWhiteSpace(x.WebSite));
                if (dtos.Count <= 0)
                {
                    _log.LogError("发布六大中心同步病患队列失败！原因：{Msg}", "没有需要同步的绿通患者");
                    return;
                }

                foreach (var item in dtos)
                {
                    #region 六大中心2.0同步病患

                    var level = dicts[TriageDict.TriageLevel.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == item.ActTriageLevel);
                    item.ActTriageLevel = level == null ? "-1" : level.SixCenterValue;

                    var greenRoad = dicts[TriageDict.GreenRoad.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == item.WebSite);
                    item.WebSite = greenRoad != null ? greenRoad.SixCenterValue : "-1";

                    var sex = dicts[TriageDict.Sex.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == item.Sex);
                    item.Sex = sex != null ? sex.SixCenterValue : "-1";

                    var toHospitalWay = dicts[TriageDict.ToHospitalWay.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == item.ToHospitalWayCode);
                    item.ToHospitalWayCode = toHospitalWay == null ? "-1" : toHospitalWay.SixCenterValue;
                    item.DepId = _configuration["Settings:SixCenter:DepId"];
                    item.DepName = _configuration["Settings:SixCenter:DepName"];
                    await _eventBus.PublishAsync(item);

                    #endregion

                    #region 六大中心1.0同步病患

                    //是否需要同步到卒中1.0
                    if (!Convert.ToBoolean(_configuration["Settings:SixCenter:IsSyncSixCenterV1"])) continue;
                    var uri = _configuration["Settings:SixCenter:SyncApiUrl"] + "/" + item.Pid +
                              $"?app={item.WebSite}&TaskId=" + item.TaskId;
                    await _httpClientHelper.GetAsync(uri);

                    #endregion
                }


                _log.LogInformation("发布六大中心同步病患队列消息成功");
            }
            catch (Exception e)
            {
                _log.LogError("发布六大中心同步病患队列消息错误！原因:{Msg}", e);
            }
        }

        /// <summary>
        /// 发布同步病患到急诊诊疗服务队列消息
        /// </summary>
        /// <param name="mqDtos"></param>
        public async Task PublishEcisPatientSyncPatientAsync(List<PatientInfoMqDto> mqDtos)
        {
            try
            {
                _log.LogInformation("发布同步病患到急诊诊疗服务队列消息开始");

                //暂存患者不同步
                foreach (var patientMqDto in mqDtos.Where(patientMqDto => patientMqDto.PatientInfo.TriageStatus != 0))
                {
                    _log.LogDebug($"发布同步病患到急诊诊疗服务队列消息，患者为：{patientMqDto.PatientInfo.PatientName}，其PatientInfoMqDto为：{JsonSerializer.Serialize(patientMqDto, options)}");

                    // 与诊疗服务使用同一个消息即可，由叫号微服务判断是否接受处理数据  by: ywlin 2021-11-22
                    ////是否需要同步病患到急诊叫号微服务
                    //if (Convert.ToBoolean(_configuration["Settings:IsPublishMqMsgToEcisCall"]))
                    //{
                    //    if (patientMqDto.ConsequenceInfo.ActTriageLevel != TriageLevel.FirstLv.GetDescriptionByEnum()
                    //        && patientMqDto.ConsequenceInfo.ActTriageLevel != TriageLevel.SecondLv.GetDescriptionByEnum())
                    //    {
                    //        await CapPublisher.PublishAsync("sync.patient.to.callservice", patientMqDto);
                    //    }
                    //}

                    //是否需要同步病患到急诊诊疗微服务
                    if (Convert.ToBoolean(_configuration["Settings:IsPublishMqMsgToEcisPatient"]))
                    {
                        if (patientMqDto.RegisterInfo.IsCancelled)
                        {
                            //同步作废状态到patient服务
                            await CapPublisher.PublishAsync("sync.patient.visitstatus.from.triageservice",
                                new SyncVisitStatusDto(patientMqDto.PatientInfo.TriagePatientInfoId, EVisitStatus.RefundNo));
                        }
                        else
                        {
                            await CapPublisher.PublishAsync("sync.patient.to.patientservice", patientMqDto);
                        }
                    }
                }

                _log.LogInformation("发布同步病患到急诊诊疗服务队列消息成功");
            }
            catch (Exception e)
            {
                _log.LogError("发布同步病患到急诊诊疗服务队列消息错误！原因：{Msg}", e);
            }
        }

        /// <summary>
        /// 发布同步病患状态到急诊诊疗服务队列消息
        /// </summary>
        /// <param name="mqDtos"></param>
        /// <exception cref="NotImplementedException"></exception>
        public async Task PublishEcisPatientStatusAsync(List<PatientInfoFromHisStatusDto> mqDtos)
        {
            try
            {
                _log.LogInformation("发布同步病患到急诊诊疗服务队列消息开始");

                await CapPublisher.PublishAsync("sync.patient.to.triagebyhisservice", mqDtos);
            }
            catch (Exception e)
            {
                _log.LogError("发布同步病患到急诊诊疗服务队列消息错误！原因：{Msg}", e);
            }
        }


        /// <summary>
        /// 发送评分记录到六大中心
        /// </summary>
        /// <param name="scoreInfos"></param>
        public async Task PublishScoreRecordToSixCenterAsync(List<ScoreInfo> scoreInfos)
        {
            try
            {
                if (scoreInfos == null || scoreInfos.Count <= 0)
                {
                    return;
                }

                var scoreDictRepository = ServiceProvider.GetRequiredService<IRepository<ScoreDict, Guid>>();
                var scoreDicts = await scoreDictRepository.ToListAsync();
                var scoreDictDtos = scoreDicts.BuildAdapter().AdaptToType<List<ScoreDictDto>>();
                //scoreDictDtos = GetScoreDictTree(scoreDictDtos);
                var eventBus = ServiceProvider.GetRequiredService<IDistributedEventBus>();
                var syncCategory = new List<string>();
                _configuration.Bind("Settings:SixCenter:Score:SyncCategory", syncCategory);
                foreach (var scoreInfo in scoreInfos)
                {
                    if (syncCategory.Exists(x => x == scoreInfo.ScoreType))
                    {
                        var gcsScore = JsonHelper.DeserializeObject<GcsScoreDto>(scoreInfo.ScoreContent);
                        var eto = new ScoringResultEvents
                        {
                            Pid = scoreInfo.PatientInfoId,
                            Score = scoreInfo.ScoreValue,
                            Name = CurrentUser.GetFullName(),
                            ScoreName = scoreInfo.ScoreType
                        };

                        eto.DetailIdList ??= new List<Guid>();

                        var eye = scoreDictDtos.FirstOrDefault(x => x.Category == "eyesVal");
                        if (eye != null)
                        {
                            var eyeChildren = scoreDictDtos.FindAll(x => x.ParentId == eye.Id)
                                .OrderBy(o => o.Sort)
                                .ToList()[Convert.ToInt32(gcsScore.EyesVal)];
                            eto.DetailIdList.Add(eyeChildren.Id);
                        }

                        var lang = scoreDictDtos.FirstOrDefault(x => x.Category == "langVal");
                        if (lang != null)
                        {
                            var langChildren = scoreDictDtos.FindAll(x => x.ParentId == lang.Id)
                                .OrderBy(o => o.Sort)
                                .ToList()[Convert.ToInt32(gcsScore.LangVal)];
                            eto.DetailIdList.Add(langChildren.Id);
                        }

                        var motion = scoreDictDtos.FirstOrDefault(x => x.Category == "motionVal");
                        if (motion != null)
                        {
                            var motionChildren = scoreDictDtos.FindAll(x => x.ParentId == motion.Id)
                                .OrderBy(o => o.Sort)
                                .ToList()[Convert.ToInt32(gcsScore.MotionVal)];
                            eto.DetailIdList.Add(motionChildren.Id);
                        }

                        switch (Convert.ToInt32(_configuration["Settings:SixCenter:Score:SyncMode"]))
                        {
                            case 1:
                                await eventBus.PublishAsync(eto);
                                break;
                            case 2:
                                var uri = _configuration["Settings:SixCenter:Score:ApiUrl"];
                                var content = new StringContent(JsonHelper.SerializeObject(eto));
                                await _httpClientHelper.PostAsync(uri, content);
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _log.LogError("发送评分记录到六大中心错误，原因：{Msg}", e);
            }
        }

        /// <summary>
        /// 发布退号消息到Call服务
        /// </summary>
        /// <param name="mqDtos"></param>
        /// <returns></returns>
        public async Task PublishEcisRefundPatientToCallAsync(HashSet<string> mqDtos)
        {
            try
            {
                _log.LogInformation("发布退号消息到Call服务开始");

                await CapPublisher.PublishAsync("sync.refundpatient.to.callservice", mqDtos);
            }
            catch (Exception e)
            {
                _log.LogError("发布退号消息到Call服务错误！原因：{Msg}", e);
            }
        }
    }
}