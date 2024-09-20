using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Temperatures;
using YiJian.Nursing.Temperatures.Contracts;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 描述：临床事件仓储
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:13:03
    /// </summary>
    public class ClinicalEventRepository : NursingRepositoryBase<ClinicalEvent, Guid>, IClinicalEventRepository
    {
        public ClinicalEventRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
