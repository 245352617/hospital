using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Temperatures;
using YiJian.Nursing.Temperatures.Contracts;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 描述：体温单动态属性仓储
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:16:04
    /// </summary>
    public class TemperatureDynamicRepository : NursingRepositoryBase<TemperatureDynamic, Guid>, ITemperatureDynamicRepository
    {
        public TemperatureDynamicRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
