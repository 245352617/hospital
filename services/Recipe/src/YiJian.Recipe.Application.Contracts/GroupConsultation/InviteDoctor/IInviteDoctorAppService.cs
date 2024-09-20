namespace YiJian.Recipes.InviteDoctor
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Dtos;
    using Volo.Abp.Application.Services;

    /// <summary>
    /// 会诊邀请医生 API Interface
    /// </summary>   
    public interface IInviteDoctorAppService : IApplicationService
    {
        /// <summary>
        /// 修改
        /// </summary> 
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(InviteDoctorUpdate input);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InviteDoctorData> GetAsync(Guid id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<InviteDoctorData>> GetListAsync(
            string filter = null,
            string sorting = null);
    }
}