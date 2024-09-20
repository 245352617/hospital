using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing.Config
{
    /// <summary>
    /// 护士站通用配置表仓储接口
    /// </summary>
    public interface INursingConfigRepository : IRepository<NursingConfig, Guid>
    {
    }
}
