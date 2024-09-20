namespace YiJian.Recipes.InviteDoctor
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;

    /// <summary>
    /// 会诊邀请医生 仓储接口
    /// </summary>  
    public interface IInviteDoctorRepository : IRepository<InviteDoctor, Guid>
    {
        /// <summary>
        /// 根据筛选获取总记录数
        /// </summary>        
        Task<long> GetCountAsync(string filter = null);

        /// <summary>
        /// 获取列表记录
        /// </summary>    
        Task<List<InviteDoctor>> GetListAsync(
            string filter = null,
            string sorting = null);

        /// <summary>
        /// 获取分页记录
        /// </summary>   
        Task<List<InviteDoctor>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null);
    }
}