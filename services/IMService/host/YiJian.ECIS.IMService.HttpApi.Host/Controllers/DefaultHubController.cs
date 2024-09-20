using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.IMService.Hubs;
using YiJian.ECIS.ShareModel.IMServiceEto;

namespace YiJian.ECIS.IMService.Controllers
{
    /// <summary>
    /// SignalR 默认通讯控制器
    /// 后端微服务向前端应用发送消息的通用实现
    /// 如果消息通知携带过多的业务逻辑，需要自定义实现
    /// </summary>
    [Route("api/im/default")]
    public class DefaultHubController : BaseHubController<DefaultHub>
    {
        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [CapSubscribe("im.default.send")]
        [HttpPost("send")]
        public async Task SendAsync(DefaultBroadcastEto message)
        {
            await HubContext.Clients.All.SendAsync(message.Method, message.Data);
        }

        /// <summary>
        /// 指定客户端消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [CapSubscribe("im.default.send.client")]
        [HttpPost("send/client")]
        public async Task SendToClientAsync(DefaultClientEto message)
        {
            await HubContext.Clients.Client(message.ConnectionId).SendAsync(message.Method, message.Data);
        }

        /// <summary>
        /// 指定用户消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [CapSubscribe("im.default.send.user")]
        [HttpPost("send/user")]
        public async Task SendToUserAsync(DefaultUserEto message)
        {
            await HubContext.Clients.User(message.UserId).SendAsync(message.Method, message.Data);
        }
    }
}
