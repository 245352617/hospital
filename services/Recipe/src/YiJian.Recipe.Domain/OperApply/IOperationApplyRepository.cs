namespace YiJian.Recipe
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;

    /// <summary>
    /// 分诊患者id 仓储接口
    /// </summary>  
    public interface IOperationApplyRepository : IRepository<OperationApply, Guid>
    {
        /// <summary>
        /// 根据筛选获取总记录数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(string filter = null);

        Task<List<OperationApply>> GetListByPIIDAsync(
            Guid pI_ID,
            string sorting = null);

        Task<List<OperationApply>> GetListByPIIDDateAsync(
           Guid pI_ID,
           DateTime? startTime,
           DateTime? endTime, string applicantId = "",
           string sorting = null);

        /// <summary>
        /// 获取列表记录
        /// </summary>    
        Task<List<OperationApply>> GetListAsync(
            DateTime? startTime,
            DateTime? endTime, string applicantId = "",
            string sorting = null);

        /// <summary>
        /// 获取分页记录
        /// </summary>   
        Task<List<OperationApply>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null);
    }
}