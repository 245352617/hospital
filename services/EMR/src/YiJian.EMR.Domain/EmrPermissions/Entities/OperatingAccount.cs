using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.EMR.EmrPermissions.Entities
{
    /// <summary>
    /// 操作账号信息
    /// </summary>
    [Comment("操作账号信息")]
    public class OperatingAccount : Entity<int>
    {
        public OperatingAccount()
        {

        }

        public OperatingAccount(string deptCode, string deptName, string doctorCode, string doctorName, int permissionId)
        {
            DeptCode = deptCode;
            DeptName = deptName;
            DoctorCode = doctorCode;
            DoctorName = doctorName;
            PermissionId = permissionId;
        }

        /// <summary>
        /// 科室编码
        /// </summary>
        [Comment("科室编码")]
        [StringLength(50)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [Comment("科室名称")]
        [StringLength(50)]
        public string DeptName { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary> 
        [Comment("医生编码")]
        [StringLength(50)]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生
        /// </summary> 
        [Comment("医生")]
        [StringLength(50)]
        public string DoctorName { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        [Comment("权限Id")]
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }

    }

}
