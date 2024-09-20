using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Recipes;
using YiJian.Nursing.Recipes.Entities;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 诊疗业务 仓储实现
    /// </summary> 
    public class TreatRepository : NursingRepositoryBase<Treat, Guid>, ITreatRepository
    {
        #region constructor
        public TreatRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        #endregion

    }
}

