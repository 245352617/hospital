using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 心跳检测
    /// </summary>
    public class HealthCheckAppService : EcisPatientAppService, IHealthCheckAppService
    {
        public HealthCheckAppService()
        {
        }

        /// <summary>
        /// http get 心跳检测
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<string>> GetAsync()
        {
            return await Task.FromResult(RespUtil.Ok(data: "The health check of the service is normal."));
        }
    }
}