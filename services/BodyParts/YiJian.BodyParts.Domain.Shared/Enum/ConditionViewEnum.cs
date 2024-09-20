using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts
{
    /// <summary>
    /// 全景视图查询状态：近24小时1；近48小时2；近72小时3；自定义0
    /// </summary>
    public enum ConditionViewEnum
    {
        近24小时 = 1,
        近48小时 = 2,
        近72小时 = 3,
        自定义 = 0
    }
}
