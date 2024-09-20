using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Recipes;
using YiJian.Nursing.Recipes.Entities;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 检查业务 仓储实现
    /// </summary> 
    public class PacsRepository : NursingRepositoryBase<Pacs, Guid>, IPacsRepository
    {
        #region constructor
        public PacsRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        #endregion

    }
}

