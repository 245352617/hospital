using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:人体图-编号字典 仓储接口
    /// </summary>  
    public interface ICanulaPartRepository : IRepository<CanulaPart, Guid>
    {
        /// <summary>
        /// 根据筛选获取总记录数
        /// </summary>        
        Task<long> GetCountAsync(string filter = null);

        /// <summary>
        /// 获取列表记录
        /// </summary>    
        Task<List<CanulaPart>> GetListAsync(
            string filter = null,
            string sorting = null);

        /// <summary>
        /// 获取分页记录
        /// </summary>   
        Task<List<CanulaPart>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null);
    }
}