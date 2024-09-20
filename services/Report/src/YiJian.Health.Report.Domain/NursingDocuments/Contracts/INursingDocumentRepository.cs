using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;  

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 护理单
    /// </summary>
    public interface INursingDocumentRepository : IRepository<NursingDocument, Guid>
    {

    }
}
