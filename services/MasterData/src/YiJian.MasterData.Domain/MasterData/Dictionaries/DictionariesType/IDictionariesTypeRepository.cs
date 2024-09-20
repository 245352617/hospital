namespace YiJian.MasterData.DictionariesType
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;

    /// <summary>
    /// 字典类型编码 仓储接口
    /// </summary>  
    public interface IDictionariesTypeRepository : IRepository<DictionariesType, Guid>
    {
        /// <summary>
        /// 根据筛选获取总记录数
        /// </summary>        
        Task<long> GetCountAsync(string filter = null,
            int status = -1,
            DateTime? startTime = null,
            DateTime? endTime = null);

        /// <summary>
        /// 获取列表记录
        /// </summary>    
        Task<List<DictionariesType>> GetListAsync(
            string filter = null,
            string sorting = null);

        /// <summary>
        /// 获取分页记录
        /// </summary>   
        Task<List<DictionariesType>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            int status = -1,
            DateTime? startTime = null,
            DateTime? endTime = null,
            string sorting = null);
    }
}