using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Recipe
{
    /// <summary>
    /// 个人偏好配置 API 
    /// </summary>
    [Authorize]
    public class UserSettingAppService : RecipeAppService, IUserSettingAppService
    {
        private readonly IUserSettingRepository _personSettingRepository;

        /// <summary>
        /// 个人偏好配置 API 
        /// </summary>
        /// <param name="personSettingRepository"></param>
        public UserSettingAppService(IUserSettingRepository personSettingRepository)
        {
            _personSettingRepository = personSettingRepository;
        }

        /// <summary>
        /// 获取个人配置列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserSettingDto>> GetListAsync(string userName)
        {
            var list = await (await _personSettingRepository.GetQueryableAsync()).Where(p => p.UserName == userName).ToListAsync();
            var map = ObjectMapper.Map<List<UserSetting>, List<UserSettingDto>>(list);
            return map;
        }

        /// <summary>
        /// 获取个人配置
        /// </summary>
        /// <returns></returns>
        public async Task<UserSettingDto> GetAsync(string userName, Guid id)
        {
            var entity = await _personSettingRepository.FindAsync(p => p.UserName == userName && p.Id == id);
            var map = ObjectMapper.Map<UserSetting, UserSettingDto>(entity);
            return map;
        }

        /// <summary>
        /// 根据编码获取个人配置
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<UserSettingDto> GetByCodeAsync(string userName, string code)
        {
            var entity = await _personSettingRepository.FirstOrDefaultAsync(p => p.UserName == userName && p.Code == code);
            var map = ObjectMapper.Map<UserSetting, UserSettingDto>(entity);
            return map;
        }

        /// <summary>
        /// 保存个人配置
        /// </summary>
        /// <returns></returns>
        public async Task SavePersonSettingsAsync(List<UserSettingDto> personSettingDtos)
        {
            // 个人已存在的配置项
            var userSettings = await _personSettingRepository.GetUserSettingsAsync(CurrentUser.UserName);
            var entityList = ObjectMapper.Map<List<UserSettingDto>, List<UserSetting>>(personSettingDtos);
            // 需要更新的配置项
            var updateList = userSettings.Where(x => entityList.Any(y => y.Code == x.Code && y.GroupCode == x.GroupCode));
            foreach (var entity in updateList)
            {
                var currentItem = entityList.FirstOrDefault(x => x.Code == entity.Code && x.GroupCode == entity.GroupCode);
                entity.Value = currentItem.Value;
            }
            // 用户配置没有的，新增一个配置项
            var addList = entityList.Where(x => !userSettings.Any(y => y.Code == x.Code && y.GroupCode == x.GroupCode));

            await _personSettingRepository.UpdateManyAsync(updateList);
            await _personSettingRepository.InsertManyAsync(addList);
        }

        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserSettingDto>> GetSystemAsync()
        {
            List<UserSetting> systemSettings = await _personSettingRepository.GetListAsync(x => x.UserName == "system");
            List<UserSettingDto> systemSettingDtos = ObjectMapper.Map<List<UserSetting>, List<UserSettingDto>>(systemSettings);
            return systemSettingDtos;
        }

        /// <summary>
        /// 保存系统配置
        /// </summary>
        /// <param name="systemSettingDtos"></param>
        /// <returns></returns>
        public async Task<bool> SaveSystemSettingsAsync(List<UserSettingDto> systemSettingDtos)
        {
            List<UserSetting> systemSettings = await _personSettingRepository.GetListAsync(x => x.UserName == "system");

            foreach (UserSetting systemSetting in systemSettings)
            {
                UserSettingDto systemSettingDto = systemSettingDtos.FirstOrDefault(x => x.Code == systemSetting.Code);
                if (systemSettingDto == null) continue;

                systemSetting.Value = systemSettingDto.Value;
            }

            if (systemSettings.Any()) await _personSettingRepository.UpdateManyAsync(systemSettings);
            return true;
        }
    }
}