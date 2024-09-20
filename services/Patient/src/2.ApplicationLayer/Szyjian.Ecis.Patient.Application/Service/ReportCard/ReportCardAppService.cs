using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 报卡类型设置
    /// </summary>
    public class ReportCardAppService : EcisPatientAppService, IReportCardAppService
    {
        private readonly IFreeSql _freeSql;
        private readonly ILogger<ReportCardAppService> _log;

        /// <summary>
        /// 报卡类型设置
        /// </summary>
        public ReportCardAppService(IFreeSql freeSql, ILogger<ReportCardAppService> log)
        {
            this._freeSql = freeSql;
            this._log = log;
        }

        /// <summary>
        /// 删除特定报卡类型
        /// </summary>
        /// <param name="id">需要删除的报卡 Guid</param>
        public async Task<ResponseResult<string>> DeleteReportCardAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                // Todo: 检查是否有上报记录，假如有的话不能删除

                //软删除报卡
                var rows = await _freeSql.Update<ReportCard>()
                                        .Set(a => a.IsDeleted, true)
                                        .Set(a => a.DeleterId, CurrentUser.Id)
                                        .Set(a => a.DeletionTime, DateTime.Now)
                                        .Where(x => x.Id == id)
                                        .ExecuteAffrowsAsync(cancellationToken);
                //软删除报卡相关诊断表信息
                await _freeSql.Update<ReportCardRelatedDiagnose>()
                                        .Set(a => a.IsDeleted, true)
                                        .Set(a => a.DeleterId, CurrentUser.Id)
                                        .Set(a => a.DeletionTime, DateTime.Now)
                                        .Where(x => x.ReportCardID == id)
                                        .ExecuteAffrowsAsync(cancellationToken);
                if (rows > 0)
                {
                    _log.LogInformation("Delete ReportCard success");
                    return RespUtil.Ok<string>(msg: "删除报卡设置成功");
                }


                _log.LogError($"Delete ReportCard error. ErrorMsg: 没有任何记录受到影响，要删除的报卡Guid为：{id}");
                return RespUtil.Error<string>(msg: "删除报卡设置失败，无法找到该报卡GUID");
            }
            catch (Exception e)
            {
                _log.LogError("Delete ReportCard error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<string>(extra: e.Message);
            }
        }

        /// <summary>
        /// 获取单个报卡类型设置信息
        /// </summary>
        /// <param name="id">报卡GUID</param>
        public async Task<ResponseResult<ReportCardDto>> GetReportCardAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                // 在获得报卡同时，带出其关联的诊断列表（诊断编码和诊断名）
                var reportCard = await _freeSql.Select<ReportCard>()
                .Where(x => x.IsDeleted == false && x.Id == id)
                .IncludeMany(x => x.RelatedDiagnoseList, then => then.Where(m => m.IsDeleted == false))
                .FirstAsync();

                if (reportCard == null)
                {
                    _log.LogError($"Get ReportCard error. ErrorMsg: 该报卡不存在，报卡GUID为：{id}");
                    return RespUtil.Error<ReportCardDto>(msg: "该报卡不存在");
                }

                // 使用Mapster做映射
                ReportCardDto reportCardDtos = reportCard.BuildAdapter().AdaptToType<ReportCardDto>();

                return RespUtil.Ok(msg: "成功获取报卡信息", data: reportCardDtos);
            }
            catch (Exception e)
            {
                _log.LogError("Get ReportCard error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<ReportCardDto>(extra: e.Message);
            }
        }

        /// <summary>
        /// 获取所有报卡类型设置信息
        /// </summary>
        public async Task<ResponseResult<List<ReportCardDto>>> GetReportCardListAsync(CancellationToken cancellationToken)
        {
            try
            {
                // 在获得报卡类型列表同时，带出其关联的诊断列表（诊断编码和诊断名）
                var reportCards = await _freeSql.Select<ReportCard>()
                    .IncludeMany(x => x.RelatedDiagnoseList, then => then.Where(m => m.IsDeleted == false).OrderBy(c => c.CreationTime))
                    .Where(x => x.IsDeleted == false)
                    .OrderByDescending(x => x.Sort)
                    .ToListAsync();

                // 使用Mapster做映射
                List<ReportCardDto> reportCardDtos = reportCards.BuildAdapter().AdaptToType<List<ReportCardDto>>();

                return RespUtil.Ok(msg: "成功获取所有报卡", data: reportCardDtos);
            }
            catch (Exception e)
            {
                _log.LogError("Get ReportCardList error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<List<ReportCardDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 创建报卡类型设置信息记录
        /// </summary>
        public async Task<ResponseResult<string>> CreateReportCardAsync(ReportCardCreateDto dto, CancellationToken cancellationToken)
        {
            try
            {
                // 判断报卡名是否存在，存在着报错
                if (_freeSql.Select<ReportCard>().Any(x => x.Name == dto.Name && x.IsDeleted == false))
                {
                    _log.LogError($"Create ReportCard error. ErrorMsg: 该报卡名已存在，报卡名为：{dto.Name}");
                    return RespUtil.Error<string>(msg: "该报卡名已存在");
                }
                // 判断报卡编码是否存在，存在着报错
                if (_freeSql.Select<ReportCard>().Any(x => x.Code == dto.Code && x.IsDeleted == false))
                {
                    _log.LogError($"Create ReportCard error. ErrorMsg: 该报卡编码已存在，报卡编码为：{dto.Code}");
                    return RespUtil.Error<string>(msg: "该报卡编码已存在");
                }

                var id = GuidGenerator.Create();
                var model = new ReportCard(id, dto.Name, dto.Code, dto.IsActived, dto.Sort);
                model.CreationTime = DateTime.Now;
                model.CreatorId = CurrentUser.Id;
                await _freeSql.Insert(model).ExecuteAffrowsAsync();

                return RespUtil.Ok(data: id.ToString(), msg: "保存成功");
            }
            catch (Exception e)
            {
                _log.LogError("Create ReportCard error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<string>(extra: e.Message);
            }
        }

        /// <summary>
        /// 更新报卡类型设置信息记录
        /// </summary>
        public async Task<ResponseResult<string>> UpdateReportCardAsync(ReportCardEditDto dto, CancellationToken cancellationToken)
        {
            try
            {
                // 判断报卡名是否存在，存在着报错
                if (_freeSql.Select<ReportCard>().Any(x => x.Id != dto.Id && x.Name == dto.Name && x.IsDeleted == false))
                {
                    _log.LogError($"Update ReportCard error. ErrorMsg: 该报卡名已存在，报卡名为：{dto.Name}");
                    return RespUtil.Error<string>(msg: "该报卡名已存在");
                }
                // 判断报卡编码是否存在，存在着报错
                if (_freeSql.Select<ReportCard>().Any(x => x.Id != dto.Id && x.Code == dto.Code && x.IsDeleted == false))
                {
                    _log.LogError($"Update ReportCard error. ErrorMsg: 该报卡编码已存在，报卡编码为：{dto.Code}");
                    return RespUtil.Error<string>(msg: "该报卡编码已存在");
                }

                var rows = await _freeSql.Update<ReportCard>()
                                         .IgnoreColumns(i => i.Id)
                                         .SetDto(dto)
                                         .Set(x => x.LastModificationTime, DateTime.Now)
                                         .Set(x => x.LastModifierId, CurrentUser.Id)
                                         .Where(w => w.Id == dto.Id)
                                         .ExecuteAffrowsAsync(cancellationToken);

                return RespUtil.Ok(data: rows.ToString(), msg: "保存成功");
            }
            catch (Exception e)
            {
                _log.LogError("Update ReportCard error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<string>(extra: e.Message);
            }
        }

        /// <summary>
        /// 获得需要填写的报卡列表
        /// </summary>
        /// <param name="codes">多个诊断代码，由'|'分割</param>
        public async Task<ResponseResult<List<ReportCardSimpleDto>>> GetReportCardListByDiagnoseAsync(string codes, CancellationToken cancellationToken = default)
        {
            try
            {
                // 诊断代码列表没数据
                if (codes.Trim().Length == 0)
                {
                    _log.LogInformation($"诊断代码列表不存在数据");
                    return RespUtil.Error<List<ReportCardSimpleDto>>(msg: "诊断代码列表为空，请检查");
                }

                var reportCards = await _freeSql.Select<ReportCard>()
                    .IncludeMany(x => x.RelatedDiagnoseList, then => then.Where(m => m.IsDeleted == false).OrderBy(c => c.CreationTime))
                    .Where(x => x.IsDeleted == false)
                    .OrderByDescending(x => x.Sort)
                    .ToListAsync();
                List<ReportCardSimpleDto> recordDtos = new List<ReportCardSimpleDto>();
                foreach (ReportCard reportCard in reportCards)
                {
                    ReportCardSimpleDto simpleDto = new ReportCardSimpleDto();
                    simpleDto.Code = reportCard.Code;
                    simpleDto.Name = reportCard.Name;
                    simpleDto.Sort = reportCard.Sort;

                    ReportCardRelatedDiagnose diagnoses = reportCard.RelatedDiagnoseList.Where(x => codes.Contains(x.DiagnoseCode)).FirstOrDefault();
                    if (diagnoses != null)
                    {
                        simpleDto.IsNeedEscalated = true;
                    }
                    else
                    {
                        simpleDto.IsNeedEscalated = false;
                    }
                    recordDtos.Add(simpleDto);
                }

                return RespUtil.Ok(msg: "获取报卡列表成功", data: recordDtos);
            }
            catch (Exception e)
            {

                _log.LogError("Get ReportCardList error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<List<ReportCardSimpleDto>>(extra: e.Message);
            }
        }
    }
}
