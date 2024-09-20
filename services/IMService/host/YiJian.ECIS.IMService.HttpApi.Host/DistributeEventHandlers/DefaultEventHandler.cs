using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using YiJian.ECIS.IMService.Hubs;
using YiJian.ECIS.ShareModel.IMServiceEto;

namespace YiJian.ECIS.IMService.DistributeEventHandlers
{
    /// <summary>
    /// 支持 abp EventBus 模式
    /// </summary>
    public class DefaultEventHandler : IDistributedEventHandler<DefaultBroadcastEto>
    {
        private readonly IHubContext<DefaultHub> _hubContext;

        public DefaultEventHandler(IHubContext<DefaultHub> hubContext)
        {
            this._hubContext = hubContext;
        }

        public async Task HandleEventAsync(DefaultBroadcastEto eventData)
        {
            Console.WriteLine(eventData);
            await this._hubContext.Clients.All.SendAsync(eventData.Method, eventData.Data);
        }
    }
}
