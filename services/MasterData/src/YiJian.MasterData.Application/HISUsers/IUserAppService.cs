using BeetleX.Http.Clients;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YiJian.MasterData.MasterData.Users;

namespace YiJian.MasterData.HISUsers;

public interface IUserAppService
{
    /// <summary>
    /// 住院申请
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [Get(Route = "api/identity/users?SkipCount=0&MaxResultCount=10000")]
    public Task<CommonResult<Users>> SyncGetUsersAsync([Header("Authorization")] string token);
}
