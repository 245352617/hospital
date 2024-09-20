using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Nursing.RecipeExecutes.Entities;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 医嘱执行历史记录
    /// </summary>  
    public interface IRecipeExecHistoryRepository : IRepository<RecipeExecHistory, Guid>
    {

    }
}
