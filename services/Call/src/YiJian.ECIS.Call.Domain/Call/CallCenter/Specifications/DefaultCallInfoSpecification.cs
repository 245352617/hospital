using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;
using YiJian.ECIS.Call.Domain.CallCenter;

namespace YiJian.ECIS.Call.CallCenter
{
    /// <summary>
    /// 规约：当前叫号列表
    /// </summary>
    public class DefaultCallInfoSpecification : Specification<CallInfo>
    {
        public override Expression<Func<CallInfo, bool>> ToExpression()
        {
            return x => x.IsShow;
                // 当天登记的患者
                //&& (x.LogDate == DateTime.Today || (x.LogTime.HasValue && x.LogTime.Value.Date == DateTime.Today));
        }
    }
}
