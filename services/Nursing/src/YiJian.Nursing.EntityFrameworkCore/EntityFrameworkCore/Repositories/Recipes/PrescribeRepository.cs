using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Recipes;
using YiJian.Nursing.Recipes.Entities;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 药物处方业务 仓储实现
    /// </summary> 
    public class PrescribeRepository : NursingRepositoryBase<Prescribe, Guid>, IPrescribeRepository
    {
        #region constructor
        public PrescribeRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        #endregion

    }
}

