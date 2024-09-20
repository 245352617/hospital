using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace YiJian.ECIS.IMService.Hubs
{
    //[Authorize]
    public class ChatHub : AbpHub
    {
        public ChatHub()
        {
        }

        [HubMethodName("send-message")]
        public async Task<object> SendMessageAsync(string targetUserName, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
            return new { id = "123", value = "test-value" };
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("connected");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("disconnected");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
