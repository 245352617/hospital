namespace YiJian.Recipes.InviteDoctor
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;
    using YiJian.Recipe;

    /// <summary>
    /// 会诊纪要医生 仓储接口
    /// </summary>  
    public interface IDoctorSummaryRepository : IRepository<DoctorSummary, Guid>
    {
        /// <summary>
        /// 根据筛选获取总记录数
        /// </summary>        
        Task<long> GetCountAsync(string filter = null);

        /// <summary>
        /// 获取列表记录
        /// </summary>    
        Task<List<DoctorSummary>> GetListAsync(
            string filter = null,
            string sorting = null);

        /// <summary>
        /// 获取分页记录
        /// </summary>   
        Task<List<DoctorSummary>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null);
    }
}