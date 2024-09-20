using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.Handover
{
    /// <summary>
    ///交接班设置API
    /// </summary>
    [Authorize]
    public class ShiftHandoverSettingAppService : HandoverAppService, IShiftHandoverSettingAppService
    {
        private readonly ICurrentUser _iCurrentUser;
        private readonly IShiftHandoverSettingRepository _iShiftHandoverSettingRepository;
        private readonly ShiftHandoverSettingManager _shiftHandoverSettingManager;

        public ShiftHandoverSettingAppService(ICurrentUser iCurrentUser,
            IShiftHandoverSettingRepository iShiftHandoverSettingRepository,
            ShiftHandoverSettingManager shiftHandoverSettingManager)
        {
            _iCurrentUser = iCurrentUser;
            _iShiftHandoverSettingRepository = iShiftHandoverSettingRepository;
            _shiftHandoverSettingManager = shiftHandoverSettingManager;
        }

        /// <summary>
        /// 新增或修改班次
        /// </summary>
        /// <param name="dto">dto.id传参表示修改，不传该参数表示新增</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ShiftHandoverSettingData> SaveShiftHandoverSettingAsync(
            ShiftHandoverSettingCreationOrUpdate dto, CancellationToken cancellationToken)
        {
            if (dto.Id == Guid.Empty)
            {
                var sort = await (await _iShiftHandoverSettingRepository.GetQueryableAsync()).Where(w => w.Type == dto.Type)
                    .CountAsync(cancellationToken: cancellationToken);
                var result = await _shiftHandoverSettingManager.CreateAsync(dto.Id, dto.CategoryCode,
                    dto.CategoryName, dto.ShiftName, dto.StartTime, dto.EndTime, dto.IsEnable, dto.MatchingColor,
                    sort + 1,
                    dto.Type, _iCurrentUser.FindClaimValue("fullName"));
                return ObjectMapper.Map<ShiftHandoverSetting, ShiftHandoverSettingData>(result);
            }
            else
            {
                var result = await _shiftHandoverSettingManager.UpdateAsync(dto.Id, dto.CategoryCode,
                    dto.CategoryName, dto.ShiftName, dto.StartTime, dto.EndTime, dto.IsEnable, dto.MatchingColor,
                    _iCurrentUser.FindClaimValue("fullName"));
                return ObjectMapper.Map<ShiftHandoverSetting, ShiftHandoverSettingData>(result);
            }
        }

        /// <summary>
        /// 根据id获取班次信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task<ShiftHandoverSettingData> GetShiftHandoverSettingDataAsync(Guid id,
            CancellationToken cancellationToken)
        {
            var model = await (await _iShiftHandoverSettingRepository.GetQueryableAsync()).AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken);
            if (model == null)
            {
                throw new EcisBusinessException("数据不存在");
            }

            var shift = ObjectMapper.Map<ShiftHandoverSetting, ShiftHandoverSettingData>(model);
            return shift;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task DeleteShiftHandoverSettingAsync(Guid id, CancellationToken cancellationToken)
        {
            if (!await (await _iShiftHandoverSettingRepository.GetQueryableAsync()).AnyAsync(a => a.Id == id, cancellationToken: cancellationToken))
            {
                throw new EcisBusinessException("数据不存在");
            }

            await _iShiftHandoverSettingRepository.DeleteAsync(id, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ShiftHandoverSettingDataList>> GetShiftHandoverSettingListAsync(
            ShiftHandoverSettingInput input, CancellationToken cancellationToken)
        {
            var list = await (await _iShiftHandoverSettingRepository.GetQueryableAsync()).Where(w => w.Type == input.Type)
                .WhereIf(input.IsEnable != -1, x => x.IsEnable == Convert.ToBoolean(input.IsEnable))
                .PageBy(input.SkipCount, input.Size)
                .OrderBy(o => o.Sort)
                .ToListAsync(cancellationToken: cancellationToken);
            var shift = ObjectMapper.Map<List<ShiftHandoverSetting>, List<ShiftHandoverSettingDataList>>(list);
            var count = await (await _iShiftHandoverSettingRepository.GetQueryableAsync()).Where(w => w.Type == input.Type)
                .CountAsync(cancellationToken: cancellationToken);
            return new PagedResultDto<ShiftHandoverSettingDataList>(count, shift.AsReadOnly());
        }
    }
}