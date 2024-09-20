using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;
using YiJian.ECIS.Call.Domain.CallCenter;

namespace YiJian.ECIS.Call.CallCenter
{
    /// <summary>
    /// 规约：叫号中的患者
    /// </summary>
    public class CallingInfoSpecification : Specification<CallInfo>
    {
        public override Expression<Func<CallInfo, bool>> ToExpression()
        {
            return x =>
                x.IsShow 
                // 未叫号的
                && x.CallStatus == CallStatus.NotYet;
        }
    }
}
