using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 开方仓储接口
    /// </summary>
    public interface IPrescribeRepository : IRepository<Prescribe, Guid>
    {
    }
}
