using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.NursingDocuments.Entities;

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 危重情况记录
    /// </summary>
    public interface ICriticalIllnessRepository : IRepository<CriticalIllness, Guid>
    {

    }
}
