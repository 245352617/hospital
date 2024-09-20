using BeetleX.Http.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using YiJian.MasterData.HISUsers;
using YiJian.MasterData.MasterData.Users;

namespace YiJian.MasterData.External.LongGang;

public class UserEventHandler : MasterDataAppService, ILongGangHospitalEventHandler, IDistributedEventHandler<FullDictUser>,
         ITransientDependency, IUserAppService
{
    public IOptionsMonitor<RemoteServices> RemoteServices { get; set; }

    public  IHttpContextAccessor Accessor { get; set; }

    public async Task HandleEventAsync(FullDictUser eventData)
    {
        var userList = JsonConvert.DeserializeObject<List<UserEto>>(
                      JsonConvert.SerializeObject(eventData.DicDatas));
        if (userList == null && userList.Count() <= 0) return;
        //var users = new List<UserEto>();
        foreach (var item in userList)
        {
          _ = await SyncGetUsersAsync(Accessor.HttpContext?.Request.Headers["Authorization"].ToString());
        }


    }

    public async Task<CommonResult<HISUsers.Users>> SyncGetUsersAsync(string token)
    {
        var service = BuildHospitalService();
        var result = await service.SyncGetUsersAsync(token);
        return result;
    }

    private IUserAppService BuildHospitalService()
    {
        var remoteServices = RemoteServices.CurrentValue;
        HttpCluster httpCluster = new HttpCluster();
        httpCluster.TimeOut = 10 * 60 * 1000;
        var host = remoteServices.Identity.BaseUrl;
        httpCluster.DefaultNode.Add(host);
        var service = httpCluster.Create<IUserAppService>();
        return service;
    }

    /// <summary>
    /// 获取token
    /// </summary>
    /// <returns></returns>
    private async Task<string> GetTokenAsync()
    {
        var token = Accessor.HttpContext.Request.Headers["Authorization"];
        return await Task.FromResult(token.ToString());
    }
}
