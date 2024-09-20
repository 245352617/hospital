using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.NursingDocuments.Entities;

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 指尖血糖 mmol/L
    /// </summary>
    public interface IMmolRepository : IRepository<Mmol, Guid>
    {


    }
}
