using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.Handover
{
    public interface IShiftHandoverSettingAppService : IApplicationService
    {
        /// <summary>
        /// 新增或修改班次
        /// </summary>
        /// <param name="dto">dto.id传参表示修改，不传该参数表示新增</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ShiftHandoverSettingData> SaveShiftHandoverSettingAsync(
            ShiftHandoverSettingCreationOrUpdate dto, CancellationToken cancellationToken);

        /// <summary>
        /// 根据id获取班次信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        Task<ShiftHandoverSettingData> GetShiftHandoverSettingDataAsync(Guid id, CancellationToken cancellationToken=default);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedResultDto<ShiftHandoverSettingDataList>> GetShiftHandoverSettingListAsync(
            ShiftHandoverSettingInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="EcisBusinessException"></exception>
        Task DeleteShiftHandoverSettingAsync(Guid id, CancellationToken cancellationToken);
    }
}