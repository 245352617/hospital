using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.EMR.Enums
{
    /// <summary>
    /// 电子文书分类
    /// </summary>
    public enum EClassify
    {
        /// <summary>
        /// 电子病历
        /// </summary>
        [Description("电子病历")] 
        EMR = 0,

        /// <summary>
        /// 文书
        /// </summary>
        [Description("文书")]
        Document = 1,

    }
}
