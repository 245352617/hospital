using System;
using System.Threading;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IFastTrackRegisterInfoAppService : IApplicationService
    {
        /// <summary>
        /// 根据Id获取患者快速通道登记信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JsonResult<FastTrackRegisterInfoDto>> GetAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据输入项获取患者快速通道登记信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JsonResult<PagedResultDto<FastTrackRegisterInfoDto>>> GetListAsync(FastTrackRegisterInfoInput input, CancellationToken cancellationToken = default);

        /// <summary>
        /// 提交患者快速通道登记信息
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JsonResult> SaveFastTrackRegisterInfoAsync(CreateOrUpdateFastTrackRegisterInfoDto dto, CancellationToken cancellationToken = default);
    }
}