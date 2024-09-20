using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing.Temperatures.Contracts
{
    /// <summary>
    /// 描述：体温单体温记录仓储接口
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:17:00
    /// </summary>
    public interface ITemperatureRecordRepository : IRepository<TemperatureRecord, Guid>
    {
    }
}
