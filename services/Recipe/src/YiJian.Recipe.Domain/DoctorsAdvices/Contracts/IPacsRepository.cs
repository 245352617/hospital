using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 检查项
    /// </summary>
    public interface IPacsRepository : IRepository<Pacs, Guid>
    {
    }
}
