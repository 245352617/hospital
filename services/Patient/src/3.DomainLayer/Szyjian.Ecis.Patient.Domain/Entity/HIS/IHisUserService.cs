using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 诊疗记录领域服务接口
    /// </summary>
    public interface IHisUserService : IDomainService
    {
        /// <summary>
        /// 获取his用户信息
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<His_Users>> GetHisUsersAsync(CancellationToken cancellationToken = default);
    }
}