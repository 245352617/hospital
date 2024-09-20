using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Temperatures;
using YiJian.Nursing.Temperatures.Contracts;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 描述：体温单体温记录仓储
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:17:44
    /// </summary>
    public class TemperatureRecordRepository : NursingRepositoryBase<TemperatureRecord, Guid>, ITemperatureRecordRepository
    {
        public TemperatureRecordRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
