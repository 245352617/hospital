using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 报卡设置
    /// </summary>
    public interface IReportCardAppService : IApplicationService
    {
        /// <summary>
        /// 获取所有报卡设置信息
        /// </summary>
        Task<ResponseResult<List<ReportCardDto>>> GetReportCardListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取单个报卡设置信息
        /// </summary>
        Task<ResponseResult<ReportCardDto>> GetReportCardAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 创建报卡设置信息记录
        /// </summary>
        Task<ResponseResult<string>> CreateReportCardAsync(ReportCardCreateDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// 修改报卡设置信息记录
        /// </summary>
        Task<ResponseResult<string>> UpdateReportCardAsync(ReportCardEditDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除报卡设置信息
        /// </summary>
        /// <param name="id">需要删除的报卡 Guid</param>
        Task<ResponseResult<string>> DeleteReportCardAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获得需要填写的报卡列表
        /// </summary>
        /// <param name="codes">多个诊断代码，由'|'分割</param>
        Task<ResponseResult<List<ReportCardSimpleDto>>> GetReportCardListByDiagnoseAsync(string codes, CancellationToken cancellationToken = default);
    }
}
