using Abp.Application.Services;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 时间轴 API
    /// </summary>
    [Authorize]
    public class TimeAxisRecordAppService : EcisPatientAppService, ITimeAxisRecordAppService, ICapSubscribe
    {
        private readonly IFreeSql _fsql;
        private readonly ILogger<TimeAxisRecordAppService> _log;

        public TimeAxisRecordAppService(IFreeSql fsql, ILogger<TimeAxisRecordAppService> log)
        {
            _fsql = fsql;
            _log = log;
        }

        /// <summary>
        /// 保存时间轴节点数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResponseResult<int>> SaveTimeAxisRecordAsync(TimeAxisRecordDto dto)
        {
            int rows = await SaveTimeAxis(dto);
            if (rows > 0)
            {
                return RespUtil.Ok(data: 0);
            }

            return RespUtil.Error(msg: "保存时间轴节点失败，数据库写入数据异常！", data: -1);
        }

        /// <summary>
        /// 同步时间节点
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [CapSubscribe("sync.timeAxisRecord.to.patient")]
        [RemoteService(false)]
        public async Task SyncTimeAxisRecordAsync(TimeAxisRecordDto dto)
        {
            try
            {
                await SaveTimeAxis(dto);
            }
            catch (Exception e)
            {
                _log.LogError("同步时间节点异常:{Msg}", e);
            }
        }

        private async Task<int> SaveTimeAxis(TimeAxisRecordDto dto)
        {
            TimeAxisRecord timeAxisRecord = dto.To<TimeAxisRecord>();
            timeAxisRecord = timeAxisRecord.SetTimePointName();
            int rows = await _fsql.Insert(timeAxisRecord).ExecuteAffrowsAsync();
            return rows;
        }

        /// <summary>
        /// 获取病患时间轴记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult<IEnumerable<TimeAxisRecordDto>>> GetTimeAxisRecordListAsync(TimeAxisRecordInput input)
        {
            try
            {
                List<TimeAxisRecordDto> timeAxisRecordDtos = await _fsql.Select<TimeAxisRecord>()
                    .WhereIf(input.PI_ID != Guid.Empty, x => x.PI_ID == input.PI_ID)
                    .OrderBy(x => x.PT_ID)
                    .ToListAsync<TimeAxisRecordDto>();

                AdmissionRecord admissionRecord = await _fsql.Select<AdmissionRecord>()
                    .WhereIf(input.PI_ID != Guid.Empty, x => x.PI_ID == input.PI_ID)
                    .FirstAsync();

                string retentionTime;
                DateTime endTime = DateTime.Now;
                if (admissionRecord.VisitStatus != VisitStatus.过号)
                {
                    var finishTime = admissionRecord.OutDeptTime ?? admissionRecord.FinishVisitTime;
                    endTime = finishTime ?? endTime;
                }
                else
                {
                    //患者过号，结束时间为过号时间，为空则是分诊时间后的24小时
                    endTime = admissionRecord.ExpireNumberTime ?? admissionRecord.TriageTime.AddHours(24);
                }

                double surplusTime = endTime.Subtract(admissionRecord.TriageTime).TotalSeconds;
                retentionTime = StringUtils.ConverterTime(surplusTime);

                TimeAxisRecordDto timeAxisRecordDto = new TimeAxisRecordDto()
                {
                    TimePointCode = "RetentionTime",
                    TimePointName = retentionTime,
                };
                timeAxisRecordDtos.Add(timeAxisRecordDto);

                return RespUtil.Ok<IEnumerable<TimeAxisRecordDto>>(data: timeAxisRecordDtos);
            }
            catch (Exception e)
            {
                return RespUtil.InternalError<IEnumerable<TimeAxisRecordDto>>(extra: e.Message);
            }
        }
    }
}