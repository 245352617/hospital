using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.EmrPermissions.Dto;

namespace YiJian.EMR.EmrPermissions
{
    /// <summary>
    /// 权限管理配置
    /// </summary>
    public interface IPermissionAppService : IApplicationService
    {

        /// <summary>
        /// 获取所有的权限列表内容
        /// </summary>
        /// <returns></returns>
        public Task<ResponseBase<List<PermissionDto>>> GetListAsync();

        /// <summary>
        /// 获取指定权限下的所有授权人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseBase<PermissionSampleDto>> GetOperatingAccountAsync(int id);

        /// <summary>
        /// 更新权限内容
        /// </summary> 
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<bool>> UpdateOperatingAccountsAsync(ModifyPermissionDto model);

    }
}
