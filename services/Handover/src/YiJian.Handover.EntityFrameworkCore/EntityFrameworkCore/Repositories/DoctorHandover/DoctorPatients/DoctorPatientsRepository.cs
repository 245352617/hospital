namespace YiJian.Handover.EntityFrameworkCore.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Volo.Abp.EntityFrameworkCore;
    using YiJian.Handover;
    using YiJian.Handover;

    public class DoctorPatientsRepository: HandoverRepositoryBase<DoctorPatients, Guid>, IDoctorPatientsRepository
    {
        #region constructor
        public DoctorPatientsRepository(IDbContextProvider<HandoverDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        #endregion

        #region GetCount
        /// <summary>
        /// 统计总记录数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync(string filter = null)
        {
            return await (await GetDbSetAsync())
                //.WhereIf(
                //    !filter.IsNullOrWhiteSpace(),
                //    d =>
                //        d.Name.Contains(filter))
                .LongCountAsync();
        }
        #endregion

        #region GetList
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sorting"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<DoctorPatients>> GetListAsync(string sorting = null,
            string filter = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                //.WhereIf(
                //    !filter.IsNullOrWhiteSpace(),
                //    d =>
                //        d.Name.Contains(filter)
                //)
                //.OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(DoctorPatients.Name) : sorting)
                .ToListAsync();
        }
        #endregion

        #region GetPagedList
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<DoctorPatients>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                //.IncludeDetails(includeDetails)
                //.WhereIf(
                //    !filter.IsNullOrWhiteSpace(),
                //    d =>
                //        d.Name.Contains(filter)
                //)
                //.OrderBy(nameof(DoctorPatients.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();
        }   
        #endregion
    }
}
