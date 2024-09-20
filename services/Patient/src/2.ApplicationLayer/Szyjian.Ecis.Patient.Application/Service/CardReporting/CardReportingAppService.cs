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
    /// 报卡
    /// </summary>
    [Authorize]
    [Obsolete("2.2.9.3报卡需求后将不在使用该服务，请使用ReportCardAppService及相关服务")]
    public class CardReportingAppService : EcisPatientAppService, ICardReportingAppService
    {
        private readonly IFreeSql _freeSql;
        private readonly ILogger<CardReportingAppService> _log;

        /// <summary>
        /// 报卡
        /// </summary>
        /// <param name="freeSql"></param>
        /// <param name="log"></param>
        /// <param name="hospitalClientAppService"></param>
        public CardReportingAppService(IFreeSql freeSql, ILogger<CardReportingAppService> log)
        {
            _freeSql = freeSql;
            _log = log;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<Guid>> SaveAsync(CardReportingSaveDto dto)
        {
            try
            {
                AdmissionRecord admissionRecord = await _freeSql.Select<AdmissionRecord>().Where(x => x.PI_ID == dto.PIID)
                    .FirstAsync();
                if (admissionRecord == null)
                {
                    return RespUtil.InternalError<Guid>(extra: "患者不存在");
                }

                _freeSql.Transaction(() =>
                {
                    //该患者报卡不存在则新增
                    if (!_freeSql.Select<CardReporting>().Any(x =>
                            x.PIID == dto.PIID && x.CardReportingType == dto.CardReportingType))
                    {
                        Guid id = GuidGenerator.Create();
                        CardReporting cardReporting = new CardReporting(id, dto.CardReportingType, dto.IsEscalation, dto.CardContent, dto.PIID);
                        _freeSql.Insert(cardReporting).ExecuteAffrows();
                    }
                    else
                    {
                        _freeSql.Update<CardReporting>()
                            .Set(s => s.CardContent, dto.CardContent)
                            .Where(x => x.PIID == dto.PIID && x.CardReportingType == dto.CardReportingType)
                            .ExecuteAffrows();
                    }
                });
                return RespUtil.Ok(data: Guid.Empty, msg: "保存成功");
            }
            catch (Exception e)
            {
                _log.LogError("Save CardReporting error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<Guid>(extra: e.Message);
            }
        }

        /// <summary>
        /// 查询患者已上报报卡
        /// </summary>
        /// <param name="piid"></param>
        /// <returns></returns>
        public async Task<ResponseResult<List<CardReportingDto>>> GetCardListAsync(Guid piid)
        {
            try
            {
                List<CardReportingDto> card = await _freeSql.Select<CardReporting>()
                     .Where(x => x.PIID == piid && x.IsEscalation)
                     .ToListAsync<CardReportingDto>();
                return RespUtil.Ok(data: card);
            }
            catch (Exception e)
            {
                _log.LogError("Get CardReporting error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<List<CardReportingDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 获取报卡信息
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<CardReportingDto>> GetAsync(Guid piid, ECardReportingType cardReportingType)
        {
            try
            {
                var card = await _freeSql.Select<CardReporting>()
                    .Where(x => x.PIID == piid && x.CardReportingType == cardReportingType)
                    .FirstAsync<CardReportingDto>();
                return RespUtil.Ok(data: card);
            }
            catch (Exception e)
            {
                _log.LogError("Get CardReporting error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<CardReportingDto>(extra: e.Message);
            }
        }
    }
}