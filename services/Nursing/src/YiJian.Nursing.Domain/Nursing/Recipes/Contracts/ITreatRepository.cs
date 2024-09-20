using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing.Recipes
{
    using Entities;

    /// <summary>
    /// 诊疗业务 仓储接口
    /// </summary>  
    public interface ITreatRepository : IRepository<Treat, Guid>
    {
    }
}
