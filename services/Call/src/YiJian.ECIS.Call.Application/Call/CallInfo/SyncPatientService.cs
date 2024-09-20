using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SamJan.MicroService.PreHospital.TriageService;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using YiJian.ECIS.Call.CallCenter.Etos;
using YiJian.ECIS.Call.Domain;
using YiJian.ECIS.Call.Domain.CallCenter;
using YiJian.ECIS.Call.Domain.CallConfig;
using YiJian.ECIS.Call.Etos;
using YiJian.ECIS.Call.Patient;
using YiJian.ECIS.Core;
using YiJian.ECIS.Domain.Call;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.IMServiceEto;
using YiJian.ECIS.ShareModel.IMServiceEtos.Call;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 同步服务消息 todo 到时候删掉 北大暂时没有用到
    /// </summary>
    public class SyncPatientService : IScopedDependency, ICapSubscribe, IApplicationService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICallInfoRepository _callInfoRepository;
        private readonly CallInfoManager _callInfoManager;
        private readonly BaseConfigManager _baseConfigManager;
        private readonly SerialNoRuleManager _serialNoRuleManager;
        private readonly TriageLevelSettingOptions _triageLevelSettingOptions;
        private readonly TriageTargetSettingOptions _triageTargetSettingOptions;
        private readonly ICapPublisher _capPublisher;
        private readonly ILogger<SyncPatientService> _logger;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="departmentRepository"></param>
        /// <param name="callInfoRepository"></param>
        /// <param name="callInfoManager"></param>
        /// <param name="baseConfigManager"></param>
        /// <param name="capPublisher"></param>
        /// <param name="logger"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="serialNoRuleManager"></param>
        /// <param name="triageLevelSettingOptions"></param>
        /// <param name="triageTargetSettingOptions"></param>
        public SyncPatientService(IDepartmentRepository departmentRepository
            , ICallInfoRepository callInfoRepository
            , CallInfoManager callInfoManager
            , BaseConfigManager baseConfigManager
            , ICapPublisher capPublisher
            , ILogger<SyncPatientService> logger
            , IUnitOfWorkManager unitOfWorkManager
            , SerialNoRuleManager serialNoRuleManager
            , IOptions<TriageLevelSettingOptions> triageLevelSettingOptions
            , IOptions<TriageTargetSettingOptions> triageTargetSettingOptions
            ,IHttpClientFactory httpClientFactory
            ,IConfiguration configuration)
        {
            _departmentRepository = departmentRepository;
            _callInfoRepository = callInfoRepository;
            _callInfoManager = callInfoManager;
            _baseConfigManager = baseConfigManager;
            _serialNoRuleManager = serialNoRuleManager;
            _capPublisher = capPublisher;
            _logger = logger;
            _unitOfWorkManager = unitOfWorkManager;
            _triageLevelSettingOptions = triageLevelSettingOptions.Value;
            _triageTargetSettingOptions = triageTargetSettingOptions.Value;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        /// <summary>
        /// 创建叫号患者信息（订阅【预检分诊】微服务的【确认分诊】消息）
        /// 由于系统中存在2种业务流程:
        /// 1:【挂号】->【分诊】->【叫号】
        /// 2:【分诊】->【挂号】->【叫号】
        /// 所以对于订阅的消息，应该考虑2种情况：
        /// 1、存在挂号信息但不存在分诊信息（通过挂号流水号判断）
        /// 2、存在分诊信息但不存在挂号信息（通过患者分诊id判断）
        /// 以上2种情况，患者信息都不为空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [CapSubscribe("sync.patient.to.patientservice")]
        public async Task SubscribePatientInCallAsync()  //这个方法没有被调用到直接消费就行了
        {
             await Task.CompletedTask;
        }

        /// <summary>
        /// 同步患者修信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [CapSubscribe("modify.patient.info.from.patient.service")]
        public async Task SubscribeUpdatePatientInfoAsync(UpdatePatientInfoEto input)
        {
            using var uow = _unitOfWorkManager.Begin();

            try
            {
                var callInfo = await (await _callInfoRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.RegisterNo == input.RegisterNo && !x.IsDeleted);
                if (callInfo == null)
                {// 不存在的患者信息，则不处理该消息
                    return;
                }
                callInfo.UpdatePatientInfo(patientID: callInfo.PatientID, patientName: input.PatientName);

                await this._callInfoRepository.UpdateAsync(callInfo);

                await uow.CompleteAsync();
            }
            catch (Exception exception)
            {
                await uow.RollbackAsync();
                _logger.LogError(exception.Message);
                throw;
            }

        }

        /// <summary>
        /// 患者取消挂号 （作废）
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("sync.refundpatient.to.callservice")]
        public async Task PatientRegisterCancelledAsync(PatientRegisterCancelledEto eto)
        {
            using var uow = this._unitOfWorkManager.Begin();

            try
            {
                var callInfos = await (await this._callInfoRepository.GetQueryableAsync()).Where(x => eto.Contains(x.RegisterNo)).ToListAsync();
                //if (callInfo == null)
                //{
                //    Oh.Error("分诊记录不存在或未同步");
                //}
                foreach (var callInfo in callInfos)
                {
                    callInfo.RegisterRefund();
                }
                await this._callInfoRepository.UpdateManyAsync(callInfos);
                await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                await uow.RollbackAsync();
                throw;
            }

            await uow.CompleteAsync();
        }

        /// <summary>
        /// 同步就诊状态
        /// </summary>
        /// <param name="syncVisitStatus"></param>
        /// <returns></returns>
        [CapSubscribe("patient.visitstatus.changed")]
        public async Task SubscribeVisitStatusChangedAsync(SyncVisitStatusEto syncVisitStatus)
        {
            //获取患者信息（这里巨坑，paitent服务会同时将消息发送到triage和call，triage用pi_id接收，call用registerno接收，所以这里通过pi_id去获取registerno）
            Guid patientId;
            var isId= Guid.TryParse(syncVisitStatus.Id,out patientId);
            if (isId)
            {
                var patientInfo = await GetPatientInfoAsync(syncVisitStatus.Id);
                if (patientInfo != null)
                {
                    syncVisitStatus.Id = patientInfo.RegisterNo;
                }
            }
            using var uow = this._unitOfWorkManager.Begin();
            try
            {
                switch (syncVisitStatus.VisitStatus)
                {
                    case EVisitStatus.None:
                        break;
                    case EVisitStatus.NotRegister:
                        break;
                    // 召回患者到待就诊
                    case EVisitStatus.WaittingTreat:
                        await this._callInfoManager.SendBackWaitingAsync(syncVisitStatus.Id);
                        // 发布叫号状态变化消息，由诊疗等服务订阅
                        await this._capPublisher.PublishAsync("call.callstatus.changed", new SyncCallStatusEto(syncVisitStatus.Id, CallStatus.NotYet, DateTime.Now));
                        break;
                    // 患者已过号
                    case EVisitStatus.UntreatedOver:
                        await this._callInfoManager.UntreatedOverAsync(syncVisitStatus.Id);
                        // 发布叫号状态变化消息，由诊疗等服务订阅
                        await this._capPublisher.PublishAsync("call.callstatus.changed", new SyncCallStatusEto(syncVisitStatus.Id, CallStatus.Over, DateTime.Now));
                        break;
                    case EVisitStatus.RefundNo:
                        break;
                    // 患者正在就诊
                    case EVisitStatus.Treating:
                        await this._callInfoManager.TreatAsync(syncVisitStatus.Id);
                        // 发布叫号状态变化消息，由诊疗等服务订阅
                        await this._capPublisher.PublishAsync("call.callstatus.changed", new SyncCallStatusEto(syncVisitStatus.Id, CallStatus.Over, DateTime.Now));
                        break;
                    // 患者已结束就诊
                    case EVisitStatus.Treated:
                        await this._callInfoManager.TreatFinishAsync(syncVisitStatus.Id);
                        // 发布叫号状态变化消息，由诊疗等服务订阅
                        await this._capPublisher.PublishAsync("call.callstatus.changed", new SyncCallStatusEto(syncVisitStatus.Id, CallStatus.Over, DateTime.Now));
                        break;
                    case EVisitStatus.OutDepartment:
                        await this._callInfoManager.OutDepartmentAsync(syncVisitStatus.Id);
                        // 发布叫号状态变化消息，由诊疗等服务订阅
                        await this._capPublisher.PublishAsync("call.callstatus.changed", new SyncCallStatusEto(syncVisitStatus.Id, CallStatus.Over, DateTime.Now));
                        break;
                    default: break;
                }
                // 发布叫号列表变化的消息
                await this._capPublisher.PublishAsync("im.call.queue.changed", new DefaultBroadcastEto());

                await uow.CompleteAsync();

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="piid"></param>
        /// <returns></returns>
        private async Task<AdmissionRecordDto> GetPatientInfoAsync(string piid)
        {
            try
            {
                using (HttpClient client = _httpClientFactory.CreateClient("patient"))
                {
                    string uri = _configuration["PatientInfoUri"] + piid;
                    var patientInfo = await client.GetAsync(uri);
                    if (patientInfo.StatusCode != System.Net.HttpStatusCode.OK) return null;

                    var content = await patientInfo.Content.ReadAsStringAsync();
                    if (content.IsNullOrWhiteSpace()) return null;

                    var data = JsonConvert.DeserializeObject<DDPResult<AdmissionRecordDto>>(content);
                    if (data.code == 200)
                    {
                        var patient = data.data;
                        return patient;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取患者信息PI_ID={piid}异常：{ex.Message}{ex.StackTrace}");
                return null;
            }
        }

        /// <summary>
        /// 通过即时通讯微服务向指定客户端发送刷新叫号列表消息（仅供测试使用）
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("im.call.refresh.queue.request")]
        public async Task SubscribeClientRefreshRequestAsync(DefaultClientEto message)
        {
            using var uow = this._unitOfWorkManager.Begin();

            await this._capPublisher.PublishAsync("im.call.queue.changed.client", new DefaultClientEto(message.ConnectionId, message.Method));
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            await uow.CompleteAsync();
        }

        /// <summary>
        /// 从诊疗服务同步患者流转记录
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("sync.patient.transfer.to.callservice")]
        public async Task SyncTransferFromMqAsync(SyncTransferEto message)
        {
            using var uow = this._unitOfWorkManager.Begin();

            try
            {
                CallInfo callInfo = await (await this._callInfoRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.RegisterNo == message.RegisterNo);
                if (callInfo == null)
                {
                    Oh.Error("患者信息不存在");
                }
                callInfo.Transfer(message.TransferTypeCode, message.ToDeptCode, message.ToDeptName);
                if (message.TransferTypeCode == TransferType.OutpatientArea)
                {// 病人转就诊区， 分配排队号                
                    await SetCallSnAsync(callInfo);
                }

                _ = await this._callInfoRepository.UpdateAsync(callInfo);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                await uow.RollbackAsync();
                throw;
            }

            await uow.CompleteAsync();
        }

        /// <summary>
        /// 从预检分诊服务同步患者就诊状态
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("sync.visitstatus.from.triage.to.callservice")]
        public async Task SyncVisitStatusFromTriageAsync(SyncVisitStatusV2Eto syncVisitStatus)
        {
            using var uow = this._unitOfWorkManager.Begin();
            try
            {
                switch (syncVisitStatus.VisitStatus)
                {
                    case EVisitStatus.None:
                        break;
                    case EVisitStatus.NotRegister:
                        break;
                    // 召回患者到待就诊
                    case EVisitStatus.WaittingTreat:
                        await this._callInfoManager.SendBackWaitingAsync(syncVisitStatus.Id);
                        // 发布叫号状态变化消息，由诊疗等服务订阅
                        await this._capPublisher.PublishAsync("call.callstatus.changed", new SyncCallStatusEto(syncVisitStatus.Id, CallStatus.NotYet, DateTime.Now));
                        break;
                    // 患者已过号
                    case EVisitStatus.UntreatedOver:
                        await this._callInfoManager.UntreatedOverAsync(syncVisitStatus.Id);
                        // 发布叫号状态变化消息，由诊疗等服务订阅
                        await this._capPublisher.PublishAsync("call.callstatus.changed", new SyncCallStatusEto(syncVisitStatus.Id, CallStatus.Over, DateTime.Now));
                        break;
                    case EVisitStatus.RefundNo:
                        await this._callInfoManager.RefundNoAsync(syncVisitStatus.RegisterNo);
                        // 发布叫号状态变化消息，由诊疗等服务订阅
                        await this._capPublisher.PublishAsync("call.callstatus.changed", new SyncCallStatusEto(syncVisitStatus.Id, CallStatus.Refund, DateTime.Now));
                        break;
                    // 患者正在就诊
                    case EVisitStatus.Treating:
                        await this._callInfoManager.TreatAsync(syncVisitStatus.Id);
                        // 发布叫号状态变化消息，由诊疗等服务订阅
                        await this._capPublisher.PublishAsync("call.callstatus.changed", new SyncCallStatusEto(syncVisitStatus.Id, CallStatus.Over, DateTime.Now));
                        break;
                    // 患者已结束就诊
                    case EVisitStatus.Treated:
                        await this._callInfoManager.TreatFinishAsync(syncVisitStatus.Id);
                        // 发布叫号状态变化消息，由诊疗等服务订阅
                        await this._capPublisher.PublishAsync("call.callstatus.changed", new SyncCallStatusEto(syncVisitStatus.Id, CallStatus.Over, DateTime.Now));
                        break;
                    case EVisitStatus.OutDepartment:
                        await this._callInfoManager.OutDepartmentAsync(syncVisitStatus.Id);
                        // 发布叫号状态变化消息，由诊疗等服务订阅
                        await this._capPublisher.PublishAsync("call.callstatus.changed", new SyncCallStatusEto(syncVisitStatus.Id, CallStatus.Over, DateTime.Now));
                        break;
                    default: break;
                }
                // 发布叫号列表变化的消息
                await this._capPublisher.PublishAsync("im.call.queue.changed", new DefaultBroadcastEto());

                await uow.CompleteAsync();

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                await uow.RollbackAsync();
                throw;
            }
        }

        #region Private Method

        /// <summary>
        /// 校验订阅消息（来源：分诊/挂号）
        /// </summary>
        /// <param name="input"></param>
        private Task ValidateSubscribeMessageFromTriageAsync(PatientInfoMqEto input)
        {
            if (input.Id == Guid.Empty && string.IsNullOrEmpty(input.RegisterNo))
            {// 既无分诊信息亦无挂号信息，则认为消息无法正常处理
                Oh.Error("既无分诊信息亦无挂号信息，无法正常处理消息");
                //throw new EcisBusinessException("Call:triage-has-not-consequence-and-register");
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 不存在科室的话就穿件科室
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        private async Task CreateDeptWhenNoExistsAsync(string deptCode, string deptName)
        {
            if (!string.IsNullOrWhiteSpace(deptCode))
            {
                // 使用科室代码与预检分诊的科室关联，查询当前服务的科室
                var department = await this._departmentRepository.GetByCodeAsync(deptCode);
                if (department is null)
                {// 当前服务不存在对应科室
                    _ = await _departmentRepository.InsertAsync(new Department(Guid.NewGuid(), deptName, deptCode, true));
                }
            }
        }

        /// <summary>
        /// 同步叫号患者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task SyncPatientToCurrentServiceAsync(PatientInfoMqEto input)
        {
            var callInfo = await this._callInfoManager.GetInfoByTriageOrRegisterAsync(input.Id, input.RegisterNo);
            // 科室、去向变更，分配新的排队号
            bool hasChangedDept = callInfo?.TriageDept != input.ConsequenceInfo.TriageDeptCode;
            // 将队列消息的信息赋值到叫号信息中
            callInfo = GetOrNewCallInfo(input, callInfo);

            if (!string.IsNullOrEmpty(input.RegisterNo))
            {// 挂号
                callInfo.Register(input.RegisterNo);
            }
            // 【患者分诊ID】存在则说明已分诊，此时绑定分诊信息
            if (input.Id != Guid.Empty)
            {
                // 关联分诊信息
                callInfo.LinkConsequenceInfo(
                    actTriageLevel: input.ConsequenceInfo.ActTriageLevel,
                    actTriageLevelName: this._triageLevelSettingOptions[input.ConsequenceInfo.ActTriageLevel].Display);
            }

            // 已挂号、已分诊
            if (!string.IsNullOrEmpty(callInfo.RegisterNo) && !string.IsNullOrEmpty(callInfo.RegisterNo) && !string.IsNullOrEmpty(input.ConsequenceInfo.TriageDeptCode) &&
                    // 未分配排队号的情况下，分配排队号。 科室、去向变更，分配新的排队号
                    (string.IsNullOrEmpty(callInfo.CallingSn) || hasChangedDept))
            {
                // 分配排队号
                await SetCallSnAsync(callInfo);
            }

            // 保存叫号患者信息
            await this._callInfoManager.InsertOrUpdateAsync(callInfo);
            // 叫号排队号生成
            await this._capPublisher.PublishAsync("call.callstatus.changed",
                new SyncCallStatusEto(callInfo.RegisterNo, callInfo.CallingSn, CallStatus.NotYet, null, callInfo.LogDate, callInfo.LogTime));
        }

        /// <summary>
        /// 分配排队号
        /// </summary>
        /// <param name="callInfo"></param>
        /// <returns></returns>
        private async Task SetCallSnAsync(CallInfo callInfo)
        {
            // 使用科室代码与预检分诊的科室关联，查询当前服务的科室
            var department = await this._departmentRepository.GetByCodeAsync(callInfo.TriageDept);
            if (department is null)
            {// 当前服务不存在对应科室
                department = await _departmentRepository.InsertAsync(new Department(Guid.NewGuid(), callInfo.TriageDeptName, callInfo.TriageDept, true));
            }
            // 获取排队号
            var callingSn = await this._serialNoRuleManager.GetSerialNoAsync(department.Id, callInfo.ActTriageLevel);
            // 获取当前工作日（比如凌晨的时间，可能根据设置需要算入上一个工作日）
            var tomorrowConfig = await this._baseConfigManager.GetTomorrowBeginAsync();
            DateTime logDate = (DateTime.Now.Hour < tomorrowConfig.Hour || (DateTime.Now.Hour == tomorrowConfig.Hour && DateTime.Now.Minute < tomorrowConfig.Minute))
                               ? DateTime.Today.AddDays(-1) : DateTime.Today;
            callInfo.SetCallingSn(callingSn, logDate);
        }

        /// <summary>
        /// 获取已存在的叫号信息，不存在时新建叫号信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="callInfo"></param>
        /// <returns></returns>
        private CallInfo GetOrNewCallInfo(PatientInfoMqEto input, CallInfo callInfo)
        {
            bool isShow = true;
            var levelSetting = this._triageLevelSettingOptions[input.ConsequenceInfo.ActTriageLevel];
            if (levelSetting == null || !levelSetting.IsSync)
            {
                isShow = false;
            }
            var targetSetting = this._triageTargetSettingOptions[input.ConsequenceInfo.TriageTargetCode];
            if (targetSetting == null || !targetSetting.IsSync)
            {
                isShow = false;
            }
            // 由于分诊、挂号2个业务均触发叫号列表的变化，所以当记录存在时，执行更新操作
            // 如果不存在已有记录，则新建一条叫号记录
            if (callInfo == null)
            {
                callInfo = new(
                    Guid.NewGuid(),
                    patientID: input.PatientID,
                    patientName: input.PatientName
                    );
            }
            else
            {
                callInfo.UpdatePatientInfo(
                    patientID: input.PatientID,
                    patientName: input.PatientName
                    );
            }
            // 根据产品要求，患者从就诊区转出到其他区域时，需要删除叫号记录
            // 当从其他区转入就诊区时，则需要恢复数据
            callInfo.SetVisible(isShow);

            return callInfo;
        }

        #endregion
    }
}
