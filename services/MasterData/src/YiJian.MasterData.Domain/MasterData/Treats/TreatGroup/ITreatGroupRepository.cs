using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData;

/// <summary>
/// 诊疗分组 仓储接口
/// </summary>  
public interface ITreatGroupRepository : IRepository<TreatGroup, Guid>
{
}