using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.Recipes.HealthCheck
{
    public interface IHealthCheckAppService : IApplicationService
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        Task<string> GetAsync();
    }
}