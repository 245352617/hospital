namespace YiJian.Handover
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;

    /// <summary>
    /// 护士交班 仓储接口
    /// </summary>  
    public interface INurseHandoverRepository : IRepository<NurseHandover, Guid>
    {
        /// <summary>
        /// 根据筛选获取总记录数
        /// </summary>        
        Task<long> GetCountAsync(string filter = null, string startDate = null, string endDate = null);

        /// <summary>
        /// 获取列表记录
        /// </summary>    
        Task<List<NurseHandover>> GetListAsync(
            string filter = null,
            string sorting = null, string startDate = null, string endDate = null);

        /// <summary>
        /// 获取分页记录
        /// </summary>   
        Task<List<NurseHandover>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null, string startDate = null, string endDate = null);        
    }
}