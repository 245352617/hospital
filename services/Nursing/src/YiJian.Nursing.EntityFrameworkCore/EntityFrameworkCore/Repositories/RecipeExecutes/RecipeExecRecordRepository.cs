using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.RecipeExecutes;
using YiJian.Nursing.RecipeExecutes.Entities;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 描述：执行记录仓库类
    /// 创建人： yangkai
    /// 创建时间：2023/3/9 14:53:18
    /// </summary>
    public class RecipeExecRecordRepository : NursingRepositoryBase<RecipeExecRecord, Guid>, IRecipeExecRecordRepository
    {
        public RecipeExecRecordRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
