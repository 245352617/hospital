using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.EMR.Enums
{
    /// <summary>
    /// xml 电子病例模板类型(0=电子病历库的模板，1=我的电子病历模板的模板，2=已存档的患者电子病历)
    /// </summary>
    public enum EXmlCategory : int
    {
        /// <summary>
        /// 模板库的电子病例
        /// </summary>
        [Description("电子病历库的模板")]
        Lib = 0,

        /// <summary>
        /// 我的模板的电子病例
        /// </summary>
        [Description("我的电子病历模板的模板")]
        Template = 1,

        /// <summary>
        /// 已写的患者的电子病例
        /// </summary>
        [Description("已存档的患者电子病历")]
        Archived = 2,
    }
}
