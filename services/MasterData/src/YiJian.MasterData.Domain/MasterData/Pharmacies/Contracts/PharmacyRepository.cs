using System;
using Volo.Abp.Domain.Repositories;
using YiJian.MasterData.Pharmacies.Entities;

namespace YiJian.MasterData.Pharmacies.Contracts;

/// <summary>
/// 药房信息
/// </summary>
public interface IPharmacyRepository : IRepository<Pharmacy, Guid>
{


}
