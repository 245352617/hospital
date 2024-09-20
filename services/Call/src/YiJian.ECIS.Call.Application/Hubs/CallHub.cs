using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using YiJian.ECIS.ShareModel.IMServiceEtos.Call;

namespace YiJian.ECIS.Hubs
{
    /// <summary>
    /// 叫号中心强类型集线器接口
    /// </summary>
    public interface ICallClient
    {
        /// <summary>
        /// 刷新叫号列表
        /// </summary>
        /// <returns></returns>
        Task CallingQueueChangedAsync();
    }
    /// <summary>
    /// 叫号服务的即时消息中心
    /// </summary>
    [HubRoute("/signalr-hubs/call")]
    public class CallHub : AbpHub<ICallClient>, ICapSubscribe
    {
        private readonly IHubContext<CallHub> _hubContext;

        /// <summary>
        /// 叫号服务的即时消息中心
        /// </summary>
        /// <param name="hubContext"></param>
        public CallHub(IHubContext<CallHub> hubContext)
        {
            this._hubContext = hubContext;
        }
        /// <summary>
        /// 刷新叫号列表
        /// </summary>
        public const string calling_queue_changed = "calling-queue-changed";

        /// <summary>
        /// 叫号中 呼叫患者到指定科室
        /// </summary>
        public const string calling = "call.calling";

        /// <summary>
        /// 完成叫号
        /// </summary>
        public const string finished = "call.finished";

        /// <summary>
        /// 取消叫号
        /// </summary>
        public const string cancelled = "call.cancelled";

        /// <summary>
        /// 过号
        /// </summary>
        public const string missed_turn = "call.missed.turn";

        /// <summary>
        /// Hub连接
        /// 连接成功后，向当前客户端发送正在叫号中的患者列表
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            // await this._hubContext.Clients.Client(this.Context.ConnectionId).SendAsync("Calling", null);
        }

        /// <summary>
        /// 刷新叫号列表
        /// </summary>
        /// <returns></returns>
        [HubMethodName(calling_queue_changed)]
        public async Task RefreshCallingQuequeAsync()
        {
            await Clients.All.CallingQueueChangedAsync();
        }

        /// <summary>
        /// 广播消息：呼叫患者到指定科室就诊
        /// </summary>
        /// <returns></returns>
        [CapSubscribe(calling)]  // 订阅呼叫患者消息
        public async Task CallingAsync(CallingEto eto)
        {
            await this._hubContext.Clients.All.SendAsync("Calling", eto);
        }

        /// <summary>
        /// 广播消息：过号
        /// </summary>
        /// <returns></returns>
        [CapSubscribe(finished)]
        public async Task CallFinishedAsync(CallingEto eto)
        {
            eto.DepartmentCode = eto.TriageDept;
            eto.DepartmentName = eto.TriageDeptName;
            await this._hubContext.Clients.All.SendAsync("CallingFinished", eto);
        }

        /// <summary>
        /// 广播消息：取消叫号 
        /// </summary>
        /// <returns></returns>
        [CapSubscribe(cancelled)]
        public async Task CallCancelledAsync(CallingEto eto)
        {
            await this._hubContext.Clients.All.SendAsync("CallingCancelled", eto);
        }

        /// <summary>
        /// 过号
        /// </summary>
        /// <returns></returns>
        [CapSubscribe(missed_turn)]
        public async Task MissedTurnAsync(CallingEto eto)
        {
            await this._hubContext.Clients.All.SendAsync("MissedTurn", eto);
        }

        /// <summary>
        /// 广播消息：叫号队列变化
        /// </summary>
        /// <returns></returns>
        //[CapSubscribe("im.patient.queue.changed")]
        [CapSubscribe(calling_queue_changed)]
        public async Task CallQueueChangededAsync()
        {
            await this._hubContext.Clients.All.SendAsync("QueueChanged");
        }

        /// <summary>
        /// 广播消息：叫号队列变化
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("im.patient.queue.changed")]
        public async Task CallQueueChangededV2Async()
        {
            await this._hubContext.Clients.All.SendAsync("QueueChanged");
        }
    }
}
