using System;
using Volo.Abp.Domain.Repositories;
using YiJian.MasterData.Regions;

namespace YiJian.MasterData.Separations.Contracts;

/// <summary>
/// 地区存储
/// </summary>
public interface IRegionRepository : IRepository<Region, Guid>
{
  
}
