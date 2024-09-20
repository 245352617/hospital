using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.EmrPermissions.Dto
{
    /// <summary>
    /// 更新的授权人数据
    /// </summary>
    public class ModifyPermissionDto : EntityDto<int>
    {
        /// <summary>
        /// 授权人信息集合
        /// </summary>
        public List<OperatingAccountBaseDto> OperatingAccountDto { get; set; }
    }

}
