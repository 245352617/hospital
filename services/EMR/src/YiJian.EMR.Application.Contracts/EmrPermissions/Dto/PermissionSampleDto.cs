using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.EmrPermissions.Dto
{
    /// <summary>
    /// 权限操作人信息
    /// </summary>
    public class PermissionSampleDto : EntityDto<int>
    {
        /// <summary>
        /// 根据科室分组的授权人记录
        /// </summary>
        public List<GroupByDeptDto> GroupByDeptOperatingAccounts { get; set; } = new List<GroupByDeptDto>();

        /// <summary>
        /// 全部展示当前权限目录下的所有授权人集合
        /// </summary>
        public List<OperatingAccountBaseDto> AllOperatingAccounts { get;set;} = new List<OperatingAccountBaseDto>();
    }

}
