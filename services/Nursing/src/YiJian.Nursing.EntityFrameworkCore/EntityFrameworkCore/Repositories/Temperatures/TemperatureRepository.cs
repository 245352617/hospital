using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Temperatures;
using YiJian.Nursing.Temperatures.Contracts;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 描述：体温单仓储
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:20:45
    /// </summary>
    public class TemperatureRepository : NursingRepositoryBase<Temperature, Guid>, ITemperatureRepository
    {
        public TemperatureRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
