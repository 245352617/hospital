using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing.Recipes
{
    using Entities;

    /// <summary>
    /// 检验业务 仓储接口
    /// </summary>  
    public interface ILisRepository : IRepository<Lis, Guid>
    {
    }
}
