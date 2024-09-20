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

    public class DoctorHandoverRepository : HandoverRepositoryBase<DoctorHandover, Guid>, IDoctorHandoverRepository
    {
        #region constructor

        public DoctorHandoverRepository(IDbContextProvider<HandoverDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        #endregion

        #region GetCount

        /// <summary>
        /// 统计总记录数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync(
            string startDate = null,
            string endDate = null)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!startDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(startDate) <= w.HandoverDate)
                .WhereIf(!endDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(endDate) >= w.HandoverDate)
                .Where(x => x.Status == 1)
                .LongCountAsync();
        }

        #endregion

        #region GetList

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sorting"></param>
        /// <param name="filter"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<DoctorHandover>> GetListAsync(string sorting = null,
            string filter = null, string startDate = null, string endDate = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .Include(c => c.PatientStatistics)
                .Include(c => c.DoctorPatients)
                .WhereIf(!startDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(startDate) <= w.HandoverDate)
                .WhereIf(!endDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(endDate) >= w.HandoverDate)
                //.WhereIf(
                //    !filter.IsNullOrWhiteSpace(),
                //    d =>
                //        d.Name.Contains(filter)
                //)
                //.OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(DoctorHandover.Name) : sorting)
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
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<DoctorHandover>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string startDate = null,
            string endDate = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .Include(i => i.PatientStatistics)
                .Include(i => i.DoctorPatients)
                .WhereIf(!startDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(startDate) <= w.HandoverDate)
                .WhereIf(!endDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(endDate) >= w.HandoverDate)
                .PageBy(skipCount, maxResultCount)
                .OrderByDescending(o => o.CreationTime)
                .ToListAsync();
        }

        #endregion
    }
}