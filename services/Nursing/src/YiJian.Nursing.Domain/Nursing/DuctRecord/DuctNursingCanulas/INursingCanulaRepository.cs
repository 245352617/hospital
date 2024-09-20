using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing
{
    /// <summary>
    /// 病人导管基础信息仓储接口
    /// </summary>
    public interface INursingCanulaRepository : IRepository<NursingCanula, Guid>
    {

    }
}