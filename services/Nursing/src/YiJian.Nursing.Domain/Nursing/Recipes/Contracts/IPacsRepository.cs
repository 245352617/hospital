using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing.Recipes
{
    using Entities;

    /// <summary>
    /// 检查业务 仓储接口
    /// </summary>  
    public interface IPacsRepository : IRepository<Pacs, Guid>
    {
    }
}
