using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 检验项
    /// </summary>
    public interface ILisItemRepository : IRepository<LisItem, Guid>
    {
    }
}
