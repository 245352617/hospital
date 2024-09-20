using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 报卡上报记录
    /// </summary>
    public interface IReportCardEscalatedRecordAppService : IApplicationService
    {
        /// <summary>
        /// 获得该患者报卡上报记录
        /// </summary>
        Task<ResponseResult<List<ReportCardEscalatedRecordDto>>> GetReportCardEscalatedRecordListAsync(string PatientId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 创建报卡上报记录
        /// </summary>
        Task<ResponseResult<string>> CreateReportCardEscalatedRecordAsync(ReportCardEscalatedRecordCreateDto dto, CancellationToken cancellationToken = default);
    }
}
