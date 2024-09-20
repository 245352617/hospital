using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using YiJian.ECIS.IMService.HubClientContracts;
using YiJian.ECIS.ShareModel.IMServiceEto;
using YiJian.ECIS.ShareModel.IMServiceEtos.Call;

namespace YiJian.ECIS.IMService.Hubs
{
    /// <summary>
    /// 叫号服务的即时消息中心
    /// </summary>
    public class CallHub : AbpHub<ICallClient>
    {
        private readonly ICapPublisher _capPublisher;

        public CallHub(ICapPublisher capPublisher)
        {
            this._capPublisher = capPublisher;
        }

        /// <summary>
        /// Hub连接
        /// 连接成功后，向当前客户端发送正在叫号中的患者列表
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await this._capPublisher.PublishAsync("ws.call.change.consulting-room.request", new ChangeConsultingRoomEto(Context.ConnectionId));
        }

        /// <summary>
        /// 刷新叫号列表
        /// </summary>
        /// <returns></returns>
        [HubMethodName("calling-queue-changed")]
        public async Task RefreshCallingQuequeAsync()
        {
            await Clients.All.CallingQueueChanged();
        }

        [HubMethodName("refresh-calling-queue-self")]
        public async Task RefreshSelfCallingQuequeAsync()
        {
            await this._capPublisher.PublishAsync("im.call.refresh.queue.request", new DefaultClientEto(this.Context.ConnectionId, "RefreshCallingQueque"));
        }

        /// <summary>
        /// 改变诊室
        /// </summary>
        /// <param name="consultingRoomCode">诊室代码</param>
        /// <returns></returns>
        [HubMethodName("change-consulting-room")]
        public async Task ChangeConsultingRoomAsync(string consultingRoomCode)
        {
            await this._capPublisher.PublishAsync("ws.call.change.consulting-room.request", new ChangeConsultingRoomEto(Context.ConnectionId, consultingRoomCode));
        }
    }
}
