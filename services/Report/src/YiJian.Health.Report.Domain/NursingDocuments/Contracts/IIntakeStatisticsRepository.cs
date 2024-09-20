using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.NursingDocuments.Entities;

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 入量出量统计
    /// </summary>
    public interface IIntakeStatisticsRepository : IRepository<IntakeStatistics, Guid>
    {
        /// <summary>
        /// 查询出入量统计信息
        /// </summary>
        /// <param name="nursingDocumentId"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        Task<List<IntakeStatistics>> GetIntakeStatisticsListAsync(Guid nursingDocumentId, DateTime? begintime, DateTime? endtime, int sheetIndex = 0);
    }
}
