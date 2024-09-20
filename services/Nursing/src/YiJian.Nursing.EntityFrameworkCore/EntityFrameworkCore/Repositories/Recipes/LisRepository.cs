using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Recipes;
using YiJian.Nursing.Recipes.Entities;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 检验业务 仓储实现
    /// </summary> 
    public class LisRepository : NursingRepositoryBase<Lis, Guid>, ILisRepository
    {
        #region constructor
        public LisRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        #endregion

    }
}

