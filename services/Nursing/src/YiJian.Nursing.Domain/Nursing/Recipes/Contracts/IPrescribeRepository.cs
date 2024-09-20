using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing.Recipes
{
    using Entities;

    /// <summary>
    /// 药物处方业务 仓储接口
    /// </summary>  
    public interface IPrescribeRepository : IRepository<Prescribe, Guid>
    {
    }
}
