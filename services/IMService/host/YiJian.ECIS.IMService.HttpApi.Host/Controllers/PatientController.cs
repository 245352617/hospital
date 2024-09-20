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
    /// 诊疗即时通讯
    /// </summary>
    [Route("api/im/patient")]
    public class PatientController : AbpController
    {
        private readonly IHubContext<PatientHub> _hubContext;

        public PatientController(IHubContext<PatientHub> hubContext)
        {
            this._hubContext = hubContext;
        }

        /// <summary>
        /// 广播消息：刷新叫号列表状态
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("im.patient.queue.changed")]  // 订阅病患列表变化消息
        [CapSubscribe("modify.patient.info.from.patient.service")]  // 订阅科室变化消息
        [HttpPost("refresh_queue")]
        public async Task RefreshQueueAsync()
        {
            await _hubContext.Clients.All.SendAsync("QueueChanged");
        }

        /// <summary>
        /// 指定客户端消息：刷新叫号列表状态
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [CapSubscribe("im.patient.queue.changed.client")]
        [HttpPost("refresh_queue/client")]
        public async Task RefreshClientQueueAsync(DefaultClientEto message)
        {
            await _hubContext.Clients.Client(message.ConnectionId).SendAsync(message.Method);
        }
    }
}
