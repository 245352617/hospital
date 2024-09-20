using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public interface IDrugDiagnoseAppService : IApplicationService
    {
        /// <summary>
        /// 获取诊断记录字典列表
        /// </summary>
        /// <param name="whereInput"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedResultDto<DrugDiagnoseDto>>> GetDrugDiagnoseListAsync(
            DrugDiagnoseWhereInput whereInput,
            CancellationToken cancellationToken);
    }
}