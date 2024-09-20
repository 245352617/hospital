using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.EMR.Enums
{
    /// <summary>
    /// 字符类型 Chars =0 , Images = 1
    /// </summary>
    public enum ECharacteTyper
    {
        /// <summary>
        /// 字符
        /// </summary>
        [Description("字符")] 
        Chars =0,

        /// <summary>
        /// 图片（base64）
        /// </summary>
        [Description("图片（base64）")]
        Images = 1,

    }
}
