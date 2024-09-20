using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using YiJian.ECIS.IMService.HubClientContracts;
using YiJian.ECIS.ShareModel.IMServiceEto;

namespace YiJian.ECIS.IMService.Hubs
{
    /// <summary>
    /// 诊疗服务的即时消息中心
    /// </summary>
    public class PatientHub : AbpHub
    {
        private readonly ICapPublisher _capPublisher;

        public PatientHub(ICapPublisher capPublisher)
        {
            this._capPublisher = capPublisher;
        }
    }
}
