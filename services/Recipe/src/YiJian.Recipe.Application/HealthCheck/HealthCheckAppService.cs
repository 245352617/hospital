using System.Threading.Tasks;
using YiJian.Recipes.HealthCheck;

namespace YiJian.Recipe.HealthCheck
{
    /// <summary>
    /// 健康检查 API
    /// </summary>
    public class HealthCheckAppService : RecipeAppService, IHealthCheckAppService
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAsync()
        {
            await Task.CompletedTask;
            return "The service is ok";
        }
    }
}