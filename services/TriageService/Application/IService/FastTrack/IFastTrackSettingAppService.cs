using SamJan.MicroService.PreHospital.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IFastTrackSettingAppService : IApplicationService
    {
        /// <summary>
        /// 新增分诊设备信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> CreateFastTrackSettingAsync(CreateOrUpdateFastTrackSettingDto dto);

        /// <summary>
        /// 修改快速通道设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateFastTrackSettingAsync(CreateOrUpdateFastTrackSettingDto dto);

        /// <summary>
        /// 删除快速通道设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteFastTrackSettingAsync(FastTrackSettingWhereInput input);

        /// <summary>
        /// 启用或禁用快速通道设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> IsEnableFastTrackSettingAsync(CreateOrUpdateFastTrackSettingDto dto);

        /// <summary>
        /// 获取快速通道设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<CreateOrUpdateFastTrackSettingDto>> GetFastTrackSettingAsync(FastTrackSettingWhereInput input);

        /// <summary>
        /// 获取快速通道设置列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<List<CreateOrUpdateFastTrackSettingDto>>> GetFastTrackSettingListAsync(FastTrackSettingWhereInput input);
    }
}
