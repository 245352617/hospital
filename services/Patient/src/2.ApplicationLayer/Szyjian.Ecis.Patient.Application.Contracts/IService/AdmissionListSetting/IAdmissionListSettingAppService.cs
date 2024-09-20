using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public interface IAdmissionListSettingAppService : IApplicationService
    {
        /// <summary>
        /// 通过表格名称，查询入科列表配置
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<IEnumerable<AdmissionListSettingDto>>> GetAdmissionListSettingAsync(AdmissionListSettingWhereInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 保存入科列表配置集合
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> UpdateAdmissionListSettingAsync(
            List<CreateOrUpdateAdmissionListSettingDto> dto, CancellationToken cancellationToken);

        /// <summary>
        /// 重置入科列表配置集合
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> ResetAdmissionListSettingAsync(
            AdmissionListSettingWhereInput input, CancellationToken cancellationToken);
    }
}