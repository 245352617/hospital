using BeetleX.Http.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.Platform.Dto;

namespace YiJian.Platform
{
    /// <summary>
    /// 认证中心
    /// </summary>
    [JsonFormater]
    public interface IIdentityProxyService
    {
        /// <summary>
        /// 获取平台用户
        /// </summary>
        /// <param name="token"></param>
        /// <param name="deptCodes">科室编码，多个科室的情况下，使用半角逗号分割</param> 
        /// <returns></returns> 
        [Get(Route = "api/identity/users?profession=Doctor&SkipCount=0&MaxResultCount=1000&deptCodes={deptCodes}")]
        public Task<CommonResult<Pagination<PlatformUserDto>>> GetDoctorListAsync([Header("Authorization")] string token, string deptCodes = "");

        /// <summary>
        /// 获取平台科室
        /// </summary>
        /// <returns></returns> 
        [Get(Route = "api/identity/depts")]
        public Task<CommonResult<List<PlatformDeptDto>>> GetDeptListAsync([Header("Authorization")] string token);
    }
}