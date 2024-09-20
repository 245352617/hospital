using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 心跳检测
    /// </summary>
    public interface IHealthCheckAppService : IApplicationService
    {
        /// <summary>
        /// http get 心跳检测
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<string>> GetAsync();
    }
}