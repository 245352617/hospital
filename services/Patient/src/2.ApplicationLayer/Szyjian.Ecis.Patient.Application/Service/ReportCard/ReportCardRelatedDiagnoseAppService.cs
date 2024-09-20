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
    /// 报卡关联诊断
    /// </summary>
    public class ReportCardRelatedDiagnoseAppService : EcisPatientAppService, IReportCardRelatedDiagnoseAppService
    {
        private readonly IFreeSql _freeSql;
        private readonly ILogger<ReportCardRelatedDiagnoseAppService> _log;

        /// <summary>
        /// 报卡关联诊断
        /// </summary>
        /// <param name="freeSql"></param>
        /// <param name="log"></param>
        public ReportCardRelatedDiagnoseAppService(IFreeSql freeSql, ILogger<ReportCardRelatedDiagnoseAppService> log)
        {
            this._freeSql = freeSql;
            this._log = log;
        }

        /// <summary>
        /// 获取该报卡所有关联诊断
        /// </summary>
        public async Task<ResponseResult<List<ReportCardRelatedDiagnoseDto>>> GetRelatedDiagnoseListAsync(Guid reportCardID, CancellationToken cancellationToken = default)
        {
            try
            {
                // 检查报卡是否存在
                if (!await _freeSql.Select<ReportCard>().AnyAsync(x => x.Id == reportCardID && x.IsDeleted == false))
                {
                    _log.LogError($"Save ReportCard error. ErrorMsg: 指定报卡不存在，报卡GUID为：{reportCardID}");
                    return RespUtil.Error<List<ReportCardRelatedDiagnoseDto>>(msg: "该报卡不存在");
                }

                var card = await _freeSql.Select<ReportCardRelatedDiagnose>().Where(x => x.ReportCardID == reportCardID && x.IsDeleted == false)
                    .ToListAsync<ReportCardRelatedDiagnoseDto>();

                return RespUtil.Ok(data: card);
            }
            catch (Exception e)
            {
                _log.LogError("Get ReportCardRelatedDiagnose error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<List<ReportCardRelatedDiagnoseDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 保存该报卡关联诊断
        /// </summary>
        public async Task<ResponseResult<string>> SaveRelatedDiagnoseListAsync(ReportCardRelatedDiagnoseListDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!await _freeSql.Select<ReportCard>().AnyAsync(x => x.Id == dto.ReportCardID && x.IsDeleted == false))
                {
                    _log.LogError($"Save ReportCard error. ErrorMsg: 指定报卡不存在，报卡GUID为：{dto.ReportCardID}");
                    return RespUtil.Error<string>(msg: "指定报卡不存在");
                }

                using (var uow = _freeSql.CreateUnitOfWork())
                {
                    // 把该报卡原有的关联诊断都软删除
                    var rows = await _freeSql.Update<ReportCardRelatedDiagnose>()
                            .Set(a => a.IsDeleted, true)
                            .Set(a => a.DeleterId, CurrentUser.Id)
                            .Set(a => a.DeletionTime, DateTime.Now)
                            .Where(x => x.ReportCardID == dto.ReportCardID)
                            .ExecuteAffrowsAsync();
                    if (rows > 0)
                    {
                        _log.LogInformation($"Soft Delete ReportCardRelatedDiagnose success，ReportCardID = {dto.ReportCardID}, Affrows = {rows}");
                    }

                    //批量插入 ReportCardRelatedDiagnose 记录
                    if (dto.RelatedDiagnoseList.Count > 0)
                    {
                        List<ReportCardRelatedDiagnose> relatedDiagnoses = new List<ReportCardRelatedDiagnose>();
                        foreach (var item in dto.RelatedDiagnoseList)
                        {
                            if (relatedDiagnoses.FindIndex(x => x.DiagnoseCode == item.DiagnoseCode) != -1)
                                continue;
                            var relatedDiagnose = new ReportCardRelatedDiagnose(GuidGenerator.Create(), dto.ReportCardID, item.DiagnoseCode, item.DiagnoseName);
                            relatedDiagnose.CreationTime = DateTime.Now;
                            relatedDiagnose.CreatorId = CurrentUser.Id;
                            relatedDiagnoses.Add(relatedDiagnose);
                        }
                        await _freeSql.Insert(relatedDiagnoses).ExecuteAffrowsAsync();
                    }

                    uow.Commit();
                    return RespUtil.Ok(data: "success", msg: "保存成功");
                }
            }
            catch (Exception e)
            {
                _log.LogError("Save SaveRelatedDiagnoseList error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<string>(extra: e.Message);
            }
        }
    }
}
