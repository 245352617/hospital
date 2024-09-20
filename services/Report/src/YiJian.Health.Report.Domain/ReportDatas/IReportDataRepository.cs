using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Health.Report.ReportDatas
{
    /// <summary>
    /// 打印数据
    /// </summary>
    public interface IReportDataRepository : IRepository<ReportData, Guid>
    {
        /// <summary>
        /// 查询打印数据列表
        /// </summary>
        /// <param name="pIId"></param>
        /// <param name="tempId"></param>
        /// <param name="operationCode"></param>
        /// <returns></returns>
        Task<List<ReportData>> GetListAsync(Guid pIId, Guid tempId, string operationCode="");
    }
}