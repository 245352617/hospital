using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 报卡关联诊断
    /// </summary>
    public interface IReportCardRelatedDiagnoseAppService : IApplicationService
    {
        /// <summary>
        /// 获取该报卡所有关联诊断
        /// </summary>
        Task<ResponseResult<List<ReportCardRelatedDiagnoseDto>>> GetRelatedDiagnoseListAsync(Guid reportCardID, CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存该报卡关联诊断
        /// </summary>
        Task<ResponseResult<string>> SaveRelatedDiagnoseListAsync(ReportCardRelatedDiagnoseListDto dto, CancellationToken cancellationToken = default);
    }
}
