using System;
using Volo.Abp.Domain.Repositories;
namespace YiJian.Nursing
{
    /// <summary>
    /// 导管护理信息仓储接口
    /// </summary>
    public interface ICanulaRepository : IRepository<Canula, Guid>
    {

    }
}