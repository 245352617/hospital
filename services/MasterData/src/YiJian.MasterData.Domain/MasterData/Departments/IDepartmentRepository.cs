using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData.Domain;

public interface IDepartmentRepository : IRepository<Department, Guid>
{
}
