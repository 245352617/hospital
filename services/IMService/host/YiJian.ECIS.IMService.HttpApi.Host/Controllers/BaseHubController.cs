using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;

namespace YiJian.ECIS.IMService.Controllers
{
    /// <summary>
    /// 专门用于接收后端 MQ 消息并转发成 WebSocket 消息发送到客户端的控制器基类
    /// by: ywlin-20211026
    /// </summary>
    /// <typeparam name="T">SignalR 的 Hub</typeparam>
    public class BaseHubController<T> : AbpController where T : Hub
    {
        public BaseHubController()
        {
        }

        protected IHubContext<T> HubContext => LazyServiceProvider.LazyGetService<IHubContext<T>>();
    }
}
