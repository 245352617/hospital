using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing.Recipes
{
    using Entities;

    /// <summary>
    /// 医嘱仓储接口
    /// </summary>  
    public interface IRecipeRepository : IRepository<Recipe, Guid>
    {

    }
}
