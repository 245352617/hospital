using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// 超过24小时自动结束就诊
    /// </summary>
    public class AutoEndVisitJob : IBackgroundJob
    {
        private readonly ILogger<AutoEndVisitJob> _log;
        private readonly IHospitalApi _hisApi;
        private readonly ICapPublisher _capPublisher;
        private readonly IFreeSql _freeSql;
        private readonly ICallApi _callService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="log"></param>
        /// <param name="hisApi"></param>
        /// <param name="capPublisher"></param>
        /// <param name="freeSql"></param>
        /// <param name="callApi"></param>
        public AutoEndVisitJob(ILogger<AutoEndVisitJob> log
            , IHospitalApi hisApi
            , ICapPublisher capPublisher
            , IFreeSql freeSql
            , ICallApi callApi
            )
        {
            _log = log;
            _hisApi = hisApi;
            _capPublisher = capPublisher;
            _freeSql = freeSql;
            _callService = callApi;
        }

        /// <summary>
        /// 自动结束就诊
        /// </summary>
        public async Task ExecuteAsync()
        {
            try
            {
                var sw = new Stopwatch();
                sw.Start();
                var now = DateTime.Now;
                //就诊区超24小时
                List<AdmissionRecord> admissionList = await _freeSql.Select<AdmissionRecord>()
                    .Where(x => x.VisitStatus == VisitStatus.正在就诊)
                    .Where(x => x.RegisterTime <= now.AddDays(-1))
                    .Where(x => x.NotAutoEnd == false)
                    // 留观区是正在就诊的要结束就诊
                    // 抢救、留观区，只有未分配床号的，才自动结束就诊
                    .Where(x => x.AreaCode == nameof(Area.OutpatientArea))
                    .ToListAsync();

                //抢救留观区超过72小时
                List<AdmissionRecord> admissionList2 = await _freeSql.Select<AdmissionRecord>()
                  .Where(x => x.VisitStatus == VisitStatus.正在就诊)
                  .Where(x => x.RegisterTime <= now.AddDays(-3))
                  .Where(x => x.NotAutoEnd == false)
                  // 留观区是正在就诊的要结束就诊
                  // 抢救、留观区，只有未分配床号的，才自动结束就诊
                  .Where(x => ((x.AreaCode == nameof(Area.RescueArea) ||
                                x.AreaCode == nameof(Area.ObservationArea)) &&
                               string.IsNullOrEmpty(x.Bed)))
                  .ToListAsync();

                admissionList.AddRange(admissionList2);
                EndVisitDto defaultEndVisit = new EndVisitDto
                {
                    LastDirectionCode = "",
                    LastDirectionName = "接诊就诊区超24小时，抢救留观区超过72小时未操作，自动结束就诊",
                    KeyDiseasesCode = "",
                    KeyDiseasesName = ""
                };
                int count = 0;

                foreach (AdmissionRecord admissionRecord in admissionList)
                {
                    if (admissionRecord.AreaCode == Area.RescueArea.ToString() || admissionRecord.AreaCode == Area.ObservationArea.ToString())
                    {
                        TransferRecord transferRecord = await HasTransferArea(admissionRecord);
                        if (transferRecord != null)
                        {
                            // 转入
                            if (DateTime.Now > transferRecord.TransferTime.AddDays(1))
                            {
                                defaultEndVisit.LastDirectionName = "转入超24小时未操作，自动结束就诊";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            //待入
                            defaultEndVisit.LastDirectionName = "挂号超24小时未操作，自动结束就诊";
                        }
                    }

                    await _callService.TreatFinishAsync(admissionRecord.RegisterNo);
                    var result = await EndVisitAsync(admissionRecord.AR_ID, defaultEndVisit,
                        admissionRecord.DeptCode, admissionRecord.FirstDoctorCode, admissionRecord.FirstDoctorName, now);

                    if (result != null && result.Code == HttpStatusCodeEnum.Ok)
                    {
                        await InsertTimeAxisRecord(admissionRecord, now);
                    }
                    count++;
                }

                sw.Stop();
                _log.LogInformation("自动结束就诊成功！此次自动结束{Count}人，耗时：{Time}",
                    count,
                    sw.ElapsedMilliseconds / 1000 > 0
                        ? sw.ElapsedMilliseconds / 1000 + "秒"
                        : ((decimal)sw.ElapsedMilliseconds / 1000).ToString("F2") + "秒");
            }
            catch (Exception e)
            {
                _log.LogError("Execute BackgroundJob Error.Msg:{Msg}", e);
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 是否有流转过区域
        /// </summary>
        /// <param name="admissionRecord"></param>
        /// <returns></returns>
        private async Task<TransferRecord> HasTransferArea(AdmissionRecord admissionRecord)
        {
            // 查询流转记录
            TransferRecord transferRecord = await _freeSql.Select<TransferRecord>()
                    .Where(x => x.PI_ID == admissionRecord.PI_ID)
                    .OrderByDescending(x => x.TransferTime)
                    .FirstAsync();
            if (transferRecord != null && (transferRecord.ToAreaCode == "ToHospital" || transferRecord.ToAreaCode == "RescueArea" || transferRecord.ToAreaCode == "ObservationArea"))
            {
                return transferRecord;
            }
            else
            {
                return null;
            }
        }

        private async Task InsertTimeAxisRecord(AdmissionRecord admission, DateTime now)
        {
            // 插入患者时间轴数据
            var timeAxisRecord = new TimeAxisRecord
            {
                PI_ID = admission.PI_ID,
                Time = now,
                TimePointCode = TimePoint.AutoEndVisitTime.ToString()
            }.SetTimePointName();
            await _capPublisher.PublishAsync("patient.visitstatus.changed",
                new
                {
                    Id = admission.PI_ID,
                    VisitStatus = VisitStatus.已就诊,
                    FinishVisitTime = now
                });
            await _freeSql.Insert(timeAxisRecord).ExecuteAffrowsAsync();
            // 病患列表变化消息
            await _capPublisher.PublishAsync("im.patient.queue.changed", new object());
        }


        /// <summary>
        /// 结束就诊
        /// </summary>
        /// <param name="aR_ID">入科记录表id</param>
        /// <param name="dto"></param>
        /// <param name="deptCode"></param>
        /// <param name="operatorCode"></param>
        /// <param name="operatorName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<ResponseResult<string>> EndVisitAsync(int aR_ID, EndVisitDto dto, string deptCode,
            string operatorCode, string operatorName, DateTime now,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var admissionRecord = await _freeSql.Select<AdmissionRecord>()
                    .Where(x => x.AR_ID == aR_ID && !string.IsNullOrEmpty(x.VisSerialNo))
                    .FirstAsync(cancellationToken);
                if (admissionRecord != null)
                {
                    int id = 0;
                    string bedHeadSticker = admissionRecord.BedHeadSticker ?? string.Empty;
                    if (admissionRecord.IsOpenGreenChannl)
                    {
                        await _hisApi.EmergencyGreenChannel(admissionRecord, false);
                        GreenChannlRecord greenChannlRecord = await _freeSql.Select<GreenChannlRecord>().Where(x => x.AR_ID == admissionRecord.AR_ID).OrderByDescending(p => p.BeginTime).FirstAsync();
                        List<string> bedHeadStickerList = bedHeadSticker.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                        bedHeadStickerList.Remove("greenRoad");
                        bedHeadSticker = string.Join(",", bedHeadStickerList);
                        if (greenChannlRecord != null)
                        {
                            greenChannlRecord.EndTime = now;
                            await _freeSql.Update<GreenChannlRecord>().SetSource(greenChannlRecord).ExecuteAffrowsAsync();
                        }
                    }

                    _freeSql.Transaction(() =>
                    {
                        var result = _freeSql.Update<AdmissionRecord>()
                            .Set(s => s.VisitStatus, VisitStatus.已就诊)
                            .Set(s => s.FinishVisitTime, now)
                            .Set(s => s.LastDirectionCode, dto.LastDirectionCode)
                            .Set(s => s.LastDirectionName, dto.LastDirectionName)
                            .Set(s => s.KeyDiseasesCode, dto.KeyDiseasesCode)
                            .Set(s => s.KeyDiseasesName, dto.KeyDiseasesName)
                            .Set(s => s.BedHeadSticker, bedHeadSticker)
                            .Set(s => s.OperatorCode, operatorCode)
                            .Set(s => s.OperatorName, operatorName)
                            .Set(s => s.IsOpenGreenChannl, false)
                            .Set(s => s.GreenRoadCode, string.Empty)
                            .Set(s => s.GreenRoadName, string.Empty)
                            .Where(w => w.AR_ID == aR_ID)
                            .ExecuteAffrows();
                        id = result;
                    });

                    var model = await _freeSql.Select<AdmissionRecord>()
                        .Where(x => x.AR_ID == aR_ID).FirstAsync();
                    //主要诊断信息
                    var diagnoseRecord = await _freeSql.Select<DiagnoseRecord>().Where(x =>
                        x.DiagnoseClassCode == DiagnoseClass.开立
                        && x.DiagnoseType == "主要诊断"
                        && x.PI_ID == model.PI_ID && !x.IsDeleted).FirstAsync();
                    await _hisApi.SaveVisitRecordAsync(model, diagnoseRecord, TransferType.OutDept, operatorCode, operatorName);

                    if (id > 0)
                    {
                        await _capPublisher.PublishAsync("patient.visitstatus.changed",
                            new
                            {
                                Id = admissionRecord.PI_ID,
                                VisitStatus = VisitStatus.已就诊,
                                dto.LastDirectionCode,
                                dto.LastDirectionName,
                                admissionRecord.FirstDoctorCode,
                                admissionRecord.FirstDoctorName,
                                admissionRecord.FinishVisitTime
                            },
                            cancellationToken: cancellationToken);
                        // 病患列表变化消息
                        await _capPublisher.PublishAsync("im.patient.queue.changed", new object());
                    }

                    return RespUtil.Ok<string>(msg: "患者结束就诊成功！");
                }

                _log.LogError("End Visit Error.Msg:{Msg}", "该患者无诊疗记录！");
                return RespUtil.Error<string>(msg: "结束就诊失败！原因：该患者无诊疗记录！");
            }
            catch (Exception e)
            {
                _log.LogError("End Visit Error.Msg:{Msg}", e);
                return RespUtil.InternalError<string>(msg: "结束就诊失败！原因：" + e.Message);
            }
        }
    }
}