using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;
using YiJian.ECIS.Call.Domain.CallCenter;

namespace YiJian.ECIS.Call.CallCenter
{
    /// <summary>
    /// 规约：大屏叫号查询基本条件
    /// </summary>
    public class ClientCallInfoSpecification : Specification<CallInfo>
    {
        public override Expression<Func<CallInfo, bool>> ToExpression()
        {
            return x =>
                x.IsShow
                && x.TriageTarget == "TriageDirection_006"  //过滤抢救留观患者
                && (x.CallStatus < CallStatus.Pause);
        }
    }
}
