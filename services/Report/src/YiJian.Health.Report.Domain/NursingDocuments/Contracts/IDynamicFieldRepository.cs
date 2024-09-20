using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 动态字段名字描述
    /// </summary>
    public interface IDynamicFieldRepository : IRepository<DynamicField, Guid>
    {
    }
}
