using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 描    述:危急值服务接口
    /// 创 建 人:杨凯
    /// 创建时间:2023/9/26 17:00:17
    /// </summary>
    public interface ICrisisAppService : IApplicationService
    {
        /// <summary>
        /// 获取危急值列表
        /// </summary>
        /// <param name="queryCrisisDto"></param>
        /// <returns></returns>
        ResponseResult<PagedResultDto<CrisisDto>> GetList(QueryCrisisDto queryCrisisDto);

        /// <summary>
        /// 获取患者危急值列表
        /// </summary>
        /// <param name="queryCrisisDto"></param>
        /// <returns></returns>
        ResponseResult<PagedResultDto<CrisisDto>> GetPatientList(QueryCrisisDto queryCrisisDto);

        /// <summary>
        /// 更新危急值信息
        /// </summary>
        /// <param name="crisisDto"></param>
        /// <returns></returns>
        ResponseResult<bool> UpdateCrisis(CrisisDto crisisDto);
    }
}
