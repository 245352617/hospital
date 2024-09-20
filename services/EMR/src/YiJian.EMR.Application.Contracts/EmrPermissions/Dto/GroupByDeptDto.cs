using System.Collections.Generic;

namespace YiJian.EMR.EmrPermissions.Dto
{
    /// <summary>
    /// 根据科室分组的授权人记录
    /// </summary>
    public class GroupByDeptDto
    {
        /// <summary>
        /// 科室编码
        /// </summary> 
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary> 
        public string DeptName { get; set; }

        /// <summary>
        /// 授权人列表
        /// </summary>
        public List<OperatingAccountDto> OperatingAccounts { get; set; } = new List<OperatingAccountDto>(); 
    }

}
