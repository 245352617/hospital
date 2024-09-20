using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public interface IDictionariesAppService : IApplicationService
    {
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<IEnumerable<DictionariesDto>>> GetDictionariesListAsync(
            DictionariesWhereInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 新增或者修改实体
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> CreateOrUpdateDictionariesAsync(CreateOrUpdateDictionariesDto dto,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据id删除字典
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> DeleteDictionariesAsync(DictionariesWhereInput input,
            CancellationToken cancellationToken);

        /// <summary>
        /// 根据id获取字典信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<DictionariesDto>> GetDictionariesAsync(DictionariesWhereInput input,
            CancellationToken cancellationToken);
    }
}