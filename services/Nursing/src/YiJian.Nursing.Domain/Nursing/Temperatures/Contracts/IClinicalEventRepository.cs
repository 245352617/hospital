using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing.Temperatures.Contracts
{
    /// <summary>
    /// 描述：临床事件仓储接口
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:09:23
    /// </summary>
    public interface IClinicalEventRepository : IRepository<ClinicalEvent, Guid>
    {
    }
}
