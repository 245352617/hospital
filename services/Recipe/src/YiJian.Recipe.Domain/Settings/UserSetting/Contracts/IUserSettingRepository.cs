using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Recipe
{
    public interface IUserSettingRepository : IRepository<UserSetting, Guid>
    {
        /// <summary>
        /// 获取用户的所有个人配置信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<List<UserSetting>> GetUserSettingsAsync(string username);
    }
}