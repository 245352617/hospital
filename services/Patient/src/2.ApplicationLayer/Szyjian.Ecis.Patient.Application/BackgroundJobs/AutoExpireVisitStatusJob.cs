using DotNetCore.CAP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.BackgroundJob
{
    /// <summary>
    /// 将待就诊、未挂号且分诊时间超过24小时患者状态置为过号
    /// </summary>
    public class AutoExpireVisitStatusJob : IBackgroundJob
    {
        private readonly ILogger<AutoExpireVisitStatusJob> _log;
        private readonly IFreeSql _freeSql;
        private readonly IHospitalApi _hospitalApi;
        private readonly ICapPublisher _capPublisher;
        private readonly ICallApi _callService;
        private IConfiguration _configuration;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="log"></param>
        /// <param name="freeSql"></param>
        /// <param name="hospitalApi"></param>
        /// <param name="capPublisher"></param>
        public AutoExpireVisitStatusJob(ILogger<AutoExpireVisitStatusJob> log
            , IFreeSql freeSql
            , IHospitalApi hospitalApi
            , IConfiguration _Configuration
            , ICapPublisher capPublisher
            , ICallApi callService
            )
        {
            _log = log;
            _freeSql = freeSql;
            _hospitalApi = hospitalApi;
            _capPublisher = capPublisher;
            _callService = callService;
            _configuration = _Configuration;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public async Task ExecuteAsync()
        {
            try
            {
                IEnumerable<AdmissionRecord> admissionRecords = await ExpireVisitStatusAsync();
                Guid[] enumerable = admissionRecords.Select(x => x.PI_ID).ToArray();
                if (enumerable.Any())
                {
                    foreach (var id in enumerable)
                    {
                        // 将叫号队列中患者的状态置为过号
                        await _capPublisher.PublishAsync("patient.visitstatus.changed",
                            new { Id = id, VisitStatus = VisitStatus.过号 });
                    }
                }
            }
            catch (Exception e)
            {
                _log.LogError("自动过号定时任务执行异常:{Msg}", e);
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 将分诊时间超过24小时且就诊状态为待就诊、未挂号患者的就诊状态置为过号
        /// </summary>
        private async Task<IEnumerable<AdmissionRecord>> ExpireVisitStatusAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                int.TryParse(_configuration["AutoExpireVisitStatusHours"], out int autoExpireVisitStatusHours);
                DateTime now = DateTime.Now;
                List<AdmissionRecord> admissionList = await _freeSql.Select<AdmissionRecord>()
                    // 待就诊或者未挂号的患者，自动过号。
                    // 就诊流水号为空的患者，也当作过号处理。
                    .Where(x => x.AreaCode == nameof(Area.OutpatientArea))
                    .Where(x => x.VisitStatus == VisitStatus.待就诊
                                || x.VisitStatus == VisitStatus.未挂号
                                || (x.VisitStatus == VisitStatus.正在就诊 && string.IsNullOrEmpty(x.VisSerialNo)))
                    .Where(x => x.TriageTime <= now.AddHours(-autoExpireVisitStatusHours))
                    .ToListAsync();

                List<AdmissionRecord> admissionList1 = await _freeSql.Select<AdmissionRecord>()
                    // 待就诊或者未挂号的患者，自动过号。
                    // 就诊流水号为空的患者，也当作过号处理。
                    .Where(x => x.AreaCode == nameof(Area.ObservationArea) || x.AreaCode == nameof(Area.RescueArea))
                    .Where(x => x.VisitStatus == VisitStatus.待就诊
                            || (x.VisitStatus == VisitStatus.正在就诊 && string.IsNullOrEmpty(x.VisSerialNo)))
                    .Where(x => x.TriageTime <= now.AddDays(-3))
                    .ToListAsync();

                admissionList.AddRange(admissionList1);
                IEnumerable<AdmissionRecord> outQueueAmissionList = admissionList.Where(x => HasTransferArea(x).Result);
                if (outQueueAmissionList.Count() > 0)
                {
                    IEnumerable<Guid> ids = outQueueAmissionList.ToList().Select(y => y.PI_ID);
                    int rows = await _freeSql.Update<AdmissionRecord>()
                        .Set(a => a.VisitStatus, VisitStatus.过号)
                        .Set(a => a.CallStatus, CallStatus.Exceed)
                        .Set(a => a.ExpireNumberTime, now)
                        .Set(a => a.OperatorCode, "admin")
                        .Set(a => a.OperatorName, "系统管理员")
                        .Set(a => a.LastDirectionName, "挂号超24小时未操作，自动过号")
                        .Where(x => ids.Contains(x.PI_ID))
                        .ExecuteAffrowsAsync(cancellationToken);

                    foreach (AdmissionRecord admission in outQueueAmissionList)
                    {
                        // 调用HIS接口过号处理（移除叫号队列）
                        await _hospitalApi.ModifyRecordStatusAsync(admission);
                        // 调用叫号系统进行过号处理
                        await _callService.MissedTurnAsync(new MissedTurnDto() { RegisterNo = admission.RegisterNo });


                        // 插入患者时间轴数据
                        var timeAxisRecord = new TimeAxisRecord
                        {
                            PI_ID = admission.PI_ID,
                            Time = DateTime.Now,
                            TimePointCode = TimePoint.AutoExpireTime.ToString()
                        }.SetTimePointName();
                        _freeSql.Insert(timeAxisRecord).ExecuteAffrows();
                    }

                    if (rows == outQueueAmissionList.Count())
                    {
                        _log.LogInformation("Expire VisitStatus Success");
                        return outQueueAmissionList;
                    }

                    _log.LogError("Expire VisitStatus Error.Msg:{Msg}", "数据库更新数据失败！");
                    return null;
                }

                _log.LogInformation("没有需要自动过号的患者");
                return outQueueAmissionList;
            }
            catch (Exception e)
            {
                _log.LogError("Expire VisitStatus Error.Msg:{Msg}", e);
                return null;
            }
        }

        /// <summary>
        /// 是否有流转过区域
        /// </summary>
        /// <param name="admissionRecord"></param>
        /// <returns></returns>
        private async Task<bool> HasTransferArea(AdmissionRecord admissionRecord)
        {
            // 查询流转记录
            long transferAreaCount = await _freeSql.Select<TransferRecord>()
                .Where(x => x.PI_ID == admissionRecord.PI_ID)
                .GroupBy(x => x.ToAreaCode)
                .CountAsync();

            return transferAreaCount <= 1;
        }
    }
}