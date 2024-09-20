using BeetleX.Http.Clients;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YiJian.MasterData.MasterData.Departments;
using YiJian.MasterData.MasterData.Users;

namespace YiJian.MasterData.HISUsers;

public interface IDeptAppService
{
    /// <summary>
    /// 住院申请
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [Get(Route = "api/identity/depts")]
    public Task<object> SyncGetDepartmentsAsync([Header("Authorization")] string token);
}