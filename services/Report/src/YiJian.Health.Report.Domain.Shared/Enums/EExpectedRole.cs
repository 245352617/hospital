using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.Health.Report.Enums
{
    /// <summary>
    /// 期望展示的权限 -1=拒绝，没有任何权限  , 0=管理员 , 1=医生 , 2=护士
    /// </summary>
    public enum EExpectedRole
    {
        /// <summary>
        /// 拒绝，没有任何权限
        /// </summary>
        [Description("拒绝，没有任何权限")]
        Refused = -1,

        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Admin = 0,

        /// <summary>
        /// 医生
        /// </summary>
        [Description("医生")]
        Doctor = 1,

        /// <summary>
        /// 护士
        /// </summary>
        [Description("护士")]
        Nurse = 2,
    }
}
