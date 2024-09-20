using System;
using Volo.Abp.Domain.Repositories;
using YiJian.MasterData.Separations.Entities;

namespace YiJian.MasterData.Separations.Contracts;

/// <summary>
/// 分方-途经存储接口
/// </summary>
public interface IUsageRepository : IRepository<Usage, Guid>
{
     
}
