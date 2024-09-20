using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using YiJian.ECIS.IMService.Hubs;
using YiJian.ECIS.ShareModel.IMServiceEto;
using YiJian.ECIS.ShareModel.IMServiceEtos.Call;

namespace YiJian.ECIS.IMService.Controllers
{
    /// <summary>
    /// 叫号即时通讯
    /// </summary>
    [Route("api/im/call")]
    public class CallController : AbpController
    {
        private readonly IHubContext<CallHub> _hubContext;

        public CallController(IHubContext<CallHub> hubContext)
        {
            this._hubContext = hubContext;
        }

        /// <summary>
        /// 广播消息：刷新叫号列表状态
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("im.call.queue.changed")]  // 订阅叫号列表变化消息
        [CapSubscribe("im.call.department.changed")]  // 订阅科室变化消息
        [HttpPost("refresh_clling_status")]
        public async Task RefreshCallingStatusAsync()
        {
            await _hubContext.Clients.All.SendAsync("CallingQueueChanged");
        }

        /// <summary>
        /// 广播消息：呼叫患者到指定科室就诊
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("im.call.calling")]  // 订阅呼叫患者消息
        [HttpPost("calling")]
        public async Task CallingAsync([FromBody] GenericClientEto<CallingEto> eto)
        {
            // 指定客户端
            if (!string.IsNullOrEmpty(eto.ConnectionId))
            {
                if (eto.Data is null || eto.Data is CallingEto { PatientId: null })
                {
                    await _hubContext.Clients.Client(eto.ConnectionId).SendAsync(eto.Method);
                }
                else
                {
                    await _hubContext.Clients.Client(eto.ConnectionId).SendAsync(eto.Method, eto.Data);
                }
                return;
            }
            if (eto.Data is null || eto.Data is CallingEto { PatientId: null })
            {
                await _hubContext.Clients.All.SendAsync(eto.Method);
            }
            else
            {
                // 向所有客户端发送消息
                await _hubContext.Clients.All.SendAsync(eto.Method, eto.Data);
            }
        }

        /// <summary>
        /// 指定客户端消息：刷新叫号列表状态
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [CapSubscribe("im.call.queue.changed.client")]
        [HttpPost("refresh_calling_status/client")]
        public async Task RefreshClientCallingStatusAsync(DefaultClientEto message)
        {
            await _hubContext.Clients.Client(message.ConnectionId).SendAsync(message.Method);
        }
    }
}
