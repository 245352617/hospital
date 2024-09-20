using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 报卡上报记录
    /// </summary>
    public class ReportCardEscalatedRecordAppService : EcisPatientAppService, IReportCardEscalatedRecordAppService
    {
        private readonly IFreeSql _freeSql;
        private readonly ILogger<ReportCardEscalatedRecordAppService> _log;

        public ReportCardEscalatedRecordAppService(IFreeSql freeSql, ILogger<ReportCardEscalatedRecordAppService> log)
        {
            this._freeSql = freeSql;
            this._log = log;
        }

        /// <summary>
        /// 创建报卡上报记录
        /// </summary>
        public async Task<ResponseResult<string>> CreateReportCardEscalatedRecordAsync(ReportCardEscalatedRecordCreateDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                var id = GuidGenerator.Create();
                var model = new ReportCardEscalatedRecord(id, dto.PatientID, dto.PIID, dto.ReportCardCode, dto.ReportCardName, CurrentUser.Id);
                await _freeSql.Insert(model).ExecuteAffrowsAsync();

                return RespUtil.Ok(data: id.ToString(), msg: "保存成功");
            }
            catch (Exception ex)
            {
                _log.LogError("Create ReportCardEscalatedRecord error. ErrorMsg:{Msg}", ex);
                return RespUtil.InternalError<string>(extra: ex.Message);
            }
        }

        /// <summary>
        /// 获得该患者报卡上报记录
        /// </summary>
        /// <param name="PatientId">患者ID</param>
        public async Task<ResponseResult<List<ReportCardEscalatedRecordDto>>> GetReportCardEscalatedRecordListAsync(string PatientId, CancellationToken cancellationToken = default)
        {
            try
            {
                var records = await _freeSql.Select<ReportCardEscalatedRecord>()
                .Where(x => x.IsDeleted == false && x.PatientID == PatientId)
                .GroupBy(a => new { a.PatientID, a.ReportCardCode })
                .OrderByDescending(x => x.Max(x.Value.CreationTime))
                .ToListAsync(x =>
                    new ReportCardEscalatedRecordDto
                    {
                        PatientID = x.Key.PatientID,
                        ReportCardCode = x.Key.ReportCardCode,
                        CreationTime = x.Max(x.Value.CreationTime)
                    });

                if (records == null)
                {
                    _log.LogError($"Get ReportCardEscalatedRecordList error. ErrorMsg: 该PatientId不存在报卡上报记录，PatientId为：{PatientId}");
                    return RespUtil.Error<List<ReportCardEscalatedRecordDto>>(msg: "该PatientId不存在报卡上报记录");
                }

                return RespUtil.Ok(msg: "成功获取该PatientId报卡上报记录", data: records);
            }
            catch (Exception ex)
            {
                _log.LogError("Get ReportCardEscalatedRecordList error. ErrorMsg:{Msg}", ex);
                return RespUtil.InternalError<List<ReportCardEscalatedRecordDto>>(extra: ex.Message);
            }
        }
    }
}
