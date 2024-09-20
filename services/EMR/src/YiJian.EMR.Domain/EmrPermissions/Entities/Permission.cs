using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.EMR.Enums;

namespace YiJian.EMR.EmrPermissions.Entities
{
    /// <summary>
    /// 权限模型
    /// </summary>
    [Comment("EMR权限管理模型")]
    public class Permission : FullAuditedAggregateRoot<int>
    {
        /// <summary>
        /// 我的权限代码,0=通用模板权限 ，1=科室模板权限
        /// </summary>
        [Comment("权限代码")]
        public EPermissionCode PermissionCode { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        [Comment("模块")]
        [StringLength(50)]
        public string Module { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [Comment("权限")]
        [StringLength(100)]
        public string PermissionTitle { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Comment("描述")]
        [StringLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 操作账号列表
        /// </summary>
        public virtual List<OperatingAccount> OperatingAccounts { get; set; } = new List<OperatingAccount>();

    }

}
