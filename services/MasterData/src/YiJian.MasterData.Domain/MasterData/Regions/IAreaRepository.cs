using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData.Regions;

/// <summary>
/// 描述：地区仓储接口
/// 创建人： yangkai
/// 创建时间：2023/2/10 17:46:16
/// </summary>
public interface IAreaRepository : IRepository<Area, Guid>
{
}
