using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;


namespace YiJian.Nursing.Settings
{
    /// <summary>
    /// 表:模块参数 仓储接口
    /// </summary>  
    public interface IParaModuleRepository : IRepository<ParaModule, Guid>
    {
        /// <summary>
        /// 根据筛选获取总记录数
        /// </summary>        
        Task<long> GetCountAsync(string filter = null);

        /// <summary>
        /// 获取列表记录
        /// </summary>    
        Task<List<ParaModule>> GetListAsync(string moduleType, string query = "", string moduleCode = "");

        /// <summary>
        /// 获取分页记录
        /// </summary>   
        Task<List<ParaModule>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null);
    }
}