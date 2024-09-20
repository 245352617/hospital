using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.EmrPermissions.Dto
{
    /// <summary>
    /// 授权信息
    /// </summary>
    public class OperatingAccountBaseDto : EntityDto<int?>
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public int PermissionId { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>  
        [StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 医生
        /// </summary>  
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        [StringLength(50)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(50)]
        public string DeptName { get; set; }
    }


}
