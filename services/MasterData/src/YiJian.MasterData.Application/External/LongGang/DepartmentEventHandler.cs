using BeetleX.Http.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using YiJian.MasterData.HISUsers;
using YiJian.MasterData.MasterData.Departments;
using YiJian.MasterData.MasterData.Users;

namespace YiJian.MasterData.External.LongGang;

[Authorize]
public class DepartmentEventHandler : MasterDataAppService, ILongGangHospitalEventHandler, IDistributedEventHandler<FullDictDepartment>,
           ITransientDependency, IDeptAppService
{
    public IOptionsMonitor<RemoteServices> RemoteServices { get; set; }

    public IHttpContextAccessor Accessor { get; set; }

    private readonly IHttpContextAccessor _httpContext;

    public DepartmentEventHandler(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }
    public async Task HandleEventAsync(FullDictDepartment eventData)
    {
        await  GetTokenAsync();

        var departmentEtoList = JsonConvert.DeserializeObject<List<DepartmentEto>>(
                      JsonConvert.SerializeObject(eventData.DicDatas));
        if (departmentEtoList == null && departmentEtoList.Count() <= 0) return;
        var users = new List<DepartmentEto>();
        var aa = await SyncGetDepartmentsAsync(Accessor.HttpContext?.Request.Headers["Authorization"].ToString());
        foreach (var item in departmentEtoList)
        {
           
        }

    }

    public async Task<object> SyncGetDepartmentsAsync(string token)
    {
        var service = BuildHospitalService();
        var result = await service.SyncGetDepartmentsAsync(token);
        return result;
    }


    private IDeptAppService BuildHospitalService()
    {
        var remoteServices = RemoteServices.CurrentValue;
        HttpCluster httpCluster = new HttpCluster();
        httpCluster.TimeOut = 10 * 60 * 1000;
        var host = remoteServices.Identity.BaseUrl;
        httpCluster.DefaultNode.Add(host);
        var service = httpCluster.Create<IDeptAppService>();
        return service;
    }

    private async Task<string> GetTokenAsync()
    {
        var token = _httpContext.HttpContext.Request.Headers["Authorization"];
        return await Task.FromResult(token.ToString());
    }
}
