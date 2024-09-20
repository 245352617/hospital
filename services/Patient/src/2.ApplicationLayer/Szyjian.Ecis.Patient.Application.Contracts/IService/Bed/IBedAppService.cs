using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public interface IBedAppService : IApplicationService
    {
        /// <summary>
        /// 获取病床列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<IEnumerable<BedDto>>> GetBedListAsync(BedWhereInput input,
            CancellationToken cancellationToken);

        /// <summary>
        /// 新增或修改床位
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> CreateOrUpdateBedAsync(CreateOrUpdateBedDto dto,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除床位
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> DeleteBedAsync(BedWhereInput input,
            CancellationToken cancellationToken);

        /// <summary>
        /// 根据id获取床位信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<BedDto>> GetBedAsync(BedWhereInput input,
            CancellationToken cancellationToken);
    }
}