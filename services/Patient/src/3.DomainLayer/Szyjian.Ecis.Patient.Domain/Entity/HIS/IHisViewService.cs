using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// his视图
    /// </summary>
    public interface IHisViewService : IDomainService
    {
        /// <summary>
        /// 获取his挂号视图
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<V_JHJK_HZLB>> GetRegisterListAsync(CancellationToken cancellationToken = default);
    }
}