using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipes.GroupConsultation;

namespace YiJian.Recipe.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 会诊管理 仓储实现
    /// </summary> 
    public class GroupConsultationRepository : RecipeRepositoryBase<GroupConsultation, Guid>,
        IGroupConsultationRepository
    {
        #region constructor

        public GroupConsultationRepository(IDbContextProvider<RecipeDbContext> dbContextProvider) : base(
            dbContextProvider)
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
                    g =>
                        g.ApplyName.Contains(filter))
                .LongCountAsync();
        }

        #endregion GetCount

        #region GetList

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pIId"></param>
        /// <param name="code"></param>
        /// <param name="typeCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<List<GroupConsultation>> GetListAsync(string pIId = null, string code = null, string typeCode = null, GroupConsultationStatus status = GroupConsultationStatus.全部)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .Include(i => i.InviteDoctors)
                .Include(i => i.DoctorSummarys)
                .WhereIf(
                    !pIId.IsNullOrEmpty(),
                    g =>
                        pIId.Contains(g.PI_ID.ToString()))
                .WhereIf(!code.IsNullOrWhiteSpace(), x =>
                    x.InviteDoctors.Any(a => a.Code == code) ||
                    x.ApplyCode == code)
                .WhereIf(!typeCode.IsNullOrWhiteSpace(), x => x.TypeCode == typeCode)
                 .WhereIf(status != GroupConsultationStatus.全部, x => x.Status == status)
                .OrderByDescending(o => o.CreationTime)
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
        public async Task<List<GroupConsultation>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    g =>
                        g.ApplyName.Contains(filter))
                .OrderBy(sorting.IsNullOrWhiteSpace() ? "CreationTime" : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();
        }

        #endregion GetPagedList
    }
}