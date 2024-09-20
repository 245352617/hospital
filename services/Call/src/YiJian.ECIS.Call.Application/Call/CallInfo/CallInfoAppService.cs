using System;
using System.Linq;
using YiJian.Apis;
using Volo.Abp.Uow;
using DotNetCore.CAP;
using YiJian.ECIS.DDP;
using System.Threading.Tasks;
using YiJian.ECIS.Call.Domain;
using YiJian.ECIS.Domain.Call;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using YiJian.ECIS.ShareModel.DDPs;
using Volo.Abp.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using YiJian.ECIS.Call.CallCenter.Dtos;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.Call.Domain.CallCenter;
using YiJian.ECIS.Call.Domain.CallConfig;
using YiJian.ECIS.ShareModel.IMServiceEto;
using YiJian.ECIS.ShareModel.IMServiceEtos.Call;
using SamJan.MicroService.PreHospital.TriageService;
using Microsoft.AspNetCore.Authorization;
using DotNetCore.CAP.Messages;
using Volo.Abp.ObjectMapping;

namespace YiJian.ECIS.Call.CallCenter
{
    /// <summary>
    /// 【叫号患者列表】
    /// </summary>
    [Route("/api/call/call-info")]
    public class CallInfoAppService : CallAppService, ICallInfoAppService, ICapSubscribe
    {
        private readonly ICapPublisher _capPublisher;
        private readonly ICallInfoRepository _callInfoRepository;
        private readonly ICallingRecordRepository _callingRecordRepository;
        private readonly DepartmentManager _departmentManager;
        private readonly IConsultingRoomRepository _consultingRoomRepository;
        private readonly CallInfoManager _callInfoManager;
        private readonly BaseConfigManager _baseConfigManager;
        private readonly ILogger<CallInfoAppService> _logger;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly SerialNoRuleManager _serialNoRuleManager;
        private readonly IDdpApiService _ddpApiService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="capPublisher"></param>
        /// <param name="callInfoRepository"></param>
        /// <param name="callingRecordRepository"></param>
        /// <param name="departmentManager"></param>
        /// <param name="consultingRoomRepository"></param>
        /// <param name="callInfoManager"></param>
        /// <param name="logger"></param>
        /// <param name="baseConfigManager"></param>
        /// <param name="serialNoRuleManager"></param>
        /// <param name="departmentRepository"></param>
        /// <param name="ddpHospitalOptionsMonitor"></param>
        /// <param name="ddpSwitch"></param>
        public CallInfoAppService(ICapPublisher capPublisher
            , ICallInfoRepository callInfoRepository
            , ICallingRecordRepository callingRecordRepository
            , DepartmentManager departmentManager
            , IConsultingRoomRepository consultingRoomRepository
            , CallInfoManager callInfoManager
            , ILogger<CallInfoAppService> logger
            , BaseConfigManager baseConfigManager
            , SerialNoRuleManager serialNoRuleManager
            , IDepartmentRepository departmentRepository
            , IOptionsMonitor<DdpHospital> ddpHospitalOptionsMonitor
            , DdpSwitch ddpSwitch)
        {
            _capPublisher = capPublisher;
            _callInfoRepository = callInfoRepository;
            _callingRecordRepository = callingRecordRepository;
            _departmentManager = departmentManager;
            _consultingRoomRepository = consultingRoomRepository;
            _callInfoManager = callInfoManager;
            _logger = logger;
            _baseConfigManager = baseConfigManager;
            _departmentRepository = departmentRepository;
            _serialNoRuleManager = serialNoRuleManager;
            _ddpApiService = ddpSwitch.CreateService(ddpHospitalOptionsMonitor.CurrentValue);
        }

        /// <summary>
        /// 患者入队列 （北大属于先挂号后分诊）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("in-queue")]
        public async Task<CallInfoData> PatientInQueueAsync(InQueueCallInfoEto input)
        {
            try
            {
                if (string.IsNullOrEmpty(input.RegisterNo))
                    Oh.Error("无挂号信息，无法正常处理消息");

                // 不存在科室的话就error
                if (!string.IsNullOrWhiteSpace(input.TriageDeptCode))
                {
                    // 使用科室代码与预检分诊的科室关联，查询当前服务的科室
                    var department = await _departmentRepository.GetByCodeAsync(input.TriageDeptCode);
                    if (department is null)
                        // 当前服务不存在对应科室
                        Oh.Error("当前科室信息不存在，请确认科室已经添加后再重试");
                    if (!department.IsActived)
                        Oh.Error("当前科室已禁用");
                }
                var callInfo = ObjectMapper.Map<InQueueCallInfoEto, CallInfo>(input);
                await SetCallSnAsync(callInfo);
                callInfo.IsShow = true;
                callInfo.CreationTime = DateTime.Now;
                //插入之前先删除 保证registerNo 唯一
                await _callInfoRepository.DeleteAsync(c => c.RegisterNo == callInfo.RegisterNo);
                var entity = await _callInfoRepository.InsertAsync(callInfo);
                //刷新队列
                await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
                return ObjectMapper.Map<CallInfo, CallInfoData>(entity);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                Oh.Error(exception.Message);
                throw;
            }
        }

        /// <summary>
        /// 根据base配置查看是何种模式，获取实际的RoomCode
        /// </summary>
        /// <returns></returns>
        private async Task<ConsultingRoom> GetRoomCodeAsync(string roomCode, string ipAddr)
        {
            var config = await _baseConfigManager.GetCurrentConfigAsync();
            switch (config.TomorrowCallMode)
            {
                case CallMode.ConsultingRoomRegular:
                    //诊室固定模式根据iP 获取roomCode 
                    var consultingRoom = await this._consultingRoomRepository.FirstOrDefaultAsync(x => x.IP == ipAddr);
                    if (consultingRoom is null)
                    {
                        // 诊室不存在
                        Oh.Error("诊室固定模式必须传递正确Ip地址！");
                    }
                    return consultingRoom;
                case CallMode.DoctorRegular:
                    var consultingRoom2 = await this._consultingRoomRepository.FirstOrDefaultAsync(x => x.Code == roomCode);
                    if (consultingRoom2 is null)
                    {
                        Oh.Error("诊室不存在");
                    }
                    return consultingRoom2;

                default:
                    Oh.Error("请检查后台配置，配置对应的叫号模式");
                    return default;
            }
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
                await UnitOfWorkManager.Current.SaveChangesAsync();
            }
            // 获取排队号
            var callingSn = await this._serialNoRuleManager.GetSerialNoAsync(department.Id, callInfo.ActTriageLevel);
            // 获取当前工作日（比如凌晨的时间，可能根据设置需要算入上一个工作日）
            var tomorrowConfig = await this._baseConfigManager.GetTomorrowBeginAsync();
            DateTime logDate = (DateTime.Now.Hour < tomorrowConfig.Hour || (DateTime.Now.Hour == tomorrowConfig.Hour && DateTime.Now.Minute < tomorrowConfig.Minute)) ? DateTime.Today.AddDays(-1) : DateTime.Today;
            callInfo.SetCallingSn(callingSn, logDate);
        }

        /// <summary>
        /// 获取指定叫号信息
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        [HttpGet("{registerNo}")]
        public async Task<CallInfoData> GetAsync(string registerNo)
        {
            var item = await this._callInfoRepository.FirstOrDefaultAsync(x => x.RegisterNo == registerNo);

            return ObjectMapper.Map<CallInfo, CallInfoData>(item);
        }

        /// <summary>
        /// 获取列表带sort
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<List<CallInfoData2>> GetListAsync(GetCallInfoInput2 input)
        {
            var query = (await this._callInfoRepository.GetQueryableAsync()).Where(new DefaultCallInfoSpecification())
             .WhereIf(input.CallingStatus > 0, x => x.CallStatus == input.CallingStatus)
             .WhereIf(!string.IsNullOrEmpty(input.ActTriageLevel), x => x.ActTriageLevel == input.ActTriageLevel)
             .WhereIf(!string.IsNullOrEmpty(input.TriageDept), x => x.TriageDept == input.TriageDept)
             .WhereIf(!string.IsNullOrEmpty(input.Filter), x => x.PatientName.Contains(input.Filter))
             .Where(x => x.InCallQueueTime >= DateTime.Now.AddDays(-1));  // 队列只保留24小时

            var list = await this.OrderByCallingStatus(query).ToListAsync();

            var dtoList = ObjectMapper.Map<List<CallInfo>, List<CallInfoData2>>(list);

            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
            foreach (var item in dtoList)
            {
                if (keyValuePairs.ContainsKey(item.TriageDept))
                {
                    keyValuePairs[item.TriageDept] = keyValuePairs[item.TriageDept] + 1;
                    item.DeptSort = keyValuePairs[item.TriageDept];
                }
                else
                {
                    keyValuePairs[item.TriageDept] = 1;
                    item.DeptSort = 1;
                }
            }

            return new List<CallInfoData2>(dtoList.AsReadOnly());

        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns></returns>
        [HttpPost("paged-list")]
        public async Task<PagedResultDto<CallInfoData>> GetPagedListAsync(GetCallInfoInput input)
        {

            var query = (await this._callInfoRepository.GetQueryableAsync())  //.Where(new DefaultCallInfoSpecification())
                .Where(new ClientCallInfoSpecification())
                //.WhereIf(input.CallingStatus > 0, x => x.CallStatus == input.CallingStatus)
                .WhereIf(!string.IsNullOrEmpty(input.ActTriageLevel), x => x.ActTriageLevel == input.ActTriageLevel)
                .WhereIf(!string.IsNullOrEmpty(input.TriageDept), x => x.TriageDept == input.TriageDept)
                .WhereIf(!string.IsNullOrEmpty(input.Filter), x => x.PatientName.Contains(input.Filter));

            var orderQuery = this.OrderByCallingStatus(query);
            var list = await orderQuery.PageBy(input.SkipCount, input.Size)
                .ToListAsync();
            var count = await orderQuery.LongCountAsync();

            var dtoList = ObjectMapper.Map<List<CallInfo>, List<CallInfoData>>(list);
            var returnList = new PagedResultDto<CallInfoData>(count, dtoList.AsReadOnly());
            return returnList;
        }

        /// <summary>
        /// 分页查询（大屏终端）
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns></returns>
        [HttpGet("client/paged-list")]
        [AllowAnonymous]
        public async Task<PagedResultDto<CallClientData>> GetClientPagedListAsync(GetClientCallInfoInput input)
        {
            var query = (await _callInfoRepository.GetQueryableAsync())
                .Where(new ClientCallInfoSpecification())
                .WhereIf(!string.IsNullOrEmpty(input.TriageDept), x => x.TriageDept == input.TriageDept);
            var orderQuery = OrderByCallingStatus(query);
            var list = await orderQuery.PageBy(input.SkipCount, input.Size)
                .ToListAsync();
            var count = await orderQuery.LongCountAsync();
            var dtoList = ObjectMapper.Map<List<CallInfo>, List<CallClientData>>(list);

            return new PagedResultDto<CallClientData>(count, dtoList.AsReadOnly());
        }

        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="registerNo"> registerNo</param>
        /// <returns></returns>
        [HttpPut("{registerNo}/move-to-top")]
        public async Task<CallInfoData> MoveToTopAsync(string registerNo)
        {
            CallInfo callInfo = await _callInfoRepository.FirstOrDefaultAsync(x => x.RegisterNo == registerNo);
            if (callInfo is null)
                // 叫号信息不存在 
                Oh.Error("叫号信息不存在");

            switch (callInfo.CallStatus)
            {
                case CallStatus.Calling:
                    Oh.Error("叫号中患者无法优先");
                    break;
                case CallStatus.Pause:
                    Oh.Error("暂停中患者无法优先");
                    break;
                case CallStatus.Over:
                    Oh.Error("已经叫号患者无法优先");
                    break;
                case CallStatus.Exceed:
                    Oh.Error("已经过号无法优先");
                    break;
                case CallStatus.Refund:
                    Oh.Error("已作废患者无法优先");
                    break;
                default:
                    break;
            }
            callInfo.MoveToTop();
            await _callInfoRepository.UpdateAsync(callInfo);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            return await GetAsync(callInfo.RegisterNo);
        }

        /// <summary>
        /// 取消置顶
        /// </summary>
        /// <param name="registerNo"> registerNo</param>
        /// <returns></returns>
        [HttpPut("{registerNo}/cancel-move-to-top")]
        public async Task<CallInfoData> CancelMoveToTopAsync(string registerNo)
        {
            CallInfo callInfo = await (await _callInfoRepository.GetQueryableAsync())
                .FirstOrDefaultAsync(x => x.RegisterNo == registerNo);
            if (callInfo is null)
                Oh.Error("叫号信息不存在");

            if (!callInfo.IsTop)
                Oh.Error("未置顶患者无法取消置顶");

            if (callInfo.CallStatus == CallStatus.Calling)
                Oh.Error("叫号中患者无法取消优先");

            callInfo.CancelMoveToTop();
            await _callInfoRepository.UpdateAsync(callInfo);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            return await GetAsync(callInfo.RegisterNo);
        }

        /// <summary>
        /// 设置暂停
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        [HttpPut("{registerNo}/set-pause")]
        public async Task<CallInfoData> SetPauseAsync(string registerNo)
        {
            CallInfo callInfo = await _callInfoRepository.FirstOrDefaultAsync(x => x.RegisterNo == registerNo);
            if (callInfo is null)
                // 叫号信息不存在 
                Oh.Error("叫号信息不存在");

            switch (callInfo.CallStatus)
            {
                case CallStatus.Calling:
                    Oh.Error("叫号中患者无法暂停");
                    break;
                case CallStatus.Pause:
                    Oh.Error("暂停中患者无法暂停");
                    break;
                case CallStatus.Over:
                    Oh.Error("已经叫号患者无法暂停");
                    break;
                case CallStatus.Exceed:
                    Oh.Error("已经过号无法暂停");
                    break;
                case CallStatus.Refund:
                    Oh.Error("已作废患者无法暂停");
                    break;
                default:
                    break;
            }
            callInfo.SetPause();
            await _callInfoRepository.UpdateAsync(callInfo);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            return await GetAsync(callInfo.RegisterNo);
        }

        /// <summary>
        /// 取消暂停
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        [HttpPut("{registerNo}/cancel-pause")]
        public async Task<CallInfoData> CancelPauseAsync(string registerNo)
        {
            CallInfo callInfo = await (await _callInfoRepository.GetQueryableAsync())
                .FirstOrDefaultAsync(x => x.RegisterNo == registerNo && x.CallStatus == CallStatus.Pause);
            if (callInfo is null)
                Oh.Error("需要取消叫号信息不存在");

            callInfo.CancelPause();
            await _callInfoRepository.UpdateAsync(callInfo);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            return await GetAsync(callInfo.RegisterNo);
        }

        /// <summary>
        /// 设置看报告
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        [HttpPut("{registerNo}/set-look-report")]
        public async Task<CallInfoData> SetLookReportAsync(string registerNo)
        {
            CallInfo callInfo = await _callInfoRepository.FirstOrDefaultAsync(x => x.RegisterNo == registerNo);
            if (callInfo is null)
                // 叫号信息不存在 
                Oh.Error("叫号信息不存在");

            switch (callInfo.CallStatus)
            {
                case CallStatus.Calling:
                    Oh.Error("叫号中患者无法看报告");
                    break;
                case CallStatus.Pause:
                    Oh.Error("暂停中患者无法看报告");
                    break;
                case CallStatus.Exceed:
                    Oh.Error("已经过号无法看报告");
                    break;
                case CallStatus.Refund:
                    Oh.Error("已作废患者无法看报告");
                    break;
                default:
                    break;
            }
            callInfo.SetLookReport();
            var entity = await _callInfoRepository.UpdateAsync(callInfo);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            return ObjectMapper.Map<CallInfo, CallInfoData>(entity);
        }

        /// <summary>
        /// 取消看报告
        /// </summary>
        /// <param name="registerNo"> registerNo</param>
        /// <returns></returns>
        [HttpPut("{registerNo}/cancel-look-report")]
        public async Task<CallInfoData> CancelLookReportAsync(string registerNo)
        {
            CallInfo callInfo = await (await _callInfoRepository.GetQueryableAsync())
                .FirstOrDefaultAsync(x => x.RegisterNo == registerNo);
            if (callInfo is null)
                Oh.Error("叫号信息不存在");

            if (!callInfo.IsReport)
                Oh.Error("未看报告患者无法取消看报告");

            callInfo.CancelLookReport();
            var entity = await _callInfoRepository.UpdateAsync(callInfo);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            return ObjectMapper.Map<CallInfo, CallInfoData>(entity);
        }

        /// <summary>
        /// 顺呼
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [HttpPost("call-next")]
        public async Task<CallInfoData> CallNextAsync(CallNextDto input)
        {
            // 获取诊室所在科室
            ConsultingRoom consultingRoom = await GetRoomCodeAsync(input.ConsultingRoomCode, input.IpAddr);
            //ConsultingRoom consultingRoom = await (await this._consultingRoomRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.Code == input.ConsultingRoomCode);
            //// 诊室不存在
            //if (consultingRoom is null)
            //{
            //    Oh.Error("诊室不存在");
            //}
            // 按叫号模式获取当前诊室/医生所在科室
            var department = await this._callInfoManager.GetDepartmentAsync(consultingRoom.Code, input.DoctorId);
            // 获取叫号中的记录 (暂时不做叫号中校验)
            var callInfo = await (await this._callInfoRepository.GetQueryableAsync())
                .OrderByDescending(x => x.CreationTime)
                .Where(x => x.CallStatus == CallStatus.Calling && x.TriageDept == department.Code && x.ConsultingRoomCode == consultingRoom.Code)
                .FirstOrDefaultAsync();
            if (callInfo != null && callInfo.CallStatus == CallStatus.Calling)
            {
                // 呼叫当前叫号患者
                callInfo.NowCalling(input.DoctorId, input.DoctorName, consultingRoom.Code, consultingRoom.Name);
                await _callInfoRepository.UpdateAsync(callInfo);
                var callingEtoV1 = new CallingEto
                {
                    DoctorId = input.DoctorId,
                    DoctorName = input.DoctorName,
                    ConsultingRoomCode = input.ConsultingRoomCode,
                    ConsultingRoomName = consultingRoom.Name,
                    PatientName = callInfo.PatientName,
                    DepartmentCode = department.Code,
                    DepartmentName = department.Name,
                    CallingSn = callInfo.CallingSn,
                    LastCalledTime = DateTime.Now,
                    ActTriageLevel = callInfo.ActTriageLevel,
                    ActTriageLevelName = callInfo.ActTriageLevelName,
                    RegisterNo = callInfo.RegisterNo

                };
                // 发布叫号信息，语音播报，呼叫指定患者到指定诊室 
                await this._capPublisher.PublishAsync(Hubs.CallHub.calling, callingEtoV1);
                await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
                return ObjectMapper.Map<CallInfo, CallInfoData>(callInfo);
            }
            // 获取当前科室排队队列的第一个叫号患者信息
            var query = (await this._callInfoRepository.GetQueryableAsync())
                .Where(new CallingInfoSpecification())
                .Where(c => string.IsNullOrEmpty(c.DoctorCode) ? true : c.DoctorCode == input.DoctorId) //如果是有绑定医生的话只能当前这个医生接诊
                .Where(x => x.TriageDept == department.Code);
            var callingInfo = await this.OrderByCallingStatus(query)
                .FirstOrDefaultAsync();
            // 暂无候诊中的患者
            if (callingInfo is null)
            {
                Oh.Error($"当前科室【{department.Name}】暂无候诊中的患者");
            }
            // 呼叫当前叫号患者
            callingInfo.NowCalling(input.DoctorId, input.DoctorName, consultingRoom.Code, consultingRoom.Name);
            await _callInfoRepository.UpdateAsync(callingInfo);



            // 叫号结束后发送 调用DDP调用叫号大屏幕
            _ = _ddpApiService.ReferralOperationAsync(new ShareModel.DDPs.Requests.PKUDReferralOperation
            {
                clinicName = callingInfo.TriageDeptName,
                patientID = callingInfo.PatientID,
                patientName = callingInfo.PatientName,
                doctorID = callingInfo.CallingDoctorCode,
                ipAddress = consultingRoom.IP

            });

            var callingEto = new CallingEto
            {
                DoctorId = input.DoctorId,
                DoctorName = input.DoctorName,
                ConsultingRoomCode = input.ConsultingRoomCode,
                ConsultingRoomName = consultingRoom.Name,
                PatientName = callingInfo.PatientName,
                DepartmentCode = department.Code,
                DepartmentName = department.Name,
                CallingSn = callingInfo.CallingSn,
                LastCalledTime = DateTime.Now,
                ActTriageLevel = callingInfo.ActTriageLevel,
                ActTriageLevelName = callingInfo.ActTriageLevelName,
                RegisterNo = callingInfo.RegisterNo

            };
            // 发布叫号信息，语音播报，呼叫指定患者到指定诊室 
            await this._capPublisher.PublishAsync(Hubs.CallHub.calling, callingEto);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            return ObjectMapper.Map<CallInfo, CallInfoData>(callingInfo);
        }

        /// <summary>
        /// 重呼
        /// </summary>
        /// <param name="input"></param>
        [HttpPost("call-again")]
        public async Task<CallInfoData> CallAgainAsync(CallAgainDto input)
        {
            // 获取诊室所在科室
            ConsultingRoom consultingRoom = await GetRoomCodeAsync(input.ConsultingRoomCode, input.IpAddr);
            //ConsultingRoom consultingRoom = await _consultingRoomRepository.GetAsync(x => x.Code == input.ConsultingRoomCode);
            //// 诊室不存在
            //if (consultingRoom is null)
            //    Oh.Error("诊室不存在");

            // 按叫号模式获取当前诊室/医生所在科室
            var department = await this._callInfoManager.GetDepartmentAsync(consultingRoom.Code, input.DoctorId);
            CallInfo callingInfo = null;
            //如果挂号信息不为空，则指定患者叫号， 获取叫号中的记录
            var justReCall = false;
            if (!string.IsNullOrWhiteSpace(input.RegisterNo))
            {
                justReCall = true;
                callingInfo = await (await this._callInfoRepository.GetQueryableAsync())
                    .FirstOrDefaultAsync(x => x.RegisterNo == input.RegisterNo);
            }
            else
            {
                callingInfo = await (await this._callInfoRepository.GetQueryableAsync())
                   .OrderByDescending(x => x.CreationTime)
                   .Where(x => x.CallStatus == CallStatus.Calling && x.TriageDept == department.Code
                        && x.ConsultingRoomCode == input.ConsultingRoomCode)
                   .Where(c => string.IsNullOrEmpty(c.DoctorCode) ? true : c.DoctorCode == input.DoctorId) //如果是有绑定医生的话只能当前这个医生重呼
                   .FirstOrDefaultAsync();
            }

            if (callingInfo is null)
                Oh.Error("当前诊室无叫号中的患者");

            // 呼叫当前叫号患者
            callingInfo.NowCalling(input.DoctorId, input.DoctorName, consultingRoom.Code, consultingRoom.Name, justReCall);

            await _callInfoRepository.UpdateAsync(callingInfo);

            var callingEto = ObjectMapper.Map<CallInfo, CallingEto>(callingInfo);

            if (justReCall)
            {
                callingEto.ConsultingRoomCode= input.ConsultingRoomCode;
                callingEto.ConsultingRoomName= input.ConsultingRoomName;
            }
            // 发布叫号信息，语音播报，呼叫指定患者到指定诊室 
            await this._capPublisher.PublishAsync(Hubs.CallHub.calling, callingEto);

            return ObjectMapper.Map<CallInfo, CallInfoData>(callingInfo);
        }

        /// <summary>
        /// 取消叫号 （暂时没有用到 2023-12-5）
        /// </summary>
        /// <param name="input"></param>
        [HttpPost("call-cancel")]
        public async Task<CallInfoData> CancelCallAsync(CallCancelDto input)
        {
            // 获取诊室所在科室
            ConsultingRoom consultingRoom = await GetRoomCodeAsync(input.ConsultingRoomCode, input.IpAddr);


            // 按叫号模式获取当前诊室/医生所在科室
            var department = await _callInfoManager.GetDepartmentAsync(consultingRoom.Code, input.DoctorId);
            // 获取叫号中的记录
            var callingInfo = await (await _callInfoRepository.GetQueryableAsync())
                .OrderByDescending(x => x.CreationTime)
                .Where(x => x.CallStatus == CallStatus.Calling)
                .FirstOrDefaultAsync();
            if (callingInfo is null)
            {
                Oh.Error("当前诊室无叫号中的患者");
            }

            // 取消呼叫当前叫号患者
            callingInfo.CancelCalling();
            await _callInfoRepository.UpdateAsync(callingInfo);

            var callingEto = ObjectMapper.Map<CallInfo, CallingEto>(callingInfo);
            await _capPublisher.PublishAsync(Hubs.CallHub.cancelled, callingEto);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());

            return ObjectMapper.Map<CallInfo, CallInfoData>(callingInfo);
        }

        /// <summary>
        /// 转诊 （修改科室 ， 修改分诊level ,指定医生）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("call-referral")]
        [Obsolete]
        public async Task<CallInfoData> CallReferralAsync(CallReferralDto input)
        {
            if (string.IsNullOrEmpty(input.RegisterNo))
                Oh.Error("无挂号信息，无法正常处理消息");

            //首先根据数据查询需要编辑的数据
            var Info = await _callInfoRepository.GetAsync(c => c.RegisterNo == input.RegisterNo);
            if (Info is null)
            {
                Oh.Error("当前需要修改的数据找不到");
            }
            else
            {
                if (Info.CallStatus == CallStatus.Calling)
                    Oh.Error("当前患者正在叫号，无法重新分诊");
            }

            //判断是否需要重新生成号码
            if (!string.IsNullOrEmpty(input.TriageLevel) || !string.IsNullOrEmpty(input.DeptCode))
            {
                //需要重新生成号码 
                var inQueueEntity = new InQueueCallInfoEto()
                {
                    RegisterNo = input.RegisterNo,
                    ActTriageLevel = Info.ActTriageLevel,
                    ActTriageLevelName = Info.ActTriageLevelName,
                    DoctorCode = Info.DoctorCode,
                    DoctorName = Info.DoctorName,
                    PatientID = Info.PatientID,
                    PatientName = Info.PatientName,
                    TriageDeptCode = Info.TriageDept,
                    TriageDeptName = Info.TriageDeptName
                };
                if (!string.IsNullOrEmpty(input.TriageLevel))
                {
                    inQueueEntity.ActTriageLevel = input.TriageLevel;
                    inQueueEntity.ActTriageLevelName = input.TriageLevelName;
                }
                if (!string.IsNullOrEmpty(input.DeptCode))
                {
                    inQueueEntity.TriageDeptCode = input.DeptCode;
                    inQueueEntity.TriageDeptName = input.DeptName;
                }
                if (!string.IsNullOrEmpty(input.DoctorId))
                {
                    inQueueEntity.DoctorName = input.DoctorName;
                    inQueueEntity.DoctorCode = input.DoctorId;
                }
                else
                {
                    inQueueEntity.DoctorName = string.Empty;
                    inQueueEntity.DoctorCode = string.Empty;
                }

                if (!string.IsNullOrEmpty(input.TriageTarget))
                {
                    inQueueEntity.TriageTarget = input.TriageTarget;
                    inQueueEntity.TriageTargetName = input.TriageTargetName;
                }

                var callInfo = ObjectMapper.Map<InQueueCallInfoEto, CallInfo>(inQueueEntity);
                await SetCallSnAsync(callInfo);
                callInfo.IsShow = true;
                callInfo.CreationTime = DateTime.Now;
                //保持之前的入队列时间
                //callInfo.SetQueueTime(Info.InCallQueueTime, Info.LogTime);
                // 重新生成排队号之后修改排队时间为最新的时间
                callInfo.SetQueueTime(DateTime.Now, Info.LogTime);
                //插入之前先删除 保证registerNo 唯一
                await _callInfoRepository.DeleteAsync(c => c.RegisterNo == callInfo.RegisterNo);
                var entity = await _callInfoRepository.InsertAsync(callInfo);
                //刷新队列
                await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
                return ObjectMapper.Map<CallInfo, CallInfoData>(entity);
            }
            else
            {
                if (string.IsNullOrEmpty(input.DoctorId))
                    Oh.Error("当前传入修改的数据不满足要求");

                Info.SetDoctor(input.DoctorId, input.DoctorName);
                var entity = await _callInfoRepository.UpdateAsync(Info);
                //刷新队列
                await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
                return ObjectMapper.Map<CallInfo, CallInfoData>(entity);
            }
        }

        /// <summary>
        /// 转诊 （修改科室 ， 修改分诊level ,指定医生）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("call-referral-v2")]
        public async Task<CallInfoData> CallReferralV2Async(CallReferralDto input)
        {
            if (string.IsNullOrEmpty(input.RegisterNo))
                Oh.Error("无挂号信息，无法正常处理消息");

            //首先根据数据查询需要编辑的数据
            var Info = await _callInfoRepository.GetAsync(c => c.RegisterNo == input.RegisterNo);
            if (Info is null)
            {
                var callInfo = ObjectMapper.Map<CallReferralDto, InQueueCallInfoEto>(input);
                return await PatientInQueueAsync(callInfo);
            }
            else
            {
                if (Info.CallStatus == CallStatus.Calling)
                    Oh.Error("当前患者正在叫号，无法重新分诊");
            }

            //判断是否需要重新生成号码
            if (!string.IsNullOrEmpty(input.TriageLevel) || !string.IsNullOrEmpty(input.DeptCode))
            {
                //需要重新生成号码 
                var inQueueEntity = new InQueueCallInfoEto()
                {
                    RegisterNo = input.RegisterNo,
                    ActTriageLevel = Info.ActTriageLevel,
                    ActTriageLevelName = Info.ActTriageLevelName,
                    DoctorCode = Info.DoctorCode,
                    DoctorName = Info.DoctorName,
                    PatientID = Info.PatientID,
                    PatientName = Info.PatientName,
                    TriageDeptCode = Info.TriageDept,
                    TriageDeptName = Info.TriageDeptName
                };
                if (!string.IsNullOrEmpty(input.TriageLevel))
                {
                    inQueueEntity.ActTriageLevel = input.TriageLevel;
                    inQueueEntity.ActTriageLevelName = input.TriageLevelName;
                }
                if (!string.IsNullOrEmpty(input.DeptCode))
                {
                    inQueueEntity.TriageDeptCode = input.DeptCode;
                    inQueueEntity.TriageDeptName = input.DeptName;
                }
                if (!string.IsNullOrEmpty(input.DoctorId))
                {
                    inQueueEntity.DoctorName = input.DoctorName;
                    inQueueEntity.DoctorCode = input.DoctorId;
                }
                else
                {
                    inQueueEntity.DoctorName = string.Empty;
                    inQueueEntity.DoctorCode = string.Empty;
                }

                if (!string.IsNullOrEmpty(input.TriageTarget))
                {
                    inQueueEntity.TriageTarget = input.TriageTarget;
                    inQueueEntity.TriageTargetName = input.TriageTargetName;
                }

                var callInfo = ObjectMapper.Map<InQueueCallInfoEto, CallInfo>(inQueueEntity);
                await SetCallSnAsync(callInfo);
                callInfo.IsShow = true;
                callInfo.CreationTime = DateTime.Now;
                //保持之前的入队列时间
                //callInfo.SetQueueTime(Info.InCallQueueTime, Info.LogTime);
                // 重新生成排队号之后修改排队时间为最新的时间
                callInfo.SetQueueTime(DateTime.Now, Info.LogTime);
                //插入之前先删除 保证registerNo 唯一
                await _callInfoRepository.DeleteAsync(c => c.RegisterNo == callInfo.RegisterNo);
                var entity = await _callInfoRepository.InsertAsync(callInfo);
                //刷新队列
                await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
                return ObjectMapper.Map<CallInfo, CallInfoData>(entity);
            }
            else
            {
                if (string.IsNullOrEmpty(input.DoctorId))
                    Oh.Error("当前传入修改的数据不满足要求");

                Info.SetDoctor(input.DoctorId, input.DoctorName);
                var entity = await _callInfoRepository.UpdateAsync(Info);
                //刷新队列
                await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
                return ObjectMapper.Map<CallInfo, CallInfoData>(entity);
            }
        }

        /// <summary>
        /// 更新callInfo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("update-call")]
        public async Task UpdateCallAsync(CallUpdateDto input)
        {
            var callEntity = await _callInfoRepository.FirstOrDefaultAsync(c => c.RegisterNo == input.RegisterNo);
            if (callEntity is null)
            {
                return;
            }
            callEntity.SetDoctor(input.DoctorCode, input.DoctorName);
            await _callInfoRepository.UpdateAsync(callEntity);
        }

        /// <summary>
        /// 退回候诊
        /// </summary>
        /// <param name="registerNo">病人 挂号No</param>
        /// <returns></returns>
        [HttpPost("send-back-waiting")]
        public async Task<CallInfoData> SendBackWaittingAsync(string registerNo)
        {
            // 获取就诊中的患者
            var callingInfo = (await this._callInfoRepository.GetQueryableAsync()).Where(x => x.RegisterNo == registerNo)
                .FirstOrDefault();
            if (callingInfo is null)
            {
                // 患者未接诊或已结束就诊
                Oh.Error("患者未接诊或已结束就诊");
            }
            callingInfo.SendBackWaiting();

            await _callInfoRepository.UpdateAsync(callingInfo);

            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());

            return await GetAsync(callingInfo.RegisterNo);
        }

        /// <summary>
        /// 结束就诊（已经叫号接口）
        /// </summary>
        /// <param name="registerNo">病人挂号No</param>
        [HttpPost("treat-finish")]
        public async Task<CallInfoData> TreatFinishAsync(string registerNo)
        {
            // 获取就诊中的患者
            var callingInfo = (await this._callInfoRepository.GetQueryableAsync()).Where(x => x.RegisterNo == registerNo)
                .FirstOrDefault();
            if (callingInfo is null)
            {
                // 患者未接诊或已结束就诊
                Oh.Error("患者未接诊或已结束就诊");
            }
            callingInfo.TreatFinish(callingInfo.CallingDoctorCode, callingInfo.CallingDoctorName, callingInfo.TriageDept, callingInfo.TriageDeptName);

            callingInfo = await _callInfoRepository.UpdateAsync(callingInfo);

            await _capPublisher.PublishAsync(Hubs.CallHub.finished, callingInfo);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            return await GetAsync(callingInfo.RegisterNo);
        }

        /// <summary>
        /// 过号（多次叫号没人）
        /// </summary>
        /// <param name="input"></param>
        [HttpPost("untreated-over")]
        public async Task<CallInfoData> UntreatedOverAsync(UntreatedOverDto input)
        {
            ConsultingRoom consultingRoom = await GetRoomCodeAsync(input.ConsultingRoomCode, input.IpAddr);

            // 按叫号模式获取当前诊室/医生所在科室
            var department = await this._callInfoManager.GetDepartmentAsync(consultingRoom.Code, input.DoctorId);

            // 获取就诊中的患者 // 正在叫号中
            var callingInfo = (await this._callInfoRepository.GetQueryableAsync())
                .Where(x => x.CallStatus == CallStatus.Calling).Where(x => x.TriageDept == department.Code && x.ConsultingRoomCode == input.ConsultingRoomCode)
                .Where(c => string.IsNullOrEmpty(c.DoctorCode) ? true : c.DoctorCode == input.DoctorId) //如果是有绑定医生的话只能当前这个医生过号
                .FirstOrDefault();

            if (callingInfo is null)
            {
                // 患者已结束就诊
                Oh.Error("当前诊室无叫号中的患者");
            }

            var callingEto = ObjectMapper.Map<CallInfo, CallingEto>(callingInfo);
            await _capPublisher.PublishAsync(Hubs.CallHub.missed_turn, callingEto);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            //await _capPublisher.PublishAsync(Hubs.CallHub.finished, callingEto);
            callingInfo.Exceed();
            await _callInfoRepository.UpdateAsync(callingInfo);

            return ObjectMapper.Map<CallInfo, CallInfoData>(callingInfo);
        }

        /// <summary>
        /// 过号通过RegisterNo 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("missed-turn")]
        public async Task<CallInfoData> MissedTurnAsync(MissedTurnDto dto)
        {
            var callingInfo = (await _callInfoRepository.GetQueryableAsync()).Where(x => x.RegisterNo == dto.RegisterNo)
              .FirstOrDefault();

            if (callingInfo == null) Oh.Error("患者信息不存在，请确认ID是否正确");
            var callingEto = ObjectMapper.Map<CallInfo, CallingEto>(callingInfo);

            await _capPublisher.PublishAsync(Hubs.CallHub.missed_turn, callingEto);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            callingInfo.Exceed();
            await _callInfoRepository.UpdateAsync(callingInfo);

            return ObjectMapper.Map<CallInfo, CallInfoData>(callingInfo);

        }

        /// <summary>
        /// 过号患者恢复候诊
        /// </summary>
        /// <param name="registerNo">挂号的No</param>
        [HttpPost("return-to-queue")]
        public async Task<CallInfoData> ReturnToQueueAsync(string registerNo)
        {
            // 获取就诊中的患者
            var callingInfo = (await _callInfoRepository.GetQueryableAsync()).Where(x => x.RegisterNo == registerNo)
                .FirstOrDefault();

            if (callingInfo == null) Oh.Error("患者信息不存在，请确认ID是否正确");
            if (callingInfo.CallStatus != CallStatus.Exceed)
                // 患者未叫号
                Oh.Error("当前患者未过号，无法恢复候诊");

            //如果当前用户是属于过号的情况 顺延到当前诊室的第二个
            //查看当前目前正在的队列 排在第二个
            var query = (await _callInfoRepository.GetQueryableAsync())
                .Where(new CallingInfoSpecification()).Where(x => x.TriageDept == callingInfo.TriageDept && x.ActTriageLevel == callingInfo.ActTriageLevel);
            var list = await OrderByCallingStatus(query).Take(2).ToListAsync();
            if (list.Count > 0)
            {
                var time = list[list.Count - 1].InCallQueueTime;
                if (time > DateTime.MinValue)
                    callingInfo.ReturnToQueue((DateTime)time);
                else
                    Oh.Error("当前诊室的第二个患者入队列时间异常，无法恢复候诊");
            }
            else
                callingInfo.ReturnToQueue();

            callingInfo.SetCallingDoctor(string.Empty, string.Empty);
            await _callInfoRepository.UpdateAsync(callingInfo);
            //刷新队列
            await _capPublisher.PublishAsync(Hubs.CallHub.calling_queue_changed, new object());
            return await GetAsync(callingInfo.RegisterNo);
        }

        /// <summary>
        /// 获取叫号统计信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistics")]
        public async Task<CallingStatisticsDto> GetStatisticsAsync()
        {
            var departments = await _departmentManager.GetAllAsync();
            var currentDate = await _baseConfigManager.GetTodayBeginAsync();

            // 查询所有科室的统计信息
            var (todayCallingCount, waittingCount, todayTreatedCount, todayUntreatedOverCount)
                = await this._callInfoManager.GetTodayStatisticsAsync(currentDate.Date);
            // 查询所有科室门诊医生数量
            var allDoctorCountQuery = from record in (await this._callInfoRepository.GetQueryableAsync())
                                      where record.LogDate == currentDate.Date
                                      group record by new
                                      {
                                          record.CallingDoctorCode,
                                      }
                                      into countRecord
                                      select countRecord.Key.CallingDoctorCode;
            var allDoctorCount = await allDoctorCountQuery.CountAsync();
            CallingStatisticsDto callingStatisticsDto = new()
            {
                DoctorCount = allDoctorCount,
                TotalCount = todayCallingCount,
                WattingCount = waittingCount,
                TreatedCount = todayTreatedCount,
                UntreatedOverCount = todayUntreatedOverCount,
            };
            callingStatisticsDto.DeptCallings = new();

            foreach (var department in departments)
            {
                // 查询科室的统计信息
                var (dptTodayCallingCount, dptWaittingCount, dptTodayTreatedCount, dptTodayUntreatedOverCount)
                    = await _callInfoManager.GetTodayStatisticsAsync(currentDate.Date, department.Code);
                // 查询门诊医生数量
                var baseQuery = from record in await (_callInfoRepository.GetQueryableAsync())
                                where record.LogDate == currentDate.Date
                                where record.TriageDept == department.Code
                                group record by new
                                {
                                    record.CallingDoctorCode,
                                }
                                into countRecord
                                select countRecord.Key.CallingDoctorCode;
                var doctorCount = await baseQuery.CountAsync();

                callingStatisticsDto.DeptCallings.Add(new()
                {
                    DeptCode = department.Code,
                    DeptID = department.Id,
                    DeptName = department.Name,
                    DoctorCount = doctorCount,
                    TotalCount = dptTodayCallingCount,
                    WattingCount = dptWaittingCount,
                    TreatedCount = dptTodayTreatedCount,
                    UntreatedOverCount = dptTodayUntreatedOverCount,
                });
            }

            return callingStatisticsDto;
        }

        /// <summary>
        /// 分页获取叫号记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("record/paged-list")]
        public async Task<PagedResultDto<CallingRecordData>> GetPagedRecordListAsync(GetCallingRecordInput input)
        {
            var (list, count) = await this._callingRecordRepository.GetPagedListAsync(input.SkipCount, input.Size,
                triageDept: input.TriageDept);
            var dtoList = ObjectMapper.Map<List<CallingRecord>, List<CallingRecordData>>(list);

            return new PagedResultDto<CallingRecordData>(count, dtoList.AsReadOnly());
        }

        /// <summary>
        /// 叫号历史查看
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("history/paged-list")]
        public async Task<PagedCallInfoHistoryDto> GetPagedHistoryAsync(GetHistoryInput input)
        {
            HistorySearchCallInfoSpecification historySearchCallInfoSpecification =
                new(input.BeginDateTime, input.EndDateTime, input.Filter, input.ActTriageLevelCode,
                     input.TriageDeptCode, input.ChargeTypeCode, input.DoctorId);
            var query = (await this._callInfoRepository.GetQueryableAsync())
                .Where(historySearchCallInfoSpecification).AsQueryable();
            var list = query.OrderByDescending(x => x.CreationTime)
                .PageBy(input.SkipCount, input.Size).ToList();
            var totalCount = query.LongCount();
            //var treatedCount = await query.Where(x => x.VisitStatus == EVisitStatus.Treated).LongCountAsync();
            //var untreatedOverCount = await query.Where(x => x.VisitStatus == EVisitStatus.UntreatedOver).LongCountAsync();
            //var notRegisterCount = await query.Where(x => x.VisitStatus == EVisitStatus.NotRegister).LongCountAsync();
            //var outDepartmentCount = await query.Where(x => x.VisitStatus == EVisitStatus.OutDepartment).LongCountAsync();
            //var refundNoCount = await query.Where(x => x.VisitStatus == EVisitStatus.RefundNo).LongCountAsync();

            return new PagedCallInfoHistoryDto
            {
                TotalCount = totalCount,
                //TreatedCount = treatedCount,
                //UntreatedOverCount = untreatedOverCount,
                //NotRegisterCount = notRegisterCount,
                //OutDepartmentCount = outDepartmentCount,
                //RefundNoCount = refundNoCount,
                Items = ObjectMapper.Map<List<CallInfo>, List<CallInfoData>>(list),
            };
        }

        /// <summary>
        /// 查询当前门诊医生列表
        /// </summary>
        /// <param name="departmentCode">科室编码</param>
        /// <returns></returns>
        [HttpGet("doctor")]
        public async Task<List<ConsultingDoctorDto>> GetConsultingDoctorListAsync(string departmentCode)
        {
            var currentDate = await this._baseConfigManager.GetTodayBeginAsync();
            // 查询医生最后一次叫号记录
            var doctorConsultings = from record in (await this._callingRecordRepository.GetQueryableAsync())
                                    where record.CreationTime >= DateTime.Now.AddDays(-1)  // 只查24小时内的
                                    where record.DoctorId != null
                                    orderby record.Id descending
                                    group record by new
                                    {
                                        record.DoctorId,
                                    }
                                    into groupRecord
                                    select new { groupRecord.Key.DoctorId, Id = groupRecord.Max(x => x.Id) };
            // 确定医生最后一次叫号记录所在诊室、科室
            var lastDoctorConsultings = from dc in doctorConsultings.AsQueryable()
                                        join record in (await this._callingRecordRepository.GetQueryableAsync()) on new { dc.DoctorId, dc.Id } equals new { record.DoctorId, record.Id }
                                        join info in (await this._callInfoRepository.GetQueryableAsync()) on record.CallInfoId equals info.Id
                                        select new
                                        {
                                            dc.DoctorId,
                                            record.ConsultingRoomCode,
                                            record.ConsultingRoomName,
                                            DepartmentId = info.TriageDept,
                                            DepartmentName = info.TriageDeptName
                                        };
            // 统计查询
            var countQuery = from record in (await this._callInfoRepository.GetQueryableAsync())
                             where record.LogDate == currentDate.Date
                             //where record.VisitStartTime >= DateTime.Now.AddDays(-1)  // 只查24小时内的 
                             where string.IsNullOrEmpty(departmentCode) || record.TriageDept == departmentCode
                             group record by new
                             {
                                 record.DoctorName,
                             }
                             into countRecord
                             select new ConsultingDoctorDto
                             {
                                 DoctorName = countRecord.Key.DoctorName,
                                 TodayConsultingCount = countRecord.Count(),
                             };

            return await countQuery.ToListAsync();
        }

        /// <summary>
        /// 向客户端发送叫号中的患者数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [CapSubscribe("ws.call.change.consulting-room.request", Group = "ecis.call")]
        [NonAction]
        public async Task SendCallingDataToClientAsync(ChangeConsultingRoomEto input)
        {
            using var uow = UnitOfWorkManager.Begin();
            // 获取就诊中的患者
            var callingInfos = await (await this._callInfoRepository.GetQueryableAsync())
                .Where(x => x.CallStatus == CallStatus.Calling)  // 正在叫号中 
                .OrderBy(x => x.LastCalledTime)
                .ToListAsync();
            if (callingInfos.Count > 0)
            {
                foreach (var callingInfo in callingInfos)
                {
                    var callingEto = ObjectMapper.Map<CallInfo, CallingEto>(callingInfo);
                    // 发布停止叫号消息，停止语音播报
                    await this._capPublisher.PublishAsync("im.call.calling", new GenericClientEto<CallingEto>(input.ConnectionId, "Calling", callingEto));
                }
            }
            else
            {
                await this._capPublisher.PublishAsync("im.call.calling", new GenericClientEto<CallingEto>(input.ConnectionId, "Calling", new CallingEto()));
            }
            await uow.CompleteAsync();
        }

        /// <summary>
        /// 默认排序
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private IOrderedQueryable<CallInfo> OrderByCallingStatus(IQueryable<CallInfo> query)
        {
            // 按产品要求，未挂号的都放在列表的最下方（无论是III级还是IV级）  by：ywlin-2021.11.12
            return query
                .OrderBy(x => x.CallStatus == CallStatus.Calling ? 0 :
                             x.CallStatus == CallStatus.NotYet || x.CallStatus == CallStatus.Over ? 1 : 2)  // 叫号中 > 未叫号
                .ThenByDescending(x => x.IsTop).ThenByDescending(x => x.TopTime)  // 置顶优先
                .ThenBy(x => x.ActTriageLevel)  // 患者等级（需要对等级进行可排序的处理） 
                .ThenBy(x => x.CallStatus == CallStatus.Over ? 0 : 1)
                .ThenBy(x => x.InCallQueueTime)
                .ThenBy(x => x.LogTime);  // 最终按登记时间排序
        }

        /// <summary>
        /// 过号患者查询（大屏终端）
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        [HttpGet("client/exceed-list")]
        [AllowAnonymous]
        public async Task<List<CallClientData>> GetClientExceedListAsync(GetClientCallInfoInput input)
        {
            var query = (await _callInfoRepository.GetQueryableAsync())
                .Where(x => x.IsShow
                && x.TriageTarget == "TriageDirection_006"  //过滤抢救留观患者
                && (x.CallStatus == CallStatus.Exceed) && (x.CreationTime > DateTime.Now.AddHours(-4)))
                .WhereIf(!string.IsNullOrEmpty(input.TriageDept), x => x.TriageDept == input.TriageDept);
            var orderQuery = OrderByCallingStatus(query);
            var list = await orderQuery.ToListAsync();
            var dtoList = ObjectMapper.Map<List<CallInfo>, List<CallClientData>>(list);
            return dtoList;
        }
    }
}
