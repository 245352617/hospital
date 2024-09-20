using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData.DictionariesType;

/// <summary>
/// 嘱托 仓储接口
/// </summary>  
public interface IEntrustRepository : IRepository<Entrust, Guid>
{
    
}