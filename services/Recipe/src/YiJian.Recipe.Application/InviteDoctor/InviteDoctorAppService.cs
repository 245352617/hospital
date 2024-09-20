using Microsoft.AspNetCore.Mvc;
using YiJian.Recipe;

namespace YiJian.Recipes.InviteDoctor
{
    using Microsoft.AspNetCore.Authorization;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Dtos;
    using YiJian.Recipe.Permissions;

    /// <summary>
    /// 会诊邀请医生 API
    /// </summary>
    public class InviteDoctorAppService : RecipeAppService, IInviteDoctorAppService
    {
        private readonly IInviteDoctorRepository _inviteDoctorRepository;

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inviteDoctorRepository"></param>
        public InviteDoctorAppService(IInviteDoctorRepository inviteDoctorRepository)
        {
            _inviteDoctorRepository = inviteDoctorRepository;
        }

        #endregion constructor


        #region Update

        /// <summary>
        /// 修改
        /// </summary> 
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAsync(InviteDoctorUpdate input)
        {
            if (input.Id == Guid.Empty)
            {
                var model = ObjectMapper.Map<InviteDoctorUpdate, InviteDoctor>(input);
                await _inviteDoctorRepository.InsertAsync(model);
            }
            else
            {
                var inviteDoctor = await _inviteDoctorRepository.GetAsync(input.Id);

                inviteDoctor.Modify(
                    checkInStatus: input.CheckInStatus, // 状态，0：已邀请，1：已报到
                    checkInTime: input.CheckInTime, // 报道时间
                    opinion: input.Opinion // 意见
                );

                await _inviteDoctorRepository.UpdateAsync(inviteDoctor);
            }
        }

        #endregion Update

        #region Get

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(RecipePermissions.InviteDoctors.Default)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<InviteDoctorData> GetAsync(Guid id)
        {
            var inviteDoctor = await _inviteDoctorRepository.GetAsync(id);

            return ObjectMapper.Map<InviteDoctor, InviteDoctorData>(inviteDoctor);
        }

        #endregion Get

        #region GetList

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [Authorize(RecipePermissions.InviteDoctors.Default)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ListResultDto<InviteDoctorData>> GetListAsync(
            string filter = null,
            string sorting = null)
        {
            var result = await _inviteDoctorRepository.GetListAsync(filter, sorting);

            return new ListResultDto<InviteDoctorData>(
                ObjectMapper.Map<List<InviteDoctor>, List<InviteDoctorData>>(result));
        }

        #endregion GetList
    }
}