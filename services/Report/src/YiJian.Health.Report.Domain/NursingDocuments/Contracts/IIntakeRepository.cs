using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.NursingDocuments.Entities;

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 入量出量
    /// </summary>
    public interface IIntakeRepository : IRepository<Intake, Guid>
    {

    }
}
