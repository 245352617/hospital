using Abp.Json;
using DotNetCore.CAP;
using MasterDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Users;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 流转记录API
    /// </summary>
    [Authorize]
    public class TransferRecordAppService : EcisPatientAppService, ITransferRecordAppService
    {
        private readonly IFreeSql _freeSql;
        private readonly ILogger<TransferRecordAppService> _log;
        private readonly ICapPublisher _capPublisher;
        private readonly GrpcClient _grpcClient;
        private readonly PdaAppService _pdaAppService;

        public TransferRecordAppService(IFreeSql freeSql
            , ILogger<TransferRecordAppService> log
            , ICapPublisher capPublisher
            , GrpcClient grpcClient
            , PdaAppService pdaAppService)
        {
            _freeSql = freeSql;
            _log = log;
            _capPublisher = capPublisher;
            _grpcClient = grpcClient;
            _pdaAppService = pdaAppService;
        }

        /// <summary>
        /// 根据分诊人id获取流转记录
        /// </summary>
        /// <param name="whereInput"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<IEnumerable<TransferRecordDto>>> GetTransferRecordListAsync(
            TransferRecordWhereInput whereInput,
            CancellationToken cancellationToken)
        {
            var list = await _freeSql.Select<TransferRecord>()
                .WhereIf(whereInput.PI_ID != Guid.Empty, w => w.PI_ID == whereInput.PI_ID)
                .ToListAsync<TransferRecordDto>(cancellationToken);
            return RespUtil.Ok<IEnumerable<TransferRecordDto>>(data: list);
        }

        /// <summary>
        /// 根据分诊人id获取流转打印记录
        /// </summary>
        /// <param name="whereInput"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<Dictionary<string, object>>> GetTransferRecordPrintAsync(TransferRecordWhereInput whereInput, CancellationToken cancellationToken)
        {
            List<TransferRecordDto> list = await _freeSql.Select<TransferRecord>()
                .WhereIf(whereInput.PI_ID != Guid.Empty, w => w.PI_ID == whereInput.PI_ID)
                .ToListAsync<TransferRecordDto>(cancellationToken);
            list.ForEach(x =>
            {
                x.FromArea = x.FromAreaCode == TransferType.ObservationArea.ToString() ? "留观区" :
                    x.FromAreaCode == TransferType.OutpatientArea.ToString() ? "就诊区" : "抢救区";
            });
            var dicData = new Dictionary<string, object> { { "TransferRecord", list } };
            var dict = new Dictionary<string, object> { { "models", dicData } };
            ArrayList array = new ArrayList
            {
                "TransferTime",
                "ToArea",
                "FromAreaCode",
                "FromArea",
                "ToDept",
                "OperatorName",
                "TransferReason"
            };
            var dicColumn = new Dictionary<string, object> { { "TransferRecord", array } };
            dict.Add("columns", dicColumn);
            return RespUtil.Ok(data: dict);
        }

        /// <summary>
        /// 新增流转记录
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> CreateTransferRecordAsync(CreateTransferRecordDro dto, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(dto.OperatorCode) || string.IsNullOrEmpty(dto.OperatorName))
            {
                dto.OperatorCode = CurrentUser.UserName;
                dto.OperatorName = CurrentUser.FindClaimValue("fullName");
            }

            try
            {
                AdmissionRecord admission = await _freeSql.Select<AdmissionRecord>().Where(w => w.PI_ID == dto.PI_ID).FirstAsync(cancellationToken);

                if (admission == null)
                {
                    _log.LogError("Create Transfer Record Error.Msg:{Msg}", "患者不存在！");
                    return RespUtil.Error<string>(msg: "患者不存在！");
                }

                TransferRecord transferRecord = dto.To<TransferRecord>();
                // 不需要更新诊疗记录
                var notUpdateAdmissionRecord = false;
                //转住院，出科，入科不需要重新给流转类型赋值
                if (!transferRecord.ToAreaCode.IsNullOrWhiteSpace() && transferRecord.TransferTypeCode != TransferType.OutDept &&
                                                               transferRecord.TransferTypeCode != TransferType.ToHospital &&
                                                               transferRecord.TransferTypeCode != TransferType.InDept &&
                                                               transferRecord.TransferTypeCode != TransferType.Death &&
                                                               transferRecord.TransferTypeCode != TransferType.EndVisit)
                {
                    if (admission.AreaCode != "OutpatientArea" && dto.ToArea == "OutpatientArea" && await _freeSql
                            .Select<DiagnoseRecord>()
                            .AnyAsync(x => !x.IsDeleted && x.PI_ID == dto.PI_ID, cancellationToken))
                    {
                        return RespUtil.Error<string>(msg: "患者已开诊断，无法转到就诊区！");
                    }

                    notUpdateAdmissionRecord = true;
                    var areaEnum = Enum.Parse<Area>(transferRecord.ToAreaCode);
                    transferRecord.TransferTypeCode = areaEnum switch
                    {
                        Area.OutpatientArea => TransferType.OutpatientArea,
                        Area.ObservationArea => TransferType.ObservationArea,
                        Area.RescueArea => TransferType.RescueArea,
                        _ => transferRecord.TransferTypeCode
                    };
                    var startTime = DateTime.Now;

                    if (admission.RecallTime.HasValue)
                    {
                        startTime = admission.RecallTime.Value;
                    }
                    else
                    {
                        var lastTimeAxisRecord = await _freeSql.Select<TimeAxisRecord>().Where(a => a.PI_ID == dto.PI_ID && a.TimePointCode == "InDeptTime").OrderByDescending(p => p.Time).FirstAsync();
                        startTime = lastTimeAxisRecord?.Time ?? startTime;
                    }
                    switch (admission.AreaCode)
                    {
                        case "RescueArea":
                            admission.RescueRetentionTime += DateTime.Now.Subtract(startTime).TotalMinutes;
                            admission.RecallTime = null;
                            break;
                        case "ObservationArea":
                            admission.ObservationRetentionTime += DateTime.Now.Subtract(startTime).TotalMinutes;
                            admission.RecallTime = null;
                            break;
                        default:
                            break;
                    }
                }

                transferRecord.OperatorName = CurrentUser.FindClaimValue("fullName");
                transferRecord.TransferType = transferRecord.TransferTypeCode.ToString();
                transferRecord.FromAreaCode = admission.AreaCode;
                transferRecord.FromDeptCode = admission.TriageDeptCode;
                transferRecord.TransferTime = DateTime.Now;
                if (transferRecord.TransferTypeCode == TransferType.OutDept)
                {
                    transferRecord.TransferTime = admission.OutDeptTime.Value;
                }

                var rows = 0;
                var isTransfer = false;  //是否就诊区转抢救留观区
                var transferArea = string.Empty;
                List<DepartmentModel> depts = await _grpcClient.GetDepartmentsAsync();
                DepartmentModel dept = depts.FirstOrDefault(x => x.Code == dto.ToDeptCode);
                _freeSql.Transaction(() =>
                {
                    rows = _freeSql.Insert(transferRecord).ExecuteAffrows();
                    if (rows > 0)
                    {

                        _log.LogInformation("添加流转记录成功，开始修改诊疗记录信息！");
                        switch (transferRecord.TransferTypeCode)
                        {

                            // 转抢救区
                            case TransferType.RescueArea:
                                if (admission.AreaCode == Area.OutpatientArea.ToString()) isTransfer = true;

                                transferArea = Area.RescueArea.GetDescription();
                                admission.AreaCode = Area.RescueArea.ToString();
                                admission.AreaName = Area.RescueArea.GetDescription();
                                admission.Bed = "";
                                break;

                            // 转留观区
                            case TransferType.ObservationArea:
                                if (admission.AreaCode == Area.OutpatientArea.ToString()) isTransfer = true;

                                transferArea = Area.RescueArea.GetDescription();
                                admission.AreaCode = Area.ObservationArea.ToString();
                                admission.AreaName = Area.ObservationArea.GetDescription();
                                admission.Bed = "";
                                break;

                            // 转就诊区
                            case TransferType.OutpatientArea:
                                admission.AreaCode = Area.OutpatientArea.ToString();
                                admission.AreaName = Area.OutpatientArea.GetDescription();
                                admission.Bed = "";
                                admission.VisitStatus = admission.VisitDate < DateTime.Now.Date.AddHours(-24)
                                    ? VisitStatus.过号
                                    : VisitStatus.待就诊;
                                admission.CallStatus = CallStatus.NotYet;
                                break;
                        }
                        _log.LogInformation("更新入科记录信息" + rows + notUpdateAdmissionRecord + ":" + admission.ToJsonString());
                        if (notUpdateAdmissionRecord)
                        {
                            admission.DeptCode = dept?.Code;
                            admission.DeptName = dept?.Name;

                            if (!string.IsNullOrWhiteSpace(dto.TriageLevel))
                            {
                                admission.TriageLevel = dto.TriageLevel;
                            }
                            if (!string.IsNullOrWhiteSpace(dto.TriageLevelName))
                            {
                                admission.TriageLevelName = dto.TriageLevelName;
                            }
                            rows = _freeSql.Update<AdmissionRecord>()
                                .SetSource(admission)
                                .ExecuteAffrows();
                        }

                        if (transferRecord.FromAreaCode == Area.RescueArea.ToString())
                        {
                            RescueRecord inRescueRecord = _freeSql.Select<RescueRecord>().Where(x => x.PI_Id == admission.PI_ID && x.TimePointName == "inrescue").OrderByDescending(x => x.TimePoint).First();
                            if (inRescueRecord != null)
                            {
                                RescueRecord rescueRecord = new RescueRecord()
                                {
                                    Id = Guid.NewGuid(),
                                    PI_Id = admission.PI_ID,
                                    TimePointName = "outrescue",
                                    TimePoint = DateTime.Now,
                                    OperateCode = dto.OperatorCode,
                                    OperateName = dto.OperatorName,
                                };

                                rescueRecord.Retention = (rescueRecord.TimePoint - inRescueRecord.TimePoint).TotalMinutes;
                                _freeSql.Insert(rescueRecord).ExecuteAffrows();
                            }
                        }

                        if (transferRecord.FromAreaCode == Area.ObservationArea.ToString())
                        {
                            RescueRecord inObservationRecord = _freeSql.Select<RescueRecord>().Where(x => x.PI_Id == admission.PI_ID && x.TimePointName == "inobservation").OrderByDescending(x => x.TimePoint).First();
                            if (inObservationRecord != null)
                            {
                                RescueRecord outObservationRecord = new RescueRecord()
                                {
                                    Id = Guid.NewGuid(),
                                    PI_Id = admission.PI_ID,
                                    TimePointName = "outobservation",
                                    TimePoint = DateTime.Now,
                                    OperateCode = dto.OperatorCode,
                                    OperateName = dto.OperatorName,
                                };

                                outObservationRecord.Retention = (outObservationRecord.TimePoint - inObservationRecord.TimePoint).TotalMinutes;
                                _freeSql.Insert(outObservationRecord).ExecuteAffrows();
                            }
                        }

                        //开始就诊不需要推消息
                        if (transferRecord.TransferTypeCode != TransferType.InDept)
                        {
                            // 未叫号患者开始接诊后发布队列消息到叫号服务
                            //_capPublisher.PublishAsync("sync.patient.transfer.to.callservice",
                            //   new
                            //   {
                            //       dto.PI_ID,
                            //       transferRecord.TransferTypeCode,
                            //       admission.AreaCode,
                            //       admission.AreaName,
                            //       transferRecord.ToDeptCode,
                            //       ToDeptName = transferRecord.ToDept,
                            //       admission.RegisterNo
                            //   });
                            //就诊区转抢救留观区需要过号处理
                            if (isTransfer)
                            {
                                _capPublisher.PublishAsync("patient.visitstatus.changed",
                                  new { Id = dto.PI_ID, VisitStatus = VisitStatus.过号, TransferArea = transferArea },
                                  cancellationToken: cancellationToken);
                            }
                        }
                    }
                });

                if (rows > 0)
                {
                    if (transferRecord.TransferTypeCode == TransferType.RescueArea || transferRecord.TransferTypeCode == TransferType.ObservationArea)
                    {
                        await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.OutDept, admission);
                    }

                    if (rows > 0 || notUpdateAdmissionRecord)
                    {
                        _log.LogInformation("流转修改诊疗记录信息成功");
                        #region 增加时间轴节点记录

                        if (transferRecord.TransferTypeCode != TransferType.InDept)
                        {
                            var timeAxisRecord = new TimeAxisRecord
                            {
                                PI_ID = transferRecord.PI_ID,
                                Time = transferRecord.TransferTime,
                                TimePointCode = transferRecord.TransferTypeCode switch
                                {
                                    TransferType.OutpatientArea => TimePoint.ToOutpatientAreaTime.ToString(),
                                    TransferType.RescueArea => TimePoint.ToRescueAreaTime.ToString(),
                                    TransferType.ObservationArea => TimePoint.ToObservationAreaTime.ToString(),
                                    TransferType.ToHospital => TimePoint.ToHospitalTime.ToString(),
                                    TransferType.OutDept => TimePoint.OutDeptTime.ToString(),
                                    TransferType.Death => TimePoint.OutDeptTime.ToString(),
                                    TransferType.InDept => TimePoint.InDeptTime.ToString(),
                                    TransferType.EndVisit => TimePoint.EndVisitTime.ToString(),
                                    _ => ""
                                }
                            };

                            if (timeAxisRecord.TimePointCode.IsNullOrWhiteSpace())
                            {
                                _log.LogInformation("Create transferRecord success");
                                return RespUtil.Ok<string>(msg: "流转成功！");
                            }

                            rows = await _freeSql.Insert(timeAxisRecord.SetTimePointName())
                                .ExecuteAffrowsAsync(cancellationToken);

                            if (rows > 0)
                            {
                                _log.LogInformation("流转添加时间轴节点成功");
                                return RespUtil.Ok<string>(msg: "流转成功!");
                            }
                        }

                        _log.LogError("Create transferRecord error.Msg:{Msg}", "数据库插入流转时间点失败！");
                        return RespUtil.Error<string>(extra: "流转失败！原因：保存时间轴节点失败！");

                        #endregion
                    }
                    _log.LogError("Create transferRecord error.Msg:{Msg}", "流转修改诊疗信息失败！");
                    return RespUtil.Error<string>(extra: "流转失败！原因：修改患者诊疗信息失败！");
                }

                _log.LogError("Create Transfer Record Error.Msg:{Msg}", "数据库保存流转记录失败！");
                return RespUtil.Error<string>(extra: "流转失败！原因：保存流转记录失败！");
            }
            catch (Exception e)
            {
                _log.LogError("Create Transfer Record Error.Msg:{Msg}", "数据库保存流转记录失败！" + e.Message + "---------------" + e.StackTrace);
                return RespUtil.InternalError<string>(msg: "流转失败！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 新增流转记录
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> CreateTransferRecordInfoAsync(CreateTransferRecordDro dto, CancellationToken cancellationToken)
        {
            try
            {
                var admission = await _freeSql.Select<AdmissionRecord>().Where(w => w.PI_ID == dto.PI_ID)
                    .FirstAsync(cancellationToken);

                if (admission == null)
                {
                    _log.LogError("Create Transfer Record Error.Msg:{Msg}", "患者不存在！");
                    return RespUtil.Error<string>(msg: "患者不存在！");
                }

                var model = dto.To<TransferRecord>();
                //转住院，出科，入科不需要重新给流转类型赋值
                if (!model.ToAreaCode.IsNullOrWhiteSpace() && (model.TransferTypeCode != TransferType.OutDept &&
                                                               model.TransferTypeCode != TransferType.ToHospital &&
                                                               model.TransferTypeCode != TransferType.InDept &&
                                                               model.TransferTypeCode != TransferType.Death))
                {
                    if (admission.AreaCode != "OutpatientArea" && dto.ToArea == "OutpatientArea" && await _freeSql
                            .Select<DiagnoseRecord>()
                            .AnyAsync(x => !x.IsDeleted && x.PI_ID == dto.PI_ID, cancellationToken))
                    {
                        return RespUtil.Error<string>(msg: "患者已开诊断，无法转到就诊区！");
                    }

                    var areaEnum = Enum.Parse<Area>(model.ToAreaCode);
                    model.TransferTypeCode = areaEnum switch
                    {
                        Area.OutpatientArea => TransferType.OutpatientArea,
                        Area.ObservationArea => TransferType.ObservationArea,
                        Area.RescueArea => TransferType.RescueArea,
                        _ => model.TransferTypeCode
                    };
                }

                model.OperatorName = CurrentUser.FindClaimValue("fullName");
                model.TransferType = model.TransferTypeCode.ToString();
                model.FromAreaCode = admission.AreaCode;
                model.FromDeptCode = admission.TriageDeptCode;
                model.TransferTime = DateTime.Now;
                var rows = 0;

                rows = _freeSql.Insert(model).ExecuteAffrows();

                if (rows > 0)
                {
                    _log.LogInformation("流转修改诊疗记录信息成功");

                    return RespUtil.Ok<string>(msg: "流转成功！");
                }

                _log.LogError("Create Transfer Record Error.Msg:{Msg}", "数据库保存流转记录失败！");
                return RespUtil.Error<string>(extra: "流转失败！原因：保存流转记录失败！");
            }
            catch (Exception e)
            {
                _log.LogError("Create Transfer Record Error.Msg:{Msg}", "数据库保存流转记录失败！" + e.Message + "---------------" + e.StackTrace);
                return RespUtil.InternalError<string>(msg: "流转失败！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 刪除流转记录
        /// </summary>
        /// <param name="transferType"></param>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> DeleteTransferRecordAsync(TransferType transferType, Guid pI_ID)
        {
            try
            {
                if (await _freeSql.Select<TransferRecord>()
                        .AnyAsync(x => x.TransferTypeCode == transferType && x.PI_ID == pI_ID))
                {
                    await _freeSql.Delete<TransferRecord>()
                        .Where(x => x.TransferTypeCode == transferType && x.PI_ID == pI_ID).ExecuteAffrowsAsync();
                }

                return RespUtil.Ok<string>(msg: "刪除成功！");
            }
            catch (Exception e)
            {
                return RespUtil.InternalError<string>(msg: "刪除失敗！原因：" + e.Message);
            }
        }
    }
}