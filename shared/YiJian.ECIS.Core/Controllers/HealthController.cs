using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace IYiJian.ECIS.Core.Controllers;

/// <summary>
/// 健康检查接口
/// 提供给Consul、SkyWalking检查服务状态的接口
/// </summary>
[Route("/health")]
[Produces("application/json")]
public class HealthController : AbpController
{
    /// <summary>
    /// 健康检查地址
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Index(string id)
    {
        return Content("health");
    }
}