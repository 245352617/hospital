namespace YiJian.ECIS.Call.EntityFrameworkCore.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Volo.Abp.EntityFrameworkCore;
    using YiJian.ECIS.Call;
    using YiJian.ECIS.Call.Domain;

    /// <summary>
    /// 科室仓储
    /// </summary>
    public class DepartmentRepository : CallRepositoryBase<Department, Guid>, IDepartmentRepository
    {
        public DepartmentRepository(IDbContextProvider<CallDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Department> GetByCodeAsync(string code)
        {
            return (await GetDbSetAsync())
               .AsNoTracking()
               .WhereIf(
                   !code.IsNullOrWhiteSpace(),
                   d => d.Code == code)
               .FirstOrDefault();
        }

        /// <summary>
        /// 统计总记录数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync(string filter = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    d =>
                        d.Name.Contains(filter))
                .LongCountAsync();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sorting"></param>
        /// <param name="filter"></param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        public async Task<List<Department>> GetListAsync(string sorting = null,
            string filter = null,
            bool includeDetails = false)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .IncludeDetails(includeDetails)
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    d =>
                        d.Name.Contains(filter)
                )
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(Department.Name) : sorting)
                .ToListAsync();
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting"></param>
        /// <param name="filter"></param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        public async Task<List<Department>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string sorting = null,
            string filter = null,
            bool includeDetails = false)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .IncludeDetails(includeDetails)
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    d =>
                        d.Name.Contains(filter)
                )
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(Department.Name) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();
        }
    }
}
