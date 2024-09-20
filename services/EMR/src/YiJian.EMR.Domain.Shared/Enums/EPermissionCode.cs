using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.EMR.Enums
{
    /// <summary>
    /// 我的权限代码,0=通用模板权限 ，1=科室模板权限
    /// </summary>
    public enum EPermissionCode
    {
        /// <summary>
        /// 0=通用模板权限
        /// </summary> 
        GeneralTemplate = 0,

        /// <summary>
        /// 1=科室模板权限
        /// </summary>
        DepartmentTemplate = 1,  
    }
}
