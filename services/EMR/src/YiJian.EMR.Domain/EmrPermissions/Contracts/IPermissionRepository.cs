using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.EmrPermissions.Entities;
using YiJian.EMR.Enums;

namespace YiJian.EMR.EmrPermissions.Contracts
{
    /// <summary>
    /// 权限仓储接口
    /// </summary>
    public interface IPermissionRepository: IRepository<Permission, int>
    {
        /// <summary>
        /// 根据医生code获取操作人的权限信息
        /// </summary>
        /// <param name="docktorCode">医生编码</param>
        /// <param name="permissionCode">权限编码</param>
        /// <param name="deptCode">科室编码</param>
        /// <returns></returns>
        public Task<List<Permission>> GetByDoctorCodeAsync(string docktorCode, EPermissionCode permissionCode, string deptCode = "");


        /// <summary>
        /// 根据医生code获取操作人的权限信息
        /// </summary>
        /// <param name="docktorCode">医生编码</param>
        /// <returns></returns>
        public Task<List<Permission>> GetPermissionByDoctorCodeAsync(string docktorCode);

        /// <summary>
        /// 获取所有的权限内容
        /// </summary>
        /// <returns></returns>
        public Task<List<Permission>> GetAllAsync();

        /// <summary>
        /// 获取指定权限下的所有授权人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Permission> GetOperatingAccountAsync(int id);

        /// <summary>
        /// 更新权限内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public Task<bool> UpdateOperatingAccountsAsync(int id, List<OperatingAccount> accounts);
    }

}
