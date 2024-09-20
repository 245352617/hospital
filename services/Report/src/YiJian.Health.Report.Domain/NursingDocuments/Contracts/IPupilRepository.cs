using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.NursingDocuments.Entities;

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 瞳孔评估
    /// </summary>
    public interface IPupilRepository : IRepository<Pupil, Guid>
    {

    }
}
