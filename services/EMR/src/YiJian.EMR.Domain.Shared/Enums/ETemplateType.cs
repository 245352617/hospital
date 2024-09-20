using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.EMR.Enums
{
    /// <summary>
    /// 模板类型，0=通用，1=科室，2=个人
    /// </summary>
    public enum ETemplateType
    {
        /// <summary>
        /// 通用模板
        /// </summary>
        [Description("通用模板")] 
        General = 0,

        /// <summary>
        /// 科室模板
        /// </summary>
        [Description("科室模板")] 
        Department = 1,

        /// <summary>
        /// 个人模板
        /// </summary>
        [Description("个人模板")] 
        Personal = 2, 
    }
}
