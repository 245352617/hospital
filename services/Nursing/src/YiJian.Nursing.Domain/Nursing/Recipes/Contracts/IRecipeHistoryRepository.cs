using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing.Recipes
{
    using Entities;

    /// <summary>
    /// 医嘱操作历史 仓储接口
    /// </summary>  
    public interface IRecipeHistoryRepository : IRepository<RecipeHistory, Guid>
    {
    }
}
