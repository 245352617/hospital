using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing.Temperatures.Contracts
{
    /// <summary>
    /// 描述：体温单动态属性仓储接口
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:15:24
    /// </summary>
    public interface ITemperatureDynamicRepository : IRepository<TemperatureDynamic, Guid>
    {
    }
}
