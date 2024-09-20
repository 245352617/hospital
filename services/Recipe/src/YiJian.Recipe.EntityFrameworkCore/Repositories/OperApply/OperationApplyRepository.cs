using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Recipe.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 分诊患者id 仓储实现
    /// </summary> 
    public class OperationApplyRepository : RecipeRepositoryBase<OperationApply, Guid>, IOperationApplyRepository
    {
        #region constructor

        public OperationApplyRepository(IDbContextProvider<RecipeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        #endregion

        #region GetCount

        /// <summary>
        /// 根据筛选获取总记录数
        /// </summary>  
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync(string filter = null)
        {
            return await (await GetDbSetAsync())
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    o =>
                        o.ApplicantName.Contains(filter))
                .LongCountAsync();
        }

        #endregion GetCount

        #region GetListByPIID

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sorting"></param>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        public async Task<List<OperationApply>> GetListByPIIDAsync(
            Guid pI_ID,
            string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(
                    pI_ID != Guid.Empty,
                    o =>
                        o.PI_Id == pI_ID)
                .OrderBy(sorting.IsNullOrWhiteSpace() ? "PI_Id" : sorting)
                .ToListAsync();
        }

        /// <summary>
        /// 根据PID，时间，排序，ApplicantId获取手术申请列表
        /// </summary>
        /// <param name="pI_ID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="applicantId"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        public async Task<List<OperationApply>> GetListByPIIDDateAsync(
           Guid pI_ID,
           DateTime? startTime,
           DateTime? endTime, string applicantId = "",
           string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(pI_ID != Guid.Empty, o => o.PI_Id == pI_ID)
                .WhereIf(startTime.HasValue, o => o.ApplyTime >= startTime)
                .WhereIf(endTime.HasValue, o => o.ApplyTime <= endTime)
                .WhereIf(!applicantId.IsNullOrWhiteSpace(), o => o.ApplicantId == applicantId)
                .OrderBy(o => o.ApplyTime)
                .ToListAsync();
        }


        #endregion GetListByPIID

        #region GetList

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="applicantId"></param>
        /// <param name="sorting"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public async Task<List<OperationApply>> GetListAsync(
            DateTime? startTime,
            DateTime? endTime, string applicantId = "",
            string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking().WhereIf(startTime != null, w => w.ApplyTime >= startTime)
                .WhereIf(endTime != null, w => w.ApplyTime <= endTime)
                .WhereIf(!applicantId.IsNullOrWhiteSpace(), w => w.ApplicantId == applicantId)

                .ToListAsync();
        }

        #endregion GetList

        #region GetPagedList

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="filter"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        public async Task<List<OperationApply>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    o =>
                        o.ApplicantName.Contains(filter))
                .OrderBy(sorting.IsNullOrWhiteSpace() ? "PI_Id" : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();
        }

        #endregion GetPagedList
    }
}