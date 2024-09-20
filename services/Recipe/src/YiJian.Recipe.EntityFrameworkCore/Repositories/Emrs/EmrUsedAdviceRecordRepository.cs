using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Emrs.Contracts;
using YiJian.Emrs.Entities;
using YiJian.Recipe.EntityFrameworkCore;

namespace YiJian.Recipe.Repositories.Emrs
{
    /// <summary>
    /// 电子病历导入医嘱相关服务
    /// </summary>
    public class EmrUsedAdviceRecordRepository : EfCoreRepository<RecipeDbContext, EmrUsedAdviceRecord, Guid>, IEmrUsedAdviceRecordRepository
    {
        /// <summary>
        /// 电子病历导入医嘱相关服务
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public EmrUsedAdviceRecordRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
