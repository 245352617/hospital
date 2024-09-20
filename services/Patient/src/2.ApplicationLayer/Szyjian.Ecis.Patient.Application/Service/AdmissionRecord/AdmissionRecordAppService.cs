using DotNetCore.CAP;
using IDCard;
using Mapster;
using MasterDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.OpenXmlFormats.Dml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 入科记录API
    /// </summary>
    [Authorize]
    public class AdmissionRecordAppService : EcisPatientAppService, IAdmissionRecordAppService, ICapSubscribe
    {
        private readonly ILogger<AdmissionRecordAppService> _log;
        private readonly IFreeSql _freeSql;
        private IConfiguration _configuration;
        private ICapPublisher _capPublisher;
        private readonly GrpcClient _grpcClient;
        private readonly ICallApi _callService;
        private readonly IHospitalApi _hisApi;
        private readonly PdaAppService _pdaAppService;

        public AdmissionRecordAppService(ILogger<AdmissionRecordAppService> log
            , IFreeSql freeSql
            , GrpcClient grpcClient
            , PdaAppService pdaAppService
            , IHospitalApi hisApi
            , IConfiguration configuration
            , ICapPublisher capPublisher
            , ICallApi callService)
        {
            _log = log;
            _freeSql = freeSql;
            _grpcClient = grpcClient;
            _pdaAppService = pdaAppService;
            _hisApi = hisApi;
            _configuration = configuration;
            _capPublisher = capPublisher;
            _callService = callService;
        }

        /// <summary>
        /// 更新患者就诊记录
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>主键自增Id</returns>
        public async Task<ResponseResult<int>> UpdateAdmissionRecordAsync(UpdateAdmissionRecordDto dto)
        {
            try
            {
                AdmissionRecord admissionRecord = await _freeSql.Select<AdmissionRecord>()
                    .Where(x => x.PI_ID == dto.PI_ID)
                    .FirstAsync();
                if (admissionRecord == null)
                {
                    return RespUtil.Error(data: -1, extra: "不存在该患者");
                }

                string attentionList = admissionRecord.AttentionCode;
                //如果此患者关注列表有当前用户且IsAttention为false，代表当前用户对此患者取消关注
                if (!string.IsNullOrWhiteSpace(admissionRecord.AttentionCode))
                {
                    if (admissionRecord.AttentionCode.Contains(CurrentUser.UserName) && !dto.IsAttention)
                    {
                        attentionList = attentionList.Replace(CurrentUser.UserName + "|", "");
                    }
                    // 若不包含且IsAttention为true则代表当前用户对此患者增加关注
                    else if (!attentionList.Contains(CurrentUser.UserName) && dto.IsAttention)
                    {
                        attentionList += CurrentUser.UserName + "|";
                    }
                }
                else
                {
                    if (dto.IsAttention)
                    {
                        attentionList += CurrentUser.UserName + "|";
                    }
                }

                string bedHeadSticker = dto.BedHeadStickerList.Count > 0
                    ? string.Join(',', dto.BedHeadStickerList)
                    : string.Empty;

                admissionRecord.Bed = dto.Bed;
                admissionRecord.AttentionCode = attentionList;
                admissionRecord.DutyDoctorCode = dto.DutyDoctorCode;
                admissionRecord.DutyDoctorName = dto.DutyDoctorName;
                admissionRecord.DutyNurseCode = dto.DutyNurseCode;
                admissionRecord.DutyNurseName = dto.DutyNurseName;
                admissionRecord.InDeptWay = dto.InDeptWay;
                admissionRecord.DeptCode = dto.DeptCode;
                admissionRecord.DeptName = dto.DeptName;
                admissionRecord.BedHeadSticker = bedHeadSticker;
                admissionRecord.InDeptTime = dto.InDeptTime;

                int rows = await _freeSql.Update<AdmissionRecord>()
                    .SetSource(admissionRecord)
                    .ExecuteAffrowsAsync();

                if (rows > 0)
                {
                    await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.UpdatePatient, admissionRecord);
                    return RespUtil.Ok<int>(extra: "更新患者入科记录成功");
                }

                return RespUtil.Error(extra: "更新患者入科记录失败", data: -1);
            }
            catch (Exception e)
            {
                return RespUtil.InternalError(extra: e.Message, data: -1);
            }
        }

        #region 获取患者列表

        /// <summary>
        /// 当前排号列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetQueueAdmissionRecordAsync(
            QueueAdmissionRecordInput input, CancellationToken cancellationToken = default)
        {
            using var uow = _freeSql.CreateUnitOfWork();
            uow.IsolationLevel = System.Data.IsolationLevel.ReadUncommitted;
            try
            {
                List<DepartmentModel> depts = await _grpcClient.GetDepartmentsAsync();
                DepartmentModel dept = depts.FirstOrDefault(x => x.Code == input.DeptCode);
                var callInfoInput = new GetCallInfoInput
                {
                    TriageDept = dept?.RegisterCode,
                    ActTriageLevel = input.ActTriageLevel,
                    Filter = input.Filter,
                    Index = input.PageIndex,
                    Size = input.PageSize
                };
                // todo 改造成北大这边的获取数据列表
                PagedResultDto<CallInfoData> pageList = new PagedResultDto<CallInfoData>();
                var rESTfulResult = await _callService.GetPagedListAsync(callInfoInput);
                if (rESTfulResult.Success)
                {
                    pageList = rESTfulResult.Data;
                }
                else
                {
                    return RespUtil.InternalError<PagedResultDto<AdmissionRecordDto>>(msg: rESTfulResult.Message);
                }

                List<string> RegisterNos = pageList.Items.Select(c => c.RegisterNo).ToList();

                List<AdmissionRecordDto> list = await _freeSql.Select<AdmissionRecord>()
                    // 过滤队列中的患者
                    .Where(x => RegisterNos.Contains(x.RegisterNo))
                    // 只显示就诊区的患者
                    .Where(x => x.AreaCode == nameof(Area.OutpatientArea))
                    //.Count(out var totalCount)
                    //.Page(input.PageIndex, input.PageSize)
                    .WithTransaction(uow.GetOrBeginTransaction())
                    .ToListAsync(a => new AdmissionRecordDto
                    {
                        IsAttention = a.AttentionCode
                    }, cancellationToken);
                list = await GetAdmissionRecordProperty(list, cancellationToken);
                //根据RegisterNo 进行排序
                list = list.OrderBy(e =>
                {
                    var index = 0;
                    index = RegisterNos.IndexOf(e.RegisterNo);
                    if (index != -1)
                        return index;
                    else
                        return int.MaxValue;
                }).ToList();
                var res = new PagedResultDto<AdmissionRecordDto>
                {
                    TotalCount = pageList.TotalCount,
                    Items = list.ToList() //.OrderByDescending(p => p.CallStatus == CallStatus.Calling ? 1 : 0)
                };
                uow.Commit();
                return RespUtil.Ok(data: res);
            }
            catch (Exception e)
            {
                uow.Rollback();
                _log.LogError("Get admissionRecord pages error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<PagedResultDto<AdmissionRecordDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 急诊诊疗患者列表（抢救区，留观区）
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetAdmissionRecordPagedAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default)
        {
            _log.LogInformation("Get admissionRecord pages begin");
            using var uow = _freeSql.CreateUnitOfWork();
            uow.IsolationLevel = System.Data.IsolationLevel.ReadUncommitted;
            try
            {
                if (input.AreaCode == "RescueArea")
                {
                    return await GetRescueAreacAdmissionRecordPagedAsync(input);
                }

                var list = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea",
                        x => (x.VisitStatus == VisitStatus.待就诊 || x.VisitStatus == VisitStatus.正在就诊) &&
                             !string.IsNullOrEmpty(x.Bed))
                    .WhereIf(input.VisitStatus != VisitStatus.全部, x => x.VisitStatus == input.VisitStatus)
                    //校验模糊查询文本
                    .WhereIf(!string.IsNullOrWhiteSpace(input.SearchText),
                        x => x.PatientName.Contains(input.SearchText)
                             || x.VisitNo.Contains(input.SearchText)
                             || x.FirstDoctorCode.Contains(input.SearchText)
                             || x.FirstDoctorName.Contains(input.SearchText)
                    )

                    // 校验接诊人
                    .WhereIf(!string.IsNullOrWhiteSpace(input.OperatorCode),
                        x => x.OperatorCode == input.OperatorCode)
                    // 校验接诊人
                    .WhereIf(!string.IsNullOrWhiteSpace(input.FirstDoctorCode),
                        x => x.FirstDoctorCode == input.FirstDoctorCode)
                    //校验分诊级别
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageLevel),
                        x => x.TriageLevel == input.TriageLevel)
                    //校验关注医生
                    .WhereIf(!string.IsNullOrWhiteSpace(input.AttentionCode),
                        x => x.AttentionCode.Contains(input.AttentionCode))
                    //校验分诊科室
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageDeptCode),
                        x => x.TriageDeptCode == input.TriageDeptCode)
                    // 校验就诊时间
                    .WhereIf(input.StartDate != null && input.EndDate != null && input.StartDate < input.EndDate,
                        x => x.VisitDate.Value.BetweenEnd(input.StartDate.Value, input.EndDate.Value))
                    //校验就诊区域
                    // .WhereIf(!input.AreaCode.IsNullOrWhiteSpace(), x => input.AreaCode.Contains(x.AreaCode))
                    .WhereIf(!input.AreaCode.IsNullOrWhiteSpace(), x => x.AreaCode == input.AreaCode)
                    // 非历史患者应查询到以下两种状态患者
                    // 1.正在就诊状态患者
                    // 2.未挂号、待就诊并且分诊时间与当前时间间隔小于24小时患者
                    .WhereIf(!input.IsHistory && string.IsNullOrWhiteSpace(input.AttentionCode) && !input.MyPatient,
                        x =>
                            (x.VisitStatus == VisitStatus.正在就诊)
                            || ((x.VisitStatus == VisitStatus.未挂号 ||
                                 x.VisitStatus == VisitStatus.待就诊) &&
                                x.TriageTime > DateTime.Now.Date.AddHours(-24)))
                    // 历史患者应查询到以下状态患者
                    // 1.出科、已就诊、已退号、过号状态患者
                    // 2.待就诊、未挂号状态且分诊时间与当前时间间隔大于24小时
                    .WhereIf(input.IsHistory && string.IsNullOrWhiteSpace(input.AttentionCode) && !input.MyPatient,
                        x =>
                            (x.VisitStatus == VisitStatus.出科 || x.VisitStatus == VisitStatus.已就诊 ||
                             x.VisitStatus == VisitStatus.已退号 || x.VisitStatus == VisitStatus.过号)
                            || ((x.VisitStatus == VisitStatus.待就诊 || x.VisitStatus == VisitStatus.未挂号) &&
                                x.VisitDate > DateTime.Now.Date.AddHours(-24)))
                    .WhereIf(input.MyPatient, x => x.VisitDate.Value.Date == DateTime.Now.Date)
                    //校验责任医生
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyDoctorCode),
                        x => x.DutyDoctorCode == input.DutyDoctorCode)
                    //校验责任护士
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyNurseCode),
                        x => x.DutyNurseCode == input.DutyNurseCode)
                    // 过滤队列中的患者
                    //.WhereIf(registerNos != null && registerNos.Count > 0, x => registerNos.Contains(x.RegisterNo)) //TODO 合并到master再处理，龙岗有，北大没有
                    .WhereIf(!string.IsNullOrEmpty(input.DeptCode), x => x.DeptCode == input.DeptCode.Trim())
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.DeptCode.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.DeptCode == input.DeptCode.Trim())
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.Pflegestufe.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.Pflegestufe == input.Pflegestufe.Trim())
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.BedHeadStickerList.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.BedHeadSticker.Contains(input.BedHeadStickerList))
                    .Count(out var totalCount)
                    .Page(input.PageIndex, input.PageSize)
                    .OrderByDescending(x => x.VisitStatus == VisitStatus.未挂号 ? 0 : 1) // 未挂号的排在最后面
                    .OrderByIf(!input.IsHistory, x => x.VisitStatus, descending: true) // 非历史患者就诊状态降序排序
                    .OrderByIf(
                        !input.IsHistory && (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea"),
                        x => string.IsNullOrEmpty(x.Bed) ? 0 : 1, descending: true) //床位为空的排序在后面
                    .OrderByIf(
                        !input.IsHistory && (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea"),
                        x => x.Bed) //床位根据序号排序
                    .OrderByPropertyNameIf(input.AreaCode == "OutpatientArea" && !input.IsHistory,
                        nameof(AdmissionRecord.TriageLevel))
                    .OrderByPropertyNameIf(input.AreaCode == "OutpatientArea" && !input.IsHistory,
                        nameof(AdmissionRecord.VisitDate), isAscending: true) // 就诊区患者就诊时间顺序排序
                    .OrderBy(x => x.TriageTime)
                    .WithTransaction(uow.GetOrBeginTransaction())
                    .ToListAsync(a => new AdmissionRecordDto
                    {
                        IsAttention = a.AttentionCode
                    }, cancellationToken);
                _log.LogInformation("Get admissionRecord pages step 1");

                //列表属性赋值
                list = await GetAdmissionRecordProperty(list, cancellationToken);

                var res = new PagedResultDto<AdmissionRecordDto>
                {
                    TotalCount = totalCount,
                    Items = list.ToList()
                };
                uow.Commit();
                _log.LogInformation("Get admissionRecord pages success");
                return RespUtil.Ok(data: res);
            }
            catch (Exception e)
            {
                uow.Rollback();
                _log.LogError("Get admissionRecord pages error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<PagedResultDto<AdmissionRecordDto>>(extra: e.Message);
            }
        }

        private async Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetRescueAreacAdmissionRecordPagedAsync(
            AdmissionRecordInput input)
        {
            var list = await _freeSql.Select<AdmissionRecord>()
                    .Where(x => x.AreaCode == "RescueArea" &&
                                (x.VisitStatus == VisitStatus.待就诊 || x.VisitStatus == VisitStatus.正在就诊) &&
                             !string.IsNullOrEmpty(x.Bed))
                    .Count(out var totalCount)
                    .Page(input.PageIndex, input.PageSize)
                    .OrderByDescending(x => x.VisitStatus == VisitStatus.未挂号 ? 0 : 1) // 未挂号的排在最后面
                    .OrderByDescending(x => x.VisitStatus)
                    .OrderByDescending(x => string.IsNullOrEmpty(x.Bed) ? 0 : 1) //床位为空的排序在后面
                    .OrderBy(x => x.Bed) //床位根据序号排序
                    .OrderBy(x => x.TriageTime)
                    .ToListAsync(a => new AdmissionRecordDto
                    {
                        IsAttention = a.AttentionCode
                    });
            _log.LogInformation("Get admissionRecord pages step 1");

            //列表属性赋值
            list = await GetAdmissionRecordProperty(list);

            var res = new PagedResultDto<AdmissionRecordDto>
            {
                TotalCount = totalCount,
                Items = list.ToList()
            };
            _log.LogInformation("Get admissionRecord pages success");
            return RespUtil.Ok(data: res);
        }

        /// <summary>
        /// 急诊诊疗患者列表（就诊区） 2023-12-14 改列表更新排序规则
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetAdmissionRecordPaged2Async(
            AdmissionRecordInput input, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea",
                        x => (x.VisitStatus == VisitStatus.待就诊 || x.VisitStatus == VisitStatus.正在就诊) &&
                             !string.IsNullOrEmpty(x.Bed))
                    .WhereIf(input.VisitStatus != VisitStatus.全部, x => x.VisitStatus == input.VisitStatus)
                    //校验模糊查询文本
                    .WhereIf(!string.IsNullOrWhiteSpace(input.SearchText),
                        x => x.PatientName.Contains(input.SearchText)
                             || x.VisitNo.Contains(input.SearchText)
                             || x.FirstDoctorCode.Contains(input.SearchText)
                             || x.FirstDoctorName.Contains(input.SearchText)
                    )

                    // 校验接诊人
                    .WhereIf(!string.IsNullOrWhiteSpace(input.OperatorCode),
                        x => x.OperatorCode == input.OperatorCode)
                    // 校验接诊人
                    .WhereIf(!string.IsNullOrWhiteSpace(input.FirstDoctorCode),
                        x => x.FirstDoctorCode == input.FirstDoctorCode)
                    //校验分诊级别
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageLevel),
                        x => x.TriageLevel == input.TriageLevel)
                    //校验关注医生
                    .WhereIf(!string.IsNullOrWhiteSpace(input.AttentionCode),
                        x => x.AttentionCode.Contains(input.AttentionCode))
                    //校验分诊科室
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageDeptCode),
                        x => x.TriageDeptCode == input.TriageDeptCode)
                    // 校验就诊时间
                    .WhereIf(input.StartDate != null && input.EndDate != null && input.StartDate < input.EndDate,
                        x => x.VisitDate.Value.BetweenEnd(input.StartDate.Value, input.EndDate.Value))
                    //校验就诊区域
                    .WhereIf(!input.AreaCode.IsNullOrWhiteSpace(), x => input.AreaCode.Contains(x.AreaCode))
                    // 非历史患者应查询到以下两种状态患者
                    // 1.正在就诊状态患者
                    // 2.未挂号、待就诊并且分诊时间与当前时间间隔小于24小时患者
                    .WhereIf(!input.IsHistory && string.IsNullOrWhiteSpace(input.AttentionCode) && !input.MyPatient,
                        x =>
                            (x.VisitStatus == VisitStatus.正在就诊)
                            || ((x.VisitStatus == VisitStatus.未挂号 ||
                                 x.VisitStatus == VisitStatus.待就诊) &&
                                x.TriageTime > DateTime.Now.Date.AddHours(-24)))
                    // 历史患者应查询到以下状态患者
                    // 1.出科、已就诊、已退号、过号状态患者
                    // 2.待就诊、未挂号状态且分诊时间与当前时间间隔大于24小时
                    .WhereIf(input.IsHistory && string.IsNullOrWhiteSpace(input.AttentionCode) && !input.MyPatient, x =>
                        (x.VisitStatus == VisitStatus.出科 || x.VisitStatus == VisitStatus.已就诊 ||
                         x.VisitStatus == VisitStatus.已退号 || x.VisitStatus == VisitStatus.过号)
                        || ((x.VisitStatus == VisitStatus.待就诊 || x.VisitStatus == VisitStatus.未挂号) &&
                            x.VisitDate > DateTime.Now.Date.AddHours(-24)))
                    .WhereIf(input.MyPatient, x => x.VisitDate.Value.Date == DateTime.Now.Date)
                    //校验责任医生
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyDoctorCode),
                        x => x.DutyDoctorCode == input.DutyDoctorCode)
                    //校验责任护士
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyNurseCode),
                        x => x.DutyNurseCode == input.DutyNurseCode)
                    // 过滤队列中的患者
                    //.WhereIf(registerNos != null && registerNos.Count > 0, x => registerNos.Contains(x.RegisterNo)) //TODO 合并到master再处理，龙岗有，北大没有
                    .WhereIf(!string.IsNullOrEmpty(input.DeptCode), x => x.DeptCode == input.DeptCode.Trim())
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.DeptCode.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.DeptCode == input.DeptCode.Trim())
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.Pflegestufe.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.Pflegestufe == input.Pflegestufe.Trim())
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.BedHeadStickerList.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.BedHeadSticker.Contains(input.BedHeadStickerList))
                    .Count(out var totalCount)
                    .Page(input.PageIndex, input.PageSize)
                    .OrderByDescending(p => p.CallStatus == CallStatus.Calling ? 1 : 0)
                    .OrderBy(c => c.VisitDate == null ? 0 : 1).OrderByDescending(c => c.VisitDate)
                    .OrderByDescending(x => x.VisitStatus == VisitStatus.未挂号 ? 0 : 1) // 未挂号的排在最后面
                    .OrderByIf(!input.IsHistory, x => x.VisitStatus, descending: true) // 非历史患者就诊状态降序排序
                    .OrderByIf(
                        !input.IsHistory && (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea"),
                        x => string.IsNullOrEmpty(x.Bed) ? 0 : 1, descending: true) //床位为空的排序在后面
                    .OrderByIf(
                        !input.IsHistory && (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea"),
                        x => x.Bed) //床位根据序号排序
                    .OrderByPropertyNameIf(input.AreaCode == "OutpatientArea" && !input.IsHistory,
                        nameof(AdmissionRecord.TriageLevel))
                    .OrderBy(x => x.TriageTime)
                    .ToListAsync(a => new AdmissionRecordDto
                    {
                        IsAttention = a.AttentionCode
                    }, cancellationToken);

                //列表属性赋值
                list = await GetAdmissionRecordProperty(list, cancellationToken);

                var res = new PagedResultDto<AdmissionRecordDto>
                {
                    TotalCount = totalCount,
                    Items = list.ToList()
                };
                _log.LogInformation("Get admissionRecord pages success");
                return RespUtil.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogError("Get admissionRecord pages error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<PagedResultDto<AdmissionRecordDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 抢救区，留观区待入或转入列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetWaitingAreaRecordPagedAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default)
        {
            using var uow = _freeSql.CreateUnitOfWork();
            uow.IsolationLevel = System.Data.IsolationLevel.ReadUncommitted;
            try
            {
                var list = await _freeSql.Select<AdmissionRecord>()
                    .Where(x => (x.VisitStatus == VisitStatus.待就诊 || x.VisitStatus == VisitStatus.正在就诊) &&
                                string.IsNullOrEmpty(x.Bed))
                    //校验模糊查询文本
                    .WhereIf(!string.IsNullOrWhiteSpace(input.SearchText),
                        x => x.PatientName.Contains(input.SearchText)
                             || x.VisitNo.Contains(input.SearchText)
                             || x.FirstDoctorCode.Contains(input.SearchText)
                             || x.FirstDoctorName.Contains(input.SearchText)
                    )
                    //校验分诊级别
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageLevel),
                        x => x.TriageLevel == input.TriageLevel)
                    //校验分诊科室
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageDeptCode),
                        x => x.TriageDeptCode == input.TriageDeptCode)
                    // 校验就诊时间
                    .WhereIf(input.StartDate != null && input.EndDate != null && input.StartDate < input.EndDate,
                        x => x.VisitDate.Value.BetweenEnd(input.StartDate.Value, input.EndDate.Value))
                    //校验就诊区域
                    .WhereIf(!input.AreaCode.IsNullOrWhiteSpace(), x => x.AreaCode == input.AreaCode)
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.DeptCode.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.DeptCode == input.DeptCode.Trim())
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.Pflegestufe.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.Pflegestufe == input.Pflegestufe.Trim())
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.BedHeadStickerList.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.BedHeadSticker.Contains(input.BedHeadStickerList))
                    .OrderByIf(
                        !input.IsHistory && (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea"),
                        x => x.RegisterTime, descending: true)
                    .Count(out var totalCount)
                    .Page(input.PageIndex, input.PageSize)
                    .OrderByDescending(x => x.TriageTime)
                    .WithTransaction(uow.GetOrBeginTransaction())
                    .ToListAsync(a => new AdmissionRecordDto
                    {
                        IsAttention = a.AttentionCode
                    }, cancellationToken);

                //列表属性赋值
                list = await GetAdmissionRecordNoBedProperty(list, cancellationToken);

                var res = new PagedResultDto<AdmissionRecordDto>
                {
                    TotalCount = totalCount,
                    Items = list.ToList(),
                };
                uow.Commit();
                return RespUtil.Ok(data: res);
            }
            catch (Exception e)
            {
                uow.Rollback();
                _log.LogError("Get admissionRecord pages error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<PagedResultDto<AdmissionRecordDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 我的患者列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetMyAdmissionRecordPagedAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default)
        {
            using var uow = _freeSql.CreateUnitOfWork();
            uow.IsolationLevel = System.Data.IsolationLevel.ReadUncommitted;
            try
            {
                var list = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(input.VisitStatus != VisitStatus.全部, x => x.VisitStatus == input.VisitStatus)
                    //校验模糊查询文本
                    .WhereIf(!string.IsNullOrWhiteSpace(input.SearchText),
                        x => x.PatientName.Contains(input.SearchText)
                             || x.VisitNo.Contains(input.SearchText)
                             || x.FirstDoctorCode.Contains(input.SearchText)
                             || x.FirstDoctorName.Contains(input.SearchText))

                    // 校验接诊人
                    .WhereIf(!string.IsNullOrWhiteSpace(input.OperatorCode),
                        x => x.OperatorCode == input.OperatorCode)
                    // 校验接诊人
                    .WhereIf(!string.IsNullOrWhiteSpace(input.FirstDoctorCode),
                        x => x.FirstDoctorCode == input.FirstDoctorCode)
                    //校验分诊级别
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageLevel),
                        x => x.TriageLevel == input.TriageLevel)
                    //校验关注医生
                    .WhereIf(!string.IsNullOrWhiteSpace(input.AttentionCode),
                        x => x.AttentionCode.Contains(input.AttentionCode))
                    //校验分诊科室
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageDeptCode),
                        x => x.TriageDeptCode == input.TriageDeptCode)
                    // 校验就诊时间
                    .WhereIf(input.StartDate != null && input.EndDate != null && input.StartDate < input.EndDate,
                        x => x.VisitDate.Value.BetweenEnd(input.StartDate.Value, input.EndDate.Value))
                    //校验就诊区域
                    .WhereIf(!input.AreaCode.IsNullOrWhiteSpace(), x => input.AreaCode.Contains(x.AreaCode))
                    // 非历史患者应查询到以下两种状态患者
                    // 1.正在就诊状态患者
                    // 2.未挂号、待就诊并且分诊时间与当前时间间隔小于24小时患者
                    .WhereIf(!input.IsHistory && string.IsNullOrWhiteSpace(input.AttentionCode) && !input.MyPatient,
                        x =>
                            (x.VisitStatus == VisitStatus.正在就诊)
                            || ((x.VisitStatus == VisitStatus.未挂号 ||
                                 x.VisitStatus == VisitStatus.待就诊) &&
                                x.TriageTime > DateTime.Now.Date.AddHours(-24)))
                    // 历史患者应查询到以下状态患者
                    // 1.出科、已就诊、已退号、过号状态患者
                    // 2.待就诊、未挂号状态且分诊时间与当前时间间隔大于24小时
                    .WhereIf(input.MyPatient, x => x.VisitDate.Value > DateTime.Now.AddDays(-1))
                    //校验责任医生
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyDoctorCode),
                        x => x.DutyDoctorCode == input.DutyDoctorCode)
                    //校验责任护士
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyNurseCode),
                        x => x.DutyNurseCode == input.DutyNurseCode)
                    .Count(out var totalCount)
                    .Page(input.PageIndex, input.PageSize)
                    .OrderByDescending(x => x.VisitStatus == VisitStatus.未挂号 ? 0 : 1) // 未挂号的排在最后面
                    .OrderByDescending(x => x.TriageTime)
                    .WithTransaction(uow.GetOrBeginTransaction())
                    .ToListAsync(a => new AdmissionRecordDto
                    {
                        IsAttention = a.AttentionCode
                    }, cancellationToken);

                //列表属性赋值
                list = await GetAdmissionRecordProperty(list, cancellationToken);

                var res = new PagedResultDto<AdmissionRecordDto>
                {
                    TotalCount = totalCount,
                    Items = list.ToList()
                };
                uow.Commit();
                _log.LogInformation("Get admissionRecord pages success");
                return RespUtil.Ok(data: res);
            }
            catch (Exception e)
            {
                uow.Rollback();
                _log.LogError("Get admissionRecord pages error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<PagedResultDto<AdmissionRecordDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 我的关注列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetMyFollowAdmissionRecordPagedAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(input.VisitStatus != VisitStatus.全部, x => x.VisitStatus == input.VisitStatus)
                    //校验模糊查询文本
                    .WhereIf(!string.IsNullOrWhiteSpace(input.SearchText),
                        x => x.PatientName.Contains(input.SearchText)
                             || x.VisitNo.Contains(input.SearchText)
                             || x.FirstDoctorCode.Contains(input.SearchText)
                             || x.FirstDoctorName.Contains(input.SearchText))

                    // 校验接诊人
                    .WhereIf(!string.IsNullOrWhiteSpace(input.OperatorCode),
                        x => x.OperatorCode == input.OperatorCode)
                    // 校验接诊人
                    .WhereIf(!string.IsNullOrWhiteSpace(input.FirstDoctorCode),
                        x => x.FirstDoctorCode == input.FirstDoctorCode)
                    //校验分诊级别
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageLevel),
                        x => x.TriageLevel == input.TriageLevel)
                    //校验关注医生
                    .WhereIf(!string.IsNullOrWhiteSpace(input.AttentionCode),
                        x => x.AttentionCode.Contains(input.AttentionCode))
                    //校验分诊科室
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageDeptCode),
                        x => x.TriageDeptCode == input.TriageDeptCode)
                    // 校验就诊时间
                    .WhereIf(input.StartDate != null && input.EndDate != null && input.StartDate < input.EndDate,
                        x => x.VisitDate.Value.BetweenEnd(input.StartDate.Value, input.EndDate.Value))
                    //校验就诊区域
                    .WhereIf(!input.AreaCode.IsNullOrWhiteSpace(), x => input.AreaCode.Contains(x.AreaCode))
                    // 非历史患者应查询到以下两种状态患者
                    // 1.正在就诊状态患者
                    // 2.未挂号、待就诊并且分诊时间与当前时间间隔小于24小时患者
                    .WhereIf(!input.IsHistory && string.IsNullOrWhiteSpace(input.AttentionCode) && !input.MyPatient,
                        x =>
                            (x.VisitStatus == VisitStatus.正在就诊)
                            || ((x.VisitStatus == VisitStatus.未挂号 ||
                                 x.VisitStatus == VisitStatus.待就诊) &&
                                x.TriageTime > DateTime.Now.Date.AddHours(-24)))
                    // 历史患者应查询到以下状态患者
                    // 1.出科、已就诊、已退号、过号状态患者
                    // 2.待就诊、未挂号状态且分诊时间与当前时间间隔大于24小时
                    .WhereIf(input.MyPatient, x => x.VisitDate.Value.Date == DateTime.Now.Date)
                    //校验责任医生
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyDoctorCode),
                        x => x.DutyDoctorCode == input.DutyDoctorCode)
                    //校验责任护士
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyNurseCode),
                        x => x.DutyNurseCode == input.DutyNurseCode)
                    // 过滤队列中的患者
                    .Count(out var totalCount)
                    .Page(input.PageIndex, input.PageSize)
                    .OrderByDescending(x => x.VisitStatus == VisitStatus.未挂号 ? 0 : 1) // 未挂号的排在最后面
                    .OrderByDescending(x => x.TriageTime)
                    .ToListAsync(a => new AdmissionRecordDto
                    {
                        IsAttention = a.AttentionCode
                    }, cancellationToken);

                //列表属性赋值
                list = await GetAdmissionRecordProperty(list, cancellationToken);

                var res = new PagedResultDto<AdmissionRecordDto>
                {
                    TotalCount = totalCount,
                    Items = list.ToList()
                };
                _log.LogInformation("Get admissionRecord pages success");
                return RespUtil.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogError("Get admissionRecord pages error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<PagedResultDto<AdmissionRecordDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 待结果患者列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetAdmissionWaitRecordPagedAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(input.VisitStatus != VisitStatus.全部, x => x.VisitStatus == input.VisitStatus)
                    .WhereIf(input.PatientStatus.HasValue,
                        x => x.PatientStatus == input.PatientStatus.Value && x.VisitStatus == VisitStatus.正在就诊)
                    //校验模糊查询文本
                    .WhereIf(!string.IsNullOrWhiteSpace(input.SearchText),
                        x => x.PatientName.Contains(input.SearchText)
                             || x.VisitNo.Contains(input.SearchText)
                             || x.FirstDoctorCode.Contains(input.SearchText)
                             || x.FirstDoctorName.Contains(input.SearchText))
                    //校验分诊级别
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageLevel),
                        x => x.TriageLevel == input.TriageLevel)
                    //校验分诊科室
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageDeptCode),
                        x => x.TriageDeptCode == input.TriageDeptCode)
                    // 校验就诊时间
                    .WhereIf(input.StartDate != null && input.EndDate != null && input.StartDate < input.EndDate,
                        x => x.VisitDate.Value.BetweenEnd(input.StartDate.Value, input.EndDate.Value))
                    //校验就诊区域
                    .WhereIf(!input.AreaCode.IsNullOrWhiteSpace(), x => input.AreaCode.Contains(x.AreaCode))
                    .Count(out var totalCount)
                    .Page(input.PageIndex, input.PageSize)
                    .OrderByDescending(x => x.VisitStatus == VisitStatus.未挂号 ? 0 : 1) // 未挂号的排在最后面
                    .OrderByDescending(x => x.TriageTime)
                    .ToListAsync(a => new AdmissionRecordDto
                    {
                        IsAttention = a.AttentionCode
                    }, cancellationToken);

                //列表属性赋值
                list = await GetAdmissionRecordProperty(list, cancellationToken);

                var res = new PagedResultDto<AdmissionRecordDto>
                {
                    TotalCount = totalCount,
                    Items = list.ToList()
                };
                _log.LogInformation("Get admissionRecord pages success");
                return RespUtil.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogError("Get admissionRecord pages error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<PagedResultDto<AdmissionRecordDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 历史诊疗患者列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetHistoryAdmissionRecordPagedAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(input.VisitStatus != VisitStatus.全部, x => x.VisitStatus == input.VisitStatus)
                    //校验模糊查询文本
                    .WhereIf(!string.IsNullOrWhiteSpace(input.SearchText),
                        x => x.PatientName.Contains(input.SearchText)
                             || x.VisitNo.Contains(input.SearchText)
                             || x.FirstDoctorCode.Contains(input.SearchText)
                             || x.FirstDoctorName.Contains(input.SearchText)
                             || x.PatientNamePy.Contains(input.SearchText))

                    //诊断搜索 
                    .WhereIf(!string.IsNullOrEmpty(input.DiagnoseSearcheText),
                        x => _freeSql.Select<DiagnoseRecord>()
                            .Where(dd =>
                                dd.DiagnoseName.Contains(input.DiagnoseSearcheText) &&
                                dd.DiagnoseClassCode == DiagnoseClass.开立 && !dd.IsDeleted)
                            .Any(dd => dd.PI_ID == x.PI_ID))

                    // 校验接诊人
                    .WhereIf(!string.IsNullOrWhiteSpace(input.OperatorCode),
                        x => x.OperatorCode == input.OperatorCode)
                    // 校验接诊人
                    .WhereIf(!string.IsNullOrWhiteSpace(input.FirstDoctorCode),
                        x => x.FirstDoctorCode == input.FirstDoctorCode)
                    //校验分诊级别
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageLevel),
                        x => x.TriageLevel == input.TriageLevel)
                    //校验关注医生
                    .WhereIf(!string.IsNullOrWhiteSpace(input.AttentionCode),
                        x => x.AttentionCode.Contains(input.AttentionCode))
                    //校验分诊科室
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TriageDeptCode),
                        x => x.TriageDeptCode == input.TriageDeptCode)
                    // 校验就诊时间
                    .WhereIf(input.StartDate != null && input.EndDate != null && input.StartDate < input.EndDate,
                        x => x.VisitDate.Value.BetweenEnd(input.StartDate.Value, input.EndDate.Value))
                    //校验就诊区域
                    .WhereIf(!input.AreaCode.IsNullOrWhiteSpace(), x => input.AreaCode.Contains(x.AreaCode))
                    // 历史患者应查询到以下状态患者
                    // 1.出科、已就诊、已退号、过号状态患者
                    // 2.待就诊、未挂号状态且分诊时间与当前时间间隔大于24小时
                    //.WhereIf(input.IsHistory && string.IsNullOrWhiteSpace(input.AttentionCode) && !input.MyPatient, x =>
                    //    (x.VisitStatus == VisitStatus.出科 || x.VisitStatus == VisitStatus.已就诊 ||
                    //     x.VisitStatus == VisitStatus.已退号 || x.VisitStatus == VisitStatus.过号)
                    //    || ((x.VisitStatus == VisitStatus.待就诊 || x.VisitStatus == VisitStatus.未挂号) &&
                    //        x.VisitDate > DateTime.Now.Date.AddHours(-24)))
                    .WhereIf(input.MyPatient, x => x.VisitDate.Value.Date == DateTime.Now.Date)
                    //校验责任医生
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyDoctorCode),
                        x => x.DutyDoctorCode == input.DutyDoctorCode)
                    //校验责任护士
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyNurseCode),
                        x => x.DutyNurseCode == input.DutyNurseCode)
                    // 过滤队列中的患者
                    //.WhereIf(registerNos != null && registerNos.Count > 0, x => registerNos.Contains(x.RegisterNo)) //TODO 合并到master再处理，龙岗有，北大没有
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.DeptCode.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.DeptCode == input.DeptCode.Trim())
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.Pflegestufe.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.Pflegestufe == input.Pflegestufe.Trim())
                    .WhereIf(
                        (input.AreaCode == "RescueArea" || input.AreaCode == "ObservationArea") &&
                        !input.BedHeadStickerList.IsNullOrEmpty(),
                        x => string.IsNullOrEmpty(x.Bed) || x.BedHeadSticker.Contains(input.BedHeadStickerList))
                    .Count(out var totalCount)
                    .Page(input.PageIndex, input.PageSize)
                    .OrderByDescending(x => x.VisitStatus == VisitStatus.未挂号 ? 0 : 1) // 未挂号的排在最后面
                    .OrderByPropertyNameIf(input.AreaCode == "OutpatientArea" && !input.IsHistory,
                        nameof(AdmissionRecord.VisitStatus), false)
                    .OrderByPropertyNameIf(input.AreaCode == "OutpatientArea" && !input.IsHistory,
                        nameof(AdmissionRecord.TriageLevel))
                    .OrderByPropertyNameIf(input.AreaCode == "OutpatientArea" && !input.IsHistory,
                        nameof(AdmissionRecord.VisitDate), false)
                    .OrderByDescending(x => x.TriageTime)
                    .ToListAsync(a => new AdmissionRecordDto
                    {
                        IsAttention = a.AttentionCode
                    }, cancellationToken);

                //列表属性赋值
                list = await GetAdmissionRecordProperty(list, cancellationToken);

                var res = new PagedResultDto<AdmissionRecordDto>
                {
                    TotalCount = totalCount,
                    Items = list.ToList()
                };
                _log.LogInformation("Get admissionRecord pages success");
                return RespUtil.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogError("Get admissionRecord pages error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<PagedResultDto<AdmissionRecordDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// /// <summary>
        /// 患者列表属性赋值
        /// </summary>
        /// <returns></returns>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<List<AdmissionRecordDto>> GetAdmissionRecordProperty(List<AdmissionRecordDto> list,
            CancellationToken cancellationToken = default)
        {
            var piIds = list.Select(s => s.PI_ID).Distinct().ToList();

            // 查询诊断编码
            var diagnoseRecordDict = (await _freeSql.Select<DiagnoseRecord>()
                    .Where(x => x.DiagnoseClassCode == DiagnoseClass.开立 && piIds.Contains(x.PI_ID) && !x.IsDeleted)
                    .ToListAsync())
                .GroupBy(x => x.PI_ID)
                .ToDictionary(g => g.Key, g => new
                {
                    DiagnoseCodeList = g.Select(s => s.DiagnoseCode).ToList(),
                    DiagnoseNameList = g.Select(s => s.DiagnoseName).ToList()
                });

            var lastTimeAxisRecords = _freeSql.Select<TimeAxisRecord>()
                .Where(x => piIds.Contains(x.PI_ID) && x.TimePointCode == "InDeptTime")
                .GroupBy(x => x.PI_ID)
                .ToList(x => new KeyValuePair<Guid, DateTime>(x.Key, x.Max(x.Value.Time)));

            Parallel.ForEach(list, item =>
            {
                if (diagnoseRecordDict.TryGetValue(item.PI_ID, out var diagnoseRecord))
                {
                    item.DiagnoseCode = string.Join(",", diagnoseRecord.DiagnoseCodeList);
                    item.DiagnoseName = string.Join(",", diagnoseRecord.DiagnoseNameList);
                }

                item.IsAttention = (!string.IsNullOrWhiteSpace(item.IsAttention) && CurrentUser != null &&
                                    !CurrentUser.UserName.IsNullOrWhiteSpace() &&
                                    item.IsAttention.Contains(CurrentUser.UserName)).ToString();

                //实时计算滞留时长
                item.RetentionTime = GetRetentionTime(item);

                //实时计算入科滞留时长
                var inDeptRetentionTime = GetInDeptRetentionTime(item, lastTimeAxisRecords);
                item.RescueRetentionViewTime = inDeptRetentionTime.Item1;
                item.ObservationRetentionViewTime = inDeptRetentionTime.Item2;

                //实时计算等待时长
                item.WaitingTime = GetWaitingTime(item);

                string outDeptInfo = !string.IsNullOrWhiteSpace(item.OutDeptReasonName)
                    ? "出科理由：" + item.OutDeptReasonName
                    : "";
                switch (item.OutDeptReasonCode)
                {
                    case "1": //转住院
                        outDeptInfo += !string.IsNullOrWhiteSpace(item.OutDeptName) ? "， 出科信息：" + item.OutDeptName : "";
                        break;
                    case "2": //死亡
                        outDeptInfo += item.DeathTime.HasValue
                            ? "， 死亡时间：" + item.DeathTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "";
                        break;
                    case "3":
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }

                //出科信息
                item.OutDeptInfo = outDeptInfo;
            });

            return list;
        }

        /// <summary>
        /// /// <summary>
        /// 抢救留观待入或转入
        /// </summary>
        /// <returns></returns>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<List<AdmissionRecordDto>> GetAdmissionRecordNoBedProperty(List<AdmissionRecordDto> list,
            CancellationToken cancellationToken = default)
        {
            var piIds = list.Select(s => s.PI_ID).Distinct().ToList();

            // 查询流转记录
            var transferListDict = (await _freeSql.Select<TransferRecord>()
                    .Where(x => piIds.Contains(x.PI_ID) &&
                                (x.ToAreaCode == "ObservationArea" || x.ToAreaCode == "RescueArea") &&
                                x.TransferReason != "预检分诊")
                    .ToListAsync())
                .GroupBy(x => x.PI_ID)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(o => o.TransferTime).FirstOrDefault());

            Parallel.ForEach(list, item =>
            {
                if (transferListDict.TryGetValue(item.PI_ID, out var transferRecord))
                {
                    item.IsHasTransfer = transferRecord?.ToAreaCode == Area.ObservationArea.ToString() ||
                                         transferRecord?.ToAreaCode == Area.RescueArea.ToString();
                }
            });

            return list;
        }

        /// <summary>
        /// 计算滞留时间
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private string GetRetentionTime(AdmissionRecordDto patient)
        {
            //1、抢救区、留观区：在患者床位卡上，只要拖入床位开始计算滞留时间
            //2、就诊区、历史就诊：列表上的“滞留时间”是患者从“分诊 / 挂号”~"结束就诊/出科" 的时间

            var retentionTime = string.Empty;

            try
            {
                DateTime endTime = DateTime.Now;
                if (patient.VisitStatus == VisitStatus.已就诊 || patient.VisitStatus == VisitStatus.出科)
                {
                    endTime = (DateTime)(patient.OutDeptTime ?? patient.FinishVisitTime);
                }

                if (patient.VisitStatus == VisitStatus.过号)
                {
                    endTime = patient.ExpireNumberTime ?? patient.TriageTime.AddHours(24);
                }

                if (patient.VisitStatus == VisitStatus.已退号)
                {
                    if (patient.FinishVisitTime != null)
                    {
                        endTime = (DateTime)patient.FinishVisitTime;
                    }
                    else
                    {
                        return "";
                    }
                }

                var surplusTime = endTime.Subtract(patient.TriageTime).TotalMinutes;
                if (surplusTime <= 0)
                {
                    surplusTime = endTime.Subtract(patient.TriageTime).TotalSeconds;
                    retentionTime = (int)surplusTime + "秒";
                }
                else
                {
                    if (surplusTime > 60)
                    {
                        retentionTime = (int)(surplusTime / 60) + "时" + (int)(surplusTime % 60) + "分";
                    }
                    else
                    {
                        retentionTime = ((int)surplusTime) + "分";
                    }
                }
            }
            catch (Exception)
            {
                retentionTime = string.Empty;
            }

            return retentionTime;
        }

        /// <summary>
        /// 计算入科滞留时间
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="lastTimeAxisRecords"></param>
        /// <returns></returns>
        private (string, string) GetInDeptRetentionTime(AdmissionRecordDto patient,
            List<KeyValuePair<Guid, DateTime>> lastTimeAxisRecords)
        {
            //1、抢救区、留观区：在患者床位卡上，只要拖入床位开始计算滞留时间
            //2、就诊区、历史就诊：列表上的“滞留时间”是患者从“分诊 / 挂号”~"结束就诊/出科" 的时间

            var rescueRetentionTime = string.Empty;
            var observationRetentionTime = string.Empty;

            try
            {
                DateTime endTime = DateTime.Now;
                if (patient.VisitStatus == VisitStatus.已就诊 || patient.VisitStatus == VisitStatus.出科)
                {
                    endTime = (DateTime)(patient.OutDeptTime ?? patient.FinishVisitTime);
                }

                if (patient.VisitStatus == VisitStatus.过号)
                {
                    endTime = patient.ExpireNumberTime ?? patient.TriageTime.AddHours(24);
                }

                if (patient.VisitStatus == VisitStatus.已退号)
                {
                    if (patient.FinishVisitTime != null)
                    {
                        endTime = (DateTime)patient.FinishVisitTime;
                    }
                    else
                    {
                        return default;
                    }
                }

                double rescueSurplusTime = 0; //抢救时长
                double observationSurplusTime = 0; //留观时长
                // TimeAxisRecord lastTimeAxisRecord = null;
                // //只有正在就诊的需要实时计算
                // if (patient.VisitStatus == VisitStatus.正在就诊 && !string.IsNullOrWhiteSpace(patient.Bed))
                // {
                //     lastTimeAxisRecord = _freeSql.Select<TimeAxisRecord>().Where(a => a.PI_ID == patient.PI_ID && a.TimePointCode == "InDeptTime").OrderByDescending(p => p.Time).First();
                // }
                DateTime lastTime = lastTimeAxisRecords.First(x => x.Key == patient.PI_ID).Value;
                if (patient.InDeptTime.HasValue)
                {
                    bool isAreaPeople =
                        new List<string>() { "RescueArea", "ObservationArea" }.Contains(patient.AreaCode);
                    switch (patient.AreaCode)
                    {
                        case "RescueArea":
                            rescueSurplusTime = lastTime != null
                                ? patient.RescueRetentionTime + endTime.Subtract(lastTime).TotalMinutes
                                : patient.RescueRetentionTime;
                            observationSurplusTime = patient.ObservationRetentionTime;
                            break;
                        case "ObservationArea":
                            rescueSurplusTime = patient.RescueRetentionTime;
                            observationSurplusTime = lastTime != null
                                ? patient.ObservationRetentionTime + endTime.Subtract(lastTime).TotalMinutes
                                : patient.ObservationRetentionTime;
                            break;
                        default:
                            break;
                    }

                    if (rescueSurplusTime > 60)
                    {
                        rescueRetentionTime = (int)(rescueSurplusTime / 60) + "时" + (int)(rescueSurplusTime % 60) + "分";
                    }
                    else
                    {
                        rescueRetentionTime = ((int)rescueSurplusTime) + "分";
                    }


                    if (observationSurplusTime > 60)
                    {
                        observationRetentionTime = (int)(observationSurplusTime / 60) + "时" +
                                                   (int)(observationSurplusTime % 60) + "分";
                    }
                    else
                    {
                        observationRetentionTime = ((int)observationSurplusTime) + "分";
                    }
                }
            }
            catch (Exception)
            {
                return (string.Empty, string.Empty);
            }

            return (rescueRetentionTime, observationRetentionTime);
        }

        /// <summary>
        /// 计算等待时间
        /// </summary>
        /// <returns></returns>
        private string GetWaitingTime(AdmissionRecordDto patient)
        {
            var waitingTime = string.Empty;
            try
            {
                if (patient.VisitDate == null &&
                    (patient.VisitStatus == VisitStatus.过号 || patient.VisitStatus == VisitStatus.已退号))
                {
                    return null;
                }
                else
                {
                    // 设置接诊时间为当前时间
                    var visitDate = patient.VisitDate ?? DateTime.Now;
                    var surplusTime = visitDate.Subtract(patient.TriageTime).TotalMinutes;
                    if (surplusTime <= 0)
                    {
                        surplusTime = visitDate.Subtract(patient.TriageTime).TotalSeconds;
                        waitingTime = (int)surplusTime + "秒";
                    }
                    else
                    {
                        if (surplusTime > 60)
                        {
                            waitingTime = (int)(surplusTime / 60) + "时" + (int)(surplusTime % 60) + "分";
                        }
                        else
                        {
                            waitingTime = ((int)surplusTime) + "分";
                        }
                    }

                    return waitingTime;
                }
            }
            catch (Exception)
            {
                return waitingTime;
            }
        }

        #endregion

        /// <summary>
        /// 通过入科流水号获取用户入科后所在区域信息
        /// </summary>
        /// <param name="pI_ID">PIID 分诊库患者入科流水号</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ResponseResult<AreaDto>> GetPatientAreaByPiidAsync(Guid pI_ID,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var record = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(pI_ID != Guid.Empty, x => x.PI_ID == pI_ID)
                    .FirstAsync<AdmissionRecordDto>(cancellationToken);
                var area = new AreaDto()
                {
                    PI_ID = pI_ID,
                    PatientID = record?.PatientID,
                    PatientName = record?.PatientName,
                    AreaCode = record?.AreaCode,
                    AreaName = record?.AreaName,
                    VisitStatus = (int)(record?.VisitStatus ?? 0)
                };

                _log.LogInformation("Get areaDto by piid success");
                return RespUtil.Ok(data: area);
            }
            catch (Exception e)
            {
                _log.LogError("Get areaDto by piid error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<AreaDto>(extra: e.Message);
            }
        }

        /// <summary>
        /// 通过Id获取指定用户入科信息
        /// </summary>
        /// <param name="aR_ID">自增Id</param>
        /// <param name="pI_ID">PVID 分诊库患者基本信息表主键ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<AdmissionRecordDto>> GetAdmissionRecordByIdAsync(int aR_ID, Guid pI_ID,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var record = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(aR_ID > 0, x => x.AR_ID == aR_ID)
                    .WhereIf(pI_ID != Guid.Empty, x => x.PI_ID == pI_ID)
                    .FirstAsync<AdmissionRecordDto>(cancellationToken);
                var diagnoseList = await _freeSql.Select<DiagnoseRecord>()
                    .Where(x => x.IsDeleted == false && x.DiagnoseClassCode == DiagnoseClass.开立)
                    .WhereIf(pI_ID != Guid.Empty, x => x.PI_ID == pI_ID)
                    .OrderBy(o => o.Sort)
                    .ToListAsync(a => new
                    {
                        a.DiagnoseCode,
                        a.DiagnoseName,
                        a.MedicalType,
                        a.DiagnoseTypeCode,
                        a.Sort,
                        a.Remark
                    }, cancellationToken);
                if (record != null)
                {
                    var vital = await _freeSql.Select<VitalSignInfo>().Where(w => w.PI_ID == record.PI_ID)
                        .FirstAsync<VitalSignInfoDto>(cancellationToken);
                    var score = await _freeSql.Select<ScoreInfo>().Where(w => w.PI_ID == record.PI_ID)
                        .ToListAsync<ScoreInfoDto>(cancellationToken);
                    record.VitalSignInfo = vital;
                    record.ScoreInfo = score;
                    record.IsAttention =
                        (!string.IsNullOrWhiteSpace(record.IsAttention) &&
                         record.IsAttention.Contains(CurrentUser.UserName)).ToString();
                    var diagnoseCode = "";
                    var diagnoseName = "";
                    foreach (var diagnose in diagnoseList.GroupBy(g => g.MedicalType).ToList())
                    {
                        int index = 1;
                        foreach (var d in diagnose)
                        {
                            var diagnoseTypeCode = d.DiagnoseTypeCode == "Suspected" ? "?" : "";
                            var remark = string.IsNullOrEmpty(d.Remark.Trim()) ? "" : $"({d.Remark})";
                            diagnoseCode += d.DiagnoseCode + ",";
                            diagnoseName += index + "." + d.DiagnoseName + remark + diagnoseTypeCode + ",";
                            index++;
                        }
                    }

                    record.DiagnoseCode = diagnoseCode.TrimEnd(',');
                    record.DiagnoseName = diagnoseName.TrimEnd(',');
                    record.IsFreeNumber = "4".Equals(record.RegType); //record.ChargeTypeName.Contains("免费");
                    //查询代办人信息
                    var agentInfo = await _freeSql.Select<AgencyPeople>().Where(x => x.PiId == record.PI_ID)
                        .FirstAsync<AgencyPeopleDto>(cancellationToken);
                    _log.LogInformation("agentInfo:{0}", agentInfo);
                    if (agentInfo != null)
                    {
                        record.AgencyPeopleName = agentInfo.AgencyPeopleName;
                        record.AgencyPeopleCard = agentInfo.AgencyPeopleCard;
                        record.AgencyPeopleAge = agentInfo.AgencyPeopleAge;
                        record.AgencyPeopleSex = agentInfo.AgencyPeopleSex;
                        record.AgencyPeopleMobile = agentInfo.AgencyPeopleMobile;
                    }
                }

                _log.LogInformation("Get admissionRecord by id success");
                return RespUtil.Ok(data: record);
            }
            catch (Exception e)
            {
                _log.LogError("Get admissionRecord by id error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<AdmissionRecordDto>(extra: e.Message);
            }
        }

        /// <summary>
        /// 患者出科
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> PatientOutDeptAsync(UpdateAdmissionByOutDeptDto dto, CancellationToken cancellationToken)
        {
            string operatorCode = CurrentUser.UserName;
            string operatorName = CurrentUser.FindClaimValue("fullName");
            try
            {
                AdmissionRecord admissionRecord = await _freeSql.Select<AdmissionRecord>()
                    .Where(a => a.AR_ID == dto.AR_ID).FirstAsync(cancellationToken);
                if (admissionRecord == null)
                {
                    return RespUtil.Error<string>(msg: "患者不存在！");
                }

                //获取出科字典名称
                GetDictionariesResponse outDeptReasonModel = await _grpcClient.GetOutDeptReason(dto.OutDeptReason);

                string outDeptReasonName = outDeptReasonModel?.DictionariesName ?? null;

                if (dto.OutDeptTime == null)
                    dto.OutDeptTime = DateTime.Now;

                if (dto.IsCancelGreen)
                {
                    //添加绿通记录
                    if (admissionRecord.IsOpenGreenChannl)
                    {
                        await SyncGreedIntoToHis(admissionRecord, false);
                        admissionRecord.IsOpenGreenChannl = false;
                        admissionRecord.GreenRoadCode = string.Empty;
                        admissionRecord.GreenRoadName = string.Empty;
                        string bedHeadSticker = admissionRecord.BedHeadSticker ?? string.Empty;
                        List<string> bedHeadStickerList = bedHeadSticker.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                        bedHeadStickerList.Remove("greenRoad");
                        admissionRecord.BedHeadSticker = string.Join(",", bedHeadStickerList);


                        GreenChannlRecord greenChannlRecord = await _freeSql.Select<GreenChannlRecord>()
                            .Where(x => x.PI_ID == admissionRecord.PI_ID).OrderByDescending(p => p.BeginTime)
                            .FirstAsync();
                        if (greenChannlRecord != null)
                        {
                            greenChannlRecord.EndTime = DateTime.Now;
                            await _freeSql.Update<GreenChannlRecord>().SetSource(greenChannlRecord)
                                .ExecuteAffrowsAsync();
                        }
                    }
                }

                string retentionTime = string.Empty;
                double surplusTime = 0;
                DateTime endTime = dto.OutDeptTime.Value;
                DateTime startTime = DateTime.Now;

                //如果是召回的使用召回时间计算，否则使用入科时间
                if (admissionRecord.RecallTime.HasValue)
                {
                    startTime = admissionRecord.RecallTime.Value;
                }
                else
                {
                    TimeAxisRecord lastTimeAxisRecord = await _freeSql.Select<TimeAxisRecord>()
                        .Where(a => a.PI_ID == admissionRecord.PI_ID && a.TimePointCode == "InDeptTime")
                        .OrderByDescending(p => p.Time).FirstAsync();
                    startTime = lastTimeAxisRecord?.Time ?? startTime;
                }

                if (admissionRecord.InDeptTime.HasValue)
                {
                    switch (admissionRecord.AreaCode)
                    {
                        case "RescueArea":
                            surplusTime = admissionRecord.RescueRetentionTime +
                                          endTime.Subtract(startTime).TotalMinutes;
                            break;
                        case "ObservationArea":
                            surplusTime = admissionRecord.ObservationRetentionTime +
                                          endTime.Subtract(startTime).TotalMinutes;
                            break;
                        default:
                            break;
                    }
                }

                admissionRecord.OutDeptTime = dto.OutDeptTime;
                admissionRecord.SupplementaryNotes = dto.SupplementaryNotes;
                admissionRecord.KeyDiseasesCode = dto.KeyDiseasesCode;
                admissionRecord.KeyDiseasesName = dto.KeyDiseasesName;
                admissionRecord.DeathTime = dto.DeathTime;
                admissionRecord.OutDeptReasonCode = dto.OutDeptReason;
                admissionRecord.OutDeptReasonName = outDeptReasonName;
                admissionRecord.VisitStatus = VisitStatus.出科;
                admissionRecord.OutDeptCode = dto.DeptCode;
                admissionRecord.OutDeptName = dto.DeptName;
                admissionRecord.RecallTime = null;
                if (admissionRecord.AreaCode == Area.RescueArea.ToString())
                {
                    admissionRecord.RescueRetentionTime = surplusTime;
                }

                if (admissionRecord.AreaCode == Area.ObservationArea.ToString())
                {
                    admissionRecord.ObservationRetentionTime = surplusTime;
                }

                await _freeSql.Update<AdmissionRecord>().SetSource(admissionRecord).ExecuteAffrowsAsync();

                OutDeptRecord outDeptRecord = new OutDeptRecord()
                {
                    Id = Guid.NewGuid(),
                    PI_Id = admissionRecord.PI_ID,
                    OutDeptTime = admissionRecord.OutDeptTime.Value,
                    CreateTime = DateTime.Now
                };
                await _freeSql.Insert(outDeptRecord).ExecuteAffrowsAsync();

                if (dto.OutDeptReason == "Death" && dto.DeathTime != null)
                {
                    await _freeSql.Insert(new TimeAxisRecord()
                    {
                        PI_ID = admissionRecord.PI_ID,
                        Time = dto.DeathTime.Value,
                        TimePointCode = TimePoint.DeathTime.ToString(),
                    }.SetTimePointName()).ExecuteAffrowsAsync();
                }

                TransferType transferTypeCode = dto.OutDeptReason == "Death"
                    ? TransferType.Death
                    : TransferType.OutDept;
                TransferRecord transferRecord = new TransferRecord()
                {
                    PI_ID = admissionRecord.PI_ID,
                    PatientID = admissionRecord.PatientID,
                    VisitNo = admissionRecord.VisitNo,
                    TransferTime = DateTime.Now,
                    OperatorCode = operatorCode,
                    OperatorName = operatorName,
                    TransferTypeCode = transferTypeCode,
                    TransferType = transferTypeCode.GetDescription(),
                    FromAreaCode = admissionRecord.AreaCode,
                    ToAreaCode = TransferType.OutDept.ToString(),
                    ToArea = TransferType.OutDept.GetDescription(),
                    FromDeptCode = admissionRecord.DeptCode,
                    ToDeptCode = transferTypeCode.ToString(),
                    ToDept = transferTypeCode.GetDescription(),
                    TransferReasonCode = dto.OutDeptReason,
                    TransferReason = dto.OutDeptReason == "Death"
                        ? outDeptReasonName + " " + dto.DeathTime
                        : outDeptReasonName
                };
                _freeSql.Insert(transferRecord).ExecuteAffrows();

                TimeAxisRecord timeAxisRecord = new TimeAxisRecord()
                {
                    PI_ID = admissionRecord.PI_ID,
                    TimePointCode = TimePoint.OutDeptTime.ToString(),
                    Time = admissionRecord.OutDeptTime.Value,
                }.SetTimePointName();
                _freeSql.Insert(timeAxisRecord).ExecuteAffrows();

                if (admissionRecord.AreaCode == Area.RescueArea.ToString())
                {
                    RescueRecord inRescueRecord = _freeSql.Select<RescueRecord>()
                        .Where(x => x.PI_Id == admissionRecord.PI_ID && x.TimePointName == "inrescue")
                        .OrderByDescending(x => x.TimePoint).First();
                    if (inRescueRecord != null)
                    {
                        RescueRecord rescueRecord = new RescueRecord()
                        {
                            Id = Guid.NewGuid(),
                            PI_Id = admissionRecord.PI_ID,
                            TimePointName = "outrescue",
                            TimePoint = admissionRecord.OutDeptTime.Value,
                            OperateCode = operatorCode,
                            OperateName = operatorName,
                        };

                        rescueRecord.Retention = (rescueRecord.TimePoint - inRescueRecord.TimePoint).TotalMinutes;
                        _freeSql.Insert(rescueRecord).ExecuteAffrows();
                    }
                }

                if (admissionRecord.AreaCode == Area.ObservationArea.ToString())
                {
                    RescueRecord inObservationRecord = _freeSql.Select<RescueRecord>()
                        .Where(x => x.PI_Id == admissionRecord.PI_ID && x.TimePointName == "inobservation")
                        .OrderByDescending(x => x.TimePoint).First();
                    if (inObservationRecord != null)
                    {
                        RescueRecord outObservationRecord = new RescueRecord()
                        {
                            Id = Guid.NewGuid(),
                            PI_Id = admissionRecord.PI_ID,
                            TimePointName = "outobservation",
                            TimePoint = admissionRecord.OutDeptTime.Value,
                            OperateCode = operatorCode,
                            OperateName = operatorName,
                        };

                        outObservationRecord.Retention =
                            (outObservationRecord.TimePoint - inObservationRecord.TimePoint).TotalMinutes;
                        _freeSql.Insert(outObservationRecord).ExecuteAffrows();
                    }
                }

                DiagnoseRecord diagnoseRecord = await _freeSql.Select<DiagnoseRecord>().Where(x =>
                    x.DiagnoseClassCode == DiagnoseClass.开立
                    && x.PI_ID == admissionRecord.PI_ID && !x.IsDeleted).FirstAsync();

                await _hisApi.SaveVisitRecordAsync(admissionRecord, diagnoseRecord, TransferType.OutDept, operatorCode,
                    operatorName);

                await _capPublisher.PublishAsync("patient.visitstatus.changed",
                    new { Id = admissionRecord.PI_ID, VisitStatus = VisitStatus.出科, FinishVisitTime = dto.OutDeptTime },
                    cancellationToken: cancellationToken);

                await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.OutDept, admissionRecord);
                await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.HospitalSettlement, admissionRecord);

                // 推送出科时间和状态到电子病历
                await _capPublisher.PublishAsync("sync.patient.outDeptTime.emr", new Dictionary<string, object>
                {
                    { "PI_ID", admissionRecord.PI_ID },
                    { "OutDeptTime", dto.OutDeptTime }
                });

                return RespUtil.Ok<string>();
            }
            catch (Exception e)
            {
                _log.LogError("患者出科报错：" + e.Message + e.StackTrace);
                return RespUtil.InternalError<string>(msg: e.Message);
            }
        }

        /// <summary>
        /// 顺呼、重呼（北大版本 直接调用callService 一切叫号相关数据以北大CallService服务为主）
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<AdmissionRecordDto>> TerminalCallAsync(TerminalCallDto input,
            CancellationToken cancellationToken)
        {
            OutQueueDto outQueueDto = input.Adapt<OutQueueDto>();
            switch (input.CallOperator)
            {
                case 0:
                    // 顺呼
                    return await _hisApi.TerminalCallingAsync(input, cancellationToken);
                case 1:
                    // 重呼 = 队首叫号 
                    return await _hisApi.TerminalReCallAsync(input, cancellationToken);
                case 2:
                    // 过号 = 移除队首患者
                    return await _hisApi.OutQueueAsync(outQueueDto, cancellationToken);
                case 3:
                    // 取消呼叫
                    return await this.CancelCallingAsync(input.DoctorId, input.ConsultingRoomCode, input.IpAddr,
                        cancellationToken);
                default:
                    return RespUtil.Error<AdmissionRecordDto>(msg: "CallOperator只能是0/1/2/3");
            }
        }

        /// <summary>
        /// 取消呼叫
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<ResponseResult<AdmissionRecordDto>> CancelCallingAsync(string doctorId,
            string consultingRoomCode, string ipAddr,
            CancellationToken cancellationToken)
        {
            try
            {
                await _callService.CancelCallAsync(new CallCancelDto
                { ConsultingRoomCode = consultingRoomCode, DoctorId = doctorId, IpAddr = ipAddr });
                return RespUtil.Ok<AdmissionRecordDto>();
            }
            catch (Exception ex)
            {
                return RespUtil.Ok<AdmissionRecordDto>(msg: ex.Message);
            }
        }

        /// <summary>
        /// 退回候诊
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> PatientWaitingAsync(int id,
            CancellationToken cancellationToken)
        {
            AdmissionRecord admissionRecord = await _freeSql.Select<AdmissionRecord>().Where(a => a.AR_ID == id)
                .FirstAsync(cancellationToken);
            if (admissionRecord == null)
            {
                return RespUtil.Error<string>(msg: "患者不存在！");
            }

            if (admissionRecord.VisitStatus != VisitStatus.正在就诊)
            {
                return RespUtil.Error<string>(msg: "患者不能退回就诊！");
            }

            if (await _freeSql.Select<DiagnoseRecord>()
                    .AnyAsync(a => a.IsDeleted == false && a.PI_ID == admissionRecord.PI_ID, cancellationToken))
            {
                return RespUtil.Error<string>(msg: "患者已开诊断，无法退回！");
            }

            try
            {
                admissionRecord.VisitStatus = VisitStatus.待就诊;
                admissionRecord.CallStatus = CallStatus.NotYet;
                admissionRecord.VisitDate = null;
                admissionRecord.OperatorCode = string.Empty;
                admissionRecord.OperatorName = string.Empty;
                admissionRecord.PatientStatus = PatientStatus.Default;
                admissionRecord.FirstDoctorCode = null;
                admissionRecord.FirstDoctorName = null;
                admissionRecord.CallingDoctorId = null;
                admissionRecord.CallingDoctorName = null;
                admissionRecord.FinishVisitTime = null;
                admissionRecord.OutDeptTime = null;
                admissionRecord.BedTime = null;
                admissionRecord.InDeptTime = null;
                admissionRecord.ExpireNumberTime = null;
                _freeSql.Update<AdmissionRecord>().SetSource(admissionRecord).ExecuteAffrows();

                await _callService.SendBackWaittingAsync(admissionRecord.RegisterNo);

                // 插入患者时间轴数据
                TimeAxisRecord timeAxisRecord = new TimeAxisRecord
                {
                    PI_ID = admissionRecord.PI_ID,
                    Time = DateTime.Now,
                    TimePointCode = TimePoint.ReWaitingVisit.ToString()
                }.SetTimePointName();

                _freeSql.Insert(timeAxisRecord).ExecuteAffrows();

                await _capPublisher.PublishAsync("patient.visitstatus.changed",
                    new
                    {
                        Id = admissionRecord.PI_ID,
                        VisitStatus = VisitStatus.待就诊
                    },
                    cancellationToken: cancellationToken);

                _freeSql.Delete<TransferRecord>()
                    .Where(x => x.TransferTypeCode == TransferType.InDept && x.PI_ID == admissionRecord.PI_ID)
                    .ExecuteAffrows();

                return RespUtil.Ok<string>();
            }
            catch (Exception e)
            {
                return RespUtil.InternalError<string>(msg: e.Message);
            }
        }

        /// <summary>
        /// 患者待结果
        /// </summary>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> PatientWaitResult(Guid pI_ID)
        {
            var model = await _freeSql.Select<AdmissionRecord>().Where(a => a.PI_ID == pI_ID).FirstAsync();
            if (model == null)
            {
                return RespUtil.Error<string>(msg: "患者不存在！");
            }

            if (model.VisitStatus != VisitStatus.正在就诊)
            {
                return RespUtil.Error<string>(msg: "非正在就诊患者不能处理为待结果！");
            }

            int result = _freeSql.Update<AdmissionRecord>()
                .Set(s => s.PatientStatus, PatientStatus.WaitResult)
                .Where(x => x.PI_ID == pI_ID)
                .Where(x => x.VisitStatus == VisitStatus.正在就诊)
                .ExecuteAffrows();

            if (result <= 0)
            {
                _log.LogError("Patient Waiting Error.Msg:数据库保存数据失败");
                throw new Exception(message: "患者退回失败！原因：保存数据失败！");
            }

            //患者结束就诊同时调用call Service 结束就诊
            await _callService.TreatFinishAsync(model.RegisterNo);

            _ = _capPublisher.PublishAsync("sync.visitstatus.from.patientservice",
                new { Id = model.PI_ID, VisitStatus = VisitStatus.已就诊 });
            return RespUtil.Ok<string>(msg: "患者待结果中");
        }

        /// <summary>
        /// 手动操作留观和抢救区待入结束就诊
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <returns></returns>
        //public async Task<ResponseResult<string>> OperatorEndVisitAsync(List<int> aR_IDs)
        //{
        //    if (aR_IDs == null || !aR_IDs.Any())
        //    {
        //        return RespUtil.Ok<string>(msg: "患者结束就诊成功！");
        //    }
        //    var now = DateTime.Now;
        //    var result = _freeSql.Update<AdmissionRecord>()
        //                        .Set(s => s.VisitStatus, VisitStatus.已就诊)
        //                        .Set(s => s.FinishVisitTime, now)
        //                        .Set(s => s.LastDirectionName, "主动操作结束就诊")
        //                        .Set(s => s.KeyDiseasesCode, "KeyDiseases_010")
        //                        .Set(s => s.KeyDiseasesName, "非重点病种")
        //                        .Set(s => s.OutDeptReasonCode, "KeyDiseasesByPatient_016")
        //                        .Set(s => s.OutDeptReasonName, "正常出科")
        //                        .Where(w => aR_IDs.Contains(w.AR_ID))
        //                        .ExecuteAffrows();

        //    var admissionRecords = await _freeSql.Select<AdmissionRecord>()
        //                                        .Where(w => aR_IDs.Contains(w.AR_ID))
        //                                        .ToListAsync<AdmissionRecord>();

        //    foreach (var admissionRecord in admissionRecords)
        //    {
        //        var transferModel = new CreateTransferRecordDro
        //        {
        //            PI_ID = admissionRecord.PI_ID,
        //            PatientID = admissionRecord.PatientID,
        //            VisitNo = admissionRecord.VisitNo,
        //            ToDeptCode = admissionRecord.TriageDeptCode,
        //            ToDept = admissionRecord.TriageDeptName,
        //            ToArea = TransferType.EndVisit.GetDescription(),
        //            ToAreaCode = TransferType.EndVisit.ToString(),
        //            FromDeptCode = admissionRecord.TriageDeptCode,
        //            FromAreaCode = admissionRecord.AreaCode,
        //            TransferTypeCode = TransferType.EndVisit,
        //            TransferReason = "主动操作结束就诊",
        //            OperatorName = CurrentUser.FindClaimValue("fullName")
        //        };
        //        //添加流转记录
        //        await _transferRecordAppService.CreateTransferRecordAsync(transferModel, default);
        //    }

        //    return RespUtil.Ok<string>(msg: "患者结束就诊成功！");
        //}

        /// <summary>
        /// 手动操作留观和抢救区退回（过号）
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> OperatorPassedNumerAsync(List<int> aR_IDs)
        {
            if (aR_IDs == null || !aR_IDs.Any())
            {
                return RespUtil.Ok<string>(msg: "患者退回成功！");
            }

            // 过号操作，更新当前科室叫号中的患者信息为已过号
            var result = _freeSql.Update<AdmissionRecord>()
                .Set(a => a.CallStatus, CallStatus.Exceed)
                .Set(a => a.VisitStatus, VisitStatus.过号)
                .Set(a => a.CallConsultingRoomCode, null)
                .Set(a => a.CallConsultingRoomName, null)
                .Set(a => a.ExpireNumberTime, DateTime.Now)
                .Where(a => aR_IDs.Contains(a.AR_ID))
                .ExecuteAffrows();

            var admissionRecords = await _freeSql.Select<AdmissionRecord>()
                .Where(w => aR_IDs.Contains(w.AR_ID))
                .ToListAsync<AdmissionRecord>();
            foreach (var item in admissionRecords)
            {
                // 插入患者时间轴数据
                var timeAxisRecord = new TimeAxisRecord
                {
                    PI_ID = item.PI_ID,
                    Time = DateTime.Now,
                    TimePointCode = TimePoint.ExpireTime.ToString()
                }.SetTimePointName();
                _freeSql.Insert(timeAxisRecord).ExecuteAffrows();
            }

            return RespUtil.Ok<string>(msg: "患者退回成功！");
        }

        /// <summary>
        /// 结束就诊
        /// </summary>
        /// <param name="aR_ID">入科记录表id</param>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> EndVisitAsync(int aR_ID, EndVisitDto dto,
            CancellationToken cancellationToken = default)
        {
            string operatorCode = CurrentUser.UserName;
            string operatorName = CurrentUser.FindClaimValue("fullName");
            try
            {
                AdmissionRecord admissionRecord = await _freeSql.Select<AdmissionRecord>().Where(x => x.AR_ID == aR_ID)
                    .FirstAsync(cancellationToken);

                if (admissionRecord == null)
                {
                    return RespUtil.Error<string>(msg: "结束就诊失败！原因：该患者无诊疗记录！");
                }

                admissionRecord.VisitStatus = VisitStatus.已就诊;
                admissionRecord.FinishVisitTime = DateTime.Now;
                admissionRecord.LastDirectionCode = dto.LastDirectionCode;
                admissionRecord.LastDirectionName = dto.LastDirectionName;
                admissionRecord.KeyDiseasesCode = dto.KeyDiseasesCode;
                admissionRecord.KeyDiseasesName = dto.KeyDiseasesName;
                admissionRecord.RecallTime = null;
                _freeSql.Update<AdmissionRecord>().SetSource(admissionRecord).ExecuteAffrows();

                TransferRecord transferRecord = new TransferRecord()
                {
                    PI_ID = admissionRecord.PI_ID,
                    PatientID = admissionRecord.PatientID,
                    VisitNo = admissionRecord.VisitNo,
                    TransferTime = DateTime.Now,
                    OperatorCode = operatorCode,
                    OperatorName = operatorName,
                    TransferTypeCode = TransferType.EndVisit,
                    TransferType = TransferType.EndVisit.GetDescription(),
                    FromAreaCode = admissionRecord.AreaCode,
                    ToAreaCode = TransferType.EndVisit.ToString(),
                    ToArea = TransferType.EndVisit.GetDescription(),
                    FromDeptCode = admissionRecord.DeptCode,
                    ToDeptCode = TransferType.EndVisit.ToString(),
                    ToDept = "结束就诊",
                    TransferReasonCode = dto.LastDirectionCode,
                    TransferReason = dto.LastDirectionName,
                };

                _freeSql.Insert(transferRecord).ExecuteAffrows();

                TimeAxisRecord timeAxisRecord = new TimeAxisRecord()
                {
                    PI_ID = admissionRecord.PI_ID,
                    TimePointCode = TimePoint.EndVisitTime.ToString(),
                    Time = DateTime.Now,
                }.SetTimePointName();
                _freeSql.Insert(timeAxisRecord).ExecuteAffrows();

                DiagnoseRecord diagnoseRecord = await _freeSql.Select<DiagnoseRecord>().Where(x =>
                    x.DiagnoseClassCode == DiagnoseClass.开立
                    && x.PI_ID == admissionRecord.PI_ID && !x.IsDeleted).FirstAsync();

                //保存就诊记录
                await _hisApi.SaveVisitRecordAsync(admissionRecord, diagnoseRecord, TransferType.OutDept, operatorCode,
                    operatorName);

                await _capPublisher.PublishAsync("patient.visitstatus.changed",
                    new
                    {
                        Id = admissionRecord.PI_ID,
                        VisitStatus = VisitStatus.已就诊,
                        dto.LastDirectionCode,
                        dto.LastDirectionName,
                        FinishVisitTime = DateTime.Now,
                        admissionRecord.FirstDoctorCode,
                        admissionRecord.FirstDoctorName,
                    },
                    cancellationToken: cancellationToken);

                //患者结束就诊同时调用call Service 结束就诊
                await _callService.TreatFinishAsync(admissionRecord.RegisterNo);

                return RespUtil.Ok<string>(msg: "患者结束就诊成功！");
            }
            catch (Exception e)
            {
                _log.LogError("结束就诊失败！原因:{Msg}", e);
                return RespUtil.InternalError<string>(msg: "结束就诊失败！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 更新患者基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> UpdatePatientInfoAsync(UpdatePatientInfoDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (Convert.ToBoolean(_configuration["Settings:IsCheckIdentityNo"]) &&
                    !string.IsNullOrWhiteSpace(dto.IDNo))
                {
                    var idCardInfo = dto.IDNo.Verify();
                    if (!idCardInfo.IsVerifyPass)
                    {
                        return RespUtil.Error<string>(msg: "修改患者信息失败！请输入合法的身份证号码");
                    }
                }

                AdmissionRecord admissionRecord =
                    await _freeSql.Select<AdmissionRecord>().Where(x => x.AR_ID == dto.AR_ID).FirstAsync();
                if (admissionRecord == null)
                {
                    return RespUtil.Error<string>(msg: "修改患者信息失败！未查询到患者信息");
                }

                try
                {
                    int rows = _freeSql.Update<AdmissionRecord>()
                        .WhereIf(dto.AR_ID > 0, x => x.AR_ID == dto.AR_ID)
                        .WhereIf(dto.PI_ID != Guid.Empty, x => x.PI_ID == dto.PI_ID)
                        .Set(x => x.PatientName, dto.PatientName)
                        .Set(x => x.HomeAddress, dto.HomeAddress)
                        .Set(x => x.Sex, dto.Sex)
                        .Set(x => x.SexName == dto.SexName)
                        .Set(x => x.IDNo, dto.IDNo)
                        .Set(x => x.Birthday, dto.Birthday)
                        .Set(x => x.FluFlag, dto.FluFlag)
                        .Set(x => x.TriageErrorFlag, dto.TriageErrorFlag)
                        .Set(x => x.CoughFlag, dto.CoughFlag)
                        .Set(x => x.ChestFlag, dto.ChestFlag)
                        .Set(x => x.TypeOfVisitCode, dto.TypeOfVisitCode)
                        .Set(x => x.TypeOfVisitName, dto.TypeOfVisitName)
                        .Set(x => x.PastMedicalHistory, dto.PastMedicalHistory)
                        .Set(x => x.AllergyHistory, dto.AllergyHistory)
                        .Set(x => x.Weight, dto.Weight)
                        .Set(x => x.Age, dto.Age)
                        .Set(x => x.ContactsPerson, dto.ContactsPerson)
                        .Set(x => x.ContactsPhone, dto.ContactsPhone)
                        .Set(x => x.NarrationCode, dto.NarrationCode)
                        .Set(x => x.NarrationName, dto.NarrationName)
                        .Set(x => x.DutyDoctorCode, dto.DutyDoctorCode)
                        .Set(x => x.DutyDoctorName, dto.DutyDoctorName)
                        .Set(x => x.DutyNurseCode, dto.DutyNurseCode)
                        .Set(x => x.DutyNurseName, dto.DutyNurseName)
                        .Set(x => x.RFID, dto.RFID)
                        .Set(x => x.GuardianIdTypeCode, dto.GuardianIdTypeCode)
                        .Set(x => x.GuardianIdTypeName, dto.GuardianIdTypeName)
                        .Set(x => x.ToHospitalWayCode, dto.ToHospitalWayCode)
                        .Set(x => x.ToHospitalWayName, dto.ToHospitalWayName)
                        .Set(x => x.CrowdCode, dto.CrowdCode)
                        .Set(x => x.CrowdName, dto.CrowdName)
                        .Set(x => x.VisitReasonCode, dto.VisitReasonCode)
                        .Set(x => x.VisitReasonName, dto.VisitReasonName)
                        .Set(x => x.GuardianIdCardNo, dto.GuardianIdCardNo)
                        .Set(x => x.GuardianPhone, dto.GuardianPhone)
                        .Set(x => x.ModifyLevelCode, dto.ModifyLevelCode)
                        .Set(x => x.ModifyLevelName, dto.ModifyLevelName)
                        .Set(x => x.GreenRoadCode, dto.GreenRoadCode)
                        .Set(x => x.GreenRoadName, dto.GreenRoadName)
                        .Set(x => x.IsOpenGreenChannl, dto.IsOpenGreenChannl)
                        .Set(x => x.KeyDiseasesCode, dto.KeyDiseasesCode)
                        .Set(x => x.KeyDiseasesName, dto.KeyDiseasesName)
                        .ExecuteAffrows();

                    //绿色通道标识有变更同步His
                    if (admissionRecord.GreenRoadCode != dto.GreenRoadCode)
                    {
                        bool isGreenChannl = true;
                        if (string.IsNullOrEmpty(dto.GreenRoadCode))
                        {
                            isGreenChannl = false;
                        }

                        await SyncGreedIntoToHis(admissionRecord, isGreenChannl);
                    }
                }
                catch (Exception e)
                {
                    _log.LogError("Message:{0}", e);
                    return RespUtil.Error<string>(msg: e.Message);
                }

                // 推送队列消息通知分诊修改患者信息
                UpdatePatientInfoMqDto mqDto = dto.BuildAdapter().AdaptToType<UpdatePatientInfoMqDto>();
                await _capPublisher.PublishAsync("modify.patient.info.from.patient.service", mqDto,
                    cancellationToken: cancellationToken);

                AdmissionRecord newAdmissionRecord =
                    await _freeSql.Select<AdmissionRecord>().Where(x => x.AR_ID == dto.AR_ID).FirstAsync();
                await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.UpdatePatient, newAdmissionRecord);

                return RespUtil.Ok<string>(msg: "修改患者信息成功");
            }
            catch (Exception e)
            {
                _log.LogError("AdmissionRecordAppService Update PatientInfo Error.Msg:{Msg}", e);
                return RespUtil.InternalError<string>(msg: "修改患者信息失败！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 同步绿色通道信息到His
        /// </summary>
        /// <param name="patientId">患者唯一标识(HIS)</param>
        /// <param name="visSerialNo">就诊流水号</param>
        /// <param name="isGreenChannl">是否开启绿色通道</param>
        /// <exception cref="Exception"></exception>
        private async Task SyncGreedIntoToHis(AdmissionRecord admissionRecord, bool isGreenChannl)
        {
            //his保存绿色通道
            await _hisApi.EmergencyGreenChannel(admissionRecord, isGreenChannl);
        }

        /// <summary>
        /// 患者开始接诊、或入科
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<string>> PatientStartVisitAsync(PatientStartVisitDto dto,
            CancellationToken cancellationToken = default)
        {
            string doctorCode = CurrentUser.UserName;
            string doctorName = CurrentUser.FindClaimValue("fullName");

            if (dto.AR_ID <= 0 && dto.PI_ID == Guid.Empty)
            {
                return RespUtil.Ok<string>(msg: "自增Id与患者分诊Id不能同时为空");
            }

            AdmissionRecord admissionRecord = await _freeSql.Select<AdmissionRecord>()
                .WhereIf(dto.AR_ID > 0, x => x.AR_ID == dto.AR_ID)
                .WhereIf(dto.PI_ID != Guid.Empty, x => x.PI_ID == dto.PI_ID)
                .FirstAsync();

            if (string.IsNullOrEmpty(admissionRecord.RegisterNo))
            {
                return RespUtil.Ok<string>(msg: "患者接诊失败！原因：患者未挂号！请先挂号！");
            }

            //就诊区患者入科不需要判断床位
            if (admissionRecord.AreaCode != Area.OutpatientArea.ToString() && string.IsNullOrEmpty(dto.Bed))
            {
                // 抢救区、留观区患者入科需选择床位
                return RespUtil.Ok<string>(msg: "患者接诊失败！原因：患者入科床位不能为空！请先选择床位！");
            }
            else
            {
                if (admissionRecord.VisitStatus != VisitStatus.正在就诊)
                {
                    if (!string.IsNullOrWhiteSpace(admissionRecord.TriageDoctorCode) &&
                        doctorCode != admissionRecord.TriageDoctorCode)
                    {
                        return RespUtil.Error<string>(msg: $"该患者已被指定为{admissionRecord.TriageDoctorName}医生",
                            data: "false");
                    }
                }
            }

            // 待就诊才能接诊
            if (admissionRecord.AreaCode == Area.OutpatientArea.ToString() &&
                admissionRecord.VisitStatus != VisitStatus.待就诊)
            {
                return RespUtil.Ok<string>(msg: $"患者接诊失败！原因：患者状态不是{VisitStatus.待就诊.ToString()}");
            }

            // 不在主页的患者要接诊叫号状态要为叫号中，主页点击开始接诊不需要为叫号中
            if (admissionRecord.AreaCode == Area.OutpatientArea.ToString() && !dto.IsDetailsPage &&
                admissionRecord.CallStatus != CallStatus.Calling)
            {
                return RespUtil.Ok<string>(msg: $"患者接诊失败！原因：患者状态不是{CallStatus.Calling.GetDescription()}");
            }

            // 待就诊或正在就诊的患者可以入科
            if ((admissionRecord.AreaCode == Area.ObservationArea.ToString() ||
                 admissionRecord.AreaCode == Area.RescueArea.ToString()) &&
                admissionRecord.VisitStatus != VisitStatus.正在就诊 && admissionRecord.VisitStatus != VisitStatus.待就诊)
            {
                return RespUtil.Ok<string>(
                    msg: $"患者入科失败！患者状态不是{VisitStatus.待就诊.ToString()}或{VisitStatus.正在就诊.ToString()}");
            }

            try
            {
                bool first = false;
                if (admissionRecord.VisitStatus == VisitStatus.待就诊)
                {
                    first = true;
                }

                // 设置接诊时间为当前时间
                DateTime visitDate = admissionRecord.VisitDate ?? DateTime.Now;
                string retentionTime = string.Empty;
                double surplusTime = visitDate.Subtract(admissionRecord.TriageTime).TotalMinutes;
                if (surplusTime <= 0)
                {
                    surplusTime = visitDate.Subtract(admissionRecord.TriageTime).TotalSeconds;
                    retentionTime = (int)surplusTime + "秒";
                }
                else
                {
                    if (surplusTime > 60)
                    {
                        retentionTime = (int)(surplusTime / 60) + "时" + (int)(surplusTime % 60) + "分";
                    }
                    else
                    {
                        retentionTime = ((int)surplusTime) + "分";
                    }
                }

                string bedHeadSticker = dto.BedHeadStickerList.Count > 0
                    ? string.Join(',', dto.BedHeadStickerList)
                    : string.Empty;

                admissionRecord.VisitStatus = VisitStatus.正在就诊;
                admissionRecord.VisitDate = visitDate;
                admissionRecord.BedHeadSticker = bedHeadSticker;
                admissionRecord.CallStatus = CallStatus.Over;
                if (!string.IsNullOrEmpty(dto.Bed))
                {
                    admissionRecord.BedTime = DateTime.Now;
                    admissionRecord.Bed = dto.Bed;
                    admissionRecord.InDeptTime = dto.InDeptTime;
                }

                admissionRecord.DutyDoctorCode = dto.DutyDoctorCode;
                admissionRecord.DutyDoctorName = dto.DutyDoctorName;
                admissionRecord.DutyNurseCode = dto.DutyNurseCode;
                admissionRecord.DutyNurseName = dto.DutyNurseName;
                admissionRecord.Pflegestufe = dto.Pflegestufe;
                //就诊区接诊不需要保存科室
                if (!dto.DeptCode.IsNullOrEmpty())
                {
                    admissionRecord.DeptCode = dto.DeptCode;
                    admissionRecord.DeptName = dto.DeptName;
                }

                admissionRecord.RetentionTime = retentionTime;
                if (admissionRecord.AreaCode == Area.OutpatientArea.ToString())
                {
                    admissionRecord.OperatorCode = doctorCode;
                    admissionRecord.OperatorName = doctorName;
                    admissionRecord.FirstDoctorCode = doctorCode;
                    admissionRecord.FirstDoctorName = doctorName;
                }

                admissionRecord.ToHospitalWayCode = dto.ToHospitalWayCode;
                admissionRecord.ToHospitalWayName = dto.ToHospitalWayName;
                admissionRecord.InDeptWay = dto.ToHospitalWayName;

                await _freeSql.Update<AdmissionRecord>()
                    .SetSource(admissionRecord)
                    .ExecuteAffrowsAsync(cancellationToken);

                // 未叫号患者开始接诊后发布队列消息到叫号服务
                await _capPublisher.PublishAsync("patient.visitstatus.changed",
                    new
                    {
                        Id = dto.PI_ID,
                        VisitStatus = VisitStatus.正在就诊,
                        FirstDoctorCode = doctorCode,
                        FirstDoctorName = doctorName,
                        VisitDate = visitDate
                    }, cancellationToken: cancellationToken);

                // 待就诊未叫号状态患者从详情页点击开始就诊需同步状态到叫号服务
                if (dto.IsDetailsPage && admissionRecord.AreaCode == Area.OutpatientArea.ToString())
                {
                    //调用结束叫号接口 开始接诊接口
                    await _callService.TreatFinishAsync(admissionRecord.RegisterNo);
                }

                //添加入科时间节点
                if (admissionRecord.AreaCode != Area.OutpatientArea.ToString() && admissionRecord.InDeptTime != null)
                {
                    await _freeSql.Insert(new TimeAxisRecord()
                    {
                        PI_ID = admissionRecord.PI_ID,
                        Time = DateTime.Now,
                        TimePointCode = TimePoint.InDeptTime.ToString(),
                    }.SetTimePointName()).ExecuteAffrowsAsync(cancellationToken);

                    //添加流转记录
                    TransferType transferType = admissionRecord.AreaCode == Area.ObservationArea.ToString()
                        ? TransferType.ObservationArea
                        : TransferType.RescueArea;
                    TransferRecord transferRecord = new TransferRecord()
                    {
                        PI_ID = admissionRecord.PI_ID,
                        PatientID = admissionRecord.PatientID,
                        VisitNo = admissionRecord.VisitNo,
                        TransferTime = DateTime.Now,
                        OperatorCode = doctorCode,
                        OperatorName = doctorName,
                        TransferTypeCode = transferType,
                        TransferType = transferType.GetDescription(),
                        FromAreaCode = admissionRecord.AreaCode,
                        ToAreaCode = admissionRecord.AreaCode,
                        ToArea = admissionRecord.AreaCode == Area.ObservationArea.ToString()
                            ? Area.ObservationArea.GetDescription()
                            : Area.RescueArea.GetDescription(),
                        FromDeptCode = admissionRecord.DeptCode,
                        ToDeptCode = admissionRecord.DeptCode,
                        ToDept = admissionRecord.DeptName,
                        TransferReasonCode = admissionRecord.AreaCode,
                        TransferReason = "入科",
                    };
                    _freeSql.Insert(transferRecord).ExecuteAffrows();

                    //同步生命体征到护理单
                    VitalSignInfoMqEto vital = await _freeSql.Select<VitalSignInfo>()
                        .Where(x => x.PI_ID == admissionRecord.PI_ID)
                        .FirstAsync<VitalSignInfoMqEto>(cancellationToken);
                    if (vital != null)
                    {
                        vital.OperationCode = doctorCode;
                        vital.OperationName = doctorName;
                        vital.Signature = dto.Signature;
                        await _capPublisher.PublishAsync("sync.report.vitalsign.info", vital, cancellationToken: cancellationToken);
                    }
                }

                if (admissionRecord.AreaCode == Area.OutpatientArea.ToString())
                {
                    await _freeSql.Insert(new TimeAxisRecord()
                    {
                        PI_ID = dto.PI_ID,
                        Time = DateTime.Now,
                        TimePointCode = TimePoint.StartVisitTime.ToString(),
                    }.SetTimePointName()).ExecuteAffrowsAsync(cancellationToken);

                    TransferRecord transferRecord = new TransferRecord()
                    {
                        PI_ID = admissionRecord.PI_ID,
                        PatientID = admissionRecord.PatientID,
                        VisitNo = admissionRecord.VisitNo,
                        TransferTime = DateTime.Now,
                        OperatorCode = doctorCode,
                        OperatorName = doctorName,
                        TransferTypeCode = TransferType.InDept,
                        TransferType = TransferType.InDept.GetDescription(),
                        FromAreaCode = "Triage",
                        ToAreaCode = Area.OutpatientArea.ToString(),
                        ToArea = Area.OutpatientArea.GetDescription(),
                        FromDeptCode = admissionRecord.TriageDeptCode,
                        ToDeptCode = admissionRecord.TriageDeptCode,
                        ToDept = admissionRecord.TriageDeptName,
                        TransferReasonCode = "InDept",
                        TransferReason = "开始接诊",
                    };

                    _freeSql.Insert(transferRecord).ExecuteAffrows();
                }

                if (admissionRecord.AreaCode == Area.RescueArea.ToString())
                {
                    RescueRecord rescueRecord = new RescueRecord()
                    {
                        Id = Guid.NewGuid(),
                        PI_Id = admissionRecord.PI_ID,
                        TimePointName = "inrescue",
                        TimePoint = DateTime.Now,
                        OperateCode = doctorCode,
                        OperateName = doctorName,
                        Retention = 0
                    };
                    _freeSql.Insert(rescueRecord).ExecuteAffrows();
                }

                if (admissionRecord.AreaCode == Area.ObservationArea.ToString())
                {
                    RescueRecord rescueRecord = new RescueRecord()
                    {
                        Id = Guid.NewGuid(),
                        PI_Id = admissionRecord.PI_ID,
                        TimePointName = "inobservation",
                        TimePoint = DateTime.Now,
                        OperateCode = doctorCode,
                        OperateName = doctorName,
                        Retention = 0
                    };
                    _freeSql.Insert(rescueRecord).ExecuteAffrows();
                }

                if (first)
                {
                    await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.ToHospital, admissionRecord);
                }

                await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.InDept, admissionRecord);

                return RespUtil.Ok<string>(msg: dto.Bed.IsNullOrWhiteSpace() ? "患者接诊成功！" : "患者入科成功！");
            }
            catch (Exception e)
            {
                _log.LogError("Patient Start Visit Error.Msg:{Msg}", e);
                return RespUtil.InternalError<string>(msg: (dto.Bed.IsNullOrWhiteSpace() ? "患者接诊失败！" : "患者入科失败！") +
                                                           "原因：" + e.Message);
            }
        }

        /// <summary>
        /// 召回就诊
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<AdmissionRecordDto>> ReCallVisitAsync(ReCallVisitDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                ResponseResult<AdmissionRecordDto> responseResult =
                    await GetAdmissionRecordByIdAsync(0, dto.PI_ID, cancellationToken);
                if (responseResult.Code != HttpStatusCodeEnum.Ok)
                {
                    return RespUtil.Error<AdmissionRecordDto>(msg: "查询患者信息失败");
                }

                AdmissionRecordDto admissionRecordDto = responseResult.Data;
                if (admissionRecordDto == null)
                {
                    return RespUtil.Error<AdmissionRecordDto>(msg: "查询患者信息失败");
                }

                // 过号召回、诊毕召回分开流程处理
                if (admissionRecordDto.VisitStatus == VisitStatus.过号)
                {
                    if (admissionRecordDto.IsReferral)
                    {
                        return RespUtil.Error<AdmissionRecordDto>(msg: "该病人已经转诊， 无法召回");
                    }

                    // 过号召回（返回候诊），召回后患者状态为待就诊、返回叫号队列，叫号状态为未叫号
                    //await _callService.ReturnToQueueAsync(admissionRecordDto.RegisterNo);     现场要求召回的患者不在需要叫号
                    return await ReQueueAsync(dto, admissionRecordDto, cancellationToken);
                }

                if (admissionRecordDto.VisitStatus == VisitStatus.出科 ||
                    admissionRecordDto.VisitStatus == VisitStatus.已就诊)
                {
                    // 诊毕召回，召回后患者状态为就诊中，不返回叫号队列，叫号状态不改变（已叫号）
                    //await _callService.SendBackWaittingAsync(admissionRecordDto.RegisterNo);       现场要求召回的患者不在需要叫号
                    return await ReVisitAsync(dto, admissionRecordDto, cancellationToken);
                }

                _log.LogError("ReCall Visit Error.Msg:{Msg} PI_ID:{ID}", $"患者状态不是出科或已就诊或过号", dto.PI_ID);
                return RespUtil.Error<AdmissionRecordDto>(msg: $"患者状态不是出科或已就诊或过号");
            }
            catch (Exception e)
            {
                _log.LogError("ReCall Visit Error.Msg:{Msg}", e);
                return RespUtil.InternalError<AdmissionRecordDto>(msg: e.Message);
            }
        }

        /// <summary>
        /// 诊毕召回
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="admissionRecordDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<ResponseResult<AdmissionRecordDto>> ReVisitAsync(ReCallVisitDto dto,
            AdmissionRecordDto admissionRecordDto, CancellationToken cancellationToken)
        {
            int.TryParse(_configuration["ReCallHours"], out int reCallHours);
            if (admissionRecordDto.VisitStatus == VisitStatus.出科)
            {
                if (string.IsNullOrEmpty(dto.Bed))
                {
                    return RespUtil.Error<AdmissionRecordDto>(msg: "请选择床位");
                }

                OutDeptRecord outDeptRecord = await _freeSql.Select<OutDeptRecord>().Where(x => x.PI_Id == admissionRecordDto.PI_ID).OrderBy(x => x.CreateTime).FirstAsync();

                if (outDeptRecord == null)
                {
                    return RespUtil.Error<AdmissionRecordDto>(msg: "患者没有出科时间");
                }

                if (outDeptRecord.OutDeptTime.AddHours(reCallHours) < DateTime.Now)
                {
                    return RespUtil.Error<AdmissionRecordDto>(msg: $"患者第一次出科时间距当前时间间隔大于{reCallHours}小时");
                }
            }

            if (admissionRecordDto.VisitStatus == VisitStatus.已就诊)
            {
                if (admissionRecordDto.RegisterTime.Value.AddHours(reCallHours) < DateTime.Now)
                {
                    return RespUtil.Error<AdmissionRecordDto>(msg: $"患者挂号时间距当前时间间隔大于{reCallHours}小时");
                }
            }

            // 对于已经结束就诊的患者，召回后状态为“就诊中”； by: ywlin 20211214
            var nextStatus = VisitStatus.正在就诊;
            _freeSql.Transaction(() =>
            {
                var rows = _freeSql.Update<AdmissionRecord>()
                    .Set(a => a.VisitStatus, nextStatus)
                    .Set(a => a.PatientStatus, PatientStatus.Default)
                    .Set(a => a.ExpireNumberTime, null)
                    .Set(a => a.FinishVisitTime, null)
                    .Set(a => a.OutDeptTime, null)
                    .Set(a => a.KeyDiseasesCode, null)
                    .Set(a => a.KeyDiseasesName, null)
                    .Set(a => a.OutDeptCode, null)
                    .Set(a => a.OutDeptName, null)
                    .Set(a => a.DeathTime, null)
                    .Set(a => a.OutDeptReasonCode, null)
                    .Set(a => a.OutDeptReasonName, null)
                    .Set(a => a.LastDirectionCode, null)
                    .Set(a => a.LastDirectionName, null)
                    .Set(a => a.SupplementaryNotes, null)
                    .Set(a => a.Bed, dto.Bed)
                    .Set(a => a.NotAutoEnd, true)
                    .Set(a => a.RecallTime, DateTime.Now)
                    .Where(x => x.PI_ID == dto.PI_ID)
                    .ExecuteAffrows();

                if (rows > 0)
                {
                    admissionRecordDto.VisitStatus = nextStatus;
                    // 插入患者时间轴数据
                    var timeAxisRecord = new TimeAxisRecord
                    {
                        PI_ID = admissionRecordDto.PI_ID,
                        Time = DateTime.Now,
                        TimePointCode = TimePoint.ReCallVisit.ToString()
                    }.SetTimePointName();
                    _freeSql.Insert(timeAxisRecord).ExecuteAffrows();

                    if (admissionRecordDto.AreaCode == Area.RescueArea.ToString())
                    {
                        string operateCode = CurrentUser.UserName;
                        string operateName = CurrentUser.FindClaimValue("fullName");
                        RescueRecord rescueRecord = new RescueRecord()
                        {
                            Id = Guid.NewGuid(),
                            PI_Id = admissionRecordDto.PI_ID,
                            TimePointName = "inrescue",
                            TimePoint = DateTime.Now,
                            OperateCode = operateCode,
                            OperateName = operateName,
                            Retention = 0
                        };
                        _freeSql.Insert(rescueRecord).ExecuteAffrows();
                    }

                    if (admissionRecordDto.AreaCode == Area.ObservationArea.ToString())
                    {
                        string operateCode = CurrentUser.UserName;
                        string operateName = CurrentUser.FindClaimValue("fullName");
                        RescueRecord rescueRecord = new RescueRecord()
                        {
                            Id = Guid.NewGuid(),
                            PI_Id = admissionRecordDto.PI_ID,
                            TimePointName = "inobservation",
                            TimePoint = DateTime.Now,
                            OperateCode = operateCode,
                            OperateName = operateName,
                            Retention = 0
                        };
                        _freeSql.Insert(rescueRecord).ExecuteAffrows();
                    }

                    _capPublisher.PublishAsync("patient.visitstatus.changed",
                            new { Id = dto.PI_ID, VisitStatus = nextStatus }, cancellationToken: cancellationToken)
                        .GetAwaiter();
                    // 病患列表变化消息
                    _capPublisher.PublishAsync("im.patient.queue.changed", new object(),
                        cancellationToken: cancellationToken).GetAwaiter();
                }
            });


            AdmissionRecord admissionRecord =
                await _freeSql.Select<AdmissionRecord>().Where(x => x.PI_ID == dto.PI_ID).FirstAsync();
            await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.ToHospital, admissionRecord);
            await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.InDept, admissionRecord);

            return RespUtil.Ok(msg: "患者召回成功！", data: admissionRecordDto);
        }

        /// <summary>
        /// 过号退回候诊
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="admissionRecordDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<ResponseResult<AdmissionRecordDto>> ReQueueAsync(ReCallVisitDto dto,
            AdmissionRecordDto admissionRecordDto, CancellationToken cancellationToken)
        {
            if (!admissionRecordDto.ExpireNumberTime.HasValue)
            {
                return RespUtil.Error<AdmissionRecordDto>(msg: $"没有过号时间");
            }

            DateTime expireNumberTime = admissionRecordDto.ExpireNumberTime.Value;
            // 只允许挂号时间在24小时内的过号患者退回候诊
            int reCallHours = 24;
            if (expireNumberTime.AddHours(reCallHours) < DateTime.Now)
            {
                return RespUtil.Error<AdmissionRecordDto>(msg: $"过号患者已超{reCallHours}小时，不能召回只能重新挂号");
            }

            // 对于过号的患者，召回后状态为“待就诊”  by: ywlin 20211214
            VisitStatus nextStatus = VisitStatus.待就诊;
            CallStatus nextCallStatus = CallStatus.NotYet;
            _freeSql.Transaction(() =>
            {
                var rows = _freeSql.Update<AdmissionRecord>().Set(a => a.VisitStatus, nextStatus)
                    .Set(a => a.PatientStatus, PatientStatus.Default)
                    .Set(a => a.CallStatus, nextCallStatus)
                    .Set(a => a.ExpireNumberTime, null)
                    .Set(a => a.OutDeptTime, null)
                    .Set(a => a.FinishVisitTime, null)
                    .Set(a => a.InDeptTime, null)
                    .Set(a => a.BedTime, null)
                    .Set(a => a.NotAutoEnd, true)
                    .Where(x => x.PI_ID == dto.PI_ID)
                    .ExecuteAffrows();

                if (rows > 0)
                {
                    admissionRecordDto.CallStatus = nextCallStatus;
                    admissionRecordDto.VisitStatus = nextStatus;
                    // 插入患者时间轴数据
                    var timeAxisRecord = new TimeAxisRecord
                    {
                        PI_ID = admissionRecordDto.PI_ID,
                        Time = DateTime.Now,
                        TimePointCode = TimePoint.ReCallVisit.ToString()
                    }.SetTimePointName();
                    _freeSql.Insert(timeAxisRecord).ExecuteAffrows();

                    _capPublisher.PublishAsync("patient.visitstatus.changed",
                            new { Id = dto.PI_ID, VisitStatus = nextStatus }, cancellationToken: cancellationToken)
                        .GetAwaiter();
                    // 病患列表变化消息
                    _capPublisher.PublishAsync("im.patient.queue.changed", new object(),
                        cancellationToken: cancellationToken).GetAwaiter();
                }
            });

            AdmissionRecord admissionRecord =
                await _freeSql.Select<AdmissionRecord>().Where(x => x.PI_ID == dto.PI_ID).FirstAsync();
            await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.ToHospital, admissionRecord);
            await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.InDept, admissionRecord);
            return RespUtil.Ok<AdmissionRecordDto>(msg: "患者召回成功！", data: admissionRecordDto);
        }

        /// <summary>
        /// 同步病患接口
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult<string>> SyncPatientAsync(TriagePatientInfosMqDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _log.LogInformation("接口同步病患开始");
                return await SyncPatientFromMqAsync(dto);
            }
            catch (Exception e)
            {
                _log.LogError("Sync Patient Error.Msg:{Msg}", e);
                return RespUtil.InternalError<string>(msg: "同步病患错误！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 同步病患排序接口
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult<string>> SyncPatientSortAsync(ReSortPatientListDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _log.LogInformation("接口同步病患排序开始");
                return await SyncPatientSortFromMqAsync(dto);
            }
            catch (Exception e)
            {
                _log.LogError("Sync Patient Sort Error.Msg:{Msg}", e);
                return RespUtil.InternalError<string>(msg: "同步病患排序错误！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 同步病患叫号状态
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult<string>> SyncPatientCallStatusAsync(SyncPatientCallStatusDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _log.LogInformation("接口同步病患叫号状态开始");
                return await SyncPatientCallStatusFromMqAsync(dto);
            }
            catch (Exception e)
            {
                _log.LogError("Sync Patient Call Status Error.Msg:{Msg}", e);
                return RespUtil.InternalError<string>(msg: "同步病患叫号状态错误！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 从队列中同步病患
        /// </summary>
        /// <param name="dtoMq"></param>
        [CapSubscribe("sync.patient.to.patientservice")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ResponseResult<string>> SyncPatientFromMqAsync(TriagePatientInfosMqDto dtoMq)
        {
            //如果是院前来的患者并且就诊状态为待分诊0则不消费，因为院前和预检分诊都发了一条消息过来
            _log.LogDebug($"sync.patient.to.patientservice获得患者-{dtoMq.PatientInfo.PatientName}-信息：{dtoMq.ToJson()}");
            if (dtoMq.PatientInfo.IsFirstAid && dtoMq.PatientInfo?.VisitStatus == 0)
            {
                _log.LogDebug(
                    $"从队列中同步病患失败！TriagePatientInfoId:{dtoMq.PatientInfo.TriagePatientInfoId},VisitStatus:{dtoMq.PatientInfo.VisitStatus}");
                return RespUtil.Ok<string>(
                    msg:
                    $"从队列中同步病患失败！TriagePatientInfoId:{dtoMq.PatientInfo.TriagePatientInfoId},VisitStatus:{dtoMq.PatientInfo.VisitStatus}");
            }

            try
            {
                PatientInfoMqDto dto = dtoMq.PatientInfo;
                if (dto.TriagePatientInfoId == Guid.Empty)
                {
                    throw new Exception("处理病患数据失败！分诊患者Id不能为空");
                }

                if (dto.ConsequenceInfo == null || dto.ConsequenceInfo.ActTriageLevel.IsNullOrWhiteSpace())
                {
                    throw new Exception($"处理队列病患数据失败，分诊级别不能不能为空。分诊Id：{dto.TriagePatientInfoId}");
                }

                AdmissionRecord admissionRecordDB = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(!string.IsNullOrWhiteSpace(dto.PatientId), x => x.PatientID == dto.PatientId)
                    .Where(x => x.PI_ID == dto.TriagePatientInfoId)
                    .FirstAsync();

                AdmissionRecord admissionRecordMQ = dto.BuildAdapter().AdaptToType<AdmissionRecord>();
                admissionRecordMQ.RegisterNo = dtoMq.RegisterInfo?.RegisterNo;
                admissionRecordMQ.RegisterTime = dtoMq.RegisterInfo?.RegisterTime;
                admissionRecordMQ.RegisterDoctorCode = dtoMq.RegisterInfo?.RegisterDoctorCode;
                admissionRecordMQ.RegisterDoctorName = dtoMq.RegisterInfo?.RegisterDoctorName;
                admissionRecordMQ.IsOpenDiagnosisCost = dtoMq.PatientInfo.IsSyncRegister; //是同步挂号患者，默认是已开诊查费
                admissionRecordMQ.TriageDirectionCode = dtoMq.ConsequenceInfo?.TriageTarget;
                admissionRecordMQ.TriageDirectionName = dtoMq.ConsequenceInfo?.TriageTargetName;
                admissionRecordMQ.TriageDoctorCode = dtoMq.ConsequenceInfo.DoctorCode;
                admissionRecordMQ.TriageDoctorName = dtoMq.ConsequenceInfo.DoctorName;
                admissionRecordMQ.OperatorName = dtoMq.PatientInfo.DoctorName;


                _log.LogDebug(
                    $"sync.patient.to.patientservice获得转换患者-{dtoMq.PatientInfo.PatientName}-AdmissionRecord：{admissionRecordMQ.ToJson()}");

                // 获取科室
                List<DepartmentModel> departments = await _grpcClient.GetDepartmentsAsync();
                var dept = departments.FirstOrDefault(x => x.RegisterCode == admissionRecordMQ.TriageDeptCode);
                // 门诊科室编码，TriageDeptCode 为挂号科室编码
                admissionRecordMQ.DeptCode = dept?.Code;
                admissionRecordMQ.DeptName = dept?.Name;

                ChooseAreaByTriageDirection(dtoMq.ConsequenceInfo.TriageTarget, admissionRecordMQ);

                #region 设置就诊状态

                switch (dto.VisitStatus)
                {
                    case TriageVisitStatus.NotTriageYet:
                        admissionRecordMQ.VisitStatus = VisitStatus.未挂号; //由于没有未分诊状态，所以未分诊的患者状态设置为未挂号
                        break;
                    case TriageVisitStatus.WattingTreat:
                        admissionRecordMQ.VisitStatus = VisitStatus.待就诊;
                        break;
                    case TriageVisitStatus.Treating:
                        admissionRecordMQ.VisitStatus = VisitStatus.正在就诊;
                        break;
                    case TriageVisitStatus.Treated:
                        // 抢救区和留观区的患者，分诊状态设置为正在就诊
                        if (admissionRecordDB.AreaCode == "ObservationArea" ||
                            admissionRecordDB.AreaCode == "RescueArea")
                            admissionRecordMQ.VisitStatus = VisitStatus.正在就诊;
                        else
                        {
                            admissionRecordMQ.VisitStatus = VisitStatus.已就诊;
                            admissionRecordMQ.FinishVisitTime = DateTime.Now;
                        }

                        break;
                    case TriageVisitStatus.Suspend:
                        admissionRecordMQ.VisitStatus = VisitStatus.待就诊;
                        break;
                    default:
                        admissionRecordMQ.VisitStatus = VisitStatus.待就诊;
                        break;
                }

                if (dtoMq.RegisterInfo?.IsCancelled ?? false)
                {
                    admissionRecordMQ.VisitStatus = VisitStatus.已退号;
                }

                #endregion


                if (admissionRecordDB == null)
                {
                    //只插入第一条数据
                    if (!(await _freeSql.Select<TransferRecord>().AnyAsync(x =>
                            x.PI_ID == admissionRecordMQ.PI_ID && x.TransferReason == "预检分诊")))
                    {
                        //添加流转记录
                        TransferRecord transferModel = new TransferRecord
                        {
                            PI_ID = admissionRecordMQ.PI_ID,
                            PatientID = admissionRecordMQ.PatientID,
                            VisitNo = admissionRecordMQ.VisitNo,
                            ToDeptCode = admissionRecordMQ.TriageDeptCode,
                            ToDept = admissionRecordMQ.TriageDeptName,
                            ToArea = admissionRecordMQ.TriageDeptName,
                            ToAreaCode = admissionRecordMQ.AreaCode,
                            FromDeptCode = "Triage",
                            FromAreaCode = "Triage",
                            TransferTypeCode = TransferType.NoInput,
                            TransferReasonCode = "Triage",
                            TransferReason = "预检分诊",
                            OperatorCode = admissionRecordMQ.TriageUserCode,
                            OperatorName = admissionRecordMQ.TriageUserName,
                            TransferTime = DateTime.Now
                        };
                        var row = _freeSql.Insert(transferModel).ExecuteAffrows();
                        if (row > 0)
                        {
                            _log.LogInformation("sync.patient.to.patientservice 添加流转记录成功！");
                        }
                    }

                    if (await _freeSql.Insert(admissionRecordMQ).ExecuteAffrowsAsync() > 0)
                    {
                        //绿通
                        if (!string.IsNullOrEmpty(admissionRecordMQ.GreenRoadCode))
                        {
                            AdmissionRecordDto record = await _freeSql.Select<AdmissionRecord>()
                                .Where(x => x.PI_ID == admissionRecordMQ.PI_ID).FirstAsync<AdmissionRecordDto>();
                            if (record != null)
                            {
                                await SaveGreenChannlByTriageAsync(record.AR_ID, admissionRecordMQ.GreenRoadName,
                                    admissionRecordMQ.GreenRoadCode, true);
                            }
                        }

                        //添加代办人信息,三无患者不需要
                        if (!admissionRecordMQ.IsNoThree)
                        {
                            await _freeSql.Insert<AgencyPeople>(new AgencyPeople(GuidGenerator.Create(),
                                    admissionRecordMQ.PI_ID, admissionRecordMQ.PatientName, admissionRecordMQ.IDNo,
                                    admissionRecordMQ.ContactsPhone, admissionRecordMQ.Sex, admissionRecordMQ.Age))
                                .ExecuteAffrowsAsync();
                        }

                        List<TimeAxisRecord> timePointList = new List<TimeAxisRecord>();

                        if (dtoMq.RegisterInfo != null && !dtoMq.RegisterInfo.RegisterNo.IsNullOrWhiteSpace() &&
                            dtoMq.RegisterInfo?.RegisterNo != "已退号")
                        {
                            timePointList.Add(new TimeAxisRecord()
                            {
                                PI_ID = dto.TriagePatientInfoId,
                                Time = dtoMq.RegisterInfo?.RegisterTime ?? DateTime.MinValue,
                                TimePointCode = TimePoint.RegisterTime.ToString(),
                            }.SetTimePointName());
                        }

                        timePointList.Add(new TimeAxisRecord()
                        {
                            PI_ID = dto.TriagePatientInfoId,
                            Time = dto.TriageTime.Value,
                            TimePointCode = TimePoint.TriageTime.ToString(),
                        }.SetTimePointName());

                        if (dtoMq.RegisterInfo?.RegisterNo == "已退号")
                        {
                            timePointList.Add(new TimeAxisRecord()
                            {
                                PI_ID = dto.TriagePatientInfoId,
                                Time = DateTime.Now,
                                TimePointCode = TimePoint.BackNumberTime.ToString(),
                            }.SetTimePointName());
                        }

                        if (await _freeSql.Insert(timePointList).ExecuteAffrowsAsync() > 0)
                        {
                            //生命体征和评分
                            if (dtoMq.VitalSignInfo != null)
                            {
                                VitalSignInfo vital = dtoMq.VitalSignInfo.To<VitalSignInfo>();
                                vital.PI_ID = dto.TriagePatientInfoId;
                                await _freeSql.Insert<VitalSignInfo>(vital).ExecuteAffrowsAsync();
                            }

                            if (dtoMq.ScoreInfo != null && dtoMq.ScoreInfo.Any())
                            {
                                List<ScoreInfo> scoreInfo = dtoMq.ScoreInfo.To<List<ScoreInfo>>();
                                List<ScoreInfo> scoreList = new List<ScoreInfo>();
                                scoreInfo.ForEach(f =>
                                {
                                    f.PI_ID = dto.TriagePatientInfoId;
                                    if (f.ScoreType != "JudgmentBd")
                                    {
                                        scoreList.Add(f);
                                    }
                                });

                                if (scoreList.Any())
                                {
                                    await _freeSql.Insert<ScoreInfo>(scoreList).ExecuteAffrowsAsync();
                                }
                            }
                        }
                    }
                }
                else
                {
                    var callingSn = dto.VisitStatus == TriageVisitStatus.NotTriageYet ? "" : dto.CallingSn;
                    int rows = await _freeSql.Update<AdmissionRecord>()
                        .Where(x => x.AR_ID == admissionRecordDB.AR_ID && x.PI_ID == admissionRecordDB.PI_ID)
                        //.SetIf(dto.VisitStatus == TriageVisitStatus.NotTriageYet, x => x.CallingSn, "")
                        .Set(x => x.PatientID, dto.PatientId)
                        .Set(x => x.PatientName, dto.PatientName)
                        .SetIf(dtoMq.RegisterInfo != null && !dtoMq.RegisterInfo.RegisterNo.IsNullOrWhiteSpace(),
                            x => x.RegisterNo,
                            dtoMq.RegisterInfo?.RegisterNo)
                        .Set(x => x.VisitNo, dto.VisitNo)
                        //.Set(x => x.VisitStatus, admissionRecordMQ.VisitStatus)
                        // 待定修改
                        .Set(x => x.RegisterTime, dtoMq.RegisterInfo?.RegisterTime)
                        .SetIf(
                            dtoMq.RegisterInfo != null && !dtoMq.RegisterInfo.RegisterDoctorCode.IsNullOrWhiteSpace(),
                            x => x.RegisterDoctorCode,
                            dtoMq.RegisterInfo?.RegisterDoctorCode)
                        .SetIf(
                            dtoMq.RegisterInfo != null && !dtoMq.RegisterInfo.RegisterDoctorName.IsNullOrWhiteSpace(),
                            x => x.RegisterDoctorName,
                            dtoMq.RegisterInfo?.RegisterDoctorName)
                        .Set(x => x.TriageDoctorCode, dtoMq.ConsequenceInfo.DoctorCode)
                        .Set(x => x.TriageDoctorName, dtoMq.ConsequenceInfo.DoctorName)
                        //   .SetIf(admissionRecordDB.VisitDate == null && dto.BeginTime != null, x => x.VisitDate, dto.BeginTime)
                        .Set(x => x.TriageLevel, dto.ConsequenceInfo?.ActTriageLevel)
                        .Set(x => x.TriageLevelName, dto.ConsequenceInfo?.ActTriageLevelName)
                        .Set(x => x.TriageDeptCode, dto.ConsequenceInfo?.TriageDept)
                        .Set(x => x.TriageDeptName, dto.ConsequenceInfo?.TriageDeptName)
                        .Set(x => x.Weight, dto.Weight)
                        .Set(x => x.TriageErrorFlag, dto.ConsequenceInfo?.ChangeLevel.IsNullOrWhiteSpace())
                        .Set(x => x.TriageTime, dto.TriageTime)
                        .Set(x => x.Sex, dto.Sex)
                        .Set(x => x.SexName, dto.SexName)
                        .Set(x => x.Birthday, dto.Birthday)
                        .Set(x => x.ContactsPerson, dto.ContactsPerson)
                        .Set(x => x.ContactsPhone, dto.ContactsPhone)
                        .Set(x => x.NarrationCode, dto.Narration)
                        .Set(x => x.NarrationName, dto.NarrationName)
                        .Set(x => x.IDNo, dto.IdentityNo)
                        .Set(x => x.Age, dto.Age)
                        .Set(x => x.CardNo, dto.CardNo)
                        .Set(x => x.HomeAddress, dto.Address)
                        .Set(x => x.ChargeType, dto.ChargeType)
                        .Set(x => x.ChargeTypeName, dto.ChargeTypeName)
                        .Set(x => x.RegType, dto.RegType)
                        .Set(x => x.SafetyNo, dto.SafetyNo)
                        .Set(x => x.KeyDiseasesCode, dto.DiseaseCode)
                        .Set(x => x.KeyDiseasesName, dto.DiseaseName)
                        .Set(x => x.GreenRoadName, dto.GreenRoadName)
                        .Set(x => x.GreenRoadCode, dto.GreenRoad)
                        .Set(x => x.ChestFlag,
                            dto.AdmissionInfo?.IsSoreThroatAndCough == "true")
                        .Set(x => x.CoughFlag,
                            dto.AdmissionInfo?.IsSoreThroatAndCough == "true")
                        .Set(x => x.FluFlag,
                            dto.AdmissionInfo?.IsAggregation == "true")
                        .Set(x => x.PastMedicalHistory, dto.AdmissionInfo?.PastMedicalHistory)
                        .Set(x => x.TriageUserCode, dto.TriageUserCode)
                        .Set(x => x.TriageUserName, dto.TriageUserName)
                        .Set(x => x.TriageDirectionCode, dtoMq.ConsequenceInfo?.TriageTarget)
                        .Set(x => x.TriageDirectionName, dtoMq.ConsequenceInfo?.TriageTargetName)
                        .Set(x => x.AreaCode, admissionRecordMQ.AreaCode)
                        .Set(x => x.AreaName, admissionRecordMQ.AreaName)
                        .Set(x => x.DeptCode, admissionRecordMQ.DeptCode)
                        .Set(x => x.DeptName, admissionRecordMQ.DeptName)
                        //.Set(x => x.OperatorName, admissionRecordMQ.OperatorName)
                        .Set(x => x.Consciousness, dto.Consciousness)
                        .Set(x => x.AllergyHistory, dto.AdmissionInfo?.AllergyHistory)
                        .Set(x => x.RFID, dto.RFID)
                        .Set(x => x.GuardianIdTypeCode, dto.GuardianIdTypeCode)
                        .Set(x => x.GuardianIdTypeName, dto.GuardianIdTypeName)
                        .Set(x => x.ToHospitalWayCode, dto.ToHospitalWay)
                        .Set(x => x.ToHospitalWayName, dto.ToHospitalWayName)
                        .Set(x => x.InDeptWay, dto.ToHospitalWayName) //ToHospitalWayName和InDeptWay业务上意义相同
                        .Set(x => x.CrowdCode, dto.CrowdCode)
                        .Set(x => x.CrowdName, dto.CrowdName)
                        .Set(x => x.VisitReasonCode, dto.VisitReasonCode)
                        .Set(x => x.VisitReasonName, dto.VisitReasonName)
                        .Set(x => x.GuardianIdCardNo, dto.GuardianIdCardNo)
                        .Set(x => x.TypeOfVisitCode, dto.TypeOfVisitCode)
                        .Set(x => x.TypeOfVisitName, dto.TypeOfVisitName)
                        .Set(x => x.GuardianPhone, dto.GuardianPhone)
                        .Set(x => x.CallingSn, callingSn)
                        .Set(x => x.IdTypeCode, dto.IdTypeCode)
                        .Set(x => x.IdTypeName, dto.IdTypeName)
                        .Set(x => x.InvoiceNum, dto.InvoiceNum)
                        //  .SetIf(admissionRecordMQ.FinishVisitTime.HasValue, x => x.FinishVisitTime, admissionRecordMQ.FinishVisitTime)
                        .ExecuteAffrowsAsync();

                    if (rows > 0)
                    {
                        //添加代办人信息，三无患者不需要添加
                        if (!admissionRecordMQ.IsNoThree)
                        {
                            AgencyPeopleDto agency = await _freeSql.Select<AgencyPeople>()
                                .Where(x => x.PiId == admissionRecordMQ.PI_ID).FirstAsync<AgencyPeopleDto>();

                            AgencyPeople agencyDto = new AgencyPeople(GuidGenerator.Create(), admissionRecordMQ.PI_ID,
                                admissionRecordMQ.PatientName, admissionRecordMQ.IDNo, admissionRecordMQ.ContactsPhone,
                                admissionRecordMQ.Sex, admissionRecordMQ.Age);
                            if (agency == null)
                            {
                                await _freeSql.Insert<AgencyPeople>(agencyDto).ExecuteAffrowsAsync();
                            }
                            else
                            {
                                agencyDto.SetId(agency.Id);
                                await _freeSql.Update<AgencyPeople>().SetSource(agencyDto).ExecuteAffrowsAsync();
                            }
                        }

                        List<TimeAxisRecord> timePointList = new List<TimeAxisRecord>();
                        if (dtoMq.RegisterInfo != null && !dtoMq.RegisterInfo.RegisterNo.IsNullOrWhiteSpace() &&
                            dtoMq.RegisterInfo?.RegisterNo != "已退号" && admissionRecordDB.VisitStatus == VisitStatus.未挂号)
                        {
                            timePointList.Add(new TimeAxisRecord()
                            {
                                PI_ID = dto.TriagePatientInfoId,
                                Time = dtoMq.RegisterInfo?.RegisterTime ?? DateTime.MinValue,
                                TimePointCode = TimePoint.RegisterTime.ToString(),
                            }.SetTimePointName());
                        }

                        if (dtoMq.RegisterInfo?.RegisterNo == "已退号")
                        {
                            timePointList.Add(new TimeAxisRecord()
                            {
                                PI_ID = dto.TriagePatientInfoId,
                                Time = DateTime.Now,
                                TimePointCode = TimePoint.BackNumberTime.ToString(),
                            }.SetTimePointName());
                        }

                        if (timePointList.Any())
                        {
                            await _freeSql.Insert(timePointList).ExecuteAffrowsAsync();
                        }

                        //生命体征和评分
                        if (dtoMq.VitalSignInfo != null)
                        {
                            VitalSignInfo vital = dtoMq.VitalSignInfo.To<VitalSignInfo>();
                            VitalSignInfo vitalModel = await _freeSql.Select<VitalSignInfo>()
                                .Where(x => x.PI_ID == admissionRecordDB.PI_ID).FirstAsync();

                            vital.PI_ID = admissionRecordDB.PI_ID;
                            if (vitalModel == null)
                            {
                                await _freeSql.Insert<VitalSignInfo>(vital).ExecuteAffrowsAsync();
                            }
                            else
                            {
                                await _freeSql.Update<VitalSignInfo>().SetDto(dtoMq.VitalSignInfo)
                                    .Where(w => w.PI_ID == admissionRecordDB.PI_ID).ExecuteAffrowsAsync();
                            }
                        }
                        else
                        {
                            await _freeSql.Delete<VitalSignInfo>().Where(w => w.PI_ID == admissionRecordDB.PI_ID)
                                .ExecuteAffrowsAsync();
                        }

                        await _freeSql.Delete<ScoreInfo>().Where(w => w.PI_ID == admissionRecordDB.PI_ID)
                            .ExecuteAffrowsAsync();
                        if (dtoMq.ScoreInfo != null && dtoMq.ScoreInfo.Any())
                        {
                            List<ScoreInfo> scoreInfo = dtoMq.ScoreInfo.To<List<ScoreInfo>>();
                            List<ScoreInfo> scoreList = new List<ScoreInfo>();
                            scoreInfo.ForEach(f =>
                            {
                                f.PI_ID = admissionRecordDB.PI_ID;
                                if (f.ScoreType != "JudgmentBd")
                                {
                                    scoreList.Add(f);
                                }
                            });
                            if (scoreList.Any())
                            {
                                await _freeSql.Insert<ScoreInfo>(scoreList).ExecuteAffrowsAsync();
                            }
                        }

                        // 病患列表变化消息
                        await _capPublisher.PublishAsync("im.patient.queue.changed", new object());
                    }
                }

                return RespUtil.Ok<string>(msg: "处理病患数据完成！");
            }
            catch (Exception e)
            {
                _log.LogError("处理病患数据失败！原因：{Msg}", e);
                throw;
            }
        }

        /// <summary>
        /// 根据分诊去向选择区域
        /// </summary>
        /// <param name="triageTargetCode">分诊去向代码</param>
        /// <param name="model"></param>
        private void ChooseAreaByTriageDirection(string triageTargetCode, AdmissionRecord model)
        {
            List<TriageDirection> triageDirections = TriageDirection.GetTriageDirections();

            // 根据分诊去向选择就诊区域
            TriageDirection triageDirection = triageDirections.Find(x => x.Code == triageTargetCode);
            if (triageDirection != null)
            {
                model.AreaCode = triageDirection.ToAreaCode;
                if (Enum.TryParse(model.AreaCode, out Area area))
                {
                    model.AreaName = area.GetDescription();
                }
            }
        }

        /// <summary>
        /// 患者取消挂号
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("sync.patient.register.cancel")]
        public async Task PatientRegisterCancelledAsync(PatientRegisterCancelledEto eto)
        {
            var row = await _freeSql.Update<AdmissionRecord>()
                .Where(x => x.PI_ID == eto.PI_ID && x.VisitStatus == VisitStatus.待就诊)
                .Set(x => x.VisitStatus, VisitStatus.已退号)
                .ExecuteAffrowsAsync();
            if (row > 0)
            {
                await _freeSql.Insert(new TimeAxisRecord()
                {
                    PI_ID = eto.PI_ID,
                    Time = DateTime.Now,
                    TimePointCode = TimePoint.BackNumberTime.ToString(),
                }.SetTimePointName()).ExecuteAffrowsAsync();
            }
        }

        /// <summary>
        /// 从队列中同步病患排序
        /// </summary>
        [CapSubscribe("sync.patient.sort.from.callservice")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ResponseResult<string>> SyncPatientSortFromMqAsync(ReSortPatientListDto dto)
        {
            try
            {
                if (dto.NewSort == dto.OldSort)
                {
                    _log.LogError("AdmissionRecordAppService ReSort Patient List Error.Msg:新序号与旧序号一致");
                    return RespUtil.Error<string>(msg: "新序号与旧序号一致");
                }

                // 更新新序号与旧序号之间的患者序号
                await _freeSql.Update<AdmissionRecord>()
                    .Where(x => x.TriageTime.BetweenEnd(DateTime.Now.Date,
                        DateTime.Now.Date.AddDays(1).AddMilliseconds(-1)))
                    .WhereIf(dto.NewSort > dto.OldSort, x => x.Sort > dto.OldSort - 1 && x.Sort < dto.NewSort)
                    .WhereIf(dto.NewSort < dto.OldSort, x => x.Sort > dto.NewSort - 1 && x.Sort < dto.OldSort)
                    .Set(a => a.Sort + 1)
                    .ExecuteAffrowsAsync();

                // 更新新患者的序号
                await _freeSql.Update<AdmissionRecord>()
                    .Where(x => x.PI_ID == dto.PI_ID)
                    .Set(a => a.Sort, dto.NewSort)
                    .ExecuteAffrowsAsync();

                // 病患列表变化消息
                await _capPublisher.PublishAsync("im.patient.queue.changed", new object());
                return RespUtil.Ok<string>(msg: "重新排序患者成功");
            }
            catch (Exception e)
            {
                _log.LogError("AdmissionRecordAppService ReSort Patient List Error.Msg:{Msg}", e);
                return RespUtil.InternalError<string>(msg: $"更新患者序号失败！原因：{e.Message}");
            }
        }

        /// <summary>
        /// 从队列中同步病患状态
        /// </summary>
        [CapSubscribe("sync.patient.visitstatus.from.callservice")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ResponseResult<string>> SyncVisitStatusFromMqAsync(SyncVisitStatusDto dto)
        {
            try
            {
                // 更新新患者的序号
                await _freeSql.Update<AdmissionRecord>()
                    .Where(x => x.PI_ID == dto.Id)
                    .Set(a => a.VisitStatus, dto.VisitStatus)
                    .SetIf(dto.VisitStatus == VisitStatus.过号, x => x.ExpireNumberTime, DateTime.Now)
                    .ExecuteAffrowsAsync();
                if (dto.VisitStatus == VisitStatus.已退号)
                {
                    //添加叫号时间节点 
                    await _freeSql.Insert(new TimeAxisRecord()
                    {
                        PI_ID = dto.Id,
                        Time = DateTime.Now,
                        TimePointCode = TimePoint.CancellationTime.ToString()
                    }.SetTimePointName()).ExecuteAffrowsAsync();
                }

                // 病患列表变化消息
                await _capPublisher.PublishAsync("im.patient.queue.changed", new object());
                return RespUtil.Ok<string>(msg: "更新患者就诊状态成功");
            }
            catch (Exception e)
            {
                _log.LogError("更新患者就诊状态成功失败.Msg:{Msg}", e);
                return RespUtil.InternalError<string>(msg: $"更新患者就诊状态成功失败！原因：{e.Message}");
            }
        }

        /// <summary>
        /// 分诊作废接口同步退号状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [CapSubscribe("sync.patient.visitstatus.from.triageservice")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ResponseResult<string>> SyncCancelStatusFromMqAsync(SyncVisitStatusDto dto)
        {
            try
            {
                // 更新新患者的序号
                await _freeSql.Update<AdmissionRecord>()
                    .Where(x => x.PI_ID == dto.Id)
                    .Set(a => a.VisitStatus, dto.VisitStatus)
                    .ExecuteAffrowsAsync();
                //添加叫号时间节点 
                await _freeSql.Insert(new TimeAxisRecord()
                {
                    PI_ID = dto.Id,
                    Time = DateTime.Now,
                    TimePointCode = TimePoint.CancellationTime.ToString()
                }.SetTimePointName()).ExecuteAffrowsAsync();
                return RespUtil.Ok<string>(msg: "更新患者就诊状态成功");
            }
            catch (Exception e)
            {
                _log.LogError("更新患者就诊状态成功失败.Msg:{Msg}", e);
                return RespUtil.InternalError<string>(msg: $"更新患者就诊状态成功失败！原因：{e.Message}");
            }
        }

        /// <summary>
        /// 分诊过号重排同步待就诊状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [CapSubscribe("triage.visitstatus.requeue")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task SyncReQueueStatusFromMqAsync(SyncVisitStatusDto dto)
        {
            try
            {
                // 更新新患者的就诊状态
                await _freeSql.Update<AdmissionRecord>()
                    .Where(x => x.PI_ID == dto.Id)
                    .Set(a => a.VisitStatus, dto.VisitStatus)
                    .Set(a => a.CallStatus, CallStatus.NotYet)
                    .Set(a => a.CallingDoctorId, string.Empty)
                    .Set(a => a.CallingDoctorName, string.Empty)
                    .Set(a => a.ExpireNumberTime, DateTime.Now)
                    .Set(a => a.IsReferral, dto.IsReferral)
                    .SetIf(!dto.IsReferral, a => a.TriageDoctorCode, string.Empty)
                    .SetIf(!dto.IsReferral, a => a.TriageDoctorName, string.Empty)
                    .ExecuteAffrowsAsync();
            }
            catch (Exception e)
            {
                _log.LogError("更新患者就诊状态成功失败.Msg:{Msg}", e);
            }
        }

        /// <summary>
        /// 更新床头贴信息
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("recipe.to.updatebedhead")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task UpdatePatientBedHeadAsync(BedHeadEto bedHeadEto)
        {
            if (bedHeadEto == null) return;

            AdmissionRecord admissionRecord = await _freeSql.Select<AdmissionRecord>().Where(x => x.PI_ID == bedHeadEto.PiId).FirstAsync();
            if (admissionRecord == null) return;

            List<string> bedHeadStickerList = new List<string>();
            if (!string.IsNullOrEmpty(admissionRecord.BedHeadSticker))
            {
                string[] BedHeadStickers = admissionRecord.BedHeadSticker.Split(',', StringSplitOptions.RemoveEmptyEntries);
                bedHeadStickerList.AddRange(BedHeadStickers);
            }

            switch (bedHeadEto.EntrustName)
            {
                case "告病危": bedHeadStickerList.Add("CriticallyIll"); break;
                case "告病重": bedHeadStickerList.Add("WasSeriouslyIll"); break;
                case "患者开绿通": bedHeadStickerList.Add("greenRoad"); break;
                case "取消告病危": bedHeadStickerList.Remove("CriticallyIll"); break;
                case "取消告病重": bedHeadStickerList.Remove("WasSeriouslyIll"); break;
                case "患者取消绿通": bedHeadStickerList.Remove("greenRoad"); break;
                default: break;
            }

            if (bedHeadEto.EntrustName == "患者开绿通")
            {
                await SyncGreedIntoToHis(admissionRecord, true);
                admissionRecord.IsOpenGreenChannl = true;
            }

            if (bedHeadEto.EntrustName == "患者取消绿通")
            {
                await SyncGreedIntoToHis(admissionRecord, false);
                admissionRecord.IsOpenGreenChannl = false;
                admissionRecord.GreenRoadCode = string.Empty;
                admissionRecord.GreenRoadName = string.Empty;
            }

            bedHeadStickerList = bedHeadStickerList.Distinct().ToList();
            admissionRecord.BedHeadSticker = string.Join(',', bedHeadStickerList);
            await _freeSql.Update<AdmissionRecord>().SetSource(admissionRecord).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// His同步患者状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [CapSubscribe("sync.patient.to.triagebyhisservice")]
        public async Task<ResponseResult<string>> SyncPatientStatusFromMqAsync(List<PatientInfoFromHisStatusDto> list)
        {
            try
            {
                var regSerialNos = list.Select(p => p.regSerialNo).ToList();
                var admissionRecords = await _freeSql.Select<AdmissionRecord>()
                    .Where(x => regSerialNos.Contains(x.RegisterNo)).ToListAsync();

                if (admissionRecords.Any())
                {
                    foreach (var item in list)
                    {
                        var record = admissionRecords.Where(p => p.RegisterNo == item.regSerialNo).FirstOrDefault();
                        //同步His就诊中的状态
                        //His      急诊                   
                        //1.诊中     4
                        //2.暂挂     1
                        //3.退号      3
                        //9.结束就诊  5

                        var icount = 0;
                        if (item.visitStatus == "3" && record.VisitStatus != VisitStatus.已退号)
                        {
                            icount++;
                            record.VisitStatus = VisitStatus.已退号;

                            _freeSql.Update<AdmissionRecord>()
                                .SetSource(record)
                                .ExecuteAffrows();
                        }
                    }
                }

                return RespUtil.Ok<string>(msg: "更新患者就诊状态成功");
            }
            catch (Exception e)
            {
                _log.LogError("更新患者就诊状态成功失败.Msg:{Msg}", e);
                return RespUtil.InternalError<string>(msg: $"更新患者就诊状态成功失败！原因：{e.Message}");
            }
        }

        /// <summary>
        /// 从队列中同步病患叫号状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [CapSubscribe("call.callstatus.changed")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ResponseResult<string>> SyncPatientCallStatusFromMqAsync(SyncPatientCallStatusDto dto)
        {
            try
            {
                if (await _freeSql.Select<AdmissionRecord>().Where(x => x.PI_ID == dto.PI_ID)
                        .AnyAsync(x =>
                            x.VisitStatus == VisitStatus.正在就诊 || x.VisitStatus == VisitStatus.已就诊 ||
                            x.VisitStatus == VisitStatus.出科))
                {
                    return RespUtil.Error<string>(msg: "患者已就诊");
                }

                var rows = await _freeSql.Update<AdmissionRecord>()
                    .Set(a => a.CallStatus, dto.CallStatus)
                    .Set(a => a.CallConsultingRoomCode, dto.CallConsultingRoomCode)
                    .Set(a => a.CallConsultingRoomName, dto.CallConsultingRoomName)
                    .Set(a => a.CallingDoctorId, dto.CallingDoctorId)
                    .Set(a => a.CallingDoctorName, dto.CallingDoctorName)
                    .Where(x => x.PI_ID == dto.PI_ID)
                    .ExecuteAffrowsAsync();
                if (rows > 0)
                {
                    // 病患列表变化消息
                    await _capPublisher.PublishAsync("im.patient.queue.changed", new object());
                    //7813 bug去掉
                    // if (dto.CallTime.HasValue)
                    // {
                    //     //添加叫号时间节点 
                    //     await _freeSql.Insert(new TimeAxisRecord()
                    //     {
                    //         PI_ID = dto.PI_ID,
                    //         Time = dto.CallTime.Value,
                    //         TimePointCode = dto.CallStatus == CallStatus.CancelCall
                    //             ? TimePoint.CancelCallTime.ToString()
                    //             : TimePoint.CallTime.ToString(),
                    //     }.SetTimePointName()).ExecuteAffrowsAsync();
                    // }

                    _log.LogInformation("Sync Patient Call Status Success");
                    return RespUtil.Ok<string>(msg: "同步病患叫号状态成功");
                }

                _log.LogError("Sync Patient Call Status Success");
                return RespUtil.Error<string>(msg: "同步病患叫号状态失败！原因：更新数据失败！");
            }
            catch (Exception e)
            {
                _log.LogError("Sync Patient Call Status Error.Msg：{Msg}", e);
                return RespUtil.InternalError<string>(msg: "同步病患叫号状态失败！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 从队列中同步病患取消叫号状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [CapSubscribe("sync.callcanceled.from.callservice")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ResponseResult<string>> SyncPatientCallCancelStatusFromMqAsync(
            SyncPatientCallCancelStatusDto dto)
        {
            try
            {
                if (!await _freeSql.Select<AdmissionRecord>().AnyAsync(x => x.CallStatus == CallStatus.Calling))
                {
                    return RespUtil.Error<string>(msg: "患者未叫号，无法取消");
                }

                var rows = await _freeSql.Update<AdmissionRecord>().Set(a => a.CallStatus, dto.CallStatus)
                    .Where(x => x.PI_ID == dto.PI_ID)
                    .ExecuteAffrowsAsync();
                if (rows > 0)
                {
                    // 病患列表变化消息
                    await _capPublisher.PublishAsync("im.patient.queue.changed", new object());
                    //添加叫号时间节点 7813 bug去掉
                    // await _freeSql.Insert(new TimeAxisRecord()
                    // {
                    //     PI_ID = dto.PI_ID,
                    //     Time = dto.CancelTime,
                    //     TimePointCode = TimePoint.CancelCallTime.ToString(),
                    // }.SetTimePointName()).ExecuteAffrowsAsync();
                    _log.LogInformation("Sync Patient Call Cancel Status Success");
                    return RespUtil.Ok<string>(msg: "同步病患叫号状态成功");
                }

                _log.LogError("Sync Patient Call Status Success");
                return RespUtil.Error<string>(msg: "同步病患取消叫号状态失败！原因：更新数据失败！");
            }
            catch (Exception e)
            {
                _log.LogError("Sync Patient Call Status Error.Msg：{Msg}", e);
                return RespUtil.InternalError<string>(msg: "同步病患取消叫号状态失败！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 患者列表，用于交接班
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetAdmissionRecordAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(input.VisitStatus != VisitStatus.全部, x => x.VisitStatus == input.VisitStatus)
                    //校验模糊查询文本
                    .WhereIf(!string.IsNullOrWhiteSpace(input.SearchText),
                        x => x.PatientName.Contains(input.SearchText)
                             || x.PatientID.Contains(input.SearchText)
                             || x.FirstDoctorCode.Contains(input.SearchText)
                             || x.FirstDoctorName.Contains(input.SearchText))
                    //校验就诊区域
                    .WhereIf(!input.AreaCode.IsNullOrWhiteSpace(), x => input.AreaCode.Contains(x.AreaCode))
                    .WhereIf(input.HasBed != -1, x => input.HasBed == 1 ? x.Bed != "" : x.Bed == "")
                    //校验责任医生
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyDoctorCode),
                        x => x.DutyDoctorCode == input.DutyDoctorCode)
                    //校验责任护士
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DutyNurseCode),
                        x => x.DutyNurseCode == input.DutyNurseCode)
                    .WhereIf(
                        input.HandoverEndTime != null && input.HandoverStartTime != null,
                        x => x.InDeptTime.Value <= input.HandoverEndTime.Value && (x.OutDeptTime == null ||
                            x.OutDeptTime.Value >= input.HandoverStartTime.Value))
                    .Count(out var totalCount)
                    .Page(input.PageIndex, input.PageSize)
                    .OrderBy(o => o.TriageLevel)
                    .OrderBy(o => o.Bed)
                    .ToListAsync(a => new AdmissionRecordDto
                    {
                        IsAttention = a.AttentionCode
                    }, cancellationToken);

                var piIds = list.Select(s => s.PI_ID).Distinct();
                // 查询诊断编码
                var diagnoseRecord = await _freeSql.Select<DiagnoseRecord>()
                    .Where(x => x.DiagnoseClassCode == DiagnoseClass.开立 && piIds.Contains(x.PI_ID))
                    .ToListAsync(a => new { a.DiagnoseCode, a.DiagnoseName, a.PI_ID },
                        cancellationToken);
                var scoreList = await _freeSql.Select<ScoreInfo>()
                    .Where(x => piIds.Contains(x.PI_ID))
                    .ToListAsync<ScoreInfoDto>(cancellationToken: cancellationToken);
                foreach (var item in list)
                {
                    item.DiagnoseCode = string.Join(",", diagnoseRecord.FindAll(x => x.PI_ID == item.PI_ID)
                        .Select(s => s.DiagnoseCode).ToList());
                    item.DiagnoseName = string.Join(",", diagnoseRecord.FindAll(x => x.PI_ID == item.PI_ID)
                        .Select(s => s.DiagnoseName).ToList());
                    item.IsAttention = (!string.IsNullOrWhiteSpace(item.IsAttention) && CurrentUser != null &&
                                        !CurrentUser.UserName.IsNullOrWhiteSpace() &&
                                        item.IsAttention.Contains(CurrentUser.UserName)).ToString();
                    item.ScoreInfo = scoreList.Where(x => x.PI_ID == item.PI_ID).ToList();
                }

                var res = new PagedResultDto<AdmissionRecordDto>
                {
                    TotalCount = totalCount,
                    Items = list.ToList()
                };
                _log.LogInformation("Get admissionRecord pages success");

                return RespUtil.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogError("Get admissionRecord pages error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<PagedResultDto<AdmissionRecordDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 列表打印获取数据信息
        /// </summary>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ResponseResult<Dictionary<string, object>>> GetPrintPatientAsync(Guid pI_ID)
        {
            var patient = await _freeSql.Select<AdmissionRecord>().Where(x => x.PI_ID == pI_ID)
                .FirstAsync<AdmissionRecordDto>();
            var dictionary = new Dictionary<string, object> { { "Patient", patient } };
            return RespUtil.Ok<Dictionary<string, object>>(data: dictionary);
        }

        /// <summary>
        /// 保存代办人信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> SaveAgentInfoAsync(AgentInfoCreateDto dto)
        {
            try
            {
                AgencyPeople agencyPeople = new AgencyPeople(GuidGenerator.Create(), dto.PiId, dto.AgencyPeopleName, dto.AgencyPeopleCard, dto.AgencyPeopleMobile);

                AgencyPeopleDto agencyPeopleDto = await _freeSql.Select<AgencyPeople>().Where(x => x.PiId == dto.PiId)
                    .FirstAsync<AgencyPeopleDto>();

                if (agencyPeopleDto == null)
                {
                    await _freeSql.Insert(agencyPeople).ExecuteAffrowsAsync();
                }
                else
                {
                    agencyPeople.SetId(agencyPeopleDto.Id);
                    await _freeSql.Update<AgencyPeople>().SetSource(agencyPeople).ExecuteAffrowsAsync();
                }

                return RespUtil.Ok(data: "保存成功");
            }
            catch (Exception e)
            {
                _log.LogError("保存代办人信息失败:{Msg}", e);
                return RespUtil.Error<string>(msg: e.Message);
            }
        }

        /// <summary>
        /// 同步分诊患者修改档案信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [CapSubscribe("sync.patient.modifypatient.from.triageservice")]
        public async Task SyncUpdatePatientByCapAsync(PatientModifyMqDto dto)
        {
            try
            {
                AdmissionRecord patient =
                    await _freeSql.Select<AdmissionRecord>().Where(x => x.PI_ID == dto.Id).FirstAsync();
                if (patient == null)
                {
                    throw new Exception(message: "患者不存在！");
                }

                await UpdatePatientAsync(dto);
            }
            catch (Exception e)
            {
                _log.LogError("修改档案失败:{Msg}", e);
                throw;
            }
        }

        public async Task UpdatePatientAsync(PatientModifyMqDto dto)
        {
            await _freeSql.Update<AdmissionRecord>()
                .Set(s => s.PatientName, dto.PatientName)
                .Set(s => s.PatientID, dto.PatientId)
                .Set(s => s.Sex, dto.Sex)
                .Set(s => s.SexName, dto.SexName)
                .Set(s => s.Birthday, DateTime.Parse(dto.BirthDay))
                .Set(s => s.ContactsPhone, dto.ContactsPhone)
                .Set(s => s.HomeAddress, dto.Address)
                .Set(s => s.IdentityCode, dto.IdTypeCode)
                .Set(s => s.IdentityName, dto.IdTypeName)
                .Set(s => s.IDNo, dto.IdentityNo)
                .Set(s => s.VisitNo, dto.VisitNo)
                // .Set(s => s.VisNo, dto.VisNo)
                // .Set(s => s.CallingSn, dto.CallingSn)
                .Set(s => s.Age, dto.GetAgeByIdCard(dto.IdentityNo) + "岁")
                .Where(x => x.PI_ID == dto.Id).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 保存绿色通道
        /// </summary>
        /// <param name="aR_ID">入科记录表-自增Id</param>
        /// <param name="dicName">开通病症的绿通name</param>
        /// <param name="dicCode">开通病症的绿通code</param>
        /// <param name="isOpen">是否开通绿通</param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> SaveGreenChannlAsync(int aR_ID, string dicName, string dicCode,
            bool isOpen = true, CancellationToken cancellationToken = default)
        {
            try
            {
                string operationUser = CurrentUser.FindClaimValue("name");

                AdmissionRecord record = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(aR_ID > 0, x => x.AR_ID == aR_ID)
                    .FirstAsync(cancellationToken);
                if (record == null)
                    return RespUtil.Error<string>(msg: "未找到患者信息");

                if (string.IsNullOrEmpty(record.VisSerialNo))
                {
                    return RespUtil.Error<string>(msg: "未写诊断，不能修改绿通状态");
                }

                GreenChannlRecord greenChannlRecord = await _freeSql.Select<GreenChannlRecord>()
                    .Where(x => x.PI_ID == record.PI_ID && x.IsOpen == true).FirstAsync();
                string bedHeadSticker = record.BedHeadSticker ?? string.Empty;
                List<string> bedHeadStickerList = bedHeadSticker.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                if (isOpen)
                {
                    //接诊后才可操作绿通，结束就诊和出科，都不允许开通绿通
                    if (record.VisitStatus == VisitStatus.正在就诊)
                    {
                        //更新His绿通状态
                        await SyncGreedIntoToHis(record, isOpen);
                        bedHeadStickerList.Add("greenRoad");

                        _freeSql.Transaction(() =>
                        {
                            if (greenChannlRecord == null)
                            {
                                var model = new GreenChannlRecord();
                                model.SetId(GuidGenerator.Create());
                                model.AR_ID = record.AR_ID;
                                model.PI_ID = record.PI_ID;
                                model.BeginTime = DateTime.Now;
                                model.OperationTime = DateTime.Now;
                                model.OperationUser = operationUser;
                                model.DicName = dicName;
                                model.DicCode = dicCode;
                                model.IsOpen = true;
                                _freeSql.Insert(model).ExecuteAffrowsAsync(cancellationToken);
                            }
                            else
                            {
                                var rows = _freeSql.Update<GreenChannlRecord>()
                                    .Set(a => a.IsOpen, isOpen)
                                    .Set(a => a.DicName, dicName)
                                    .Set(a => a.DicCode, dicCode)
                                    .Set(a => a.EndTime, null)
                                    .Set(a => a.OperationTime, DateTime.Now)
                                    .Set(a => a.OperationUser, operationUser)
                                    .Where(x => x.PI_ID == record.PI_ID)
                                    .ExecuteAffrowsAsync();
                            }

                            record.IsOpenGreenChannl = isOpen;
                            record.GreenRoadCode = dicCode;
                            record.GreenRoadName = dicName;
                            record.BedHeadSticker = string.Join(',', bedHeadStickerList.Distinct());
                            _freeSql.Update<AdmissionRecord>().SetSource(record).ExecuteAffrowsAsync();
                        });
                    }
                    else
                    {
                        return RespUtil.Error<string>(msg: "非正在就诊患者不可操作绿通");
                    }
                }
                else
                {
                    //更新His绿通状态
                    await SyncGreedIntoToHis(record, isOpen);
                    bedHeadStickerList.Remove("greenRoad");

                    _freeSql.Transaction(() =>
                    {
                        var rows = _freeSql.Update<GreenChannlRecord>()
                            .Set(a => a.IsOpen, isOpen)
                            .Set(a => a.EndTime, DateTime.Now)
                            .Set(a => a.OperationTime, DateTime.Now)
                            .Set(a => a.OperationUser, operationUser)
                            .Where(x => x.PI_ID == record.PI_ID)
                            .ExecuteAffrowsAsync();

                        record.IsOpenGreenChannl = isOpen;
                        record.GreenRoadCode = null;
                        record.GreenRoadName = null;
                        record.BedHeadSticker = string.Join(',', bedHeadStickerList);

                        _freeSql.Update<AdmissionRecord>().SetSource(record).ExecuteAffrowsAsync();
                    });
                }

                AdmissionRecord newRecord = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(aR_ID > 0, x => x.AR_ID == aR_ID)
                    .FirstAsync(cancellationToken);
                await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.UpdatePatient, newRecord);
                return RespUtil.Ok(data: "保存成功");
            }
            catch (Exception e)
            {
                _log.LogError("保存绿色通道失败:{Msg}", e);
                return RespUtil.Error<string>(msg: e.Message);
            }
        }

        /// <summary>
        /// 预检分诊保存绿通
        /// </summary>
        /// <param name="aR_ID"></param>
        /// <param name="dicName"></param>
        /// <param name="dicCode"></param>
        /// <param name="isOpen"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<ResponseResult<string>> SaveGreenChannlByTriageAsync(int aR_ID, string dicName,
            string dicCode, bool isOpen = true,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var record = await _freeSql.Select<AdmissionRecord>()
                    .WhereIf(aR_ID > 0, x => x.AR_ID == aR_ID)
                    .FirstAsync(cancellationToken);
                if (record == null)
                    return RespUtil.Error<string>(msg: "未找到患者信息");

                var greenChannlRecord = await _freeSql.Select<GreenChannlRecord>().Where(x => x.PI_ID == record.PI_ID)
                    .FirstAsync();
                if (isOpen)
                {
                    _freeSql.Transaction(() =>
                    {
                        if (greenChannlRecord == null)
                        {
                            var model = new GreenChannlRecord();
                            model.SetId(GuidGenerator.Create());
                            model.AR_ID = record.AR_ID;
                            model.PI_ID = record.PI_ID;
                            model.BeginTime = DateTime.Now;
                            model.OperationTime = DateTime.Now;
                            model.DicName = dicName;
                            model.DicCode = dicCode;
                            model.IsOpen = true;
                            _freeSql.Insert(model).ExecuteAffrowsAsync(cancellationToken);
                        }
                        else
                        {
                            var rows = _freeSql.Update<GreenChannlRecord>()
                                .Set(a => a.IsOpen, isOpen)
                                .Set(a => a.DicName, dicName)
                                .Set(a => a.DicCode, dicCode)
                                .Set(a => a.EndTime, null)
                                .Set(a => a.OperationTime, DateTime.Now)
                                .Where(x => x.PI_ID == record.PI_ID)
                                .ExecuteAffrowsAsync();
                        }

                        record.IsOpenGreenChannl = isOpen;
                        _freeSql.Update<AdmissionRecord>().SetSource(record).ExecuteAffrowsAsync();
                    });
                }
                else
                {
                    _freeSql.Transaction(() =>
                    {
                        var rows = _freeSql.Update<GreenChannlRecord>()
                            .Set(a => a.IsOpen, isOpen)
                            .Set(a => a.DicName, null)
                            .Set(a => a.DicCode, null)
                            .Set(a => a.EndTime, DateTime.Now)
                            .Set(a => a.OperationTime, DateTime.Now)
                            .Where(x => x.PI_ID == record.PI_ID)
                            .ExecuteAffrowsAsync();

                        record.IsOpenGreenChannl = isOpen;
                        record.GreenRoadCode = null;
                        record.GreenRoadName = null;
                        _freeSql.Update<AdmissionRecord>().SetSource(record).ExecuteAffrowsAsync();
                    });
                }

                return RespUtil.Ok(data: "保存成功");
            }
            catch (Exception e)
            {
                _log.LogError("保存绿色通道失败:{Msg}", e);
                return RespUtil.Error<string>(msg: e.Message);
            }
        }

        /// <summary>
        /// 获取篮网影像url
        /// </summary>
        /// <param name="visitNo"></param>
        /// <returns></returns>
        public Task<string> GetNetsImageUrl(string visitNo)
        {
            var url = _configuration["NetsImageUrl"] + "&externalNumber=" + visitNo;
            return Task.FromResult(url);
        }

        /// <summary>
        /// 入科同步更新患者就诊记录
        /// </summary>
        /// <param name="list"></param>
        /// <returns>主键自增Id</returns>
        public async Task<ResponseResult<int>> UpdateAdmissionRecordByViewAsync(
            List<UpdateAdmissionRecordByViewDto> list)
        {
            try
            {
                int rows = 0;
                if (list != null && list.Count > 0)
                {
                    foreach (var dto in list)
                    {
                        var model = await _freeSql.Select<AdmissionRecord>()
                            .WhereIf(dto.PatientID != string.Empty, x => x.PatientID == dto.PatientID)
                            .WhereIf(dto.RegisterNo != string.Empty, x => x.RegisterNo == dto.RegisterNo)
                            .FirstAsync();
                        if (model == null)
                        {
                            _log.LogError(
                                "入科同步更新患者就诊记录,不存在此患者. AR_ID:{PatientID} RegisterNo:{RegisterNo}",
                                dto.PatientID, dto.RegisterNo);
                            continue;
                        }

                        #region 更新就诊记录

                        //视图出科去向字段在表中无合适字段存储，暂存HisDeptCode
                        rows += await _freeSql.Update<AdmissionRecord>()
                            .SetIf(!string.IsNullOrWhiteSpace(dto.FirstDoctorCode), a => a.FirstDoctorCode,
                                dto.FirstDoctorCode)
                            .SetIf(!string.IsNullOrWhiteSpace(dto.FirstDoctorName), a => a.FirstDoctorName,
                                dto.FirstDoctorName)
                            .SetIf(dto.VisitDate != null, a => a.VisitDate, dto.VisitDate)
                            .SetIf(!string.IsNullOrWhiteSpace(dto.VisitNo), a => a.VisitNo, dto.VisitNo)
                            .SetIf(!string.IsNullOrWhiteSpace(dto.VisSerialNo), a => a.VisSerialNo, dto.VisSerialNo)
                            .SetIf(!string.IsNullOrWhiteSpace(dto.CallingDoctorName), a => a.CallingDoctorName,
                                dto.CallingDoctorName)
                            .SetIf(dto.OutDeptReasonCode != null, a => a.HisDeptCode, dto.OutDeptReasonCode)
                            .WhereIf(dto.PatientID != string.Empty, x => x.PatientID == dto.PatientID)
                            .WhereIf(dto.RegisterNo != string.Empty, x => x.RegisterNo == dto.RegisterNo)
                            .Where(x => x.AreaName == "抢救区" || x.AreaName == "留观区")
                            .ExecuteAffrowsAsync();

                        #endregion
                    }
                }
                else
                {
                    _log.LogInformation("入科同步更新患者就诊记录:{Msg}", "无可更新的患者");
                    return RespUtil.Ok<int>(extra: "无可更新的患者");
                }

                if (rows > 0)
                {
                    var msg = list.Count - rows > 0 ? "系统中有" + (list.Count - rows) + "患者不存在" : "";
                    _log.LogInformation("入科同步更新患者就诊记录成功:{Msg}", msg);
                    return RespUtil.Ok<int>(extra: "更新患者入科记录成功");
                }

                return RespUtil.Ok<int>(extra: "无可更新的患者");
            }
            catch (Exception e)
            {
                _log.LogError("Update admissionRecord error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError(extra: e.Message, data: -1);
            }
        }

        /// <summary>
        /// 查询当前抢救区和留观区正在就诊的患者
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<List<GetVisitAdmissionRecordDto>>> GetVisitAdmissionRecordAsync()
        {
            try
            {
                var admissionList = await _freeSql.Select<AdmissionRecord>()
                    .Where(x => x.VisitStatus == VisitStatus.正在就诊)
                    .Where(x => x.AreaName == "抢救区" || x.AreaName == "留观区")
                    .ToListAsync<GetVisitAdmissionRecordDto>();

                return RespUtil.Ok<List<GetVisitAdmissionRecordDto>>(data: admissionList);
            }
            catch (Exception e)
            {
                _log.LogError("Update admissionRecord error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<List<GetVisitAdmissionRecordDto>>(msg: e.Message);
            }
        }

        /// <summary>
        /// 根据PIID获取患者信息（不用token 不给前端用 其他内部服务调用）
        /// </summary>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ResponseResult<AdmissionRecordDto>> GetPatientInfoByIdAsync(Guid pI_ID)
        {
            try
            {
                if (pI_ID == Guid.Empty)
                {
                    return RespUtil.InternalError<AdmissionRecordDto>(msg: "请求参数为空");
                }

                AdmissionRecordDto admissionRecord = await _freeSql.Select<AdmissionRecord>()
                    .Where(x => x.PI_ID == pI_ID)
                    .FirstAsync<AdmissionRecordDto>();
                if (admissionRecord == null)
                {
                    return RespUtil.InternalError<AdmissionRecordDto>(msg: "未查到患者信息");
                }

                List<DiagnoseRecord> diagnoseList = await _freeSql.Select<DiagnoseRecord>()
                    .Where(x => x.IsDeleted == false && x.DiagnoseClassCode == DiagnoseClass.开立)
                    .Where(x => x.PI_ID == pI_ID)
                    .OrderBy(o => o.Sort)
                    .ToListAsync();

                VitalSignInfoDto vital = await _freeSql.Select<VitalSignInfo>()
                    .Where(w => w.PI_ID == admissionRecord.PI_ID).FirstAsync<VitalSignInfoDto>();

                List<ScoreInfoDto> score = await _freeSql.Select<ScoreInfo>()
                    .Where(w => w.PI_ID == admissionRecord.PI_ID).ToListAsync<ScoreInfoDto>();

                admissionRecord.VitalSignInfo = vital;
                admissionRecord.ScoreInfo = score;
                if (diagnoseList.Any())
                {
                    admissionRecord.DiagnoseCode = string.Join(',', diagnoseList.Select(x => x.DiagnoseCode));
                    admissionRecord.DiagnoseName = string.Join(',', diagnoseList.Select(x => x.DiagnoseName));
                }

                admissionRecord.IsFreeNumber = "4".Equals(admissionRecord.RegType);
                //查询代办人信息
                AgencyPeopleDto agentInfo = await _freeSql.Select<AgencyPeople>()
                    .Where(x => x.PiId == admissionRecord.PI_ID).FirstAsync<AgencyPeopleDto>();
                if (agentInfo != null)
                {
                    admissionRecord.AgencyPeopleName = agentInfo.AgencyPeopleName;
                    admissionRecord.AgencyPeopleCard = agentInfo.AgencyPeopleCard;
                    admissionRecord.AgencyPeopleAge = agentInfo.AgencyPeopleAge;
                    admissionRecord.AgencyPeopleSex = agentInfo.AgencyPeopleSex;
                    admissionRecord.AgencyPeopleMobile = agentInfo.AgencyPeopleMobile;
                }

                return RespUtil.Ok(data: admissionRecord);
            }
            catch (Exception e)
            {
                _log.LogError("Get admissionRecord pages error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<AdmissionRecordDto>(extra: e.Message);
            }
        }

        /// <summary>
        /// 腕带打印用
        /// </summary>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<WristStrapDto> GetWristStrapInfoByIdAsync(Guid pI_ID)
        {
            AdmissionRecordDto admissionRecord = await _freeSql.Select<AdmissionRecord>()
                .Where(x => x.PI_ID == pI_ID)
                .FirstAsync<AdmissionRecordDto>();

            if (admissionRecord.AreaCode == "RescueArea")
            {
                admissionRecord.TriageDeptName = "急诊抢救室";
            }

            if (admissionRecord.AreaCode == "ObservationArea")
            {
                admissionRecord.TriageDeptName = "急诊观察区";
            }

            WristStrapDto wristStrapDto = new WristStrapDto()
            {
                AdmissionRecords = new List<AdmissionRecordDto>() { admissionRecord }
            };

            return wristStrapDto;
        }

        /// <summary>
        /// 通过patientId获取用户就诊记录
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ResponseResult<List<AdmissionRecordDto>>> GetAdmissionRecordsByPatientIdAsync(string patientId, DateTime? startTime, DateTime? endTime, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(patientId))
            {
                return RespUtil.Forbidden<List<AdmissionRecordDto>>(msg: "参数patientId不能为空.");
            }
            try
            {
                var record = await _freeSql.Select<AdmissionRecord>()
                    .Where(x => x.PatientID == patientId)
                    .WhereIf(startTime.HasValue, x => x.RegisterTime >= startTime.Value)
                    .WhereIf(endTime.HasValue, x => x.RegisterTime < endTime.Value)
                    .OrderBy(p => p.RegisterTime)
                    .ToListAsync(a => new AdmissionRecordDto
                    {
                        IsAttention = a.AttentionCode
                    }, cancellationToken);

                return RespUtil.Ok(data: record);
            }
            catch (Exception e)
            {
                return RespUtil.InternalError<List<AdmissionRecordDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 更新特殊病人标识
        /// </summary>
        /// <param name="admissionRecordDto"></param>
        /// <returns></returns>
        public async Task<ResponseResult<bool>> UpdatePatientSpecialFlag(AdmissionRecordDto admissionRecordDto)
        {
            AdmissionRecord admissionRecord = await _freeSql.Select<AdmissionRecord>().Where(x => x.PI_ID == admissionRecordDto.PI_ID).FirstAsync();
            if (admissionRecord == null)
            {
                return RespUtil.InternalError<bool>(msg: "未查到患者信息");
            }

            admissionRecord.SpecialCode = admissionRecordDto.SpecialCode;
            admissionRecord.SpecialName = admissionRecordDto.SpecialName;
            await _freeSql.Update<AdmissionRecord>().SetSource(admissionRecord).ExecuteAffrowsAsync();

            return RespUtil.Ok(data: true);
        }
    }
}