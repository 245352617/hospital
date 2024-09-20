using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 药理等级
    /// </summary>
    public interface IToxicRepository : IRepository<Toxic, Guid>
    {
    }
}
