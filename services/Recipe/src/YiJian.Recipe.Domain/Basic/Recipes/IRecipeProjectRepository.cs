using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Recipes.Basic.Recipes
{
    /// <summary>
    /// 医嘱项目
    /// Author: ywlin
    /// Date: 2021-12-04
    /// </summary>
    public interface IRecipeProjectRepository : IRepository<RecipeProject, Guid>
    {
        Task InsertOrUpdateAsync(IEnumerable<RecipeProject> list);
    }
}
