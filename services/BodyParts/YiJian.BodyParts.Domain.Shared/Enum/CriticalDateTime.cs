using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts
{
    /// <summary>
    /// 危急值时间查询(近3天 = 0,本周 = 1,上周 = 2,本月 = 3,上月 = 4,全部 = 5)
    /// </summary>
    public enum CriticalDateTime
    {
        近3天 = 0,
        本周 = 1,
        上周 = 2,
        本月 = 3,
        上月 = 4,
        全部 = 5
    }
}
