using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Nursing.RecipeExecutes.Entities;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 描述：执行记录仓库接口
    /// 创建人： yangkai
    /// 创建时间：2023/3/9 14:50:26
    /// </summary>
    public interface IRecipeExecRecordRepository : IRepository<RecipeExecRecord, Guid>
    {
    }
}
