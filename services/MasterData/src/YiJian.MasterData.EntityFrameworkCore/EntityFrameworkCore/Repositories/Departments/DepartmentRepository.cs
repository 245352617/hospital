using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Domain;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


/// <summary>
/// 科室仓储
/// </summary>
public class DepartmentRepository : MasterDataRepositoryBase<Department, Guid>, IDepartmentRepository
{
    public DepartmentRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}

