namespace YiJian.Recipes.GroupConsultation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;

    /// <summary>
    /// 会诊管理 仓储接口
    /// </summary>  
    public interface IGroupConsultationRepository : IRepository<GroupConsultation, Guid>
    {
        /// <summary>
        /// 根据筛选获取总记录数
        /// </summary>        
        Task<long> GetCountAsync(string filter = null);

        /// <summary>
        /// 获取列表记录
        /// </summary>    
        Task<List<GroupConsultation>> GetListAsync(string pIId, string code = null, string typeCode = null, GroupConsultationStatus status = GroupConsultationStatus.全部);

        /// <summary>
        /// 获取分页记录
        /// </summary>   
        Task<List<GroupConsultation>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null);
    }

}