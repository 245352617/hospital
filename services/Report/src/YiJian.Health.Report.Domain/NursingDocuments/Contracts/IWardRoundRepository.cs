using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 查房信息
    /// </summary>
    public interface IWardRoundRepository : IRepository<WardRound, Guid>
    {
    }
}
