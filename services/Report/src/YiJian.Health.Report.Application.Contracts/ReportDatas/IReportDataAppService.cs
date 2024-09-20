using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Etos.ReportHistoryDatas;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.ReportDatas.Dto;

namespace YiJian.Health.Report.ReportDatas
{
    /// <summary>
    /// 报表数据服务接口
    /// </summary>
    public interface IReportDataAppService : IApplicationService
    {
        /// <summary>
        /// 新增打印数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        Task<ResponseBase<string>> CreateReportDataAsync(List<ReportDataEto> dto);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pIId"></param>
        /// <param name="tempId"></param>
        /// <returns></returns>
        Task<ResponseBase<List<ReportDataDto>>> GetReportDataAsync(Guid pIId, Guid tempId);
    }
}