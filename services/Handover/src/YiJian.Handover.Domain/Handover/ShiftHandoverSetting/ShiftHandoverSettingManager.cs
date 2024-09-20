using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.Handover
{
    public class ShiftHandoverSettingManager : DomainService
    {
        private readonly IShiftHandoverSettingRepository _iShiftHandoverSettingRepository;

        public ShiftHandoverSettingManager(IShiftHandoverSettingRepository iShiftHandoverSettingRepository)
        {
            _iShiftHandoverSettingRepository = iShiftHandoverSettingRepository;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryCode">类别编码</param>
        /// <param name="categoryName">类别名称</param>
        /// <param name="shiftName">班次名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="matchingColor">匹配颜色</param>
        /// <param name="sort">排序</param>
        /// <param name="type">类型，医生1，护士0</param>
        /// <param name="creationName">创建人名称</param>
        /// <returns></returns>
        public async Task<ShiftHandoverSetting> CreateAsync(Guid id, string categoryCode, string categoryName,
            string shiftName,
            string startTime, string endTime, bool isEnable, string matchingColor, int sort, int type,
            string creationName = null)
        {
            return await _iShiftHandoverSettingRepository.InsertAsync(new ShiftHandoverSetting(id, categoryCode,
                categoryName, shiftName, startTime, endTime, isEnable, matchingColor, sort, type, creationName));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryCode">类别编码</param>
        /// <param name="categoryName">类别名称</param>
        /// <param name="shiftName">班次名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="matchingColor">匹配颜色</param>
        /// <param name="modificationName">修改人名称</param>
        /// <returns></returns>
        public async Task<ShiftHandoverSetting> UpdateAsync(Guid id, string categoryCode, string categoryName,
            string shiftName,
            string startTime, string endTime, bool isEnable, string matchingColor, string modificationName = null)
        {
            var model = await ( await _iShiftHandoverSettingRepository.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            if (model == null)
            {
                throw new EcisBusinessException("数据不存在");
            }

            model.Edit(id, categoryCode,
                categoryName, shiftName, startTime, endTime, isEnable, matchingColor, modificationName);
            return await _iShiftHandoverSettingRepository.UpdateAsync(model);
        }
    }
}