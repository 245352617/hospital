using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 检查小项
    /// </summary>
    public interface IPacsItemRepository : IRepository<PacsItem, Guid>
    {
    }
}
