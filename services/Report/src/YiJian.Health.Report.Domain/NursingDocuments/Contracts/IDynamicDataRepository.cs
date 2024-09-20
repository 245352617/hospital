using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.NursingDocuments.Entities;

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 动态字段数据
    /// </summary>
    public interface IDynamicDataRepository : IRepository<DynamicData, Guid>
    {
    }
}
