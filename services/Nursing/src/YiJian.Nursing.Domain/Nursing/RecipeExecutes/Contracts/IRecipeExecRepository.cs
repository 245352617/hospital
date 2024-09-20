using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Nursing.RecipeExecutes.Entities;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 拆分记录表(执行单)仓储接口
    /// </summary>  
    public interface IRecipeExecRepository : IRepository<RecipeExec, Guid>
    {

    }
}
