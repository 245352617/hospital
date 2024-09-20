using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Linq;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.ReportDatas;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 打印数据
    /// </summary>
    public class ReportDataRepository : EfCoreRepository<ReportDbContext, ReportData, Guid>, IReportDataRepository
    {
        public ReportDataRepository(IDbContextProvider<ReportDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 查询打印数据列表
        /// </summary>
        /// <param name="pIId"></param>
        /// <param name="tempId"></param>
        /// <param name="operationCode"></param>
        /// <returns></returns>
        public async Task<List<ReportData>> GetListAsync(Guid pIId, Guid tempId, string operationCode="")
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .WhereIf(pIId != Guid.Empty, x => x.PIID == pIId)
                .WhereIf(tempId != Guid.Empty, x => x.TempId == tempId)
                .WhereIf(!operationCode.IsNullOrEmpty(), x => x.OperationCode == operationCode)
                .OrderByDescending(o => o.CreationTime)
                .ToListAsync();
            return query;
        }
    }
}