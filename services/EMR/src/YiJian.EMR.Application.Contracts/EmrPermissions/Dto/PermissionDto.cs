using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.EmrPermissions.Dto
{
    /// <summary>
    /// EMR权限列表信息
    /// </summary>
    public class PermissionDto:EntityDto<int>
    {
        public PermissionDto(int id, string module, string permissionTitle, string remark, string operatingAccounts)
        {
            Id = id;
            Module = module;
            PermissionTitle = permissionTitle;
            Remark = remark;
            OperatingAccounts = operatingAccounts;
        }

        /// <summary>
        /// 模块
        /// </summary> 
        public string Module { get; set; }

        /// <summary>
        /// 权限
        /// </summary> 
        public string PermissionTitle { get; set; }

        /// <summary>
        /// 描述
        /// </summary> 
        public string Remark { get; set; }

        /// <summary>
        /// 操作账号列表
        /// </summary>
        public string OperatingAccounts { get; set; }

    } 
}
