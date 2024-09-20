using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.NursingDocuments.Entities;

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 病人特征记录
    /// </summary>
    public interface ICharacteristicRepository : IRepository<Characteristic, Guid>
    {

    }
}
